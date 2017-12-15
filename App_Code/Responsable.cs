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
using MySql.Data.MySqlClient;
using System.Collections.Generic;

/// <summary>
/// Summary description for Personal
/// </summary>
public class Responsable
{
    public int id { get; set; }
    public String rif { get; set; }
    public String nombre { get; set; }
    public String telefono { get; set; }
    public String correo { get; set; }
    public Boolean tipo { get; set; }
    public int empresa_id { get; set; }

    public String empresa { get; set; }

    public String soporte { get; set; }


	public Responsable()
	{
	
	}


    public Boolean agregarActualizarPersonal(int id, String rif, String nombre, String telefono, String correo, Boolean tipo, String empresa_id)
    {

        Boolean retu = true;
        String sql;

        if (id == 0)
        {
            sql = @"insert into responsable(rif,nombre,telefono,correo,tipo,empresa_id) 
                       values(@rif,@nombre,@telefono,@correo,@tipo,@empresa_id)";
        }
        else
        {
            sql = @"update responsable set rif=@rif,nombre=@nombre,telefono=@telefono,
                    correo=@correo,tipo=@tipo,empresa_id=@empresa_id where id=@id";
        }
            try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                if (id != 0)
                {
                    comando.Parameters.AddWithValue("@id", id);
                }
                comando.Parameters.AddWithValue("@rif", rif.ToUpper());
                comando.Parameters.AddWithValue("@nombre", nombre.ToUpper());
                comando.Parameters.AddWithValue("@telefono", telefono);
                comando.Parameters.AddWithValue("@correo", correo.ToUpper());
                comando.Parameters.AddWithValue("@tipo", tipo);
                comando.Parameters.AddWithValue("@empresa_id", empresa_id);
                comando.ExecuteNonQuery();
            }
        }
        catch
        {
            retu = false;
        }

        return retu;

    }

    public Boolean obtenerPersonalId(int id) {

        Boolean retu = false;
        MySqlDataReader reader = null;
        String sql = @"select id,rif,nombre,telefono,correo,tipo,empresa_id from responsable
                       where id=@id";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@id", id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    this.id = reader.GetInt32(0);
                    this.rif = reader.GetString(1);
                    this.nombre = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    this.telefono = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    this.correo = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    this.tipo = reader.GetBoolean(5);
                    this.empresa_id = reader[6] == DBNull.Value ? 0 : Int32.Parse(reader.GetValue(6).ToString());
                    retu = true;
                }
            }
        }
        catch
        {
        }
        finally
        {
            if ( reader != null && !reader.IsClosed)
                reader.Close();

        }

        return retu;
    
    }


    public static List<Responsable> obtenerListaPersonal(int tipo)
    {
        List<Responsable> personas = new List<Responsable>();
 
        MySqlDataReader reader = null;
        String sql;
        if (tipo > 1)
        {
            sql = @" select responsable.id,responsable.rif,responsable.nombre,responsable.telefono,responsable.correo,responsable.tipo,ifnull(empresa.nombre,'N/A'),soporte from responsable 
                     left join empresa on responsable.empresa_id=empresa.id
                     order by responsable.nombre";
        }
        else
        {
            sql = @"select responsable.id,responsable.rif,responsable.nombre,responsable.telefono,responsable.correo,responsable.tipo,ifnull(empresa.nombre,'N/A'),soporte  from responsable
                     left join empresa on responsable.empresa_id=empresa.id 
                     where tipo=@tipo order by responsable.nombre";
        }         
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                if (tipo == 1 || tipo==0)
                {
                    comando.Parameters.AddWithValue("@tipo", tipo);
                }
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Responsable persona = new Responsable();
                    persona.id = reader.GetInt32(0);
                    persona.rif = reader.GetString(1);
                    persona.nombre = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    persona.telefono = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    persona.correo = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    persona.tipo = reader.GetBoolean(5);
                    persona.empresa = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                    persona.soporte = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    personas.Add(persona);
                }
            }
        }
        catch{

        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return personas;

    }


    public Boolean obtenerPersonalRif(string rif)
    {

        Boolean retu = false;
        MySqlDataReader reader = null;
        String sql = @"select id,rif,nombre,telefono,correo from responsable
                       where rif=@rif";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@rif", rif.ToUpper());
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    this.id = reader.GetInt32(0);
                    this.rif = reader.GetString(1);
                    this.nombre = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    this.telefono = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    this.correo = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    retu = true;
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

        return retu;

    }
}
