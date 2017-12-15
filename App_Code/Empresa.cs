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
/// Summary description for Empresa
/// </summary>
public class Empresa
{


    public int id { get; set; }
    public String nombre { get; set; }
    public String rif { get; set; }
    public String telefono { get; set; }
    public String direccion { get; set; }
    public String descripcion { get; set; }

	public Empresa()
	{

	}


    public static List<Empresa> obtenerListadoEmpresa(Boolean ubicaciones)
    {

        List<Empresa> empresas = new List<Empresa>();
        MySqlDataReader reader = null;


        String sql;

        if (!ubicaciones)
        {
            sql = "select id,nombre,rif,telefono,direccion,descripcion from Empresa order by nombre asc";
        }
        else
        {
            sql = @"select DISTINCT empresa.id,empresa.nombre,empresa.rif,empresa.telefono,empresa.direccion,empresa.descripcion from Empresa 
                    inner join ubicacion on empresa.id=ubicacion.empresa_id order by nombre asc";
        }
        

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Empresa empresa = new Empresa();
                    empresa.id = reader.GetInt32(0);
                    empresa.nombre = reader.GetString(1);
                    empresa.rif = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    empresa.telefono = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    empresa.direccion = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    empresa.descripcion = reader[5] == DBNull.Value ? String.Empty : reader.GetValue(5).ToString();
                    empresas.Add(empresa);
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

        return empresas;

    }




    public Boolean agregarActualzarEmpresa(int id, String nombre, String rif, String telefono, String direccion, String descripcion)
    {

        Boolean retu = true;
        String sql;

        if (id == 0)
        {
            sql = "insert into empresa(nombre,rif,telefono,direccion,descripcion) values(@nombre,@rif,@telefono,@direccion,@descripcion)";
        }
        else
        {
            sql = "update empresa set nombre=@nombre,rif=@rif,telefono=@telefono,direccion=@direccion,descripcion=@descripcion where id=@id";
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
                comando.Parameters.AddWithValue("@rif", rif.ToUpper());
                comando.Parameters.AddWithValue("@telefono", telefono);
                comando.Parameters.AddWithValue("@direccion", direccion.ToUpper());
                comando.Parameters.AddWithValue("@descripcion", descripcion.ToUpper());

                
                comando.ExecuteNonQuery();
            }
        }
        catch
        {
            retu = false;
        }

        return retu;

    }

    public int unicoNombre(String nombre, int id)
    {

        int retu = 1;

        MySqlDataReader reader = null;

        String sql = @"select id from empresa where id!=@id and nombre=@nombre limit 1";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@nombre", nombre.ToUpper());
                comando.Parameters.AddWithValue("@id", id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    retu = 0;
                    break;
                }
            }
        }
        catch
        {
            retu = -1;
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return retu;

    }

    public int unicoRif(String rif, int id)
    {

        int retu = 1;

        MySqlDataReader reader = null;

        String sql = @"select id from empresa where id!=@id and rif=@rif limit 1";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@rif", rif.ToUpper());
                comando.Parameters.AddWithValue("@id", id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    retu = 0;
                    break;
                }
            }
        }
        catch
        {
            retu = -1;
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return retu;

    }


    public Boolean obtenerEmpresaId(int id)
    {
        Boolean bo = false;
        MySqlDataReader reader = null;

        String sql = @"select id,nombre,rif,telefono,direccion,descripcion from empresa where id=@id";

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
                    this.nombre = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                    this.rif = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    this.telefono = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    this.direccion = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    this.descripcion = reader[5] == DBNull.Value ? String.Empty : reader.GetValue(5).ToString();
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
