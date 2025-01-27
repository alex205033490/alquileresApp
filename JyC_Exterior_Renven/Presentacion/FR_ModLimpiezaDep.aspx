<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true" CodeBehind="FR_ModLimpiezaDep.aspx.cs" Inherits="JyC_Exterior.Presentacion.FR_ModLimpiezaDep" %>

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

    <div class="form-limpiezaDep">
        <div class="container-main">
            <div class="title_principal">
                <h1 class="">Registro de visita a departamento</h1>
            </div>

            <div class="form_datosDep">
                <h3>Datos del Departamento</h3>
                <div class="row mb-2">
                    <div class="item_Edificio col-12">
                        <p class="p_nombre mb-1">Edificio:</p>
                        <asp:TextBox ID="txt_edificio" runat="server" Style="font-size: 12px" CssClass="form-control" AutoComplete="off" OnTextChanged="txt_edificio_TextChanged" AutoPostBack="true" placeholder="Ingrese el nombre de un edificio"></asp:TextBox>

                        <div class="table-responsive list_dpto mt-1">
                            <asp:GridView ID="gv_getDepartamentos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped gv_dpto" OnSelectedIndexChanged="gv_getDepartamentos_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                    <asp:BoundField DataField="codDep" HeaderText="Codigo Dpto" HtmlEncode="false" />
                                    <asp:BoundField DataField="Edificio" HeaderText="Edificio" HtmlEncode="false" />
                                    <asp:BoundField DataField="nroInmueble" HeaderText="Numero Inmueble" HtmlEncode="false" />
                                    <asp:BoundField DataField="nroDormitorios" HeaderText="Numero Dormitorios" HtmlEncode="false" />
                                    <asp:BoundField DataField="direccionDep" HeaderText="Dirección" HtmlEncode="false" />
                                    <asp:BoundField DataField="ciudad" HeaderText="Ciudad" HtmlEncode="false" />
                                    <asp:BoundField DataField="codSimec" HeaderText="Codigo Simec" HtmlEncode="false" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="row mb-2 container_ddpto">
                    <div class="item_departamento col-6">
                        <p class="p_nombre mb-1">Cod Departamento:</p>
                        <asp:TextBox ID="txt_codDepartamento" style="background-color: #204d773b;" runat="server" CssClass="form-control" AutoComplete="off" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="item_nroHabitaciones col-6 mb-2">
                        <p class="p_nombre mb-1">Nro Habitaciones:</p>
                        <asp:TextBox ID="txt_nroHabitaciones" runat="server" style="background-color: #204d773b;" CssClass="form-control" AutoComplete="off" ReadOnly="true" HtmlEncode="false"></asp:TextBox>
                    </div>


                    <div class="item_direccion col-7 mb-1">
                        <p class="p_nombre mb-1">Dirección:</p>
                        <asp:TextBox ID="txt_Direccion" Style="font-size: 0.6rem; height: 3.5rem; background-color: #204d773b;" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="4" Wrap="true" CssClass="form-control txt_dir" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="item_depInmueble col-5">
                        <p class="p_nombre mb-1">Ciudad:</p>
                        <asp:TextBox ID="txt_Ciudad" runat="server" style="font-size:13px; background-color: #204d773b;" CssClass="form-control" AutoComplete="off" ReadOnly="true"></asp:TextBox>

                    </div>
                </div>
            </div>

            <div class="row">
                <div class="item_tipoLimpieza col-6">
                    <p class="p_nombre mb-1">Tipo de limpieza:</p>
                    <asp:DropDownList ID="dd_tipoLimpieza" style="font-size:0.7rem;" runat="server" CssClass="form-select">
                    </asp:DropDownList>

                </div>
                <div class="item_direccion col-6">
                    <p class="p_nombre mb-1">Observación:</p>
                    <asp:TextBox ID="txt_observacion" Style="font-size: 0.6rem; height: 3.5rem;" ReadOnly="false" runat="server" TextMode="MultiLine" Rows="4" Wrap="true" CssClass="form-control txt_dir" AutoComplete="off"></asp:TextBox>


                </div>

            </div>

            <div class="container_itemsRepo mt-4">
                <div class="tittle_itemRepo">
                    <h3>Lista de Insumos</h3>
                </div>

                <div class="listItemsRepo" >
                    <asp:GridView ID="gv_items" runat="server" AutoGenerateColumns="false" CssClass="gv_items table table-bordered">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="CODIGO" HtmlEncode="false" />
                            <asp:BoundField DataField="nombre" HeaderText="INSUMO" HtmlEncode="false" />
                            <asp:TemplateField HeaderText="CANTIDAD" ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <div class="col-12">
                                        <asp:TextBox ID="txt_cantidadItem" style="font-size:0.7rem;" type="number" runat="server" CssClass="form-control cantidad-textbox"
                                            TextMode="SingleLine" MaxLength="3"></asp:TextBox>
                                    </div>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>

            <div class="container_btn">
                <asp:Button ID="btn_registrarLimpdep" runat="server" Text="Registrar Reposición" CssClass="btn btn-success" OnClick="btn_registrarLimpdep_Click" />

            </div>
            <br />

        </div>
    </div>
</asp:Content>
