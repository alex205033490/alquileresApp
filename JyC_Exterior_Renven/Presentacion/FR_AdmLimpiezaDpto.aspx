﻿<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true" CodeBehind="FR_AdmLimpiezaDpto.aspx.cs" Inherits="JyC_Exterior.Presentacion.FR_AdmLimpiezaDpto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RENVEN</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="Shortcut Icon" href="../img/logoRenven3.png" />
    <link href="../img/renvenLogo.png" rel="icon">
    <link href="../img/renvenLogo.png" rel="apple-touch-icon">

    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet" />
    <link href="../Styles/Style_RenvenLimpiezaDep.css" rel="stylesheet" type="text/css" />

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Bootstrap JS (con Popper) -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.min.js"></script>
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-ADMlimpiezaDep">
                   
        <h1> Administracion de visitas </h1>

        <div class="container-datosdpto">
            <div class="container_buscarDpto row">
                <div class="col-6">
                    <asp:Label runat="server"> Edificio</asp:Label>
                    <asp:TextBox ID="txt_dpto" runat="server" CssClass="form-control" OnTextChanged="txt_dpto_TextChanged" AutoPostBack="true"> </asp:textbox>
                    <asp:AutoCompleteExtender ID="txt_dpto_AutoCompleteExtender" runat="server" 
                        TargetControlID="txt_dpto" CompletionSetCount="12" MinimumPrefixLength="1" 
                        serviceMethod="GetAutoCompletListRegistros" UseContextKey="true" CompletionListCssClass="CompletionList" 
                        CompletionListItemCssClass="CompletionlistItem" CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"
                        CompletionInterval="10">
                    </asp:AutoCompleteExtender>

                </div>
                <div class="col-3">
                    <asp:Button ID="btn_buscarDpto" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btn_buscarDpto_Click" />
                </div>
                <div class="col-3">
                    <asp:Button ID="btn_anular" runat="server" Text="Anular" CssClass="btn btn-danger" OnClick="btn_anular_Click" />
                </div>

            </div>
            
        </div>

        <div ID="container-listRegistros" class="table-responsive">
            <asp:GridView ID="gv_listRegistrosVisitas" runat="server" AutoGenerateColumns="false" CssClass="table table-striped gv_dptosAdmiLD">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_anularVisita" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nro" HeaderText="Nro Registro" HtmlEncode="false"/>
                    <asp:BoundField DataField="Edificio" HeaderText="Edificio" HtmlEncode="false"/>
                    <asp:BoundField DataField="nroHabitacion" HeaderText="Nro Habitación" HtmlEncode="false"/>
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" HtmlEncode="false"/>
                    <asp:BoundField DataField="tipoLimpieza" HeaderText="Tipo" HtmlEncode="false"/>
                    <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" HtmlEncode="false"/>
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" HtmlEncode="false"/>
                    <asp:BoundField DataField="hora" HeaderText="Hora" HtmlEncode="false"/>
                    <asp:BoundField DataField="Detalles" HeaderText="Observación" HtmlEncode="false"/>
                </Columns>
            </asp:GridView>
        </div>

        <div class="container-btns">
        
        </div>

    </div>
</asp:Content>


