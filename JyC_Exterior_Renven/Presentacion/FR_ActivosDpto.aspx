<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true" CodeBehind="FR_ActivosDpto.aspx.cs" Inherits="JyC_Exterior.Presentacion.FR_ActivosDpto" %>

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
    <script type="text/javascript">
        function disableCodActivo() {
            const input = document.getElementById("txt_codActivo");

            if (input) {
                input.addEventListener("keydown", function (e) {
                    e.preventDefault();
                });

                input.addEventListener("input", function () {
                    input.value = "";
                });
            }
        }
        document.addEventListener("DOMContentLoaded", disableCodActivo);


        var scrollPos = 0;
        function saveScroll() {
            var container = document.querySelector(".container_listActivos");
            if (container) {
                scrollPos = container.scrollTop;
            }
        }
        function restoreScroll() {
            var container = document.querySelector(".container_listActivos");
            if (container){
                container.scrollTop = scrollPos;
            }
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            saveScroll();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            restoreScroll();
            disableCodActivo();
        })

        function onItemSelectedItemActivo(sender, args) {
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

        $(document).ready(function () {
            var table = $(".table-sticky");

            if (table.find("thead").length === 0) {
                table.prepend("<thead>") + table.find("tr:first").html() + "</thead>";
                table.find("tr:first").remove();
            }
        });


    </script>

    <asp:UpdatePanel ID="updatePanel_listAddActivos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-activosDpto">
                <div class="container-main">

                    <div class="title_principal">
                        <h1 class="">| GESTIÓN DE ACTIVOS |</h1>
                    </div>


                    <div class="form_datosDpto mb-1">
                        <h4>Datos del Departamento</h4>

                        <div class="item_Edificio col-12 mb-2">
                            <p class="p_nombre mb-1">Edificio:</p>

                            <asp:TextBox ID="txt_edificio" runat="server" CssClass="form-control" Style="font-size: 0.8rem;" AutoComplete="off" OnTextChanged="txt_edificio_TextChanged" AutoPostBack="true" placeholder="Ingrese el nombre de un edificio"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txt_edificio_AutoCompleteExtender" runat="server"
                                TargetControlID="txt_edificio"
                                CompletionSetCount="12"
                                MinimumPrefixLength="1" ServiceMethod="getLIstaEdificio"
                                UseContextKey="True"
                                CompletionListCssClass="CompletionList"
                                CompletionListItemCssClass="CompletionlistItem"
                                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                            </asp:AutoCompleteExtender>

                            <div class="table-responsive list_dpto mt-1">
                                
                                <asp:GridView ID="gv_getDepartamentos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped gv_dpto" OnSelectedIndexChanged="gv_getDepartamentos_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button style="font-size:10px;" ID="btnseleccionar_dpto" runat="server" Text="Seleccionar" CommandName="Select" CssClass="btn btn-success"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigo" HeaderText="Codigo Dpto"/>
                                        <asp:BoundField DataField="edificio" HeaderText="Edificio" HtmlEncode="false" />
                                        <asp:BoundField DataField="denominacion" HeaderText="Nro habitación" HtmlEncode="false" />
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo" HtmlEncode="false" />
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección" HtmlEncode="false" />
                                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" HtmlEncode="false" />
                                        <asp:BoundField DataField="codSimec" HeaderText="Cod Simec" HtmlEncode="false" />
                                        <asp:BoundField DataField="nroInmueble" HeaderText="Nro Inmueble" HtmlEncode="false" />
                                        <asp:BoundField DataField="nrohabitaciones" HeaderText="Nro dormitorios" />

                                        </Columns>
                                    </asp:GridView>

                            </div>
                        </div>

                        <div class="row">
                            <div class="item_departamento col-3">
                                <p class="p_nombre mb-1">Nro Habitación:</p>
                                <asp:TextBox ID="txt_Habitacion" runat="server" CssClass="txt_info form-control" AutoComplete="off" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="item_direccion col-5">
                                <p class="p_nombre mb-1">Dirección:</p>
                                <asp:TextBox ID="txt_Direccion" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="4" Wrap="true" CssClass="form-control txt_info txt_infoMultiline" AutoComplete="off"></asp:TextBox>
                            </div>

                            <div class="item_tipo col-4">
                                <p class="p_nombre mb-1">Tipo:</p>
                                <asp:TextBox ID="txt_tipo" runat="server" CssClass="txt_info form-control" AutoComplete="off" ReadOnly="true" ></asp:TextBox>
                            </div>

                        </div>


                    </div>

                    <!--            LISTA DE ACTIVOS                     -->


                    <div class="form_datosActivo">
                        <h4>Lista de activos</h4>
                        <div class="container_activo">

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
                            
                            <div class="row mb-2">
                                <div class="col-4">
                                    <div class="item_cod mb-3">
                                        <p class="p_nombre mb-1">Codigo</p>
                                        <asp:TextBox ID="txt_codActivo" ClientIDMode="Static" AutoComplete="off" runat="server" style="font-size:0.8rem;" CssClass="form-control txt_codActivo txt_info"></asp:TextBox>
                                    </div>
                                    <div class="item_cantidad mb-2">
                                        <p class="p_nombre mb-1">Cantidad</p>
                                        <asp:TextBox ID="txt_cantidadActivo" type="number" runat="server" Style="font-size: 0.8rem;" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-8">
                                    <div class="item_nombre col-12 mb-2">
                                        <p class="p_nombre mb-1">Activo:</p>
                                        <asp:TextBox ID="txt_activo" runat="server" Style="font-size: 0.8rem;" CssClass="form-control" AutoComplete="off" AutoPostBack="true" placeholder="Seleccione un item"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txt_activo_AutoCompleteExtender" runat="server"
                                            TargetControlID="txt_activo"
                                            CompletionSetCount="12"
                                            MinimumPrefixLength="1" ServiceMethod="GetlistActivos"
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList"
                                            CompletionListItemCssClass="CompletionlistItem"
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" 
                                            OnClientItemSelected="onItemSelectedItemActivo">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <div class="item_almacen col-12 mb-2">
                                        <p class="p_nombre mb-1">Almacén:</p>
                                        <asp:DropDownList ID="dd_listAlmacen" runat="server" Style="font-size: 0.7rem" CssClass="form-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="">
                                <div class="btn_addActivo col-12">
                                    <asp:Button ID="btn_addActivo" runat="server" CssClass="btn btn-dark w-100" Style="font-size: 15px;" Text="Agregar Activo" OnClick="btn_addActivo_Click" />
                                </div>
                                </div>
                            </div>

                        </asp:Panel>

                        <div class="table-responsive container_listActivos mt-2 mb-2" data-clientid="<%= gv_listActivos.ClientID %>"">
                            <asp:GridView ID="gv_listActivos" runat="server" AutoGenerateColumns="false" CssClass="table table-sticky table-striped gv_listActivos" 
                                OnSelectedIndexChanged="gv_listActivos_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnseleccionar_activo" style="font-size:12px;" runat="server" Text="Seleccionar" CommandName="select" CssClass="btn btn-success"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codigo" HeaderText="Codigo" HtmlEncode="false" />
                                    <asp:BoundField DataField="nombre" HeaderText="Activo" HtmlEncode="false" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-12">
                            <asp:Button ID="btn_registrarForm" runat="server" Text="REGISTRAR FORMULARIO" CssClass="btn btn-success col-12" OnClick="btn_registrarForm_Click" />
                        </div>
                        <div class="container_BTNs mt-2 mb-3">
                            <asp:Button ID="btn_limpiar" runat="server" CssClass="btn btn-secondary col-5 " Style="font-size: 15px;" Text="Limpiar Formulario" OnClick="btn_limpiar_Click" />
                            <asp:Button ID="btn_volverAtras" runat="server" CssClass="btn btn-danger col-5" Style="font-size: 15px;" Text="Volver Atras" OnClick="btn_volverAtras_Click" />
                        </div>
                    </div>
                </div>
                        <br />
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gv_listActivos" EventName="SelectedIndexChanged" />
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_registrarForm" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
