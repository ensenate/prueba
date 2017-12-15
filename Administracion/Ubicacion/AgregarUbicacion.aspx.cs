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

public partial class Matenimiento_Ubicacion_AgregarUbicacion : System.Web.UI.Page
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

    public String auxImagen
    {
        get
        {
            if (ViewState["auxImagen"] == null) return "sinimagen.png";
            return ViewState["auxImagen"].ToString();

        }
        set
        {
            ViewState["auxImagen"] = value;

        }
    }

    List<Empresa> empresas = new List<Empresa>();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieUbicacion));

            }
            catch
            {
                this.id = 0;
            }

            empresas = Empresa.obtenerListadoEmpresa(false);
            lstEmpresa.Items.Clear();

            foreach (Empresa em in empresas)
            {
                lstEmpresa.Items.Add(new ListItem(em.nombre, em.id.ToString()));
            }
            if (id != 0)
            {
                popularValores();
            }
        }
    }

    private void popularValores()
    {
        Ubicacion ubicacion = new Ubicacion();

        if (ubicacion.obtenerUbicacionId(this.id))
        {
            txtNombre.Text = ubicacion.nombre;
            txtDescripcion .Text = ubicacion.descripcion;
            imgUbicacion.ImageUrl = "~/Recursos/Ubicaciones/" + ubicacion.imagen;
            upload_value.Text = ubicacion.imagen;
            this.auxImagen = ubicacion.imagen;

            int pos = 0;
            int n = lstEmpresa.Items.Count;
            for(int i=0;i<n;i++){

                if (lstEmpresa.Items[i].Value == ubicacion.empresa_id.ToString())
                {
                    pos = i;
                    break;
                }
            }
            lstEmpresa.SelectedIndex = pos;
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Ubicacion ubicacion = new Ubicacion();


        String nombreImagen = String.Empty;
        bool validaImagen = cargarImagen(ref  nombreImagen);

        if (this.auxImagen != "sinimagen.png" && nombreImagen == "sinimagen.png")
        {
            nombreImagen = this.auxImagen;
        }

        if (ubicacion.agregarActualizarUbicacion(this.id, Int32.Parse(lstEmpresa.SelectedValue), txtNombre.Text, txtDescripcion.Text, nombreImagen))
        {
            if (validaImagen || !txtImagen.HasFile)
            {
                lblMensaje.Text = Resources.Mensajes.Bien_Dominio;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                Response.Redirect("Default.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            lblMensaje.Text = Resources.Mensajes.Error_Ubicacion;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    private bool cargarImagen(ref String nombreImagen)
    {
        Boolean fileOK = false;
        String path = Server.MapPath("~/Recursos/Ubicaciones/");
        if (txtImagen.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(txtImagen.FileName).ToLower();
            String[] allowedExtensions = { ".bmp", ".gif", ".png", ".jpeg", ".jpg" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
        }
        else
        {
            nombreImagen = "sinimagen.png";
            return true;
        }

        if (fileOK)
        {
            try
            {
                string fileName = System.IO.Path.GetRandomFileName();
                txtImagen.PostedFile.SaveAs(path + fileName + "_" + txtImagen.FileName);
                nombreImagen = fileName + "_" + txtImagen.FileName;
                return true;
            }
            catch (Exception ex)
            {
                nombreImagen = "sinimagen.png";
                return false;
            }
        }
        else
        {
            nombreImagen = "sinimagen.png";
            return false;
        }

    }



}
