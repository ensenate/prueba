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

public partial class Administracion_Categoria_AgregarCategoria : System.Web.UI.Page
{
    Categoria categoria = new Categoria();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieCategoria));

        }
        catch
        {
            this.id = 0;
        }


        if (!IsPostBack && this.id != 0)
        {
            popularValores();
            txtPrefijo.Enabled = false;
        }

    }


    private void popularValores()
    {
        Categoria categoria = new Categoria();

        if (categoria.obtenerCagoriaId(this.id))
        {
            txtNombre.Text = categoria.nombre;
            txtPrefijo.Text = categoria.prefijo;
            ckAsociar.Checked = categoria.asociar;
            txtEjemplo1.Text = categoria.ejemplo1;
            txtEjemplo2.Text = categoria.ejemplo2;
            imgCategoria.ImageUrl = "~/Recursos/Categorias/" + categoria.imagen;
            upload_value.Text = categoria.imagen;
            this.auxImagen = categoria.imagen;
        }
        else
        {
            Response.Redirect("ListadoCategoria.aspx");
        }

    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListadoCategoria.aspx");
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int bo = categoria.UnicoNombre(txtNombre.Text.Trim(),this.id);
        switch (bo)
        {
            case 0:
                args.IsValid = false;
                break;

            case -1:
                args.IsValid = false;
                lblMensaje.Text = Resources.Mensajes.Error_Categoria;
                break;
        
        }

    }

    protected void vldPrefijo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int bo = categoria.UnicoPrefijo (txtPrefijo.Text.Trim(), this.id);
        switch (bo)
        {

            case 0:
                args.IsValid = false;
                break;

            case -1:
                args.IsValid = false;
                lblMensaje.Text = Resources.Mensajes.Error_Categoria;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                break;

        }
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            string isPc="N";

            if (chkPc.Checked)
            {
                isPc = "S";
            }

            String nombreImagen = String.Empty;
            bool validaImagen = cargarImagen(ref  nombreImagen);

            if (this.auxImagen !="sinimagen.png"  && nombreImagen == "sinimagen.png")
            {
                nombreImagen= this.auxImagen;
            }

            if (categoria.agregarActualizarCategoria(this.id, txtNombre.Text.Trim(), txtPrefijo.Text.Trim(), ckAsociar.Checked, txtEjemplo1.Text.Trim(), txtEjemplo2.Text.Trim(), isPc, nombreImagen))
            {
                if (validaImagen || !txtImagen.HasFile)
                {
                    lblMensaje.Text = Resources.Mensajes.Bien_Categoria;
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("ListadoCategoria.aspx");
               }
                else {
                    Response.Redirect("ListadoCategoria.aspx");
                }
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Error_Categoria;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    private bool cargarImagen(ref String nombreImagen)
    {
        Boolean fileOK = false;
        String path = Server.MapPath("~/Recursos/Categorias/");
        if (txtImagen.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(txtImagen.FileName).ToLower();
            String[] allowedExtensions = { ".bmp",".gif", ".png", ".jpeg", ".jpg" };
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
