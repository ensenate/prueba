using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
//Prueba de la libreria itextsharp
//Clase necesaria para poder utilizar iTextSharp
using System.IO;
//Clases necesarias de iTextSharp
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
   

//prueba 2
using System.Collections.Generic;
using System.Net;

public partial class _Default : System.Web.UI.Page
{

    List<Dominio> domCategoriaF, domMarcaF, domModeloF,domKeyF;
    List<Ubicacion> ubicaciones;
    List<Modelo> domModelo = new List<Modelo>();

    protected void Page_Load(object sender, EventArgs e)
    {   
    	if (!IsPostBack)
        {
  			//SE ELIMINARON LAS VARIABLES CONTROLES, FILTRO Y FILTRO KEYS, NO SE LE VIO UTILIDAD

            //CARGA DE DATOS PARA QUERY 
            domCategoriaF = Dominio.obtenerDominios(GlobalEnum.dominios.Categoria.ToString());
            lstCategoriaF.Items.Add(new System.Web.UI.WebControls.ListItem ("N/A", "0"));
            foreach (Dominio d in domCategoriaF)
            {
                lstCategoriaF.Items.Add(new System.Web.UI.WebControls.ListItem (d.nombre, d.id.ToString()));
            }


            domMarcaF = Dominio.obtenerDominios(GlobalEnum.dominios.Marca.ToString());
            lstMarcaF.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
            foreach (Dominio d in domMarcaF)
            {
                lstMarcaF.Items.Add(new System.Web.UI.WebControls.ListItem(d.nombre, d.id.ToString()));
            }

            //la siguiente line se comento para probar el rellenado automatico de los modelos por marca
            //domModeloF = Dominio.obtenerDominios(GlobalEnum.dominios.Modelo.ToString());
            lstModeloF.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
            //el siguiente for se removio para probar el rellenado automatico de las ubicasiones de las empresas
            /*foreach (Dominio d in domModeloF)
            {
                lstModeloF.Items.Add(new System.Web.UI.WebControls.ListItem(d.nombre, d.id.ToString()));
            }*/


            List<Empresa> empresas = Empresa.obtenerListadoEmpresa(false);
            lstEmpresa2.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
            foreach (Empresa d in empresas)
            {
                lstEmpresa2.Items.Add(new System.Web.UI.WebControls.ListItem(d.nombre, d.id.ToString()));

            }


            List<Responsable> responsables = Responsable.obtenerListaPersonal(1);
            lstResponsable.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
			foreach (Responsable d in responsables)
            {
                lstResponsable.Items.Add(new System.Web.UI.WebControls.ListItem(d.nombre, d.id.ToString()));

            }


            //la siguiente line se comento para probar el rellenado automatico de las ubicaciones por empresa
            //List<Ubicacion> ubicaciones = Ubicacion.obtenerListadoUbicacion(0);
            lstUbicacion.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
            //lstUbicacion.Items.Add(new ListItem("NINGUNA", "0"));
            //SE LE REMOVIO LA LINEA ANTERIOR NO SE LE VEIA LOGICA 
			
            //el siguiente for se removio para probar el rellenado automatico de las ubicasiones de las empresas
            /*foreach (Ubicacion d in ubicaciones)
            {
                lstUbicacion.Items.Add(new System.Web.UI.WebControls.ListItem(d.nombre, d.id.ToString()));

            }*/


            List<Dominio> domEstadoF = Dominio.obtenerDominios(GlobalEnum.dominios.Estado.ToString());
            lstEstado.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
            //SE LE REMOVIO LA LINEA ANTERIOR NO SE LE VEIA LOGICA 
            //lstEstado.Items.Add(new ListItem("NINGUNO", "0"));
            foreach (Dominio d in domEstadoF)
            {
                lstEstado.Items.Add(new System.Web.UI.WebControls.ListItem(d.nombre, d.id.ToString()));
            }
//-------------- CARGA DEL COMBOTEXT EN USO OJO --------------------------------------------
            lstEstadoF.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
            lstEstadoF.Items.Add(new System.Web.UI.WebControls.ListItem("Si", "1"));
            lstEstadoF.Items.Add(new System.Web.UI.WebControls.ListItem("No", "2"));
            

//-------------- CARGA DE COMBOTEXT ELIMINADOS OJO ------------------------------------------
            lstEliminados.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
            lstEliminados.Items.Add(new System.Web.UI.WebControls.ListItem("Si", "1"));
            lstEliminados.Items.Add(new System.Web.UI.WebControls.ListItem("No", "2"));



            //hay argumentos que no se usaron debido a que no se consideraron necesarios extraerlos desde el default del inventario
        }

	}
//-------------- CARGA DE SOLO LAS UBICASIONES DE UNA EMPRESA COMO TAL--------------------------------
    protected void lstEmpresa2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(lstEmpresa2.SelectedValue != "0"){
            if (lstEmpresa2.Items.Count > 0)
            {
                ubicaciones = Ubicacion.obtenerListadoUbicacion(Int32.Parse(lstEmpresa2.SelectedValue));
                lstUbicacion.Items.Clear();
                foreach (Ubicacion u in ubicaciones)
                {
                    lstUbicacion.Items.Add(new System.Web.UI.WebControls.ListItem(u.nombre, u.id.ToString()));
                }
            }
        }
        else{
            lstUbicacion.Items.Clear();
            lstUbicacion.Items.Add(new System.Web.UI.WebControls.ListItem("N/A","0"));
        }
    }
