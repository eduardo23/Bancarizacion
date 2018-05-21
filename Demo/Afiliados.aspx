<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Afiliados.aspx.cs" Inherits="Demo.Afiliados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<link href="Css/standar.css" rel="stylesheet" />
<script src="Scripts/moment.js"></script>
    
<%--<script>   
    $('#txtNumeroDocumento').propddlOncTitEdtGradotype', 'number'); 
</script>--%>
<script>
    $(document).ready(function () {
        $(window).load(function () {           
            $("#pnlMadre *").prop('disabled', true);
        });
        $(this).load(function () {
            $("#pnlMadre *").prop('disabled', true);
        });
        $(document).load(function () {
            $("#pnlMadre *").prop('disabled', true);
        });

<%--        $("#<%=btnSgtePaso3.ClientID %>").click(function () {
            viewForm($("#<%=hdnProductoId.ClientID %>").val());
            //return false;
        });--%>
    })

    function onchange_OncConApePat(e) {
        $("#<%=hdnOncConApePat.ClientID %>").val($("#<%=txtOncConApePat.ClientID %>").val());
    }
    function onchange_OncConApeMat(e) {
        $("#<%=hdnOncConApeMat.ClientID %>").val($("#<%=txtOncConApeMat.ClientID %>").val());
    }
    function onchange_OncConNom(e) {
        $("#<%=hdnOncConNom.ClientID %>").val($("#<%=txtOncConNom.ClientID %>").val());
    }
    function onchange_OncConTipoDoc(e) {
        $("#<%=hdnOncConTipoDoc.ClientID %>").val($("#<%=ddlOncConTipoDoc.ClientID %>").val());
    }
    function onchange_OncConNroDoc(e) {
        $("#<%=hdnOncConNroDoc.ClientID %>").val($("#<%=txtOncConNroDoc.ClientID %>").val());
    }
    function onchange_OncConFecNac(e) {
        $("#<%=hdnOncConFecNac.ClientID %>").val($("#<%=txtOncConFecNac.ClientID %>").val());
    }

    function onblur_OncTitFecNac() {
        var valor= $("#<%=txtOncTitFecNac.ClientID %>").val()
        var fechanacimiento = moment(valor, "DD-MM-YYYY");
        //var fechanacimiento = moment(e.target.value, "DD-MM-YYYY");

        $("#<%=hdnEdad.ClientID %>").val(0);
        if (fechanacimiento.isValid()) {
            var years = moment().diff(valor, 'years');
            $("#<%=hdnEdad.ClientID %>").val(years);

            if ($("#<%=hdnProductoId.ClientID %>").val() == 3) { //Oncologico Escolar
                if (years > 18) {
                    $("#<%=txtOncTitFecNac.ClientID %>").focus();
                    $("#<%=lblcombo.ClientID %>").html('La edad máxima de afiliación al seguro Oncológico Escolar es de 18 años.');
                    $('#myModalCombo').modal('show');
                    return;
                }
            }

            if ($("#<%=hdnProductoId.ClientID %>").val() == 7) { //Oncologico Superior
                if (years > 30) {
                    $("#<%=txtOncTitFecNac.ClientID %>").focus();
                    $("#<%=lblcombo.ClientID %>").html('La edad máxima de afiliación al seguro Oncológico Superior es de 30 años.');
                    $('#myModalCombo').modal('show');
                    return;
                }
            }

            //var years = moment().diff(e.target.value, 'years');
            $("#<%=hdnOncTitFecNac.ClientID %>").val(years);

            //$("#<%=hdnOncConApePat.ClientID %>").val('');
            //$("#<%=txtOncConApePat.ClientID %>").val('');
            $("#<%=txtOncConApePat.ClientID%>").removeAttr("disabled");

            //$("#<%=hdnOncConApeMat.ClientID %>").val('');
            //$("#<%=txtOncConApeMat.ClientID %>").val('');
            $("#<%=txtOncConApeMat.ClientID%>").removeAttr("disabled");

            //$("#<%=chkOncConSoloApe.ClientID%>").removeAttr("checked");
            $("#<%=chkOncConSoloApe.ClientID%>").removeAttr("disabled");

            //$("#<%=hdnOncConNom.ClientID %>").val('');
            //$("#<%=txtOncConNom.ClientID %>").val('');
            $("#<%=txtOncConNom.ClientID%>").removeAttr("disabled");

            //$("#<%=hdnOncConTipoDoc.ClientID %>").val('0');
            //$("#<%=ddlOncConTipoDoc.ClientID %>").val('0');
            $("#<%=ddlOncConTipoDoc.ClientID%>").removeAttr("disabled");

            //$("#<%=hdnOncConNroDoc.ClientID %>").val('');
            //$("#<%=txtOncConNroDoc.ClientID %>").val('');
            $("#<%=txtOncConNroDoc.ClientID%>").removeAttr("disabled");

            //$("#<%=hdnOncConFecNac.ClientID %>").val('');
            //$("#<%=txtOncConFecNac.ClientID %>").val('');
            $("#<%=txtOncConFecNac.ClientID%>").removeAttr("disabled");
            //$("#divOncConPar").css("display", 'none');
            //var rb=$("#<%=rblOncConPar.ClientID %> input");
            //var radio1 = rb[0];
            //var radio2 = rb[1];
            //radio1.checked = false;
            //radio2.checked = false;
            if (years >= 18) {
                $("#<%=hdnOncConApePat.ClientID %>").val($("#<%=txtOncTitApePat.ClientID %>").val());
                $("#<%=txtOncConApePat.ClientID %>").val($("#<%=txtOncTitApePat.ClientID %>").val());
                $("#<%=txtOncConApePat.ClientID%>").attr("disabled", "disabled");

                $("#<%=hdnOncConApeMat.ClientID %>").val($("#<%=txtOncTitApeMat.ClientID %>").val());
                $("#<%=txtOncConApeMat.ClientID %>").val($("#<%=txtOncTitApeMat.ClientID %>").val());
                $("#<%=txtOncConApeMat.ClientID%>").attr("disabled", "disabled");

                if ($("#<%=chkOncTitSoloApe.ClientID %>").prop('checked')) {
                    $("#<%=chkOncConSoloApe.ClientID %>").prop('checked', true);
                }
                else {
                    $("#<%=chkOncConSoloApe.ClientID%>").removeAttr("checked");
                }
                $("#<%=chkOncConSoloApe.ClientID%>").attr("disabled", "disabled");

                $("#<%=hdnOncConNom.ClientID %>").val($("#<%=txtOncTitNom.ClientID %>").val());
                $("#<%=txtOncConNom.ClientID %>").val($("#<%=txtOncTitNom.ClientID %>").val());
                $("#<%=txtOncConNom.ClientID%>").attr("disabled", "disabled");

                $("#<%=hdnOncConTipoDoc.ClientID %>").val($("#<%=ddlOncTitTipoDoc.ClientID %>").val());
                $("#<%=ddlOncConTipoDoc.ClientID %>").val($("#<%=ddlOncTitTipoDoc.ClientID %>").val());
                $("#<%=ddlOncConTipoDoc.ClientID%>").attr("disabled", "disabled");

                $("#<%=hdnOncConNroDoc.ClientID %>").val($("#<%=txtOncTitNroDoc.ClientID %>").val());
                $("#<%=txtOncConNroDoc.ClientID %>").val($("#<%=txtOncTitNroDoc.ClientID %>").val());
                $("#<%=txtOncConNroDoc.ClientID%>").attr("disabled", "disabled");

                $("#<%=hdnOncConFecNac.ClientID %>").val($("#<%=txtOncTitFecNac.ClientID %>").val());
                $("#<%=txtOncConFecNac.ClientID %>").val($("#<%=txtOncTitFecNac.ClientID %>").val());
                $("#<%=txtOncConFecNac.ClientID%>").attr("disabled", "disabled");

                $("#divOncConPar").css("display", 'none');
                var rb = $("#<%=rblOncConPar.ClientID %> input");
                var radio1 = rb[0];
                var radio2 = rb[1];
                radio1.checked = false;
                radio2.checked = false;
            }
            else {
                $("#divOncConPar").css("display", 'block');
            }
        }
    }

    //$("#ContentPlaceHolder1_txtOncTitFecNac").change(function () {
    //    alert('hola');
    //});

    //$('#search-form .term').bind('input', function () {
    //    console.log('this actually works');
    //});
    // do stuff...

<%--        debugger;
        var value = $("#<%=txtOncTitFecNac.ClientID %>").val();
        var fechanacimiento = moment(value, "DD-MM-YYYY");
        if (!fechanacimiento.isValid())
            return false;
    });--%>

    function viewForm(tipoProd) {
<%--        //$("#divAlumno").css("display", 'none');
        //$("#divOncTit").css("display", 'none');
        //$("#divOncCon").css("display", 'none');
        

        $("#<%=txtApellidoPaterno.ClientID %>").removeAttr('required');
        $("#<%=txtApellidoMaterno.ClientID %>").removeAttr('required');
        $("#<%=txtNombres.ClientID %>").removeAttr('required');
        $("#<%=DDLTipoDocumento.ClientID %>").removeAttr('required');
        $("#<%=txtNumeroDocumento.ClientID %>").removeAttr('required');
        $("#<%=txtFechaNacimiento.ClientID %>").removeAttr('required');
        $("#<%=DDLGrado.ClientID %>").removeAttr('required');

        //INPUTS DATOS TITULAR
        $("#<%=txtOncTitApePat.ClientID %>").removeAttr('required');
        $("#<%=txtOncTitApeMat.ClientID %>").removeAttr('required');
        $("#<%=txtOncTitNom.ClientID %>").removeAttr('required');
        $("#<%=ddlOncTitTipoDoc.ClientID %>").removeAttr('required');
        $("#<%=txtOncTitNroDoc.ClientID %>").removeAttr('required');
        $("#<%=txtOncTitFecNac.ClientID %>").removeAttr('required');
        $("#<%=ddlOncTitGrado.ClientID %>").removeAttr('required');

        //INPUTS DATOS CONTRATANTE
        $("#<%=txtOncConApePat.ClientID %>").removeAttr('required');
        $("#<%=txtOncConApeMat.ClientID %>").removeAttr('required');
        $("#<%=txtOncConNom.ClientID %>").removeAttr('required');
        $("#<%=ddlOncConTipoDoc.ClientID %>").removeAttr('required');
        $("#<%=txtOncConNroDoc.ClientID %>").removeAttr('required');
        $("#<%=txtOncConFecNac.ClientID %>").removeAttr('required');
        $("#<%=ddlOncConPaisNac.ClientID %>").removeAttr('required');
        $("#<%=ddlOncConEstcivil.ClientID %>").removeAttr('required');
        $("#<%=ddlOncConDirPais.ClientID %>").removeAttr('required');
        $("#<%=ddlOncConDirDep.ClientID %>").removeAttr('required');
        $("#<%=ddlOncConDirPrv.ClientID %>").removeAttr('required');
        $("#<%=ddlOncConDirDis.ClientID %>").removeAttr('required');
        $("#<%=txtOncConDirEnt.ClientID %>").removeAttr('required');
        $("#<%=txtOncConTelDom.ClientID %>").removeAttr('required');
        $("#<%=txtOncConTelTrab.ClientID %>").removeAttr('required');
        $("#<%=txtOncConNroCel.ClientID %>").removeAttr('required');
        $("#<%=txtOncConEmail.ClientID %>").removeAttr('required');
        
        
        if (tipoProd == 1 || tipoProd == 2) 
        {
            //$("#divAlumno").css("display", 'block');
            $("#<%=txtApellidoPaterno.ClientID %>").attr("required", "true");
            $("#<%=txtApellidoMaterno.ClientID %>").attr("required", "true");
            $("#<%=txtNombres.ClientID %>").attr("required", "true");
            $("#<%=DDLTipoDocumento.ClientID %>").attr("required", "true");
            $("#<%=txtNumeroDocumento.ClientID %>").attr("required", "true");
            $("#<%=txtFechaNacimiento.ClientID %>").attr("required", "true");
            $("#<%=DDLGrado.ClientID %>").attr("required", "true");
        }
        if (tipoProd == 3 || tipoProd == 7)
        {
            //INPUTS DATOS TITULAR
            $("#<%=txtOncTitApePat.ClientID %>").attr("required", "true");
            $("#<%=txtOncTitApeMat.ClientID %>").attr("required", "true");
            $("#<%=txtOncTitNom.ClientID %>").attr("required", "true");
            $("#<%=ddlOncTitTipoDoc.ClientID %>").attr("required", "true");
            $("#<%=txtOncTitNroDoc.ClientID %>").attr("required", "true");
            $("#<%=txtOncTitFecNac.ClientID %>").attr("required", "true");
            $("#<%=ddlOncTitGrado.ClientID %>").attr("required", "true");

            //INPUTS DATOS CONTRATANTE
            $("#<%=txtOncConApePat.ClientID %>").attr("required", "true");
            $("#<%=txtOncConApeMat.ClientID %>").attr("required", "true");
            $("#<%=txtOncConNom.ClientID %>").attr("required", "true");
            $("#<%=ddlOncConTipoDoc.ClientID %>").attr("required", "true");
            $("#<%=txtOncConNroDoc.ClientID %>").attr("required", "true");
            $("#<%=txtOncConFecNac.ClientID %>").attr("required", "true");
            $("#<%=ddlOncConPaisNac.ClientID %>").attr("required", "true");
            $("#<%=ddlOncConEstcivil.ClientID %>").attr("required", "true");
            $("#<%=ddlOncConDirPais.ClientID %>").attr("required", "true");
            $("#<%=ddlOncConDirDep.ClientID %>").attr("required", "true");
            $("#<%=ddlOncConDirPrv.ClientID %>").attr("required", "true");
            $("#<%=ddlOncConDirDis.ClientID %>").attr("required", "true");
            $("#<%=txtOncConDirEnt.ClientID %>").attr("required", "true");
            $("#<%=txtOncConTelDom.ClientID %>").attr("required", "true");
            $("#<%=txtOncConTelTrab.ClientID %>").attr("required", "true");
            $("#<%=txtOncConNroCel.ClientID %>").attr("required", "true");
            $("#<%=txtOncConEmail.ClientID %>").attr("required", "true");

            //$("#divOncTit").css("display", 'block');
            //$("#divOncCon").css("display", 'block');
        }--%>
    }
