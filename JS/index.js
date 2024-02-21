

var txtCodBarras = $('#CodBarras');
var txtDesc = $('#Desc');
var txtMarca = $('#Marca');
var slcategoria = $('#categoria');
var SearchItem = $('#SearchItem');

function LoadData()
{
    $.ajax({
        type: 'POST',
        async: false,
        processData: false,
        url: 'Index.aspx/LoadData',
        data: '',
        contentType: "application/json",
        dataType: 'json',
        //ACCION QUE SE HARA SI LA PETICION ES CORRECTA
        success: function (data) {

            var reponsedata = JSON.parse(data.d)
            $("#TableData").DataTable().destroy();
            //AQUI SACAMOS EL VALOR CON LA VARIABLE DATA ARRAY  
            var dataArray = reponsedata.reponse;
            var table = $("#TableData").DataTable({
                "rowCallback": function (row, data) {
                },
                paging: false,
                responsive: true,
                searching: true,
                ordering: false,
                info: false,
                deferRender: true,
                'sDom': 't',
                'scrollX': false,
                data: dataArray,
                //CARGAMOS LOS DATOS A LA TABLA DE HTML
                columns: [
                    { data: "idProducto" },
                    { data: "codigoBarra" },
                    { data: "descripcion" },
                    { data: "marca" },
                    { data: "idCategoria" },
                    { data: "precio" },
                    {
                        title: "", data: null, render: function (data, type, row) {
                            return "<button  onclick = ModifyItem(" + row["idProducto"] + "); class = 'editar btn btn-primary'>Editar</button>";
                        }
                    },
                    {
                        title: "", data: null, render: function (data, type, row) {
                            return "<button  onclick = DeleteItem(" + row["idProducto"] + "); class = 'delete btn btn-danger'>Eliminar</button>";
                        }
                    }
                   
                ]

            });

        },
        //ACCION QUE SE HARA SI LA PETICION ES INCORRECTA
        error: function (data) {
            alert("Error");

        }
    });


}


function ModifyItem(idItem)
{

    $.ajax({
        type: 'POST',
        async: false,
        processData: false,
        url: 'Index.aspx/SearchItem',
        data: JSON.stringify({

            'idItem': idItem
        }),
        contentType: "application/json",
        dataType: 'json',
        //ACCION QUE SE HARA SI LA PETICION ES CORRECTA
        success: function (data) {

            var reponsedata = JSON.parse(data.d)
            $("#TableData").DataTable().destroy();
            //AQUI SACAMOS EL VALOR CON LA VARIABLE DATA ARRAY  
            var dataArray = reponsedata.reponse;
          

            $('#idproducto').val(dataArray.idProducto);
            $('#codBarras').val(dataArray.codigoBarra);
            $('#descripcion').val(dataArray.descripcion);
            $('#marca').val(dataArray.marca);
            $('#categoriaid').val(dataArray.idCategoria);
            $('#precio').val(dataArray.precio);

            //ABRIMOS EL MODAL PARA CARGAR LOS DATOS.
            $('#EditItemModal').modal('show');

        },
        //ACCION QUE SE HARA SI LA PETICION ES INCORRECTA
        error: function (data) {
            alert("Error");

        }
    });
}

function DeleteItem(idItem) {
    // Mostrar mensaje de confirmación
    var confirmDelete = confirm("¿Estás seguro de que deseas eliminar este Producto?");
    if (!confirmDelete) {
        return; // Cancelar la operación si el usuario no confirma
    }

    $.ajax({
        type: 'POST',
        async: false,
        processData: false,
        url: 'Index.aspx/DeleteItem',
        data: JSON.stringify({
            'idItem': idItem
        }),
        contentType: "application/json",
        dataType: 'json',
        success: function (data) {
            // Mostrar mensaje de éxito si la petición es correcta
            $('#ModalSuccess').modal('show').fadeIn();
            setTimeout(function () {
                $('#ModalSuccess').modal('hide');
            }, 2300);
            LoadData();
        },
        error: function (data) {
            // Mostrar mensaje de error si la petición es incorrecta
            alert("Error");
        }
    });
}

function SaveChanges() {
    var idItem = $('#idproducto').val();
    var codBarras = $('#codBarras').val();
    var descripcion = $('#descripcion').val();
    var marca = $('#marca').val();
    var categoria = $('#catEdit').val();
    var precio = $('#precio').val();

    // Validaciones
    if (!codBarras || !descripcion || !marca || !categoria || !precio) {
        alert("Todos los campos son obligatorios");
        return;
    }

    if (isNaN(precio) || parseFloat(precio) <= 0) {
        alert("El precio debe ser un número mayor que cero");
        return;
    }

    // Realizar la petición AJAX si las validaciones son exitosas
    $.ajax({
        type: 'POST',
        async: false,
        processData: false,
        url: 'Index.aspx/SaveChanges',
        data: JSON.stringify({
            'idItem': idItem,
            'codBarras': codBarras,
            'descripcion': descripcion,
            'marca': marca,
            'categoria': categoria,
            'precio': precio
        }),
        contentType: "application/json",
        dataType: 'json',
        //ACCION QUE SE HARA SI LA PETICION ES CORRECTA
        success: function (data) {
            $('#ModalSuccess').modal('show').fadeIn();
            //CERRAMOS EL MODAL AUTOMATICAMENTE
            setTimeout(function () {
                $('#ModalSuccess').modal('hide');
            }, 2300);

            //CARGAMOS DE NUEVO LA TABLA CON LA INFO ACTUALIZADA
            LoadData();
            $('#EditItemModal').modal('hide');

        },
        //ACCION QUE SE HARA SI LA PETICION ES INCORRECTA
        error: function (data) {
            alert("Error");

        }
    });
}



