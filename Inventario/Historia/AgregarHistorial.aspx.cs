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
using System.Globalization;

public partial class Inventario_Historia_AgregarHistoria : System.Web.UI.Page
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
    List<Dominio> estados;
    List<Responsable> responsables, autorizados;
    Historial historia = new Historial();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieArticulo));

            }
            catch
            {
                this.id = 0;
            }

            if (this.id == 0)
            {
                Response.Redirect("Default.aspx");
            }

            empresas = Empresa.obtenerListadoEmpresa(true);
            foreach (Empresa em in empresas)
            {
                lstEmpresa.Items.Add(new ListItem(em.nombre, em.id.ToString()));
            }

            if (lstEmpresa.Items.Count > 0)
            {
                ubicaciones = Ubicacion.obtenerListadoUbicacion(Int32.Parse(lstEmpresa.SelectedValue));
                foreach (Ubicacion u in ubicaciones)
                {
                    lstUbicacion.Items.Add(new ListItem(u.nombre, u.id.ToString()));
                }

            }

            estados = Dominio.obtenerDominios(GlobalEnum.dominios.Estado.ToString());
            foreach (Dominio d in estados)
            {
                lstEstado.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }


            responsables = Responsable.obtenerListaPersonal(1);

            foreach (Responsable r in responsables)
            {
                lstResponsable.Items.Add(new ListItem(r.nombre, r.id.ToString()));
            }


            autorizados = responsables;

            foreach (Responsable r in autorizados)
            {
                lstAutorizado.Items.Add(new ListItem(r.nombre, r.id.ToString()));
            }

            cargarInventario();

            if (grdArticulo.Rows.Count <= 0)
            {
                btnMarcalos.Visible = false;
                btnDesmarcalos.Visible = false;
            }

        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {

 
    }
  
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void lstEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstEmpresa.Items.Count > 0)
        {
            ubicaciones = Ubicacion.obtenerListadoUbicacion(Int32.Parse(lstEmpresa.SelectedValue));
            lstUbicacion.Items.Clear();
            foreach (Ubicacion u in ubicaciones)
            {
                lstUbicacion.Items.Add(new ListItem(u.nombre, u.id.ToString()));
            }
       
        }
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

        string []vecTemp=txtFecha.Text.Split('/');

        String auxFecha = vecTemp[1] + "/" + vecTemp[0] + "/" + vecTemp[2];
        String []vec = auxFecha.Split('/');
        auxFecha = vec[2] + "/" + vec[1] + "/" + vec[0];
        int bo = historia.validarFecha(this.id, auxFecha);

        switch (bo)
        {
            case 1:
                // args.IsValid = true;
                break;

            case 0:
                args.IsValid = false;
                lblMensaje.Text = Resources.Mensajes.Fecha_Invalidad;
                break;

            case -1:
                args.IsValid = false;
                lblMensaje.Text = Resources.Mensajes.Fecha_Invalidad;
                break;
        }

    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            if (lstUbicacion.Items.Count > 0)
            {
                Articulo articulo = new Articulo();

                if (articulo.obtenerArticuloId(this.id))
                {
                    if (articulo.articulo_id != 0)
                    {
                        lblMensaje.Text = Resources.Mensajes.Articulo_Asociado;
                        return;
                    }
                }
                else
                {
                    lblMensaje.Text = Resources.Mensajes.Error_Historia;
                    return;
                }

                string[] vecTemp = txtFecha.Text.Split('/');

                String auxFecha = vecTemp[1] + "/" + vecTemp[0] + "/" + vecTemp[2];
                String[] vec = auxFecha.Split('/');
                auxFecha = vec[2] + "/" + vec[1] + "/" + vec[0];


                if (historia.agregarHistoria(auxFecha, txtDesc.Text.Trim(), Int32.Parse(lstUbicacion.SelectedValue), this.id, Int32.Parse(lstResponsable.SelectedValue), Int32.Parse(lstEstado.SelectedValue), Int32.Parse(Request.Cookies[cookiesNombre.cookieApp]["id"].ToString()), Int32.Parse(lstAutorizado.SelectedValue)))
                {
                    if (!moveAsociadosSeleccionados(auxFecha))
                    {
                        lblMensaje.Text = Resources.Mensajes.Error_Mover_Asociados;
                    }

                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMensaje.Text = Resources.Mensajes.Error_Historia;
                }
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Ubicacion_Requerida;
            }
        }
    }

    private bool moveAsociadosSeleccionados(String auxFecha)
    {
        int estadoA = 0;
        int idFila = 0;

        foreach (GridViewRow row in grdArticulo.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("chkMarcado");

            if (cb != null && cb.Checked)
            {
                idFila = Convert.ToInt32(grdArticulo.DataKeys[row.RowIndex].Value);

                if (historia.obtenerUltimaHistoria(idFila))
                {
                    estadoA = historia.estado_id;
                }
                else
                {
                    estadoA = Int32.Parse(lstEstado.SelectedValue);
                }

                if (!historia.agregarHistoria(auxFecha, txtDesc.Text.Trim() + " (Traslado Automatico)", Int32.Parse(lstUbicacion.SelectedValue), idFila, Int32.Parse(lstResponsable.SelectedValue), estadoA, Int32.Parse(Request.Cookies[cookiesNombre.cookieApp]["id"].ToString()), Int32.Parse(lstAutorizado.SelectedValue)))
                {
                    return false;
                }

            }
        }
        return true;

    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }
   
     private void cargarInventario()
    {
        Articulo articulo = new Articulo();

        List<Articulo> articulos = articulo.obtenerListaArticulosReferencias(" where tabla.articulo_id="+this.id.ToString(), string.Empty,0);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("serial");
        dataTable.Columns.Add("control");
        dataTable.Columns.Add("empresa");
        dataTable.Columns.Add("ubicacion");
        dataTable.Columns.Add("marca");
        dataTable.Columns.Add("modelo");
        dataTable.Columns.Add("categoria");
        dataTable.Columns.Add("marcado",typeof(bool));
    
        foreach (Articulo a in articulos)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = a.id;
            dataRow["serial"] = a.serial;
            dataRow["control"] = a.control;
 
            dataRow["empresa"] = a.empresa;
            dataRow["ubicacion"] = a.ubicacion;
            

            dataRow["marca"] = a.marca;
            dataRow["modelo"] = a.modelo;
            dataRow["categoria"] = a.categoria;

            dataRow["marcado"] = true;
            
           
            dataTable.Rows.Add(dataRow);

        }


        dataTable.AcceptChanges();
        grdArticulo .DataSource = dataTable;
        grdArticulo.DataBind();
       

   
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
                             this.Page.Response.Redirect("../Reportes/Default.aspx");
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

     protected void marcarDesmarcar(Boolean check)
     {

         foreach (GridViewRow row in grdArticulo.Rows)
         {

             CheckBox cb = (CheckBox)row.FindControl("chkMarcado");
             if (cb != null)
             {
                 cb.Checked = check;
             }
         }

     }

 
     protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
     {
         marcarDesmarcar(true);
     }
     protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
     {
         marcarDesmarcar(false);
     }
}
