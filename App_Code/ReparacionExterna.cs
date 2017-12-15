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
/// Summary description for ReparacionExterna
/// </summary>
public class ReparacionExterna
{
    public int id;
    public String fechap;
    public Responsable responsable;
    public String fechaEnvio;
    public String descripcion;
    public String fechaRecepcion;
    public float costo;
    public String descripcionf;

	public ReparacionExterna()
	{
        responsable = new Responsable();
        fechaEnvio = string.Empty;
        descripcion = string.Empty;
        fechaRecepcion = string.Empty;
        costo = 0;
	}

    public List<ReparacionExterna> obtenerReparacionExternas(int mantenimiento_id)
    {
        MySqlDataReader reader = null;
        String sql;
        List<ReparacionExterna> reparacionExternas = new List<ReparacionExterna>();

        sql = @" select rex.id,date_format(rex.fechap,'%d/%m/%Y'),res.nombre,date_format(rex.fecha_envio,'%d/%m/%Y'),rex.descripcion,date_format(rex.fecha_recepcion,'%d/%m/%Y'),rex.costo,rex.descripcionf
                      from reparacionex  as rex
                      left join responsable as res on rex.responsable_id=res.id  
                      where rex.mantenimiento_id=@mantenimiento_id 
                       order by fecha_envio desc ,id desc  ";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();

                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@mantenimiento_id", mantenimiento_id);

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    ReparacionExterna reparacionExterna = new ReparacionExterna();

                    reparacionExterna.id = reader.GetInt32(0);
                    reparacionExterna.fechap = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                    reparacionExterna.responsable.nombre = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    reparacionExterna.fechaEnvio = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    reparacionExterna.descripcion = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    reparacionExterna.fechaRecepcion = reader[5] == DBNull.Value ? String.Empty : reader.GetValue(5).ToString();
                    reparacionExterna.costo = reader[6] == DBNull.Value ? 0 : float.Parse(reader.GetValue(6).ToString());
                    reparacionExterna.descripcionf = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    reparacionExternas.Add(reparacionExterna);
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

        return reparacionExternas;
    }


    public bool agregarReparacionExterna(int mantenimiento_id, int responsable_id, String fecha_envio,  String descripcion)
    {

        Boolean retu = true;
        String sql;

        sql = @"insert into reparacionex(mantenimiento_id,responsable_id,fecha_envio,descripcion)
                    values(@mantenimiento_id,@responsable_id,@fecha_envio,@descripcion)";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);

                comando.Parameters.AddWithValue("@mantenimiento_id", mantenimiento_id);
                comando.Parameters.AddWithValue("@responsable_id", responsable_id);
                comando.Parameters.AddWithValue("@fecha_envio", fecha_envio);
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

    public bool indicarCulminacion(String fecha_recepcion, String descripcionf, float costo, int id)
    {

        Boolean retu = true;
        String sql;

        sql = "update reparacionex set fecha_recepcion=@fecha_recepcion,descripcionf=@descripcionf, costo=@costo where id=@id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@fecha_recepcion", fecha_recepcion);
                comando.Parameters.AddWithValue("@descripcionf", descripcionf.ToUpper());
                comando.Parameters.AddWithValue("@costo", costo);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
        }
        catch
        {
            retu = false;
        }

        return retu;

    }



    public int obtenerReferenciaSoporteActual(int mantenimiento_id)
    {

        int max = 0;

        MySqlDataReader reader = null;
        String sql;
        List<ReparacionExterna> reparacionExternas = new List<ReparacionExterna>();

        sql = @" select ifnull(max(id),0)
                      from reparacionex  as rex 
                      where rex.mantenimiento_id=@mantenimiento_id ";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();

                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@mantenimiento_id", mantenimiento_id);

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    max = reader.GetInt32(0);
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
        return max;

    }


}
