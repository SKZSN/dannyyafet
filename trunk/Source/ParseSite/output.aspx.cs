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
using System.Collections;

public partial class output : System.Web.UI.Page
{
    // A class to handle tags
    class htmltag
    {
        public string name;    // Tags name
        public string tag;     // Tags syntax
        public int offset;     // Tags offset

        // Constructor
        public htmltag(string name, string tag)
        {   
            this.name = name;
            this.tag = tag;
        }
    } // -- end of htmltag class

    // Global Variables
    string htmlsource = ""; // Varible to store htmlsource
    string url = ""; // Variable to store url
    string result = ""; // Variable to store result
    htmltag imgtag = new htmltag("image", "<img src="); // Creating an imgtag
    
    /*
     * Event's Trigger: automatically, after redirection from source page
     * Action: shows the output from the url given (images and texts) 
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        /* Alternative using session
         * url = (string)Session["url"];
         * Write(Session["url"]);
         * Get url from previouspage with preserveform method and display it on OutputTB
         */
        url = Request.Form["InputTB"];
        Response.Write(url+"<br>");

        // Check if in the process of opening up the url and storing html source, there is an error
        if (getHtmlSource() == -1)// || saveHtmlSource() == -1)
        {
            return;
        }
        // Parse the htmlsource
        parseSource(imgtag);   
    }

    /*
     * Method: to parse the source atm only "<img" , "/>"
     * Parameter: (2) tag and offset. Offset is the length of this tag
     * Return: void
     */
    private void parseSource(htmltag tag)
    {
        int location = 0; // Start index for parsing tags
        int start_tag = 0; // Beginning of a parsed string
        int end_tag = 0; // End of a a parsed string
        int counter = 0;  // Count tag

        // Parsing Loop
        do
        {   
            // Search for "<img"
            if (htmlsource.IndexOf(tag.tag, location) != -1)
            {
                // Parsing tag <img></img> start_tag -> begin of tag , end_tag -> end of tag
                start_tag = htmlsource.IndexOf(imgtag.tag, location, StringComparison.OrdinalIgnoreCase);
                end_tag = htmlsource.IndexOf(">", start_tag, StringComparison.OrdinalIgnoreCase) + 1;

                // Saving the parsed tags, change location value to end_tag, increment counter
                // start_tag + offset (in case img tag) means we start from <img src=x <- here
                result = htmlsource.Substring(start_tag, end_tag - start_tag); 

                // Counter increment and Current Location allocation
                location = end_tag;
                counter++;

                // Printout the result
                Response.Write("<br><br>" + result + " Location : ||" + start_tag + "||<br>");
            }
            else
            {
                location++; // Location increment
            }
        } while (location <= htmlsource.Length) ; // Loop till end of source 

        // Display found tags
        Response.Write(" found " + counter + " " + tag.name);
    }

    /*
     * Method: to request and open up a given url, store html source into string htmlText
     * Return: 0 if succesfull, -1 if exception occurs
     */
    private int getHtmlSource()
    {
        /* URL validation - not implemented
         * If http:// not found then add it
         */
        try
        {
            // Validation check
            if (!url.Contains("http://"))
            {
                string temp1;

                temp1 = url;
                url = "http://";
                url += temp1;
            }
        }
        catch (Exception)
        {
            // Display error message
            Response.Write("Empty url");
            return -1;
        }

        // Open up the url
        // If url is a bad link, then shows an error message
        try
        {
            // Creating Request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Get Response: actually goes and tries to access the website
            WebResponse response = request.GetResponse();

            // Convert response into string
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            htmlsource = reader.ReadToEnd();

            // Close StreamReader
            reader.Close();
            return 0;
        }
        catch (Exception)
        {
            // Display error message
            Response.Write("URL(" + url + ") can't be opened");
            return -1;
        }
    }
}

//ArrayList imgarraylist = new ArrayList();
//imgarraylist.Add(htmlsource.Substring(start_tag+10, end_tag - start_tag));
/* Printout the result
for (int i = 0; i < imgarraylist.Count; i++)
{
    string temp = "<img src=" + url + "/" + (string)imgarraylist[i];
    imgarraylist[i] = temp;
    Response.Write("<br><br>" + imgarraylist[i]);
}*/
/*
                // If the image files on the same server
                if (result.Contains("http://"))
                {
                    // add img tag back, url 
                    string temp = tag.tag + url + "/" + result;
                    result = temp;

                }
                else // if the image files not on the same server
                {
                    // just add the image tag back
                    string temp = tag.tag + result;
                    result = temp;
                }*/