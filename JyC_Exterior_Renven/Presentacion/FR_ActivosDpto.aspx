<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true" CodeBehind="FR_ActivosDpto.aspx.cs" Inherits="JyC_Exterior.Presentacion.FR_ActivosDpto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RENVEN</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="Shortcut Icon" href="../img/renvenLogo.png" />
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


    <asp:UpdatePanel ID="updatePanel_listAddActivos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-activosDpto">
                <div class="container-main">
                    <div class="title_principal">
                        <h1 class="">ADMINISTRACIÓN DE ACTIVOS</h1>
                    </div>

                    <div class="form_datosDpto mb-3">
                        <h3>Datos del Departamento</h3>

                        <div class="item_Edificio col-12">
                            <p class="p_nombre mb-1">Edificio:</p>

                            <asp:TextBox ID="txt_edificio" runat="server" CssClass="form-control" Style="font-size: 0.8rem;" AutoComplete="off" OnTextChanged="txt_edificio_TextChanged" AutoPostBack="true" placeholder="Ingrese el nombre de un edificio"></asp:TextBox>

                            <div class="table-responsive list_dpto mt-1">

                                <asp:GridView ID="gv_getDepartamentos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped gv_dpto" OnSelectedIndexChanged="gv_getDepartamentos_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo Dpto" HtmlEncode="false" />
                                        <asp:BoundField DataField="Edificio" HeaderText="Edificio" HtmlEncode="false" />
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección" HtmlEncode="false" />
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>

                        <div class="row">
                            <div class="item_departamento col-4">
                                <p class="p_nombre mb-1">Codigo Dpto:</p>
                                <asp:TextBox ID="txt_codDepartamento" runat="server" CssClass="form-control" AutoComplete="off" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="item_direccion col-8 fs-3">
                                <p class="p_nombre mb-1">Dirección:</p>
                                <asp:TextBox ID="txt_Direccion" Style="font-size: 0.6rem; height: 3.5rem;" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="4" Wrap="true" CssClass="form-control txt_dir" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <!--            LISTA DE ACTIVOS                     -->


                    <div class="form_datosActivo">
                        <h3>Lista de activos</h3>
                        <div class="container_activo">

                                <asp:GridView ID="gv_activos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped gv_dpto" OnRowCommand="gv_activosDelete">
                                    <Columns>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEliminar" runat="server" Text="Quitar" CommandName="Eliminar" CommandArgument='<%# Eval("codigo") %>' CssClass="btn btn-danger btn-sm" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" HtmlEncode="false" />
                                        <asp:BoundField DataField="nombre" HeaderText="Activo" HtmlEncode="false" />
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" HtmlEncode="false" />
                                    </Columns>
                                </asp:GridView>

                            </div>


                                    <asp:Panel ID="Panel_addItem" runat="server" DefaultButton="btn_addActivo">
                            <div class="row">


                                <div class="item_nombre col-8">
                                    <p class="p_nombre mb-1">Item:</p>
                                    <asp:TextBox ID="txt_activo" runat="server" Style="font-size: 0.8rem;" CssClass="form-control" AutoComplete="off" AutoPostBack="true" placeholder="Busque y seleccione un item" OnTextChanged="txt_activo_TextChanged"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txt_activo_AutoCompleteExtender" runat="server"
                                        TargetControlID="txt_activo"
                                        CompletionSetCount="12"
                                        MinimumPrefixLength="1" ServiceMethod="GetlistActivos"
                                        UseContextKey="True"
                                        CompletionListCssClass="CompletionList"
                                        CompletionListItemCssClass="CompletionlistItem"
                                        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                    </asp:AutoCompleteExtender>
                                </div>

                                <div class="item_cantidad col-4">
                                    <p class="p_cantidad mb-1">Cantidad</p>
                                    <asp:TextBox ID="txt_cantidadActivo" type="number" runat="server" Style="font-size: 0.8rem;" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                                        </asp:Panel>
                            <div class="container_BTNs mt-2">
                                <asp:Button ID="btn_addActivo" runat="server" CssClass="btn btn-dark" Text="Agregar Activo" OnClick="btn_addActivo_Click" />
                                <asp:Button ID="btn_limpiar" runat="server" CssClass="btn btn-danger" Text="Limpiar Formulario" OnClick="btn_limpiar_Click" />
                                <asp:Button ID="btn_registrarForm" runat="server" Text="REGISTRAR" CssClass="btn btn-success" OnClick="btn_registrarForm_Click" />
                            </div>



                            <div class="table-responsive list_dpto mt-1">
                                <asp:GridView ID="gv_listActivos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped gv_dpto" OnSelectedIndexChanged="gv_listActivos_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" HtmlEncode="false" />
                                        <asp:BoundField DataField="nombre" HeaderText="Activo" HtmlEncode="false" />
                                    </Columns>
                                </asp:GridView>
                            </div>


                        </div>
                    </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_registrarForm" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>





</asp:Content>
