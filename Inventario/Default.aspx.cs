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
using System.Text;
using System.IO;

using System.Xml;


public partial class Inventario_Default : System.Web.UI.Page
{   
    List<Ubicacion> ubicaciones;
    List<Modelo> domModelo = new List<Modelo>();
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

    public String filtroKeys
    {
        get
        {
            if (ViewState["filtroKeys"] == null) return "";
            return ViewState["filtroKeys"].ToString();

        }
        set
        {
            ViewState["filtroKeys"] = value;

        }
    }

    public String controles
    {
        get
        {
            if (ViewState["controles"] == null) return "";
            return ViewState["controles"].ToString();

        }
        set
        {
            ViewState["controles"] = value;

        }
    }

    List<Dominio> domCategoriaF, domMarcaF, domModeloF,domKeyF;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.filtro = Util.GetCookieValue(cookiesNombre.cookieFiltroArticulo.ToString());

            }
            catch
            {
                this.filtro = string.Empty;
            }


            try
            {
                this.filtroKeys = Util.GetCookieValue(cookiesNombre.cookieFiltroKeyArticulo.ToString());

            }
            catch
            {
                this.filtroKeys = string.Empty;
            }


            try
            {
                this.controles = Util.GetCookieValue(cookiesNombre.cookieFiltroControlesArticulo.ToString());

            }
            catch
            {
                this.controles = string.Empty;
            }


            String rol = Request.Cookies[cookiesNombre.cookieApp]["rol"].ToString();
            lblMensaje.Text = "";

            if (rol != GlobalEnum.roles.admin.ToString() && rol != GlobalEnum.roles.superusuario.ToString())
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

            //domModeloF = Dominio.obtenerDominios(GlobalEnum.dominios.Modelo.ToString());
            lstModeloF.Items.Add(new ListItem("N/A", "0"));
            /*foreach (Dominio d in domModeloF)
            {
                lstModeloF.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }*/


            domKeyF = Dominio.obtenerDominios(GlobalEnum.dominios.Tip.ToString());
            lstKeywords.Items.Add(new ListItem("N/A", "0"));


            foreach (Dominio d in domKeyF)
            {
                lstKeywords.Items.Add(new ListItem(d.nombre, d.id.ToString()));

            }

            List<Empresa> empresas = Empresa.obtenerListadoEmpresa(false);
            lstEmpresa2.Items.Add(new ListItem("N/A", "0"));
            lstEmpresa.Items.Add(new ListItem("N/A", "0"));

            foreach (Empresa d in empresas)
            {
                lstEmpresa.Items.Add(new ListItem(d.nombre, d.id.ToString()));
                lstEmpresa2.Items.Add(new ListItem(d.nombre, d.id.ToString()));

            }

            //-------------------------------------------------------------------------------------------

            List<Responsable> responsables = Responsable.obtenerListaPersonal(1);
            lstResponsable.Items.Add(new ListItem("N/A", "0"));


            foreach (Responsable d in responsables)
            {
                lstResponsable.Items.Add(new ListItem(d.nombre, d.id.ToString()));

            }



            //List<Ubicacion> ubicaciones = Ubicacion.obtenerListadoUbicacion(0);
            //lstUbicacion.Items.Add(new ListItem("N/A", ""));
            lstUbicacion.Items.Add(new ListItem("N/A", "0"));

