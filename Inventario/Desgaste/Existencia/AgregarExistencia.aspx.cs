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

public partial class Inventario_Desgaste_Existencia_AgregarExistencia : System.Web.UI.Page
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

    List<Empresa> empresas;
    List<Ubicacion> ubicaciones;
    List<Responsable> responsables;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieDesgaste));

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



            lstEmpresa.Items.Clear();
            empresas = Empresa.obtenerListadoEmpresa(true);
			lstEmpresa.Items.Add(new ListItem("S/E","0"));
            foreach (Empresa em in empresas)
            {
                lstEmpresa.Items.Add(new ListItem(em.nombre, em.id.ToString()));
            }

            lstUbicacion.Items.Clear();

            if (lstEmpresa.Items.Count > 0)
            {
				lstUbicacion.Items.Add(new ListItem("S/E","0"));
				int key=Int32.Parse(lstEmpresa.SelectedValue);
				if(key!=0){
                  ubicaciones = Ubicacion.obtenerListadoUbicacion(key);
			    
                  foreach (Ubicacion u in ubicaciones)
                  {
                      lstUbicacion.Items.Add(new ListItem(u.nombre, u.id.ToString()));
                  }
			    }
            }
        }

    }
    protected void Unnamed1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGuardar_Click1(object sender, ImageClickEventArgs e)
    {

        if (Page.IsValid)
        {
            Existencia existencia = new Existencia();
            Desgaste desgaste = new Desgaste();
            String unidad = "N/A";


            if (desgaste.obtenerDesgasteId(this.id))
            {
                unidad = desgaste.unidad;
            }

            int retu=existencia.agregarExistencia(float.Parse(txtCantidad.Text.Trim()), txtObs.Text.Trim(), this.id, Request.Cookies[cookiesNombre.cookieApp]["id"].ToString(), unidad, lstResponsable.SelectedValue, lstUbicacion.SelectedValue, lstTipo.SelectedValue);
      
            if (retu==1)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                if (retu == -1)
                {
                    lblMensaje.Text = Resources.Mensajes.Error_Existencia;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else {
                    if (retu == 0)
                    {
                        lblMensaje.Text = Resources.Mensajes.Existencia_Positiva;
                        lblMensaje.ForeColor = System.Drawing.Color.Red;

                    }
                }
            }
        }
    }
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
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


    protected void lstEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void lstEmpresa_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (lstEmpresa.Items.Count > 0)
        {
            if (lstEmpresa.SelectedValue.ToString() != "0")
            {
                ubicaciones = Ubicacion.obtenerListadoUbicacion(Int32.Parse(lstEmpresa.SelectedValue));
                lstUbicacion.Items.Clear();
                foreach (Ubicacion u in ubicaciones)
                {
                    lstUbicacion.Items.Add(new ListItem(u.nombre, u.id.ToString()));
                }
            }
            else
            {
                lstUbicacion.Items.Clear();
                lstUbicacion.Items.Add(new ListItem("S/E", "0"));
            }

        }
    }
}
