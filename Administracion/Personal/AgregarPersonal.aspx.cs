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
using System.Web.Services.Description;
using System.Collections.Generic;

public partial class Matenimiento_Personal_AgregarPersonal : System.Web.UI.Page
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

    public String viejoRif
    {
        get
        {
            if (ViewState["viejoRif"] == null) return "";
            return ViewState["viejoRif"].ToString();

        }
        set
        {
            ViewState["viejoRif"] = value;

        }
    }

    List<Empresa> empresas;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookiePersonal));

            }
            catch
            {
                this.id = 0;
            }

            empresas = Empresa.obtenerListadoEmpresa(false);
            lstEmpresa.Items.Clear();

            foreach (Empresa em in empresas)
            {
                lstEmpresa.Items.Add(new ListItem(em.nombre, em.id.ToString()));
            }
            if (id != 0)
            {
                popularValores();
            }
        }
    }

    private void popularValores()
    {
        Responsable personal = new Responsable();
        if (personal.obtenerPersonalId(this.id))
        {
            this.viejoRif = personal.rif;
            txtRif.Text = personal.rif;
            txtNombre.Text = personal.nombre;
            txtTelefono.Text = personal.telefono;
            txtCorreo.Text = personal.correo;
            txtTipo.Checked = personal.tipo;

            if (personal.tipo == true)
            {
                int pos = 0;
                int n = lstEmpresa.Items.Count;
                for (int i = 0; i < n; i++)
                {

                    if (lstEmpresa.Items[i].Value == personal.empresa_id.ToString())
                    {
                        pos = i;
                        break;
                    }
                }
                lstEmpresa.SelectedIndex = pos;
            }
            else {
                lstEmpresa.Enabled = false;
            }
        }
        else {
            Response.Redirect("Default.aspx");
        }

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        String rif = txtRif.Text.Trim();
        Responsable personal = new Responsable();

        if (txtRif.Text.Trim() != "")
        {
            if (viejoRif != rif)
            {
                Boolean bo = personal.obtenerPersonalRif(rif);
                if (bo)
                {
                    lblMensaje.Text = Resources.Mensajes.Rif_Repetido;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
        }

        string empresa_id = txtTipo.Checked==true?lstEmpresa.SelectedValue:"0";
            if (personal.agregarActualizarPersonal(this.id, rif, txtNombre.Text, txtTelefono.Text, txtCorreo.Text,txtTipo.Checked,empresa_id))
            {
                lblMensaje.Text = Resources.Mensajes.Bien_Dominio;
                lblMensaje.ForeColor=System.Drawing.Color.Green;
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Error_Personal;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
    }
    protected void txtTipo_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void txtTipo_CheckedChanged1(object sender, EventArgs e)
    {

    }
}