function SaveNewItem() {
    var codBarras = $('#CodBarras').val();
    var descripcion = $('#Desc').val();
    var marca = $('#Marca').val();
    var categoria = $('#categoria').val();
    var precio = $('#Precio').val();

    // Validación de campos obligatorios
    if (!codBarras || !descripcion || !marca || !categoria || !precio) {
        alert("Por favor, complete todos los campos.");
        return;
    }

    // Validación de formato de precio
    if (isNaN(parseFloat(precio))  || parseFloat(precio) <= 0) {
        alert("Ingrese un precio válido.");
        return;
    }

    // Realizar la petición AJAX solo si todas las validaciones son correctas
    $.ajax({
        type: 'POST',
        async: false,
        processData: false,
        url: 'Index.aspx/SaveNewItem',
        data: JSON.stringify({
            'codBarras': codBarras,
            'descripcion': descripcion,
            'marca': marca,
            'categoria': categoria,
            'precio': precio
        }),
        contentType: "application/json",
        dataType: 'json',
        //ACCION QUE SE HARA SI LA PETICION ES CORRECTA
        success: function (data) {
            $('#ModalSuccess').modal('show').fadeIn();
            //CERRAMOS EL MODAL AUTOMATICAMENTE
            setTimeout(function () {
                $('#ModalSuccess').modal('hide');
            }, 2300);

            //CARGAMOS DE NUEVO LA TABLA CON LA INFO ACTUALIZADA
            LoadData();
            $('#AddItemModal').modal('hide');

        },
        //ACCION QUE SE HARA SI LA PETICION ES INCORRECTA
        error: function (data) {
            alert("Error");

        }
    });
}


function LoadCategories() {

    $.ajax({
        type: 'POST',
        async: false,
        processData: false,
        url: 'Index.aspx/LoadCategories',
        data: '',
        contentType: "application/json",
        dataType: 'json',
        //ACCION QUE SE HARA SI LA PETICION ES CORRECTA
        success: function (data) {

            if (!data.d.includes("ERROR")) {
                var Categoria = jQuery.parseJSON(data.d);
                for (var i = 0; i < Categoria.length; i++) {
                    var option = "<option value='" + Categoria[i].CategoriaId + "'>" + Categoria[i].DescripcionCategoria + "</option>"
                    $('#categoria').append(option);
                    $('#catEdit').append(option);
                    
                }
            }
        },
        //ACCION QUE SE HARA SI LA PETICION ES INCORRECTA
        error: function (data) {
            alert("Error");

        }
    });

}

function TypeAhead(query) {
    if (query.length > 2) {
        $.ajax({
            type: 'POST',
            async: false,
            url: 'Index.aspx/GetItemsTypeHead',
            data: JSON.stringify({
                'Query': query
            }),
            contentType: "application/json",
            dataType: 'json',
            success: function (data) {
                var Items = [];
                var aux = jQuery.parseJSON(data.d);
                productsArr = [];
                $.each(aux, function (index, aux) {
                    Items.push({ label: aux.Codigobarra, desc: aux.Descripcion, marca: aux.Marca });
                });

                if (Items.length >= 0) {
                    var list = Items.map(function (item) { return item.label; });
                    var input = document.getElementById('CodBarras');
                    if (input) {
                        var awesomeplete = new Awesomplete(input, {
                            list: list,
                            minChars: 2, // Mínimo de 2 caracteres antes de mostrar sugerencias
                            autoFirst: true
                        });

                        // Mantener la lista desplegada mientras el campo de entrada tiene el foco
                        input.addEventListener('focus', function () {
                            awesomeplete.open();
                        });

                        // Manejar el evento select para manejar la selección de elementos
                        input.addEventListener('awesomplete-select', function (event) {
                            var selectedItem = event.text.value; // El valor seleccionado está en event.text.value
                            var selectedItemData = Items.find(function (item) { return item.label === selectedItem; });
                            var txtDesc = document.getElementById('Desc');
                            var txtMarca = document.getElementById('Marca');
                            if (selectedItemData) {
                                if (txtDesc) {
                                    txtDesc.value = selectedItemData.desc;
                                }
                                if (txtMarca) {
                                    txtMarca.value = selectedItemData.marca;
                                }
                            }
                        });

                        // Manejar el evento blur para cerrar la lista cuando el campo de entrada pierde el foco
                        input.addEventListener('blur', function () {
                            awesomeplete.close();
                        });
                    }
                }



            },
        });
    }
}


SearchItem.on('keyup', function (e) {
    var charCode = (e.which) ? e.which : e.keyCode;
    if (charCode == 13) {
        $('#TableData').DataTable().search(this.value).draw();
    }
    else if (this.value == '') {
        $('#TableData').DataTable().search(this.value).draw();
    }

})


$(document).ready(function () {
    LoadData();
    LoadCategories();
});