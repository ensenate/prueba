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

public partial class Administracion_Dominio_AgregarModelo : System.Web.UI.Page
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


    List<Dominio> domModelos;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieModelo));

            }
            catch
            {
                this.id = 0;
            }


            lstMarca.Items.Clear();
            domModelos = Dominio.obtenerDominios(GlobalEnum.dominios.Marca.ToString());
            
            foreach (Dominio d in domModelos)
            {
                lstMarca.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

            if (this.id != 0)
            {
                popularValores();
            }
        }

    }

    private void popularValores()
    {
        Modelo modelo = new Modelo();

        if (modelo.obtenerModeloId(this.id))
        {
            txtNombre.Text = modelo.nombre;


            int pos;
            int n;

            pos = 0;
            n = lstMarca.Items.Count;
            for (int i = 0; i < n; i++)
            {

                if (lstMarca.Items[i].Value == modelo.marca_id.ToString())
                {
                    pos = i;
                    break;
                }
            }

            lstMarca.SelectedIndex = pos;
        }
        else
        {
            Response.Redirect("ListadoModelo.aspx");
        }

    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {

        Modelo modelo = new Modelo();

        String nombre = txtNombre.Text.Trim();


        if (!modelo.modeloUnicoPorMarca(nombre, lstMarca.Items[lstMarca.SelectedIndex].Value))
        {
        
            lblMensaje.Text = Resources.Mensajes.Modelo_Existente;
            lblMensaje.ForeColor=System.Drawing.Color.Red;
            return;
        }

        if (modelo.agregarActualizarModelo(this.id, nombre,lstMarca.Items[lstMarca.SelectedIndex].Value))
        {
            lblMensaje.Text = Resources.Mensajes.Bien_Modelo;
            Response.Redirect("ListadoModelo.aspx");
        }
        else
        {
            lblMensaje.Text = Resources.Mensajes.Error_Modelo;
        }


    }
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListadoModelo.aspx");
    }
}
