<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" 
        CodeBehind ="Asociacion.aspx.cs" Inherits="Demo.Asociacion" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.5.2.min.js"></script>
    <link href="Css/standar.css" rel="stylesheet" />   
    <style>
    fieldset { 
    display: block;
    margin-left: 2px;
    margin-right: 2px;
    padding-top: 0.35em;
    padding-bottom: 0.625em;
    padding-left: 0.75em;
    padding-right: 0.75em;
    border: 2px groove (internal value);
}
   mostrar{
       display: block;
   }
  
 input[type=file]
        {
        width:140px;
        color:transparent;
        }

    </style>
        <script>     
            $('#FUpPlan').change(function () {
                $(this).removeClass("bar");
            })

            
      function OnClienteSelected(source, e) {
            var idx = source._selectIndex;
            var clientes = source.get_completionList().childNodes;
            var value = clientes[idx]._value;
            var text = clientes[idx].firstChild.nodeValue;
            source.get_element().value = text;
            $get("<%=hdnIDCliente1.ClientID %>").value = e.get_value();
        }

        function ClientItemSelected(sender, e) {
            $get("<%=hdnIDCliente1.ClientID %>").value = e.get_value();           
        }

        function App_Context_Key(source, e) {
            source.set_contextKey($get("<%=ddlTipoInstEduca.ClientID%>").value + '|' + $get("<%=txtSearch.ClientID%>").value);            
        }

  </script>
 <%--Fin Autocompele script id   --%>  
     
   <style type="text/css">       
     td {  
         cursor: pointer;  
     }  
     .hover_row {  
         background-color: #A1DCF2;  
     }  

       </style>  
    <script type="text/javascript">  
     $(function () {  
         $("[id*=grvAsociaciones] td").hover(function () {
             $("td", $(this).closest("tr")).addClass("hover_row");  
         }, function () {  
             $("td", $(this).closest("tr")).removeClass("hover_row");  
         });


         $("#<%= ddlTipoSeguro.ClientID %>").change(function () {
             disabledInputsOncologico();
         });
     });

        $(window).load(function () {
            disabledInputsOncologico();
        });

        function disabledInputsOncologico() {
             $("#<%=txtFechaVigenciaInicio.ClientID %>").removeAttr("disabled");
             $("#<%=txtFFVigenciaBanco.ClientID %>").removeAttr("disabled");
             $("#<%=txtFIVigenciaPoliza.ClientID %>").removeAttr("disabled");
             $("#<%=txtFFVigenciaPoliza.ClientID %>").removeAttr("disabled");             
            
            var rb=$("#<%= rbtTipoCarga.ClientID %> input");
            var radio = rb[1];
            radio.disabled=false;
             
            if ($(<%= ddlTipoSeguro.ClientID %>).val() == 3 || $(<%= ddlTipoSeguro.ClientID %>).val() == 7) {
                $("#<%= txtFechaVigenciaInicio.ClientID %>").attr("disabled", "disabled");
                $("#<%= txtFechaVigenciaInicio.ClientID %>").val('');
                $("#<%= txtFFVigenciaBanco.ClientID %>").attr("disabled", "disabled");
                $("#<%= txtFFVigenciaBanco.ClientID %>").val('');
                $("#<%= txtFIVigenciaPoliza.ClientID %>").attr("disabled", "disabled");
                $("#<%= txtFIVigenciaPoliza.ClientID %>").val('');
                $("#<%= txtFFVigenciaPoliza.ClientID %>").attr("disabled", "disabled");
                $("#<%= txtFFVigenciaPoliza.ClientID %>").val('');
                 radio.disabled=true;
             }
        }
 </script>  


    <%--   <script type="text/javascript">
            $(document).ready(function () {
            $("#<%=txtBusqueda.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({                                                          
                    url: 'WS_ServiceHermes.asmx/GetInstitucionesEducativas") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("#<%=pkInstitucionEducativa.ClientID %>").val(i.item.val);
            },
            minLength: 1
        });
    });       
</script>--%>
    <script>
        
       //     $('#'+'<%=lblFilex.ClientID %>').attr('value', $('#'+'<%=FUpPlan.ClientID %>').val())
        
        $(document).ready(function () {
            $('input[type="file"]').change(function (e) {
                var fileName = e.target.files[0].name;
                $('#'+'<%= lblFilex.ClientID %>').val(fileName);      
            });
        });

    </script>

