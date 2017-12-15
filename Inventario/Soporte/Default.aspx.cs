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

public partial class Matenimiento_Soporte_Default : System.Web.UI.Page
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
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieArticulo));

            }
            catch
            {
                this.id = 0;
            }

            if (this.id == 0)
            {
                Response.Redirect("../Default.aspx");
            }

            cargarSoportes();

        }
    }

    private void cargarSoportes()
    {
            Soporte soporte = new Soporte();
            List<Soporte> soportes = null;// soporte.obtenerListaSoportes(this.id);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inicio");
            dataTable.Columns.Add("fin");
            dataTable.Columns.Add("obsi");
            dataTable.Columns.Add("obsf");
            dataTable.Columns.Add("responsable");


            foreach (Soporte s in soportes)
            {
                DataRow dataRow = dataTable.NewRow();

                dataRow["inicio"] = s.fecha_inicio;
                dataRow["fin"] = s.fecha_fin;
                dataRow["obsi"] = s.obsi;
                dataRow["obsf"] = s.obsf;
                dataRow["responsable"] = s.responsable;

                dataTable.Rows.Add(dataRow);

            }

            dataTable.AcceptChanges();
       //     grdSoporte.DataSource = dataTable;
       //     grdSoporte.DataBind();
        }

    

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieArticulo, this.id.ToString());
        Util.AddCookieValues(cookieValues);
        Response.Redirect("AgregarSoporte.aspx");
    }

}
