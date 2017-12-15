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
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

/// <summary>
/// Summary description for Cuenta
/// </summary>
public class Cuenta
{

    public int id { get; set; }
    public String usuario { get; set; }
    public String clave { get; set; }
    public int rol_id { get; set; }
    public int responsable_id { get; set; }

    public String rol { get; set; }
    public String responsable { get; set; }

	public Cuenta()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Boolean verificarLogin(String usuario,String clave)
    {
        Boolean bo = false;
        MySqlDataReader reader = null;
        String claveE = HashPassword(clave);

        String sql = @"select responsable.id,responsable.nombre,rol.nombre as rol from cuenta
                        inner join responsable on cuenta.responsable_id=responsable.id
                        inner join rol on rol_id=rol.id
                         where cuenta.usuario=@usuario and cuenta.clave=@clave";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@usuario", usuario.ToUpper());
                comando.Parameters.AddWithValue("@clave", claveE);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    this.responsable_id = reader.GetInt32(0);
                    this.usuario = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString(); 
                    this.rol = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
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
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return bo;
    }


    public String HashPassword(String pass)
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
        byte[] hash = md5.ComputeHash(inputBytes);

        return BitConverter.ToString(hash).Replace("-", "");
    }

    public static List<Cuenta> obtenerListadoCuentas(String id)
    {

        List<Cuenta> cuentas = new List<Cuenta>();
        MySqlDataReader reader = null;
        String sql = @"select cuenta.id,cuenta.usuario,responsable.nombre,rol.nombre from cuenta
                        inner join responsable on cuenta.responsable_id =responsable.id
                        inner join rol on cuenta.rol_id=rol.id
                        where responsable_id!=@id";

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
                    Cuenta cuenta = new Cuenta();
                    cuenta.id = reader.GetInt32(0);
                    cuenta.usuario = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                    cuenta.responsable = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    cuenta.rol = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    cuentas.Add(cuenta);
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

        return cuentas;
    }


    public Boolean agregarActualizarCuenta(int id,String usuario,String clave,int rol_id,int responsable_id) {


        Boolean retu = true;
        String sql;
        String claveE="";

        if (clave != "")
        {
            claveE = HashPassword(clave);
        }
        if (id == 0)
        {
            sql = "insert into cuenta(usuario,clave,rol_id,responsable_id) values(@usuario,@clave,@rol_id,@responsable_id)";
        }
        else
        {
            sql = @"update cuenta set @clave rol_id=@rol_id where id=@id";
        }
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();

                if (clave != "")
                {
                    if (id != 0)
                    {
                        sql = sql.Replace("@clave", "clave='" + claveE + "',");
                    }
                    else
                    {
                        sql = sql.Replace("@clave", "'"+claveE+"'");
                    }
                }
                else
                {
                    sql = sql.Replace("@clave", " ");
                }
                MySqlCommand comando = new MySqlCommand(sql, conex);

                comando.Parameters.AddWithValue("rol_id", rol_id);
                if (id != 0)
                {
                    comando.Parameters.AddWithValue("@id", id);
                }
                else
                {
                    comando.Parameters.AddWithValue("responsable_id", responsable_id);
                    comando.Parameters.AddWithValue("usuario", usuario.ToUpper());
                }
                comando.ExecuteNonQuery();
            }
        }
        catch
        {
            retu = false;
        }

        return retu;

    }

    public Boolean obtenerCuentaId(int id)
    {
        Boolean bo = false;
        MySqlDataReader reader = null;

        String sql = @"select cuenta.id,cuenta.usuario,cuenta.clave,cuenta.responsable_id,cuenta.rol_id,responsable.nombre,rol.nombre from cuenta
                        inner join responsable on cuenta.responsable_id =responsable.id
                        inner join rol on cuenta.rol_id=rol.id
                         where cuenta.id=@id";

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
                    this.usuario = reader.GetString(1);
                    this.clave = reader.GetString(2);
                    this.responsable_id = reader.GetInt32(3);
                    this.rol_id = reader.GetInt32(4);

                    this.responsable  = reader[5] == DBNull.Value ? String.Empty : reader.GetValue(5).ToString();
                    this.rol = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();

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
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return bo;
    }

    public int validarCuenta(String usuario,int id)
    {
        int bo = 1;
        MySqlDataReader reader = null;

        String sql = @"select id from cuenta  where usuario=@usuario and id<>@id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@usuario", usuario.ToUpper());
                comando.Parameters.AddWithValue("@id", id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    bo = 0;
                }
            }
        }
        catch
        {
            bo = -1;
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return bo;
    }


    public Boolean verificarCuenta(String responsable_id)
    {

        Boolean bo = true;

        MySqlDataReader reader = null;

        String sql = @"select id from cuenta where responsable_id=@responsable_id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@responsable_id", responsable_id);
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
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return bo;

    }

}
