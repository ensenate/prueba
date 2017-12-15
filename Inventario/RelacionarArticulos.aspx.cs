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

public partial class Inventario_relacionarArticulos : System.Web.UI.Page
{
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

    List<Dominio> domCategoriaF, domMarcaF, domModeloF;
    Articulo articulo = new Articulo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieRelacionArticulo));

            }
            catch
            {
                this.id = 0;
            }

            //---------------------------------------------------------

            articulo.obtenerArticuloId(this.id);
            ltrArticulo.Text = articulo.control;

            domCategoriaF = Dominio.obtenerDominios(GlobalEnum.dominios.Categoria.ToString());
            lstCategoriaF.Items.Add(new ListItem("N/A", "0"));
            foreach (Dominio d in domCategoriaF)
            {
                lstCategoriaF.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

            lstMarcaF.Items.Add(new ListItem("N/A", "0"));
            domMarcaF = Dominio.obtenerDominios(GlobalEnum.dominios.Marca.ToString());
            foreach (Dominio d in domMarcaF)
            {
                lstMarcaF.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

            lstModeloF.Items.Add(new ListItem("N/A", "0"));
            domModeloF = Dominio.obtenerDominios(GlobalEnum.dominios.Modelo.ToString());
            foreach (Dominio d in domModeloF)
            {
                lstModeloF.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }



            lstEstadoF.Items.Add(new ListItem("N/A", "2"));
            lstEstadoF.Items.Add(new ListItem("Si", "1"));
            lstEstadoF.Items.Add(new ListItem("No", "0"));

            //----------------------------------------------------------

                cargarInventario();
                cargarArticulos();

      
        }
    }




    private void cargarInventario()
    {
        Articulo articulo = new Articulo();

        String sql = "where tabla.articulo_id=" + this.id.ToString();


        List<Articulo> articulos = articulo.obtenerListaArticulosReferencias(sql,"",0);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("serial");
        dataTable.Columns.Add("control");
        dataTable.Columns.Add("extra1");
        dataTable.Columns.Add("extra2");
        dataTable.Columns.Add("activo");
        dataTable.Columns.Add("marca");
        dataTable.Columns.Add("modelo");
        dataTable.Columns.Add("categoria");
        dataTable.Columns.Add("pertenece");
        dataTable.Columns.Add("descripcion");



        foreach (Articulo a in articulos)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = a.id;
            dataRow["serial"] = a.serial;
            dataRow["control"] = a.control;
            dataRow["extra1"] = a.extra1;
            dataRow["extra2"] = a.extra2;
            dataRow["activo"] = a.activo == true ? "Si" : "No";
            dataRow["marca"] = a.marca;
            dataRow["modelo"] = a.modelo;
            dataRow["categoria"] = a.categoria;
            dataRow["pertenece"] = a.pertenece;
            dataRow["descripcion"] = a.descrip;
            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdArticulo.DataSource = dataTable;
        grdArticulo.DataBind();
    }

    private void cargarArticulos()
    {
        Articulo articulo = new Articulo();

        if (this.filtro == "")
        {
            this.filtro = " where tabla.id <> "+this.id+ " and tabla.articulo_id=0 and  ifnull(tabla2.cantidad,0)=0 ";
        }
        else
        {
            this.filtro = this.filtro + " and tabla.id <> " + this.id + " and tabla.articulo_id=0 and  ifnull(tabla2.cantidad,0)=0 ";
        }



        List<Articulo> articulos = articulo.obtenerListaArticulosReferencias(this.filtro,"",0);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));

        dataTable.Columns.Add("serial");
        dataTable.Columns.Add("control");
        dataTable.Columns.Add("extra1");
        dataTable.Columns.Add("extra2");
        dataTable.Columns.Add("activo");
        dataTable.Columns.Add("marca");
        dataTable.Columns.Add("modelo");
        dataTable.Columns.Add("categoria");
        dataTable.Columns.Add("pertenece");



        foreach (Articulo a in articulos)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = a.id;
            dataRow["serial"] = a.serial;
            dataRow["control"] = a.control;
            dataRow["extra1"] = a.extra1;
            dataRow["extra2"] = a.extra2;
            dataRow["activo"] = a.activo == true ? "Si" : "No";
            dataRow["marca"] = a.marca;
            dataRow["modelo"] = a.modelo;
            dataRow["categoria"] = a.categoria;
            dataRow["pertenece"] = a.pertenece;
            dataTable.Rows.Add(dataRow);

        }

        dataTable.AcceptChanges();
        grdArticulo2.DataSource = dataTable;
        grdArticulo2.DataBind();
    }

    protected void grdArticulo2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editArticulo")
        {

            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieArticulo, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            Response.Write("<script language='JavaScript'>window.open('AgregarArticulo.aspx')</script>");
        }
        else
        {
            if (e.CommandName == "agregarArticulo")
            {
                if (e.CommandArgument.ToString().Trim() == ltrArticulo.Text.Trim())
                {
                    lblMensajeControl.ForeColor = System.Drawing.Color.Red;
                    lblMensajeControl.Text = Resources.Mensajes.Error_Control;
                    return;
                }

                if (articulo.agregarItem(e.CommandArgument.ToString().Trim(), this.id, Request.Cookies[cookiesNombre.cookieApp]["id"].ToString()))
                {

                    if (trasladarA.Value == "N")
                    {
                        lblMensajeControl.ForeColor = System.Drawing.Color.Green;
                        lblMensajeControl.Text = Resources.Mensajes.Bien_Control;
                        txtItem.Text = "";
                        cargarInventario();
                        cargarArticulos();
                        return;
                    }


                    Historial historia = new Historial();

                    if (historia.moverCompleto(e.CommandArgument.ToString().Trim(), this.id, Request.Cookies[cookiesNombre.cookieApp]["id"].ToString()))
                    {
                        lblMensajeControl.ForeColor = System.Drawing.Color.Green;
                        lblMensajeControl.Text = Resources.Mensajes.Bien_Control;
                        txtItem.Text = "";
                        cargarInventario();
                        cargarArticulos();
                    }
                }
                else
                {
                    lblMensajeControl.ForeColor = System.Drawing.Color.Red;
                    lblMensajeControl.Text = Resources.Mensajes.Error_Control;
                }
            }
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        this.filtro = "";

        String valor = "";


        valor = txtControl.Text.Trim();

        if (valor != "")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.serialInterno like '%" + valor + "%' ";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.serialInterno like '%" + valor + "%' ";
            }
        }


        valor=txtExtra1.Text.Trim();

        if (valor != "")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.extra1 like'%" + valor + "%'";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.extra1 like'%" + valor + "%'";
            }
        }


        valor = txtExtra2.Text.Trim();
        if (valor != "")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.extra2 like'%" + valor + "%'";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.extra2 like'%" + valor + "%'";
            }
        }

        valor = lstEstadoF.SelectedValue;
        if (valor == "1" || valor == "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.activo='" + valor + "'";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.activo='" + valor + "'";
            }
        }


        valor = lstCategoriaF.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.categoria_id='" + valor + "'";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.categoria_id='" + valor + "'";
            }
        }

        valor = lstMarcaF.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.marca_id='" + valor + "'";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.marca_id='" + valor + "'";
            }
        }


        valor = lstModeloF.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.modelo_id='" + valor + "'";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.modelo_id='" + valor + "'";
            }
        }
        cargarArticulos();
    }


    protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
    {

        if (articulo.obtenerArticuloControl(txtItem.Text.Trim()))
        {
            if (ltrArticulo.Text.Trim() == txtItem.Text.Trim())
            {
                lblMensajeControl.Text = Resources.Mensajes.Control_No_Encontrado;
                lblMensajeControl.ForeColor = System.Drawing.Color.Red;
                return;
            }


            if (articulo.articulo_id != 0)
            {
                lblMensajeControl.Text = Resources.Mensajes.Control_Asociado;
                lblMensajeControl.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (!articulo.validarNoTengaArticulo(articulo.id))
                {
                    lblMensajeControl.Text = Resources.Mensajes.Control_Asociado;
                    lblMensajeControl.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (articulo.agregarItem(txtItem.Text.Trim(), this.id, Request.Cookies[cookiesNombre.cookieApp]["id"].ToString()))
                {

                    if(trasladarA.Value=="N"){
                        lblMensajeControl.ForeColor = System.Drawing.Color.Green;
                        lblMensajeControl.Text = Resources.Mensajes.Bien_Control;
                        txtItem.Text = "";
                        cargarInventario();
                        cargarArticulos();
                        return;
                    }

                    Historial historia = new Historial();

                    if (historia.moverCompleto(txtItem.Text.Trim(), this.id, Request.Cookies[cookiesNombre.cookieApp]["id"].ToString()))
                    {
                        lblMensajeControl.ForeColor = System.Drawing.Color.Green;
                        lblMensajeControl.Text = Resources.Mensajes.Bien_Control;
                        txtItem.Text = "";
                        cargarInventario();
                        cargarArticulos();
                    }
                }

                else
                {
                    lblMensajeControl.ForeColor = System.Drawing.Color.Red;
                    lblMensajeControl.Text = Resources.Mensajes.Error_Control;
                }
            }
        }
        else
        {
            lblMensajeControl.Text = Resources.Mensajes.Control_No_Encontrado;
        }
    }


    protected void grdArticulo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editArticulo")
        {

            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieArticulo, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            Response.Write("<script language='JavaScript'>window.open('AgregarArticulo.aspx')</script>");
        }
        else
        {
            if (e.CommandName == "eliminarArticulo")
            {
                articulo.agregarItem(e.CommandArgument.ToString(), 0, Request.Cookies[cookiesNombre.cookieApp]["id"].ToString());
                lblMensajeControl.Text = "";
                cargarInventario();
                cargarArticulos();
            }
        }

    }


    protected void grdArticulo2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArticulo2.PageIndex = e.NewPageIndex;
        cargarArticulos();
    }

    protected void grdArticulo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArticulo.PageIndex = e.NewPageIndex;
        cargarInventario();
    }

    protected void grdArticulo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
