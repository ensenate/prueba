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

public partial class Administracion_Mantenimiento_Default : System.Web.UI.Page
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

    public String complemento
    {
        get
        {
            if (ViewState["complemento"] == null) return "";
            return ViewState["complemento"].ToString();

        }
        set
        {
            ViewState["complemento"] = value;

        }
    }

    List<Dominio> domCategoriaF;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            domCategoriaF = Dominio.obtenerDominios(GlobalEnum.dominios.Categoria.ToString());
            lstCategoria.Items.Add(new ListItem("N/A", "0"));
            foreach (Dominio d in domCategoriaF)
            {
                lstCategoria.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }
            lstCategoria.SelectedIndex = 0;

            lstEstado.Items.Add(new ListItem("N/A", ""));
            foreach (String en in Enum.GetNames(typeof(GlobalEnum.estadoMantenimiento)))
            {
                lstEstado.Items.Add(new ListItem(en, en));
            }
            lstEstado.SelectedIndex = 2;

            this.filtro = " where estado='PENDIENTE' ";
            this.complemento = " order by id desc ";

            cargarMantenimientos();


        }
    }

    private void cargarMantenimientos()
    {
        Mantenimiento mantenimiento = new Mantenimiento();

        List<Mantenimiento> mantenimientos = mantenimiento.obtenerListadoMantenimientos(this.filtro, this.complemento);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("idArticulo", typeof(int));
        dataTable.Columns.Add("serial");
        dataTable.Columns.Add("control");
        dataTable.Columns.Add("marca");
        dataTable.Columns.Add("modelo");
        dataTable.Columns.Add("categoria");

        dataTable.Columns.Add("empresa");
        dataTable.Columns.Add("ubicacion");
        dataTable.Columns.Add("responsablea");

        dataTable.Columns.Add("fechaSolicitud");
        dataTable.Columns.Add("usuario");
        dataTable.Columns.Add("descripcion");
        dataTable.Columns.Add("estado");

        dataTable.Columns.Add("fechaRecibido");
        dataTable.Columns.Add("responsable");
        dataTable.Columns.Add("fechaEnvio");

        dataTable.Columns.Add("ocultarRecepcion");
        dataTable.Columns.Add("ocultarExternoCerrado");
        dataTable.Columns.Add("ocultarExternoAbrir");
        dataTable.Columns.Add("ocultarResuelto");
        dataTable.Columns.Add("ocultarDiagnostico");

        foreach (Mantenimiento a in mantenimientos)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = a.id;
            dataRow["idArticulo"] = a.articulo_id;
            dataRow["serial"] = a.articulo.serial;
            dataRow["control"] = a.articulo.control;


            dataRow["marca"] = a.articulo.marca;
            dataRow["modelo"] = a.articulo.modelo;
            dataRow["responsablea"] = a.articulo.responsable_nombre;


            dataRow["empresa"] = a.articulo.empresa;
            dataRow["ubicacion"] = a.articulo.ubicacion;
            dataRow["ubicacion"] = a.articulo.ubicacion;

            dataRow["fechaRecibido"] = a.fechaRecepcion;
            dataRow["responsable"] = a.responsable.nombre;

            dataRow["fechaSolicitud"] = a.fechaSolicitud;
            dataRow["usuario"] = a.usuario.nombre;
            dataRow["descripcion"] = a.descripcion;
            dataRow["estado"] = a.estado;
            dataRow["fechaEnvio"] = a.fecha_envio;


            if (a.estado == GlobalEnum.estadoMantenimiento.ESPERA.ToString())
            {
                dataRow["ocultarRecepcion"] = "";
                dataRow["ocultarExternoCerrado"] = "ocultarAsociar";
                dataRow["ocultarExternoAbrir"] = "ocultarAsociar";
                dataRow["ocultarResuelto"] = "ocultarAsociar";
                dataRow["ocultarDiagnostico"] = "ocultarAsociar";
            }
            else
            {
                if (a.estado == GlobalEnum.estadoMantenimiento.PENDIENTE.ToString())
                {
                    dataRow["ocultarRecepcion"] = "ocultarAsociar";
                    dataRow["ocultarExternoCerrado"] = "ocultarAsociar";

                   
                    if (a.numeroDiagnostico>0)
                    {
                        dataRow["ocultarExternoAbrir"] = "";
                        dataRow["ocultarResuelto"] = "";
                    }
                    else
                    {
                        dataRow["ocultarExternoAbrir"] = "ocultarAsociar";
                        dataRow["ocultarResuelto"] = "ocultarAsociar";
                    }
                    
                    dataRow["ocultarDiagnostico"] = "";
                }
                else
                {
                    if (a.estado == GlobalEnum.estadoMantenimiento.EXTERNO.ToString())
                    {
                        dataRow["ocultarRecepcion"] = "ocultarAsociar";
                        dataRow["ocultarExternoCerrado"] = "";
                        dataRow["ocultarExternoAbrir"] = "ocultarAsociar";
                        dataRow["ocultarResuelto"] = "ocultarAsociar";
                        dataRow["ocultarDiagnostico"] = "ocultarAsociar";
                    }
                    else
                    {
                        if (a.estado == GlobalEnum.estadoMantenimiento.PRUEBA.ToString())
                        {
                            dataRow["ocultarRecepcion"] = "ocultarAsociar";
                            dataRow["ocultarExternoCerrado"] = "ocultarAsociar";
                            dataRow["ocultarExternoAbrir"] = "ocultarAsociar";
                            dataRow["ocultarResuelto"] = "ocultarAsociar";
   
                            dataRow["ocultarDiagnostico"] = "";
                        }
                        else
                        {
                            dataRow["ocultarRecepcion"] = "ocultarAsociar";
                            dataRow["ocultarExternoCerrado"] = "ocultarAsociar";
                            dataRow["ocultarExternoAbrir"] = "ocultarAsociar";
                            dataRow["ocultarResuelto"] = "ocultarAsociar";
                            dataRow["ocultarDiagnostico"] = "ocultarAsociar";
                        }
                    }
                }
            }

            dataTable.Rows.Add(dataRow);

        }


        dataTable.AcceptChanges();
        grdArticulo.DataSource = dataTable;
        grdArticulo.DataBind();



    }

    protected void grdArticulo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Diagnostico")
        {

            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieDiagnostico, e.CommandArgument.ToString());
            Util.AddCookieValues(cookieValues);
            this.Page.Response.Redirect("Diagnostico.aspx");
        }
        else
        {
            if (e.CommandName == "Recepcion")
            {

                Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                cookieValues.Add(cookiesNombre.cookieRecepcion, e.CommandArgument.ToString());
                Util.AddCookieValues(cookieValues);
                this.Page.Response.Redirect("Recepcion.aspx");
            }
            else
            {
                if (e.CommandName == "Resuelto")
                {
                    Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                    cookieValues.Add(cookiesNombre.cookieEnvio, e.CommandArgument.ToString());
                    Util.AddCookieValues(cookieValues);
                    this.Page.Response.Redirect("Enviar.aspx");
                }
                else
                {
                    if (e.CommandName == "Ver")
                    {
                        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                        cookieValues.Add(cookiesNombre.cookieResumen, e.CommandArgument.ToString());
                        Util.AddCookieValues(cookieValues);
                        this.Page.Response.Redirect("Resumen.aspx");
                    }
                    else
                    {
                        if (e.CommandName == "Articulo")
                        {
                            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                            cookieValues.Add(cookiesNombre.cookieArticulo, e.CommandArgument.ToString());
                            Util.AddCookieValues(cookieValues);
                            string mensaje = "window.open('../../Inventario/AgregarArticulo.aspx');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);
                            //Response.Write("<script language='JavaScript'>window.open('../../Inventario/AgregarArticulo.aspx')</script>");
                        }
                        else
                        {
                            if (e.CommandName == "ExternoAbrir")
                            {
                                Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                                cookieValues.Add(cookiesNombre.cookieSoporteExterno, e.CommandArgument.ToString());
                                Util.AddCookieValues(cookieValues);
                                this.Page.Response.Redirect("ReparacionExterna.aspx");
                            }
                            else
                            {
                                if (e.CommandName == "ExternoCerrar")
                                {
                                    Dictionary<string, string> cookieValues = new Dictionary<string, string>();
                                    cookieValues.Add(cookiesNombre.cookieSoporteExternoCerrar, e.CommandArgument.ToString());
                                    Util.AddCookieValues(cookieValues);
                                    this.Page.Response.Redirect("ReparacionExternaCerrar.aspx");
                                }
                            }
                        }
                    }
                }
            }
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArticulo.PageIndex = e.NewPageIndex;
        cargarMantenimientos();
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        String valor = String.Empty;
        this.filtro = String.Empty;

        valor = lstCategoria.SelectedValue;
        if (valor != "0")
        {
            if (this.filtro == "")
            {
                this.filtro = " where cat.categoria_id='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and cat.categoria_id='" + valor + "' ";
            }
        }

        //ToString("f2", CultureInfo.CreateSpecificCulture("es-ES"));
        valor = lstEstado.SelectedValue;
        if (valor != string.Empty)
        {
            if (this.filtro == "")
            {
                this.filtro = " where man.estado='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and man.estado='" + valor + "' ";
            }
        }


        valor = txtInicio.Text;

        if (valor != string.Empty)
        {
            string[] vecTemp = valor.Split('/');
            valor = vecTemp[2] + "-" + vecTemp[1] + "-" + vecTemp[0];

            if (this.filtro == "")
            {
                this.filtro = " where man.fecha_solicitud>='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and man.fecha_solicitud>='" + valor + "' ";
            }
        }
        

        valor = txtFin.Text;
        if (valor != string.Empty)
        {
            string[] vecTemp = valor.Split('/');
            valor = vecTemp[2] + "-" + vecTemp[1] + "-" + vecTemp[0];

            if (this.filtro == "")
            {
                this.filtro = " where man.fecha_solicitud<='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and man.fecha_solicitud<='" + valor + "' ";
            }
        }


        valor = txtArticulo.Text.Trim();
        if (valor != string.Empty)
        {
            if (this.filtro == "")
            {
                this.filtro = " where art.serialInterno='" + valor + "' ";
            }
            else
            {
                this.filtro = this.filtro + " and art.serialInterno='" + valor + "' ";
            }
        }


        cargarMantenimientos();

    }


    protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AgregarMantenimiento.aspx");
    }
}