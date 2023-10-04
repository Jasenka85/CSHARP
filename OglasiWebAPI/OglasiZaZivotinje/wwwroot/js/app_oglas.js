$(document).foundation();


$('#dodaj').click(function(){

   
    const oglas = { 
        ime: $('#ime').val(), 
        prezime: $('#prezime').val(), 
        email: $('#email').val(), 
        mobitel: $('#mobitel').val(), 
        grad: $('#grad').val(), 
        kategorija: $('#kategorija').val(),
        naslov: $('#naslov').val(),
        opis: $('#opis').val(),
        vrsta_zivotinje: $('#vrstazivotinje').val(),
        ime_zivotinje: $('#imezivotinje').val(),
        spol_zivotinje: $('#spolzivotinje').val(),
        dob_zivotinje: $('#dobzivotinje').val(),
        kastriran: $('#kastriran').val()
    };

    $.ajax('https://jasenka85-001-site1.etempurl.com/api/v1/Oglasi/CijeliOglas', {
        type: 'POST',  
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(oglas), 
        
        success: function (oglas, status, xhr) {
            alert('Oglas uspješno objavljen!');
              
        },
        error: function (e) {
            const greske = e.responseJSON.errors;
            let poruka='';
            for(svojstvo in greske){
                //console.log(varijabla);
                //console.log(`${g[varijabla]}`);
                poruka += `${greske[svojstvo]}` + '\n';
            }
            alert(poruka);
        }
        
    });

    
    setTimeout(function(){location.reload();}, 5000);  


    return false;
});





