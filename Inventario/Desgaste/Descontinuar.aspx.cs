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

public partial class Desgaste_Descontinuar : System.Web.UI.Page
{

    public int id
    {
        get
        {
            if (ViewState["id"] == null) return 0;
            return Int32.Parse(ViewState["id"].ToString());

        }
        set
        {
            ViewState["id"] = value;

        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieDescontinuar.ToString()));

            }
            catch
            {
                this.id = 0;
            }

            if (id == 0)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                Desgaste desgaste = new Desgaste();
                if (desgaste.obtenerDesgasteId(this.id))
                {
                    ltrArticulo.Text = desgaste.control;
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Desgaste desgaste = new Desgaste();
        string usuario = Request.Cookies[cookiesNombre.cookieApp]["usuario"].ToString();
        if (desgaste.descontinuar(this.id, usuario + ": " + txtMotivo.Text.Trim().ToUpper()))
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblMensaje.Text = Resources.Mensajes.Error_Descontinuar;
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
