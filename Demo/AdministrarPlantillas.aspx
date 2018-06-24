<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="AdministrarPlantillas.aspx.cs" Inherits="Demo.AdministrarPlantillas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="Css/open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />--%>


    <style>
        .btnHermes {
            background-color: orangered;
            border: none;
            color: white;
            padding: 10px 22px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-family: Arial, Helvetica, sans-serif;
            box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
        }

        .btnHermesNegro {
            background-color: #3c454f;
            border: none;
            color: white;
            padding: 10px 22px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-family: Arial, Helvetica, sans-serif;
            /* border-radius: 12px; */
            box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
        }

        table.table-style-one {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #3A3A3A;
            border-collapse: collapse;
        }

            table.table-style-one th {
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #ffffff;
                background-color: #3c454f;
                color: white;
                text-align: center;
            }

            table.table-style-one td {
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #3A3A3A;
                background-color: #ffffff;
            }

        .alert-top {
            top: 50px;
            width: 100%;
            display: none;
            text-align: center;
            margin-bottom: 0;
            padding: 10px;
        }

        .contenedo-planilla {
            width: 100%;
            height: 320px;
            border: 1px solid #D5CFDF;
            border-radius: 6px;
            overflow: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-header">
        <div class="container-fluid">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color: white"><b>Administrar Plantilla</b> </a>
                </div>
                <div class="panel-body">
                    <div class="form-group row">
                        <div class="col-md-8">
                            <div class="form-group row">
                                <div class="col-lg-12">
                                    <div id="alert-info" class="alert alert-info alert-top" role="alert">
                                        <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                        <span class="alert-msg"></span>
                                    </div>
                                    <div id="alert-warn" class="alert alert-warning alert-top" role="alert">
                                        <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                        <span class="alert-msg"></span>
                                    </div>
                                    <div id="alert-danger" class="alert alert-danger alert-top" role="alert">
                                        <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                        <span class="alert-msg"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-12 col-lg-12">
                                    <label for="inputEmail3" class="control-label">Descripción:</label>
                                    <input type="text" class="form-control" id="txt_descripcion" placeholder="Ingrese descripción">
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-12 col-lg-12">
                                    <label>Seleccione Archivo - HTML</label>
                                    <div class="input-group">
                                        <input type="text" readonly="readonly" id="file_path_input08" class="form-control" placeholder="Adjunte Archivo">
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-success" style="color: white" type="button" id="file_browser_input08">
                                                <i class="fa fa-search"></i>Examinar</button>
                                        </span>
                                    </div>
                                    <input type="file" class="hidden" id="input08" name="input08">
                                    <%--<label>Seleccione Archivo - HTML</label>
                                    <input type="file" id="input08" onchange="checkfile(this);">--%>
                                </div>
                            </div>
                            <%-- <div class="form-group row">
                                <div class="col-sm-12 col-lg-8">
                                    <label>Seleccione Archivo - Imagen</label>
                                    <input type="file" id="input09" onchange="checkfileImgen(this);">
                                </div>
                                <div class="col-sm-12 col-lg-4" style="margin-top: 23px;">
                                    <button type="button" class="btnHermes" style="width: 100%;" onclick="AgregarImagen();">
                                        Agregar Imagen
                                    </button>
                                </div>
                            </div>--%>

                            <div class="form-group row">
                                <div class="col-sm-12 col-lg-8">
                                    <label>Seleccione Archivo - Imagen</label>
                                    <div class="input-group">
                                        <input type="text" readonly="readonly" id="file_path" class="form-control" placeholder="Adjunte Archivo">
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-success" style="color: white" type="button" id="file_browser">
                                                <i class="fa fa-search"></i>Examinar</button>
                                        </span>
                                    </div>
                                    <input type="file" class="hidden" id="input09" name="input09">
                                </div>
                                <div class="col-sm-12 col-lg-4" style="margin-top: 23px;">
                                    <button type="button" class="btnHermes" style="width: 100%;" onclick="AgregarImagen();">
                                        Agregar Imagen
                                    </button>
                                </div>
                            </div>


                            <div class="form-group row">
                                <div class="col-sm-12 col-lg-12">
                                    <table class="table-style-one" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>Imgen</th>
                                                <th>Acción</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodygrupocorreo">
                                            <tr>
                                                <td colspan="2">No existen registros.</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3 text-center">
                                    <button type="button" class="btnHermes" id="btn_grabar_planilla" onclick="GrabarPlanilla();">
                                        Grabar
                                    </button>
                                </div>
                                <div class="col-lg-3 text-center">
                                    <button type="button" class="btnHermes" id="btn_anular_planilla" style="display: none;">
                                        Anular Plantilla
                                    </button>
                                </div>
                                <div class="col-lg-3 text-center">
                                    <button type="button" class="btnHermes" id="btn_vista_previa">
                                        Vista Previa
                                    </button>
                                </div>
                                <div class="col-lg-3 text-center">
                                    <button type="button" class="btnHermes" onclick="nuevaplantillar()">
                                        Nuevo
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group row">
                                <div class="col-sm-12 col-lg-12">
                                    <h4>Plantilla</h4>
                                    <div class="contenedo-planilla">
                                        <div class="list-group" id="listgroup">
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" data-dismiss="modal">
            <div class="modal-content">
                <div class="modal-body">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <img src="" class="imagepreview" style="width: 100%;">
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="preview" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Vista previa - Plantilla</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group row">
                        <div class="col-lg-12">
                            <div style="width: 100%; height: auto; padding: 0px 40px;" id="contentplantilla">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--      <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>--%>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="confirm-submit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    Mensaje de Confirmacion
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    Esta seguro que desea anular la plantilla?                
                </div>
                <div class="modal-footer">
                    <button id="btn-submit-confirmacion" type="button" class="btnHermes" onclick="Grabar();">Aceptar</button>
                    <button type="button" class="btnHermesNegro" data-dismiss="modal" aria-label="Close">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
   <%-- <script type="text/javascript" src="Scripts/FileStyle/bootstrap-filestyle.js"></script>--%>
    <input type="hidden" id="flag_accion" value="INS" />
    <script type="text/javascript">
        //$('#input08').filestyle({
        //    'placeholder': 'adjunte archivo',
        //    text: ' Examinar',
        //    btnClass: 'btn-success'
        //});
        //$('#input09').filestyle({
        //    'placeholder': 'adjunte archivo',
        //    text: ' Examinar',
        //    btnClass: 'btn-success'
        //});

        $('#file_browser_input08').click(function (e) {
            e.preventDefault();
            $('#input08').click();
        });
        $('#input08').change(function () {            
            $('#file_path_input08').val($(this).val());
            var fl_result = checkfile($('#file_path_input08').val());
            if (!fl_result) {
                $('#file_path_input08').val("");
                $('#input08').val("");
            }
        });


        $('#file_browser').click(function (e) {
            e.preventDefault();
            $('#input09').click();
        });
        $('#input09').change(function () {
            $('#file_path').val($(this).val());
            var fl_result = checkfileImgen($('#file_path').val());
            if (!fl_result) {
                $('#file_path').val("");
                $('#input09').val("");
            }
        });


        $(document).ready(function (e) {

            listarPlanilla();
        });
        Alert = {
            show: function ($div, msg) {
                $div.find('.alert-msg').text(msg);
                if ($div.css('display') === 'none') {
                    $div.fadeIn(1000).delay(2000).fadeOut(3000);
                }
            },
            info: function (msg) {
                this.show($('#alert-info'), msg);
            },
            warn: function (msg) {
                this.show($('#alert-warn'), msg);
            },
            danger: function (msg) {
                this.show($('#alert-danger'), msg);
            }
        }

        function checkfile(sender) {
            var validExts = new Array(".html");
            var fileExt = sender;//sender.value;
            fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
            if (validExts.indexOf(fileExt) < 0) {
                sender.value = "";
                Alert.warn("El archivo seleccionado es inválido , los archivos válidos son de tipo " +
                    validExts.toString() + "");
                return false;
            }
            else return true;
        }

        function checkfileImgen(sender) {
            var validExts = new Array(".jpg", ".JPEG ", ".png");
            var fileExt = sender//sender.value;
            console.log(fileExt);
            fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
            if (validExts.indexOf(fileExt) < 0) {
                sender.value = "";
                Alert.warn("El archivo seleccionado es inválido , los archivos válidos son de tipo " +
                    validExts.toString() + "");
                return false;
            }
            else return true;
        }

        function confirmar_AnularPlantilla(id) {
            $("#confirm-submit").modal("show");
            $('#btn-submit-confirmacion').attr('onclick', 'AnularPlantilla(' + id + ')');
        }

        function notify(el, data) {
            resetElements();
            console.log(el.innerHTML);
            el.classList.add('active');
            $("#txt_descripcion").val(data.descripcion);
            $('#txt_descripcion').prop('readonly', true);
            cargarTablaImagenBD(data.list_plantilla_detalle);
            $('#btn_anular_planilla').attr('onclick', 'confirmar_AnularPlantilla(' + data.id + ')');
            $('#btn_vista_previa').attr('onclick', 'modalPreview(' + data.id + ')');
            $("#flag_accion").val('UPD');
            $("#btn_grabar_planilla").css('display', 'none');
            $("#btn_anular_planilla").css('display', 'block');

        }

        function nuevaplantillar() {
            $("#btn_grabar_planilla").css('display', 'block');
            $("#btn_anular_planilla").css('display', 'none');
            $('#file_path').val("");
            $('#file_path_input08').val("");
            $('#input08').val("");
            $('#txt_descripcion').prop('readonly', false);
            document.getElementById("tbodygrupocorreo").innerHTML = "";
            var HTML = "";
            HTML += "<tr>";
            HTML += "<td colspan='2'>No existen registros</td>";
            HTML += "</tr>";
            document.getElementById("tbodygrupocorreo").innerHTML = HTML;

            $("#txt_descripcion").val("");
        }

        function modalPreview(id_plantilla) {
            $.ajax({
                type: "POST",
                url: "AdministrarPlantillas.aspx/Preview",
                contentType: "application/json",
                data: '{plantilla:"' + id_plantilla + '"}',
                dataType: "json",
                success: function (response) {
                    var result = response.d.Status;
                    var ViewResult = response.d.ViewResult;
                    if (result == "OK") {
                        $('#contentplantilla').html(ViewResult);
                        $('#preview').modal('show');
                    } else {
                        Alert.danger("Ocurrio un error comuniquese con el Administrador.");
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        Alert.danger(response);
                }
            });
        }

        function cargarTablaImagenBD(listImagen) {
            var HTML = "";
            var verimagen = "";
            debugger;
            document.getElementById("tbodygrupocorreo").innerHTML = "";
            if (listImagen.length > 0) {
                for (var index = 0; index < listImagen.length; index++) {
                    var vals = {
                        id: parseInt(listImagen[index].id),
                        ruta_site_imagen: listImagen[index].ruta_site_imagen
                    };
                    var Vals = JSON.stringify(vals);
                    verimagen = "VerImagen(" + Vals + ")";
                    HTML += "<tr>";
                    HTML += "<td>" + listImagen[index].NombreArchivoImagen + "</td>";
                    HTML += "<td>";
                    HTML += "<a onclick='" + verimagen + "' href='javascript:void(0);' title='Ver Imagen' ><span class=''></span> Ver Imagen</a>";
                    HTML += "</td>";
                    HTML += "</tr>";
                }
            } else {
                HTML += "<tr>";
                HTML += "<td colspan='2'>No existen registros</td>";
                HTML += "</tr>";
            }
            document.getElementById("tbodygrupocorreo").innerHTML = HTML;
        }

        function VerImagen(data) {
            $('.imagepreview').attr('src', data.ruta_site_imagen);
            $('#imagemodal').modal('show');
        }

        function AnularPlantilla(id_plantilla) {
            $.ajax({
                type: "POST",
                url: "AdministrarPlantillas.aspx/AnularPlantilla",
                contentType: "application/json",
                data: '{plantilla:"' + id_plantilla + '"}',
                dataType: "json",
                success: function (response) {
                    var result = response.d.Status;
                    var mensaje = response.d.Mensaje;
                    if (result == "Ok") {
                        Alert.info(mensaje);
                        listarPlanilla();
                        $("#txt_descripcion").val("");
                        document.getElementById("tbodygrupocorreo").innerHTML = "";
                        var tr = "<tr><td colspan='2'>No existen registros.</td></tr>";
                        document.getElementById("tbodygrupocorreo").innerHTML = tr;
                        $("#flag_accion").val('INS');
                    } else {
                        Alert.danger(mensaje);
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        Alert.danger(mensaje);
                }
            });
            $("#confirm-submit").modal("hide");
        }

        function resetElements() {
            var els = document.getElementsByClassName("active");
            for (var i = 0; i < els.length; i++) {
                els[i].classList.remove('active')
            }
        }
        var listImagen = [];
        function AgregarImagen() {
            var file_text = $('#file_path').val();
            if (file_text != "") {
                var files = $("#input09").get(0).files;
                listImagen.push(files);
                cargarTablaImagen(listImagen);
                $('#file_path').val("");
            } else {
                Alert.danger("Por favor agregue Archivo - Imagen");
            }
        }

        function cargarTablaImagen(listImagen) {
            var HTML = "";
            var modalEliminar = "";
            document.getElementById("tbodygrupocorreo").innerHTML = "";
            if (listImagen.length > 0) {
                for (var index = 0; index < listImagen.length; index++) {
                    var vals = {
                        id: parseInt(index)
                    };
                    var Vals = JSON.stringify(vals);
                    modalEliminar = "modalEliminar(" + Vals + ")";
                    HTML += "<tr>";
                    HTML += "<td>" + listImagen[index][0].name + "</td>";
                    HTML += "<td>";
                    HTML += "<a href='#' onclick='" + modalEliminar + "' title='Eliminar registro' ><span class='fa fa-trash'></span> Eliminar</a>";
                    HTML += "</td>";
                    HTML += "</tr>";
                }
            } else {
                HTML += "<tr>";
                HTML += "<td colspan='2'>No existen registros</td>";
                HTML += "</tr>";
            }
            document.getElementById("tbodygrupocorreo").innerHTML = HTML;
        }

        function modalEliminar(data) {
            listImagen.splice(listImagen.indexOf(data.id), 1);
            cargarTablaImagen(listImagen);
        }


        /*
        function VerImagen(data) {
            listImagen.splice(listImagen.indexOf(data.id), 1);
            cargarTablaImagen(listImagen);
        }        */

        function listarPlanilla() {
            $.ajax({
                type: "POST",
                url: "AdministrarPlantillas.aspx/getPlanilla",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var vals = new Object();
                    var result = JSON.parse(response.d.DataJson);
                    var HTML = "";
                    var modalActualizar = "";
                    document.getElementById("listgroup").innerHTML = "";
                    if (result.length > 0) {
                        var contador = 0;
                        for (index in result) {
                            var id = parseInt(result[index]["id"]);
                            var descripcion = result[index]["descripcion"];
                            var NombreArchivoHtml = result[index]["NombreArchivoHtml"];
                            var ruta_plantilla_html = result[index]["ruta_plantilla_html"];
                            var fl_nuevo = result[index]["fl_nuevo"];
                            var listPlantillaDet = result[index].list_plantilla_detalle;
                            var listDet = [];
                            for (var i = 0; i < listPlantillaDet.length; i++) {
                                detallePlantilla = {
                                    id: listPlantillaDet[i]["id"],
                                    NombreArchivoImagen: listPlantillaDet[i]["NombreArchivoImagen"],
                                    ruta_imagen: listPlantillaDet[i]["ruta_imagen"],
                                    ruta_site_imagen: listPlantillaDet[i]["ruta_site_imagen"],
                                    id_estado: listPlantillaDet[i]["id_estado"],
                                    fl_nuevo: listPlantillaDet[i]["fl_nuevo"]
                                }
                                listDet.push(detallePlantilla);
                            }

                            plantilla = {
                                id: id,
                                descripcion: descripcion,
                                NombreArchivoHtml: NombreArchivoHtml,
                                ruta_plantilla_html: ruta_plantilla_html,
                                fl_nuevo: fl_nuevo,
                                list_plantilla_detalle: listDet
                            }

                            var Vals = JSON.stringify(plantilla);
                            /* if (contador == 0) {
                                 HTML += "<button type='button' onclick='notify(this," + Vals + ")' class='list-group-item list-group-item-action active'>" + result[index]["descripcion"] + "</button>";
                             } else {*/
                            HTML += "<button type='button' onclick='notify(this," + Vals + ")' class='list-group-item list-group-item-action'>" + result[index]["descripcion"] + "</button>";
                            /* }*/
                            contador++;
                        }
                        document.getElementById("listgroup").innerHTML = HTML;

                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        Alert.danger(response);
                }
            });
        }

        function GrabarPlanilla() {
            var data = new FormData();
            var txt_descripcion = $('#txt_descripcion').val();
            if (txt_descripcion == "") {
                Alert.danger("Por favor ingrese Descripcion ");
                $('#txt_descripcion').focus();
                return false;
            }
            // Add the uploaded image content to the form data collection
            var file_text = $('#file_path_input08').val();
            if (file_text != "") {
                var files = $("#input08").get(0).files;
                data.append("FiledataHTML", files[0]);
                data.append("FiledataHTMLName", files[0].name);
            } else {
                Alert.danger("Por favor Seleccione Archivo - HTML");
                return false;
            }
           
            if (listImagen.length > 0) {
                for (var index = 0; index < listImagen.length; index++) {
                    data.append("FiledataImagenes" + index, listImagen[index][0]);
                }
            } else {
                Alert.danger("Por favor agregue Archivo - Imagen");
                // $("#input09").focus();
                return false;
            }
            data.append("txt_descripcion", txt_descripcion);
            $.ajax({
                type: 'post',
                url: "UploadPlantilla.ashx",
                contentType: false,
                processData: false,
                data: data,
                success: function (response) {
                    var objeto = JSON.parse(response);
                    if (objeto.Result == "Ok") {
                        Alert.info(objeto.Mensaje);
                        listarPlanilla();
                        listImagen = [];
                        document.getElementById("tbodygrupocorreo").innerHTML = "";
                        $('#txt_descripcion').val('');
                        $('#file_path_input08').val("");
                        $('#input08').val("");                        
                    } else {
                        Alert.danger(objeto.Mensaje);
                    }
                },
                error: function (error) {
                    Alert.danger("Error Consulte con el Administrador de Sistema.");
                }
            });
        }


    </script>
</asp:Content>
