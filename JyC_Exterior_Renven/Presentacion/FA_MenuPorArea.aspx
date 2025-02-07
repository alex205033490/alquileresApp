<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FA_MenuPorArea.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_MenuPorArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">  
    <title>Renven</title>
    <link rel="Shortcut Icon" href="../img/renvenLogo3.png"/> 
    <meta charset="UTF-8" name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="../Styles/Style_MenuArea.css" rel="stylesheet" type="text/css" />

    <!-- Bootstrap Core CSS -->    
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Theme CSS -->
    <link href="../jquery/css/freelancer.css" rel="stylesheet" type="text/css" />    
    <!-- Custom Fonts -->
    <link href="../jquery/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />    
</head>

<body>
    <form id="form1" runat="server">
    <div>
     <!-- Header -->
    <header>
        <div class="container123">
                <a href="#portfolio">
                 <!--  imagen del logo  -->
                    <img class="img-principal" src="../img/logoRenven.png" alt=""/> </a>  
           
        </div>
    </header>

    <!-- Portfolio Grid Section -->
       <div class="container_menu">
            <div class="row_menu">               
                
                <div class="col-5 portfolio-itemMenu" style="background-color: #9a9ea57a;">
                    <a href="./FR_ModLimpiezaDep.aspx" class="portfolio-link" data-toggle="modal">
                        <img src="../img/logoLimpieza2.png" class="img-servicesMenu" alt=""/>
                    </a>
                    <p class="title_menu">Servicio de limpieza</p>
                </div>

                <div class="col-5 portfolio-itemMenu" style="background-color:#9a9ea57a;">
                    <a href="./FR_ActivosDpto.aspx" class="portfolio-link" data-toggle="modal">
                        <img src="../img/logoMueble.png" class="img-servicesMenu" alt=""/>
                    </a>
                    <p class="title_menu">Administración de activos</p>
                </div>
                
                <div class="col-5 portfolio-itemMenu" style="background-color:#9a9ea57a;">
                    <a href="./FR_AdmLimpiezaDpto.aspx" class="portfolio-link" data-toggle="modal">
                        <img src="../img/logoLimpieza2.png" class="img-servicesMenu" alt=""/>
                    </a>
                    <p class="title_menu">Gestión de limpieza y reposión de insumos</p>
                </div>
                
                
                <div class="col-5 portfolio-itemMenu" style="background-color:#9a9ea57a;">
                    <a href="./FR_TraspasoAlmacen.aspx" class="portfolio-link" data-toggle="modal">
                        <img src="../img/traspasoDpto.png" class="img-servicesMenu" alt=""/>
                    </a>
                    <p class="title_menu">Traspaso de activos</p>
                </div>


            </div>
           </div>
  
   

    <!-- jQuery -->    
    <script src="../jquery/jquery/jquery.min.js" type="text/javascript"></script>

    <!-- Bootstrap Core JavaScript -->    
    <script src="../jquery/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>

    <!-- Plugin JavaScript -->
    <script src="http://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>

    <!-- Contact Form JavaScript -->
    <script src="../jquery/js/jqBootstrapValidation.js" type="text/javascript"></script>
    <script src="../jquery/js/contact_me.js" type="text/javascript"></script>    

    <!-- Theme JavaScript -->    
    <script src="../jquery/js/freelancer.min.js" type="text/javascript"></script>


    
    </div>
    </form>
</body>
</html>
