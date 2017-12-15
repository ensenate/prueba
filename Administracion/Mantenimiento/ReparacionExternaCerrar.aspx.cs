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

public partial class Administracion_Mantenimiento_ReparacionExternaCerrar : System.Web.UI.Page
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
                    this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieSoporteExternoCerrar));

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
        string[] vecTemp = txtFecha.Text.Split('/');
        String fecha = vecTemp[2] + "-" + vecTemp[1] + "-" + vecTemp[0];
        float costo=0;

        try
        {
             costo = float.Parse(txtCosto.Text);
        }
        catch
        {
            lblMensaje.Text = "Monto incorrecto";
            return;
        }

        ReparacionExterna reparacionExterna = new ReparacionExterna();
        int referencia = reparacionExterna.obtenerReferenciaSoporteActual(this.id);

        if (reparacionExterna.indicarCulminacion(fecha, txtDiagnostico.Text.Trim(), costo, referencia))
        {
            Mantenimiento mantenimiento = new Mantenimiento();
            if (mantenimiento.cambiarEstado(this.id, GlobalEnum.estadoMantenimiento.PRUEBA.ToString()))
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Error_Soporte_Externo;
            }
        }
        else
        {
            lblMensaje.Text = Resources.Mensajes.Error_Soporte_Externo;
        }

    }

}