</script>
<script>

    function SelectOncConPar(obj) {
        var rb = $("#<%=rblOncConGen.ClientID %> input");
        var radio1 = rb[0];
        var radio2 = rb[1];

        if (obj.value == "1") {
            radio1.checked = true;
            radio2.checked = false;
        }

        if (obj.value == "2") {
            radio1.checked = false;
            radio2.checked = true;
        }
    }

        $(document).ready(function () {

            varid = $("#<%=DDLTipoDocumento.ClientID %>").on('change', function () {
           if ($(varid).val() == 1 || $(varid).val() == 2) {
               //alert($(this).val());
               $("#<%=txtNumeroDocumento.ClientID %>").val('');
             $("#<%=txtNumeroDocumento.ClientID %>").attr('maxlength', 8);

         }
         else {
             $("#<%=txtNumeroDocumento.ClientID %>").val('');
             $("#<%=txtNumeroDocumento.ClientID %>").attr('maxlength', 50);
         }
     });

       //ddpadre
       varid1 = $("#<%=ddlTipoDocuPadre.ClientID %>").on('change', function () {
           if ($(varid1).val() == 1) {
               $("#<%=txtNumDocPadre.ClientID %>").val('');
        $("#<%=txtNumDocPadre.ClientID %>").attr('maxlength', 8);

    }
    else {
        $("#<%=txtNumDocPadre.ClientID %>").val('');
        $("#<%=txtNumDocPadre.ClientID %>").attr('maxlength', 50);
           }

});
       //madreDDLTipoDocMadre
       varid2 = $("#<%=DDLTipoDocMadre.ClientID %>").on('change', function () {
           if ($(varid2).val() == 1) {
               $("#<%=txtNumDocMadre.ClientID %>").val('');
        $("#<%=txtNumDocMadre.ClientID %>").attr('maxlength', 8);

    }
    else {
        $("#<%=txtNumDocMadre.ClientID %>").val('');
        $("#<%=txtNumDocMadre.ClientID %>").attr('maxlength', 50);
    }
       });

       //TDocTit
       varTitTDoc = $("#<%=ddlOncTitTipoDoc.ClientID %>").on('change', function () {
           if ($(varTitTDoc).val() == 1) {
               $("#<%=txtOncTitNroDoc.ClientID %>").val('');
                $("#<%=txtOncTitNroDoc.ClientID %>").attr('maxlength', 8);
            }
            else {
                $("#<%=txtOncTitNroDoc.ClientID %>").val('');
                $("#<%=txtOncTitNroDoc.ClientID %>").attr('maxlength', 50);
            }
       });

       //TDocCon
       varConTDoc = $("#<%=ddlOncConTipoDoc.ClientID %>").on('change', function () {
           if ($(varConTDoc).val() == 1) {
               $("#<%=txtOncConNroDoc.ClientID %>").val('');
                $("#<%=txtOncConNroDoc.ClientID %>").attr('maxlength', 8);
            }
            else {
                $("#<%=txtOncConNroDoc.ClientID %>").val('');
                $("#<%=txtOncConNroDoc.ClientID %>").attr('maxlength', 50);
            }
       });

   })

//function load() {
//    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jsFunctions);
    //}
    function validNumericosTit(evt) {

        if ($(varTitTDoc).val() == 1 || $(varTitTDoc).val() == 2) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (((charCode == 9) || (charCode == 8) || (charCode == 46)
            || (charCode >= 35 && charCode <= 40)
                || (charCode >= 48 && charCode <= 57)
                || (charCode >= 96 && charCode <= 105))) {

                return true;
            }
            else {
                return false;
            }
        }
    }
    function validNumericosCon(evt) {

        if ($(varConTDoc).val() == 1 || $(varConTDoc).val() == 2) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (((charCode == 9) || (charCode == 8) || (charCode == 46)
            || (charCode >= 35 && charCode <= 40)
                || (charCode >= 48 && charCode <= 57)
                || (charCode >= 96 && charCode <= 105))) {

                return true;
            }
            else {
                return false;
            }
        }
    }
function validNumericos(evt) {

    if ($(varid).val() == 1 || $(varid).val() == 2) {

        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode == 9) || (charCode == 8) || (charCode == 46)
        || (charCode >= 35 && charCode <= 40)
            || (charCode >= 48 && charCode <= 57)
            || (charCode >= 96 && charCode <= 105))) {

            return true;
        }
        else {
            return false;
        }
    }
}

//padre
function validNumericos1(evt) {

    if ($(varid1).val() == 1) {

        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode == 9) ||(charCode == 8) || (charCode == 46)
        || (charCode >= 35 && charCode <= 40)
            || (charCode >= 48 && charCode <= 57)
            || (charCode >= 96 && charCode <= 105))) {

            return true;
        }
        else {
            return false;
        }
    }


}
//MADRE
function validNumericos2(evt) {

    if ($(varid2).val() == 1) {

        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode == 9) || (charCode == 8) || (charCode == 46)
        || (charCode >= 35 && charCode <= 40)
            || (charCode >= 48 && charCode <= 57)
            || (charCode >= 96 && charCode <= 105))) {

            return true;
        }
        else {
            return false;
        }
    }

}

    </script>
    <%--validaciones editar--%>
    <script>
        $(document).ready(function () {

            edit = $("#<%=DDLTipoDocEdit.ClientID %>").on('change', function () {
           if ($(edit).val() == 1 || $(edit).val() == 2) {
               //alert($(this).val());
               $("#<%=txtNumDocEdit.ClientID %>").val('');
             $("#<%=txtNumDocEdit.ClientID %>").attr('maxlength', 8);

         }
         else {
             $("#<%=txtNumDocEdit.ClientID %>").val('');
             $("#<%=txtNumDocEdit.ClientID %>").attr('maxlength', 50);
         }
     });

       //ddpadre
       editPadre = $("#<%=DDLPadTipoDocuEdit.ClientID %>").on('change', function () {
           if ($(editPadre).val() == 1) {
               $("#<%=txtPadNumDocEdit.ClientID %>").val('');
               $("#<%=txtPadNumDocEdit.ClientID %>").attr('maxlength', 8);

           }
           else {
               $("#<%=txtPadNumDocEdit.ClientID %>").val('');
               $("#<%=txtPadNumDocEdit.ClientID %>").attr('maxlength', 50);
           }
       });
//beneficiario
       editBenef = $("#<%=DDLBeneficiarioTipoDocuEdit.ClientID %>").on('change', function () {
           if ($(editBenef).val() == 1) {
               $("#<%=txtBeneficiarioNumeroDocuEdit.ClientID %>").val('');
               $("#<%=txtBeneficiarioNumeroDocuEdit.ClientID %>").attr('maxlength', 8);
           }
           else {
               $("#<%=txtBeneficiarioNumeroDocuEdit.ClientID %>").val('');
               $("#<%=txtBeneficiarioNumeroDocuEdit.ClientID %>").attr('maxlength', 50);
           }
       }); 
       //madreDDLTipoDocMadre
<%--       varid2 = $("#<%=DDLTipoDocMadre.ClientID %>").on('change', function () {
           if ($(varid2).val() == 1) {
               $("#<%=txtNumDocMadre.ClientID %>").val('');
        $("#<%=txtNumDocMadre.ClientID %>").attr('maxlength', 8);

    }
    else {
        $("#<%=txtNumDocMadre.ClientID %>").val('');
        $("#<%=txtNumDocMadre.ClientID %>").attr('maxlength', 50);
    }
});--%>
   })

//function load() {
//    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jsFunctions);
//}
function valedit(evt) {

    if ($(edit).val() == 1 || $(edit).val() == 2) {

        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode == 8) || (charCode == 46)
        || (charCode >= 35 && charCode <= 40)
            || (charCode >= 48 && charCode <= 57)
            || (charCode >= 96 && charCode <= 105))) {

            return true;
        }
        else {
            return false;
        }
    }


}
//padre
function valeditPadre(evt) {

    if ($(editPadre).val() == 1) {

        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode == 8) || (charCode == 46)
        || (charCode >= 35 && charCode <= 40)
            || (charCode >= 48 && charCode <= 57)
            || (charCode >= 96 && charCode <= 105))) {

            return true;
        }
        else {
            return false;
        }
    }
}
//beneficiario
function valeditBenef(evt) {

    if ($(editBenef).val() == 1) {

        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode == 8) || (charCode == 46)
        || (charCode >= 35 && charCode <= 40)
            || (charCode >= 48 && charCode <= 57)
            || (charCode >= 96 && charCode <= 105))) {

            return true;
        }
        else {
            return false;
        }
    }


}
//MADRE
//function valeditMadre(evt) {

//    if ($(varid2).val() == 1) {

//        var charCode = (evt.which) ? evt.which : event.keyCode
//        if (((charCode == 8) || (charCode == 46)
//        || (charCode >= 35 && charCode <= 40)
//            || (charCode >= 48 && charCode <= 57)
//            || (charCode >= 96 && charCode <= 105))) {

