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
using MySql.Data.MySqlClient;

public partial class Matenimiento_Dominio_AgregarDominio : System.Web.UI.Page
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

    public String tipoDominio
    {
        get
        {
            object o = ViewState["tipoDominio"];
            return (o == null) ? String.Empty : (string)o;
        }

        set
        {
            ViewState["tipoDominio"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieDominioValor));

        }
        catch
        {
            this.id = 0;
        }


        try
        {
            this.tipoDominio = Util.GetCookieValue(cookiesNombre.cookieDominioNombre).ToString();

        }
        catch
        {
            this.tipoDominio = GlobalEnum.dominios.Estado.ToString();
        }

        if (this.tipoDominio == "")
        {
            this.tipoDominio = GlobalEnum.dominios.Estado.ToString();
        }




        if (this.tipoDominio != "Tip")
        {
           
            ltrTitulo.Text = "Gestionar " + this.tipoDominio;
        }
        else
        {
        
            ltrTitulo.Text = "Gestionar Keyworks";
        }


        if (!IsPostBack && this.id != 0)
        {
            popularValores();
        }

    }

    private void popularValores()
    {
        Dominio dominio= new Dominio();
        if (dominio.obtenerDominioId(this.id, this.tipoDominio))
        {
            txtNombre.Text = dominio.nombre;

        }
        else {
            Response.Redirect("Default.aspx");
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        
        Dominio dominio = new Dominio();
        
        String nombre = txtNombre.Text.Trim();

        if (!dominio.validarDominio(this.tipoDominio))
        {
            lblMensaje.Text = Resources.Mensajes.Error_Dominio;
            tipoDominio = GlobalEnum.dominios.Estado.ToString();
            return;
        } 

        if (!dominio.validarNombreUnico(this.id,nombre, tipoDominio))
        {
            lblMensaje.Text = Resources.Mensajes.Repetido_Nombre;
            return;
        }

        if (dominio.agregarActualizarDominio(this.id,nombre,tipoDominio))
        {
            lblMensaje.Text = Resources.Mensajes.Bien_Dominio;
            Response.Redirect("Default.aspx");
        }
        else {
            lblMensaje.Text = Resources.Mensajes.Error_Dominio;
        }

        
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
