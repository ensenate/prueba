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
/// Summary description for Articulo
/// </summary>
public class Articulo
{

    public int id { get; set; }
    public String detalle2 { get; set; }
    public String serial { get; set; }
    public String control { get; set; }
    public String extra1 { get; set; }
    public String extra2 { get; set; }
    public String descrip { get; set; }
    public Boolean activo { get; set; }
    public String pertenece { get; set; }

    public int contenidos { get; set; }

    public int marca_id { get; set; }
    public int modelo_id { get; set; }
    public int articulo_id { get; set; }
    public int categoria_id { get; set; }
    public int empresa_id { get; set; }

    public String marca { get; set; }
    public String modelo { get; set; }
    public String articulo { get; set; }
    public String categoria { get; set; }
    public String empresa { get; set; }
    public Boolean asociar { get; set; }
    public String obs { get; set; }

    public String ip { get; set; }
    public String nombre_red { get; set; }
    public String nombre_equipo { get; set; }
    public String mac { get; set; }
    public String programas { get; set; }
    public String permisos { get; set; }
    public String tarea { get; set; }
    public String so { get; set; }

    public String fecha_formateo { get; set; }
    public String fecha_compra { get; set; }
    public String fecha_garantia { get; set; }
    public String usuarioso { get; set; }
    public String grupo { get; set; }

    public String borrado { get; set; }
    public String emotivo { get; set; }


    public String ubicacion { get; set; }

    public String responsable_nombre { get; set; }

    public String imagen { get; set; }
  

    public Articulo()
    {

    }

