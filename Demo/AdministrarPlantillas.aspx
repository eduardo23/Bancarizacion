<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="AdministrarPlantillas.aspx.cs" Inherits="Demo.AdministrarPlantillas" %>

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
                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color: white"><b>Envio Correo</b> </a>
                </div>
                <div class="panel-body">
                    <div class="form-group row">
                        <div class="col-md-6">
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
                                    <input type="text" class="form-control" id="txt_asunto" placeholder="Ingrese descripción">
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-12 col-lg-12">
                                    <label>Seleccione Archivo</label>
                                    <input type="file" id="input08" onchange="checkfile(this);">
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-12 col-lg-8">
                                    <label>Seleccione Archivo - Imagen</label>
                                    <input type="file" id="input09" onchange="checkfileImgen(this);">
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
                                                <td>XXXXXXX</td>
                                                <td><a href="javascript:void();" class="btn-link-tabla">Eliminar</a></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-4 text-center">
                                    <button type="button" class="btnHermes" onclick="modalRegistrar();">
                                        Grabar
                                    </button>
                                </div>
                                <div class="col-lg-4 text-center">

                                    <button type="button" class="btnHermes" onclick="EnviarCorreo();">
                                        Anular Plantilla
                                    </button>

                                </div>
                                <div class="col-lg-4 text-center">
                                    <button type="button" class="btnHermes" onclick="modalRegistrar();">
                                        Vista Previa
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <div class="col-sm-12 col-lg-12">
                                    <h4>Planilla</h4>
                                    <div class="contenedo-planilla">
                                        <div class="list-group">
                                            <button type="button" onclick="notify(this)" class="list-group-item list-group-item-action active">
                                                Cras justo odio
                                            </button>
                                            <button type="button" onclick="notify(this)" class="list-group-item list-group-item-action">Dapibus ac facilisis in</button>
                                            <button type="button" onclick="notify(this)" class="list-group-item list-group-item-action">Morbi leo risus</button>
                                            <button type="button" onclick="notify(this)" class="list-group-item list-group-item-action">Porta ac consectetur ac</button>
                                            <button type="button" onclick="notify(this)" class="list-group-item list-group-item-action">Vestibulum at eros</button>
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
    <script type="text/javascript">
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
            var fileExt = sender.value;
            fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
            if (validExts.indexOf(fileExt) < 0) {
                sender.value = "";
                Alert.warn("El archivo seleccionado es inválido , los archivos válidos son de tipo " +
                    validExts.toString() + "");
                return false;
            }
            else return true;
        }
        $('#input08').filestyle({
            'placeholder': 'adjunte archivo',
            text: ' Examinar',
            btnClass: 'btn-success'
        });

        function checkfileImgen(sender) {
            var validExts = new Array(".jpg", "JPEG ", "png");
            var fileExt = sender.value;
            fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
            if (validExts.indexOf(fileExt) < 0) {
                sender.value = "";
                Alert.warn("El archivo seleccionado es inválido , los archivos válidos son de tipo " +
                    validExts.toString() + "");
                return false;
            }
            else return true;
        }
        $('#input09').filestyle({
            'placeholder': 'adjunte archivo',
            text: ' Examinar',
            btnClass: 'btn-success'
        });

        function AgregarImagen() {

        }

        function notify(el) {
            resetElements();
            console.log(el.innerHTML);
            el.classList.add('active');
        }

        function resetElements() {
            // Get all elements with "active" class
            var els = document.getElementsByClassName("active");

            // Loop over Elements to remove active class;
            for (var i = 0; i < els.length; i++) {
                els[i].classList.remove('active')
            }
        }


    </script>
</asp:Content>
