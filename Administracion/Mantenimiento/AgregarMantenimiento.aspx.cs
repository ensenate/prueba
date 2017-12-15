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
using Resources;

public partial class Administracion_Mantenimiento_Default : System.Web.UI.Page
{

    List<Responsable> usuario, responsable;

    protected void Page_Load(object sender, EventArgs e)
    {
    
        if(!IsPostBack){

            usuario = Responsable.obtenerListaPersonal(1);

            foreach (Responsable r in usuario)
            {
                if (r.soporte == GlobalEnum.condicionesSN.S.ToString())
                {
                    lstResponsable.Items.Add(new ListItem(r.nombre, r.id.ToString()));
                }

                lstUsuario.Items.Add(new ListItem(r.nombre, r.id.ToString()));
            }

        }

    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {

            Mantenimiento mantenimiento = new Mantenimiento();
            Articulo articulo = new Articulo();

            if (articulo.obtenerArticuloControl(txtControl.Text.Trim()))
            {
                string[] vecTemp = txtFecha.Text.Split('/');
                String fechaSolicitud = vecTemp[2] + "-" + vecTemp[1] + "-" + vecTemp[0];

                String estado = GlobalEnum.estadoMantenimiento.ESPERA.ToString();
                String fechaRecepcion = String.Empty;

                if (txtRecepcion.Text != String.Empty)
                {
                    vecTemp = txtRecepcion.Text.Split('/');
                    fechaRecepcion = vecTemp[2] + "-" + vecTemp[1] + "-" + vecTemp[0];
                    estado = GlobalEnum.estadoMantenimiento.PENDIENTE.ToString();
                }

                if (mantenimiento.agregarMantenimiento(articulo.id, fechaSolicitud, Int32.Parse(lstResponsable.SelectedValue), Int32.Parse(lstUsuario.SelectedValue), fechaRecepcion, txtDescripcion.Text.Trim(), estado,String.Empty))
                {
                    if (chkTraslado.Checked == true)
                    {
                        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                        cookieValues.Add(cookiesNombre.cookieArticulo, articulo.id.ToString());
                        Util.AddCookieValues(cookieValues);
                        Response.Redirect("../../Inventario/Historia/AgregarHistorial.aspx");
                    }
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMensaje.Text = Mensajes.Error_Agregar_Mantenimiento;
                }
            }
            else
            {
                lblMensaje.Text = Mensajes.Error_Agregar_Mantenimiento;
            }
        }
    }


    protected void ctsControl_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Articulo articulo = new Articulo();

        bool bo = articulo.obtenerArticuloControl(txtControl.Text.Trim());

        if (bo == false)
        {
            ctsControl.ErrorMessage = Resources.Mensajes.Control_No_Encontrado;
            args.IsValid = false;
        }
        else
        {
            Mantenimiento mantenimiento =new Mantenimiento();
            bo = mantenimiento.validarMantenimientoAsociadoSinCerrar(articulo.id);
            if (bo == false)
            {
                ctsControl.ErrorMessage = Resources.Mensajes.Articulo_Mantenimiento;
                args.IsValid = false;
            }
        }
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void btnAbrir_Click(object sender, ImageClickEventArgs e)
    {

    }
}
