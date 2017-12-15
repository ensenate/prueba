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

public partial class Matenimiento_Dominio_Default : System.Web.UI.Page
{

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
            this.tipoDominio = Util.GetCookieValue(cookiesNombre.cookieDominioNombre).ToString();

        }
        catch
        {
            this.tipoDominio = this.tipoDominio = GlobalEnum.dominios.Estado.ToString();
        }

        lblMensaje.Text = "";

        if (this.tipoDominio == "")
        {
            this.tipoDominio = GlobalEnum.dominios.Estado.ToString();
        }
        if (this.tipoDominio != "Tip")
        {
            ltrH2.Text = "Gestionar " + this.tipoDominio;

        }
        else {
             ltrH2.Text = "Gestionar Keyworks";

        }

        if (!IsPostBack)
        {
            cargarDominios();
        }
    }

    private void cargarDominios()
    {
        List<Dominio> dominios = Dominio.obtenerDominios(this.tipoDominio);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("nombre");

        foreach (Dominio d in dominios)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = d.id;
            dataRow["nombre"] = d.nombre;
            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdDominio.DataSource = dataTable;
        grdDominio.DataBind();
    }



    protected void grdDominio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editDominio")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieDominioNombre, tipoDominio.ToString());
            cookieValues.Add(cookiesNombre.cookieDominioValor, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarDominio.aspx");
        }

        if (e.CommandName == "eliminarDominio")
        {
            Dominio dominio = new Dominio();

           if (!dominio.verificarUsoDominio(e.CommandArgument.ToString(), this.tipoDominio))
            {
                lblMensaje.Text = Resources.Mensajes.Dominio_Uso;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!dominio.eliminarDominio(e.CommandArgument.ToString(), this.tipoDominio))
            {
                lblMensaje.Text = Resources.Mensajes.Error_Dominio_Eliminar;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Bien_Eliminar_Dominio;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                cargarDominios();
            }
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, this.tipoDominio);
        cookieValues.Add(cookiesNombre.cookieDominioValor, "");
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarDominio.aspx"); 
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDominio.PageIndex = e.NewPageIndex;
        cargarDominios();
    }

}
   