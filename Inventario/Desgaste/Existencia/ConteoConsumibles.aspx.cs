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

public partial class Inventario_Desgaste_Existencia_ConteoConsumibles : System.Web.UI.Page
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

    List<Responsable> responsables;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieConteoArticulos));

            }
            catch
            {
                this.id = 0;
            }

            responsables = Responsable.obtenerListaPersonal(1);

            foreach (Responsable r in responsables)
            {
                lstResponsable.Items.Add(new ListItem(r.nombre, r.id.ToString()));
            }

        }

    }

    protected void btnGuardar_Click1(object sender, ImageClickEventArgs e)
    {

        if (Page.IsValid)
        {
         
           
            string []vecTemp=txtFecha.Text.Split('/');
            String auxFecha = vecTemp[1] + "/" + vecTemp[0] + "/" + vecTemp[2];
            String[] vec = auxFecha.Split('/');
            auxFecha = vec[2] + "/" + vec[1] + "/" + vec[0];

            Desgaste desgaste = new Desgaste();
            float cantidadAnterior = desgaste.obtenerCantidad(this.id);

             Conteo conteo = new Conteo();
             if (conteo.agregarConteo(auxFecha, Int32.Parse(Request.Cookies[cookiesNombre.cookieApp]["id"].ToString()), Int32.Parse(lstResponsable.SelectedValue),  float.Parse(txtCantidad.Text.Trim()), cantidadAnterior, this.id, txtObs.Text.Trim()))
            {
                     Response.Redirect("../Default.aspx");
            }
            else {
                lblMensaje.Text = Resources.Mensajes.Error_Conteo;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }

    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {

        try
        {
            float valor = float.Parse(txtCantidad.Text.Trim());
            if (valor<0)
            {
                args.IsValid = false;
            }
        }
        catch
        {
            args.IsValid = false;
        }
    }


}