<%--   <script>
             function openModal() {
                 $('#myModal').modal('show');
             }
    </script>--%>

    <script>
             function openAnularAsociacion() {
                 $('#pnlAnular').modal('show');
             }
    </script>
             
    <script type="text/javascript">
        <%--  $(function () {
            $('#<%=grvAsociaciones.ClientID %> img').click(function () {                
                var img = $(this)     
                var orderid = $(this).attr('orderid');             
                var tr = $('#<%=grvAsociaciones.ClientID %> tr[orderid =' + orderid + ']')           
                tr.toggle();                
                if (tr.is(':visible'))
                    img.attr('src', 'Images/icoContacto2.png');
                else
                    img.attr('src', 'Images/icoContacto.png');               
            });
        });--%>                      
    </script>

    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
        <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>  
       <script>
           function formulas() {
               $(document).ready(function () {
                   $('#' + '<%= txtPensionMensual.ClientID %>').keyup(function (event) {

                       var penMen = $('#' + '<%= txtPensionMensual.ClientID %>').val();
                       //      var penMen = $('#' + '<%= txtMesesPension.ClientID %>').val(); 
                       //      var penAños = $('#' + '<%= txtAñosPension.ClientID %>').val(); 

                       //var formula = meses.concat(penMen.val());
                       var formulafinal1 = " = " + penMen + " x ";
                       var formulafinal2 = " = (" + penMen + " x ) x";

                       $('#<%=formula1.ClientID%>').html(formulafinal1);
                       $('#<%=formula2.ClientID%>').html(formulafinal2);
                   });
               });

               $(document).ready(function () {
                   $('#' + '<%= txtMesesPension.ClientID %>').keyup(function (event) {

                       var penMen = $('#' + '<%= txtPensionMensual.ClientID %>').val();
                       var MesesPen = $('#' + '<%= txtMesesPension.ClientID %>').val();
                       //var penAños = $('#' + '<%= txtAñosPension.ClientID %>').val(); 
                       //var formula = meses.concat(penMen.val());
                       var formulafinal1 = " = " + penMen + " x " + MesesPen;
                       var formulafinal2 = " = (" + penMen + " x " + MesesPen + " ) x";

                       $('#<%=formula1.ClientID%>').html(formulafinal1);
                       $('#<%=formula2.ClientID%>').html(formulafinal2);
                   });
               });

               $(document).ready(function () {
                   $('#' + '<%= txtAñosPension.ClientID %>').keyup(function (event) {

                       var penMen = $('#' + '<%= txtPensionMensual.ClientID %>').val();
                       var MesesPen = $('#' + '<%= txtMesesPension.ClientID %>').val();
                       var penAños = $('#' + '<%= txtAñosPension.ClientID %>').val();
                       //var formula = meses.concat(penMen.val());

                       var formulafinal1 = " = " + penMen + " x " + MesesPen;
                       var formulafinal2 = " = (" + penMen + " x " + MesesPen + " ) x" + penAños;

                       $('#<%=formula1.ClientID%>').html(formulafinal1);
                       $('#<%=formula2.ClientID%>').html(formulafinal2);
                   });
               });
           }
       </script>



    <div>        
        <asp:MultiView ID="MVTipoBancos" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwBusqueda" runat="server">
                <div class="myContent">
                    <div class="content-header">
                        <div class="container-fluid">
                            <div class="row">                                                      
                                    <div class="panel panel-primary">
                                    <div class="panel-heading">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" style="color:white">
                                     <b>  Asociación de Entidades : </b>
                                        Criterios de Búsqueda</a>
                                      </div>
                                         <div class="panel-body">                      
                                         <div style="padding: 2px;">
                                        Texto de busqueda
                                        <asp:TextBox ID="txtBusqueda" runat="server" CssClass="cajaTexto" Style="width: 70%; text-transform: uppercase;"></asp:TextBox>
                                        <asp:Button ID="btnBuscar" runat="server" CssClass="button" OnClick="btnBuscar_Click" Text="Buscar" />
                                        <asp:Button ID="btnNuevo" runat="server" CssClass="button" OnClick="btnNuevo_Click" Style="background-color: orangered" Text="Nuevo" />
                                    </div>
                                    <div style="padding: 5px;">
                                        <div>
                                            Resultados
                                        </div>
                                        <div style="overflow:scroll;width:100%">
                                        <asp:GridView ID="grvAsociaciones" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid" 
                                             OnRowCommand="grvAsociaciones_RowCommand" PageSize="10" Width="100%" OnRowDataBound="grvAsociaciones_RowDataBound"
                                             OnPageIndexChanging="grvAsociaciones_PageIndexChanging1" OnSelectedIndexChanged="grvAsociaciones_SelectedIndexChanged">
                                            <Columns>
                                                  
                                                <asp:BoundField DataField="Codigo" HeaderText="Codigo Afiliación" InsertVisible="False" ReadOnly="True" SortExpression="Codigo" />
                                                <asp:BoundField DataField="NombreNatural" HeaderText="Nombre" SortExpression="Nombre" />
                                                <asp:BoundField DataField="CiaSeguro" HeaderText="CiaSeguro" SortExpression="CiaSeguro" />
                                                <asp:BoundField DataField="TipoSeguro" HeaderText="TipoSeguro" SortExpression="TipoSeguro" />

                                                <asp:TemplateField HeaderText="Prima" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPrima"   Text='<%# Convert.ToInt32( Eval("MonedaID"))  == 1  ?  "S/. " + Eval("Prima") : "US$ " + Eval("Prima")    %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" /></asp:TemplateField>
                                                <asp:BoundField DataField="Recaudador" HeaderText="Recaudador" SortExpression="Recaudador" />
                                                
                                                <asp:TemplateField HeaderText="Bancos">
                                                    <ItemTemplate>                                                        
                                                        <asp:Label ID="lblBancos" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cargar Datos" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" ID="btnCargarDatos" Text="Cargar Datos" CssClass="btn-danger"  BorderStyle="None" style="padding:4px;border-radius:3px"
                                                            CommandName="CargarDatos"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                                        </asp:Button>
                                                    </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Generar Códigos" ItemStyle-Width="40px"  ><ItemTemplate><asp:Button runat="server" ID="btnGenerar" Text="Generar" CssClass="btn-success"   style="padding:4px;border-radius:3px" BorderStyle="None"  
                                                          CommandName="GenerarCodigos"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:Button></ItemTemplate><ItemStyle HorizontalAlign="Center" /></asp:TemplateField>

                                               <%-- <asp:TemplateField HeaderText="Plan de Seguros" ItemStyle-Width="40px"   >
                                                    <ItemTemplate>
                                                         <asp:ImageButton runat="server" ID="btnCargarPlan" ImageUrl="~/Images/DescargaPDF.png" width="16px"  Height="16px"   AlternateText="Descargar plan de seguros" ToolTip="Descargar plan de seguros">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>                                               
                                                <asp:TemplateField HeaderText="Plan">
                                                        <ItemTemplate>                                                             
                                                              <asp:ImageButton ID="btnPlan" runat="server" OnClick="btnPlan_Click"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/Images/DescargaPDF.png" Width="12" Height="12"/>
                                                          </ItemTemplate>
                                                       <ItemStyle HorizontalAlign="Center" Width="35px"  />
                                               </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Póliza">
                                                     <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="btnCargarPoliza" ImageUrl="~/Images/Poliza.png" width="18px"  Height="18px"  AlternateText="Asignar poliza"  ToolTip="Asignar poliza de seguros"  CommandName="AsignarPoliza"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:ImageButton>
                                                      </ItemTemplate>
                                                      <ItemStyle HorizontalAlign="Center"  Width="25px"/>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edita"  ItemStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditaAsociacion"  Height="16px" ImageUrl="~/Images/icoEditar.png"  ToolTip="Editar Asociacion" Width="16px" OnClick="btnEditar_Click1"/>
                                                    </ItemTemplate><ItemStyle HorizontalAlign="Center" Width="25px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Anula"  ><ItemTemplate><asp:ImageButton ID="btnAnular" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AnulaAsociacion" Height="16px" ImageUrl="~/Images/Anular.png"  ToolTip="Anular Asociacion " Width="16px" /></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="20px" /></asp:TemplateField>
                                                <asp:TemplateField HeaderText="Elimina"  ItemStyle-Width="35px"><ItemTemplate><asp:ImageButton ID="btnEliminar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EliminaAsociacion" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/deletes.png" title="" ToolTip="Eliminar Asociación"  Width="16px" /></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="45px" /></asp:TemplateField>
                                                
                                                <asp:BoundField DataField="InstitucionEducativaId" HeaderText="InstitucionEducativaId" InsertVisible="False" ReadOnly="True" SortExpression="ID"  HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">
                                                  
                                                <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                                  
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CiaSeguroID" HeaderText="CiaSeguroID" SortExpression="CiaSeguroID"  HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RecaudadorID" HeaderText="RecaudadorID" SortExpression="RecaudadorID"  HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProductoID" HeaderText="ProductoID" SortExpression="ProductoID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">                                                

                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="FechaVigenciaInicio" HeaderText="FechaVigenciaInicio" SortExpression="FechaVigenciaInicio" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">                                                
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FechaVigenciaFin" HeaderText="FechaVigenciaFin" SortExpression="FechaVigenciaFin" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">                                                
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FechaVigenciaPolizaInicio" HeaderText="FechaVigenciaPolizaInicio" SortExpression="FechaVigenciaPolizaInicio" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto"> 
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FechaVigenciaPolizaFin" HeaderText="FechaVigenciaPolizaFin" SortExpression="FechaVigenciaPolizaFin" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">       
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MonedaID" HeaderText="MonedaID" SortExpression="MonedaID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">       
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Prima" HeaderText="Prima" SortExpression="Prima" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">       

                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="InvalidezPermanenteTotal" HeaderText="InvalidezPermanenteTotal" SortExpression="InvalidezPermanenteTotal" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">       
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="InvalidezPermanenteParcial" HeaderText="InvalidezPermanenteParcial" SortExpression="InvalidezPermanenteParcial" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">       
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GastoCuracion" HeaderText="GastoCuracion" SortExpression="GastoCuracion" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">       
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MuerteAccidental" HeaderText="MuerteAccidental" SortExpression="MuerteAccidental" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">       
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GastosSepelio" HeaderText="GastosSepelio" SortExpression="GastosSepelio" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">                                                                                                                                                                                     
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MuerteAccidental" HeaderText="MuerteAccidental" SortExpression="MuerteAccidental" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">       
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FilePlanSeguro" HeaderText="FilePlanSeguro" SortExpression="FilePlanSeguro" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto">       
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                               <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" >  
                                                   <HeaderStyle CssClass="oculto" />
                                                   <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NroPoliza" HeaderText="NroPoliza" SortExpression="NroPoliza" HeaderStyle-CssClass="oculto"  ItemStyle-CssClass="oculto" >  
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CodigoContratante" HeaderText="CodigoContratante" SortExpression="CodigoContratante" HeaderStyle-CssClass="oculto"  ItemStyle-CssClass="oculto">  
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NombreContratante" HeaderText="NombreContratante" SortExpression="NombreContratante" HeaderStyle-CssClass="oculto"  ItemStyle-CssClass="oculto">  
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NombreContratante" HeaderText="RazonSocial" SortExpression="RazonSocial" HeaderStyle-CssClass="oculto"  ItemStyle-CssClass="oculto">  
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="BancoID"  ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblids" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FileNamePlanSeguro" HeaderText="FileNamePlanSeguro" SortExpression="FileNamePlanSeguro" HeaderStyle-CssClass="oculto"  ItemStyle-CssClass="oculto">  
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" >                                                      
                                                           <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                       
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo"> 
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="TipoCargaCodigos" HeaderText="TipoCargaCodigos" SortExpression="TipoCargaCodigos"> 
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>


                                                <asp:BoundField DataField="pensionMensual" HeaderText="pensionMensual" SortExpression="pensionMensual"> 
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>

                                                 <asp:BoundField DataField="MesesPension" HeaderText="MesesPension" SortExpression="MesesPension"> 
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="anniosPension" HeaderText="anniosPension" SortExpression="anniosPension"> 
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="CodGen" HeaderText="CodGen" SortExpression="CodGen"> 
                                                    <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="FileNamePlanSeguro" HeaderText="FileNamePlanSeguro" SortExpression="FileNamePlanSeguro"  HeaderStyle-CssClass="oculto"   ItemStyle-CssClass="oculto"   >
                                                <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TipoAsociacion" HeaderText="TipoAsociacion" SortExpression="TipoAsociacion"  HeaderStyle-CssClass="oculto"   ItemStyle-CssClass="oculto"   >
                                                <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Deducible" HeaderText="Deducible" SortExpression="Deducible"  HeaderStyle-CssClass="oculto"   ItemStyle-CssClass="oculto"   >

                                                <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TipoInstitucionEducativaID" HeaderText="TipoInstitucionEducativaID" SortExpression="TipoInstitucionEducativaID"  HeaderStyle-CssClass="oculto"   ItemStyle-CssClass="oculto"   >

                                                <HeaderStyle CssClass="oculto" />
                                                <ItemStyle CssClass="oculto" />
                                                </asp:BoundField>

                                            </Columns>
                                        </asp:GridView>
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>                                   
           <asp:View ID="VwNuevo" runat="server">
               
                  <%--<div class="container-fluid">--%>
                 
                   <div class="panel panel-primary">
                    <div class="panel-heading">Asociación</div>
                            <div class="panel-body">                      
                                 <div class="row">
                                     <div class="col-lg-7">
                                         <div class="form-group">
                                             <label for="TipoInstitucionEducativa" class="col-lg-4 control-label">Tipo Institucion Educativa: </label>
                                             <div class="col-lg-8">
                                                <asp:DropDownList ID="ddlTipoInstEduca" runat="server" AppendDataBoundItems="True" class="form-control"  placeholder=""  required="true" Width="320px"  >
                                                <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                             </div>
                                         </div>
                                     </div>
                                     <div class="col-lg-5">

                                     </div>
                                 </div>
                                 <div class="row">
                                         <div class="col-lg-7">
                                        <asp:HiddenField ID="hdnID" runat="server" Value="0" />                                            
          
                                        <div class="form-group">    
                                        <label for="InstitucionEducativa" class="col-lg-4 control-label">Institucion Educativa: </label>
                                        <div class="col-lg-8">
                                        <%--<asp:DropDownList ID="ddlinstitucionIE" runat="server" AppendDataBoundItems="True" class="form-control"  placeholder=""  required="true" Width="220px"  >
                                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <asp:HiddenField ID="hdnIDCliente1" runat="server" />
                                        <asp:TextBox runat="server" ID="txtSearch" class="form-control" 
                                            AutoPostBack="True" placeholder="Institucion Educativa" 
                                            OnTextChanged="txtSearch_TextChanged" Width="320"></asp:TextBox>                                                         
                                              <ajaxToolkit:AutoCompleteExtender ID="txtSearch_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="10" OnClientItemSelected = "ClientItemSelected"                                                                                       
                                                  BehaviorID="txtSearch_AutoCompleteExtender" 
                                                  DelimiterCharacters="2"
                                                  ServiceMethod="SearchClientes"
                                                  MinimumPrefixLength = "2"                                                  
                                                  TargetControlID="txtSearch"
                                                  UseContextKey="true"
                                                  OnClientPopulating="App_Context_Key">                                         
                                              </ajaxToolkit:AutoCompleteExtender>
                                              
                                            <%-- <asp:TextBox ID="hdnIDCliente" CssClass="hidden" runat="server"></asp:TextBox>--%>                               
                                    </div>                              
                                    </div>
                      
                            <div class="form-group">
                             <label for="CIASEGUROS" class="col-lg-4 control-label">CIA de Seguros:</label>
                              <div class="col-lg-8">
                                  <asp:UpdatePanel runat="server">
                                      <ContentTemplate>
                                    <asp:DropDownList ID="ddlCiaSeguros" runat="server" AppendDataBoundItems="True"  class="form-control" placeholder="" AutoPostBack="True" OnSelectedIndexChanged="ddlCiaSeguros_SelectedIndexChanged"  Width="320px" >
                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList>                                          
                                 </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            </div>

                        <div class="form-group">
                            <label for="EMPRESARECAUDADORA" class="col-lg-4 control-label">Empresa Recaudadora:</label>
                            <div class="col-lg-8">
                                    <asp:DropDownList ID="ddlEmpresaRecaudadora" runat="server" AppendDataBoundItems="True"  class="form-control" placeholder=""  Width="320px">
                                        <asp:ListItem Value="0" Selected="True">Seleccione</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                        </div>
                      
                          <div class="form-group">
                            <label for="TipoSeguro" class="col-lg-4 control-label">Tipo Seguro:</label>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="ddlTipoSeguro" runat="server" AppendDataBoundItems="True"  class="form-control" placeholder=""   Width="320px" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoSeguro_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                        </div>
                                
                        <div class="form-inline">
                            <label for="VigenciaBanco" class="col-lg-4 control-label">Fecha Vigencia Inicio</label>
                                <div class="col-lg-8">
                                    <asp:TextBox runat="server" class="form-control" id="txtFechaVigenciaInicio" ToolTip="Fecha Inicio de vigencia de Banco" 
                                                data-toggle="tooltip" data-placement="left"  Width="149px" type="date"
                                                 placeholder="Fecha Inicio de vigencia de Banco" required="true"   data-date-format="DD MM YYYY" />&nbsp; A &nbsp;
                                                                                                                                                
                                                <asp:TextBox runat="server" class="form-control" id="txtFFVigenciaBanco" ToolTip="Fecha fin Vigencia de Banco"  data-toggle="tooltip" data-placement="left"
                                                 placeholder="Ingrese Fecha de Vigencia inicio" required="true"   Width="149px"  type="date"   data-date-format="DD MM YYYY" />
                                </div>                    
                         </div>

                    <div class="form-inline">
                        <label for="VigenciaBanco" class="col-lg-4 control-label">Fecha Vigencia Poliza</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" id="txtFIVigenciaPoliza" ToolTip="Fecha Inicio de vigencia de Poliza" 
                                            data-toggle="tooltip" data-placement="left"  Width="149px" type="date"
                                             placeholder="Fecha Inicio de vigencia de Banco" required="true"   data-date-format="DD MM YYYY"  />&nbsp; A &nbsp;
                                                                                                                                                
                                            <asp:TextBox runat="server" class="form-control" id="txtFFVigenciaPoliza" ToolTip="Fecha fin Vigencia de Poliza"  data-toggle="tooltip" data-placement="left"
                                             placeholder="Ingrese Fecha de Vigencia inicio" required="true"  Width="149px"  type="date" data-date-format="DD MM YYYY"   />
                            </div>                    
                     </div>                                                   

                          <div class="form-group">
                                    <label for="co" class="col-lg-4 control-label">Plan de seguro</label>
                                    <div class="col-lg-5">
                                        <asp:TextBox runat="server" class="form-control" ID="lblFilex" ToolTip="" data-toggle="tooltip" 
                                            data-placement="left" placeholder="Seleccione el plan de seguros" required="true"  Width="320px"/>
                                    </div>
                           </div>

                             <div class="form-inline">
                                    <label for="Pl" class="col-lg-4 control-label">Seleccione Plan</label>
                                    <div class="col-lg-8">
                                     
                                                    <asp:FileUpload ID="FUpPlan" runat="server" />
                                     
                                    </div>
                             </div>                           
                             <div class="form-group">
                                    <label for="co" class="col-lg-12 control-label">&nbsp;</label>                            
                              </div>
                                
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                       <fieldset class="form-inline">
                                       <legend  style="font-size:12px;font-weight:bold">Tipo de Asociación</legend>                                                                                  
                                                    <asp:RadioButtonList runat="server" ID="rbtTipoAsociacion" RepeatDirection="Horizontal" Width="250px" Visible="false">
                                                    <asp:ListItem Value="1" Selected="True">Un solo padre</asp:ListItem>
                                                    <asp:ListItem Value="2">Ambos padres</asp:ListItem>
                                                </asp:RadioButtonList>                                                                                  
                                       </fieldset>                                                             
                                    </ContentTemplate>
                            </asp:UpdatePanel>
                                </div>       
                                                                          
                      <div class="col-lg-5" >                        
                          <div class="form-group">
                                <label for="TipoMoneda" class="col-lg-4 control-label">Tipo Moneda</label>
                                <div class="col-lg-8">
                                    <asp:RadioButtonList runat="server"   RepeatDirection="Horizontal" ID="rbtMoneda"  style="width:120px">  
                                        <asp:ListItem Text="S/." Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="US$." Value="2"> </asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                           </div>                                         
                                                  
                                <div class="form-group">
                                    <label for="co" class="col-lg-4 control-label">Prima</label>
                                    <div class="col-lg-8">
                                        <asp:TextBox runat="server" class="form-control" ID="txtPrima" ToolTip="f la Prima" data-toggle="tooltip" data-placement="left"  type="number"
                                            placeholder="Ingrese el monto de Prima" required="true"  />
                                    </div>
                                </div>
                                                                                                   
                           <div class="form-group"  style="padding:10px">                           
                               <div>
                                      <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                   <asp:GridView runat="server" ID="grvBancos" AutoGenerateColumns="false" CssClass="Grid2"   
                                       ShowHeaderWhenEmpty="True"     Width="100%" Caption="Selección de Bancos" EmptyDataText="Sin Registros" 
                                       captionalign="Left" >
                                       <Columns>
                                           <asp:BoundField DataField="ID" HeaderText="Item"  ItemStyle-Width="25px"/>
                                           <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="300px" />                                        
                                         
                                           <asp:TemplateField>
                                               <HeaderTemplate>
                                                   <asp:CheckBox ID="chkALL" runat="server"  AutoPostBack="True" OnCheckedChanged="chkALL_CheckedChanged"  width="15px"/>
                                               </HeaderTemplate>
                                               <ItemTemplate>
                                                   <asp:CheckBox ID="chkActivo" runat="server" Checked="false"  width="15px"/>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                       </Columns>
                                   </asp:GridView>
                                           </ContentTemplate>
                                       </asp:UpdatePanel>

                                   <fieldset>
                                       <legend  style="font-size:12px;font-weight:bold">Tipo de Generación Codigos</legend>                                         
                                                    <asp:RadioButtonList runat="server" ID="rbtTipoCarga" RepeatDirection="Horizontal" Width="250px">
                                                    <asp:ListItem Value="1" Selected="True">Carga Automatica</asp:ListItem>
                                                    <asp:ListItem Value="2">Carga Archivo</asp:ListItem>
                                                </asp:RadioButtonList>                                                                      
                                   </fieldset>
                                                                            
                                   
                           </div>
                      </div>
                   </div>
           </div>
            </div>
           <asp:UpdatePanel runat="server" ID="updResumenes">
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="ddlTipoSeguro" EventName="SelectedIndexChanged" />
                           </Triggers>
                           <ContentTemplate>

              
            <asp:Panel runat="server" ID="PnlSumasAccidentes" Visible="false" Style="width: 100%">

                          <div class="panel panel-primary">
                              <div class="panel-heading">Sumas Asegurada</div>
                              <div class="panel-body">

                                  <div class="form-inline">
                                      <label for="co" class="col-lg-3 control-label">Invalidez Permanente Total</label>
                                      <div class="col-lg-3">
                                          <asp:TextBox runat="server" class="form-control" ID="txtInvalidezPT" ToolTip="Invalidez Permanente Total" 
                                              data-toggle="tooltip" data-placement="left"
                                              placeholder="Ingrese Invalidez Permanente Total" required="true" Type="number"/>
                                          
                                      </div>
                                  </div>

                                  <div class="form-inline">
                                      <label for="co" class="col-lg-3 control-label">Invalidez Permanente Parcial</label>
                                      <div class="col-lg-3">
                                          <asp:TextBox runat="server" class="form-control" ID="txtInvalidezParcial" ToolTip="Invalidez Permanente Parcial" 
                                              data-toggle="tooltip" data-placement="left"
                                              placeholder="Ingrese Invalidez Permanente Parcial" required="true" Type="number"  />
                                      </div>
                                  </div>

                                  <div class="form-inline">
                                      <label for="co" class="col-lg-3 control-label">Gastos de curación</label>
                                      <div class="col-lg-3">
                                          <asp:TextBox ID="txtGastosCuracion" runat="server" class="form-control" data-placement="left" data-toggle="tooltip" 
                                              placeholder="Gastos de curación:" required="true" ToolTip="Gastos de curación:" Type="number"  />
                                      </div>
                                  </div>

                                  <div class="form-inline">
                                      <label for="co" class="col-lg-3 control-label">Muerte Accidental</label>
                                      <div class="col-lg-3">
                                          <asp:TextBox runat="server" class="form-control" ID="txtMuerteAccidental" ToolTip="Muerte accidental:" 
                                              data-toggle="tooltip" data-placement="left"
                                              placeholder="Muerte Accidental:" required="true" Type="number"  />
                                      </div>
                                  </div>

                                  <div class="form-inline">
                                      <label for="co" class="col-lg-3 control-label">Gastos de sepelio</label>
                                      <div class="col-lg-3">
                                          <asp:TextBox runat="server" class="form-control" ID="txtGastosSepelio" ToolTip="Gastos de sepelio:" 
                                              data-toggle="tooltip" data-placement="left"
                                              placeholder="Gastos de sepelio:" required="true" Type="number" />
                                      </div>
                                  </div>

                                  <div class="form-inline">
                                      <label for="co" class="col-lg-3 control-label">Deducible</label>
                                      <div class="col-lg-3">
                                          <asp:TextBox runat="server" class="form-control" ID="txtdeducible" ToolTip="Gastos de sepelio:" 
                                              data-toggle="tooltip" data-placement="left"
                                              placeholder="Deducible:" required="true" Type="number" />
                                      </div>
                                  </div>

                                    </div>
                              </div>                         
                      </asp:Panel>
                                                                                                              
                          <asp:Panel runat="server" ID="PnlSumasRentas" Visible="false" Style="width: 100%"   >
                          <div class="panel panel-primary">
                              <div class="panel-heading">Sumas Aseguradas</div>
                              <div class="panel-body">

                                  <div class="form-inline">
                                      <label for="co" class="col-lg-3 control-label">Pensión Mensual</label>
                                      <div class="col-lg-9">
                                          <asp:TextBox runat="server" class="form-control" ID="txtPensionMensual" ToolTip="Ingrese Pension Mensual" 
                                              data-toggle="tooltip" data-placement="left" 
                                              placeholder="Ingrese Pension Mensual" required="true" Type="number"  />
                                      </div>
                                  </div>

                                  <div class="form-inline">
                                      <label for="co" class="col-lg-3 control-label">Meses de Pensión</label>
                                      <div class="col-lg-9">
                                          <asp:TextBox runat="server" class="form-control" ID="txtMesesPension" ToolTip="MesesPension" data-toggle="tooltip" data-placement="left"
                                              placeholder="Ingrese Meses Pensión" required="true"  Type="number" />
                                          <asp:Label runat="server" ID="formula1" Text=""></asp:Label>
                                      </div>
                                  </div>

                                  <div class="form-inline">
                                      <label for="co" class="col-lg-3 control-label">Años de Pensión</label>
                                      <div class="col-lg-9">
                                          <asp:TextBox ID="txtAñosPension" runat="server" class="form-control" data-placement="left" data-toggle="tooltip"
                                               placeholder="Ingrese años de pensión" required="true" ToolTip="Gastos de curación:" Type="number"  />
                                      
                                          <asp:Label runat="server" ID="formula2" Text=""></asp:Label>
                                      </div>
                                  </div>
                                  
                                    </div>
                              </div>
                          
                      </asp:Panel>
                               </ContentTemplate>
                               </asp:UpdatePanel>
                      
                      <%--<div class="form-group">
                          <label for="co" class="col-lg-12 control-label"></label>
                          <div class="col-lg-12">
                              <asp:GridView runat="server" ID="grvBanco" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" CssClass="Grid">
                                  <Columns>
                                      <asp:BoundField DataField="Codigo" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                                      <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                  </Columns>
                              </asp:GridView>
                          </div>
                      </div>--%>
        
                      <div class="modal-footer">
                          <asp:Button class="button" runat="server" ID="btnAsociar" Text="Asociar" OnClick="btnAsociar_Click" Height="41px" />
                          <asp:Button class="button" runat="server" ID="btnLimpiar" Text="Limpiar" OnClick="btnLimpiar_Click"  style="background-color: green"/>
                          <asp:Button class="button" Text="Cancelar" runat="server" ID="btnCancelar" OnClick="btnCancelar_Click1" formnovalidate  style="background-color: orangered"/>
                      </div>        
              
           </asp:View>
        </asp:MultiView>
        
      <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Hermes Seguros</h4>
                    </div>
                    <div class="modal-body">
                        <div class="msgcentrado">
                            <asp:Label Text="" ID="txtmensaje" runat="server" Style="font-size: 18px"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button   ID="btnCerrar" runat="server"   Text="Cerrar" CssClass="button" />
                    </div>
                </div>
            </div>
      </div>    
                      
    <div id="pnlAnular" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Hermes Seguros</h4>
                    </div>
                    <div class="modal-body">
                        <div class="msgcentrado">
                            <asp:Label Text="" ID="lblMensaje" runat="server" Style="font-size: 18px"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        
                        <asp:Button class="button" runat="server" ID="btnAnular" Text="Aceptar" OnClick="btnAnular_Click1"  />
                        <asp:Button class="button" runat="server" ID="btnCancelarAnula" Text="Cancelar" Style="background-color:orangered" />                        
                    </div>
                </div>
            </div>
        </div>
                   
    <div id="pnlAsignarPoliza" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Asociar Póliza a Institución Educativa</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <label for="Codigo" class="col-lg-4 control-label" style="left: 0px; top: 0px">No Póliza:</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtNroPoliza" placeholder="Ingrese Codigo" />
                            </div>
                        </div>
                        <div class="Nombre">
                            <label for="Nombre" class="col-lg-4 control-label">Código Contratante:</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtCodigoContratante" placeholder="Código contratante" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-4 control-label">Nombre contratante:</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="txtNombreContrante" placeholder="Ingrese Nombre contratante" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button class="button" runat="server" ID="btnEditar" Text="Editar" OnClick="btnEditar_Click" />
                        <asp:Button class="button" runat="server" ID="Cerrar" Text="Cerrar"  Style="background-color:orangered"/>
                        
                    </div>
                </div>
            </div>
        </div>        
   </div>        
</div>
              

</asp:Content>