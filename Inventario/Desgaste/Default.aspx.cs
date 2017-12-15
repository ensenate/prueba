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

public partial class Desgaste_Default : System.Web.UI.Page
{
    Desgaste desgaste = new Desgaste();

        public String filtro
    {
        get
        {
            if (ViewState["filtro"] == null) return "";
            return ViewState["filtro"].ToString();

        }
        set
        {
            ViewState["filtro"] = value;

        }
    }

        List<Dominio> domCategoriaF, domMarcaF, domModeloF;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            String rol = Request.Cookies[cookiesNombre.cookieApp]["rol"].ToString();

            if (rol != "admin" && rol != "superusuario")
            {
                btnAgregar.Visible = false;
            }

            domCategoriaF = Dominio.obtenerDominios(GlobalEnum.dominios.Categoria.ToString());
            lstCategoriaF.Items.Add(new ListItem("N/A", "0"));
            foreach (Dominio d in domCategoriaF)
            {
                lstCategoriaF.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

            domMarcaF = Dominio.obtenerDominios(GlobalEnum.dominios.Marca.ToString());
            lstMarcaF.Items.Add(new ListItem("N/A", "0"));
            foreach (Dominio d in domMarcaF)
            {
                lstMarcaF.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

            domModeloF = Dominio.obtenerDominios(GlobalEnum.dominios.Modelo.ToString());
            lstModeloF.Items.Add(new ListItem("N/A", "0"));
            foreach (Dominio d in domModeloF)
            {
                lstModeloF.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }


            List<Empresa> empresas = Empresa.obtenerListadoEmpresa(false);
 
            lstEmpresa.Items.Add(new ListItem("N/A", "0"));

            foreach (Empresa d in empresas)
            {
                lstEmpresa.Items.Add(new ListItem(d.nombre, d.id.ToString()));

            }

            this.filtro = " where descontinuado='N' ";
            cargarInventario();
        }

    }

    private void cargarInventario()
    {

        List<Desgaste> desgastes = desgaste.obtenerListaDesgastes(this.filtro);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("control");
        dataTable.Columns.Add("descripcion");
        dataTable.Columns.Add("cantidad");
        dataTable.Columns.Add("categoria");
        dataTable.Columns.Add("marca");
        dataTable.Columns.Add("modelo");
        dataTable.Columns.Add("unidad");
        dataTable.Columns.Add("empresa");
        dataTable.Columns.Add("ocultarEliminar");

        foreach (Desgaste a in desgastes)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = a.id;
            dataRow["control"] = a.control;
            dataRow["descripcion"] = a.descrip;
            dataRow["cantidad"] = a.cantidad;
            dataRow["categoria"] = a.categoria;
            dataRow["marca"] = a.marca;
            dataRow["modelo"] = a.modelo;
            dataRow["unidad"] = a.unidad;
            dataRow["modelo"] = a.modelo;
            dataRow["empresa"] = a.empresa;

              if (a.descontinuado == "S" )
            {
                dataRow["ocultarEliminar"] = "ocultarEliminar";
            }

              else
              {
                  dataRow["ocultarEliminar"] = "";
              }
              

            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdArticulo.DataSource = dataTable;
        grdArticulo.DataBind();
    }

    protected void grdArticulo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editArticulo")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieDesgaste, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarArticulo.aspx");
        }

        if (e.CommandName == "editExistencia")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieDesgaste, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("Existencia/Default.aspx");
        }
        
	    if (e.CommandName == "contarConsumibles")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieConteoArticulos, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("Existencia/ConteoConsumibles.aspx");
        }

        if (e.CommandName == "descontinuar")
        {
            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieDescontinuar, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("Descontinuar.aspx");
        }
		
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {


        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDesgaste, "");
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarArticulo.aspx");
    }
    protected void grdArticulo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArticulo.PageIndex = e.NewPageIndex;
        cargarInventario();
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        this.filtro = "";

        String valor;

        valor = txtControlF.Text.Trim();
        if (valor != "")
        {
            if (this.filtro == "")
            {
                this.filtro = " where desgaste.serialInterno like'%" + valor + "%' ";
            }
            else
            {
                this.filtro = this.filtro + " and desgaste.serialInterno like'%" + valor + "%' ";
            }
        }


        valor = lstEmpresa.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where desgaste.empresa_id='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and desgaste.empresa_id='" + valor + "' ";
            }
        }

        valor = lstCategoriaF.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where desgaste.categoria_id='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and desgaste.categoria_id='" + valor + "' ";
            }
        }

        valor = lstMarcaF.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where desgaste.marca_id='" + valor + " ' ";
            }
            else
            {
                this.filtro = this.filtro + " and desgaste.marca_id='" + valor + "' ";
            }
        }


        valor = lstModeloF.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where desgaste.modelo_id='" + valor + "'";
            }
            else
            {
                this.filtro = this.filtro + " and desgaste.modelo_id='" + valor + "'";
            }
        }

  
        if (lstDescontinuado.SelectedIndex != 2)
        {
            if (lstDescontinuado.SelectedIndex == 1)
            {
                if (this.filtro == "")
                {
                    this.filtro = " where desgaste.descontinuado='N' ";
                }
                else
                {
                    this.filtro = this.filtro + " and desgaste.descontinuado='N' ";
                }
            }
            else
            {
                if (this.filtro == "")
                {
                    this.filtro = " where desgaste.descontinuado='S' ";
                }
                else
                {
                    this.filtro = this.filtro + " and desgaste.descontinuado='S' ";
                }
            }
        }





        cargarInventario();
        
    }
}
