
$(document).ready(function() {
	
	
    $("#save").click(function() {
            $("#submit").click();
    });

	
		$( "select" )
  .change(function() {
    var str = "";
    $( "select option:selected" ).each(function() {
		if($( this ).text() == "Furniture")
			{ 
				$("#attribute_1").hide();
				$("#attribute_2").show();
				$("#attribute_3").hide();
				
				$("#size").attr('required',false);
				$("#length").attr('required',true);
				$("#width").attr('required',true);
				$("#height").attr('required',true);
				$("#weight").attr('required',false);
				str = '" Please, provide dimensions - for the ';
			}
		if($( this ).text() == "Book")
			{ 
				$("#attribute_1").hide();
				$("#attribute_3").show();
				$("#attribute_2").hide();
				
				$("#size").attr('required',false);
				$("#length").attr('required',false);
				$("#width").attr('required',false);
				$("#height").attr('required',false);
				$("#weight").attr('required',true);
				str = '" Please, provide weight - for the ';
			}
		if($( this ).text() == "DVD-disc")
			{ 
				$("#attribute_2").hide();
				$("#attribute_1").show();
				$("#attribute_3").hide();
				
				$("#size").attr('required',true);
				$("#length").attr('required',false);
				$("#width").attr('required',false);
				$("#height").attr('required',false);
				$("#weight").attr('required',false);
				str = '" Please, provide size - for the ';
			}
      str += $( this ).text() + " ";
    });
    $( "#para" ).text( str + '"');
  })
  .trigger( "change" );
});