/*
            foreach (Ubicacion d in ubicaciones)
            {
                lstUbicacion.Items.Add(new ListItem(d.nombre, d.id.ToString()));

            }
*/

            List<Dominio> domEstadoF = Dominio.obtenerDominios(GlobalEnum.dominios.Estado.ToString());
            lstEstado.Items.Add(new ListItem("N/A", ""));
            lstEstado.Items.Add(new ListItem("NINGUNO", "0"));


            foreach (Dominio d in domEstadoF)
            {
                lstEstado.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }


            //-------------------------------------------------------------------------------------------

            lstEstadoF.Items.Add(new ListItem("N/A", "2"));
            lstEstadoF.Items.Add(new ListItem("Si", "1"));
            lstEstadoF.Items.Add(new ListItem("No", "0"));
            lstEstadoF.SelectedIndex = 1;

            if (this.filtro == string.Empty && this.filtroKeys == string.Empty)
            {
                this.filtro = " where tabla.borrado='N' and tabla.activo='1' ";
            }
            else
            {
                recuperarFiltro();
            }

            cargarInventario();
        }

    }

    //optimizar
    private void recuperarFiltro()
    {
        try
        {
            if (this.controles != string.Empty)
            {
                String[] tempControles = controles.Split(',');
                for (int i = 0; i < tempControles.Length; i = i + 3)
                {
                    if (tempControles[i+2] == "TextBox")
                    {
                        ((TextBox)pnlFiltros.FindControl(tempControles[i])).Text = tempControles[i + 1];
                    }
                    else
                    {
                        if (tempControles[i+2] == "DropDownList")
                        {
                            ((DropDownList)pnlFiltros.FindControl(tempControles[i])).SelectedValue = tempControles[i + 1];
                        }
                    }
                }
            }
        }
        catch { }
    }

    //optimizar
   private void componenteFiltro() {

        String tempControles = string.Empty;

            foreach (Control control in pnlFiltros.Controls)
            {
                if (control is TextBox)
                {
                    tempControles += ((TextBox)control).ID;
                    tempControles += ",";
                    tempControles += ((TextBox)control).Text;
                    tempControles += ",";
                    tempControles += "TextBox";
                    tempControles += ",";
                }
                if (control is DropDownList)
                {
                    tempControles += ((DropDownList)control).ID;
                    tempControles += ",";
                    tempControles += ((DropDownList)control).SelectedValue;
                    tempControles += ",";
                    tempControles += "DropDownList";
                    tempControles += ",";
                }

        }

            this.controles = tempControles.Substring(0, tempControles.Length-1);

    }

    private void cargarInventario()
    {
        Articulo articulo = new Articulo();

        List<Articulo> articulos = articulo.obtenerListaArticulosReferencias(this.filtro,this.filtroKeys,0);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("idArticulo", typeof(int));
        dataTable.Columns.Add("serial");
        dataTable.Columns.Add("control");
        dataTable.Columns.Add("extra1");
        dataTable.Columns.Add("extra2");
        dataTable.Columns.Add("activo");
        dataTable.Columns.Add("empresa");
        dataTable.Columns.Add("ubicacion");
        dataTable.Columns.Add("marca");
        dataTable.Columns.Add("modelo");
        dataTable.Columns.Add("categoria");
        dataTable.Columns.Add("pertenece");
        dataTable.Columns.Add("contenidos");
        dataTable.Columns.Add("ocultarAsociar");
        dataTable.Columns.Add("ocultarTraslado");
        dataTable.Columns.Add("ocultarEliminar");

    
        foreach (Articulo a in articulos)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = a.id;
            dataRow["serial"] = a.serial;
            dataRow["control"] = a.control;
            dataRow["extra1"] = a.extra1;
            dataRow["extra2"] = a.extra2;
            dataRow["activo"] = a.activo == true ? "Si" : "No";

            dataRow["empresa"] = a.empresa;
            dataRow["ubicacion"] = a.ubicacion;
            

            dataRow["marca"] = a.marca;
            dataRow["modelo"] = a.modelo;
            dataRow["categoria"] = a.categoria;
            dataRow["pertenece"] = a.pertenece;
            dataRow["contenidos"] = a.contenidos;


            if (a.asociar == false )
            {
                dataRow["ocultarAsociar"] = "ocultarAsociar";
            }

            else {
                dataRow["ocultarAsociar"] = "";
            }



            if (a.borrado == "S" || a.contenidos>0)
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
        grdArticulo .DataSource = dataTable;
        grdArticulo.DataBind();
       

   
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
         Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieArticulo, "");
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarArticulo.aspx");
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
       
    }

    protected void grdArticulo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editArticulo")
        {

            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieArticulo, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("AgregarArticulo.aspx");

        }
        else
        {
            if (e.CommandName == "historiaArticulo")
            {

                Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                cookieValues.Add(cookiesNombre.cookieArticulo, e.CommandArgument.ToString());
                Util.AddCookieValues(cookieValues);
                this.Page.Response.Redirect("Historia/Default.aspx");

            }
            else
            {
                if (e.CommandName == "soporteArticulo")
                {
                    Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                    cookieValues.Add(cookiesNombre.cookieArticulo, e.CommandArgument.ToString());
                    Util.AddCookieValues(cookieValues);
                    this.Page.Response.Redirect("Soporte/Default.aspx");
                }
                else
                {
                    if (e.CommandName == "relacionarArticulo")
                    {

                        Articulo articulo = new Articulo();
                        if (articulo.obtenerArticuloId(Int32.Parse(e.CommandArgument.ToString())))
                        {
                            if (articulo.asociar == false)
                            {
                                lblMensaje.Text = Resources.Mensajes.Error_Asociar;
                                lblMensaje.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                            cookieValues.Add(cookiesNombre.cookieRelacionArticulo, e.CommandArgument.ToString());
                            Util.AddCookieValues(cookieValues);
                            this.Page.Response.Redirect("RelacionarArticulos.aspx");
                        }
                    }
                    else
                    {

                        if (e.CommandName == "imprimirEtiqueta")
                        {
                            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                            cookieValues.Add(cookiesNombre.cookieImprimir, e.CommandArgument.ToString());
                            Util.AddCookieValues(cookieValues);
                            this.Page.Response.Redirect("../Reportes/GenerarEtiqueta.aspx");
                        }
                        else
                        {
                            if (e.CommandName == "eliminarArticulo")
                            {
                                Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                                cookieValues.Add(cookiesNombre.cookieEliminarArticulo, e.CommandArgument.ToString());
                                Util.AddCookieValues(cookieValues);
                                this.Page.Response.Redirect("EliminarArticulo.aspx");
                            }
                            else
                            {
                                if (e.CommandName == "imprimirEtiqueta2")
                                {
                                    Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                                    cookieValues.Add(cookiesNombre.cookieImprimir, e.CommandArgument.ToString());
                                    Util.AddCookieValues(cookieValues);
                                    this.Page.Response.Redirect("../Reportes/VerReporte.aspx?r=2");
                                }
                            }
                        }

                    }
                }
            }
        }


    }

    protected void grdArticulo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         grdArticulo.PageIndex = e.NewPageIndex;
         cargarInventario();
    }

    protected void grdArticulo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    private void busqueda() {


        this.filtro = "";
        this.filtroKeys = "";

        String valor;

        valor = txtControlF.Text.Trim();
        if (valor != "")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.serialInterno like'%" + valor + "%' ";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.serialInterno like'%" + valor + "%' ";
            }
        }


        valor = txtSerialF.Text.Trim();
        if (valor != "")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.serial like'%" + valor + "%' ";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.serial like'%" + valor + "%' ";
            }
        }


        valor = txtExtra1F.Text.Trim();
        if (valor != "")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.extra1 like'%" + valor + "%' ";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.extra like'%" + valor + "%' ";
            }
        }


        valor = txtExtra2F.Text.Trim();
        if (valor != "")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.extra2 like'%" + valor + "%' ";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.extra2 like'%" + valor + "%' ";
            }
        }

        valor = lstEstadoF.SelectedValue;
        if (valor == "1" || valor == "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.activo='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.activo='" + valor + "' ";
            }
        }

        valor = lstEmpresa.SelectedValue;

        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.empresa_id='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.empresa_id='" + valor + "' ";
            }
        }


        valor = lstCategoriaF.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.categoria_id='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.categoria_id='" + valor + "' ";
            }
        }

        valor = lstMarcaF.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where tabla.marca_id='" + valor + " ' ";
            }
            else
            {
                this.filtro = this.filtro + " and tabla.marca_id='" + valor + "' ";
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

        valor = lstKeywords.SelectedValue;



        if (valor != "0")
        {
            filtroKeys = " tip_id='" + valor + "' ";
        }


        if (lstEliminados.SelectedIndex != 2)
        {
            if (lstEliminados.SelectedIndex == 0)
            {
                if (this.filtro == "")
                {
                    this.filtro = " where tabla.borrado='N' ";
                }
                else
                {
                    this.filtro = this.filtro + " and tabla.borrado='N' ";
                }
            }
            else
            {
                if (this.filtro == "")
                {
                    this.filtro = " where tabla.borrado='S' ";
                }
                else
                {
                    this.filtro = this.filtro + " and tabla.borrado='S' ";
                }
            }
        }


        //----------------------------------------------------------------------------------------------


        valor = lstResponsable.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where his.responsablea='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and his.responsablea='" + valor + "' ";
            }
        }


        valor = lstUbicacion.SelectedValue;

        if (valor != "")
        {
            if (valor != "0")
            {

                if (this.filtro == "")
                {
                    this.filtro = " where his.ubicaciona='" + valor + "' ";
                }
                else
                {
                    this.filtro = this.filtro + " and his.ubicaciona='" + valor + "' ";
                }
            }

            //bug arreglado de la ubicacion de los articulos
            /*else
            {
                if (this.filtro == "")
                {
                    this.filtro = " where his.ubicaciona is null ";
                }
                else
                {
                    this.filtro = this.filtro + " and his.ubicaciona is null ";
                }
            }*/
        }

        valor = lstEstado.SelectedValue;
        if (valor != "")
        {
            if (valor != "0")
            {
                if (this.filtro == "")
                {
                    this.filtro = " where his.estadoa='" + valor + "' ";
                }
                else
                {
                    this.filtro = this.filtro + " and his.estadoa='" + valor + "' ";

                }
            }
            else
            {
                if (this.filtro == "")
                {
                    this.filtro = " where his.estadoa is null";
                }
                else
                {
                    this.filtro = this.filtro + " and his.estadoa is null ";

                }
            }
        }

        valor = lstEmpresa2.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where his.ubicacionae='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and his.ubicacionae='" + valor + "' ";

            }
        }

    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {

        busqueda();
        componenteFiltro();

        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieFiltroArticulo, this.filtro);
        cookieValues.Add(cookiesNombre.cookieFiltroKeyArticulo, this.filtroKeys);
        cookieValues.Add(cookiesNombre.cookieFiltroControlesArticulo, this.controles);
        Util.AddCookieValues(cookieValues);

        cargarInventario();
 
    }

    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
     
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition",
         "attachment;filename=GridViewExport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grdArticulo.AllowPaging = false;
        grdArticulo.DataBind();
        grdArticulo.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
{
    /* Verifies that the control is rendered */
}

    protected void lstEmpresa2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstEmpresa2.Items.Count > 0)
        {
            ubicaciones = Ubicacion.obtenerListadoUbicacion(Int32.Parse(lstEmpresa2.SelectedValue));
            lstUbicacion.Items.Clear();
            lstUbicacion.Items.Add(new System.Web.UI.WebControls.ListItem("N/A","0"));
            foreach (Ubicacion u in ubicaciones)
            {
                lstUbicacion.Items.Add(new System.Web.UI.WebControls.ListItem(u.nombre, u.id.ToString()));
            }
        }       
    }
    protected void lstMarcaF_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstMarcaF.Items.Count > 0)
        {
            domModelo = Modelo.obtenerListadoModelos(lstMarcaF.SelectedValue);
            lstModeloF.Items.Clear();
            lstModeloF.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
            foreach (Modelo u in domModelo)
            {
                lstModeloF.Items.Add(new System.Web.UI.WebControls.ListItem(u.nombre, u.id.ToString()));
            }
        }

    }


}