    public List<Articulo> obtenerListaArticulosReferencias(String filtro,String keys, int limite)
    {
        List<Articulo> articulos = new List<Articulo>();
        MySqlDataReader reader = null;
        String sql = "";


        if (keys != String.Empty)
        {
            sql = @"select tabla.id, tabla.detalle2, tabla.serial,tabla.extra1,tabla.extra2,tabla.activo,marca.nombre as marca,modelo.nombre as modelo,
                      categoria.nombre as categoria,tabla3.serialInterno as articulo,tabla.descripcion,tabla.serialInterno, ifnull(tabla2.cantidad,0) as contenidos,categoria.asociar,
                      tabla.borrado,tabla.emotivo,ifnull(his.nom_empresa,'Ninguna'),ifnull(his.nom_ubicacion,'Ninguna')  from articulo as tabla
                      left join categoria on tabla.categoria_id=categoria.id
                      left join modelo on tabla.modelo_id=modelo.id
                      left join marca on tabla.marca_id=marca.id
                      left join (select  articulo_id,count(*) as cantidad from articulo 
                      group by articulo_id ) as tabla2 on tabla.id=tabla2.articulo_id
                      left join articulo tabla3 on  tabla.articulo_id=tabla3.id
	                  left join (select  empresa.nombre as nom_empresa,ubicacion.nombre as nom_ubicacion,
                                 historial.articulo_id  as articuloa, responsable.id as responsablea,
				                 estado.id as estadoa,ubicacion.id as ubicaciona,ubicacion.empresa_id as ubicacionae from historial
				                 inner join (select MAX(historial.id) as idMax,articulo_id from historial group by articulo_id) as tablaMax on tablaMax.articulo_id=historial.articulo_id
								 left join responsable on historial.responsable_id=responsable.id
				                 left join estado on historial.estado_id=estado.id
				                 left join ubicacion on historial.ubicacion_id=ubicacion.id
                                 left join empresa on ubicacion.empresa_id=empresa.id
								 where historial.id=tablaMax.idMax			                           
                      			 
                                 ) as his on tabla.id=his.articuloa
                      inner join (select articulo_id,tip_id from keywork where (@keys) ) as keywork on tabla.id=keywork.articulo_id                     
                      @filtro
                      order by tabla.id desc";
                
            sql = sql.Replace("@keys", keys);

        }
        else
        {
            sql = @"select tabla.id, tabla.detalle2, tabla.serial,tabla.extra1,tabla.extra2,tabla.activo,marca.nombre as marca,modelo.nombre as modelo,
                      categoria.nombre as categoria,tabla3.serialInterno as articulo,tabla.descripcion,tabla.serialInterno, ifnull(tabla2.cantidad,0) as contenidos,categoria.asociar,
                      tabla.borrado,tabla.emotivo,ifnull(his.nom_empresa,'Ninguna'),ifnull(his.nom_ubicacion,'Ninguna') from articulo as tabla
                      left join categoria on tabla.categoria_id=categoria.id
                      left join modelo on tabla.modelo_id=modelo.id
                      left join marca on tabla.marca_id=marca.id
                      left join (select  articulo_id,count(*) as cantidad from articulo 
                      group by articulo_id ) as tabla2 on tabla.id=tabla2.articulo_id
                      left join articulo tabla3 on  tabla.articulo_id=tabla3.id
	                  left join (select  empresa.nombre as nom_empresa,ubicacion.nombre as nom_ubicacion,
                                 historial.articulo_id  as articuloa, responsable.id as responsablea,
				                 estado.id as estadoa,ubicacion.id as ubicaciona,ubicacion.empresa_id as ubicacionae from historial
				                 inner join (select MAX(historial.id) as idMax,articulo_id from historial group by articulo_id) as tablaMax on tablaMax.articulo_id=historial.articulo_id
								 left join responsable on historial.responsable_id=responsable.id
				                 left join estado on historial.estado_id=estado.id
				                 left join ubicacion on historial.ubicacion_id=ubicacion.id
                                 left join empresa on ubicacion.empresa_id=empresa.id
								 where historial.id=tablaMax.idMax			                           
                      			 
                                 ) as his on tabla.id=his.articuloa
                      @filtro  
                      order by tabla.id desc ";
        }

        if (limite > 0)
        {
            sql = sql + " limit  "+limite;
        }

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
                        Articulo articulo = new Articulo();
                        articulo.id = reader.GetInt32(0);
                        articulo.detalle2 = reader.GetString(1);
                        articulo.serial = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                        articulo.extra1 = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                        articulo.extra2 = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                        articulo.activo = reader.GetBoolean(5);
                        articulo.marca = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                        articulo.modelo = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                        articulo.categoria = reader[8] == DBNull.Value ? String.Empty : reader.GetValue(8).ToString();
                        articulo.pertenece = reader[9] == DBNull.Value ? String.Empty : reader.GetValue(9).ToString();
                        articulo.descrip = reader[10] == DBNull.Value ? String.Empty : reader.GetValue(10).ToString();
                        articulo.control = reader[11] == DBNull.Value ? String.Empty : reader.GetValue(11).ToString();
                    
                        articulo.contenidos = reader.GetInt32(12);
                        articulo.asociar = reader.GetBoolean(13);

                        articulo.borrado = reader[14] == DBNull.Value ? String.Empty : reader.GetValue(14).ToString();
                        articulo.emotivo = reader[15] == DBNull.Value ? String.Empty : reader.GetValue(15).ToString();

                        articulo.empresa = reader[16] == DBNull.Value ? String.Empty : reader.GetValue(16).ToString();
                        articulo.ubicacion = reader[17] == DBNull.Value ? String.Empty : reader.GetValue(17).ToString();

                        articulos.Add(articulo);

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

        return articulos;

    }

