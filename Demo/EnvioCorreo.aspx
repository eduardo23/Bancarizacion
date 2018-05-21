<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="EnvioCorreo.aspx.cs" Inherits="Demo.EnvioCorreo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        .containerpanel {
            width: 100%;
            height: auto;
            padding: 0px 104px;
        }

        .panel1 {
            width: 40%;
            height: 179px;
            border: 1px solid #D5CFDF;
            border-radius: 6px;
        }

        .panel2 {
            width: 40%;
            height: 179px;
            border: 1px solid #D5CFDF;
            border-radius: 6px;
            float: right;
            margin-top: -180px;
            margin-right: -18px;
        }

        .panelButton {
            width: 16%;
            height: 178px;
            border: 1px solid #D5CFDF;
            border-radius: 6px;
            float: right;
            margin-top: -179px;
            margin-right: 417px;
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
                            <input type="text" class="form-control" id="txt_asunto" placeholder="Ingrese Asunto">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-1 control-label">Origen:</label>
                        <div class="col-sm-5">
                            <select id="cbo_origen" class="form-control">
                                <option value="0">--Seleccione--</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                            </select>
                        </div>
                        <label for="inputEmail3" class="col-sm-1 control-label">Plantilla:</label>
                        <div class="col-sm-5">
                            <select id="cbo_plantilla" class="form-control">
                                <option value="0">--Seleccione--</option>
                                <option value="1">1</option>
                                <option value="2">2</option>                                
                            </select>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-12 col-lg-12">
                            <div class="containerpanel">
                                <div class="panel1">
                                    <div class="tilepanel">
                                        <label for="exampleInputEmail1">Datos Origen</label>
                                    </div>
                                    <div>
                                        <select multiple="multiple" name="origen[]" id="origen" class="form-control" style="height: 152px;">
                                            <option value="1">Option 1</option>
                                            <option value="2">Option 2</option>
                                            <option value="3">Option 3</option>
                                            <option value="4">Option 4</option>
                                            <option value="5">Option 5</option>
                                            <option value="6">Option 6</option>
                                            <option value="7">Option 7</option>
                                            <option value="8">Option 8</option>
                                            <option value="9">Option 9</option>
                                            <option value="10">Option 10</option>
                                            <option value="11">Option 11</option>
                                            <option value="12">Option 12</option>
                                        </select>
                                    </div>

                                </div>
                                <div class="panelButton">
                                    <button type="button" class="btnHermesEnvioCorreo" id="quitartodos">
                                        |<<
                                    </button>
                                    <br />
                                    <button type="button" class="btnHermesEnvioCorreo" id="quitar">
                                        |<
                                    </button>
                                    <br />
                                    <button type="button" class="btnHermesEnvioCorreo" id="pasar">
                                        >|
                                    </button>
                                    <br />
                                    <button type="button" class="btnHermesEnvioCorreo" id="pasartodos">
                                        >>|
                                    </button>
                                </div>
                                <div class="panel2">
                                    <div class="tilepanel">
                                        <label for="exampleInputEmail1">Datos Destino</label>
                                    </div>
                                    <div>
                                        <select multiple="multiple" name="destino[]" id="destino" class="form-control" style="height: 152px;">
                                        </select>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-4 text-center">
                            <button type="button" class="btnHermes" data-toggle="modal" onclick="modalRegistrar();">
                                Vista Previa
                            </button>
                        </div>
                        <div class="col-lg-4 text-center">

                            <button type="button" class="btnHermes" data-toggle="modal" onclick="EnviarCorreo();">
                                Enviar Correo
                            </button>

                        </div>
                        <div class="col-lg-4 text-center">
                            <button type="button" class="btnHermes" data-toggle="modal" onclick="modalRegistrar();">
                                Salir
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
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

        function EnviarCorreo() {            
            var selO = document.getElementsByName('destino[]')[0];
            var list = [];
            for (i = 0; i < selO.length; i++) {
                var objeto = {
                    id: selO.options[i].value
                };
                list.push(objeto);
            }
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
            if (list.length > 0) {
                Alert.warn("Por favor agregue datos a la seccion destino.");
                return false;
            }
            
        }

        $(document).ready(function () {
            $('#pasar').click(function () {
                copiarOpcion($('#origen option:selected').clone(), "#destino");
                $('#origen option:selected').remove();
            });
            $('#pasartodos').click(function () {
                $('#origen option').each(function () {
                    copiarOpcion($(this).clone(), "#destino");
                });
                $('#origen option').each(function () {
                    $(this).remove();
                });
            });
            $('#quitar').click(function () {
                copiarOpcion($('#destino option:selected').clone(), "#origen");
                $('#destino option:selected').remove();
            });
            $('#quitartodos').click(function () {
                $('#destino option').each(function () {
                    copiarOpcion($(this).clone(), "#origen");
                });
                $('#destino option').each(function () {
                    $(this).remove();
                });
            });
        });
    </script>
</asp:Content>
