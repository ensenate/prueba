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

public partial class Administracion_Mantenimiento_ReparacionExterna : System.Web.UI.Page
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
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieSoporteExterno));

            }
            catch
            {
                this.id = 0;

            }

            if (this.id == 0)
            {
                Response.Redirect("Default.aspx");
            }


            List<Responsable> responsables = Responsable.obtenerListaPersonal(0);

            foreach (Responsable r in responsables)
            {
                lstResponsable.Items.Add(new ListItem(r.nombre, r.id.ToString()));
            }

        }

    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        ReparacionExterna reparacionExterno = new ReparacionExterna();

        string[] vecTemp = txtFechaEnvio.Text.Split('/');
        String fecha = vecTemp[2] + "-" + vecTemp[1] + "-" + vecTemp[0];

        if (reparacionExterno.agregarReparacionExterna(this.id, Int32.Parse(lstResponsable.SelectedValue.ToString()), fecha, txtDescripcion.Text.Trim()))
        {
            Mantenimiento mantenimiento = new Mantenimiento();
            if (mantenimiento.cambiarEstado(this.id, GlobalEnum.estadoMantenimiento.EXTERNO.ToString()))
            {
                if (chkTraslado.Checked == false)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    Response.Redirect("../../Inventario/Historia/AgregarHistorial.aspx");
                }
            }
            else {
                lblMensaje.Text = Resources.Mensajes.Error_Soporte_Externo;
            }
        }
        else
        {
            lblMensaje.Text = Resources.Mensajes.Error_Soporte_Externo;
        }

    }

}

