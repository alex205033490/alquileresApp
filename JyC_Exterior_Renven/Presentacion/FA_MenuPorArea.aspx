<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FA_MenuPorArea.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_MenuPorArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">  
    <title>Renven</title>
    <link rel="Shortcut Icon" href="../img/renvenLogo.png"/> 
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
            <div class="row">
                <div class="col-lg-12">
                <a href="#portfolio">
                 <!--  imagen del logo  -->
                    <img class="img-principal" src="../img/renvenLogo.png" alt=""/> </a>  
                    <div class="intro-text">
                        <span class="tittle-Principal">RENVEN</span>                        
                        
                    </div>
                </div>
            </div>
        </div>
    </header>

    <!-- Portfolio Grid Section -->
       <div class="container_menu">
    
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <p class="pTitulo">
                        <span class="spTitulo">
                            Servicios
                        </span>
                    </p>
                    <br />
                </div>
            </div>

            <div class="row">               
                
                <div class="col-6 portfolio-item">
                    <a href="./FR_ModLimpiezaDep.aspx" class="portfolio-link" data-toggle="modal">
                        <img src="../img/logoLimpieza22.png" class="img-services" alt=""/>
                    </a>
                    <h6>Servicio de limpieza</h6>
                </div>

                <div class="col-6 portfolio-item">
                    <a href="./FR_ActivosDpto.aspx" class="portfolio-link" data-toggle="modal">
                        <img src="../img/logoMueble.png" class="img-services" alt=""/>
                    </a>
                    <h6>Administración de activos</h6>
                </div>


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
