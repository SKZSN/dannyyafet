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


public partial class output : System.Web.UI.Page
{
    // To store an url's name from URLTextBox on the source page (default.aspx)
    private string url;
    //To store an html source code in text type from a given url
    private string htmlText;

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
                Label1.Text = url;
            }
            else{
                //If can't get the URLTextBox from source, shows an error message and stop/return
                Page.Response.Write("urlTextBox == null");
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

        //parseTag("<img");
    }

    /*
     * Event's Trigger: on click of the parseButton (of output.aspx, not default.aspx)
     * Action: shows the output from the url given (images and texts) to output.aspx
     */
    protected void parseButton_Click(object sender, EventArgs e){
        //Assign the value of URLTextBox (url's name) to string url
        url = URLTextBox.Text;
        Label1.Text = url;

        //Check if in the process of opening up the url and storing html source, there is an error
        if (getHtmlSource() == -1 || saveHtmlSource() == -1){
            return;
        }

        parseTag("<img");
    }

    
    private void parseTag(string tag){
        int loc = 0;
        /*
        //Development process
        int locImg = 0;
        int locText = 0;
        */

        loc = htmlText.IndexOf(tag, StringComparison.OrdinalIgnoreCase);
        while(loc != -1){
            Page.Response.Write("location = "+loc+"\r\n");

            loc = htmlText.IndexOf(tag, loc + 1, StringComparison.OrdinalIgnoreCase);
        }
        Page.Response.Write("location = " + loc+"\r\n");
        
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

            /*//Make filename from url, convention: (url root's name without http://).txt
            int size = url.Length - 7; // minus 7 = http://
            char[] nameBuffer = new char[size];
            url.CopyTo(7, nameBuffer, 0, size);
            string filename = new string(nameBuffer);
            filename = filename + ".txt";*/

            //7 = to exclude "http://"
            //Make filename from url, convention: (url root's name without http://).txt
            if(url.StartsWith("http://")){
                filename = url.Substring(7, url.Length - 7);
                filename += ".txt";
            }else{
                filename = url+".txt";
            }

            //write to the *.txt
            try{
                StreamWriter writer = new StreamWriter(Server.MapPath(filename));
                writer.Write(htmlText);
                writer.Close();
            }
            catch(Exception){
                Page.Response.Write("Writing to file error");
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
        //URL validation - not implemented

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
            Page.Response.Write("URL(" + url + ") can't be opened");
            return -1;
        }
    }
}
