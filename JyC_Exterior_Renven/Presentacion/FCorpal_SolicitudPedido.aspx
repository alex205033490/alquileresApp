<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true" CodeBehind="FCorpal_SolicitudPedido.aspx.cs" Inherits="JyC_Exterior.Presentacion.FCorpal_SolicitudPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Corpal Srl</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../Images/naxsnax.png" rel="icon">
    <link href="../Images/naxsnax.png" rel="apple-touch-icon">

    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet" />
    <link href="../Styles/StyleUpon.css" rel="stylesheet" type="text/css" />

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Bootstrap JS (con Popper) -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.min.js"></script>
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pedido-form">
        <div class="container-main">

            <div class="form_solicitudPedido">
                <h2 class="tittle">Solicitud De Pedido</h2>

                <!-- DATOS -->
                <asp:UpdatePanel ID="updatePanelGet_IID" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="form_datosPedido mb-3">

                            <div class="row">
                                <div class="item_Nro col-4">
                                    <p class="mb-1">Nro:</p>
                                    <asp:TextBox ID="txt_Nro" runat="server" CssClass="form-control" AutoComplete="off" ReadOnly="True"></asp:TextBox>
                                </div>

                                <div class="item_solicitante col-7">
                                    <p class="mb-1">Solicitante:</p>
                                    <asp:TextBox ID="txt_Solicitante" runat="server" CssClass="form-control" AutoComplete="off" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="item_fechaEntrega col-6">
                                    <p class="mb-1">Fecha de Entrega:</p>
                                    <asp:TextBox ID="txt_fechaEntrega" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="tx_fechaEntrega_CalendarExtender" runat="server"
                                        TargetControlID="txt_fechaEntrega"></asp:CalendarExtender>
                                </div>

                                <div class="item_horaEntrega col-6">
                                    <p class="mb-1">Hora de Entrega:</p>
                                    <asp:TextBox ID="txt_horaEntrega" runat="server" CssClass="form-control" AutoComplete="off" placeholder="HH:mm:ss"></asp:TextBox>
                                </div>
                            </div>

                            <div class="item_cliente">
                                <p class="mb-1">Cliente:</p>
                                <asp:TextBox ID="txt_cliente" runat="server" CssClass="form-control" AutoComplete="off" AutoPostBack="true" OnTextChanged="txt_cliente_TextChanged1" placeholder="Ingrese un nombre de cliente para buscar"></asp:TextBox>

                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gv_clientesPedido" AutoGenerateColumns="false" CssClass="table table-bordered" OnSelectedIndexChanged="gv_clientesPedidos_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                            <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                            <asp:BoundField DataField="tiendaname" HeaderText="Nombre" HtmlEncode="false" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>


                        <!-- DATOS PRODUCTO -->
                        <div class="form_datosProducto rounded border">
                            <h4>Solicitud de Productos </h4>


                            <div class="item_nomProducto">
                                <p class="mb-1">Producto</p>
                                <asp:TextBox ID="txt_nomProducto" runat="server" CssClass="form-control" AutoComplete="off" OnTextChanged="txt_nomProducto_TextChanged" AutoPostBack="true" placeholder="Ingrese un producto para buscar"></asp:TextBox>
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gv_productosPedidos" AutoGenerateColumns="false" OnSelectedIndexChanged="gv_productosPedidos_SelectedIndexChanged" EnableViewState="true" CssClass="table table-bordered mt-2">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                            <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                            <asp:BoundField DataField="producto" HeaderText="Producto" HtmlEncode="false" />
                                            <asp:BoundField DataField="medida" HeaderText="Medida" />
                                            <asp:BoundField DataField="precio" HeaderText="Precio" />
                                            <asp:BoundField DataField="StockAlmacen" HeaderText="Stock" />

                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                            <div class="row">
                                <div class="item_cantidadProducto col-6">
                                    <p class="mb-1">Cantidad</p>
                                    <asp:TextBox ID="txt_cantidadProducto" CssClass="form-control" AutoComplete="off" runat="server" oninput="convertDotComma(event);"></asp:TextBox>
                                </div>

                                <div class="item_tipoSolicitud mb-2 col-6">
                                    <p class="mb-1">Tipo de Solicitud</p>
                                    <asp:DropDownList ID="dd_tipoSolicitud" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="VENTA">Venta</asp:ListItem>
                                        <asp:ListItem Value="DEGUSTACION">Degustación</asp:ListItem>
                                        <asp:ListItem Value="MUESTRA">Muestra</asp:ListItem>
                                        <asp:ListItem Value="OTROS">Otros</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="contenedor_btnAdd mb-2 col-12">
                                <asp:Button runat="server" ID="btn_addProducto" Text="Agregar Producto" CssClass="btn btn-dark" OnClick="btn_addProducto_Click" />
                            </div>

                            <div class="table-responsive">
                                <asp:GridView runat="server" ID="gv_listaProductosPedido" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCommand="gv_listaProductosPedido_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEliminar" runat="server" Text="eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("codigo") %>' CssClass="btn btn-danger btn-sm " />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                        <asp:BoundField DataField="producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="medida" HeaderText="Medida" />
                                        <asp:BoundField DataField="precio" HeaderText="Precio" />
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="tipoSolicitud" HeaderText="Tipo de Solicitud" />
                                        <asp:BoundField DataField="precioTotal" HeaderText="Precio Total" />
                                    </Columns>
                                </asp:GridView>
                            </div>


                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_POSTPedido" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <br />

                <div class="container_btnInsertPedido">
                    <asp:Button ID="btn_POSTPedido" runat="server" Text="Registrar Pedido" CssClass="btn btn-success mr-3" OnClick="btn_POSTPedido_Click" />
                    <asp:Button ID="btn_Inicio" runat="server" Text="Atrás" CssClass="btn btn-danger" OnClick="btn_Inicio_Click" />
                </div>

                <br />
            </div>
        </div>
    </div>
    <script src="../js/jsPedido.js" type="text/javascript"></script>
</asp:Content>
