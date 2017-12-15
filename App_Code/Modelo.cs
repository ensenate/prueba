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
/// Summary description for Modelo
/// </summary>
public class Modelo
{

    public int id { get; set; }
    public String nombre { get; set; }
    public int marca_id { get; set; }

    public String marca { get; set; }


	public Modelo()
	{
	}


    public Boolean agregarActualizarModelo(int id,String nombre, String marca_id)
    {
        Boolean retu = true;
        String sql;

        if (id == 0)
        {
            sql = "insert into modelo(nombre,marca_id) values(@nombre,@marca_id)";
        }
        else
        {
            sql = "update modelo set nombre=@nombre,marca_id=@marca_id where id=@id";
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
                comando.Parameters.AddWithValue("@marca_id", marca_id);

                comando.ExecuteNonQuery();
            }
        }
        catch
        {
            retu = false;
        }

        return retu;
    }



    public  Boolean obtenerModeloId(int id)
    {
        Boolean bo = false;
        MySqlDataReader reader = null;
        String sql = @" select modelo.id,modelo.nombre,modelo.marca_id,marca.nombre  from modelo
                        inner join marca on modelo.marca_id=marca.id
                        where modelo.id=@id";
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
                    this.marca_id = reader.GetInt32(2);
                    this.marca = reader.GetString(3);

                    bo = true;
                }
            }
        }
        catch
        {
            bo = false;
        }
        finally
        {

            if (!reader.IsClosed && reader != null)
                reader.Close();

        }

        return bo;
    }


    public static List<Modelo> obtenerListadoModelos(String marca_id)
    {
        List<Modelo> modelos=new List<Modelo>();

        MySqlDataReader reader = null;

        String sql = @" select  modelo.id,modelo.nombre,modelo.marca_id,marca.nombre from modelo
                        inner join marca on modelo.marca_id=marca.id
                        where marca_id=@marca_id order by modelo.nombre asc";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@marca_id", marca_id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Modelo modelo = new Modelo();
                    modelo.id = reader.GetInt32(0);
                    modelo.nombre = reader.GetString(1);
                    modelo.marca_id = reader.GetInt32(2);
                    modelo.marca = reader.GetString(3);
                    modelos.Add(modelo);
                    
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

        return modelos;
    }


    public static List<Modelo> obtenerListadoModelos()
    {
        List<Modelo> modelos = new List<Modelo>();

        MySqlDataReader reader = null;

        String sql = @" select modelo.id,modelo.nombre,modelo.marca_id,marca.nombre from modelo
                        inner join marca on modelo.marca_id=marca.id order by marca.nombre ";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Modelo modelo = new Modelo();
                    modelo.id = reader.GetInt32(0);
                    modelo.nombre = reader.GetString(1);
                    modelo.marca_id = reader.GetInt32(2);
                    modelo.marca = reader.GetString(3);
                    modelos.Add(modelo);

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

        return modelos;
    }



    public Boolean modeloUnicoPorMarca(String nombre,String marca_id) {

        Boolean bo = true;

        MySqlDataReader reader = null;
        String sql = " select id from modelo where nombre=@nombre and marca_id=@marca_id limit 1";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@marca_id", marca_id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    bo = false;
                }
            }
        }
        catch
        {
            bo = false;
        }
        finally
        {

            if (!reader.IsClosed && reader != null)
                reader.Close();

        }


        return bo;
    }



}
