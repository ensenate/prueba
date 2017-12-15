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

public partial class Administracion_Mantenimiento_Diagnostico : System.Web.UI.Page
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
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieDiagnostico));

            }
            catch
            {
                this.id = 0;

            }

            if (this.id == 0)
            {
                Response.Redirect("Default.aspx");
            }
        }

    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {

        Diagnostico diagnostico = new Diagnostico();

        string[] vecTemp = txtFecha.Text.Split('/');
        String fecha = vecTemp[2] + "-" + vecTemp[1] + "-" + vecTemp[0];

        if (diagnostico.agregarDiagnostico(this.id, fecha, txtRevision.Text.Trim(), txtDiagnostico.Text.Trim()))
        {
            Mantenimiento mantenimiento = new Mantenimiento();

            if (mantenimiento.cambiarEstado(this.id,GlobalEnum.estadoMantenimiento.PENDIENTE.ToString()))
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Error_Diagnostico;
            }
        }
        else
        {
            lblMensaje.Text = Resources.Mensajes.Error_Diagnostico;
        }

    }


}
