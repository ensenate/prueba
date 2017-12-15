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

/// <summary>
/// Summary description for Conteo
/// </summary>
public class Conteo
{

    public int id;
    public String fecha;
    public String fechap;
    public int realizado_id;
    public int responsable_id;
    public float cantidad;
    public float cantidada;
    public int desgaste_id;
    public String obs;

	public Conteo()
	{
            
	}


    public Boolean agregarConteo(String fecha, int realizado_id, int responsable_id, float cantidad, float cantidada, int desgaste_id, String obs)
    {

        Boolean retu = true;
        String sql = "insert into conteo(fecha,realizado_id,responsable_id,cantidad,cantidada,desgaste_id,obs) values(@fecha,@realizado_id,@responsable_id,@cantidad,@cantidada,@desgaste_id,@obs)";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);

                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@realizado_id", realizado_id);
                comando.Parameters.AddWithValue("@responsable_id", responsable_id);
                comando.Parameters.AddWithValue("@obs", obs.ToUpper());
                comando.Parameters.AddWithValue("@cantidad", cantidad);
                comando.Parameters.AddWithValue("@cantidada", cantidada);
                comando.Parameters.AddWithValue("@desgaste_id", desgaste_id);
                

                comando.ExecuteNonQuery();
            }
        }
        catch
        {
            retu = false;
        }

        return retu;

    }


}
