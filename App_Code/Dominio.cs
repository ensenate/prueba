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
/// Summary description for Domnio
/// </summary>
public class Dominio
{
    public int id { get; set; }
    public String nombre { get; set; }

	public Dominio()
	{
		
	}


    public Boolean verificarUsoDominio(String id, String tipoDominio)
    {

        String sql = "";
        MySqlDataReader reader = null;

        switch (tipoDominio)
        {

            case "Tip":
                sql = "select tip_id from keywork where tip_id=@id limit 1";
                break;

            case "Estado":
                sql = "select estado_id from historial where estado_id=@id limit 1";
                break;

            case "Unidad":
                sql = "select unidad_id from desgaste where unidad_id=@id limit 1";
                break;

            case "Modelo":
                sql = "select modelo_id from articulo where modelo_id=@id limit 1";
                break;

            case "Marca":
                sql = "select marca_id from articulo where marca_id=@id limit 1";
                break;

            case "Categoria":
                sql = "select id from articulo where categoria_id=@id limit 1";
                break;

            default:
                return false;
        }

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@id",id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    return false;
                }
            }
        }
        catch
        {
            return false;
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return true;

    }



    public Boolean eliminarDominio(String id, String tipoDominio)
    {

        if (validarDominio(tipoDominio))
        {
            Boolean retu = true;
            String sql = "delete from @tabla where id=@id";
            MySqlConnection conex = null;
            try
            {
                conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());
                conex.Open();
                sql = sql.Replace("@tabla", tipoDominio);

                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {
                retu = false;
            }
            finally
            {

                conex.Close();
            }

            return retu;
        }
        else
        {
            return false;
        }
    }

    public bool agregarActualizarDominio(int id,String nombre,String tipoDominio){
        
        bool retu = true;
        String sql;

        if (id == 0)
        {
            sql = "insert into @tabla(nombre) values(@nombre)";
        }
        else {
            sql = "update @tabla set nombre=@nombre where id=@id";
        }
            MySqlConnection conex=null;
        try
        {
            conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());
            conex.Open();
            sql = sql.Replace("@tabla",tipoDominio);

            MySqlCommand comando = new MySqlCommand(sql, conex);
            comando.Parameters.AddWithValue("@nombre", nombre.ToUpper());

            if (id != 0)
            {
                comando.Parameters.AddWithValue("@id", id);
            }

            comando.ExecuteNonQuery();
    
        }
        catch (Exception)
        {
            retu = false;
        }
        finally {

            conex.Close();
        }

        return retu;
    }

    public Boolean validarDominio(string tipoDominio)
    {
        if (GlobalEnum.dominios.Estado.ToString() == tipoDominio ||
            GlobalEnum.dominios.Tip.ToString() == tipoDominio ||
            GlobalEnum.dominios.Marca.ToString() == tipoDominio ||
            GlobalEnum.dominios.Modelo.ToString() == tipoDominio ||
            GlobalEnum.dominios.Unidad.ToString() == tipoDominio)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public Boolean validarNombreUnico(int id,String nombre, String tipoDominio)
    {

        Boolean retu = true;
        MySqlDataReader reader = null;
        String sql = "select id from @tabla where nombre=@nombre and id!=@id ";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                sql = sql.Replace("@tabla", tipoDominio);
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@nombre", nombre.ToUpper());
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    retu = false;
                }
            }
        }
        catch
        {
            retu = false;
        }
        finally
        {
            if(!reader.IsClosed && reader!=null)
               reader.Close();
                       
        }

        return retu;
    }



    public static List<Dominio> obtenerDominios(String tipoDominio)
    {

        List<Dominio> dominios = new List<Dominio>();

        MySqlDataReader reader = null;
        String sql = "select id,nombre from @tabla order by nombre ASC";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                sql = sql.Replace("@tabla",tipoDominio);
                MySqlCommand comando = new MySqlCommand(sql, conex);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Dominio dominio = new Dominio();
                    dominio.id = reader.GetInt32(0);
                    dominio.nombre = reader.GetString(1);
                    dominios.Add(dominio);
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

        return dominios;

    }

    public static List<Dominio> obtenerDominios(String tipoDominio,int id)
    {

        List<Dominio> dominios = new List<Dominio>();

        MySqlDataReader reader = null;
        String sql = "select id,nombre from @tabla order by id ASC";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                sql = sql.Replace("@tabla", tipoDominio);
                MySqlCommand comando = new MySqlCommand(sql, conex);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Dominio dominio = new Dominio();
                    dominio.id = reader.GetInt32(0);
                    dominio.nombre = reader.GetString(1);
                    dominios.Add(dominio);
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

        return dominios;

    }


    public Boolean obtenerDominioId(int id, String tipoDominio)
    {
        Boolean bo=false;
        MySqlDataReader reader = null;
        String sql = "select id,nombre from @tabla where id=@id limit 1";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                sql = sql.Replace("@tabla",tipoDominio);
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@id",id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    this.id = reader.GetInt32(0);
                    this.nombre = reader.GetString(1);
                    bo = true;
                }
            }
        }
        catch
        {
            bo=false;
        }
        finally
        {
            if (!reader.IsClosed && reader != null)
                reader.Close();

        }

        return bo;
    }

}
