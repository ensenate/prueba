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
/// Summary description for Reportes
/// </summary>
public class Reportes
{
	public Reportes()
	{
		
	}

    public  bool listaUsuarios(DataGeneral.UsuariosDataTable data)
    {

        bool bo = false;
        MySqlDataReader reader = null;
        String sql = @"select cuenta.usuario as Cuenta,responsable.nombre as Usuario,rol.nombre as Rol from cuenta
                        inner join responsable on cuenta.responsable_id =responsable.id
                        inner join rol on cuenta.rol_id=rol.id";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();

                MySqlCommand comando = new MySqlCommand(sql, conex);

                reader = comando.ExecuteReader();
                if (reader != null)
                {
                    data.Load(reader);
                    bo = true;
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

        return bo;
    }


    public void listaActivosAsociaciones(DataGeneral.PadreHijosDataTable data)
    {
    
        MySqlDataReader reader = null;
        String sql = @"select   tabla.serialInterno as Control, categoria.nombre as Categoria,marca.nombre as Marca,modelo.nombre as Modelo,
                      ifnull(his.nom_empresa,'S/E') as Empresa,ifnull(his.nom_ubicacion,'S/E') as Ubicacion,tabla3.serialInterno as Padre,tabla3.serial,tabla3.extra1,tabla3.extra2 from articulo as tabla
                      left join categoria on tabla.categoria_id=categoria.id
                      left join modelo on tabla.modelo_id=modelo.id
                      left join marca on tabla.marca_id=marca.id
                      left join (select  articulo_id,count(*) as cantidad from articulo 
                      group by articulo_id ) as tabla2 on tabla.id=tabla2.articulo_id
                      left join articulo tabla3 on  tabla.articulo_id=tabla3.id
                      left join empresa tabla4 on  tabla3.empresa_id=tabla4.id
	                  left join (select  ubicacion.empresa_id,empresa.nombre as nom_empresa,ubicacion.nombre as nom_ubicacion,
                                 historial.articulo_id  as articuloa, responsable.id as responsablea,
				                 estado.id as estadoa,ubicacion.id as ubicaciona,ubicacion.empresa_id as ubicacionae from historial
				                 inner join (select MAX(historial.id) as idMax,articulo_id from historial group by articulo_id) as tablaMax on tablaMax.articulo_id=historial.articulo_id
								 left join responsable on historial.responsable_id=responsable.id
				                 left join estado on historial.estado_id=estado.id
				                 left join ubicacion on historial.ubicacion_id=ubicacion.id
                                 left join empresa on ubicacion.empresa_id=empresa.id
								 where historial.id=tablaMax.idMax			                                      			 
                                 ) as his on tabla.id=his.articuloa
                                  WHERE   tabla3.serialInterno IS NOT NULL
                      order by tabla.id asc";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();

                MySqlCommand comando = new MySqlCommand(sql, conex);

                reader = comando.ExecuteReader();
                if (reader != null)
                {
                    data.Load(reader);
                    
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


    }

    public void listadoEtiquetas(DataGeneral.EtiquetaDataTable data, String filtro, String keys)
    {

        MySqlDataReader reader = null;
        String sql = "";


        if (keys != String.Empty)
        {
            sql = @"select tabla.serialInterno as Codigo,emp.nombre as Empresa,  date_format(now(),'%d/%m/%Y') as Fecha  from articulo as tabla
                      left join categoria on tabla.categoria_id=categoria.id
                      left join modelo on tabla.modelo_id=modelo.id
                      left join marca on tabla.marca_id=marca.id
                      left join (select  articulo_id,count(*) as cantidad from articulo 
                      group by articulo_id ) as tabla2 on tabla.id=tabla2.articulo_id
                      left join articulo tabla3 on  tabla.articulo_id=tabla3.id
                      left join empresa  as emp on  tabla.empresa_id=emp.id  
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
            sql = @"select tabla.serialInterno as Codigo,emp.nombre as Empresa, date_format(now(),'%d/%m/%Y') as Fecha  from articulo as tabla
                      left join categoria on tabla.categoria_id=categoria.id
                      left join modelo on tabla.modelo_id=modelo.id
                      left join marca on tabla.marca_id=marca.id
                      left join (select  articulo_id,count(*) as cantidad from articulo 
                      group by articulo_id ) as tabla2 on tabla.id=tabla2.articulo_id
                      left join articulo tabla3 on  tabla.articulo_id=tabla3.id
                      left join empresa  as emp on  tabla.empresa_id=emp.id
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
                      order by tabla.id desc";
        }

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                sql = sql.Replace("@filtro", filtro);
                MySqlCommand comando = new MySqlCommand(sql, conex);

                reader = comando.ExecuteReader();
                if (reader != null)
                {
                    data.Load(reader);

                }
            }
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

    }

    public void listadoRetirados(DataGeneral.RetiradosDataTable data){

        MySqlDataReader reader = null;
        String sql = @"select   tabla.serialInterno as Control, categoria.nombre as Categoria,marca.nombre as Marca,modelo.nombre as Modelo,
                      ifnull(tabla.serial,'S/E') as Serial ,tabla.emotivo as Obs ,'' as Usuario from articulo as tabla
                      left join categoria on tabla.categoria_id=categoria.id
                      left join modelo on tabla.modelo_id=modelo.id
                      left join marca on tabla.marca_id=marca.id
                      where borrado='S'  ";

        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();

                MySqlCommand comando = new MySqlCommand(sql, conex);

                reader = comando.ExecuteReader();
                if (reader != null)
                {
                    data.Load(reader);

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


    }

}
