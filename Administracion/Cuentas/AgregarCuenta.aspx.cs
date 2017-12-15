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

public partial class Administracion_Cuentas_AgregarCuenta : System.Web.UI.Page
{

    List<Rol> roles = new List<Rol>();
    List<Responsable> responsables = new List<Responsable>();

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
                this.id = Int32.Parse(Util.GetCookieValue(cookiesNombre.cookieCuenta));
            }
            catch
            {
                this.id = 0;
            }

            roles = Rol.obtenerRoles();
            lstRol.Items.Clear();

            foreach (Rol em in roles)
            {
                 lstRol.Items.Add(new ListItem(em.nombre, em.id.ToString()));
            }

            responsables = Responsable.obtenerListaPersonal(1);
            lstResponsable.Items.Clear();

            foreach (Responsable em in responsables)
            {
                lstResponsable.Items.Add(new ListItem(em.nombre, em.id.ToString()));
            }

            if (this.id != 0)
            {
                popularValores();
                lstResponsable.Enabled = false;
                txtUsuario.Enabled = false;
            }
        }

    }

    private void popularValores()
    {
        Cuenta cuenta = new Cuenta();

        if (cuenta.obtenerCuentaId(this.id))
        {
            txtUsuario.Text = cuenta.usuario;
           
            int pos = 0;
            int n = lstResponsable.Items.Count;
            for (int i = 0; i < n; i++)
            {
                if (lstResponsable.Items[i].Value == cuenta.responsable_id.ToString())
                {
                    pos = i;
                    break;
                }
            }
            lstResponsable.SelectedIndex = pos;

            pos = 0;
            n = lstRol.Items.Count;
            for (int i = 0; i < n; i++)
            {
                if (lstRol.Items[i].Value == cuenta.rol_id.ToString())
                {
                    pos = i;
                    break;
                }
            }
            lstRol.SelectedIndex = pos;
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
             if (txtClave.Text == "" && this.id == 0)
            {
                lblMensaje.Text = Resources.Mensajes.Clave_Requerida;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            Cuenta cuenta = new Cuenta();

            if (this.id == 0)
            {
                if (!cuenta.verificarCuenta(lstResponsable.SelectedValue))
                {
                    lblMensaje.Text = Resources.Mensajes.Usuario_Cuenta;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            if (cuenta.agregarActualizarCuenta(this.id, txtUsuario.Text.Trim(), txtClave.Text.Trim(), Int32.Parse(lstRol.SelectedValue), Int32.Parse(lstResponsable.SelectedValue.ToString())))
            {
                lblMensaje.Text = Resources.Mensajes.Bien_Cuenta;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMensaje.Text = Resources.Mensajes.Error_Cuenta;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (this.id == 0)
        {
            Cuenta cuenta = new Cuenta();
            int bo = cuenta.validarCuenta(txtUsuario.Text.Trim(), this.id);
            switch (bo)
            {
                case 1:
                    // args.IsValid = true;
                    break;

                case 0:
                    args.IsValid = false;
                    break;

                case -1:
                    args.IsValid = false;
                    lblMensaje.Text = Resources.Mensajes.Error_Cuenta;
                    break;
            }
        }
    }
}

