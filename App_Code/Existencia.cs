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
/// Summary description for Existencia
/// </summary>
public class Existencia
{
    public int id { get; set; }
    public float cantidadAnterior { get; set; }
    public float cantidadActual { get; set; }
    public float cantidad { get; set; }
    public String fecha { get; set; }
    public String obs { get; set; }

    public int desgaste_id { get; set; }

    public String desgaste { get; set; }
    public String usuario { get; set; }
    public String responsable { get; set; }
    public String unidad { get; set; }
    public String ubicacion { get; set; }
    public String empresa { get; set; }
    public String tipo { get; set; }

	public Existencia()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public List<Existencia> obtenerListaExistencias(int desgaste_id)
    {
        List<Existencia> existencias = new List<Existencia>();

        MySqlDataReader reader = null;

        String sql = @"select existencia.id,cantidadAnterior,cantidadActual,existencia.obs,desgaste.descripcion,usuario.nombre,unidad,fecha,respo.nombre,IFNULL(ubicacion.nombre,'S/E'),IFNULL(empresa.nombre,'S/E'),existencia.tipo,existencia.cantidad from existencia 
                       inner join desgaste on existencia.desgaste_id=desgaste.id  
                       inner join responsable as respo on existencia.responsable_id=respo.id
                       inner join responsable as usuario on existencia.usuario_id=usuario.id
                       left join ubicacion on existencia.ubicacion_id=ubicacion.id
                       left join empresa on ubicacion.empresa_id=empresa.id
                       where existencia.desgaste_id=@desgaste_id order by  existencia.fecha desc";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@desgaste_id", desgaste_id);
                reader = comando.ExecuteReader();
               
                while (reader.Read())
                {
                    Existencia existencia = new Existencia();

                    existencia.id = reader.GetInt32(0);
                    existencia.cantidadAnterior = reader.GetFloat(1);
                    existencia.cantidadActual = reader.GetFloat(2);
                    existencia.obs= reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    existencia.desgaste = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    existencia.usuario = reader[5] == DBNull.Value ? String.Empty : reader.GetValue(5).ToString();
                    existencia.unidad = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                    existencia.fecha = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    existencia.responsable= reader[8] == DBNull.Value ? String.Empty : reader.GetValue(8).ToString();
                    existencia.ubicacion = reader[9] == DBNull.Value ? String.Empty : reader.GetValue(9).ToString();
                    existencia.empresa = reader[10] == DBNull.Value ? String.Empty : reader.GetValue(10).ToString();
                    existencia.tipo = reader[11] == DBNull.Value ? String.Empty : reader.GetValue(11).ToString();
                    existencia.cantidad = reader.GetFloat(12);

                    existencias.Add(existencia);
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

        return existencias;

    }

    public int agregarExistencia(float cantidad,String obs,int desgaste_id,String usuario_id,String unidad,String responsable_id,String ubicacion_id,String tipo) {

        int retu = 1;

        String sql = "insert into existencia(cantidadAnterior,cantidadActual,obs,desgaste_id,usuario_id,unidad,responsable_id,ubicacion_id,tipo,cantidad) values(@cantidadAnterior,@cantidadActual,@obs,@desgaste_id,@usuario_id,@unidad,@responsable_id,@ubicacion_id,@tipo,@cantidad)";

        Desgaste desgaste = new Desgaste();

        cantidadAnterior = desgaste.obtenerCantidad(desgaste_id);

        if (tipo == "S")
        {
            cantidad = cantidad * (-1);
        }

        cantidadActual = cantidadAnterior + cantidad;
        
        if (cantidadActual<0)
        {
            return 0;
        }
        
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);

                comando.Parameters.AddWithValue("@cantidadAnterior", cantidadAnterior);
                comando.Parameters.AddWithValue("@cantidadActual", cantidadActual);
                comando.Parameters.AddWithValue("@cantidad", cantidad);
                comando.Parameters.AddWithValue("@obs", obs.ToUpper());
                comando.Parameters.AddWithValue("@desgaste_id", desgaste_id);
                comando.Parameters.AddWithValue("@usuario_id", usuario_id);
                comando.Parameters.AddWithValue("@unidad", unidad.ToUpper());
                comando.Parameters.AddWithValue("@responsable_id", responsable_id);
                comando.Parameters.AddWithValue("@ubicacion_id", ubicacion_id);
                comando.Parameters.AddWithValue("@tipo", tipo);
                comando.ExecuteNonQuery();

                if (!desgaste.actualizarCantidad(desgaste_id, cantidadActual))
                {
                    retu = -1;
                }

            }
        }
        catch
        {
            retu = -1;
        }

        return retu;

    
    }


}

