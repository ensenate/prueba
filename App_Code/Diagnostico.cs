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
/// Summary description for Diagnostico
/// </summary>
public class Diagnostico
{

    public int id { get; set; }
    public String fechap { get; set; } 
    public int mantenimiento_id { get; set; } 
    public String fechaRevision { get; set; }
    public String responsable { get; set; }
    public String descripcion { get; set; }
    public String tipo { get; set; } 




	public Diagnostico()
	{

	}

    public bool agregarDiagnostico(int mantenimiento_id, String fecha_revision, String responsable, String descripcion)
    {

        Boolean retu = true;
        String sql;

        sql = @"insert into diagnostico(mantenimiento_id,fecha_revision,responsable,descripcion)
                    values(@mantenimiento_id,@fecha_revision,@responsable,@descripcion)";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);

                comando.Parameters.AddWithValue("@mantenimiento_id", mantenimiento_id);
                comando.Parameters.AddWithValue("@fecha_revision", fecha_revision);
                comando.Parameters.AddWithValue("@responsable", responsable.ToUpper());
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



    public List<Diagnostico> obtenerDiagnosticos(int mantenimiento_id)
    {
        MySqlDataReader reader = null;
        String sql;
        List<Diagnostico> diagnosticos =new List<Diagnostico>();


        sql = @" select id,date_format(fecha_revision,'%d/%m/%Y'),responsable,descripcion,tipo from diagnostico where mantenimiento_id=@mantenimiento_id order by fecha_revision desc,id desc";

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
                    Diagnostico diagnostico=new Diagnostico();
                  
                    diagnostico.id = reader.GetInt32(0);
                    diagnostico.fechaRevision = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                    diagnostico.responsable = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    diagnostico.descripcion = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    diagnostico.tipo = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();

                    diagnosticos.Add(diagnostico);
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

        return diagnosticos;
    }

}
