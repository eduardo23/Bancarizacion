<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Home.Master" CodeBehind="ConsultarCampañas.aspx.cs" Inherits="Demo.ConsultarCampañas" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.5.2.min.js"></script>
    <link href="Css/standar.css" rel="stylesheet" />

      <script>
             function openModal() {
                 $('#myModal').modal('show');
             }
    </script>

    <script>
        function openAperturarCampaña() {
                 $('#pnlAperturarCampaña').modal('show');
             }
    </script>
        

      <script>
       function openActualizarCampaña() {
           $('#pnlActualizarCampaña').modal('show');
       }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
   
    <div>
        <asp:MultiView ID="MVTipoCampañas" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwBusqueda" runat="server">
                
                <div class="myContent">
                    <div class="content-header">
                        <div class="container-fluid">
                            <div class="row">
                             <div class="panel panel-primary">
                                <div class="panel-heading">Consultar Campaña : Criterios de búsqueda</div>
                                <div class="panel-body">                                                        
                                <div class="row">
                                    <div style="padding: 10px;">
                                       Texto de busqueda
                                       
                                         <asp:DropDownList ID="ddlCampañas" AutoPostBack="true" CssClass="cajaTexto" Style="width: 70%"  AppendDataBoundItems="true" runat="server">
                                         <asp:ListItem Value="T">TODOS</asp:ListItem>
                                         </asp:DropDownList>
                                       
                                         <asp:Button ID="btnBuscar" runat="server" CssClass="button" Text="Buscar" OnClick="btnBuscar_Click" />

                                    </div>
                                    <div style="padding:10px;">                                        
                                            Resultados                                        
                            

                                         <asp:GridView ID="grConsultarCampañas" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid"   PageSize="15" Width="100%">
                                            <Columns>
                                                  
                                                <asp:BoundField HeaderText="Código" InsertVisible="False" ReadOnly="True" DataField="_ID" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Campaña" DataField="_Nombre" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Inicio Vigencia" DataField="_InicioVigencia" DataFormatString="{0:dd/MM/yyyy}" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Fin Vigencia" DataField="_FinVigencia" DataFormatString="{0:dd/MM/yyyy}" >

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="150px" />
                                                </asp:BoundField>

                                                <asp:BoundField HeaderText="Estado" DataField="_Estado" >

                                                <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="HdCodigoCampaña" runat="server" />




                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
        </div>                
                
      </asp:View>                                   
     
 </asp:MultiView>
        
        
    </div>
    
  </ContentTemplate>
    </asp:UpdatePanel>
                 
</asp:Content>