//----------------------------------------------------------------------------------------------------------------------    
	protected void btnReporteFiltro_Click(object sender, EventArgs e)
    {
        busqueda_del_filtro();
    }

    private void busqueda_del_filtro() {

    	string filtro = "";
    	string dato;


    	//el filtro por categoria
    	dato = lstCategoriaF.SelectedValue;
        if (dato != "0")
        {
    	dato = lstCategoriaF.SelectedItem.ToString();
            if (filtro == "")
            {
                filtro = " where categoria.nombre like '" + dato + "' ";
            }
            else
            {
                filtro = filtro + " and categoria.nombre like '" + dato + "' ";
            }
        }


        //el filtro por marca
        dato = lstMarcaF.SelectedValue;
        if (dato != "0")
        {
        dato = lstMarcaF.SelectedItem.ToString();
            if (filtro == "")
            {
                filtro = " where marca.nombre like '" + dato + "' ";
            }
            else
            {
                filtro = filtro + " and marca.nombre like '" + dato + "' ";
            }
        }


        //el filtro por modelo
        dato = lstModeloF.SelectedValue;
        if (dato != "0")
        {
        dato = lstModeloF.SelectedItem.ToString();
            if (filtro == "")
            {
                filtro = " where modelo.nombre like '" + dato + "' ";
            }
            else
            {
                filtro = filtro + " and modelo.nombre like '" + dato + "' ";
            }
        }

        //el filtro por SERIAL
        dato = txtSerialF.Text.Trim();
        if (dato != "")
        {
            if (filtro == "")
            {
                filtro = " where serial like'%" + dato + "%' ";
            }
            else
            {
                filtro = filtro + " and serial like'%" + dato + "%' ";
            }
        }


        //FILTRO PARA EL CONTROL
        dato = txtControlF.Text.Trim();
        if (dato != "")
        {
            if (filtro == "")
            {
                filtro = " where articulo.serialinterno like'%" + dato + "%' ";
            }
            else
            {
                filtro = filtro + " and articulo.serialinterno like'%" + dato + "%' ";
            }
        }


        //FILTRO EN USO (ACTIVO)
		dato = lstEstadoF.SelectedValue;
        if (dato != "0")
        {
            if (filtro == "")
            {
            	if(dato == "1"){
                	filtro = " where activo='" + dato + "' ";
            	}
            	if(dato == "2"){
            		dato = "0";
            		filtro = " where activo='" + dato + "' ";
            	}
            }
            else
            {
            	if(dato == "1"){
                	filtro = filtro + " and activo='" + dato + "' ";
            	}
            	if(dato == "2"){
            		dato = "0";
            		filtro = filtro + " and activo='" + dato + "' ";
            	}
            }
        }

        
        //FILTRO EXTRA 1
        dato = txtExtra1F.Text.Trim();
        if (dato != "")
        {
            if (filtro == "")
            {
                filtro = " where extra1 like'%" + dato + "%' ";
            }
            else
            {
                filtro = filtro + " and tabla.extra like'%" + dato + "%' ";
            }
        }


        //FILTRO EXTRA 2
        dato = txtExtra2F.Text.Trim();
        if (dato != "")
        {
            if (filtro == "")
            {
                filtro = " where extra2 like'%" + dato + "%' ";
            }
            else
            {
                filtro = filtro + " and extra2 like'%" + dato + "%' ";
            }
        }



        //FILTRO ELIMINADOS
        dato = lstEliminados.SelectedValue;
        if (dato != "0")
        {
            if (filtro == "")
            {
            	if(dato == "1"){
            		dato = "S";
                	filtro = " where borrado='" + dato + "' ";
            	}
            	if(dato == "2"){
            		dato = "N";
            		filtro = " where borrado='" + dato + "' ";
            	}
            }
            else
            {
            	if(dato == "1"){
            		dato = "S";
                	filtro = filtro + " and borrado='" + dato + "' ";
            	}
            	if(dato == "2"){
            		dato = "N";
            		filtro = filtro + " and borrado='" + dato + "' ";
            	}
            }
        }
	
		//el filtro por EMPRESA
    	dato = lstEmpresa2.SelectedValue;
        if (dato != "0")
        {
    	dato = lstEmpresa2.SelectedItem.ToString();
            if (filtro == "")
            {
                filtro = " where h1.e1 like '" + dato + "' ";
            }
            else
            {
                filtro = filtro + " and h1.e1 like '" + dato + "' ";
            }
        }

        //el filtro por Ubicacion
    	dato = lstUbicacion.SelectedValue;

        if (dato != "0")
        {
    	dato = lstUbicacion.SelectedItem.ToString();
            if (filtro == "")
            {
                filtro = " where h1.n1 like '" + dato + "' ";
            }
            else
            {
                filtro = filtro + " and h1.n1 like '" + dato + "' ";
            }
        }
        
        //el filtro por Responsable
    	dato = lstResponsable.SelectedValue;
        if (dato != "0")
        {
    	dato = lstResponsable.SelectedItem.ToString();

            if (filtro == "")
            {
                filtro = " where h1.n2 like '" + dato + "' ";
            }
            else
            {
                filtro = filtro + " and h1.n2 like '" + dato + "' ";
            }
        }

		//txtExtra1F.Text = filtro;

        //FILTRADO POR ESTADO
        dato = lstEstado.SelectedValue;
        if (dato != "0")
        {
    	dato = lstEstado.SelectedItem.ToString();

            if (filtro == "")
            {
                filtro = " where h1.n3 like '" + dato + "' ";
            }
            else
            {
                filtro = filtro + " and h1.n3 like '" + dato + "' ";
            }
        }


        //txtControlF.Text = filtro;

        /*string categoria = "";
        string marca = "";
        string modelo = "";
        string serial = "";
        string control = "";
        string uso = "";
        string extra1 = "";
        string extra2 = "";
        string propietario = "";
        string eliminados = "";
        string responsable = "";
        string estado = "";
        string empresa = "";
        string ubicacion = "";

    	categoria = validar_listas(lstCategoriaF.SelectedValue);
    	marca = validar_listas(lstMarcaF.SelectedValue);
    	modelo = validar_listas(lstModeloF.SelectedValue);



    	serial = txtSerialF.Text.Trim();
    	control = txtControlF.Text.Trim();
    	uso = validar_listas(lstEstadoF.SelectedValue);


    	extra1 = txtExtra1F.Text.Trim();
    	extra2 = txtExtra2F.Text.Trim();

    	propietario = validar_listas(lstEmpresa.SelectedValue);
    	eliminados = validar_listas(lstEliminados.SelectedIndex.ToString());
		responsable = validar_listas(lstResponsable.SelectedValue);

		estado = validar_listas(lstEstado.SelectedValue);
		empresa = validar_listas(lstEmpresa2.SelectedValue);
		ubicacion = validar_listas(lstUbicacion.SelectedValue);

		buscar(categoria, marca, modelo, serial, control, uso, extra1, extra2, propietario, eliminados, responsable, estado, empresa, ubicacion);*/

		buscar(filtro);

    }
 