    public Boolean agregarActualizarArticulo(int id, String detalle2, String serial, String extra1,
        String extra2, Boolean activo, String categoria_id, String marca_id, String modelo_id, String descripcion,String usuario,String empresa_id,String obs,
        String ip,String nombre_red,String nombre_equipo,String mac,String programas,String permisos,
        String tarea, String so, String fecha_formateo, String fecha_compra, String fecha_garantia,
        String usuarioso, String grupo, String imagen)
    {

        Boolean retu = true;
        String sql;

        if (id == 0)
        {
            sql = @"insert into articulo(serialInterno,detalle2,serial,extra1,extra2,activo,marca_id ,modelo_id,categoria_id,articulo_id,descripcion,usuario,empresa_id,obs,ip,nombre_red,nombre_equipo,mac,programas,permisos,tarea,so,fecha_formateo,
                     fecha_compra,fecha_garantia,usuarioso,grupo,imagen) values(@serialInterno,@detalle2,@serial,@extra1,@extra2,@activo, @marca_id,@modelo_id,@categoria_id,@articulo_id,@descripcion,@usuario,@empresa_id,@obs,
                     @ip,@nombre_red,@nombre_equipo,@mac,@programas,@permisos,@tarea,@so,@fecha_formateo,
                     @fecha_compra,@fecha_garantia,@usuarioso,@grupo,@imagen)";
        }
        else
        {
            sql = @"update articulo set detalle2=@detalle2,serial=@serial,extra1=@extra1,
                    extra2=@extra2,activo=@activo,marca_id=@marca_id,
                    modelo_id=@modelo_id,
                    descripcion=@descripcion,usuario=@usuario,empresa_id=@empresa_id,obs=@obs,
                    ip=@ip,nombre_red=@nombre_red,nombre_equipo=@nombre_equipo,mac=@mac,
                    programas=@programas,permisos=@permisos,tarea=@tarea,so=@so,fecha_formateo=@fecha_formateo,
                    fecha_compra=@fecha_compra,fecha_garantia=@fecha_garantia,usuarioso=@usuarioso,grupo=@grupo,imagen=@imagen
                    where id=@id";
        }
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);

                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@empresa_id", empresa_id);
                comando.Parameters.AddWithValue("@obs", obs);
                String prefijo = "";
                if (id != 0)
                {
                    comando.Parameters.AddWithValue("@id", id);
                    prefijo = "N/A";
                }
                else {
                    prefijo = generarSerial(categoria_id);
               
                    comando.Parameters.AddWithValue("@serialInterno", prefijo);
                    comando.Parameters.AddWithValue("@categoria_id", categoria_id);
                 
                }
                if (prefijo != "")
                {
                    comando.Parameters.AddWithValue("@detalle2", detalle2.ToUpper());
                    comando.Parameters.AddWithValue("@serial", serial.ToUpper());
                    comando.Parameters.AddWithValue("@extra1", extra1.ToUpper());
                    comando.Parameters.AddWithValue("@extra2", extra2.ToUpper());
                    comando.Parameters.AddWithValue("@activo", activo);
                    comando.Parameters.AddWithValue("@marca_id", marca_id);
                    comando.Parameters.AddWithValue("@modelo_id", modelo_id);
                    if (id == 0)
                    {
                        comando.Parameters.AddWithValue("@articulo_id", 0);
                    }
                    comando.Parameters.AddWithValue("@descripcion", descripcion.ToUpper());

                    comando.Parameters.AddWithValue("@ip", ip.ToUpper());
                    comando.Parameters.AddWithValue("@nombre_red", nombre_red.ToUpper());
                    comando.Parameters.AddWithValue("@nombre_equipo", nombre_equipo.ToUpper());
                    comando.Parameters.AddWithValue("@mac", mac.ToUpper());
                    comando.Parameters.AddWithValue("@programas", programas.ToUpper());
                    comando.Parameters.AddWithValue("@permisos", permisos.ToUpper());
                    comando.Parameters.AddWithValue("@tarea", tarea.ToUpper());
                    comando.Parameters.AddWithValue("@so", so.ToUpper());
                    comando.Parameters.AddWithValue("@fecha_formateo", fecha_formateo==String.Empty?null:fecha_formateo);
                    comando.Parameters.AddWithValue("@fecha_compra", fecha_compra==String.Empty?null:fecha_compra);
                    comando.Parameters.AddWithValue("@fecha_garantia", fecha_garantia==String.Empty?null:fecha_garantia);
                    comando.Parameters.AddWithValue("@usuarioso", usuarioso.ToUpper());
                    comando.Parameters.AddWithValue("@grupo", grupo.ToUpper());
                    comando.Parameters.AddWithValue("@imagen", imagen.ToUpper());
                    
                    comando.ExecuteNonQuery();
                }
                else
                {
                    retu = false;
                }
                 
            } 
        }
        catch
        {
            
            retu = false;
        }

        return retu;

    }

    public String generarSerial(String categoria)
    {

         String serialInterno = "";

         try
         {
             using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
             {
                 conex.Open();
                 String prefijo = Categoria.obtenerPrefijo(categoria);
                 if (prefijo != "")
                 {
                     String sql = "select serial from configuracion";
                     MySqlCommand comando = new MySqlCommand(sql, conex);
                     int serial = Int32.Parse(comando.ExecuteScalar().ToString());
                     sql = "update configuracion set serial=(serial+1)";
                     comando.CommandText = sql;
                     comando.ExecuteNonQuery();
                     serialInterno = Util.formatiarControl(serial.ToString(), prefijo);
    

                 }
                 else
                 {
                     serialInterno = "";
                 }
             }
         }
         catch
         {
             serialInterno = "";
         }


        return serialInterno;

    }

    public Boolean obtenerArticuloId(int id)
    {

        Boolean bo = false;
        MySqlDataReader reader = null;

        String sql = @"select articulo.id, articulo.detalle2, serial,extra1,extra2,activo,marca.nombre as marca,modelo.nombre as modelo,
                        categoria.nombre as categoria,articulo.detalle2 as articulo,articulo.descripcion,serialInterno,empresa.nombre,empresa.id,
                        articulo.categoria_id,articulo.marca_id,articulo.modelo_id,categoria.asociar,
                      
                        ip,nombre_red,nombre_equipo,mac,programas,permisos,tarea,so,fecha_formateo,
                        fecha_compra,fecha_garantia,usuarioso,grupo,obs,articulo_id,articulo.imagen from articulo 
                      
                        left join categoria on articulo.categoria_id=categoria.id
                        left join modelo on articulo.modelo_id=modelo.id
                        left join marca on articulo.marca_id=marca.id
                        left join empresa on articulo.empresa_id=empresa.id
                        where articulo.id=@id ";

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
                    this.detalle2 = reader.GetString(1);
                    this.serial = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    this.extra1 = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    this.extra2 = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    this.activo = reader.GetBoolean(5);
                    this.marca = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                    this.modelo = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    this.categoria = reader[8] == DBNull.Value ? String.Empty : reader.GetValue(8).ToString();
                    this.pertenece = reader[9] == DBNull.Value ? String.Empty : reader.GetValue(9).ToString();
                    this.descrip = reader[10] == DBNull.Value ? String.Empty : reader.GetValue(10).ToString();
                    this.control = reader[11] == DBNull.Value ? String.Empty : reader.GetValue(11).ToString();
                    this.empresa = reader[12] == DBNull.Value ? String.Empty : reader.GetValue(12).ToString();
                    this.empresa_id = reader[13] == DBNull.Value ? 0 : reader.GetInt32(13);

                    this.categoria_id = reader[14] == DBNull.Value ? 0 : reader.GetInt32(14);
                    this.marca_id = reader[15] == DBNull.Value ? 0 : reader.GetInt32(15);
                    this.modelo_id = reader[16] == DBNull.Value ? 0 : reader.GetInt32(16);
                    this.asociar = reader.GetBoolean(17);

                    this.ip = reader[18] == DBNull.Value ? String.Empty : reader.GetValue(18).ToString();
                    this.nombre_red = reader[19] == DBNull.Value ? String.Empty : reader.GetValue(19).ToString();
                    this.nombre_equipo = reader[20] == DBNull.Value ? String.Empty : reader.GetValue(20).ToString();
                    this.mac = reader[21] == DBNull.Value ? String.Empty : reader.GetValue(21).ToString();
                    this.programas = reader[22] == DBNull.Value ? String.Empty : reader.GetValue(22).ToString();
                    this.permisos = reader[23] == DBNull.Value ? String.Empty : reader.GetValue(23).ToString();
                    this.tarea = reader[24] == DBNull.Value ? String.Empty : reader.GetValue(24).ToString();
                    this.so = reader[25] == DBNull.Value ? String.Empty : reader.GetValue(25).ToString();
                    this.fecha_formateo = reader[26] == DBNull.Value ? String.Empty : reader.GetValue(26).ToString();
                    this.fecha_compra = reader[27] == DBNull.Value ? String.Empty : reader.GetValue(27).ToString();
                    this.fecha_garantia = reader[28] == DBNull.Value ? String.Empty : reader.GetValue(28).ToString();
                    this.usuarioso = reader[29] == DBNull.Value ? String.Empty : reader.GetValue(29).ToString();
                    this.grupo = reader[30] == DBNull.Value ? String.Empty : reader.GetValue(30).ToString();
                    this.obs = reader[31] == DBNull.Value ? String.Empty : reader.GetValue(31).ToString();
                    this.articulo_id = reader.GetInt32(32);
                    this.imagen = reader[33] == DBNull.Value ? String.Empty : reader.GetValue(33).ToString();
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


    public Boolean agregarItem(String serialInterno, int articulo_id,String usuario)
    {

        Boolean retu = true;

        String sql = " update articulo set articulo_id=@articulo_id,usuario=@usuario where serialInterno=@serialInterno ";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@articulo_id", articulo_id);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@serialInterno", serialInterno.ToUpper());
                int n=comando.ExecuteNonQuery();
                
                if(n==0){
                    retu = false;
                }
            }
        }
        catch
        {
            retu = false;
        }


        return retu;

    }

    public Boolean agregarItem(int id, int articulo_id, String usuario)
    {

        Boolean retu = true;

        String sql = " update articulo set articulo_id=@articulo_id,usuario=@usuario where id=@id ";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@articulo_id", articulo_id);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@id", id);
                int n = comando.ExecuteNonQuery();

                if (n == 0)
                {
                    retu = false;
                }
            }
        }
        catch
        {
            retu = false;
        }


        return retu;

    }



    public Boolean obtenerArticuloControl(String control)
    {

        Boolean retu = false;
        MySqlDataReader reader = null;

        String sql = "select id,articulo_id,serialInterno from articulo where serialInterno=@control limit 1";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@control", control.ToUpper());
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    this.id = reader.GetInt32(0);
                    this.articulo_id = reader.GetInt32(1);
                    this.control = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
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

    public int obtenerUltimoId()
    {
        int idArticulo = 0;

        String sql = "select max(id)  from articulo";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                Object obj = comando.ExecuteScalar();
                if (obj != null)
                {
                    idArticulo = Convert.ToInt32(obj);
                }
            }
        }
        catch
        {
            idArticulo = 0;
        }


        return idArticulo;

    }

    public List<Articulo> obtenerArticulosSinNingunaRelacion(String filtro){

        List<Articulo> articulos = new List<Articulo>();

        MySqlDataReader reader = null;

        String sql = @"select articulo.id, articulo.detalle2, serial,extra1,extra2,activo,marca.nombre as marca,modelo.nombre as modelo,
                          categoria.nombre as categoria,articulo.nombre as articulo,descripcion,serialInterno from articulo 
                          left join categoria on articulo.categoria_id=categoria.id
                          left join modelo on articulo.modelo_id=modelo.id
                          left join marca on articulo.marca_id=marca.id
                          @filtro  order by articulo.id desc";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                sql = sql.Replace("@filtro", filtro.ToUpper());
                MySqlCommand comando = new MySqlCommand(sql, conex);

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.id = reader.GetInt32(0);
                    articulo.detalle2 = reader.GetString(1);
                    articulo.serial = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    articulo.extra1 = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    articulo.extra2 = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    articulo.activo = reader.GetBoolean(5);
                    articulo.marca = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                    articulo.modelo = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    articulo.categoria = reader[8] == DBNull.Value ? String.Empty : reader.GetValue(8).ToString();
                    articulo.pertenece = reader[9] == DBNull.Value ? String.Empty : reader.GetValue(9).ToString();
                    articulo.descrip = reader[10] == DBNull.Value ? String.Empty : reader.GetValue(10).ToString();
                    articulo.control = reader[11] == DBNull.Value ? String.Empty : reader.GetValue(11).ToString();
                    articulos.Add(articulo);
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

        return articulos;
    }


    public Boolean validarNoTengaArticulo(int articulo_id)
    {

        Boolean bo = true;
        MySqlDataReader reader = null;

        String sql = @"select id from articulo where articulo_id=@articulo_id limit 1 ";

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



    public List<String> obtenerExtras(bool extra1, int categoria_id)
    {

        List<String> extras = new List<String>();

        MySqlDataReader reader = null;
        String sql = "";
        if (extra1)
        {
             sql = @"select DISTINCT articulo.extra1 from articulo
                        where articulo.extra1<>'' and articulo.categoria_id=@categoria_id order by articulo.extra1 asc ";
        }
        else {
             sql = @"select DISTINCT articulo.extra2 from articulo
                        where articulo.extra2<>'' and articulo.categoria_id=@categoria_id order by articulo.extra2 asc ";
        }

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@categoria_id", categoria_id);

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    extras.Add(reader.GetString(0));
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

        return extras;
    }


    public Boolean eliminarItem(string serialInterno, string motivo)
    {

        Boolean retu = true;

        String sql = " update articulo set activo=0, articulo_id=0 ,borrado='S',emotivo=@motivo where serialInterno=@serialInterno ";
        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@motivo", motivo);
                comando.Parameters.AddWithValue("@serialInterno", serialInterno);
                int n = comando.ExecuteNonQuery();

                if (n == 0)
                {
                    retu = false;
                }
            }
        }
        catch
        {
            retu = false;
        }


        return retu;

    }

    
    public List<Articulo> obtenerAsociados(int id) {

        List<Articulo> articulos = new List<Articulo>();

        MySqlDataReader reader = null;

        String sql = @"select articulo.id, articulo.detalle2, serial,extra1,extra2,activo,marca.nombre as marca,modelo.nombre as modelo,
                        categoria.nombre as categoria,articulo.detalle2 as articulo,articulo.descripcion,serialInterno,empresa.nombre,empresa.id,
                        articulo.categoria_id,articulo.marca_id,articulo.modelo_id,categoria.asociar,
                      
                        ip,nombre_red,nombre_equipo,mac,programas,permisos,tarea,so,fecha_formateo,
                        fecha_compra,fecha_garantia,usuarioso,grupo,obs,articulo_id from articulo 
                      
                        left join categoria on articulo.categoria_id=categoria.id
                        left join modelo on articulo.modelo_id=modelo.id
                        left join marca on articulo.marca_id=marca.id
                        left join empresa on articulo.empresa_id=empresa.id
                        where articulo.id=@id ";

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
                    Articulo articulo = new Articulo();

                    articulo.id = reader.GetInt32(0);
                    articulo.detalle2 = reader.GetString(1);
                    articulo.serial = reader[2] == DBNull.Value ? String.Empty : reader.GetValue(2).ToString();
                    articulo.extra1 = reader[3] == DBNull.Value ? String.Empty : reader.GetValue(3).ToString();
                    articulo.extra2 = reader[4] == DBNull.Value ? String.Empty : reader.GetValue(4).ToString();
                    articulo.activo = reader.GetBoolean(5);
                    articulo.marca = reader[6] == DBNull.Value ? String.Empty : reader.GetValue(6).ToString();
                    articulo.modelo = reader[7] == DBNull.Value ? String.Empty : reader.GetValue(7).ToString();
                    articulo.categoria = reader[8] == DBNull.Value ? String.Empty : reader.GetValue(8).ToString();
                    articulo.pertenece = reader[9] == DBNull.Value ? String.Empty : reader.GetValue(9).ToString();
                    articulo.descrip = reader[10] == DBNull.Value ? String.Empty : reader.GetValue(10).ToString();
                    articulo.control = reader[11] == DBNull.Value ? String.Empty : reader.GetValue(11).ToString();
                    articulo.empresa = reader[12] == DBNull.Value ? String.Empty : reader.GetValue(12).ToString();
                    articulo.empresa_id = reader[13] == DBNull.Value ? 0 : reader.GetInt32(13);

                    articulo.categoria_id = reader[14] == DBNull.Value ? 0 : reader.GetInt32(14);
                    articulo.marca_id = reader[15] == DBNull.Value ? 0 : reader.GetInt32(15);
                    articulo.modelo_id = reader[16] == DBNull.Value ? 0 : reader.GetInt32(16);
                    articulo.asociar = reader.GetBoolean(17);

                    articulo.ip = reader[18] == DBNull.Value ? String.Empty : reader.GetValue(18).ToString();
                    articulo.nombre_red = reader[19] == DBNull.Value ? String.Empty : reader.GetValue(19).ToString();
                    articulo.nombre_equipo = reader[20] == DBNull.Value ? String.Empty : reader.GetValue(20).ToString();
                    articulo.mac = reader[21] == DBNull.Value ? String.Empty : reader.GetValue(21).ToString();
                    articulo.programas = reader[22] == DBNull.Value ? String.Empty : reader.GetValue(22).ToString();
                    articulo.permisos = reader[23] == DBNull.Value ? String.Empty : reader.GetValue(23).ToString();
                    articulo.tarea = reader[24] == DBNull.Value ? String.Empty : reader.GetValue(24).ToString();
                    articulo.so = reader[25] == DBNull.Value ? String.Empty : reader.GetValue(25).ToString();
                    articulo.fecha_formateo = reader[26] == DBNull.Value ? String.Empty : reader.GetValue(26).ToString();
                    articulo.fecha_compra = reader[27] == DBNull.Value ? String.Empty : reader.GetValue(27).ToString();
                    articulo.fecha_garantia = reader[28] == DBNull.Value ? String.Empty : reader.GetValue(28).ToString();
                    articulo.usuarioso = reader[29] == DBNull.Value ? String.Empty : reader.GetValue(29).ToString();
                    articulo.grupo = reader[30] == DBNull.Value ? String.Empty : reader.GetValue(30).ToString();
                    articulo.obs = reader[31] == DBNull.Value ? String.Empty : reader.GetValue(31).ToString();
                    articulo.articulo_id = reader.GetInt32(32);
                    articulos.Add(articulo);

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

        return articulos;
    }


}