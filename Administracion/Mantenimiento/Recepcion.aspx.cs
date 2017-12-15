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

public partial class Administracion_Mantenimiento_Recepcion : System.Web.UI.Page
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
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieRecepcion));

            }
            catch
            {
                this.id = 0;
            }

            if (this.id == 0)
            {
                Response.Redirect("Default.aspx");
            }


            Mantenimiento mantenimiento = new Mantenimiento();
            if (mantenimiento.obtenerMantenimiento(this.id))
            {
                ltrControl.Text = mantenimiento.articulo.control;
                ltrCategoria.Text = mantenimiento.articulo.categoria;
                ltrFecha.Text = mantenimiento.fechaSolicitud;
                ltrDescripcion.Text = mantenimiento.descripcion;
                ltrUsuario.Text = mantenimiento.usuario.nombre;
                
            }
            else {
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
        Mantenimiento mantenimiento = new Mantenimiento();

        string[] vecTemp = txtFecha.Text.Split('/');
        String fecha = vecTemp[2] + "-" + vecTemp[1] + "-" + vecTemp[0];

        if (mantenimiento.indicarRecepcion(fecha, this.id))
        {
            if (chkTraslado.Checked == false)
            {
                Response.Redirect("Default.aspx");
            }
            else {
                Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                cookieValues.Add(cookiesNombre.cookieArticulo, this.id.ToString());
                Util.AddCookieValues(cookieValues);
                Response.Redirect("../../Inventario/Historia/AgregarHistorial.aspx");
            }
        }
        else {
            lblMensaje.Text = Resources.Mensajes.Error_Recepcion;
        }
        
    }
}