//-------------- CARGA DE SOLO LOS MODELOS DE UNA MARCA COMO TAL--------------------------------
    protected void lstMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstMarcaF.Items.Count > 0)
        {
            domModelo = Modelo.obtenerListadoModelos(lstMarcaF.SelectedValue);
            lstModeloF.Items.Clear();
            lstModeloF.Items.Add(new System.Web.UI.WebControls.ListItem("N/A", "0"));
            foreach (Modelo u in domModelo)
            {
                lstModeloF.Items.Add(new System.Web.UI.WebControls.ListItem(u.nombre, u.id.ToString()));
            }
        }

    }
//esto salio de los parametros de la funcion buscar
//string categoria, string marca, string modelo, string serial, string control, string uso, string extra1, string extra2, string propietario, string eliminados, string responsable, string estado, string empresa, string ubicacion
    protected void buscar(string filtro){
        String sql = "select h1.e1 AS EMPRESA, h1.n1 AS UBICACION,h1.n2 AS RESPONSABLE,articulo.serialinterno as CODIGO, categoria.nombre AS CATEGORIA,marca.nombre AS MARCA,modelo.nombre AS MODELO, serial AS SERIAL,extra1 AS EXTRA1,extra2 AS EXTRA2,IF(activo=1,'SI','NO') AS ACTIVO, ifnull(art.serialinterno,'') AS PADRE, h1.n3 AS ESTADO,borrado,emotivo,empresa.nombre  from articulo left join empresa on empresa.id = articulo.empresa_id  left join categoria on articulo.categoria_id=categoria.id left join marca on articulo.marca_id=marca.id left join modelo on articulo.modelo_id=modelo.id left join (select serialinterno,id from articulo) as art on art.id=articulo.articulo_id left join (select * from (select historial.id,articulo_id,ubicacion.nombre as n1,responsable.nombre as n2,estado.nombre as n3, fecha_inicio,fecha_fin,e1 from historial left join (select ubicacion.*,empresa.nombre as e1 from ubicacion, empresa where ubicacion.empresa_id=empresa.id) as ubicacion  on historial.ubicacion_id=ubicacion.id left join responsable on historial.responsable_id=responsable.id left join estado on historial.estado_id=estado.id order by historial.id desc) as h group by articulo_id) as h1 on h1.articulo_id=articulo.id " + filtro + " ORDER BY h1.e1,h1.n1,h1.n2,h1.n3";
        MySqlDataReader reader = null;

        

        string ruta = Server.MapPath("reporte");
        string fecha = DateTime.Now.ToString("dd-mm-yyy,H-mm-ss");
        


        ruta += fecha + ".pdf"; 

        // Creamos el documento con el tamaño de página tradicional
        Document doc = new Document(PageSize.TABLOID.Rotate());
        // Indicamos donde vamos a guardar el documento

        //se debe corregir el error de los permisos por eso no se logra guardar el pdf
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.OpenOrCreate));
        //iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
         
        // Le colocamos el título y el autor
        // **Nota: Esto no será visible en el documento
        doc.AddTitle("REPORTE");
        doc.AddCreator("EMPRESA SUMITEX");
         
        // Abrimos el archivo
        doc.Open();
        //se crea la tabla para agregar los campos de la base de datos
        PdfPTable tblPrueba = new PdfPTable(16);
        tblPrueba.WidthPercentage = 100;

        // Creamos el tipo de Font que vamos utilizar
        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        // Configuramos el título de las columnas de la tabla
        PdfPCell clEmpresa = new PdfPCell(new Phrase("EMPRESA", _standardFont));
        clEmpresa.BorderWidth = 0;
        clEmpresa.BorderWidthBottom = 0.75f;
         
        PdfPCell clUbicacion = new PdfPCell(new Phrase("UBICACION", _standardFont));
        clUbicacion.BorderWidth = 0;
        clUbicacion.BorderWidthBottom = 0.75f;
         
        PdfPCell clResponsable = new PdfPCell(new Phrase("RESPONSABLE", _standardFont));
        clResponsable.BorderWidth = 0;
        clResponsable.BorderWidthBottom = 0.75f;

        PdfPCell clCodigo = new PdfPCell(new Phrase("CODIGO", _standardFont));
        clCodigo.BorderWidth = 0;
        clCodigo.BorderWidthBottom = 0.75f;

        PdfPCell clCategoria = new PdfPCell(new Phrase("CATEGORIA", _standardFont));
        clCategoria.BorderWidth = 0;
        clCategoria.BorderWidthBottom = 0.75f;

        PdfPCell clMarca = new PdfPCell(new Phrase("MARCA", _standardFont));
        clMarca.BorderWidth = 0;
        clMarca.BorderWidthBottom = 0.75f;

        PdfPCell clModelo = new PdfPCell(new Phrase("MODELO", _standardFont));
        clModelo.BorderWidth = 0;
        clModelo.BorderWidthBottom = 0.75f;

        PdfPCell clSerial = new PdfPCell(new Phrase("SERIAL", _standardFont));
        clSerial.BorderWidth = 0;
        clSerial.BorderWidthBottom = 0.75f;

        PdfPCell clExtra1 = new PdfPCell(new Phrase("EXTRA_1", _standardFont));
        clExtra1.BorderWidth = 0;
        clExtra1.BorderWidthBottom = 0.75f;

        PdfPCell clExtra2 = new PdfPCell(new Phrase("EXTRA_2", _standardFont));
        clExtra2.BorderWidth = 0;
        clExtra2.BorderWidthBottom = 0.75f;

        PdfPCell clActivo = new PdfPCell(new Phrase("ACTIVO", _standardFont));
        clActivo.BorderWidth = 0;
        clActivo.BorderWidthBottom = 0.75f;
        
        PdfPCell clPadre = new PdfPCell(new Phrase("PADRE", _standardFont));
        clPadre.BorderWidth = 0;
        clPadre.BorderWidthBottom = 0.75f;
        
        PdfPCell clEstado = new PdfPCell(new Phrase("ESTADO", _standardFont));
        clEstado.BorderWidth = 0;
        clEstado.BorderWidthBottom = 0.75f;
        
        PdfPCell clBorrado = new PdfPCell(new Phrase("BORRADO", _standardFont));
        clBorrado.BorderWidth = 0;
        clBorrado.BorderWidthBottom = 0.75f;
        
        PdfPCell clMotivo = new PdfPCell(new Phrase("MOTIVO", _standardFont));
        clMotivo.BorderWidth = 0;
        clMotivo.BorderWidthBottom = 0.75f;


        PdfPCell clNombre = new PdfPCell(new Phrase("NOMBRE", _standardFont));
        clNombre.BorderWidth = 0;
        clNombre.BorderWidthBottom = 0.75f;



        tblPrueba.AddCell(clEmpresa);
        tblPrueba.AddCell(clUbicacion);
        tblPrueba.AddCell(clResponsable);
        tblPrueba.AddCell(clCodigo);
        tblPrueba.AddCell(clCategoria);
        tblPrueba.AddCell(clMarca);
        tblPrueba.AddCell(clModelo);
        tblPrueba.AddCell(clSerial);
        tblPrueba.AddCell(clExtra1);
        tblPrueba.AddCell(clExtra2);
        tblPrueba.AddCell(clActivo);
        tblPrueba.AddCell(clPadre);
        tblPrueba.AddCell(clEstado);
        tblPrueba.AddCell(clBorrado);
        tblPrueba.AddCell(clMotivo);
        tblPrueba.AddCell(clNombre);


        //aqui quede revisar el administrador de la base de datos el query y demas para continuar mañana en la mañana


        try
        {
            using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
            {
                conex.Open();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                
                //prueba de texto
                String texto = "";
                reader = comando.ExecuteReader();
                
                

                while (reader.Read())
                {
                    //como guardar en pdf los archivos
                    for (int i = 0; i <= 15; i++)
                    {   

                        if(i == 0){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clEmpresa = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clEmpresa.BorderWidth = 0;
                            }
                            else{
                                clEmpresa = new PdfPCell(new Phrase("NULL", _standardFont));
                                clEmpresa.BorderWidth = 0;
                            }
                        }
                        if(i == 1){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clUbicacion = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clUbicacion.BorderWidth = 0;
                            }
                            else{
                                clUbicacion = new PdfPCell(new Phrase("NULL", _standardFont));
                                clUbicacion.BorderWidth = 0;
                            }
                        }
                        if(i == 2){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clResponsable = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clResponsable.BorderWidth = 0;
                            }
                            else{
                                clResponsable = new PdfPCell(new Phrase("NULL", _standardFont));
                                clResponsable.BorderWidth = 0;
                            }
                        }
                        if(i == 3){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clCodigo = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clCodigo.BorderWidth = 0;
                            }
                            else{
                                clCodigo = new PdfPCell(new Phrase("NULL", _standardFont));
                                clCodigo.BorderWidth = 0;
                            }
                        }
                        if(i == 4){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clCategoria = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clCategoria.BorderWidth = 0;
                            }
                            else{
                                clCategoria = new PdfPCell(new Phrase("NULL", _standardFont));
                                clCategoria.BorderWidth = 0;
                            }
                        }
                        if(i == 5){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clMarca = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clMarca.BorderWidth = 0;
                            }
                            else{
                                clMarca = new PdfPCell(new Phrase("NULL", _standardFont));
                                clMarca.BorderWidth = 0;
                            }
                        }
                        if(i == 6){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clModelo = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clModelo.BorderWidth = 0;
                            }
                            else{
                                clModelo = new PdfPCell(new Phrase("NULL", _standardFont));
                                clModelo.BorderWidth = 0;
                            }
                        }
                        if(i == 7){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clSerial = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clSerial.BorderWidth = 0;
                            }
                            else{
                                clSerial = new PdfPCell(new Phrase("NULL", _standardFont));
                                clSerial.BorderWidth = 0;
                            }
                        }
                        if(i == 8){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clExtra1 = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clExtra1.BorderWidth = 0;
                            }
                            else{
                                clExtra1 = new PdfPCell(new Phrase("NULL", _standardFont));
                                clExtra1.BorderWidth = 0;
                            }
                        }
                        if(i == 9){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clExtra2 = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clExtra2.BorderWidth = 0;
                            }
                            else{
                                clExtra2 = new PdfPCell(new Phrase("NULL", _standardFont));
                                clExtra2.BorderWidth = 0;
                            }
                        }
                        if(i == 10){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clActivo = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clActivo.BorderWidth = 0;
                            }
                            else{
                                clActivo = new PdfPCell(new Phrase("NULL", _standardFont));
                                clActivo.BorderWidth = 0;
                            }
                        }
                        if(i == 11){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clPadre = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clPadre.BorderWidth = 0;
                            }
                            else{
                                clPadre = new PdfPCell(new Phrase(" ", _standardFont));
                                clPadre.BorderWidth = 0;
                            }
                        }
                        if(i == 12){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clEstado = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clEstado.BorderWidth = 0;
                            }
                            else{
                                clEstado = new PdfPCell(new Phrase("NULL", _standardFont));
                                clEstado.BorderWidth = 0;
                            }
                        }
                        if(i == 13){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clBorrado = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clBorrado.BorderWidth = 0;
                            }
                            else{
                                clBorrado = new PdfPCell(new Phrase("NULL", _standardFont));
                                clBorrado.BorderWidth = 0;
                            }
                        }
                        if(i == 14){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clMotivo = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clMotivo.BorderWidth = 0;
                            }
                            else{
                                clMotivo = new PdfPCell(new Phrase("NULL", _standardFont));
                                clMotivo.BorderWidth = 0;
                            }
                        }
                        if(i == 15){
                            if(!reader.IsDBNull(i)){
                                // Configuramos el título de las columnas de la tabla
                                clNombre = new PdfPCell(new Phrase(reader.GetString(i), _standardFont));
                                clNombre.BorderWidth = 0;
                            }
                            else{
                                clNombre = new PdfPCell(new Phrase("NULL", _standardFont));
                                clNombre.BorderWidth = 0;
                            }
                        }
                       
                        if(!reader.IsDBNull(i))
                        {
                            //texto += reader.GetString(i);

                        }
                    }
                    
                    tblPrueba.AddCell(clEmpresa);
                    tblPrueba.AddCell(clUbicacion);
                    tblPrueba.AddCell(clResponsable);
                    tblPrueba.AddCell(clCodigo);
                    tblPrueba.AddCell(clCategoria);
                    tblPrueba.AddCell(clMarca);
                    tblPrueba.AddCell(clModelo);
                    tblPrueba.AddCell(clSerial);
                    tblPrueba.AddCell(clExtra1);
                    tblPrueba.AddCell(clExtra2);
                    tblPrueba.AddCell(clActivo);
                    tblPrueba.AddCell(clPadre);
                    tblPrueba.AddCell(clEstado);
                    tblPrueba.AddCell(clBorrado);
                    tblPrueba.AddCell(clMotivo);
                    tblPrueba.AddCell(clNombre);

                    
                    

                    


                }

                //prueba
                Paragraph titulo = new Paragraph("REPORTE");
                titulo.Alignment = Element.ALIGN_JUSTIFIED;


                doc.Add(titulo);
                doc.Add(tblPrueba);
            }
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();

        }

        doc.Close();
       // writer.Close();

       // Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename="+"REPORTE "+fecha+".pdf");
        //Response.Write(doc);
        //Response.End();

        MostrarArchivo(ruta);
       
    }

    private void MostrarArchivo(string ruta)
    {
        WebClient client = new WebClient();
        Byte[] buffer = client.DownloadData(ruta);

        if (buffer != null)
        {
            Response.ContentType = "application/pdf";
            Response.WriteFile(ruta);
            Response.End();
            //Response.AddHeader("content-length", buffer.Length.ToString());
            //Response.BinaryWrite(buffer);
        }

    }

}