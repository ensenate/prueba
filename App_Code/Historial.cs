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
/// Summary description for Historial
/// </summary>
public class Historial
{
    public int id { get; set; }
    public String fecha_inicio { get; set; }
    public String fecha_fin { get; set; }
    public String obs { get; set; }

    public int estado_id { get; set; }
    public int responsable_id { get; set; }
    public int autorizado_id { get; set; }
    public int realizado_id { get; set; }
    public int articulo_id { get; set; }
    public int ubicacion_id { get; set; }

    public String estado { get; set; }
    public String responsable { get; set; }
    public String autorizado { get; set; }
    public String realizado { get; set; }
    public String articulo { get; set; }
    public String ubicacion { get; set; }
    public String empresa { get; set; }



	public Historial()
	{


	}

    public List<Historial> obtenerListaHistoriasReferencias(int articulo_id)
    {
        List<Historial> historias = new List<Historial>();

        MySqlDataReader reader = null;

        String sql = @"select historial.id,date_format(fecha_inicio,'%d/%m/%Y'),ifnull(date_format(fecha_fin,'%d/%m/%Y'),'N/A'),historial.obs,ubicacion.nombre,
                       respo.nombre,estado.nombre,realizado.nombre,autorizado.nombre,empresa.nombre from historial
                       left join   estado on historial.estado_id=estado.id
                       left join responsable as respo on historial.responsable_id=respo.id
                       left join responsable as realizado on historial.realizado_id=realizado.id
                       left join responsable as autorizado on historial.autorizado_id=autorizado.id
                       left join articulo on  historial.articulo_id =articulo.id
                       left join ubicacion on historial.ubicacion_id= ubicacion.id
                       left join empresa on ubicacion.empresa_id= empresa.id
                       where historial.articulo_id=@articulo_id order by id desc";

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
                    Historial historia = new Historial();
                    historia.id = reader.GetInt32(0);
                    historia.fecha_inicio = reader.GetValue(1).ToString();
                    historia.fecha_fin = reader.GetValue(2).ToString();
                    historia.obs = reader.GetValue(3).ToString();
                    historia.ubicacion = reader.GetValue(4).ToString();
                    historia.responsable = reader.GetValue(5).ToString();
                    historia.estado = reader.GetValue(6).ToString();
                    historia.realizado = reader.GetValue(7).ToString();
                    historia.autorizado = reader.GetValue(8).ToString();
                    historia.empresa = reader.GetValue(9).ToString();
                    historias.Add(historia);
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

        return historias;

    }



    public Boolean agregarHistoria(String fecha_inicio, String obs,
        int ubicacion_id, int articulo_id, int responsable_id, int estado_id, int realizado_id,int autorizado_id)
    {


        if (cerrarHistoria(articulo_id, fecha_inicio))
        {

            Boolean retu = true;
            String sql = @"insert into historial(fecha_inicio,obs,ubicacion_id,articulo_id,responsable_id,estado_id,realizado_id,autorizado_id)
                       values(@fecha_inicio,@obs,@ubicacion_id,@articulo_id,@responsable_id,@estado_id,@realizado_id,@autorizado_id)";

            try
            {
                using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
                {
                    conex.Open();
                    MySqlCommand comando = new MySqlCommand(sql, conex);

                    fecha_inicio = fecha_inicio.Replace('/','-');
                    comando.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                    comando.Parameters.AddWithValue("@obs", obs.ToUpper());
                    comando.Parameters.AddWithValue("@ubicacion_id", ubicacion_id);
                    comando.Parameters.AddWithValue("@articulo_id", articulo_id);
                    comando.Parameters.AddWithValue("@responsable_id", responsable_id);
                    comando.Parameters.AddWithValue("@estado_id", estado_id);
                    comando.Parameters.AddWithValue("@realizado_id", realizado_id);
                    comando.Parameters.AddWithValue("@autorizado_id", autorizado_id);
                    comando.ExecuteNonQuery();
                }
            }
            catch
            {
                retu = false;
            }

            return retu;

        }
        else
        {
            return false;
        }
    }


