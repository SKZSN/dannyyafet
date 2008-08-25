using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{   
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /*
     * Event's Trigger: when the user click on the button 
     * Action: redirect to output.aspx, which contains the images and texts of the url
     * Validation: URL's name - not implemented yet
     */ 
    protected void parseButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("output.aspx");
    }

    /*
     * Method to pass the value (an url's name) from URLTextBox to output.aspx
     * in which output.aspx will open this url and start parsing for images and texts 
     */
    /*public string getUrl
    {
        get
        {
            return URLTextBox.Text;
        }
    }*/
}