//            return true;
//        }
//        else {
//            return false;
//        }
//    }

//}

    </script>
 <script>
        function openModalTerminos2() {
            $('#pnlTerminos2').modal('show');
        }
 </script>
<%--kevin--%>

<script>
    $(document).ready(function () {
    $("#<%=chkApAlu.ClientID%>").click(function () {
        if ($(this).prop('checked')) {
            $("#<%=txtApellidoMaterno.ClientID %>").val('');
            $("#<%=txtApellidoMaterno.ClientID%>").attr("disabled", "disabled");
        } else{
           $("#<%=txtApellidoMaterno.ClientID%>").removeAttr("disabled");
        }       
    });

    $("#<%=chkOncTitSoloApe.ClientID%>").click(function () {
        if ($(this).prop('checked')) {
            $("#<%=txtOncTitApeMat.ClientID %>").val('');
            $("#<%=txtOncTitApeMat.ClientID%>").attr("disabled", "disabled");
        } else{
           $("#<%=txtOncTitApeMat.ClientID%>").removeAttr("disabled");
        }       
    });

    $("#<%=chkOncConSoloApe.ClientID%>").click(function () {
        if ($(this).prop('checked')) {
            $("#<%=txtOncConApeMat.ClientID %>").val('');
            $("#<%=txtOncConApeMat.ClientID%>").attr("disabled", "disabled");
        } else{
           $("#<%=txtOncConApeMat.ClientID%>").removeAttr("disabled");
        }       
    });

    });
</script>
    <script>
    $(document).ready(function () {
    $("#<%=chkPaterno.ClientID%>").click(function () {
        if ($(this).prop('checked')) {
             $("#<%=txtApeMatPadre.ClientID %>").val('');
            $("#<%=txtApeMatPadre.ClientID%>").attr("disabled", "disabled");
        } else{
           $("#<%=txtApeMatPadre.ClientID%>").removeAttr("disabled");
        }       
    });
    });
</script> 
      <script>
    $(document).ready(function () {
    $("#<%=chkMaterno.ClientID%>").click(function () {
        if ($(this).prop('checked')) {
            $("#<%=txtApeMatMadre.ClientID %>").val('');
            $("#<%=txtApeMatMadre.ClientID%>").attr("disabled", "disabled");
        } else{
           $("#<%=txtApeMatMadre.ClientID%>").removeAttr("disabled");
        }       
    });
    });
</script>
     <script>
         function openModalcombo() {
            $('#myModalCombo').modal('show');
        }
    </script>  
    <%--finkevin--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
    

    <div class="my-content" style="border-style:solid;background-color:white;border-width:thin;width:100%;height:100%">

              <div class="panel panel-primary">
                              <div class="panel-heading"><h4>Generación de códigos Online - Pasos del 1 al 4</h4></div>
                              <div class="panel-body" style="text-align:center">                                   
                                  <%--  <label for="Nombres" class="col-lg-4 control-label">Nombres</label>--%>
       
               <div class="row">
                      <div class= "col-xs-12 col-sm-12 col-lg-12" style="padding:10px; ">
                          <asp:Image ImageUrl="~/Images/1.png"  runat="server" Width="16%" />
                          <asp:Image ImageUrl="~/Images/2.png"  runat="server" Width="16%"/>
                          <asp:Image ImageUrl="~/Images/3.png"  runat="server" Width="16%" />
                          <asp:Image ImageUrl="~/Images/4.png"  runat="server" Width="16%"/>                          
                      </div>
             </div>
            <div class="row">
                        <br />
                        <div class= "col-xs-12 col-sm-12 col-lg-12" style="background-color:lightgray;height:15px"></div>
           </div>                                                                     
                <asp:MultiView ID="MVWAfiliacion" runat="server" ActiveViewIndex="0" >
                    
                    <asp:View ID="Vw1" runat="server"  >
                        <br />
                         <div class="row">                              
                              <div class="panel panel-primary">
                              <div class="panel-heading" style="background-color:#F88423;padding:3px">
                                  <H5><b>CODIGO DE AFILIACIÓN</b></H5>
                              </div>
                                    <div class="panel-body" style="text-align:center">                                       
                                  <asp:TextBox runat="server" ID="txtCodigo"   required="true" ToolTip="El codigo de afiliacion es obligatorio"  
                                      data-toggle="tooltip" title="El codigo de afiliacion es obligatorio"  Style="padding:8px;text-align:center;font-size:18px" ></asp:TextBox>
                                   <i class="fa fa-question" aria-hidden="true"  data-toggle="tooltip" 
                                      title="Ingrese el código que se encuentra dentro de los comunicados informativos de los seguros, en caso de no contar con ello comunicarse con la institución educativa o a Hermes al número 421 4115."></i>                            
                            </div>
                            </div>
                            </div>                              
                              <div class="row">
                                   <div>
                                              <div class="div_title" style="background-color:lightgray;padding:15px">                                                   
                                                        <asp:Button  runat="server" ID="btnSiguiente" Text="Siguiente >>" CssClass="button"   OnClick="btnSiguiente_Click"  />                                                    
                                              </div>                                           
                                    </div>
                            </div>                                                  
                    </asp:View>
                    
                    <asp:View ID="Vw2" runat="server">
                        <br />
                        <div class="panel panel-primary">
                            <div class="panel-heading" style="background-color: #F88423; padding: 3px">
                                <h5><b>SELECCIONE TIPO DE SEGURO</b></h5>
                            </div>
                            <div class="panel-body" style="text-align: center">
                                <div style="width: 100%; height: 187px; overflow: scroll;">
                                    <asp:UpdatePanel runat="server" ID="pnlUpdProductos">
                                        <ContentTemplate>
                                    <asp:GridView ID="grvSeguros" runat="server" CssClass="Grid2" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="grvSeguros_SelectedIndexChanged" OnRowDataBound="grvSeguros_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkElegir" runat="server" Style="width: 10px" AutoPostBack="True" OnCheckedChanged="chkElegir_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle Width="25px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IDAsociacion"  HeaderText="IdAsociacion">
                                                <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NombreNatural" HeaderText="Institucion Educativa" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Producto" HeaderText="Producto" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Aseguradora" HeaderText="Aseguradora" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="InstitucionEducativaID" HeaderStyle-CssClass="oculto" HeaderText="InstitucionEducativaID" ItemStyle-CssClass="oculto">
                                                <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IdProducto"  HeaderText="IdProducto" >
                                                <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IdAseguradora" HeaderStyle-CssClass="oculto" HeaderText="IdAseguradora" ItemStyle-CssClass="oculto">
                                                <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Prima" HeaderText="Prima" >
                                              <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MonedaId" HeaderText="MonedaId" >
                                              <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>

                                             <asp:BoundField DataField="FechaVigenciaPolizaInicio" HeaderText="FechaVigenciaPolizaInicio" >
                                              <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>

                                             <asp:BoundField DataField="FechaVigenciaPolizaFin" HeaderText="FechaVigenciaPolizaFin" >
                                              <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>

                                                  <asp:BoundField DataField="FechaVigenciaFin" HeaderText="FechaVigenciaFin" >
                                             <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>


                                            <%--<asp:BoundField DataField="FilePlanSeguro" HeaderText="IdSeguro" >
                                              <HeaderStyle CssClass="oculto" />
                                              <ItemStyle CssClass="oculto" /
                                            </asp:BoundField>>--%>

                                             <asp:BoundField DataField="FileNamePlanSeguro" HeaderText="NamePlanSeguro" >
                                             <HeaderStyle CssClass="oculto" />
                                             <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="TipoAsociacion" HeaderText="TipoAsociacion" >
                                             <HeaderStyle CssClass="oculto" />
                                             <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="TipoInstitucionEducativaID" HeaderText="TipoInstitucionEducativaID" >
                                             <HeaderStyle CssClass="oculto" />
                                             <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>
                                            


                                        </Columns>
                                    </asp:GridView>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                </div>                            </div>
                        </div>
                        <div>
                            <div class="div_title" style="background-color: lightgray; padding: 15px">
                                <asp:HiddenField ID="hdnidAseguradora" runat="server" />
                                <asp:Button runat="server" ID="btnSgtePaso2" Text="Siguiente >>" CssClass="button" OnClick="btnSgtePaso2_Click" />
                                <asp:HiddenField ID="hdnProducto" runat="server" />
                                <asp:HiddenField ID="hdnFilaEdit" runat="server" />
                                <asp:HiddenField ID="hdnBeneficiario" runat="server" />
                                <asp:HiddenField ID="hdnAntiguo" runat="server" />                       
                            </div>
                        </div>
                        
                    </asp:View>
                   
                    <asp:View ID="VwDatosAlumnos" runat="server">   
                                             
                            <div class="container-fluid" style="background-color: white;">

                                <div class="form-horizontal" style="background-color: white;padding:4px">
                                    <div class="row" style="background-color: white">
