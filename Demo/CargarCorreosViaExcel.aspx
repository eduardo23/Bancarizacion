<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="CargarCorreosViaExcel.aspx.cs" Inherits="Demo.CargarCorreosViaExcel" %>

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
                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color: white"><b>Carga Correo Automatico</b> </a>
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
                        <div class="col-sm-12 col-lg-12">
                            <label>Seleccione Archivo</label>
                            <input type="file" id="input08" onchange="checkfile(this);">
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-12 col-lg-12">
                            <div class="progress" id="idprogress" style="display: none;">
                                <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="col-sm-12 col-lg-12">
                            <button type="button" class="btnHermes" data-toggle="modal" onclick="cargarArchivo();">
                                Cargar
                            </button>
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
            var validExts = new Array(".xlsx", ".xls");
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

        function cargarArchivo() {
            debugger;
            /*var formData = new FormData();
            var totalFiles = document.getElementById("input08").files.length;
            for (var i = 0; i < totalFiles; i++) {
                var file = document.getElementById("input08").files[i];
                formData.append("FileUpload", file);                
            }
            */
            var data = new FormData();

            var files = $("#input08").get(0).files;

            // Add the uploaded image content to the form data collection
            if (files.length > 0) {
                data.append("Filedata", files[0]);
            }
            $("#idprogress").css('display', 'block');
            $.ajax({
                xhr: function () {
                    var xhr = new window.XMLHttpRequest();
                    //Upload Progress
                    xhr.upload.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            var percentComplete = (evt.loaded / evt.total) * 100;
                            $('div.progress > div.progress-bar').css({ "width": percentComplete + "%" });
                            //$('div.progress > div.progress-bar').html(percentComplete + '%');
                        }
                    }, false);
                    //Download progress
                    xhr.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            var percentComplete = (evt.loaded / evt.total) * 100;
                            $("div.progress > div.progress-bar").css({ "width": percentComplete + "%" });
                            //$('div.progress > div.progress-bar').html(percentComplete + '%');
                        }
                    },
                   false);
                    return xhr;
                },
                type: 'post',
                url: "UploadFile.ashx",
                contentType: false,
                processData: false,
                data: data,
                success: function (response) {
                    var objeto = JSON.parse(response);
                    if (objeto.Result == "Ok") {
                        Alert.info(objeto.Mensaje);
                        $("div.progress > div.progress-bar").css({ "width": 0 + "%" });
                    } else {
                        Alert.danger(objeto.Mensaje);
                    }
                    $("#idprogress").css('display', 'none');
                },
                error: function (error) {
                    Alert.danger("Error Consulte con el Administrador de Sistema.");
                }
            });
        }
    </script>
</asp:Content>