    private Boolean cerrarHistoria(int articulo_id, String fecha_fin)
    {

        Boolean retu = false;

        String sql = "select id from historial where articulo_id=articulo_id order by id desc limit 1";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {

                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@articulo_id)", articulo_id); 
                 Object obj = comando.ExecuteScalar();
                 if (obj!=null)
                {
                    int idMax = Convert.ToInt32(obj);

                    comando.Parameters.Clear();
                    sql = "update historial set fecha_fin=@fecha_fin where id=@id";

                    comando.CommandText = sql;
                    comando.Parameters.AddWithValue("@id", idMax);
                    fecha_fin = fecha_fin.Replace('/','-');
                    comando.Parameters.AddWithValue("@fecha_fin", fecha_fin);

                    comando.ExecuteNonQuery();
                }
                retu = true;
            }
        }
        catch
        {
            retu = false;
        }


        return retu;
    }



    public bool obtenerUltimaHistoria(int articulo_id)
    {
        bool bo=false;

        MySqlDataReader reader = null;

      /*  String sql = @"select historial.id,date_format(fecha_inicio,'%d/%m/%Y'),ifnull(date_format(fecha_fin,'%d/%m/%Y'),'N/A'),historial.obs,ubicacion.nombre,
                       respo.nombre,estado.nombre,realizado.nombre,autorizado.nombre,historial.estado_id,historial.ubicacion_id,historial.responsable_id from historial
                       left join   estado on historial.estado_id=estado.id
                       left join responsable as respo on historial.responsable_id=respo.id
                       left join responsable as realizado on historial.realizado_id=realizado.id
                       left join responsable as autorizado on historial.autorizado_id=autorizado.id
                       left join articulo on  historial.articulo_id =articulo.id
                       left join ubicacion on historial.ubicacion_id= ubicacion.id
                       where historial.articulo_id=@articulo_id order by id desc limit 1";*/


        String sql = @"select historial.id,fecha_inicio,ifnull(fecha_fin,'N/A'),historial.obs,ubicacion.id, responsable.id,estado.id,
                    empresa.nombre as nom_empresa,
                    historial.articulo_id  as articuloa,
	            	ubicacion.id as ubicaciona,ubicacion.empresa_id as ubicacionae from historial
		            inner join (select MAX(historial.id) as idMax,articulo_id from historial 
                    group by articulo_id) as tablaMax on tablaMax.articulo_id=historial.articulo_id
		            left join responsable on historial.responsable_id=responsable.id
		            left join estado on historial.estado_id=estado.id
		            left join ubicacion on historial.ubicacion_id=ubicacion.id
                    left join empresa on ubicacion.empresa_id=empresa.id
		            where historial.id=tablaMax.idMax and historial.articulo_id =@articulo_id";
       
        
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
                    this.id = reader.GetInt32(0);
                    this.fecha_inicio = reader.GetValue(1).ToString();
                    this.fecha_fin = reader.GetValue(2).ToString();
                    this.obs = reader.GetValue(3).ToString();
                    this.ubicacion_id = reader.GetInt32(4);
                    this.responsable_id = reader.GetInt32(5);
                    this.estado_id = reader.GetInt32(6);
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


    public int validarFecha(int articulo_id, String fecha_inicio)
    {

        DateTime fechaHistoria = Convert.ToDateTime(fecha_inicio);
        DateTime hoy = DateTime.Now;
        int bo = 1;

        if (fechaHistoria > hoy)
        {
            return 0;
        }
        else
        {
            MySqlDataReader reader = null;
            String sql = "select id from historial where fecha_inicio>@fecha_inicio and articulo_id=@articulo_id limit 1";
            try
            {
                using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
                {
                    conex.Open();
                    MySqlCommand comando = new MySqlCommand(sql, conex);
                    comando.Parameters.AddWithValue("@articulo_id", articulo_id);
                    fecha_inicio=fecha_inicio.Replace('/','-');
                    comando.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                    reader =  comando.ExecuteReader();
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
        }


        return bo;

    }



    public bool moverCompleto(string controlMovido, int idPadre, string usuario)
    {
        bool bo = false;
        try
        {
            String auxFecha = String.Format("{0:d}", DateTime.Now.ToShortDateString());
            String[] vec = auxFecha.Split('/');
            auxFecha = vec[2] + "/" + vec[1] + "/" + vec[0];

            // Datos de que se movio
            Articulo articuloTemp = new Articulo();
            if (articuloTemp.obtenerArticuloControl(controlMovido))
            {
                int auxId = articuloTemp.id;
                int auxEstado = 0;

                if (obtenerUltimaHistoria(auxId))
                {
                    auxEstado = this.estado_id;
                }

                // obteniendo ubicacion y datos del padre
                int auxUbicacion = 0;
                int auxResponsable = 0;
                int auxUsuario = 0;

                if (obtenerUltimaHistoria(idPadre))
                {

                    auxUbicacion = this.ubicacion_id;
                    auxResponsable = this.responsable_id;
                    auxUsuario = Int32.Parse(usuario);
                    if (auxEstado == 0)
                    {
                        auxEstado = this.estado_id;
                    }
                    if (agregarHistoria(auxFecha, Resources.Mensajes.Traslado_Automatico, auxUbicacion, auxId, auxResponsable, auxEstado, auxUsuario, auxUsuario))
                    {
                        bo = true;
                    }
                }
            }

        }
        catch
        {

        }

        return bo;
    }



 
}