<asp:Panel runat="server" ID="pnlDatosAlumno" Visible="false"> 
                                    <div id="divAlumno" class="col-sm-4" style="background-color: white;">                                      
                                        <div class="panel panel-primary" style="width: 100%">
                                        <div class="panel-heading" style="background-color: #F88423; padding: 1px">
                                            <h5>&nbsp;<strong><asp:Label ID="lblProducto" runat="server" Text=""></asp:Label>
                                                </strong>
                                            </h5>
                                        </div>
                                            <div class="panel-body" style="text-align: left">

                                                <div class="form-group">
                                                    <%--<label for="ejemplo_email_3" class="col-lg-4 control-label">Apellido Paterno</label>--%>
                                                    <div class="col-lg-12" id="search-form">
                                                        <asp:TextBox runat="server" class="form-control term" ID="txtApellidoPaterno" 
                                                            placeholder="Ingrese apellido paterno" Style="text-transform: uppercase" required></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <%--<label for="ApellidoMaterno" class="col-lg-4 control-label">Apellido Materno</label>--%>
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" class="form-control" ID="txtApellidoMaterno" placeholder="Ingrese apellido materno" Style="text-transform: uppercase" required ></asp:TextBox>
                                                    </div>
                                                    </div>   
                                                <asp:CheckBox ID="chkApAlu" runat="server" /><small style="color: #00008B; font:sans-serif"> Si cuenta con un solo apellido, marcar esta opción</small>
                                                    <div class="form-group">
                                                        <%--  <label for="Nombres" class="col-lg-4 control-label">Nombres</label>--%>
                                                        <div class="col-lg-12">
                                                            <asp:TextBox runat="server" class="form-control" ID="txtNombres" placeholder="Ingrese Nombre" Style="text-transform: uppercase" required></asp:TextBox>
                                                        </div>
                                                    </div>

                                                <div class="form-group">
                                                <%--<label for="TipoDocumento" class="col-lg-4 control-label">Tipo Documento</label>--%>
                                                    <div class="col-lg-12">                                 
                                                      
                                                            <asp:DropDownList runat="server" class="form-control" id="DDLTipoDocumento" placeholder="Tipo Documento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" required>
                                                            <asp:ListItem Selected="True" Value="0">Seleccione Tipo de Documento</asp:ListItem>
                                                            </asp:DropDownList>                                                                                                          
                                                </div>
                                                </div> 
                                      
                                                <div class="form-group">
                                                <%--  <label for="NumeroDocumento" class="col-lg-4 control-label">Numero Documento</label>--%>
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" class="form-control" id="txtNumeroDocumento" placeholder="Ingrese número de documento" onkeydown="return validNumericos(event)" required></asp:TextBox>
                                                    </div>
                                                </div>

                                            <div class="form-group">
                                            <%--<label for="Fecha Nacimiento" class="col-lg-4 control-label">Fecha Nacimiento</label>--%>
                                            <div class="col-lg-12">
                                                <asp:TextBox  runat="server" class="form-control" id="txtFechaNacimiento"  data-date-format="dd/mm/yyyy"  placeholder="Ingrese fecha de nacimiento" onfocus="(this.type='date')" required></asp:TextBox>
                                            </div>
                                            </div>

                                        <div class="form-group">
                                        <%--  <label for="CodigoAlumno" class="col-lg-4 control-label">Grado</label>--%>
                                        <div class="col-lg-12">
                                            <asp:DropDownList ID="DDLGrado" runat="server" CssClass="combo" Width="100%" AppendDataBoundItems="True" required>
                                                <asp:ListItem Selected="True" Value="0">Seleccione el Grado</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox runat="server" class="form-control" id="txtFacultad" placeholder="Ingrese Facultad" Style="text-transform: uppercase"/>
                                        </div>
                                        </div>

                                        <div class="form-group">
                                        <%-- <label for="Carrera" class="col-lg-4 control-label">Sección</label>--%>
                                        <div class="col-lg-12">
                                            <asp:TextBox runat="server" class="form-control" id="txtSeccion" placeholder="Ingrese la sección" Style="text-transform: uppercase"/>
                                            <asp:TextBox runat="server" class="form-control" id="txtCarrera" placeholder="Ingrese Carrera" Style="text-transform: uppercase"/>
                                        </div>
                                    </div>

                                        <div class="form-group">                                                                                    
                                            <div class="col-lg-12">Genero
                                                <asp:RadioButtonList runat="server"  id="rbtSexo"  RepeatDirection="Horizontal" >
                                                    <asp:ListItem Text ="&nbsp;Masculino&nbsp;&nbsp;" Enabled="true" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text ="&nbsp;Femenino&nbsp;" Enabled="true" Selected="False"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            </div>
                                        </div>                                                                       
                        </div>
            </div>
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="pnlDatosOncTit" Visible="false"> 
    <div id="divOncTit" class="col-sm-4" style="background-color: white;">                                      
        <div class="panel panel-primary" style="width: 100%">
        <div class="panel-heading" style="background-color: #F88423; padding: 1px">
            <h5>&nbsp;<strong>INGRESE DATOS DEL TITULAR (ESTUDIANTE ASEGURADO)
                </strong>
            </h5>
        </div>
            <div class="panel-body" style="text-align: left">

                <div class="form-group">
                    <div class="col-lg-12">
                        <asp:TextBox runat="server" class="form-control" ID="txtOncTitApePat" 
                            placeholder="Ingrese apellido paterno" Style="text-transform: uppercase" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-lg-12">
                        <asp:TextBox runat="server" class="form-control" ID="txtOncTitApeMat" placeholder="Ingrese apellido materno" Style="text-transform: uppercase" required></asp:TextBox>
                    </div>
                    </div>   
                <asp:CheckBox ID="chkOncTitSoloApe" runat="server" /><small style="color: #00008B; font:sans-serif"> Si cuenta con un solo apellido, marcar esta opción</small>
                    <div class="form-group">
                        <div class="col-lg-12">
                            <asp:TextBox runat="server" class="form-control" ID="txtOncTitNom" placeholder="Ingrese Nombre" Style="text-transform: uppercase" required></asp:TextBox>
                        </div>
                    </div>

                <div class="form-group">
                    <div class="col-lg-12">                                 
                                                      
                            <asp:DropDownList runat="server" class="form-control" id="ddlOncTitTipoDoc" placeholder="Tipo Documento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" required>
                            <asp:ListItem Selected="True" Value="0">Seleccione Tipo de Documento</asp:ListItem>
                            </asp:DropDownList>                                                                                                          
                </div>
                </div> 
                                      
                <div class="form-group">
                    <div class="col-lg-12">
                        <asp:TextBox runat="server" class="form-control" id="txtOncTitNroDoc" placeholder="Ingrese número de documento" onkeydown="return validNumericosTit(event)" required></asp:TextBox>
                    </div>
                </div>

            <div class="form-group">
            <div class="col-lg-12">
                <asp:TextBox  runat="server" class="form-control" id="txtOncTitFecNac"  data-date-format="dd/mm/yyyy"  placeholder="Ingrese fecha de nacimiento" onfocus="(this.type='date')" onblur="onblur_OncTitFecNac();" required></asp:TextBox>
                <asp:HiddenField ID="hdnOncTitFecNac" runat="server" Value="0" />
            </div>
            </div>

        <div class="form-group">
        <div class="col-lg-12">
            <asp:DropDownList ID="ddlOncTitGrado" runat="server" CssClass="combo" Width="100%" AppendDataBoundItems="True" required>
                <asp:ListItem Selected="True" Value="0">Seleccione el Grado</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" class="form-control" id="txtOncTitFacultad" placeholder="Ingrese Facultad" Style="text-transform: uppercase"/>                                                                                             
        </div>
        </div>

        <div class="form-group">
        <div class="col-lg-12">
            <asp:TextBox runat="server" class="form-control" id="txtOncTitCarrera" placeholder="Ingrese Carrera" Style="text-transform: uppercase"/>                                                                                             
        </div>
        </div>

        <div class="form-group">                                                                                    
            <div class="col-lg-12">Genero
                <asp:RadioButtonList runat="server"  id="rblOncTitGenero"  RepeatDirection="Horizontal" >
                    <asp:ListItem Text ="&nbsp;Masculino&nbsp;&nbsp;" Enabled="true" Selected="True"></asp:ListItem>
                    <asp:ListItem Text ="&nbsp;Femenino&nbsp;" Enabled="true" Selected="False"></asp:ListItem>
            </asp:RadioButtonList>
            </div>
        </div>    
        <%--<div class="form-group" style="height:75px;">                                                                                    
        </div>  --%>
</div>
</div>
</div>
</asp:Panel>
<asp:Panel runat="server" ID="pnlDatosOncCon" Visible="false"> 
    <div id="divOncCon" class="col-sm-8" style="background-color: white;">                                      
        <div class="panel panel-primary" style="width: 100%">
            <div class="panel-heading" style="background-color: #F88423; padding: 1px">
                <h5>&nbsp;<strong>DATOS DEL CONTRATANTE (SI EL ESTUDIANTE ES MENOR DE EDAD DEBE INGRESAR LOS DATOS DE SU APODERADO)
                    </strong>
                </h5>
            </div> 
            <div class="panel-body" style="text-align: left">    
                <div class="form-group" id="divOncConPar" style="display:none;">
                    <div class="col-lg-2"><span>Parentesco:</span> </div>
                    <div class="col-lg-10">
                        <asp:RadioButtonList ID="rblOncConPar" runat="server" RepeatDirection="Horizontal" Width="191px" >
                            <asp:ListItem Selected="True" Value="1" onclick="SelectOncConPar(this);">Padre</asp:ListItem>
                            <asp:ListItem Value="2" onclick="SelectOncConPar(this);">Madre</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div> 
                <div class="form-group">
                    <div class="col-lg-6">
                        <asp:HiddenField ID="hdnOncConApePat" runat="server" />
                        <asp:TextBox runat="server" class="form-control" ID="txtOncConApePat" 
                            placeholder="Ingrese apellido paterno" Style="text-transform: uppercase" onchange="onchange_OncConApePat(event);" required></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:HiddenField ID="hdnOncConApeMat" runat="server" />
                        <asp:TextBox runat="server" class="form-control" ID="txtOncConApeMat" placeholder="Ingrese apellido materno" Style="text-transform: uppercase" onchange="onchange_OncConApeMat(event);" required></asp:TextBox>                        
                        <asp:CheckBox ID="chkOncConSoloApe" runat="server" /><small style="color: #00008B; font:sans-serif"> Si cuenta con un solo apellido, marcar esta opción</small>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-6">
                        <asp:HiddenField ID="hdnOncConNom" runat="server" />
                        <asp:TextBox runat="server" class="form-control" ID="txtOncConNom" placeholder="Ingrese Nombre" Style="text-transform: uppercase" onchange="onchange_OncConNom(event);" required></asp:TextBox>
                    </div>
                    <div class="col-lg-6">           
                        <asp:HiddenField ID="hdnOncConFecNac" runat="server" />
                        <asp:TextBox  runat="server" class="form-control" id="txtOncConFecNac"  data-date-format="dd/mm/yyyy"  placeholder="Ingrese fecha de nacimiento" onfocus="(this.type='date')" onchange="onchange_OncConFecNac(event);" required></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-6">
                        <asp:HiddenField ID="hdnOncConTipoDoc" runat="server" />
                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConTipoDoc" placeholder="Tipo Documento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" onchange="onchange_OncConTipoDoc(event);" required>
                        <asp:ListItem Selected="True" Value="0">Seleccione Tipo de Documento</asp:ListItem>
                        </asp:DropDownList>                                                                                                          
                    </div>
                    <div class="col-lg-6">
                        <asp:HiddenField ID="hdnOncConNroDoc" runat="server" />
                        <asp:TextBox runat="server" class="form-control" id="txtOncConNroDoc" placeholder="Ingrese número de documento" onkeydown="return validNumericosCon(event)" onchange="onchange_OncConNroDoc(event);" required></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-6">
                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConEstcivil" placeholder="Estado Civil"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" required>
                        <asp:ListItem Selected="True" Value="0">Seleccione el Estado Civil</asp:ListItem>
                            <asp:ListItem Value="1">Soltero</asp:ListItem>
                            <asp:ListItem Value="2">Casado</asp:ListItem>
                            <asp:ListItem Value="3">Conviviente</asp:ListItem>
                            <asp:ListItem Value="4">Divorciado</asp:ListItem>
                            <asp:ListItem Value="5">Viudo</asp:ListItem>
                            <asp:ListItem Value="6">Otro</asp:ListItem>
                        </asp:DropDownList>    
                    </div>
                    <div class="col-lg-6">           
