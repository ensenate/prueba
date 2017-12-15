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
/// Summary description for KeyWorks
/// </summary>
public class KeyWorks
{

    public int tip_id { get; set; }
    public int articulo_id { get; set; }

    public int id { get; set; }
    public String nombre { get; set; }

	public KeyWorks()
	{


	}


    public Boolean agregarActualizarKeyWorks(int articulo_id, ListItemCollection lista)
    {


        Boolean retu = true;
        String sql;


        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                sql = "delete from keywork where articulo_id=@articulo_id ";

                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@articulo_id", articulo_id);
                comando.ExecuteNonQuery();

                comando.Parameters.Clear();

                sql = "insert into keywork(tip_id,articulo_id) values(@tip_id,@articulo_id)";
                comando.CommandText = sql;

                foreach (ListItem item in lista)
                {
                    comando.Parameters.AddWithValue("@tip_id", item.Value);
                    comando.Parameters.AddWithValue("@articulo_id", articulo_id);
                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();
   
                }

                
            }
        }
        catch
        {
            retu = false;
        }

        return retu;

        

    }


    public List<KeyWorks> obtenerKeywork(int articulo_id, Boolean contiene)
    {


        List<KeyWorks> lista = new List<KeyWorks>();

        MySqlDataReader reader = null;
        String sql;

        if (contiene)
        {
            sql = @"select tip.id,tip.nombre from tip inner join keywork on  tip.id=keywork.tip_id
                 where keywork.articulo_id=@articulo_id";
        }
        else
        {
            sql = @"select tip.id,tip.nombre from tip
                  left join keywork on ( tip.id=keywork.tip_id and keywork.articulo_id=@articulo_id)
                  where keywork.articulo_id is null ";
        }


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
                    KeyWorks item = new KeyWorks();
                    item.id = reader.GetInt32(0);
                    item.nombre = reader.GetString(1);
                    lista.Add(item);
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

        return lista;

    }


}
