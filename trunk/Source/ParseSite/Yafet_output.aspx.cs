using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class output : System.Web.UI.Page
{
    //To store an url's name from URLTextBox on the source page (default.aspx)
    private string url;
    //To store an url's root name
    private string rootUrl;
    //To store an html source code in text type from a given url
    private string htmlText;
    //Number of Element found;
    private int element = 0;

    /*
     * Event's Trigger: automatically, after redirection from source page
     * Action: shows the output from the url given (images and texts) 
     */
    protected void Page_Load(object sender, EventArgs e){
        //Get a reference to the previous page (source page = default.aspx)
        if (Page.PreviousPage != null){
            //Find URLTextBox control from default.aspx
            TextBox urlTextBox = (TextBox)Page.PreviousPage.FindControl("URLTextBox");
            if (urlTextBox != null){
                //Assign the value of URLTextBox to string url
                url = urlTextBox.Text;
                //if ensureHttp() catches exception, silent the error, and do nothing
                if (ensureHttp() == -1 || getRootName() == -1){
                    return;
                }
                Label1.Text = url;
            }
            else{
                //If can't get the URLTextBox from source, shows an error message and stop/return
                Page.Response.Write("urlTextBox == null<br>");
                return;
            }
        }
        else{
            //If reference == null, silent this error and simply do nothing/return void
            //Page.Response.Write("Page.Previous == null");
            return;
        }

        //Check if in the process of opening up the url and storing html source, there is an error
        if (getHtmlSource() == -1 || saveHtmlSource() == -1){
            return;
        }

        //parseImageTag();
    }

    /*
     * Event's Trigger: on click of the parseButton (of output.aspx, not default.aspx)
     * Action: shows the output from the url given (images and texts) to output.aspx
     */
    protected void parseButton_Click(object sender, EventArgs e){
        //Assign the value of URLTextBox (url's name) to string url
        url = URLTextBox.Text;
        //if ensureHttp() catches exception, silent the error, and do nothing
        if (ensureHttp() == -1 || getRootName() == -1){
            return;
        }
        Label1.Text = url;

        //Check if in the process of opening up the url and storing html source, there is an error
        if (getHtmlSource() == -1 || saveHtmlSource() == -1){
            return;
        }

        parseImageTag();
    }

    private int parseImageTag(){
        int loc = 0; //Location of the <img...> tag
        string tag = "<img"; //Flag for finding <img...> using string.IndexOf(..)
        string whole = null; //String to store the whole <img...> tag
        //len = length of the <img...> tag, end = location of ending '>' bracket to close <img...> tag 
        int len = 0, end = 0;
        /* ----- SQL RELATED VARIABLES & PREPARATION TO ACCESS TABLE ----- */
        //To store the connection to the database
        SqlConnection conn;
        //SQL command
        SqlCommand comm, commOut;
        //SQL Reader for Data Binding
        SqlDataReader reader;
        //Flag to exit from try-catch-finally
        int ok = 1;

        //Define database connection
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); 
        //Create Command
        comm = new SqlCommand(
            "INSERT INTO imageTable (webPage, element, sub, type, tag, loc)" +
            "VALUES (@webPage, @element, @sub, @type, @tag, @loc)", conn);
        //Add command parameters
        comm.Parameters.Add("@webPage", SqlDbType.VarChar);
        comm.Parameters.Add("@element", SqlDbType.Int);
        comm.Parameters.Add("@sub", SqlDbType.Char);
        comm.Parameters.Add("@type", SqlDbType.VarChar);
        comm.Parameters.Add("@tag", SqlDbType.VarChar);
        comm.Parameters.Add("@loc", SqlDbType.Int);
        /* ----- SQL PREPARATION END ----- */

        //Locate <img...> tag
        loc = htmlText.IndexOf(tag, StringComparison.OrdinalIgnoreCase);
        while(loc != -1){ //If FOUND
            //Page.Response.Write("location = "+loc+"<br>");
            //Increment number of element
            element++;
            //Get closing bracket location
            end = htmlText.IndexOf('>', loc);
            len = end - loc + 1;
            //copy the whole <img...> tag to string whole
            whole = htmlText.Substring(loc, len);
            //Page.Response.Write("tag = "+whole+"<br><br>");

            /* ----- SQL QUERY: INSERT TO TABLE: imageTable ----- */
            //Set Parameters Value
            comm.Parameters["@webPage"].Value = rootUrl;
            comm.Parameters["@element"].Value = element;
            comm.Parameters["@sub"].Value = 'a';
            comm.Parameters["@type"].Value = "Image";
            comm.Parameters["@tag"].Value = whole;
            comm.Parameters["@loc"].Value = loc;
            //Enclose database code in Try-Catch-Finally
            try{
                //Open the connection
                conn.Open();
                //Execute the command
                comm.ExecuteNonQuery();
                ok = 1;
            }
            catch{
                //Display error message
                Page.Response.Write("SQL INSERTION ERROR<br>");
                ok = 0;
            }
            finally{
                //Close the Connection
                conn.Close();
            }
            /* ----- SQL QUERY END ----- */
            if (ok == 0)
                return -1;

            //Find next <img...> tag from "end" location
            loc = htmlText.IndexOf(tag, end, StringComparison.OrdinalIgnoreCase);
        }
        //Page.Response.Write("location = " + loc+"<br>");

        //TEST SQL QUERY
        //Define database connection
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //SQL command to output the table
        commOut = new SqlCommand("SELECT webPage, element, sub, type, tag, loc FROM imageTable", conn);
        try{
            //Open the connection
            conn.Open();
            //Execute the command
            reader = commOut.ExecuteReader();
            //Fill the grid with data
            GridView1.DataSource = reader;
            GridView1.DataBind();
            //Close the reader
            reader.Close();
        }
        finally{
            //Close the connection
            conn.Close();
        }

        /*
        //Development process
        do{
            //Parsing for either "<img..." or "<h1..."
            locImg=htmlText.IndexOf("<img", loc, StringComparison.OrdinalIgnoreCase);
            Page.Response.Write("location Img = "+locImg+"\r\n");
            locText=htmlText.IndexOf("<h1", loc, StringComparison.OrdinalIgnoreCase);
            Page.Response.Write("location H1 = "+locText+"\r\n");

            //Check location whichever comes first
            if (locImg != -1){
                if (locText == -1 || locImg < locText){
                    loc = locImg + 1;
                }else{
                    loc = locText + 1;
                }
            }else{

            }
        */
        return 0;
    }

    /*
     * Method: saving html source code (htmlText), after getHtmlSource(), to a filename.txt
     * Return: 0 if succesfull, -1 if exception occurs
     */
    private int saveHtmlSource(){
        //Checking html source
        if (htmlText != null && htmlText.Length != 0){
            //URL length validation - not implemented yet
            string filename = null;

            //Just in case (url always start with http:// , but who knows) 
            //7 = to exclude "http://"
            //Make filename from url, convention: (url's name without http://).txt
            if(url.StartsWith("http://")){
                filename = url.Substring(7, url.Length - 7);
                filename = Regex.Replace(filename, @"[\/?:*<>""|]", ".");
                filename += ".txt";
            }else{ 
                filename = Regex.Replace(url, @"[\/?:*<>|]", ".");
                filename += ".txt";
            }

            //write to the *.txt
            try{
                StreamWriter writer = new StreamWriter(Server.MapPath(filename));
                writer.Write(htmlText);
                writer.Close();
            }
            catch(Exception){
                Page.Response.Write("Writing to file error<br>");
                return -1;
            }
            /*
            //Debugging Purpose
            Page.Response.Write(filename+", url.Length =  "+url.Length);
             */
            return 0;
        }
        return -1;
    }

    /*
     * Method: to request and open up a given url, store html source into string htmlText
     * Return: 0 if succesfull, -1 if exception occurs
     */
    private int getHtmlSource(){
        /* Development process
        //Check for "http://"
        try{
            if(!url.Contains("http://")){
                //Page.Response.Write("no http://<br>");
                url = url.Insert(0,"http://");
                //Page.Response.Write("url = "+url+"<br>");
            }
        }
        catch{
            //If empty string or null
            Page.Response.Write("Empty URL<br>");
            return -1;
        }*/

        //Open up the url
        //If url is a bad link, then shows an error message
        try{
            //Creating Request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //Get Response: actually goes and tries to access the website
            WebResponse response = request.GetResponse();
            //Convert response into string
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            htmlText = reader.ReadToEnd();
            //Close StreamReader
            reader.Close();
            return 0;
        }
        catch (Exception){
            Page.Response.Write("URL(" + url + ") can't be opened<br>");
            return -1;
        }
    }

    /*
     * Method to ensure string url starts with http://
     * Return 0 upon success, -1 otherwise
     */
    private int ensureHttp(){
        try{
            //7 = to exclude "http://
            if (!(url.StartsWith("http://"))){
                url = url.Insert(0, "http://");
            }
        }
        catch{
            //If empty string or null
            Page.Response.Write("Empty URL<br>");
            return -1;
        }
        return 0;
    }
    /*
     * Method to get the root's name from a given url
     * Return 0 upon success, -1 otherwise
     */
    private int getRootName(){
        int end = 0;
        try{
            //Get the first appearance of "/" from a given url (after "http://")
            end = url.IndexOf('/', 7);
            if (end != -1)
                rootUrl = url.Substring(0, end);
            else
                rootUrl = url;
            //Page.Response.Write("root = "+rootUrl+"<br>");
        }
        catch{
            //Silent the exception, and return -1 to do nothing
            //Page.Response.Write("Can't retrieve root<br>");
            return -1;
        }
        return 0;
    }
}