<%--                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConPaisNac" placeholder="Pais de Nacimiento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" required>
                            <asp:ListItem Selected="True" Value="0">Seleccione el Pais de Nacimiento</asp:ListItem>
                            <asp:ListItem Value="1">Peru</asp:ListItem>
                        </asp:DropDownList>  --%>
                        <asp:TextBox  runat="server" class="form-control" id="txtOncConPaisNac"  placeholder="Pais de Nacimiento" required Text="PERU"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">                                                                                    
                    <div class="col-lg-6">
                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConDirPais" placeholder="Direccion Pais"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" required>
                            <asp:ListItem Value="0">Seleccione la Direccion Pais</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Peru</asp:ListItem>
                        </asp:DropDownList> 
                    </div>
                    <div class="col-lg-6">
                     <asp:UpdatePanel runat="server" ID="updPnlConDirDep">
                     <ContentTemplate>
                         <asp:DropDownList runat="server" class="form-control" id="ddlOncConDirDep" placeholder="Direccion Departamento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlOncConDirDep_SelectedIndexChanged" required>
                        <asp:ListItem Selected="True" Value="0">Seleccione la Direccion Departamento</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </div>   
                <div class="form-group">                                                                                    
                    <div class="col-lg-6">
                     <asp:UpdatePanel runat="server" ID="updPnlConDirPrv">
                     <ContentTemplate>
                          <asp:DropDownList runat="server" class="form-control" id="ddlOncConDirPrv" placeholder="Direccion Provincia"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlOncConDirPrv_SelectedIndexChanged" required>
                        <asp:ListItem Selected="True" Value="0">Seleccione la Direccion Provincia</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-6">
                     <asp:UpdatePanel runat="server" ID="updPnlConDirDis">
                     <ContentTemplate>
                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConDirDis" placeholder="Direccion Distrito"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" required>
                        <asp:ListItem Selected="True" Value="0">Seleccione la Direccion Distrito</asp:ListItem>
                        </asp:DropDownList> 
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </div>   
                <div class="form-group">                                                                                    
                    <div class="col-lg-6">
                        <asp:TextBox  runat="server" class="form-control" id="txtOncConDirEnt"  placeholder="Ingrese la Direccion de Entrega" required></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:TextBox  runat="server" class="form-control" id="txtOncConTelDom"  placeholder="Ingrese el Telefono de Domicilio"></asp:TextBox>
                    </div>
                </div>  
                <div class="form-group">                                                                                    
                    <div class="col-lg-6">
                        <asp:TextBox  runat="server" class="form-control" id="txtOncConTelTrab"  placeholder="Ingrese el Telefono de Trabajo"></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:TextBox  runat="server" class="form-control" id="txtOncConNroCel"  placeholder="Ingrese el Numero de Celular" required></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">                                                                                    
                    <div class="col-lg-6">
                        <asp:TextBox  runat="server" class="form-control" id="txtOncConEmail"  placeholder="Ingrese el Email" required></asp:TextBox>
                    </div>
                    <div class="col-lg-6">Genero
                        <asp:RadioButtonList runat="server"  id="rblOncConGen"  RepeatDirection="Horizontal" >
                            <asp:ListItem Text ="&nbsp;Masculino&nbsp;&nbsp;" Enabled="true" Selected="True"></asp:ListItem>
                            <asp:ListItem Text ="&nbsp;Femenino&nbsp;" Enabled="true" Selected="False"></asp:ListItem>
                    </asp:RadioButtonList>
                    </div>
                </div>
            </div>                        
        </div>
    </div> 
</asp:Panel>
      <asp:Panel runat="server" ID="pnlDatosPadre" Visible="false"> 
      <div class="col-sm-4" style="background-color: white;">      
                      <div class="panel panel-primary" style="width:100%" >
                         <div class="panel-heading" style="background-color:#F88423;font-weight:bold;padding:1px"><h5>RENTA ESTUDIANTIL - DATOS DE PADRES &nbsp;</h5></div>                                 
                           <div class="panel-body" style="text-align:left">                                                            
                                             <div class="panel panel-primary">
                                            <div class="panel-heading">PRIMER PADRE ASEGURADO</div>
                                            <div class="panel-body">
                                  <div class="form-group">
                                      <%--label for="ejemplo_email_3" class="col-lg-4 control-label">Apellido Paterno</label>--%>
                                      <div class="col-lg-12">
                                          <asp:TextBox runat="server" class="form-control" id="txtApePadre"    placeholder="Ingrese apellido paterno" Style="text-transform: uppercase"  required></asp:TextBox>
                                      </div>
                                  </div>
                                  <div class="form-group">
                                     <%-- <label for="ApellidoMaterno" class="col-lg-4 control-label">Apellido Materno</label>--%>
                                      <div class="col-lg-12">
                                           <asp:TextBox runat="server" class="form-control" id="txtApeMatPadre"   placeholder="Ingrese apellido materno" Style="text-transform: uppercase"  required></asp:TextBox>
                                      </div>
                                      </div>
                                         <asp:CheckBox ID="chkPaterno" runat="server" /><small style="color: #00008B; font:sans-serif"> Si cuenta con un solo apellido, marcar esta opción</small>
                                      <div class="form-group">
                                        <%--  <label for="Nombres" class="col-lg-4 control-label">Nombres</label>--%>
                                          <div class="col-lg-12">
                                               <asp:TextBox runat="server" class="form-control" id="txtNomPadre" placeholder="Ingrese Nombre" Style="text-transform: uppercase"  required></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                 <%--         <label for="TipoDocumento" class="col-lg-4 control-label">Tipo Documento</label>--%>
                                          <div class="col-lg-12">
                                               <asp:DropDownList runat="server" class="form-control" id="ddlTipoDocuPadre" placeholder="Tipo Documento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True"  required>
                                                   <asp:ListItem Selected="True" Value="0">Seleccione Tipo de Documento</asp:ListItem>
                                               </asp:DropDownList>
                                          </div>
                                      </div>
                                      
                                      <div class="form-group">
                                        <%--  <label for="NumeroDocumento" class="col-lg-4 control-label">Numero Documento</label>--%>
                                          <div class="col-lg-12">                                               
                                               <asp:TextBox runat="server" class="form-control" id="txtNumDocPadre" placeholder="Ingrese número de documento" onkeydown="return validNumericos1(event)" required></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                        <%--  <label for="Fecha Nacimiento" class="col-lg-4 control-label">Fecha Nacimiento</label>--%>
                                          <div class="col-lg-12">
                                              <asp:TextBox ID="txtFechaNacPadre" runat="server" class="form-control"  data-date-format="dd/mm/yyyy" onfocus="(this.type='date')" placeholder="Ingrese fecha de nacimiento" required  ></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <%--<label for="CodigoAlumno" class="col-lg-4 control-label">Parentesco</label>--%>
                                          <div class="col-lg-10">
                                              Parentesco
                                              <asp:RadioButtonList ID="rbtTipoPadre" runat="server" Height="19px" RepeatDirection="Horizontal" Width="191px">
                                                  <asp:ListItem Selected="True" Value="1">Padre</asp:ListItem>
                                                  <asp:ListItem Value="2">Madre</asp:ListItem>
                                              </asp:RadioButtonList>
                                          </div>
                                      </div>                                       
                                      </div>                                  
                                  </div>
                              </div>
                       </div>                                                                          
          </div>          
          </asp:Panel>
                                        

