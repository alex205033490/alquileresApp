<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuPlantillaPrincipal.ascx.cs" Inherits="JyC_Exterior.NegocioPlantilla.MenuPlantillaPrincipal" %>
<%-- Incluir archivo CSS en el Head --%>
<asp:PlaceHolder ID="headPlaceholder" runat="server"></asp:PlaceHolder>

<nav class="navbar navbar-dark container-navbar" style="background-color:#000000f2;" aria-label="First navbar example">
    <div class="container-fluid">
      
        <div class="d-flex align-items-center justify-content-between">
          <a href="../Presentacion/FA_MenuPorArea.aspx" class="logo d-flex align-items-center">
            <img src="../img/logoRenven2.png" style="width:150px; height:auto;" alt="">
              
          </a>
        </div><!-- End Logo -->

      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarsExample01" aria-controls="navbarsExample01" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>

        
      <div class="collapse navbar-collapse" id="navbarsExample01">
        <ul class="navbar-nav me-auto container-ul">

        <li class="nav-item container-navItem">
          <a class="nav-link" href="./FA_MenuPorArea.aspx">Menú Principal</a>
        </li>

        <li class="nav-item container-navItem">
            <a class="nav-link active" aria-current="page" href="./FR_ModLimpiezaDep.aspx">Servicio de limpieza</a>
        </li>

        <li class="nav-item container-navItem">
          <a class="nav-link" href="./FR_ActivosDpto.aspx">Administración de activos</a>
        </li>

            <li class="nav-item container-navItem">
                <a class="nav-link" href="./FR_AdministracionLimpiezaDpto">Gestión de limpieza y reposición</a>
            </li>


        <li class="nav-item container-navItem">
          <a class="nav-link" href="./FR_TraspasoAlmacen.aspx">Traspaso de activos</a>
        </li>


        <li class="nav-item container-navItem">
          <a class="nav-link" href="FA_Login.aspx">Salir</a>
        </li>

        </ul>
      </div>

    </div>
  </nav>