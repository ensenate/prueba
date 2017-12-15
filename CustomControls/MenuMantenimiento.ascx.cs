using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class CustomControls_MenuMantenimiento : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkSession_Click(object sender, EventArgs e)
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

    protected void ver_articulos_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieFiltroArticulo, string.Empty);
        cookieValues.Add(cookiesNombre.cookieFiltroKeyArticulo, string.Empty);
        cookieValues.Add(cookiesNombre.cookieFiltroControlesArticulo, string.Empty);
        Util.AddCookieValues(cookieValues);
        this.Page.Response.Redirect("~/Inventario/Default.aspx");
    }

}

    