<asp:Panel runat="server" ID="pnlDatoMadre" Visible="false">            
      <div class="col-sm-4" style="background-color: white;">      
            <div class="panel panel-primary" style="width:100%" >
                         <div class="panel-heading" style="background-color:#F88423;font-weight:bold;padding:1px"><h5>RENTA ESTUDIANTIL - DATOS DE PADRES <asp:CheckBox ID="chkSegundoPadre" runat="server"  Text="SI" OnCheckedChanged="chkSegundoPadre_CheckedChanged" AutoPostBack="true"/>
                             </h5></div>                                        
                             <asp:Panel runat="server" ID="pnlPadre2"  Enabled="false">

                                      <div class="panel-body" style="text-align:left">                                        
                                             <div class="panel panel-primary">
                                                 <div class="panel-heading">SEGUNDO PADRE ASEGURADO</div>
                                                        <div class="panel-body">                                                                                                                                                                                                                                                                                                                                                                                                              
                                                       <div class="form-group">

                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" class="form-control" ID="txtApePatMadre" placeholder="Ingrese apellido paterno" Style="text-transform: uppercase"  required></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                   <%-- <label for="ApellidoMaterno" class="col-lg-4 control-label">Apellido Materno</label>--%>
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" class="form-control" ID="txtApeMatMadre" placeholder="Ingrese apellido materno"  Style="text-transform: uppercase" required></asp:TextBox>
                                                    </div>
                                                </div>
                                         <asp:CheckBox ID="chkMaterno" runat="server" /><small style="color: #00008B; font:sans-serif"> Si cuenta con un solo apellido, marcar esta opción</small>
                                      <div class="form-group">
                                        <%--  <label for="Nombres" class="col-lg-4 control-label">Nombres</label>--%>
                                          <div class="col-lg-12">
                                               <asp:TextBox runat="server" class="form-control" id="txtNombreMadre" placeholder="Ingrese Nombre" Style="text-transform: uppercase"  required></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <%--<label for="TipoDocumento" class="col-lg-4 control-label">Tipo Documento</label>--%>
                                          <div class="col-lg-12">
                                               <asp:DropDownList runat="server" class="form-control" id="DDLTipoDocMadre" placeholder="Tipo Documento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" required>
                                                   <asp:ListItem Selected="True" Value="0">Seleccione Tipo de Documento</asp:ListItem>
                                               </asp:DropDownList>
                                          </div>
                                      </div>
                                      
                                      <div class="form-group">
                                          <%--<label for="NumeroDocumento" class="col-lg-4 control-label">Numero Documento</label>--%>
                                          <div class="col-lg-12">
                                               <asp:TextBox runat="server" class="form-control" id="txtNumDocMadre" placeholder="Ingrese número de  documento" onkeydown="return validNumericos2(event)" required></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <%--<label for="Fecha Nacimiento" class="col-lg-4 control-label">Fecha Nacimiento</label>--%>
                                          <div class="col-lg-12">
                                               <asp:TextBox  runat="server" class="form-control"  onfocus="(this.type='date')" id="txtFechaNacMadre" placeholder="Fecha de Nacimiento" required    data-date-format="dd/mm/yyyy" ></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <%--<label for="CodigoAlumno" class="col-lg-4 control-label">Parentesco</label>--%>
                                          <div class="col-lg-12">Parentesco
                                              <asp:RadioButtonList ID="rbtParentescoMadre" runat="server" Height="19px" RepeatDirection="Horizontal" Width="191px" >
                                                  <asp:ListItem Value="1">Padre</asp:ListItem>
                                                  <asp:ListItem Value="2" Selected="True">Madre</asp:ListItem>
                                              </asp:RadioButtonList>
                                          </div>                                          
                                        </div>                                                                                     
                                      </div>                                                                                   
                                  </div>                                                                                                              
                         </div>     
                                 </asp:Panel>
                   </div>            
            </asp:Panel>
                                        
            </div>
          </div>                                                                                                                                                                                                                                       
         </div>           
                        
        <br />
         <div class="col-lg-12" style="background-color: #273746; padding: 10px">
             <div>
                  <asp:Button runat="server" ID="btnSgtePaso3" Text="Siguiente >>" CssClass="button" OnClick="btnSgtePaso3_Click"  Style="background-color:orangered"/>
                 <asp:HiddenField ID="hdnEdad" runat="server" />
                 <asp:HiddenField ID="hdnProductoId" runat="server" />
                 <asp:HiddenField ID="hdnPrima" runat="server" />
                 <asp:HiddenField ID="hdnAsociacionId" runat="server" />
                 <asp:HiddenField ID="hdnInstitucionEducativa" runat="server" />
                  <asp:HiddenField ID="hdnPadreId" runat="server" />

                 </div>
         </div>                            
       </asp:View>                    
                    
                    <asp:View ID="VwConfirmar" runat="server">
                        <br />
                        <div class="panel panel-primary">
                        <div class="panel-heading" style="background-color:#F88423;padding:3px"><h5><b>CONFIRMAR AFILIACIÓN</b></h5></div>
                        <div class="panel-body">
                        <div style="width: 100%;  overflow: scroll;">
                            <asp:GridView ID="GrvConfirmar" runat="server" CssClass="Grid2" AutoGenerateColumns="False" Width="100%" 
                                OnRowDataBound="GrvConfirmar_RowDataBound" ShowFooter="True" EmptyDataText="Sin registros"  Font-Size="Small"
                                ShowHeaderWhenEmpty="True" >
                            <Columns>
                                <asp:BoundField DataField="Asegurado" HeaderText="Asegurado" HtmlEncode="False" >
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="210px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="beneficiario" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Beneficiario" HtmlEncode="False" >
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NroDoc" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Nro.Doc">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecnac" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Fec.Nac">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Producto" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Seguro"  HtmlEncode="False" >
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="300px" />
                                </asp:BoundField>
                              
                                <asp:BoundField DataField="TipoMoneda" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="">
                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="35px" HorizontalAlign="Right" />
                                  </asp:BoundField> 
                                 <asp:BoundField DataField="Prima" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Prima">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                               <%--   <asp:TemplateField HeaderText="prima">
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblSoles" runat="server" Text="Soles"></asp:Label>
                                                                                    <br>                                                                                    
                                                                                    <asp:Label ID="LblDolares" runat="server" Text="Dolares"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPrima" runat="server" Text='<%# Eval("Prima") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="100px" />
                                </asp:TemplateField>--%>

                                <asp:BoundField DataField="CiaSeguros" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="CIA.Seguros" HtmlEncode="False" >
                               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Plan" ItemStyle-Width="40px">
                                    <ItemTemplate>
                                       <%-- <a href='<%# Eval("plan")%>'>
                                        <image height="16px" src="images/plan.png" Tooltip="Descargar plan" width="16px" />
                                        </a>--%>
                                        <asp:ImageButton ID="btnPlan" runat="server" width="16px" ImageUrl="~/Images/DescargaPDF.png" OnClick="btnPlan_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acepto Términos y Condiciones" ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAcepto" runat="server" Checked='<%# Convert.ToBoolean( Eval("Acepto"))%>' Style="width:10px" />
                                        <asp:ImageButton ID="btnVerTerminos" runat="server" data-target="#PnlTerminoCond" data-toggle="modal" Height="16px" ImageUrl="./images/plan.png" OnClick="btnVerTerminos_Click" width="16px" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="oculto" HorizontalAlign="Center" Width="25px" />
                                    <HeaderStyle CssClass="oculto" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Editar" ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" data-toggle="modal" Height="16px" ImageUrl="~/Images/edit.png" OnClick="btnEditar_Click" width="16px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" Height="16px" ImageUrl="~/Images/deletes.png" OnClick="btnEliminar_Click" width="16px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="IdAsegurado" HeaderText="IdAsegurado" HeaderStyle-CssClass="oculto" >
                                     <HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="IdMoneda" HeaderText="CodMonenda">
                                 <HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="NombreColegio" HeaderText="Nombre Colegio" >
                                <HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />
                                </asp:BoundField>

                                  <asp:BoundField DataField="IdProducto" HeaderText="IdProducto" >
                              <HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="IdAsociacion" HeaderText="IdAsociacion" >
                              <HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />
                                </asp:BoundField>
                                

                                 <%--<asp:BoundField DataField="FilePlanSeguro" HeaderText="IdSeguro" >
                                              <HeaderStyle CssClass="oculto" />
                                              <ItemStyle CssClass="oculto" />
                                            </asp:BoundField>--%>

                             <asp:BoundField DataField="FileNamePlanSeguro" HeaderText="NamePlanSeguro">
                                    <HeaderStyle CssClass="oculto" />
                                    <ItemStyle CssClass="oculto" />
                               </asp:BoundField>

                           <%--<asp:BoundField DataField="FileNamePlanSeguro" HeaderText="NamePlanSeguro">
                                    <HeaderStyle CssClass="oculto" />
                                    <ItemStyle CssClass="oculto" />
                            </asp:BoundField>--%>
                          <asp:BoundField DataField="IdPadre" HeaderText="IDPadre">
                                <HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />
                         </asp:BoundField>

                       <asp:BoundField DataField="IdHijo" HeaderText="IDHijo">
                                <HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />
                         </asp:BoundField>

                        <asp:BoundField DataField="Id" HeaderText="Id">
                                <%--<HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />--%>
                         </asp:BoundField>

                                <asp:BoundField DataField="Ide" HeaderText="Ide">
                              <%--  <HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />--%>
                         </asp:BoundField>

                            </Columns>
                                <EmptyDataTemplate>
                                    <div class="etiquetaTitulo">
                                    </div>
                                </EmptyDataTemplate>
                        </asp:GridView>                            
                        </div>

                        <div>
                            <div class="div_title" style="background-color: #D6EAF8;color:#0B243B;padding: 10px">
                                <div>
                                    Estimado usuario, usted debe leer y aceptar los términos y condiciones de los seguros a contratar a fin de generar los respectivos recibos de pagos, sírvase verificar la información contenida en el presente resumen a fin de no generar inconvenientes. 
                                    <asp:LinkButton ID="lnkbtnTerminos" runat="server" OnClick="lnkbtnTerminos_Click" >Leer mas... <span class="glyphicon glyphicon-info-sign"></span></asp:LinkButton>
                                </div>                               
                              </div>
                             <div  style="padding:4px">
                                    <asp:CheckBox runat="server" ID="chkAcepto" Text="Acepto los términos y condiciones"   Checked="false" />                                
                                </div>

                               <div class="div_title" style="padding:12px">
                                    <asp:Button runat="server" ID="BtnFinalizar" Text="Finalizar" CssClass="button"  OnClick="BtnFinalizar_Click" />
                                    <asp:Button runat="server" ID="BtnAsegOtro" Text="Registrar un  nuevo asegurado" CssClass="button"  OnClick="BtnAsegOtro_Click" Width="220px" BackColor="OrangeRed" />
                                
                                </div>                                                          
                            </div>
                            </div>
                            </div>                                                                                
                    </asp:View>                                    
                </asp:MultiView>
                  
              </div>              
            </div>  
    
    
    
        <div style="display:none">            
           <div style="display:none">
            <rsweb:ReportViewer ID="RPVBoletaDeAfiliados" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="BoletaDePagosAfiliados.rdlc">              
            </LocalReport>
            </rsweb:ReportViewer>


        <rsweb:ReportViewer ID="RPVBoletaRentaAfiliados" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="BoletaDePagosRentaAfiliados.rdlc">              
            </LocalReport>
        </rsweb:ReportViewer>
            </div>

            </div>

             
    <div id="myModal" class="modal fade" role="dialog">  
            <div class="modal-dialog" >             
                     <div class="modal-content">
                                 <div class="modal-header" style="background-color:#D6EAF8">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>  
                                          <h4 class="modal-title" style="background-color:#D6EAF8" >Hermes | Seguros</h4>
                                </div>
                                <div class="modal-body">
                                            <div class="msgcentrado">
                                                    <asp:Label Text="" ID="txtmensaje" runat="server"  Style="font-size:18px"></asp:Label>
                                             </div>
                                </div>                   
                               <div class="modal-footer">                                      
                                        <asp:Button  runat="server" ID="btnCerrarModal" Text="Cerrar" CssClass="button" OnClick="btnCerrarModal_Click"  formnovalidate />                                                    
                               </div>
                   </div>
         </div>
  </div>

    <div id="myModalFinalizar" class="modal fade" role="dialog">  
            <div class="modal-dialog" >             
                     <div class="modal-content">
                                 <div class="modal-header" style="background-color:#D6EAF8">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>  
                                          <h4 class="modal-title" style="background-color:#D6EAF8" >Hermes | Seguros</h4>
                                </div>
                                <div class="modal-body">
                                            <div class="msgcentrado">
                                                    <asp:Label Text="" ID="lblFinalizar" runat="server"  Style="font-size:18px"></asp:Label>
                                             </div>
                                </div>                   
                               <div class="modal-footer">                                      
                                        <asp:Button  runat="server" ID="btnFinalizarEstadia" Text="Cerrar" CssClass="button" OnClick="btnFinalizarEstadia_Click"  />                                                    
                               </div>
                   </div>
         </div>
  </div>



    <div id="PnlTerminoCond" class="modal fade" role="dialog" style="height: 700px">
        <div class="modal-dialog" style="height: 600px">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Terminos y condiciones</h4>
                </div>
                <div class="modal-body" style="overflow: scroll; height: 500px">
                    <div class="msgcentrado">
                        <asp:Label Text="" ID="lblTerminos" runat="server" Style="font-size: 14px; text-align: justify"></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">                    
                         <asp:Button  runat="server" ID="btncerrarTerm" Text="Siguiente >>" CssClass="button"    />                                                    
                </div>
            </div>
        </div>
    </div>

    <div id="pnlEditarAlumno" class="modal fade" role="dialog">
            <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                            <div class="modal-header" style="background-color:#D6EAF8">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title" style="background-color:#D6EAF8" >Editar asegurado</h4>
                            </div>
                            <div class="modal-body" >
                                     <div class="container-fluid">
                                     <div class="form-group">
                                      <label for="ejemplo_email_3" class="col-lg-2 control-label">Ape.Paterno</label>
                                      <div class="col-lg-10">
                                          <asp:TextBox runat="server" class="form-control" id="TxtApePateEdit"   placeholder="Apellido Paterno"/>
                                      </div>
                                  </div>
                                  <div class="form-group">
                                      <label for="ApellidoMaterno" class="col-lg-2 control-label">Ape.Materno</label>
                                      <div class="col-lg-10">
                                           <asp:TextBox runat="server" class="form-control" id="TxtApeMateEdit"    placeholder="Apellido Materno"/>
                                      </div>
                                      <div class="form-group">
                                          <label for="Nombres" class="col-lg-2 control-label">Nombres</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="TxtNombresEdit" placeholder="Nombres"/>
                                          </div>
                                      </div>
                                          <div class="form-group">
                                              <label for="NumeroDocumento" class="col-lg-2 control-label">TipoDocumento</label>
                                              <div class="col-lg-10">
                                                <asp:DropDownList runat="server" class="form-control" id="DDLTipoDocEdit" placeholder="Tipo Documento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True">
                                                                <asp:ListItem Selected="True" Value="0">Seleccione Tipo de Documento</asp:ListItem>
                                                               </asp:DropDownList>             
                                      </div>
                                      </div>
                                      
                                      <div class="form-group">
                                          <label for="NumeroDocumento" class="col-lg-2 control-label">Numero</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" ID="txtNumDocEdit" placeholder="Numero Documento" onkeydown="return valedit(event)"/>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <label for="Fecha Nacimiento" class="col-lg-2 control-label">Fec.Nac</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox  runat="server" class="form-control" id="txtFechaNacEdit" placeholder="Fecha de Nacimiento" type="date"  onfocus="(this.type='date')"  data-date-format="dd/mm/yyyy" />
                                          </div>
                                      </div>

                                       <%--<div class="form-group">
                                          <label for="Grado que cursara" class="col-lg-2 control-label">Grado</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox  runat="server" class="form-control" id="txtGradoEdit" placeholder="Grado que cursara"   />
                                          </div>
                                      <>--%>

                                         <div class="form-group">
                                              <label for="Grado que cursara" class="col-lg-2 control-label">Grado</label>
                                             <div class="col-lg-10">
                                                <asp:DropDownList runat="server" class="form-control" id="ddlGradoEdit" placeholder="Grado"  CssClass="combo"  Width="100%" AppendDataBoundItems="True">
                                                                <asp:ListItem Selected="True" Value="0">Seleccione Grado</asp:ListItem>
                                                               </asp:DropDownList>
                                                 <asp:TextBox  runat="server" class="form-control" id="txtFacultadEdit" placeholder="Facultad" />
                                            </div>
                                      </div>

                                       <div class="form-group">
                                          <label for="Seccion" class="col-lg-2 control-label">Sección</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox  runat="server" class="form-control" id="txtSeccionEdit" placeholder="Sección" />
                                              <asp:TextBox  runat="server" class="form-control" id="txtCarreraEdit" placeholder="Carrera" />
                                          </div>
                                  </div>
                                     
                                     <%-- <div class="form-group"  style="">
                                          <label for="Genero" class="col-lg-2 control-label">Genero</label>
                                          <div class="col-lg-10">
                                               <asp:RadioButtonList runat="server"  id="rbtSexoEdit"  RepeatDirection="Horizontal" >
                                                        <asp:ListItem Text ="&nbsp;Masculino&nbsp;&nbsp;" Enabled="true" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text ="&nbsp;Femenino&nbsp;" Enabled="true" Selected="False"></asp:ListItem>
                                               </asp:RadioButtonList>
                                          </div>
                                      </div>--%>
                                            
                                    </div>
                            </div>
                            <div class="modal-footer">
                                    <%--<asp:Button   class="btn btn-facebook" runat="server"  ID="btnActualizar" Text="Guardar" OnClick="btnActualizar_Click"  />--%>
                                    <asp:Button ID="btnEditarAlumnos" runat="server" Text="Guardar"  class="button" OnClick="btnEditarAlumnos_Click"/>                                    
                                     <asp:Button  runat="server" ID="btncerrarEditar" Text="Cerrar" CssClass="button"  Style="background-color:orangered"   />                                                    
                            </div>
                  </div>      
               </div>                    
            </div>
    </div>  

    <div id="myConfirm" class="modal fade" role="dialog">      
            <div class="modal-dialog" >             
                 <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="background-color: #D6EAF8">
                        <asp:Label Text="" ID="lblTitleConfirm" runat="server" Style="font-size: 18px"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div class="msgcentrado">
                        <asp:Label Text="" ID="lblmsgConfirm" runat="server" Style="font-size: 18px"></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Confirmar" ID="btnConfirmar" runat="server" CssClass="button"  OnClick="btnConfirmar_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="False" />                    
                    <asp:Button Text="Cerrar" ID="btncerrarc" runat="server"  CssClass="button"  Style="background-color:orangered" />
                </div>
            </div>
        </div>
    </div>
         
    <div id="myPreg" class="modal fade" role="dialog">      
            <div class="modal-dialog" style="width:650px;">             
                 <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="background-color: #D6EAF8">
                        <asp:Label Text="" ID="lblTitlePreg" runat="server" Style="font-size: 18px"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div>
                        <asp:Label Text="" ID="lblPreg" runat="server" Style="font-size: 18px"></asp:Label>
                        <br />
                        <asp:RadioButtonList ID="rblDscPrg" runat="server" RepeatDirection="Horizontal" Width="191px" CssClass="form-control-static">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div>
                        <br />
                        <asp:Label Text="" ID="lblPregFuma" runat="server" Style="font-size: 18px"></asp:Label>                        
                        <br />
                        <asp:RadioButtonList ID="rblDscPrgFuma" runat="server" RepeatDirection="Horizontal" Width="191px" CssClass="form-control-static">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Grabar Respuesta" ID="btnAceptaResPreg" runat="server" CssClass="button"  OnClick="btnAceptaResPreg_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="False" />                    
                    <asp:Button Text="Cerrar" ID="btnCerrarResPreg" runat="server"  CssClass="button"  Style="background-color:orangered" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

       <div id="pnlEditarPadre" class="modal fade" role="dialog"  >
            <div class="modal-dialog modal-lg">                
                            <div class="modal-content">
                                    <div class="modal-header" style="background-color:#D6EAF8">
                                               <button type="button" class="close" data-dismiss="modal">&times;</button>
                                               <h4 class="modal-title" style="background-color:#D6EAF8" >Editar datos de Asegurado - Beneficiario</h4>
                                    </div>
                
                                    <div class="row"   style="padding:8px">                                                                    
                                      <div class="col-lg-6"  >                                      
                                                                                                                   
                                     <div class="panel panel-default">
                                            <div class="panel-heading">Datos del Padre Asegurado</div>
                                            <div class="panel-body">
                                          
                                                                              


                                     <div class="form-group">
                                                <label for="ejemplo_email_4" class="col-lg-5 control-label">Apellido Paterno</label>
                                                <div class="col-lg-7">
                                                        <asp:TextBox runat="server" class="form-control" id="txtPadApePaternoEdit" Style="text-transform: uppercase"  placeholder="Apellido Paterno"/>
                                                </div>
                                  </div>

                                  <div class="form-group">
                                      <label for="ApellidoMaterno" class="col-lg-5 control-label">Apellido Materno</label>
                                      <div class="col-lg-7">
                                           <asp:TextBox runat="server" class="form-control" id="txtPadApeMaternoEdit"  Style="text-transform: uppercase"  placeholder="Apellido Materno"/>
                                      </div>
                                 </div>

                                      <div class="form-group">
                                          <label for="Nombres" class="col-lg-5 control-label">Nombres</label>
                                          <div class="col-lg-7">
                                               <asp:TextBox runat="server" class="form-control" id="txtPadNombresEdit" Style="text-transform: uppercase" placeholder="Nombres"/>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <label for="TipoDocumento" class="col-lg-5 control-label">Tipo Documento</label>
                                          <div class="col-lg-7">
                                               <asp:DropDownList runat="server" class="form-control" id="DDLPadTipoDocuEdit" placeholder="Tipo Documento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True"/>
                                          </div>
                                      </div>
                                      
                                      <div class="form-group">
                                          <label for="NumeroDocumento" class="col-lg-5 control-label">Numero</label>
                                          <div class="col-lg-7">
                                               <asp:TextBox runat="server" class="form-control" ID="txtPadNumDocEdit" placeholder="Numero Documento" onkeydown="return valeditPadre(event)"/>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <label for="Fecha Nacimiento" class="col-lg-5 control-label">Fecha Nacimiento</label>
                                          <div class="col-lg-7">
                                               <asp:TextBox  runat="server" class="form-control" id="txtPadFechaNacEdit" placeholder="Fecha de Nacimiento" type="date"  onfocus="(this.type='date')"  data-date-format="dd/mm/yyyy" />
                                          </div>
                                     </div>
                            

                                         <div class="form-group">
                                          <label for="Seccion" class="col-lg-5 control-label">&nbsp; </label>
                                          <div class="col-lg-7">
                                               &nbsp;
                                         </div>
                                        </div>

                                           <div class="form-group">
                                                    <label for="Seccion" class="col-lg-5 control-label">&nbsp; </label>
                                                    <div class="col-lg-7">                                               
                                                        &nbsp;
                                                    </div>
                                          </div>

                                                 <div class="form-group">
                                                    <label for="Seccion" class="col-lg-5 control-label">&nbsp; </label>
                                                    <div class="col-lg-7">                                               
                                                      
                                                    </div>
                                          </div>
                                 </div>
                      </div> 
               </div>                                                                                                         
                    <div class="col-lg-6" >                                      
                                         <div class="panel panel-default">
                                            <div class="panel-heading">Datos del Hijo Beneficiario</div>
                                            <div class="panel-body">                                                                 
                                        <div class="form-group">
                                      <label for="ejemplo_email_3" class="col-lg-5 control-label">Apellido Paterno</label>
                                      <div class="col-lg-7">
                                          <asp:TextBox runat="server" class="form-control" id="txtApePatBeneficiarioEdit" Style="text-transform: uppercase"  placeholder="Apellido Paterno"/>
                                      </div>
                                  </div>

                                  <div class="form-group">
                                      <label for="ApellidoMaterno" class="col-lg-5 control-label">Apellido Materno</label>
                                      <div class="col-lg-7">
                                           <asp:TextBox runat="server" class="form-control" id="txtApeMatBeneficiarioEdit"  Style="text-transform: uppercase"  placeholder="Apellido Materno"/>
                                      </div>
                                      </div>

                                      <div class="form-group">
                                          <label for="Nombres" class="col-lg-5 control-label">Nombres</label>
                                          <div class="col-lg-7">
                                               <asp:TextBox runat="server" class="form-control" id="txtNombreBeneficiarioEdit" Style="text-transform: uppercase" placeholder="Nombres"/>
                                          </div>
                                      </div>

                                          <div class="form-group">
                                              <label for="NumeroDocumento" class="col-lg-5 control-label">Tipo Documento</label>
                                              <div class="col-lg-7">
                                                <asp:DropDownList runat="server" class="form-control" id="DDLBeneficiarioTipoDocuEdit" placeholder="Tipo Documento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True">
                                                                <asp:ListItem Selected="True" Value="0">Seleccione Tipo de Documento</asp:ListItem>
                                                               </asp:DropDownList>             
                                                 </div>
                                            </div>
                                      
                                      <div class="form-group">
                                          <label for="NumeroDocumento" class="col-lg-5 control-label">Numero</label>
                                          <div class="col-lg-7">
                                               <asp:TextBox runat="server" class="form-control" ID="txtBeneficiarioNumeroDocuEdit" placeholder="Numero Documento" onkeydown="return valeditBenef(event)" />
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <label for="Fecha Nacimiento" class="col-lg-5 control-label">Fec.Nac</label>
                                          <div class="col-lg-7">
                                               <asp:TextBox  runat="server" class="form-control" id="txtBeneficiarioFechaNacEdit" placeholder="Fecha de Nacimiento" type="date" onfocus="(this.type='date')" data-date-format="dd/mm/yyyy" />
                                          </div>
                                      </div>

                                       <%--<div class="form-group">
                                          <label for="Grado que cursara" class="col-lg-2 control-label">Grado</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox  runat="server" class="form-control" id="txtGradoEdit" placeholder="Grado que cursara"   />
                                          </div>
                                      <>--%>

                                         <div class="form-group">
                                              <label for="Grado que cursara" class="col-lg-5 control-label">Grado</label>
                                             <div class="col-lg-7">
                                                <asp:DropDownList runat="server" class="form-control" id="DDLBeneficiarioGradoEdit" placeholder="Grado"  CssClass="combo"  Width="100%" AppendDataBoundItems="True">
                                                                <asp:ListItem Selected="True" Value="0">Seleccione Grado</asp:ListItem>
                                                               </asp:DropDownList>
                                                 <asp:TextBox  runat="server" class="form-control" id="txtBeneficiarioFacultadEdit" Style="text-transform: uppercase" placeholder="Facultad" />
                                            </div>
                                      </div>

                                       <div class="form-group">
                                          <label for="Seccion" class="col-lg-5 control-label">Sección</label>
                                          <div class="col-lg-7">
                                               <asp:TextBox  runat="server" class="form-control" id="txtBeneficiarioSeccionEdit" Style="text-transform: uppercase" placeholder="Sección" />
                                              <asp:TextBox  runat="server" class="form-control" id="txtBeneficiarioCarreraEdit" Style="text-transform: uppercase" placeholder="Carrera" />
                                         </div>
                                  </div>
                                       
                                          
                                     </div>
                                                </div>
                                     <%-- <div class="form-group"  style="">
                                          <label for="Genero" class="col-lg-2 control-label">Genero</label>
                                          <div class="col-lg-10">
                                               <asp:RadioButtonList runat="server"  id="rbtSexoEdit"  RepeatDirection="Horizontal" >
                                                        <asp:ListItem Text ="&nbsp;Masculino&nbsp;&nbsp;" Enabled="true" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text ="&nbsp;Femenino&nbsp;" Enabled="true" Selected="False"></asp:ListItem>
                                               </asp:RadioButtonList>
                                          </div>
                                      </div>--%>
                                            
                                    </div>
                            </div>
                    <div class="modal-footer" style="background-color:white">
                                    <asp:Button   class="button" runat="server"  ID="btnActualizarPadre" Text="Guardar" OnClick="btnActualizarPadre_Click"/>                                  
                                   <asp:Button Text="Cerrar" ID="btnclose11" runat="server"  CssClass="button"  Style="background-color:orangered" />                                           
                            </div>
                            </div>   
                    </div>
                      
                  </div>

       <div id="pnlEdtDatosOnco" class="modal fade" role="dialog"  >
            <div class="modal-dialog modal-lg">                
                            <div class="modal-content">
                                    <div class="modal-header" style="background-color:#D6EAF8">
                                               <button type="button" class="close" data-dismiss="modal">&times;</button>
                                               <h4 class="modal-title" style="background-color:#D6EAF8" >Editar datos de Afiliacion</h4>
                                    </div>
                
                                    <div class="row"   style="padding:8px">                                                                    
                                      <div class="col-lg-4"  >                                      
                                                                                                                   
                                     <div class="panel panel-default">
                                            <div class="panel-heading">Datos del Titular</div>
                                            <div class="panel-body">

                                                <div class="form-group">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" class="form-control" ID="txtOncTitEdtApePat" 
                                                            placeholder="Ingrese apellido paterno" Style="text-transform: uppercase"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" class="form-control" ID="txtOncTitEdtApeMat" placeholder="Ingrese apellido materno" Style="text-transform: uppercase"></asp:TextBox>
                                                    </div>
                                                    </div>   

                                                    <div class="form-group">
                                                        <div class="col-lg-12">
                                                            <asp:TextBox runat="server" class="form-control" ID="txtOncTitEdtNom" placeholder="Ingrese Nombre" Style="text-transform: uppercase"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                <div class="form-group">
                                                    <div class="col-lg-12">                                 
                                                      
                                                            <asp:DropDownList runat="server" class="form-control" id="ddlOncTitEdtTipoDoc" placeholder="Tipo Documento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True">
                                                            <asp:ListItem Selected="True" Value="0">Seleccione Tipo de Documento</asp:ListItem>
                                                            </asp:DropDownList>                                                                                                          
                                                </div>
                                                </div> 
                                      
                                                <div class="form-group">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" class="form-control" id="txtOncTitEdtNroDoc" placeholder="Ingrese número de documento" onkeydown="return validNumericosTit(event)"></asp:TextBox>
                                                    </div>
                                                </div>

                                            <div class="form-group">
                                            <div class="col-lg-12">
                                                <asp:TextBox  runat="server" class="form-control" id="txtOncTitEdtFecNac"  data-date-format="dd/mm/yyyy"  placeholder="Ingrese fecha de nacimiento" onfocus="(this.type='date')" onblur="onblur_OncTitFecNac();"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                            </div>
                                            </div>

                                        <div class="form-group">
                                        <div class="col-lg-12">
                                            <asp:DropDownList ID="ddlOncTitEdtGrado" runat="server" CssClass="combo" Width="100%" AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True" Value="0">Seleccione el Grado</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox runat="server" class="form-control" id="txtOncTitEdtFacultad" placeholder="Ingrese Facultad" ></asp:TextBox>
                                        </div>
                                        </div>

                                        <div class="form-group">
                                        <div class="col-lg-12">
                                            <asp:TextBox runat="server" class="form-control" id="txtOncTitEdtCarrera" placeholder="Ingrese Carrera" ></asp:TextBox>
                                        </div>
                                        </div>

                                        <div class="form-group">                                                                                    
                                            <div class="col-lg-12">Genero
                                                <asp:RadioButtonList runat="server"  id="rblOncTitEdtGenero"  RepeatDirection="Horizontal" >
                                                    <asp:ListItem Text ="&nbsp;Masculino&nbsp;&nbsp;" Enabled="true" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text ="&nbsp;Femenino&nbsp;" Enabled="true" Selected="False"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            </div>
                                        </div>    
                                        <div class="form-group" style="height:75px;">                                                                                    
                                        </div>  
                                                
                                 </div>
                      </div> 
               </div>                                                                                                         
                    <div class="col-lg-8" >                                      
                                         <div class="panel panel-default">
                                            <div class="panel-heading">Datos del Contratante</div>
                                            <div class="panel-body">
                                       
                                                <div class="form-group" id="divOncConEdtPar" style="display:none;">
                                                    <div class="col-lg-2"><span>Parentesco:</span> </div>
                                                    <div class="col-lg-10">
                                                        <asp:RadioButtonList ID="rblOncConEdtPar" runat="server" RepeatDirection="Horizontal" Width="191px">
                                                            <asp:ListItem Selected="True" Value="1">Padre</asp:ListItem>
                                                            <asp:ListItem Value="2">Madre</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div> 
                                                <div class="form-group">
                                                    <div class="col-lg-6">
                                                        <asp:HiddenField ID="hdnOncConEdtApePat" runat="server" />
                                                        <asp:TextBox runat="server" class="form-control" ID="txtOncConEdtApePat" 
                                                            placeholder="Ingrese apellido paterno" Style="text-transform: uppercase" onchange="onchange_OncConEdtApePat(event);" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <asp:HiddenField ID="hdnOncConEdtApeMat" runat="server" />
                                                        <asp:TextBox runat="server" class="form-control" ID="txtOncConEdtApeMat" placeholder="Ingrese apellido materno" Style="text-transform: uppercase" onchange="onchange_OncConEdtApeMat(event);" ></asp:TextBox>                        
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <div class="col-lg-6">
                                                        <asp:HiddenField ID="hdnOncConEdtNom" runat="server" />
                                                        <asp:TextBox runat="server" class="form-control" ID="txtOncConEdtNom" placeholder="Ingrese Nombre" Style="text-transform: uppercase" onchange="onchange_OncConEdtNom(event);" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6">           
                                                        <asp:HiddenField ID="hdnOncConEdtTipoDoc" runat="server" />
                                                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConEdtTipoDoc" placeholder="Tipo Documento"  CssClass="combo"  AppendDataBoundItems="True" Width="100%" onchange="onchange_OncConEdtTipoDoc(event);" >
                                                        <asp:ListItem Selected="True" Value="0">Seleccione Tipo de Documento</asp:ListItem>
                                                        </asp:DropDownList>                                                                                                          
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <div class="col-lg-6">
                                                        <asp:HiddenField ID="hdnOncConEdtNroDoc" runat="server" />
                                                        <asp:TextBox runat="server" class="form-control" id="txtOncConEdtNroDoc" placeholder="Ingrese número de documento" onkeydown="return validNumericosCon(event)" onchange="onchange_OncConEdtNroDoc(event);" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <asp:HiddenField ID="hdnOncConEdtFecNac" runat="server" />
                                                        <asp:TextBox  runat="server" class="form-control" id="txtOncConEdtFecNac"  data-date-format="dd/mm/yyyy"  placeholder="Ingrese fecha de nacimiento" onfocus="(this.type='date')" onchange="onchange_OncConEdtFecNac(event);" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <div class="col-lg-6">
                                                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConEdtEstcivil" placeholder="Estado Civil"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" >
                                                        <asp:ListItem Selected="True" Value="0">Seleccione el Estado Civil</asp:ListItem>
                                                            <asp:ListItem Value="1">Soltero</asp:ListItem>
                                                            <asp:ListItem Value="2">Casado</asp:ListItem>
                                                        </asp:DropDownList>    
                                                    </div>
                                                    <div class="col-lg-6">           
