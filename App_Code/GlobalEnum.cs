using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for GogalEnum
/// </summary>
public class GlobalEnum
{
	public GlobalEnum()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public enum dominios
    {

        Tip,
        Categoria,
        Marca,
        Modelo,
        Estado,
        Unidad,
        Rol

    }

    public enum roles {

        admin,
        superusuario,
        consultor,
        usuario
    }

    public enum condiciones { 
    
        and,
        or
    }

    public enum condicionesSN
    {
        S,
        N
    }

    public enum estadoMantenimiento
    {

        ESPERA,
        PENDIENTE,
        EXTERNO,
        PRUEBA,
        RESUELTO

    }



}
