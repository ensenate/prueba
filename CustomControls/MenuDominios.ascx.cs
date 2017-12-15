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

public partial class CustomControls_MenuDominios : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkKey_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Tip.ToString());
        Util.AddCookieValues(cookieValues);
        Response.Redirect("../Dominio/Default.aspx");
       
    }
    protected void lnkMarca_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Marca.ToString());
        Util.AddCookieValues(cookieValues);
        Response.Redirect("../Dominio/Default.aspx");
    }
    protected void lnkModelo_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListadoModelo.aspx");
    }
    protected void lnkEstado_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Estado.ToString());
        Util.AddCookieValues(cookieValues);
        Response.Redirect("../Dominio/Default.aspx");
    }
    protected void lnkRol_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Rol.ToString());
        Util.AddCookieValues(cookieValues);
        Response.Redirect("../Dominio/Default.aspx");
    }
    protected void lnkUnidad_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Unidad.ToString());
        Util.AddCookieValues(cookieValues);
        Response.Redirect("../Dominio/Default.aspx");
    }
    protected void lnkSalir_Click(object sender, EventArgs e)
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
    protected void lnkCategoria_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListadoCategoria.aspx");
    }
}
