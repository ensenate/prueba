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
/// Summary description for Soporte
/// </summary>
public class Soporte
{

    public int id { get; set; }
    public String fecha_inicio { get; set; }
    public String fecha_fin { get; set; }
    public String obsi { get; set; }
    public String obsf { get; set; }

    public int responsable_id { get; set; }
    public int historial_id { get; set; }

    public String responsable { get; set; }



	public Soporte()
	{
		//
		// TODO: Add constructor logic here
		//
	}
  /*   public List<Soporte> obtenerListaSoportes(int articulo_id)
    {
        List<Soporte> soportes = new List<Soporte>();

        MySqlDataReader reader = null;

        String sql = @"select soporte.id,soporte.fecha_inicio.soporte.fecha_fin,soporte.obsi,soporte.obsf,responsable.nombre,responsable.responsable_id
                       from soporte inner join responsable on soporte.responsable_id=responsable.id
                       inner join historial on soporte.historial_id=historial.id
                       where  historial.articulo_id.id=@articulo_id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@articulo_id", articulo_id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Soporte soporte = new Soporte();
                    soporte.id = reader.GetInt32(0);
                    soporte.fecha_inicio=reader.GetString(1);
                    soporte.fecha_fin=reader.GetString(2);
                    soporte.obsi=reader.GetString(3);
                    soporte.obsi=reader.GetString(4);
                    soporte.responsable= reader.GetString(5);
                    soporte.responsable_id=reader.GetInt32(6);

                    soportes.Add(soporte);
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

        return soportes;

    }



    public Boolean agregarActualzarSoporte(int id,String fecha_inicio, String obsi,
          int historia_id, int responsable_id)
    {

        Boolean retu = true;
        String sql ;
        if(id==0){
           sql = @"insert into soporte(fecha_inicio,obsi,responsable_id,historial_id)
                       values(@fecha_inicio,@obsi,@responsable_id,@historial_id)";
        }else{
           sql=@"update soporte set fecha_inicio=@fecha_inicio,obsi=@obsi, 
                 responsable_id=@responsable_id, historial_id=@historial_id where id=@id ";
        }
            try
            {
                using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
                {
                    conex.Open();
                    MySqlCommand comando = new MySqlCommand(sql, conex);
                    if(id!=0){
                         comando.Parameters.AddWithValue("@id", id);
                    }
                    comando.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                    comando.Parameters.AddWithValue("@obsi", obsi);
                    comando.Parameters.AddWithValue("@responsable_id", responsable_id);
                    comando.Parameters.AddWithValue("@historial_id", historia_id);
                    comando.ExecuteNonQuery();
                }
            }
            catch
            {
                retu = false;
            }

            return retu;
    }

    */
  

}