<%--                                                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConEdtPaisNac" placeholder="Pais de Nacimiento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" >
                                                            <asp:ListItem Selected="True" Value="0">Seleccione el Pais de Nacimiento</asp:ListItem>
                                                            <asp:ListItem Value="1">Peru</asp:ListItem>
                                                        </asp:DropDownList>  --%>
                                                        <asp:TextBox  runat="server" class="form-control" id="txtOncConEdtPaisNac"  placeholder="Pais de Nacimiento"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group ">                                                                                    
                                                    <div class="col-lg-6">
                                                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConEdtDirPais" placeholder="Direccion Pais"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" >
                                                            <asp:ListItem Selected="True" Value="0">Seleccione la Direccion Pais</asp:ListItem>
                                                            <asp:ListItem Value="1">Peru</asp:ListItem>
                                                        </asp:DropDownList>                                                         
                                                    </div>
                                                    <div class="col-lg-6">
                                                     <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                     <ContentTemplate>
                                                         <asp:DropDownList runat="server" class="form-control" id="ddlOncConEdtDirDep" placeholder="Direccion Departamento"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlOncConEdtDirDep_SelectedIndexChanged" >
                                                        <asp:ListItem Selected="True" Value="0">Seleccione la Direccion Departamento</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    </div>
                                                </div>   
                                                <div class="form-group ">                                                                                    
                                                    <div class="col-lg-6">
                                                     <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                     <ContentTemplate>
                                                          <asp:DropDownList runat="server" class="form-control" id="ddlOncConEdtDirPrv" placeholder="Direccion Provincia"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlOncConEdtDirPrv_SelectedIndexChanged" >
                                                        <asp:ListItem Selected="True" Value="0">Seleccione la Direccion Provincia</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-lg-6">
                                                     <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                                     <ContentTemplate>
                                                        <asp:DropDownList runat="server" class="form-control" id="ddlOncConEdtDirDis" placeholder="Direccion Distrito"  CssClass="combo"  Width="100%" AppendDataBoundItems="True" >
                                                        <asp:ListItem Selected="True" Value="0">Seleccione la Direccion Distrito</asp:ListItem>
                                                        </asp:DropDownList> 
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    </div>
                                                </div>   
                                                <div class="form-group ">                                                                                    
                                                    <div class="col-lg-6">
                                                        <asp:TextBox  runat="server" class="form-control" id="txtOncConEdtDirEnt"  placeholder="Ingrese la Direccion de Entrega" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <asp:TextBox  runat="server" class="form-control" id="txtOncConEdtTelDom"  placeholder="Ingrese el Telefono de Domicilio" ></asp:TextBox>
                                                    </div>
                                                </div>  
                                                <div class="form-group ">                                                                                    
                                                    <div class="col-lg-6">
                                                        <asp:TextBox  runat="server" class="form-control" id="txtOncConEdtTelTrab"  placeholder="Ingrese el Telefono de Trabajo" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <asp:TextBox  runat="server" class="form-control" id="txtOncConEdtNroCel"  placeholder="Ingrese el Numero de Celular" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group ">                                                                                    
                                                    <div class="col-lg-6">
                                                        <asp:TextBox  runat="server" class="form-control" id="txtOncConEdtEmail"  placeholder="Ingrese el Email" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6">Genero
                                                        <asp:RadioButtonList runat="server"  id="rblOncConEdtGen"  RepeatDirection="Horizontal" >
                                                            <asp:ListItem Text ="&nbsp;Masculino&nbsp;&nbsp;" Enabled="true" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text ="&nbsp;Femenino&nbsp;" Enabled="true" Selected="False"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                     </div>
                                                </div>
                                            
                                    </div>
                            </div>
                    <div class="modal-footer" style="background-color:white">
                                    <asp:Button   class="button" runat="server"  ID="btnOncEdtGuarDatosAseg" Text="Guardar" OnClick="btnOncEdtGuarDatosAseg_Click" style="display:none;"/>                                  
                                   <asp:Button Text="Cerrar" ID="btnOncEdtCerrDatosAseg" runat="server"  CssClass="button"  Style="background-color:orangered"  data-dismiss="modal"/>                                           
                            </div>
                            </div>   
                    </div>
                      
                  </div>
          
        <div id="pnlTerminos2" class="modal fade" role="dialog">
       <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                   <asp:Literal  runat="server" ID="ltlTerminos"></asp:Literal>
                        </div>
                    </div>
                    <div class="modal-footer">                        
                              <asp:Button Text="Aceptar" ID="Btnaceptacontrato" runat="server"  CssClass="button"  Style="background-color:orangered" />
                    </div>
                </div>
            </div>
    </div>

         <div id="myModalCombo" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Hermes Seguros</h4>
                    </div>
                    <div class="modal-body">
                        <div class="msgcentrado">
                            <asp:Label Text="" ID="lblcombo" runat="server" Style="font-size: 18px"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
      </div>
</asp:Content>
