<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="EnvioCorreo.aspx.cs" Inherits="Demo.EnvioCorreo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/FileStyle/bootstrap-filestyle.js"></script>
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

        .containerpanel {
            width: 100%;
            height: auto;
            /*padding: 0px 85px;*/
        }

        .panel1 {
            width: 100%;
            height: 179px;
            border: 1px solid #D5CFDF;
            border-radius: 6px;
        }

        .panel2 {
            width: 100%;
            height: 179px;
            border: 1px solid #D5CFDF;
            border-radius: 6px;
        }

        .panelButton {
            width: 100%;
            height: 178px;
            border: 1px solid #D5CFDF;
            border-radius: 6px;
            float: right;
            /* margin-top: -179px; */
            /* margin-right: 417px; */
            padding: 5px 5px;
        }

        .btnHermesEnvioCorreo {
            margin-top: 18px;
            width: 100%;
            background-color: orangered;
            border: none;
            color: white;
            padding: 1px 22px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-family: Arial, Helvetica, sans-serif;
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
        }

        .tilepanel {
            padding: 0px 6px;
        }

        .alert-top {
            top: 50px;
            width: 100%;
            display: none;
            text-align: center;
            margin-bottom: 0;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-header">
        <div class="container-fluid">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color: white"><b>Envio Correo</b> </a>
                </div>
                <div class="panel-body">
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
                        <label for="inputEmail3" class="col-sm-1 control-label">Asunto:</label>
                        <div class="col-sm-11">
                            <input id="hdnUserSession" runat="server" name="hdnUserSession" type="hidden">
                            <input type="text" class="form-control" id="txt_asunto" placeholder="Ingrese Asunto">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-1 control-label">Origen:</label>
                        <div class="col-sm-4">
                            <select id="cbo_origen" onchange="selectChange()" class="form-control">
                            </select>
                        </div>
                        <label for="inputEmail3" class="col-sm-2 control-label text-right">Plantilla:</label>
                        <div class="col-sm-5">
                            <select id="cbo_plantilla" class="form-control">
                            </select>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="containerpanel">
                            <div class="col-sm-12 col-sm-offset-0 col-lg-4 col-md-offset-1">
                                <div class="panel1">
                                    <div class="tilepanel">
                                        <label for="exampleInputEmail1">Grupo Correo Origen</label>
                                    </div>
                                    <div>
                                        <select multiple="multiple" name="cbogrupocorreoorigen[]" id="cbogrupocorreoorigen" class="form-control" style="height: 152px;">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-lg-2">
                                <div class="panelButton">
                                    <button type="button" class="btnHermesEnvioCorreo" id="pasartodos">
                                        >>|
                                    </button>
                                    <br />
                                    <button type="button" class="btnHermesEnvioCorreo" id="pasar">
                                        >|
                                    </button>
                                    <br />
                                    <button type="button" class="btnHermesEnvioCorreo" id="quitar">
                                        |<
                                    </button>
                                    <br />
                                    <button type="button" class="btnHermesEnvioCorreo" id="quitartodos">
                                        |<<
                                    </button>
                                </div>
                            </div>
                            <div class="col-sm-12 col-lg-5">
                                <div class="panel2">
                                    <div class="tilepanel">
                                        <label for="exampleInputEmail1">Grupo Correo Destino</label>
                                    </div>
                                    <div>
                                        <select multiple="multiple" name="cbogrupocorreodestino[]" id="cbogrupocorreodestino" class="form-control" style="height: 152px;">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-1 control-label">Seleccione Archivo:</label>
                        <div class="col-sm-11">
                            <%-- <input type="file" id="input08" onchange="checkfile(this);" multiple>--%>
                            <div class="input-group">
                                <input type="text" readonly="readonly" id="file_path" class="form-control" placeholder="Adjunte Archivo">
                                <span class="input-group-btn">
                                    <button class="btn btn-default btn-success" style="color: white" type="button" id="file_browser">
                                        <i class="fa fa-search"></i>Examinar</button>
                                </span>
                            </div>
                            <input type="file" class="hidden" id="input08" name="input08">
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-4 text-center">
                            <button type="button" class="btnHermes" id="btn_vista_previa" onclick="modalPreview();">
                                Vista Previa
                            </button>
                        </div>
                        <div class="col-lg-4 text-center">

                            <button type="button" class="btnHermes" onclick="confirmarEnvioCorreo();">
                                Enviar Correo
                            </button>

                        </div>
                        <div class="col-lg-4 text-center">
                            <button type="button" class="btnHermes" data-toggle="modal" onclick="">
                                Salir
                            </button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="preview" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog  modal-lg" role="document">
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
                    Esta seguro que desea enviar el correo?                
                </div>
                <div class="modal-footer">
                    <button id="btn-submit-confirmacion" type="button" class="btnHermes" onclick="Grabar();">Aceptar</button>
                    <button type="button" class="btnHermesNegro" data-dismiss="modal" aria-label="Close">Cancelar</button>
                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <a id="btn-submit-confirmacion" class="btn btn-success success">Ok</a>--%>
                </div>
            </div>
        </div>
    </div>
    <div id="modalpromt" class="modal fade in" style="padding-left: 17px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div id="ezAlerts-header" class="modal-header alert-primary" style="padding: 15px; border-top-left-radius: 5px; border-top-right-radius: 5px;">
                    <button id="close-button" type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button><h4 id="ezAlerts-title" class="modal-title">Ingrese parrafo</h4>
                </div>
                <div id="ezAlerts-body" class="modal-body">
                    <div class="form-group row">
                        <div class="col-lg-12">
                            <div id="alert-info-promt" class="alert alert-info alert-top" role="alert">
                                <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <span class="alert-msg"></span>
                            </div>
                            <div id="alert-warn-promt" class="alert alert-warning alert-top" role="alert">
                                <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <span class="alert-msg"></span>
                            </div>
                            <div id="alert-danger-promt" class="alert alert-danger alert-top" role="alert">
                                <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <span class="alert-msg"></span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-12">
                            <textarea class="form-control" rows="8" id="txt_prompt"></textarea>
                        </div>
                    </div>
                </div>
                <div id="ezAlerts-footer" class="modal-footer">
                    <input class="btnHermes" id="btnpront" value="Aceptar" />
                    <button type="button" class="btnHermesNegro" data-dismiss="modal" aria-label="Close">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        var listOrigen = [];
        var listDestino = [];
        //$('#input08').filestyle({
        //    'placeholder': 'adjunte archivo',
        //    text: ' Examinar',
        //    btnClass: 'btn-success'
        //});

        $('#file_browser').click(function (e) {
            e.preventDefault();
            $('#input08').click();
        });
        $('#input08').change(function () {
            $('#file_path').val($(this).val());
            var fl_result = checkfile($('#file_path').val());
            if (!fl_result) {
                $('#file_path').val("");
                $('#input08').val("");
            }
        });


        //var list = [];
        function confirmarEnvioCorreo() {
            //var selO = document.getElementsByName('cbogrupocorreodestino[]')[0];
            //for (i = 0; i < selO.length; i++) {
            //    id = selO.options[i].value;
            //    list.push(id);
            //}
            var cbo_origen = $("#cbo_origen").val();
            var cbo_plantilla = $("#cbo_plantilla").val();
            var txt_asunto = $("#txt_asunto").val();

            if (txt_asunto == "") {
                Alert.warn("Por favor Ingrese Asunto");
                return false;
            }
            if (cbo_origen == "0") {
                Alert.warn("Por favor seleccione Fuentes de Datos");
                return false;
            }
            if (cbo_plantilla == "0") {
                Alert.warn("Por favor seleccione Plantilla");
                return false;
            }
            if (listDestino.length == 0) {
                Alert.warn("Por favor agregue datos a la seccion Grupo Correo Destino.");
                return false;
            }
            if (cbo_plantilla.split("-")[1] > 0) {
                $("#modalpromt").modal("show");
                $('#btnpront').attr('onclick', 'aceptarPromt()');
            } else {
                $("#confirm-submit").modal("show");
                $('#btn-submit-confirmacion').attr('onclick', 'EnviarCorreo()');
            }
        }
        function aceptarPromt() {
            var txtpromt = $("#txt_prompt").val();
            if (txtpromt == "") {
                AlertPromt.warn("Por favor ingrese parrafo");
                return false;
            }
            $("#modalpromt").modal("hide");
            $("#confirm-submit").modal("show");
            $('#btn-submit-confirmacion').attr('onclick', 'EnviarCorreo()');
            return false;
        }
        function checkfile(sender) {
            var validExts = new Array(".xlsx", ".xls", ".jpg", "JPEG", "png", ".doc", ".pdf", ".docx");
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
        AlertPromt = {
            show: function ($div, msg) {
                $div.find('.alert-msg').text(msg);
                if ($div.css('display') === 'none') {
                    // fadein, fadeout.                    
                    $div.fadeIn(1000).delay(2000).fadeOut(2000);
                }
            },
            info: function (msg) {
                this.show($('#alert-info-promt'), msg);
            },
            warn: function (msg) {
                this.show($('#alert-warn-promt'), msg);
            },
            danger: function (msg) {
                this.show($('#alert-danger-promt'), msg);
            }
        }

        Alert = {
            show: function ($div, msg) {
                $div.find('.alert-msg').text(msg);
                if ($div.css('display') === 'none') {
                    // fadein, fadeout.                    
                    $div.fadeIn(1000).delay(2000).fadeOut(2000);
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

        function copiarOpcion(opcion, destino) {
            var valor = $(opcion).val();
            if ($(destino + " option[value=" + valor + "]").length == 0) {
                $(opcion).appendTo(destino);
            }
        }

        function modalPreview() {
            var cbo_plantilla = $("#cbo_plantilla").val();
            if (cbo_plantilla == "0") {
                $("#cbo_plantilla").focus();
                return false;
            }
            cbo_plantilla = cbo_plantilla.split("-")[0];
            $.ajax({
                type: "POST",
                url: "EnvioCorreo.aspx/Preview",
                contentType: "application/json",
                data: '{plantilla:"' + cbo_plantilla + '"}',
                dataType: "json",
                success: function (response) {
                    var result = response.d.Status;
                    var ViewResult = response.d.ViewResult;
                    if (result == "OK") {
                        $('#contentplantilla').html(ViewResult);
                        $('#preview').modal('show');
                    } else {
                        Alert.danger(mensaje);
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        Alert.danger(response);
                }
            });
        }

        function loadOrigen() {
            $.ajax({
                type: "POST",
                url: "MantGrupoCorreo.aspx/getListParametrosMaestro",
                data: '{skey:"ORIGEN_GRUPOCORREO"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    var models = JSON.parse(response.d.DataJson);// (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    $('#cbo_origen').empty();
                    $('#cbo_origen').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < models.length; i++) {
                        var valor = models[i].valor;
                        var text = models[i].descripcion;
                        $("#cbo_origen").append($("<option></option>").val(valor).html(text));
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });
        }

        function selectChange() {
            var origen = $("#cbo_origen").val();
            loadGrupoCorreoXOrigen(origen);
        }
        function loadGrupoCorreoXOrigen(origen) {
            $.ajax({
                type: "POST",
                url: "MantGrupoCorreo.aspx/getListGrupoCorreoXOrigen",
                data: '{origen:"' + origen + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var models = JSON.parse(response.d.DataJson);

                    if (listDestino.length > 0) {
                        listOrigen = [];
                        for (var j = 0; j < models.length; j++) {
                            var objeto = listDestino.find(obj => obj.id === models[j].id);
                            if (objeto === undefined || objeto === null) {
                                listOrigen.push(models[j]);
                            }
                        }

                    } else {
                        listOrigen = models;
                    }
                    cargargrupocorre_origen();
                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });
        }

        function cargargrupocorre_origen() {
            $('#cbogrupocorreoorigen').empty();
            for (var i = 0; i < listOrigen.length; i++) {
                var valor = listOrigen[i].id;
                var text = listOrigen[i].descripcion;
                $("#cbogrupocorreoorigen").append($("<option></option>").val(valor).html(text));
            }
        }
        function cargargrupocorre_destino() {
            $('#cbogrupocorreodestino').empty();
            for (var i = 0; i < listDestino.length; i++) {
                var valor = listDestino[i].id;
                var text = listDestino[i].descripcion;
                $("#cbogrupocorreodestino").append($("<option></option>").val(valor).html(text));
            }
        }


        function loadPlantilla() {
            $.ajax({
                type: "POST",
                url: "AdministrarPlantillas.aspx/getPlanilla",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var models = JSON.parse(response.d.DataJson);
                    $('#cbo_plantilla').empty();
                    $('#cbo_plantilla').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < models.length; i++) {
                        var valor = models[i].id + "-" + models[i].fl_parrafo;
                        var text = models[i].descripcion;
                        $("#cbo_plantilla").append($("<option></option>").val(valor).html(text));
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });
        }


        function EnviarCorreo() {
            debugger;
            var cbo_origen = $("#cbo_origen").val().split("");
            var cbo_plantilla = $("#cbo_plantilla").val();
            var txt_asunto = $("#txt_asunto").val();
            var txt_prompt = $("#txt_prompt").val();
            var txt_UserSession = $("#<%=hdnUserSession.ClientID %>").val();

            var data = new FormData();
            for (var i = 0, len = document.getElementById('input08').files.length; i < len; i++) {
                data.append("Files" + i, document.getElementById('input08').files[i]);
            }
            cbo_plantilla = cbo_plantilla.split("-")[0];
            data.append("cbo_origen", cbo_origen);
            data.append("cbo_plantilla", cbo_plantilla);
            data.append("txt_asunto", txt_asunto);
            data.append("txt_prompt", txt_prompt);
            data.append("txt_UserSession", txt_UserSession);

            var list = [];
            for (var i = 0; i < listDestino.length; i++) {
                var id = listDestino[i].id.toString();
                list.push(id);
            }
            data.append("list", list);
            $.ajax({
                type: 'post',
                url: "UploadEnvioCorreo.ashx",
                contentType: false,
                processData: false,
                data: data,
                success: function (response) {
                    var objeto = JSON.parse(response);
                    if (objeto.Result == "Ok") {
                        Alert.info(objeto.Mensaje);
                        $("#txt_asunto").val('');
                        $("#cbo_origen").val('0');
                        $("#cbo_plantilla").val('0');
                        $("#txt_prompt").val('');
                        listDestino = [];
                        listOrigen = [];
                        cargargrupocorre_origen();
                        cargargrupocorre_destino();
                        $('#file_path').val("");
                        $('#input08').val("");
                    } else {
                        Alert.danger(objeto.Mensaje);
                    }
                },
                error: function (error) {
                    Alert.danger("Error Consulte con el Administrador de Sistema.");
                }
            });
            $("#confirm-submit").modal("hide");
        }

        $(document).ready(function () {
            loadOrigen();
            loadPlantilla();
            $('#pasar').click(function () {
                var arrayId = "";
                var selO = document.getElementsByName('cbogrupocorreoorigen[]')[0];
                for (i = 0; i < selO.length; i++) {
                    if (selO.options[i].selected == true) {
                        id = parseInt(selO.options[i].value);
                        var objeto = listOrigen.find(obj => obj.id === id);
                        arrayId += objeto.id + ",";
                        //listOrigen.find(obj => obj.id === id).flag = 1;
                        listDestino.push(objeto);
                        //var index = listOrigen.indexOf(objeto);
                        //listOrigen.splice(index, 1);
                    }
                }
                arrayId = arrayId.slice(0, -1);
                var arrayID = arrayId.split(",");
                for (var e = 0; e < arrayID.length; e++) {
                    var id = parseInt(arrayID[e]);
                    var objeto = listOrigen.find(book => book.id === id);
                    var Index = listOrigen.indexOf(objeto);
                    listOrigen.splice(Index, 1);
                }
                cargargrupocorre_origen();
                cargargrupocorre_destino();
                //copiarOpcion($('#cbogrupocorreoorigen option:selected').clone(), "#cbogrupocorreodestino");
                //$('#cbogrupocorreoorigen option:selected').remove();
            });
            $('#pasartodos').click(function () {
                var arrayId = "";
                var selO = document.getElementsByName('cbogrupocorreoorigen[]')[0];
                for (i = 0; i < selO.length; i++) {
                    //if (selO.options[i].selected == true) {
                    id = parseInt(selO.options[i].value);
                    var objeto = listOrigen.find(obj => obj.id === id);
                    arrayId += objeto.id + ",";
                    listDestino.push(objeto);
                    //var index = listOrigen.indexOf(objeto);
                    //listOrigen.splice(index, 1);
                    //}
                }

                arrayId = arrayId.slice(0, -1);
                var arrayID = arrayId.split(",");
                for (var e = 0; e < arrayID.length; e++) {
                    var id = parseInt(arrayID[e]);
                    var objeto = listOrigen.find(book => book.id === id);
                    var Index = listOrigen.indexOf(objeto);
                    listOrigen.splice(Index, 1);
                }

                cargargrupocorre_origen();
                cargargrupocorre_destino();
                //$('#cbogrupocorreoorigen option').each(function () {
                //    copiarOpcion($(this).clone(), "#cbogrupocorreodestino");
                //});
                //$('#cbogrupocorreoorigen option').each(function () {
                //    $(this).remove();
                //});
            });
            $('#quitar').click(function () {
                var arrayId = "";
                var selO = document.getElementsByName('cbogrupocorreodestino[]')[0];
                for (i = 0; i < selO.length; i++) {
                    if (selO.options[i].selected == true) {
                        id = parseInt(selO.options[i].value);
                        var objeto = listDestino.find(obj => obj.id === id);
                        arrayId += objeto.id + ",";
                        listOrigen.push(objeto);
                    }
                }

                arrayId = arrayId.slice(0, -1);
                var arrayID = arrayId.split(",");
                for (var e = 0; e < arrayID.length; e++) {
                    var id = parseInt(arrayID[e]);
                    var objeto = listDestino.find(book => book.id === id);
                    var Index = listDestino.indexOf(objeto);
                    listDestino.splice(Index, 1);
                }

                cargargrupocorre_origen();
                cargargrupocorre_destino();
                //copiarOpcion($('#cbogrupocorreodestino option:selected').clone(), "#cbogrupocorreoorigen");
                //$('#cbogrupocorreodestino option:selected').remove();
            });
            $('#quitartodos').click(function () {
                var arrayId = "";
                var selO = document.getElementsByName('cbogrupocorreodestino[]')[0];
                for (i = 0; i < selO.length; i++) {
                    id = parseInt(selO.options[i].value);
                    var objeto = listDestino.find(obj => obj.id === id);
                    arrayId += objeto.id + ",";
                    listOrigen.push(objeto);
                }
                arrayId = arrayId.slice(0, -1);
                var arrayID = arrayId.split(",");
                for (var e = 0; e < arrayID.length; e++) {
                    var id = parseInt(arrayID[e]);
                    var objeto = listDestino.find(book => book.id === id);
                    var Index = listDestino.indexOf(objeto);
                    listDestino.splice(Index, 1);
                }

                //segundo mike
                cargargrupocorre_origen();
                cargargrupocorre_destino();
                //$('#cbogrupocorreodestino option').each(function () {
                //    copiarOpcion($(this).clone(), "#cbogrupocorreoorigen");
                //});
                //$('#cbogrupocorreodestino option').each(function () {
                //    $(this).remove();
                //});
            });
        });
    </script>
</asp:Content>
