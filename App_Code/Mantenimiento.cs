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
/// Summary description for Mantenimiento
/// </summary>
public class Mantenimiento
{
    public int id { get; set; }
    public String fechap { get; set; }
    public int articulo_id { get; set; }
    public String fechaSolicitud { get; set; }
    public int responsable_id { get; set; }
    public int usuario_id { get; set; }
    public String fechaRecepcion { get; set; }
    public String descripcion { get; set; }
    public String estado { get; set; }
    public String prioridad { get; set; }

    public String fecha_envio { get; set; }

    public int numeroDiagnostico { get; set; }


    public List<Diagnostico> diagnostico = new List<Diagnostico>();
    public List<ReparacionExterna> reparacionExterna = new List<ReparacionExterna>();

    public Responsable responsable = new Responsable();
    public Responsable usuario = new Responsable();
    public Articulo articulo = new Articulo();


    public Mantenimiento()
    {
    }


    public bool indicarRecepcion(String fecha_Recepcion, int id)
    {

        Boolean retu = true;
        String sql;

        sql = "update mantenimiento set fecha_Recepcion=@fecha_Recepcion,estado='PENDIENTE' where id=@id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@fecha_Recepcion", fecha_Recepcion);
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


    public bool agregarMantenimiento(int articulo_id, String fecha_solicitud, int responsable_id, int usuario_id, String fecha_recepcion, String descripcion, String estado, String prioridad)
    {

        Boolean retu = true;
        String sql;

        sql = @"insert into mantenimiento(articulo_id,fecha_solicitud,responsable_id,usuario_id,fecha_recepcion,descripcion,estado,prioridad)
                    values(@articulo_id,@fecha_solicitud,@responsable_id,@usuario_id,@fecha_recepcion,@descripcion,@estado,@prioridad)";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);

                comando.Parameters.AddWithValue("@articulo_id", articulo_id);
                comando.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                comando.Parameters.AddWithValue("@responsable_id", responsable_id);
                comando.Parameters.AddWithValue("@usuario_id", usuario_id);
                comando.Parameters.AddWithValue("@fecha_recepcion", fecha_recepcion == string.Empty ? null : fecha_recepcion);
                comando.Parameters.AddWithValue("@descripcion", descripcion.ToUpper());
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@prioridad", prioridad);

                comando.ExecuteNonQuery();
            }
        }
        catch
        {
            retu = false;
        }

        return retu;


    }



    public List<Mantenimiento> obtenerListadoMantenimientos(String filtro, String complemento)
    {

        List<Mantenimiento> mantenimientos = new List<Mantenimiento>();
        MySqlDataReader reader = null;
        String sql;

        sql = @" select  man.id,date_format(man.fechap,'%d/%m/%Y'),man.articulo_id,date_format(man.fecha_solicitud,'%d/%m/%Y'),man.responsable_id, man.usuario_id,
                   date_format(man.fecha_recepcion,'%d/%m/%Y'),man.descripcion,man.estado,resr.nombre,resu.nombre,art.serial,cat.nombre,mar.nombre,mo.nombre,art.serialInterno,man.prioridad,date_format(man.fecha_envio,'%d/%m/%Y'),ifnull(dia.numero,0),
                   his.nom_empresa,his.nom_ubicacion,his.responsablea
                   from mantenimiento as man 
                   left join articulo as art on man.articulo_id=art.id
                   left join categoria as  cat on art.categoria_id=cat.id
                   left join modelo as mo on art.modelo_id=mo.id
                   left join marca as mar on art.marca_id=mar.id
                   left join  responsable as resr on man.responsable_id=resr.id
                   left join  responsable as resu on man.usuario_id=resu.id
                   left join (select mantenimiento_id, count(*) as numero from diagnostico GROUP by mantenimiento_id   ) as dia on man.id=dia.mantenimiento_id	

	               left join (select  empresa.nombre as nom_empresa,ubicacion.nombre as nom_ubicacion,
                                 historial.articulo_id  as articuloa, responsable.nombre as responsablea,
				                 estado.id as estadoa,ubicacion.id as ubicaciona,ubicacion.empresa_id as ubicacionae from historial
				                 inner join (select MAX(historial.id) as idMax,articulo_id from historial group by articulo_id) as tablaMax on tablaMax.articulo_id=historial.articulo_id
								 left join responsable on historial.responsable_id=responsable.id
				                 left join estado on historial.estado_id=estado.id
				                 left join ubicacion on historial.ubicacion_id=ubicacion.id
                                 left join empresa on ubicacion.empresa_id=empresa.id
								 where historial.id=tablaMax.idMax			                           
                      			 
                                 ) as his on man.articulo_id =his.articuloa             

                   @filtro @complemento ";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();

                sql = sql.Replace("@filtro", filtro);
                sql = sql.Replace("@complemento", complemento);


                MySqlCommand comando = new MySqlCommand(sql, conex);

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Mantenimiento mantenimiento = new Mantenimiento();

                    mantenimiento.id = reader.GetInt32(0);
                    mantenimiento.fechap = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                    mantenimiento.articulo_id = reader[2] == DBNull.Value ? 0 : Int32.Parse(reader.GetValue(2).ToString());
                    mantenimiento.fechaSolicitud = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    mantenimiento.responsable_id = reader[4] == DBNull.Value ? 0 : Int32.Parse(reader.GetValue(4).ToString());
                    mantenimiento.usuario_id = reader[5] == DBNull.Value ? 0 : Int32.Parse(reader.GetValue(5).ToString());
                    mantenimiento.fechaRecepcion = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                    mantenimiento.descripcion = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    mantenimiento.estado = reader[8] == DBNull.Value ? String.Empty : reader.GetValue(8).ToString();

                    mantenimiento.responsable.id = mantenimiento.responsable_id;
                    mantenimiento.responsable.nombre = reader[9] == DBNull.Value ? String.Empty : reader.GetValue(9).ToString();

                    mantenimiento.usuario.id = mantenimiento.usuario_id;
                    mantenimiento.usuario.nombre = reader[10] == DBNull.Value ? String.Empty : reader.GetValue(10).ToString();

                    mantenimiento.articulo.serial = reader[11] == DBNull.Value ? String.Empty : reader.GetValue(11).ToString();
                    mantenimiento.articulo.categoria = reader[12] == DBNull.Value ? String.Empty : reader.GetValue(12).ToString();
                    mantenimiento.articulo.marca = reader[13] == DBNull.Value ? String.Empty : reader.GetValue(13).ToString();
                    mantenimiento.articulo.modelo = reader[14] == DBNull.Value ? String.Empty : reader.GetValue(14).ToString();
                    mantenimiento.articulo.control = reader[15] == DBNull.Value ? String.Empty : reader.GetValue(15).ToString();

                    mantenimiento.prioridad = reader[16] == DBNull.Value ? String.Empty : reader.GetValue(16).ToString();
                    mantenimiento.fecha_envio = reader[17] == DBNull.Value ? String.Empty : reader.GetValue(17).ToString();
                    mantenimiento.numeroDiagnostico = reader[18] == DBNull.Value ? 0 : reader.GetInt32(18);

                    mantenimiento.articulo.empresa = reader[19] == DBNull.Value ? String.Empty : reader.GetValue(19).ToString();
                    mantenimiento.articulo.ubicacion = reader[20] == DBNull.Value ? String.Empty : reader.GetValue(20).ToString();
                    mantenimiento.articulo.responsable_nombre = reader[21] == DBNull.Value ? String.Empty : reader.GetValue(21).ToString();
                    

                    mantenimientos.Add(mantenimiento);
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

        return mantenimientos;

    }


    public bool validarMantenimientoAsociadoSinCerrar(int articulo_id)
    {

        List<Mantenimiento> mantenimientos = new List<Mantenimiento>();
        MySqlDataReader reader = null;
        bool bo = true;
        String sql;

        sql = @" select id from mantenimiento where estado <> 'Cerrado' and articulo_id =@articulo_id  limit 1";

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



    public bool obtenerMantenimiento(int id)
    {
        MySqlDataReader reader = null;
        String sql;
        bool bo = false;

        sql = @" select  man.id,date_format(man.fechap,'%d/%m/%Y'),man.articulo_id,date_format(man.fecha_solicitud,'%d/%m/%Y'),man.responsable_id, man.usuario_id,
                   date_format(man.fecha_recepcion,'%d/%m/%Y'),man.descripcion,man.estado,resr.nombre,resu.nombre,art.serial,cat.nombre,mar.nombre,mo.nombre,art.serialInterno,man.prioridad,date_format(man.fecha_envio,'%d/%m/%Y')
                   from mantenimiento as man 
                   left join articulo as art on man.articulo_id=art.id
                   left join categoria as  cat on art.categoria_id=cat.id
                   left join modelo as mo on art.modelo_id=mo.id
                   left join marca as mar on art.marca_id=mar.id
                   left join  responsable as resr on man.responsable_id=resr.id
                   left join  responsable as resu on man.usuario_id=resu.id
                   where  man.id=@id";

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
                    this.fechap = reader[1] == DBNull.Value ? String.Empty : reader.GetValue(1).ToString();
                    this.articulo_id = reader[2] == DBNull.Value ? 0 : Int32.Parse(reader.GetValue(2).ToString());
                    this.fechaSolicitud = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    this.responsable_id = reader[4] == DBNull.Value ? 0 : Int32.Parse(reader.GetValue(4).ToString());
                    this.usuario_id = reader[5] == DBNull.Value ? 0 : Int32.Parse(reader.GetValue(5).ToString());
                    this.fechaRecepcion = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                    this.descripcion = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    this.estado = reader[8] == DBNull.Value ? String.Empty : reader.GetValue(8).ToString();

                    this.responsable.id = this.responsable_id;
                    this.responsable.nombre = reader[9] == DBNull.Value ? String.Empty : reader.GetValue(9).ToString();

                    this.usuario.id = this.usuario_id;
                    this.usuario.nombre = reader[10] == DBNull.Value ? String.Empty : reader.GetValue(10).ToString();


                    this.articulo.serial = reader[11] == DBNull.Value ? String.Empty : reader.GetValue(11).ToString();
                    this.articulo.categoria = reader[12] == DBNull.Value ? String.Empty : reader.GetValue(12).ToString();
                    this.articulo.marca = reader[13] == DBNull.Value ? String.Empty : reader.GetValue(13).ToString();
                    this.articulo.modelo = reader[14] == DBNull.Value ? String.Empty : reader.GetValue(14).ToString();
                    this.articulo.control = reader[15] == DBNull.Value ? String.Empty : reader.GetValue(15).ToString();

                    this.prioridad = reader[16] == DBNull.Value ? String.Empty : reader.GetValue(16).ToString();
                    this.fecha_envio = reader[17] == DBNull.Value ? String.Empty : reader.GetValue(17).ToString();
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



    public bool indicarEnvio(String fecha_envio, int id)
    {

        Boolean retu = true;
        String sql;

        sql = "update mantenimiento set fecha_envio=@fecha_envio,estado='RESUELTO' where id=@id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@fecha_envio", fecha_envio);
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


    public bool cambiarEstado(int id,String estado)
    {

        Boolean retu = true;
        String sql;

        sql = "update mantenimiento set estado=@estado where id=@id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@estado", estado);
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

}
