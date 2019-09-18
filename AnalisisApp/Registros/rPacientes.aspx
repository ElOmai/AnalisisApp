<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rPacientes.aspx.cs" Inherits="AnalisisApp.Registros.rPacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <div class="panel" style="background-color: #0094ff">
        <div class="panel-heading" style="font-family: Arial Black; font-size: 20px; text-align:center; color: Black">Registro de Paciente</div>
    </div>
    <div class="panel-body">
        <div class="form-horizontal col-md-12" role="form">

            <div class="container">
                <%--PacienteId--%>
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

                <%-- Nombres--%>
                <div class="form-group">
                    <label for="NombresTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Nombres</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="NombresTextBox" runat="server" onkeypress="return isLetterKey(event)" placeholder="Ej. Juan Perez" class="form-control input-sm" Style="font-size: small"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="Valida" runat="server" ErrorMessage="El campo &quot;Nombres&quot; esta vacio" ControlToValidate="NombresTextBox" ForeColor="Red" Display="Dynamic" ToolTip="Campo Nombres es obligatorio" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </div>

                <%-- Direccion--%>
                <div class="form-group">
                    <label for="DireccionTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Direccion</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="DireccionTextBox" runat="server" onkeypress="return isLetterKey(event)" placeholder="Ej. C. Castillo #32" class="form-control input-sm" Style="font-size: small"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="El campo &quot;Nombres&quot; esta vacio" ControlToValidate="DireccionTextBox" ForeColor="Red" Display="Dynamic" ToolTip="Campo Nombres es obligatorio" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </div>

                <%--Telefono--%>
                <div class="form-group">
                    <label for="TelefonoTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Telefono</label>
                    <div class="col-md-6">
                        <asp:TextBox class="form-control" ID="TelefonoTextBox" placeholder="###-###-####" runat="server"></asp:TextBox>
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

