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
    protected void ParseBtn_Click(object sender, EventArgs e)
    {
        /* Alternative using session
         * Session["url"] = InputTB.Text;
         * Response.Redirect("output.aspx");
         */
        Server.Transfer("output.aspx",true);
    }
}
