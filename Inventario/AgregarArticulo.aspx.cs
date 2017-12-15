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

public partial class Inventario_AgregarInventario : System.Web.UI.Page
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



    private static int idStatico = 0;
    List<Dominio> domMarca, domKey, domCategoria;

    Articulo articulo = new Articulo();
    List<KeyWorks> izquierda, derecha;
    KeyWorks keyWorks = new KeyWorks();
    List<Empresa> empresas = new List<Empresa>();
    List<Modelo> domModelo = new List<Modelo>();


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

            lstCategoria.Items.Clear();
            domCategoria = Dominio.obtenerDominios(GlobalEnum.dominios.Categoria.ToString());
            foreach (Dominio d in domCategoria)
            {
                lstCategoria.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

            lstMarca.Items.Clear();
            domMarca = Dominio.obtenerDominios(GlobalEnum.dominios.Marca.ToString());
            foreach (Dominio d in domMarca)
            {
                lstMarca.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

            lstModelo.Items.Clear();
            lstModelo.Items.Add(new ListItem("S/E", "0"));
            domModelo = Modelo.obtenerListadoModelos(lstMarca.SelectedValue);
            foreach (Modelo d in domModelo)
            {
                lstModelo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

            lstEmpresa.Items.Clear();
            empresas = Empresa.obtenerListadoEmpresa(false);
            foreach (Empresa d in empresas)
            {
                lstEmpresa.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }
            
            
            if (this.id != 0)
            {
                lstCategoria.Enabled = false;


                if (Request.Cookies[cookiesNombre.cookieApp]["rol"].ToString() != GlobalEnum.roles.admin.ToString())
                {
                    lstEmpresa.Enabled = false;
                }



                popularValores();
            }

            else
            {

                domKey = Dominio.obtenerDominios(GlobalEnum.dominios.Tip.ToString());
                foreach (Dominio d in domKey)
                {
                    lsbTodo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
                }

            }
        }

    
        Categoria categoria = new Categoria();

        if (categoria.obtenerCagoriaId(Int32.Parse(lstCategoria.Items[lstCategoria.SelectedIndex].Value)))
        {
            ltrExtra1.Title = categoria.ejemplo1 == string.Empty ? "Sin Especificar :(" : categoria.ejemplo1;
            ltrExtra2.Title = categoria.ejemplo2 == string.Empty ? "Sin Especificar :(" : categoria.ejemplo2;
            idStatico = Int32.Parse(lstCategoria.Items[lstCategoria.SelectedIndex].Value);
        }

        if (categoria.pc == "S")
        {
            divPc.Visible = true;
        }


    }

    private bool cargarImagen(ref String nombreImagen)
    {
        Boolean fileOK = false;
        String path = Server.MapPath("~/Recursos/Articulos/");
        if (txtImagen.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(txtImagen.FileName).ToLower();
            String[] allowedExtensions = {".bmp", ".gif", ".png", ".jpeg", ".jpg" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
        }
        else {
            nombreImagen="sinimagen.png";
            return true;
        }

        if (fileOK)
        {
            try
            {
                string fileName = System.IO.Path.GetRandomFileName();
                txtImagen.PostedFile.SaveAs(path + fileName+"_"+txtImagen.FileName);
                nombreImagen=fileName+"_"+txtImagen.FileName;
                return true;
            }
            catch (Exception ex)
            {
                nombreImagen="sinimagen.png";
                return false;
            }
        }
        else
        {
            nombreImagen="sinimagen.png";
            return false;
        }

    }


    private void popularValores()
    {
        if (articulo.obtenerArticuloId(this.id))
        {

            txtSerial.Text = articulo.serial;
            txtExtra1.Text = articulo.extra1;
            txtExtra2.Text = articulo.extra2;
            chkActivo.Checked = articulo.activo;
            txtDetalle.Text = articulo.descrip;
            txtDetalle2.Text = articulo.detalle2;
            txtObs.Text = articulo.obs;

            txtIp.Text = articulo.ip;
            txtNombreRed.Text = articulo.nombre_red;
            txtNombreMaquina.Text = articulo.nombre_equipo;
            txtMac.Text = articulo.mac;
            txtProgramas.Text = articulo.programas;
            txtPermisos.Text = articulo.permisos;

            txtSistema.Text = articulo.so;
            txtFechaCompra.Text = articulo.fecha_compra != String.Empty ? articulo.fecha_compra.Split(' ')[0] : String.Empty;
            txtFechaFormateo.Text = articulo.fecha_formateo != String.Empty ? articulo.fecha_formateo.Split(' ')[0] : String.Empty;
            txtFechaGarantia.Text = articulo.fecha_garantia != String.Empty ? articulo.fecha_garantia.Split(' ')[0] : String.Empty;
            txtUusario.Text = articulo.usuarioso;
            txtGrupo.Text = articulo.grupo;
            imgArticulo.ImageUrl = "~/Recursos/Articulos/"+articulo.imagen;
            this.auxImagen=articulo.imagen;
            upload_value.Text = articulo.imagen;

            if (chkActivo.Checked == false)
            {
                txtObs.Enabled = true;
            }

            int pos = 0;
            int n;

            pos = 0;
            n = lstCategoria.Items.Count;
            for (int i = 0; i < n; i++)
            {

                if (lstCategoria.Items[i].Value == articulo.categoria_id.ToString())
                {
                    pos = i;
                    break;
                }
            }

            lstCategoria.SelectedIndex = pos;

            pos = 0;
            n = lstMarca.Items.Count;
            for (int i = 0; i < n; i++)
            {
                if (lstMarca.Items[i].Value == articulo.marca_id.ToString())
                {
                    pos = i;
                    break;
                }
            }
            lstMarca.SelectedIndex = pos;



            //---------------------------
            lstModelo.Items.Clear();
            domModelo = Modelo.obtenerListadoModelos(lstMarca.SelectedValue);
            lstModelo.Items.Add(new ListItem("S/E", "0"));
            foreach (Modelo d in domModelo)
            {
                lstModelo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }
            //---------------------------

            pos = 0;
            n = lstModelo.Items.Count;
            for (int i = 0; i < n; i++)
            {

                if (lstModelo.Items[i].Value == articulo.modelo_id.ToString())
                {
                    pos = i;
                    break;
                }
            }
            lstModelo.SelectedIndex = pos;



            pos = 0;
            n = lstEmpresa.Items.Count;
            for (int i = 0; i < n; i++)
            {

                if (lstEmpresa.Items[i].Value == articulo.empresa_id.ToString())
                {
                    pos = i;
                    break;
                }
            }
            lstEmpresa.SelectedIndex = pos;


            izquierda = keyWorks.obtenerKeywork(this.id, false);
            foreach (KeyWorks d in izquierda)
            {
                lsbTodo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }


            derecha = keyWorks.obtenerKeywork(this.id, true);
            foreach (KeyWorks d in derecha)
            {
                lsbArticulo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

        }
        else
        {
            Response.Redirect("Default.aspx");
        }

    }

    protected void d_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtGrupo_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void limpiarFormulario()
    {
        txtSerial.Text = "";
        txtExtra1.Text = "";
        txtExtra2.Text = "";
        txtDetalle.Text = "";
        txtDetalle2.Text = "";
        chkActivo.Checked = true;

        lstCategoria.SelectedIndex = 0;
        lstModelo.SelectedIndex = 0;
        lstMarca.SelectedIndex = 0;

        txtIp.Text = "";
        txtNombreRed.Text = "";
        txtNombreMaquina.Text = "";
        txtMac.Text = "";
        txtProgramas.Text = "";
        txtPermisos.Text = "";

        txtSistema.Text = "";
        txtFechaCompra.Text = "";
        txtFechaFormateo.Text = "";
        txtFechaGarantia.Text = "";
        txtUusario.Text = "";
        txtGrupo.Text = "";


    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (lsbTodo.SelectedIndex >= 0)
        {
            lsbArticulo.Items.Add(lsbTodo.SelectedItem);
            lsbTodo.Items.Remove(lsbTodo.SelectedItem);
        }
        lsbTodo.SelectedIndex = -1;
        lsbArticulo.SelectedIndex = -1;


    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        if (lsbArticulo.SelectedIndex >= 0)
        {
            lsbTodo.Items.Add(lsbArticulo.SelectedItem);
            lsbArticulo.Items.Remove(lsbArticulo.SelectedItem);
        }

        lsbTodo.SelectedIndex = -1;
        lsbArticulo.SelectedIndex = -1;
    }

    protected void btnGuardar_Click2(object sender, EventArgs e)
    {


    }

    protected void vldObs_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (chkActivo.Checked == false && txtObs.Text.Trim() == "")
        {
            args.IsValid = false;
            txtObs.Enabled = true;
        }


    }
    protected void btnGuardar_ClickG(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            String categoriaValor = lstCategoria.Items[lstCategoria.SelectedIndex].Value;
            String auxFecha = "", fechaFormateo = "", fechaCompra = "", fechaGarantia = "";
            String[] vec;

            if (txtFechaFormateo.Text != String.Empty)
            {
                auxFecha = String.Format("{0:d}", txtFechaFormateo.Text);
                vec = auxFecha.Split('/');
                fechaFormateo = vec[2] + "/" + vec[1] + "/" + vec[0];
            }

            if (txtFechaGarantia.Text != String.Empty)
            {
                auxFecha = String.Format("{0:d}", txtFechaGarantia.Text);
                vec = auxFecha.Split('/');
                fechaGarantia = vec[2] + "/" + vec[1] + "/" + vec[0];
            }
            if (txtFechaCompra.Text != String.Empty)
            {
                auxFecha = String.Format("{0:d}", txtFechaCompra.Text);
                vec = auxFecha.Split('/');
                fechaCompra = vec[2] + "/" + vec[1] + "/" + vec[0];
            }

            String nombreImagen = String.Empty;
            bool validaImagen = cargarImagen(ref  nombreImagen);

            if (this.auxImagen != "sinimagen.png" && nombreImagen == "sinimagen.png")
            {
                nombreImagen = this.auxImagen;
            }

            if (articulo.agregarActualizarArticulo(this.id, txtDetalle2.Text.Trim(), txtSerial.Text.Trim(), txtExtra1.Text.Trim(),
            txtExtra2.Text.Trim(), chkActivo.Checked, categoriaValor, lstMarca.Items[lstMarca.SelectedIndex].Value, lstModelo.Items[lstModelo.SelectedIndex].Value, txtDetalle.Text.Trim(), Request.Cookies[cookiesNombre.cookieApp]["id"].ToString(), lstEmpresa.Items[lstEmpresa.SelectedIndex].Value,
            txtObs.Text.Trim(), txtIp.Text.Trim(), txtNombreRed.Text.Trim(), txtNombreMaquina.Text.Trim(), txtMac.Text.Trim(), txtProgramas.Text.Trim(), txtPermisos.Text.Trim(), "", txtSistema.Text.Trim(), fechaFormateo, fechaCompra, fechaGarantia,
            txtUusario.Text.Trim(), txtGrupo.Text.Trim(), nombreImagen))

                if (articulo != null)
                {
                    int idArticulo = 0;

                    if (this.id == 0)
                    {
                        idArticulo = articulo.obtenerUltimoId();
                    }
                    else
                    {
                        idArticulo = this.id;
                    }

                    KeyWorks keyWorks = new KeyWorks();

                    if (keyWorks.agregarActualizarKeyWorks(idArticulo, lsbArticulo.Items))
                    {
                        if (validaImagen || !txtImagen.HasFile)
                        {
                            lblMensaje.Text = Resources.Mensajes.Bien_Articulo;
                            lblMensaje.ForeColor = System.Drawing.Color.Green;
                            Response.Redirect("Default.aspx");
                        }
                        else {
                            Response.Redirect("Default.aspx");
                        }
                    }
                    else
                    {
                        lblMensaje.Text = Resources.Mensajes.Error_Articulo;
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else
                {
                    lblMensaje.Text = Resources.Mensajes.Error_Articulo;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
        }
    }

    protected void lstMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstMarca.Items.Count > 0)
        {
            domModelo = Modelo.obtenerListadoModelos(lstMarca.SelectedValue);
            lstModelo.Items.Clear();

            lstModelo.Items.Add(new ListItem("S/E", "0"));

            foreach (Modelo u in domModelo)
            {
                lstModelo.Items.Add(new ListItem(u.nombre, u.id.ToString()));
            }

        }
    }



    protected void ImageButton4_Click1(object sender, ImageClickEventArgs e)
    {
        //   Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        // cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Marca.ToString());
        //   Util.AddCookieValues(cookieValues);
        //Response.Write("<script> var centerWidth = (window.screen.width - 400) / 2; var centerHeight = (window.screen.height - 400) / 2;window.open('../Administracion/Dominio/AgregarDominio.aspx','NewWindow','top=centerHeight,left=centerWidth,width=400,height=400,status=yes,resizable=yes,scrollbars=yes')</script>");
    }
    protected void lstCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        Categoria categoria = new Categoria();

        categoria.obtenerCagoriaId(Int32.Parse(lstCategoria.Items[lstCategoria.SelectedIndex].Value));
        if (categoria.pc == "S")
        {
            divPc.Visible = true;
        }
        else
        {
            divPc.Visible = false;
        }

        if (categoria.obtenerCagoriaId(Int32.Parse(lstCategoria.Items[lstCategoria.SelectedIndex].Value)))
        {
            ltrExtra1.Title = categoria.ejemplo1 == string.Empty ? "Sin Especificar :(" : categoria.ejemplo1;
            ltrExtra2.Title = categoria.ejemplo2 == string.Empty ? "Sin Especificar :(" : categoria.ejemplo2;
            idStatico = Int32.Parse(lstCategoria.Items[lstCategoria.SelectedIndex].Value);
        }
    }
/*
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionList(string prefixText, int count, string contextKey)
    {
        Articulo articulo=new Articulo();
        List<string> extras = articulo.obtenerExtras(true, idStatico);
        if (extras.Count == 0) {
            extras.Add("No hay recomendaciones");
        }
        return extras;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionList2(string prefixText, int count, string contextKey)
    {
        Articulo articulo = new Articulo();
        List<string> extras = articulo.obtenerExtras(false, idStatico);
        if (extras.Count == 0)
        {
            extras.Add("No hay recomendaciones");
        }
        return extras;

    }*/
/*
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieCategoria, string.Empty);
        Util.AddCookieValues(cookieValues);
        Response.Write("<script language='JavaScript'>window.open('../Administracion/Dominio/AgregarCategoria.aspx')</script>");
  
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieCategoria, string.Empty);
        Util.AddCookieValues(cookieValues);
        Response.Write("<script language='JavaScript'>window.open('../Administracion/Empresa/AgregarEmpresa.aspx')</script>");
    }

    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Marca.ToString());
        Util.AddCookieValues(cookieValues);
        Response.Write("<script language='JavaScript'>window.open('../Administracion/Dominio/AgregarDominio.aspx')</script>");
    }

    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieModelo, string.Empty);
        Util.AddCookieValues(cookieValues);
        Response.Write("<script language='JavaScript'>window.open('../Administracion/Dominio/AgregarModelo.aspx')</script>");
    }


    protected void ImageButton74_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Tip.ToString());
        Util.AddCookieValues(cookieValues);
        Response.Write("<script language='JavaScript'>window.open('../Administracion/Dominio/AgregarDominio.aspx')</script>");
    }*/

    protected void ImageButtonCN_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieCategoria, "");
        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../Administracion/Dominio/AgregarCategoria.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);
    }
    protected void ImageButtonEN_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieEmpresa, "");

        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../Administracion/Empresa/AgregarEmpresa.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);
    }
    protected void ImageButtonEA_Click(object sender, ImageClickEventArgs e)
    {
        lstEmpresa.Items.Clear();
        empresas = Empresa.obtenerListadoEmpresa(false);
        foreach (Empresa d in empresas)
        {
            lstEmpresa.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }
    }
    protected void ImageButtonCA_Click(object sender, ImageClickEventArgs e)
    {
        lstCategoria.Items.Clear();
        domCategoria = Dominio.obtenerDominios(GlobalEnum.dominios.Categoria.ToString());
        foreach (Dominio d in domCategoria)
        {
            lstCategoria.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }

    }

    //--

    protected void ImageButtonMN_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Marca.ToString());
        cookieValues.Add(cookiesNombre.cookieDominioValor, "");
        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../Administracion/Dominio/AgregarDominio.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);

    }

    protected void ImageButtonMA_Click(object sender, ImageClickEventArgs e) {

        lstMarca.Items.Clear();
        domMarca = Dominio.obtenerDominios(GlobalEnum.dominios.Marca.ToString());
        foreach (Dominio d in domMarca)
        {
            lstMarca.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }

        lstModelo.Items.Clear();
        domModelo = Modelo.obtenerListadoModelos(lstMarca.SelectedValue);
        lstModelo.Items.Add(new ListItem("S/E", "0"));
        foreach (Modelo d in domModelo)
        {
            lstModelo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }

    }

    protected void ImageButtonMON_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieModelo, "");
        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../Administracion/Dominio/AgregarModelo.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);

    }

    protected void ImageButtonMOA_Click(object sender, ImageClickEventArgs e)
    {
        lstModelo.Items.Clear();
        domModelo = Modelo.obtenerListadoModelos(lstMarca.SelectedValue);
        lstModelo.Items.Add(new ListItem("S/E", "0"));
        foreach (Modelo d in domModelo)
        {
            lstModelo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }

    }


    protected void ImageButtonKN_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Tip.ToString());
        cookieValues.Add(cookiesNombre.cookieDominioValor, "");
        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../Administracion/Dominio/AgregarDominio.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);

    }

    protected void ImageButtonKA_Click(object sender, ImageClickEventArgs e)
    {
        lstModelo.Items.Clear();
        domKey = Dominio.obtenerDominios(GlobalEnum.dominios.Tip.ToString());
        lsbTodo.Items.Add(new ListItem("S/E", "0"));
        foreach (Dominio d in domKey)
        {
            lsbTodo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }

    }


}
