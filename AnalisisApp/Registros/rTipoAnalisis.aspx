<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rTipoAnalisis.aspx.cs" Inherits="AnalisisApp.Registros.rTipoAnalisis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel" style="background-color: #0094ff">
        <div class="panel-heading" style="font-family: Arial Black; font-size: 20px; text-align: center; color: Black">Registro de Tipos de Analisis</div>
    </div>
    <div class="panel-body">
        <div class="form-horizontal col-md-12" role="form">

            <div class="container">
                <%--TipoId--%>
                <div class="form-group">
                    <label for="IdTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Id</label>
                    <div class="col-md-1 ">
                        <asp:TextBox ID="IdTextBox" runat="server" placeholder="0" class="form-control input-sm" Style="font-size: small" TextMode="Number"></asp:TextBox>
                    </div>
                    <asp:RegularExpressionValidator ID="ValidaID" runat="server" ErrorMessage='Campo "ID" solo acepta numeros' ControlToValidate="IdTextBox" ValidationExpression="^[0-9]*" Text="*" ForeColor="Red" Display="Dynamic" ToolTip="Entrada no valida" ValidationGroup="Guardar"></asp:RegularExpressionValidator>
                    <div class="col-md-1 ">
                        <asp:Button ID="BuscarButton" runat="server" Text="Buscar" class="btn btn-primary" OnClick="BuscarButton_Click1" />
                    </div>
                </div>

                <%-- Descripcion--%>
                <div class="form-group">
                    <label for="DescripcionTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Descripcion</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="DescripcionTextBox" runat="server" onkeypress="return isLetterKey(event)" placeholder="Ej. Hemograma" class="form-control input-sm" Style="font-size: small"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="Valida" runat="server" ErrorMessage="El campo &quot;Descripcion&quot; esta vacio" ControlToValidate="DescripcionTextBox" ForeColor="Red" Display="Dynamic" ToolTip="Campo Descripcion es obligatorio" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </div>

                    <%--Precio--%>
             <div class="form-group">
                    <label for="PrecioTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Precio</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="PrecioTextBox" runat="server"  TextMode="Number"  placeholder="0" class="form-control input-sm" Style="font-size: small"></asp:TextBox>
                    </div>
                </div>


                <%--Botones--%>
                <div class="panel">
                    <div class="text-center">
                        <div class="form-group">
                            <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" class="btn btn-primary" OnClick="NuevoButton_Click" />
                            <asp:Button ID="GuardarButton" runat="server" Text="Guardar" class="btn btn-success" ValidationGroup="Guardar" OnClick="GuardarButton_Click1" />
                            <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" class="btn btn-danger" OnClick="EliminarButton_Click1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

