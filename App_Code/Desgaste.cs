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
/// Summary description for Desgaste
/// </summary>
public class Desgaste
{

    public int id { get; set; }
    public String control { get; set; }
    public String descrip { get; set; }

    public float cantidad { get; set; }

    public int marca_id { get; set; }
    public int modelo_id { get; set; }
    public int categoria_id { get; set; }
    public int unidad_id { get; set; }
    public int empresa_id { get; set; }

    public String marca { get; set; }
    public String modelo { get; set; }
    public String categoria { get; set; }
    public String unidad { get; set; }
    public String empresa { get; set; }
    public String descontinuado { get; set; }
    public String dmotivo { get; set; }
    

    public Desgaste()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public List<Desgaste> obtenerListaDesgastes(string filtro)
    {
        List<Desgaste> desgastes = new List<Desgaste>();

        MySqlDataReader reader = null;

        String sql = @" select desgaste.id,desgaste.serialinterno,desgaste.descripcion,
                        desgaste.cantidad,categoria.nombre,modelo.nombre,marca.nombre,unidad.nombre,empresa.nombre,desgaste.descontinuado,desgaste.dmotivo from desgaste 
                        left join categoria on desgaste.categoria_id=categoria.id
                        left join modelo on desgaste.modelo_id=modelo.id
                        left join marca on desgaste.marca_id=marca.id
                        left join unidad on desgaste.unidad_id=unidad.id
                        left join empresa on desgaste.empresa_id=empresa.id      
                        @filtro                 
                        order by desgaste.descripcion";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                sql = sql.Replace("@filtro", filtro);
                MySqlCommand comando = new MySqlCommand(sql, conex);

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Desgaste desgaste = new Desgaste();

                    desgaste.id = reader.GetInt32(0);
                    desgaste.control = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                    desgaste.descrip = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    desgaste.cantidad = reader.GetFloat(3);
                    desgaste.categoria = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    desgaste.modelo = reader[5] == DBNull.Value ? String.Empty : reader.GetValue(5).ToString();
                    desgaste.marca = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                    desgaste.unidad = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    desgaste.empresa = reader[8] == DBNull.Value ? String.Empty : reader.GetValue(8).ToString();
                    desgaste.descontinuado = reader[9] == DBNull.Value ? String.Empty : reader.GetValue(9).ToString();
                    desgaste.dmotivo = reader[10] == DBNull.Value ? String.Empty : reader.GetValue(10).ToString();

                    desgastes.Add(desgaste);
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

        return desgastes;

    }


    public Boolean agregarActualizarDesgastes(int id, String descripcion,  int modelo_id, int marca_id, int categoria_id, int empresa_id, int unidad_id)
    {
        Boolean retu = true;
        String sql;

        if (id == 0)
        {
            sql = "insert into desgaste(cantidad,serialinterno,descripcion,categoria_id,marca_id,modelo_id,empresa_id,unidad_id) values(0,@serialInterno,@descripcion,@categoria_id,@marca_id,@modelo_id,@empresa_id,@unidad_id)";
        }
        else
        {

            sql = "update desgaste set descripcion=@descripcion, categoria_id=@categoria_id,marca_id=@marca_id,modelo_id=@modelo_id,empresa_id=@empresa_id,unidad_id=@unidad_id where id=@id";

        }
            try
            {
                using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
                {
                    conex.Open();
                    MySqlCommand comando = new MySqlCommand(sql, conex);

                    String prefijo = "";

                    if (id != 0)
                    {
                        comando.Parameters.AddWithValue("@id", id);
                    }
                    else
                    {
                        Articulo articulo = new Articulo();
                        prefijo = articulo.generarSerial(categoria_id.ToString());
                        comando.Parameters.AddWithValue("@serialInterno", prefijo.ToUpper());
                    }

                    comando.Parameters.AddWithValue("@descripcion", descripcion.ToUpper());
                    comando.Parameters.AddWithValue("@unidad_id", unidad_id);
                    comando.Parameters.AddWithValue("@empresa_id", empresa_id);
                    comando.Parameters.AddWithValue("@categoria_id", categoria_id);
                    comando.Parameters.AddWithValue("@marca_id", marca_id);
                    comando.Parameters.AddWithValue("@modelo_id", modelo_id);
                    comando.ExecuteNonQuery();
                }
            }
            catch
            {
                retu = false;
            }

            return retu;


    }

    public Boolean obtenerDesgasteId(int id) {

        MySqlDataReader reader = null;
        Boolean retu = false;
        String sql = @" select desgaste.id,desgaste.serialinterno,desgaste.descripcion,
                        desgaste.cantidad,categoria.nombre,modelo.nombre,marca.nombre,unidad.nombre,empresa.nombre,desgaste.descontinuado,desgaste.dmotivo from desgaste 
                        left join categoria on desgaste.categoria_id=categoria.id
                        left join modelo on desgaste.modelo_id=modelo.id
                        left join marca on desgaste.marca_id=marca.id
                        left join unidad on desgaste.unidad_id=unidad.id
                        left join empresa on desgaste.empresa_id=empresa.id
                        where desgaste.id=@id limit 1";

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
                    this.id = reader.GetInt32(0);
                    this.control = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                    this.descrip = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    this.cantidad = reader.GetFloat(3);
                    this.categoria = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    this.modelo = reader[5] == DBNull.Value ? String.Empty : reader.GetValue(5).ToString();
                    this.marca = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                    this.unidad = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    this.empresa = reader[8] == DBNull.Value ? String.Empty : reader.GetValue(8).ToString();
                    this.descontinuado = reader[9] == DBNull.Value ? String.Empty : reader.GetValue(9).ToString();
                    this.dmotivo = reader[10] == DBNull.Value ? String.Empty : reader.GetValue(10).ToString();

                    retu = true;
                }
            }
        }
        catch
        {
            retu = false;
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        return retu;


    }


    public int validarDescripcion(String descripcion,int id)
    {

        MySqlDataReader reader = null;
        int retu=1;
        String sql = " select id from desgaste where id<> @id and descripcion=@descripcion limit 1";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@descripcion", descripcion.ToUpper());
                comando.Parameters.AddWithValue ("@id", id);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    retu = 0;
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


    public float obtenerCantidad(int id)
    {
        float cantidad = 0;

        String sql = "select cantidad from desgaste where id=@id  limit 1";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@id", id);
                Object obj = comando.ExecuteScalar();
                if (obj != null)
                {
                    cantidad = Convert.ToInt64(obj);
                }
            }
        }
        catch
        {
            cantidad = -1;
        }


        return cantidad;

    }



    public Boolean actualizarCantidad(int id, float cantidad)
    {

        Boolean retu = false;
        String sql = "update desgaste set cantidad=@cantidad where id=@id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@cantidad", cantidad);

                comando.ExecuteNonQuery();
                retu = true;
            }
        }
        catch
        {
            retu = false;
        }

        return retu;

    }


    public Boolean descontinuar(int id,String dmotivo)
    {

        Boolean retu = false;
        String sql = "update desgaste set descontinuado='S',dmotivo=@dmotivo where id=@id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@dmotivo", dmotivo);


                comando.ExecuteNonQuery();
                retu = true;
            }
        }
        catch
        {
            retu = false;
        }

        return retu;

    }


}