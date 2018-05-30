<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desafiliacion.aspx.cs" Inherits="Demo.Desafiliacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Css/bootstrap/css/bootstrap.css" />
    <script type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
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
            width: 100%;
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

        .contenedor {
            width: 100%;
            padding: 60px 350px;
        }

        .contenido-text {
            font-size: 13px;
            text-align: justify;
            font-weight: bold;
        }
    </style>
</head>
<body>

    <div class="container">
        <div class="contenedor">
            <div class="form-group row">
                <img src="Images/logo-hermes.png" style="width: 100%; height: 112px;" />
            </div>
            <div class="form-group row">
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
            <div class="form-group row">
                <div class="contenido-text">
                    <p>Esta ud. Seguro que desea darse de baja? </p>
                    <p>,no podra recibir mas promociones </p>
                </div>
            </div>
            <div class="form-group row text-center">
                <div class="col-lg-6">
                    <button type="button" class="btnHermes" id="btn_confirmar" onclick="confirmar();">
                        Confirmar
                    </button>
                </div>
                <div class="col-lg-6">

                    <button type="button" class="btnHermes" id="btn_salir" onclick="cancelar();">
                        Cancelar
                    </button>
                </div>
            </div>
        </div>
    </div>
    <script>
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
        function confirmar() {
            $.ajax({
                type: "POST",
                url: "Desafiliacion.aspx/dardeBaja",
                contentType: "application/json",
                data: '{}',
                dataType: "json",
                success: function (response) {
                    debugger;
                    var result = response.d.Status;
                    var Mensaje = response.d.Mensaje;
                    if (result == "OK") {
                        //$("#btn_confirmar").css('disabled', false);
                        $("#btn_confirmar").css("display","none");
                        //$("#btn_confirmar").prop("disabled", false);
                        //$("btn_confirmar").attr('disabled', 'disabled');
                        Alert.info(Mensaje);
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
        function cancelar() {
            window.close();
        }
    </script>
</body>
</html>
