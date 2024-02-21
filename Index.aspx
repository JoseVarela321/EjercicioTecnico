<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EjercicioTecnico_02.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Tiendita</title>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />

    <!-- DataTables CSS -->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.min.css" />

    <!-- Awesomplete CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/awesomplete/1.1.2/awesomplete.min.css" crossorigin="anonymous" />

</head>
<body class="bg-light">

    <div class="container mt-5">
        <h2 class="mb-4">Productos</h2>
        <div class="row mb-4">
            <div class="col-md-6">
                <input type="text" class="form-control form-control-sm" id="SearchItem" value="" placeholder="Buscar Producto(presione enter para buscar)..." />
            </div>
            <div class="col-md-6">
                <button class="btn btn-primary" data-toggle="modal" data-target="#AddItemModal">Agregar Producto</button>
            </div>
        </div>
        
        <div class="table-responsive">
            <table id="TableData" class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Id Producto</th>
                        <th>Código de Barras</th>
                        <th>Descripción</th>
                        <th>Marca</th>
                        <th>Categoría ID</th>
                        <th>Precio</th>
                        <th>Editar</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody id="BodyTable">
                    <!-- Aquí se cargarán los datos -->
                </tbody>
            </table>
        </div>


        <!-- Modal Agregar Producto -->
      <div class="modal fade" id="AddItemModal" data-backdrop="static" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Nuevo Producto</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div class="form-group">
                                <label for="CodBarras">Codigo de Barras</label>
                                <input type="text" onkeyup="TypeAhead(this.value)" class="form-control" placeholder="" id="CodBarras">
                            </div>

                        </div>
                        <div class="form-group">
                            <label for="">Descripcion</label>
                            <input type="text" class="form-control" placeholder="" id="Desc">
                        </div>
                        <div class="form-group">
                            <label for="">Marca</label>
                            <input type="text" class="form-control" placeholder="" id="Marca">
                        </div>
                        <div class="form-group">
                            <label for="">Categoria</label>
                            <select id="categoria" class="form-control input-xs" style="font-weight: bold" tabindex="5"></select>
                        </div>
                        <div class="form-group">
                            <label for="">Precio</label>
                            <input type="text" class="form-control" placeholder="" id="Precio">
                        </div>
                        <button class="btn btn-success" id="" onclick="SaveNewItem();">Guardar</button>
                    </div>
                    <div class="modal-footer">
                        <button id="" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>



        <!-- Modal Editar Producto -->
         <div class="modal fade" id="EditItemModal" data-backdrop="static" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">EDITAR DATOS</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="DetalleID">Id Producto</label>
                            <input type="number" class="form-control" placeholder="" id="idproducto" readonly>
                        </div>
                        <div class="form-group">
                            <label for="CodBarras">Codigo de Barras</label>
                            <input type="text" class="form-control" placeholder="" id="codBarras">
                        </div>
                        <div class="form-group">
                            <label for="">Descripcion</label>
                            <input type="text" class="form-control" placeholder="" id="descripcion">
                        </div>
                        <div class="form-group">
                            <label for="">Marca</label>
                            <input type="text" class="form-control" placeholder="" id="marca">
                        </div>
                        <div class="form-group">
                            <label for="">Categoria</label>
                           <select id="catEdit" class="form-control input-xs" style="font-weight: bold" tabindex="5"></select>
                        </div>
                        <div class="form-group">
                            <label for="">Precio</label>
                            <input type="text" class="form-control" placeholder="" id="precio">
                        </div>
                        <button class="btn btn-success" id="btn-Guardar" onclick="SaveChanges();">Guardar</button>
                    </div>
                    <div class="modal-footer">
                        <button id="btnCloseModal" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal de Éxito -->
        <div class="modal fade" id="ModalSuccess" tabindex="-1" role="dialog" aria-labelledby="modalSuccessLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content bg-success text-white">
                    <div class="modal-header border-0">
                        <h5 class="modal-title" id="modalSuccessLabel">¡Operación exitosa!</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>Se han guardado los cambios correctamente.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- JavaScript -->
    <script src="https://code.jquery.com/jquery-3.6.0.js" integrity="sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk=" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/awesomplete/1.1.2/awesomplete.min.js"></script>
    <script src="JS/index.js"></script>
</body>
</html>
