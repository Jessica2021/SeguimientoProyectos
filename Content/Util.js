function GET_DATA(url, data) {
    return new $.ajax({
        url: url,
        data: data,
        type: 'POST'
    })
}

//onkeypress = "return OnValidateCharacter(event)"

function OnValidateCharacter(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /[A-Za-z ÁÉÍÓÚáéíóú´]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}

function OnValidateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}


function ValidateEmail(NameEmail) {
    //const $result = $("#result");
    const email = $(`#${NameEmail}`).val();
    //$result.text("");
    if (email == "") return true;

    if (OnValidateEmail(email)) {
        return true;
        //$result.text(email + " is valid :)");
        //$result.css("color", "green");
    } else {
        //$result.text(email + " is not valid :(");
        //$result.css("color", "red");
    }
    return false;
}

function KeyUp_FormatoModena(n) {
    n = String(n).replace(/\D/g, "");
    return n === '' ? n : Number(n).toLocaleString();
}

function FormatoModena(numero) {
    try {
        numero = numero.toFixed();
    } catch (e) { }
    var s = new Intl.NumberFormat().format(numero);

    return '$ ' + s;
}

function GenerarTabla(id,array) {

    // Setup - add a text input to each footer cell
    $('.filtro').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text"  placeholder="Buscar ' + title + '" style="width:100%;    font-weight: 100;" />');
    });

    // DataTable
    var table = $('#' + id).DataTable({
        "autoWidth": false,
        "lengthMenu": [[20, 25, 50, 100, -1], [20, 25, 50,100, "Todo"]],
        "displayStart": 25,
        "processing": true,
        "ordering": false,
        "dom": 'B<"pull-right"f><t><"pull-right"p><"pull-left"l>',
        "initComplete": function () {
            $('.buttons-excel').appendTo('.nav-botones');
        },
        "language":
            {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst":  "   Primero    ",
                    "sLast": "   Último   ",
                    "sNext": "   Siguiente   ",
                    "sPrevious": "   Anterior   "
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                }
            },
        "buttons": [
        {
            extend: 'excelHtml5',
            text: '<i class="glyphicon glyphicon-export"></i> Exportar',
            titleAttr: 'Exportar Excel',
            className: 'btn-exportar-excel',
            exportOptions: {
                columns: array
            }
        }
        ]
    });


    // Apply the search
    table.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

}

function CargarModal(url) {
    ModalProcesandoShow();
    var urlcode = encodeURI(url);
    GET_DATA(urlcode, {}).done(function (data) {
        $("#dialog").html(data);
    })
    .fail(function (jqXHR, textStatus) {
        console.log('error')
    });
}

//USe plugin sweet alert 2
function MensajesPangea(title = 'Información', text = '', type = 'info', confirmButtonText = 'OK') {
    swal({
        title: title,
        text: text,
        type: type,
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: confirmButtonText,
        //cancelButtonText: 'Cancelar'
    }).then(function () {
    });
}

$('g:has(> g[stroke="#3cabff"])').hide();
