﻿<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true" CodeBehind="FR_TraspasoAlmacen.aspx.cs" Inherits="JyC_Exterior.Presentacion.FR_TraspasoAlmacen" %>

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
    <script>
        function onItemSelectedItemTraspasoAlm(sender, args) {
            console.log("Item Selected");
            var dataItem = args.get_value();
            console.log("DataItem:", dataItem);

            var parts = dataItem.split("|");

            if (parts.length > 1) {
                var codigo = parts[0];
                var item = parts[1];

                document.getElementById('<%= txt_activo.ClientID %>').value = item;
                document.getElementById('<%= txt_codActivo.ClientID %>').value = codigo;
            }
        }
    </script>


    <asp:UpdatePanel ID="updatePanel_listAddActivos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-activosDpto">
                <div class="container-main">
                   <!-- <div class="title_principal">
                        <h1 class="">Traspaso de activos</h1>
                    </div>  -->

                    <div class="form_datosDpto mb-3">
                        <div class="mb-3">
                            <h3>Datos del almacén</h3>
                        </div>

                        <div class="item_almacen col-12 mb-2">
                            <p class="p_nombre mb-1">Almacén Origen:</p>
                            <asp:DropDownList ID="dd_almacenOrigen" runat="server" Style="font-size: 0.7rem" CssClass="form-select" OnSelectedIndexChanged="dd_almacenO_SelectedIndexChanged">
                               </asp:DropDownList>
                        </div>

                        <div class="item_almacen col-12 mb-2">
                            <p class="p_nombre mb-1">Almacén Destino:</p>
                            <asp:DropDownList ID="dd_almacenDestino" runat="server" Style="font-size: 0.7rem" CssClass="form-select" OnSelectedIndexChanged="dd_almacenD_SelectedIndexChanged">
                               </asp:DropDownList>
                        </div>

                    </div>

                    <!--            LISTA DE ACTIVOS                     -->


                    <div class="form_datosActivo">
                        <h3>Lista de activos</h3>
                        <div class="container_activo">

                            <!-- LISTA DE ACTIVOS AGREGADOS EN LA LISTA -->
                            <asp:GridView ID="gv_activos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped gv_dpto" OnRowCommand="gv_activosDelete">
                                <Columns>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminar" runat="server" Style="font-size: 12px;" Text="Quitar" CommandName="Eliminar" CommandArgument='<%# Eval("codigo") %>' CssClass="btn btn-danger btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="codigo" HeaderText="Codigo" HtmlEncode="false" />
                                    <asp:BoundField DataField="nombre" HeaderText="Activo" HtmlEncode="false" />
                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" HtmlEncode="false" />
                                </Columns>
                            </asp:GridView>

                        </div>


                        <asp:Panel ID="Panel_addItem" runat="server" DefaultButton="btn_addActivo">
                            <div class="row mb-2 container-addActivo">
                                
                                <div class="col-4 mb-2">
                                    <div class="item_codIActivo mb-2">
                                        <p class="p_nombre mb-1">Codigo:</p>
                                        <asp:TextBox ID="txt_codActivo" AutoComplete="off" runat="server" CssClass="form-control" Style="font-size:0.8rem;"></asp:TextBox>
                                    </div>
                                    <div class="item_cantidad">
                                        <p class="p_nombre mb-1">Cantidad</p>
                                        <asp:TextBox ID="txt_cantidadActivo" type="number" runat="server" Style="font-size: 0.8rem;" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-8">
                                    <div class="item_nombre col-12 mb-2">
                                        <p class="p_nombre mb-1">Activo:</p>
                                        <asp:TextBox ID="txt_activo" runat="server" Style="font-size: 0.8rem;" CssClass="form-control" AutoComplete="off" AutoPostBack="true" ></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txt_activo_AutoCompleteExtender" runat="server"
                                            TargetControlID="txt_activo"
                                            CompletionSetCount="12"
                                            MinimumPrefixLength="1" ServiceMethod="GetListActivos"
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList"
                                            CompletionListItemCssClass="CompletionlistItem"
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                            OnClientItemSelected="onItemSelectedItemTraspasoAlm">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <div class="mt-4 w-100 col-8">
                                            <asp:Button ID="btn_addActivo" runat="server" CssClass="btn btn-dark " Style="font-size: 16px; width:100%;" Text="Agregar Activo" OnClick="btn_addActivo_Click" />
                                    </div>
                                </div>

                                
                            </div>
                        </asp:Panel>

                        <!-- GV LISTA DE ACTIVOS -->
                        <div class="table-responsive container_listActivos mt-1">
                            <asp:GridView ID="gv_listActivos" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="table table-striped gv_dpto" OnSelectedIndexChanged="gv_listActivos_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button id="btn_seleccionar" runat="server" style="font-size:12px;"  Text="Seleccionar" CssClass="btn btn-success" CommandName="select" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codigo" HeaderText="Codigo" HtmlEncode="false" />
                                    <asp:BoundField DataField="nombre" HeaderText="Activo" HtmlEncode="false" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="container_BTNs mt-2 mb-3">
                            <asp:Button ID="btn_limpiar" runat="server" CssClass="btn btn-secondary col-5 " Style="font-size: 15px;" Text="Limpiar Formulario" OnClick="btn_limpiar_Click" />
                            <asp:Button ID="btn_volverAtras" runat="server" CssClass="btn btn-danger col-5" Style="font-size: 15px;" Text="Volver Atras" OnClick="btn_volverAtras_Click" />
                        </div>
                        <div class="col-12">
                            <asp:Button ID="btn_registrarForm" runat="server" Text="REGISTRAR FORMULARIO" CssClass="btn btn-success col-12" OnClick="btn_registrarForm_Click" />
                        </div>
                    </div>
                </div>
                        <br />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_registrarForm" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>


    <!-- <script src="../js/js_RenvenTraspasoActivos.js" type="text/javascript"></script> -->
</asp:Content>