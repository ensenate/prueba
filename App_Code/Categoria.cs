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
/// Summary description for Categoria
/// </summary>
public class Categoria
{

    public int id { get; set; }
    public String nombre { get; set; }
    public String prefijo { get; set; }
    public String ejemplo1 { get; set; }
    public String ejemplo2 { get; set; }
    public String pc { get; set; }
    public String imagen { get; set; }

    public Boolean asociar { get; set; }


	public Categoria()
	{

	}


   public static String  obtenerPrefijo(String id){

       String prefijo="";

       String sql = "select prefijo from categoria where id=@id";

       try
       {
           using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
           {
               conex.Open();
               MySqlCommand comando = new MySqlCommand(sql, conex);
               comando.Parameters.AddWithValue("@id",id);
               prefijo= comando.ExecuteScalar().ToString();
           }
       }
       catch
       {
           prefijo = "";
       }


       return prefijo;
   }



   public static List<Categoria> obtenerCategorias()
   {
       List<Categoria> categorias = new List<Categoria>();
       MySqlDataReader reader = null;

       String sql = "select id,nombre,prefijo,asociar,ejemplo1,ejemplo2,pc from categoria order by nombre asc";

       try
       {
           using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
           {
               conex.Open();
               MySqlCommand comando = new MySqlCommand(sql, conex);
               reader = comando.ExecuteReader();

               while (reader.Read())
               {
                   Categoria categoria = new Categoria();
                   categoria.id = reader.GetInt32(0);
                   categoria.nombre = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                   categoria.prefijo = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                   categoria.asociar = reader.GetBoolean(3);
                   categoria.ejemplo1 = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                   categoria.ejemplo2 = reader[5] == DBNull.Value ? String.Empty : reader.GetValue(5).ToString();
                   categoria.pc = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                   categorias.Add(categoria);
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

       return categorias;
   }

   public Boolean agregarActualizarCategoria(int id, String nombre, String prefijo, Boolean asociar, string ejemplo1, string ejemplo2, string pc, String imagen)
   {

       Boolean retu = true;
       String sql;

       if (id == 0)
       {
           sql = "insert into categoria(nombre,prefijo,asociar,ejemplo1,ejemplo2,pc,imagen) values(@nombre,@prefijo,@asociar,@ejemplo1,@ejemplo2,@pc,@imagen)";
       }
       else
       {
           sql = @"update categoria set nombre=@nombre,prefijo=@prefijo,asociar=@asociar,ejemplo1=@ejemplo1,ejemplo2=@ejemplo2,pc=@pc,imagen=@imagen where id=@id";
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
               comando.Parameters.AddWithValue("@prefijo", prefijo.ToUpper());
               comando.Parameters.AddWithValue("@asociar", asociar);
               comando.Parameters.AddWithValue("@ejemplo1", ejemplo1.ToUpper());
               comando.Parameters.AddWithValue("@ejemplo2", ejemplo2.ToUpper());
               comando.Parameters.AddWithValue("@pc", pc);
               comando.Parameters.AddWithValue("@imagen", imagen);
               
               
               comando.ExecuteNonQuery();
           }
       }
       catch
       {
           retu = false;
       }

       return retu;
   
   }



   public Boolean obtenerCagoriaId(int id){

       Boolean bo = false;
       MySqlDataReader reader = null;

       String sql = @"select id,nombre,prefijo,asociar,ejemplo1,ejemplo2,pc,imagen from categoria where id=@id";

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
                   this.prefijo = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                   this.asociar = reader.GetBoolean(3);
                   this.ejemplo1 = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                   this.ejemplo2 = reader[5] == DBNull.Value ? String.Empty : reader.GetValue(5).ToString();
                   this.pc = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                   this.imagen = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                   
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


   public int UnicoNombre(String prefijo, int id)
   {

       int retu = 1;

       MySqlDataReader reader = null;

       String sql = @"select id from categoria where id!=@id and nombre=@nombre limit 1";

       try
       {
           using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
           {
               conex.Open();
               MySqlCommand comando = new MySqlCommand(sql, conex);
               comando.Parameters.AddWithValue("@nombre", prefijo.ToUpper());
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

   public int UnicoPrefijo(String prefijo,int id)
   {

       int retu = 1;

       MySqlDataReader reader = null;

       String sql = @"select id from categoria where id!=@id and prefijo=@prefijo limit 1";

       try
       {
           using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
           {
               conex.Open();
               MySqlCommand comando = new MySqlCommand(sql, conex);
               comando.Parameters.AddWithValue("@prefijo", prefijo.ToUpper());
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



}
