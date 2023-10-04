$(document).foundation();

let podaci=[];
let trenutniKorisnik=0;

function ucitajKorisnike(){
    $.ajax('https://localhost:7110/api/v1/Korisnik',   // request url
    {
        success: function (data, status, xhr) {// success callback function
           podaci = data;
           $('#podaci').html('');
           for(let i=0;i<data.length;i++){
            $('#podaci').append('<li>' + data[i].ime + ' ' + data[i].prezime + 
            ' <a class="brisi" href="#" id="s_' + data[i].sifra + '"><span class="badge alert">X</span></a>' + 
            ' <a class="promjena" href="#" id="p_' + data[i].sifra + '"><span class="badge primary">P</span></a>' + 
            '</li>');
           }
           definirajDogadaje();
    }
});
}

ucitajKorisnike();


function definirajDogadaje(){
    $('.brisi').off('click');
    $('.brisi').click(function(){

        const element = $(this);
        const sifra = element.attr('id').split('_')[1];
        console.log('Brišem: ' + sifra);

        $.ajax('https://localhost:7110/api/v1/Korisnik/' + sifra, {
        type: 'DELETE', 
        success: function (data, status, xhr) {
           element.parent().remove();
        },
        error: function (e) {
                console.log(e);
                alert(e.responseJSON);
        }
    });
        return false;
    });


    $('.promjena').off('click');
    $('.promjena').click(function(){
        const element = $(this);
        
         const sifra = element.attr('id').split('_')[1];
        
        trenutniKorisnik = sifra;
        for(let i=0;i<podaci.length;i++){
            const s = podaci[i];
            if(s.sifra==sifra){
                $('#ime').val(s.ime);
                $('#prezime').val(s.prezime);
                $('#email').val(s.email);
                $('#mobitel').val(s.mobitel);
                $('#grad').val(s.grad);
                break;
            }
        }

        return false;
    });
}


$('#dodaj').click(function(){

    const korisnik = { 
        ime: $('#ime').val(), 
        prezime: $('#prezime').val(), 
        email: $('#email').val(), 
        mobitel: $('#mobitel').val(), 
        grad: $('#grad').val(), 
    };

    $.ajax('https://localhost:7110/api/v1/Korisnik', {
        type: 'POST',  
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(korisnik),  
        success: function (korisnik, status, xhr) {
            console.log(podaci);
            podaci.push(korisnik);
            $('#podaci').append('<li>' + $('#ime').val() + ' ' + $('#prezime').val() +
            ' <a class="brisi" href="#" id="s_' + korisnik.sifra + '"><span class="badge alert">X</span></a>' + 
            ' <a class="promjena" href="#" id="p_' + korisnik.sifra + '"><span class="badge primary">P</span></a>' + 
            '</li>');
            definirajDogadaje();
        },
        error: function (e) {
                const greske = e.responseJSON.errors;
                let poruka='';
                for(svojstvo in greske){
                    poruka += `${greske[svojstvo]}` + '\n';
                }
                alert(poruka);

        }
    });

    return false;
});



$('#promjeni').click(function(){

    if(trenutniKorisnik==0){
        alert('Prvo odaberite korisnika za promjenu');
        return;
    }


    const korisnik = { 
        ime: $('#ime').val(), 
        prezime: $('#prezime').val(), 
        email: $('#email').val(), 
        mobitel: $('#mobitel').val(), 
        grad: $('#grad').val(), 
    };

    

    $.ajax('https://localhost:7110/api/v1/Korisnik/' + trenutniKorisnik, {
        type: 'PUT',  
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(korisnik),  
        success: function (korisnik, status, xhr) {
           ucitajPodatke();
        },
        error: function (e) {
                const greske = e.responseJSON.errors;
                let poruka='';
                for(svojstvo in greske){
                    poruka += `${greske[svojstvo]}` + '\n';
                }
                alert(poruka);

        }
    });

    return false;
});

