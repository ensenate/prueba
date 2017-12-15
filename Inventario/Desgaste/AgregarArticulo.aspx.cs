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

public partial class Desgaste_AgregarProducto : System.Web.UI.Page
{

    List<Dominio> domCategoria, domMarca, domUnidades;
    List<Empresa> empresas;
    List<Modelo> domModelo;
    Desgaste desgate = new Desgaste();


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
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieDesgaste));

            }
            catch
            {
                this.id = 0;
            }

            domCategoria = Dominio.obtenerDominios(GlobalEnum.dominios.Categoria.ToString());
            foreach (Dominio d in domCategoria)
            {
                lstCategoria.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

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

            empresas = Empresa.obtenerListadoEmpresa(false);
            foreach (Empresa d in empresas)
            {
                lstEmpresa.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }

            domUnidades = Dominio.obtenerDominios(GlobalEnum.dominios.Unidad.ToString());
            foreach (Dominio d in domUnidades)
            {
                lstUnidad.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }


            if (this.id != 0)
            {
                popularValores();


                if (Request.Cookies[cookiesNombre.cookieApp]["rol"].ToString() != GlobalEnum.roles.admin.ToString())
                {
                    lstEmpresa.Enabled = false;
                }
            }


            Dictionary<string, string> cookieValues = new Dictionary<string, string>();
            cookieValues.Add(cookiesNombre.cookieCategoria, string.Empty);
            Util.AddCookieValues(cookieValues);


        }

    }

    private void popularValores()
    {
        if (desgate.obtenerDesgasteId(this.id))
        {
            txtDesc.Text = desgate.descrip;


            int pos = 0;
            int n = lstCategoria.Items.Count;
            for (int i = 0; i < n; i++)
            {
                if (lstCategoria.Items[i].Text == desgate.categoria)
                {
                    pos = i;
                    break;
                }
            }
            lstCategoria.SelectedIndex = pos;

            //---
            pos = 0;
            n = lstMarca.Items.Count;
            for (int i = 0; i < n; i++)
            {
                if (lstMarca.Items[i].Text == desgate.marca)
                {
                    pos = i;
                    break;
                }
            }
            lstMarca.SelectedIndex = pos;


            //---

            lstModelo.Items.Clear();
            domModelo = Modelo.obtenerListadoModelos(lstMarca.SelectedValue);
            lstModelo.Items.Add(new ListItem("S/E", "0"));
            foreach (Modelo d in domModelo)
            {
                lstModelo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
            }


            pos = 0;
            n = lstModelo.Items.Count;
            for (int i = 0; i < n; i++)
            {
                if (lstModelo.Items[i].Text == desgate.modelo)
                {
                    pos = i;
                    break;
                }
            }
            lstModelo.SelectedIndex = pos;


            //---
            pos = 0;
            n = lstEmpresa.Items.Count;
            for (int i = 0; i < n; i++)
            {
                if (lstEmpresa.Items[i].Text == desgate.empresa)
                {
                    pos = i;
                    break;
                }
            }
            lstEmpresa.SelectedIndex = pos;

            //---
            pos = 0;
            n = lstUnidad.Items.Count;
            for (int i = 0; i < n; i++)
            {
                if (lstUnidad.Items[i].Text == desgate.unidad)
                {
                    pos = i;
                    break;
                }
            }
            lstUnidad.SelectedIndex = pos;

        }

        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void btnGuardar_Click1(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            if (desgate.agregarActualizarDesgastes(this.id, txtDesc.Text.Trim(), Int32.Parse(lstModelo.SelectedValue), Int32.Parse(lstMarca.SelectedValue), Int32.Parse(lstCategoria.SelectedValue), Int32.Parse(lstEmpresa.SelectedValue), Int32.Parse(lstUnidad.SelectedValue)))
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Error_Desgaste;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    private void limpiarFormulario()
    {

        txtDesc.Text = "";

        lstUnidad.SelectedIndex = 0;
        lstEmpresa.SelectedIndex = 0;
        lstCategoria.SelectedIndex = 0;
        lstMarca.SelectedIndex = 0;
        lstModelo.SelectedIndex = 0;

    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int bo = desgate.validarDescripcion(txtDesc.Text.Trim(), this.id);

        switch (bo)
        {
            case 0:
                args.IsValid = false;
                break;

            case -1:
                args.IsValid = false;
                lblMensaje.Text = Resources.Mensajes.Error_Desgaste;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                break;

        }
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
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

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieCategoria, "");
        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../../Administracion/Dominio/AgregarCategoria.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);

    }

    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        lstCategoria.Items.Clear();
        domCategoria = Dominio.obtenerDominios(GlobalEnum.dominios.Categoria.ToString());
        foreach (Dominio d in domCategoria)
        {
            lstCategoria.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }

    }



    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Marca.ToString());
        cookieValues.Add(cookiesNombre.cookieDominioValor, "");
        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../../Administracion/Dominio/AgregarDominio.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);

    }

    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
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


    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieModelo,"");
        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../../Administracion/Dominio/AgregarModelo.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);

    }

    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        lstModelo.Items.Clear();
        domModelo = Modelo.obtenerListadoModelos(lstMarca.SelectedValue);
        lstModelo.Items.Add(new ListItem("S/E", "0"));
        foreach (Modelo d in domModelo)
        {
            lstModelo.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }
        
    }



    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieDominioNombre, GlobalEnum.dominios.Unidad.ToString());
        cookieValues.Add(cookiesNombre.cookieDominioValor, "");
        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../../Administracion/Dominio/AgregarDominio.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);

    }

    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        lstUnidad.Items.Clear();
        domUnidades = Dominio.obtenerDominios(GlobalEnum.dominios.Unidad.ToString());
        foreach (Dominio d in domUnidades)
        {
            lstUnidad.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }

    }



    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Dictionary<string, string> cookieValues = new Dictionary<string, string>();
        cookieValues.Add(cookiesNombre.cookieEmpresa, "");

        Util.AddCookieValues(cookieValues);

        string mensaje = "window.open('../../Administracion/Empresa/AgregarEmpresa.aspx');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", mensaje, true);

    }

    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        lstEmpresa.Items.Clear();
        empresas = Empresa.obtenerListadoEmpresa(false);
        foreach (Empresa d in empresas)
        {
            lstEmpresa.Items.Add(new ListItem(d.nombre, d.id.ToString()));
        }

    }

}
