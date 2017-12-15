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

public partial class Inventario_EliminarArticulo : System.Web.UI.Page
{

    public string serialInterno
    {
        get
        {
            if (ViewState["serialInterno"] == null) return "";
            return ViewState["serialInterno"].ToString();

        }
        set
        {
            ViewState["serialInterno"] = value;

        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                this.serialInterno = Util.GetCookieValue(cookiesNombre.cookieEliminarArticulo.ToString());

            }
            catch
            {
                this.serialInterno = "";
            }

            if (serialInterno == string.Empty)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                ltrArticulo.Text = this.serialInterno;
            }
        }

    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Articulo articulo = new Articulo();
        string usuario = Request.Cookies[cookiesNombre.cookieApp]["usuario"].ToString();
        if (articulo.eliminarItem(this.serialInterno, usuario+": "+txtMotivo.Text.Trim().ToUpper()))
        {
            lblMensaje.Text = Resources.Mensajes.Bien_Eliminar_Articulo;
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblMensaje.Text = Resources.Mensajes.Error_Eliminar_Articulo;
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
