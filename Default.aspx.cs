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
    
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

        try
        {
            if(IsPostBack){
                Util.CreateEncryptedCookie(true);
            }

            if (this.User.Identity.IsAuthenticated)
            { 
               
                Util.redirect(Request.Cookies[cookiesNombre.cookieApp]["rol"]);
            }

       /*     if (!IsPostBack)
            {
                    try
                    {
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        if (Page.Request.Cookies["ASP.NET_SessionId"] != null)
                        {
                            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddYears(-30);
                        }
                        HttpCookie cookieUser = new HttpCookie(cookiesNombre.cookieApp);
                        cookieUser.Values.Add("id", "0");
                        cookieUser.Values.Add("rol", "0");
                        cookieUser.Values.Add("usuario", "0");
                        cookieUser.Expires = DateTime.Now.AddDays(-30);
                        Response.AppendCookie(cookieUser);
                    }
                    catch (Exception)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    Util.CreateEncryptedCookie(true);
                    FormsAuthentication.RedirectToLoginPage();
                }*/
            }
        
        catch (Exception) { }
      

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Cuenta cuenta = new Cuenta();
        if (cuenta.verificarLogin(txtUsuario.Text.Trim(), txtClave.Text.Trim()))
        {
            FormsAuthenticationTicket authTkt = new FormsAuthenticationTicket(1, cuenta.rol, DateTime.Now,
            DateTime.Now.AddDays(30), chkRecorcar.Checked, cuenta.rol);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName);

            if (chkRecorcar.Checked)
                authCookie.Expires = DateTime.Now.AddDays(30);

            authCookie.Value = FormsAuthentication.Encrypt(authTkt);
            HttpContext.Current.Response.AppendCookie(authCookie);


            HttpCookie cookieUser = new HttpCookie(cookiesNombre.cookieApp);
            cookieUser.Values.Add("id", cuenta.responsable_id.ToString());
            cookieUser.Values.Add("rol", cuenta.rol.ToString());
            cookieUser.Values.Add("usuario",cuenta.usuario);
            cookieUser.Expires = DateTime.Now.AddDays(30);
            Response.AppendCookie(cookieUser);
            Util.redirect(cuenta.rol);

        }
        else {
            lblMesanje.Text = "Usuario o Contraseña Invalida";
            txtClave.Text = "";
            txtUsuario.Text = "";
        }
    }

    public void SignOut()
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
