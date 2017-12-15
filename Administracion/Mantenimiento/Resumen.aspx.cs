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
using System.Globalization;
using System.Collections.Generic;

public partial class Administracion_Mantenimiento_Resumen : System.Web.UI.Page
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



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieResumen));

            }
            catch
            {
                this.id = 0;

            }

            if (this.id == 0)
            {
                Response.Redirect("Default.aspx");
            }

            cargarMantenimiento();
            cargarDiagnosticos();
            cargarReparacionesExternas();

        }

    }

    private void cargarMantenimiento()
    {
        Mantenimiento mantenimiento = new Mantenimiento();
        if (mantenimiento.obtenerMantenimiento(this.id))
        {
            ltrControl.Text = mantenimiento.articulo.control;
            ltrDescripcion.Text = mantenimiento.descripcion;
            ltrEstado.Text = mantenimiento.estado;
            ltrRecepcion.Text = mantenimiento.fechaRecepcion;
            ltrResponsable.Text = mantenimiento.responsable.nombre;
            ltrUsuario.Text=mantenimiento.usuario.nombre;
            ltrSolicitud.Text=mantenimiento.fechaSolicitud;

        }else{
            Response.Redirect("Default.aspx");
        }
    }


    private void cargarDiagnosticos()
    {

        Diagnostico diagnostico = new Diagnostico();

        List<Diagnostico> diagnosticos = diagnostico.obtenerDiagnosticos(this.id);
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("revision");
        dataTable.Columns.Add("responsable");
        dataTable.Columns.Add("descripcion");


        foreach (Diagnostico a in diagnosticos)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["id"] = a.id;
            dataRow["revision"] = a.fechaRevision;
            dataRow["responsable"] = a.responsable.ToUpper();
            dataRow["descripcion"] = a.descripcion.ToUpper();
            dataTable.Rows.Add(dataRow);

        }


        dataTable.AcceptChanges();
        grdDiagnosticos.DataSource = dataTable;
        grdDiagnosticos.DataBind();

    }


    private void cargarReparacionesExternas()
    {
        ReparacionExterna reparacionExterna = new ReparacionExterna();

        List<ReparacionExterna> reparacionExternas = reparacionExterna.obtenerReparacionExternas(this.id);
 
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("responsable");
            dataTable.Columns.Add("envio");
            dataTable.Columns.Add("recepcion");
            dataTable.Columns.Add("costo");
            dataTable.Columns.Add("descripcion");
            dataTable.Columns.Add("descripcionf");
        

            foreach (ReparacionExterna a in reparacionExternas)
            {
                DataRow dataRow = dataTable.NewRow();

                dataRow["id"] = a.id;
                dataRow["responsable"] = a.responsable.nombre;
                dataRow["envio"] = a.fechaEnvio;
                dataRow["recepcion"] = a.fechaRecepcion;
                dataRow["costo"] = a.costo.ToString("f2", CultureInfo.CreateSpecificCulture("es-ES"));
                dataRow["descripcion"] = a.descripcion.ToUpper();
                dataRow["descripcionf"] = a.descripcionf.ToUpper();

                dataTable.Rows.Add(dataRow);

            }


            dataTable.AcceptChanges();
            grdExterno.DataSource = dataTable;
            grdExterno.DataBind();


    }
}
