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

public partial class Administracion_Empresa_AgregarEmpresa : System.Web.UI.Page
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

    Empresa empresa = new Empresa();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieEmpresa));

        }
        catch
        {
            this.id = 0;
        }


        if (!IsPostBack && this.id != 0)
        {
            popularValores();
        }
    }

    private void popularValores()
    {
        Empresa empresa = new Empresa();

        if (empresa.obtenerEmpresaId(this.id))
        {
            txtNombre.Text = empresa.nombre;
            txtRif.Text = empresa.rif;
            txtTelefono.Text = empresa.telefono;
            txtDescripcion.Text = empresa.descripcion;
            txtDireccion.Text = empresa.direccion;

        }
        else
        {
            Response.Redirect("Default.aspx");
        }

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
            if (empresa.agregarActualzarEmpresa(this.id, txtNombre.Text.Trim(), txtRif.Text.Trim(),txtTelefono.Text.Trim(),txtDireccion.Text.Trim(),txtDescripcion.Text.Trim()))
            {
                lblMensaje.Text = Resources.Mensajes.Bien_Empresa;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Error_Empresa;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }


    protected void CustomValidator1_ServerValidate1(object source, ServerValidateEventArgs args)
    {
        int bo =  empresa.unicoNombre(txtNombre.Text.Trim(), this.id);
        switch (bo)
        {

            case 1:
                args.IsValid = true;
                break;

            case 0:
                args.IsValid = false;
                break;

            case -1:
                args.IsValid = false;
                lblMensaje.Text = Resources.Mensajes.Error_Empresa;
                break;

        }
    }
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int bo = empresa.unicoRif(txtRif.Text.Trim(), this.id);
        switch (bo)
        {

            case 1:
                args.IsValid = true;
                break;

            case 0:
                args.IsValid = false;
                break;

            case -1:
                args.IsValid = false;
                lblMensaje.Text = Resources.Mensajes.Error_Empresa;
                break;

        }
    }
}

