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
/// Summary description for Ubicacion
/// </summary>
public class Ubicacion
{
    public int id { get; set; }
    public String nombre { get; set; }
    public String nombre_empresa { get; set; }
    public String descripcion { get; set; }
    public int empresa_id { get; set; }

    public String empresa { get; set; }
    public String imagen { get; set; }

	public Ubicacion()
	{
	
	}

    public static List<Ubicacion> obtenerListadoUbicacion(int empresa_id){

        List<Ubicacion> ubicaciones = new List<Ubicacion>();
        MySqlDataReader reader = null;
        String sql;

        if (empresa_id == 0)
        {
             sql = "select id,nombre,descripcion from ubicacion order by nombre asc";
        }
        else {
            sql = "select id,nombre,descripcion from ubicacion where empresa_id=@empresa_id order by nombre asc";
        }
            try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();

                MySqlCommand comando = new MySqlCommand(sql, conex);

                if (empresa_id != 0)
                {
                    comando.Parameters.AddWithValue("empresa_id", empresa_id);
                }

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Ubicacion ubicacion = new Ubicacion();
                    ubicacion.id = reader.GetInt32(0);
                    ubicacion.nombre = reader.GetString(1);
                    ubicacion.descripcion = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    ubicaciones.Add(ubicacion);
                }
            }
        }
        catch
        {
        }


        return ubicaciones;

    }

    public static List<Ubicacion> obtenerListadoUbicacionEmpresa(int empresa_id)
    {

        List<Ubicacion> ubicaciones = new List<Ubicacion>();
        MySqlDataReader reader = null;
        String sql;

        if (empresa_id == 0)
        {
            sql = @"select ubicacion.id,ubicacion.nombre,ubicacion.descripcion,empresa.nombre from ubicacion 
                    inner join empresa on ubicacion.empresa_id=empresa.id order by empresa.nombre asc";
        }
        else
        {
            sql = @"select ubicacion.id,ubicacion.nombre,ubicacion.descripcion,empresa.nombre from ubicacion 
                  inner join empresa on ubicacion.empresa_id=empresa.id where empresa_id=@empresa_id order by nombre asc";
        }
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();

                MySqlCommand comando = new MySqlCommand(sql, conex);

                if (empresa_id != 0)
                {
                    comando.Parameters.AddWithValue("empresa_id", empresa_id);
                }

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Ubicacion ubicacion = new Ubicacion();
                    ubicacion.id = reader.GetInt32(0);
                    ubicacion.nombre = reader.GetString(1);
                    ubicacion.descripcion = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    ubicacion.empresa = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    ubicaciones.Add(ubicacion);
                }
            }
        }
        catch
        {
        }
        finally
        {
            if (!reader.IsClosed && reader != null)
                reader.Close();

        }

        return ubicaciones;

    }



    public Boolean agregarActualizarUbicacion(int id, int empresa_id, String nombre, String descripcion,String imagen)
    {

        Boolean retu = true;
        String sql;

        if (id == 0)
        {
            sql = "insert into Ubicacion(nombre,descripcion,empresa_id,imagen) values(@nombre,@descripcion,@empresa_id,@imagen)";
        }
        else
        {
            sql = @"update Ubicacion set nombre=@nombre,descripcion=@descripcion,empresa_id=@empresa_id,imagen=@imagen where id=@id";
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

                comando.Parameters.AddWithValue("@nombre", nombre.ToUpper());
                comando.Parameters.AddWithValue("@empresa_id", empresa_id);
                comando.Parameters.AddWithValue("@descripcion", descripcion.ToUpper());
                comando.Parameters.AddWithValue("@imagen", imagen.ToUpper());
                comando.ExecuteNonQuery();
            }
        }
        catch
        {
            retu = false;
        }

        return retu;

    }



    public Boolean obtenerUbicacionId(int id)
    {
        Boolean bo = false;
        MySqlDataReader reader = null;

        String sql = @"select id,nombre,descripcion,empresa_id,imagen from ubicacion where id=@id";

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
                    this.nombre = reader.GetString(1);
                    this.descripcion = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    this.empresa_id = reader.GetInt32(3);
                    this.imagen = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    bo = true;

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

        return bo;
    }


    public Boolean obtenerUbicacionEmpresaId(int id)
    {
        Boolean bo = false;
        MySqlDataReader reader = null;

        String sql = @"select ubicacion.id,ubicacion.nombre,ubicacion.descripcion,empresa_id,empresa.nombre from ubicacion
                       inner join empresa on ubicacion=empresa_id=empresa.id where ubicacion.id=@id";

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
                    this.nombre = reader.GetString(1);
                    this.descripcion = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    this.empresa_id = reader.GetInt32(3);
                    this.empresa = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    bo = true;
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

        return bo;
    }
}
