using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        cerrarSession();
    }

    public void cerrarSession()
    {

        try
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            if (Page.Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddYears(-30);
            }
            FormsAuthentication.RedirectToLoginPage();
        }
        catch (Exception) { }
    }
}
