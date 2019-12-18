var data = '[{	"id": "100","name": "Gökhan Türkmen","tckn": "29292929292","phone": "05111111111", "unit":"Ticari Krediler","position":"Team Leader", "birthPlace":"Kayseri","itemNo": "12"},'+
			'{"id": "101","name": "Selim Yıldız","tckn": "29292929293","phone": "05111111114", "unit":"Ticari Krediler","position":"Developer","birthPlace":"İzmir","itemNo": "14"},'+
			'{"id": "102","name": "Orhan Işık","tckn": "29292929294","phone": "05111111115", "unit":"Ticari Krediler","position":"Developer","birthPlace":"Manisa","itemNo": "15"},'+
			'{"id": "200","name": "Fatma Turunç","tckn": "29292929291","phone": "05111111122", "unit":"Bankamatik islem","position":"Team Leader","birthPlace":"Adana",	"itemNo": "16"},'+
			'{"id": "201","name": "Hasan Özsoy","tckn": "29292929295","phone": "05111111126", "unit":"Bankamatik islem","position":"Developer","birthPlace":"Aydın",	"itemNo": "17"},'+
			'{"id": "202","name": "Özge Fırat","tckn": "29292929296","phone": "05111111127", "unit":"Bankamatik islem","position":"Developer","birthPlace":"Balıkesir","itemNo": "18"	}]';

response = $.parseJSON(data);
var counter = 0;
$(document).ready(function(){
 $.each(response, function(i, item) {
    	
    	if(item.position == "Team Leader")
	    {
            counter++;
			var $tr = $('<tr >').append(
            $('<td> ').html('<a href="#collapse'+counter+'" name="'+counter+'" class="oncol " id="collapse"> + </a> ' + item.name ),
            $('<td>').html('<a href="#ex1" rel="modal:open" id="popupOpener">'+ item.tckn +'</a>'),
            $('<td>').html('<a href="tel:+9'+item.phone+'">'+item.phone+'</a>')
        	);    		  		        
		}
        else 
        {    	
    		var $tr = $('<tr id="element'+counter+'" class="secure'+counter+'" name="secure"  style="display:none">').append(	
            $('<td>').html(item.name),
            $('<td>').html('<a href="#ex1" rel="modal:open" id="popupOpener">'+ item.tckn +'</a>'),
            $('<td>').html('<a href="tel:+9'+item.phone+'">'+item.phone+'</a>')
        	);        	        	               	
        }
			$('#records_table tbody').append($tr);
							

    });

$(document).on("click", "#collapse", function(f){

    var name = $(this).attr("name");
    var list = document.getElementsByClassName("secure" + name);
    
    var item = $("#element"+name).attr("style");
    console.log(item); 
    if( item == "display:none")
    {
        $(this).html("-");
        for (var i = 0; i < list.length; i++) 
        {
         list[i].setAttribute("style", "display: table-row");
        }
    }
    else
    {
        $(this).html("+");
        for (var i = 0; i < list.length; i++) 
        {
         list[i].setAttribute("style", "display:none");
        }
    }
});
 

$(document).on("click", "#popupOpener", function(f){

 	var tc = $(this).text() ;
 	//console.log(response);

    $.each(response, function(i, item) {
        if (item.tckn === tc ) {
            $('#popupName').html(item.name);
            $('#popupTc').html(item.tckn);
            $('#popupPlace').html(item.itemNo);
            $('#popupBirth').html(item.birthPlace);
            return;
        }
});

	//$('#popupName').html('changed valuee');
	
});


});

