$(document).ready(function(){
		$( ".datepicker" ).datepicker({
			changeMonth: true,
			changeYear: true,
			firstDay: 1,
			maxDate: new Date()
		
		});	 
		
		$( ".datepickers" ).datepicker({
		    dateFormat: 'dd/mm/yy',
			changeMonth: true,
			changeYear: true,
			firstDay: 1,
			maxDate: new Date()
		
		});	
		
		$( ".datepickera" ).datepicker({
		    dateFormat: 'dd/mm/yy',
			changeMonth: true,
			changeYear: true,
			firstDay: 1,
			minDate: new Date()
		
		});	
		
	/**Se encarga de habilitar o desabilitar la lista de empresas en caso que este registrando una empresa externa**/
		
    $("#ctl00_ContentPlaceHolder1_txtTipo").click(function(event){	
		 if($(this).is(":checked")) {
		    $("#ctl00_ContentPlaceHolder1_lstEmpresa").attr('disabled',false);
		}else{
		    $("#ctl00_ContentPlaceHolder1_lstEmpresa").attr('disabled',true);
		}
	 });
    //--------------------------------------------------------------------------------------------//	
	
	
	/**Se encarga de habilitar o desabilitar la obs**/
		
    $("#ctl00_ContentPlaceHolder1_chkActivo").click(function(event){	
        $("#ctl00_ContentPlaceHolder1_txtObs").val("");
		 if($(this).is(":checked")) {
		    $("#ctl00_ContentPlaceHolder1_txtObs").attr('disabled',true);
		}else{
		    $("#ctl00_ContentPlaceHolder1_txtObs").attr('disabled',false);	   
		}
	 });
	 
	$(".pruebac").click(function(){
	    var re=confirm("¿Desea ralizar el traslado de manera automatica?");
	    if(re==true){
	        $("#ctl00_ContentPlaceHolder1_trasladarA").val("S");

	    }else{
	       $("#ctl00_ContentPlaceHolder1_trasladarA").val("N");
	    }
	    return true;
	});
	 

	$( ".tooltipExtra" ).tooltip();	
	
	
//------------------------------------------------------------------------------------------------//
	
    $("a#fancyBoxLink").fancybox({
        'href'   : '#myDivID',
        'titleShow'  : false,
        'transitionIn'  : 'elastic',
        'transitionOut' : 'elastic'
    });	
    
    $("a#fancyBoxLinkArticulo").fancybox({
        'href'   : '#myDivIDArticulo',
        'titleShow'  : false,
        'transitionIn'  : 'elastic',
        'transitionOut' : 'elastic'
    });	
    
    $("a#fancyBoxLinkCategoria").fancybox({
        'href'   : '#myDivIDCategoria',
        'titleShow'  : false,
        'transitionIn'  : 'elastic',
        'transitionOut' : 'elastic'
    });	
    
    $("a#fancyBoxLinkUbicacion").fancybox({
        'href'   : '#myDivIDUbicacion',
        'titleShow'  : false,
        'transitionIn'  : 'elastic',
        'transitionOut' : 'elastic'
    });
    
 //----------------------------------------------------------------------------------------------//
	
	$(".filtrar tr:has(td)").each(function() { 
	
            var t = $(this).text().toLowerCase();  
            if (t=="")
                return;
                
                $("<td class='indexColumn'></td>") 
                .hide().text(t).appendTo(this); 
            });
            
            //Agregar el comportamiento al texto (se selecciona por el ID) 
            $("#ctl00_ContentPlaceHolder1_BuscarArticulo1_txtBuscar").keyup(function() { 
                var s = $(this).val().toLowerCase().split(" "); 
                $(".filtrar tr:hidden").show(); 
                $.each(s, function() { 
                     $(".filtrar tr:visible .indexColumn:not(:contains('" 
                     + this + "'))").parent().hide(); 
                });  
            });      
            

 });
 
 
 	
 
	
	
	