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
using System.Collections.Generic;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for Rol
/// </summary>
public class Rol
{

    public int id { get; set; }
    public String nombre{get;set;}

	public Rol()
	{
	
	}

    public static List<Rol> obtenerRoles()
    {
        List<Rol> roles = new List<Rol>();
        MySqlDataReader reader = null;

        String sql = "select id,nombre from rol";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Rol rol = new Rol();
                    rol.id = reader.GetInt32(0);
                    rol.nombre = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                    roles.Add(rol);
                }
            }
        }
        catch
        {

        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return roles;
    }
}
