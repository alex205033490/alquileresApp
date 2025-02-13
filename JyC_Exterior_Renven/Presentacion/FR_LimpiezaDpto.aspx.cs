using JyC_Exterior.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Globalization;
using jycboliviaASP.net.Presentacion;
using System.Web.Services;
using System.Web.Script.Services;

namespace JyC_Exterior.Presentacion
{
    public partial class FR_ModLimpiezaDep : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(18) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                cargarTiposLimpieza();
                CargarDatos();
            }
            NA_Responsables nResp = new NA_Responsables();
            string usu = Session["NameUser"].ToString();
            string pass = Session["passworuser"].ToString();

            int codRlimpieza = nResp.getCodUsuario(usu, pass);
            //lb_codrlimpieza.Text = nResp.get_responsable(codRlimpieza).Tables[0].Rows[0][0].ToString();
        }

        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }

        // autocomplete edificios
        [WebMethod]
        [ScriptMethod]
        public static string[] GetAutoCompletListEdificios(string prefixText, int count)
        {
            string edificio = prefixText;

            NA_limpiezaDep negocio = new NA_limpiezaDep();
            DataSet tuplas = negocio.get_mostrarEdificios(edificio);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i =0; i<fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;


        }

        // get mostrar departamentos 
        protected void txt_edificio_TextChanged(object sender, EventArgs e)
        {
            string nombreEdificio = txt_edificio.Text.Trim();

            NA_limpiezaDep na_ldep = new NA_limpiezaDep();
            DataSet tuplas = na_ldep.get_mostrarDep(nombreEdificio);

            if (tuplas.Tables[0].Rows.Count > 0)
            {
                gv_getDepartamentos.DataSource = tuplas;
                gv_getDepartamentos.DataBind();
                gv_getDepartamentos.Visible = true;
            } else
            {
                gv_getDepartamentos.DataSource = null;
                gv_getDepartamentos.DataBind();
            }
        }

        // SelectedGV Dpto
        protected void gv_getDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_getDepartamentos.SelectedIndex;
            GridViewRow row = gv_getDepartamentos.Rows[index];

            string codDep = row.Cells[1].Text;
            string edificio = row.Cells[2].Text;
            string nroHabitacion = row.Cells[3].Text;
            string direccionDep = row.Cells[4].Text;
            string nroInmueble = row.Cells[5].Text;
            string codSimec = row.Cells[6].Text;
            string nroDormitorios = row.Cells[7].Text;
            string ciudad = row.Cells[8].Text;

            if(nroHabitacion == "&nbsp;")
            {
                nroHabitacion = "";
            }
            if(nroInmueble == "&nbsp;")
            {
                nroInmueble = "";
            }
            if(nroDormitorios == "&nbsp;")
            {
                nroDormitorios = "0";
            }
            if (codSimec == "&nbsp;")
            {
                codSimec = "";
            }

            txt_codDepartamento.Text = codDep;

            Session["SLDedificio"] = edificio;
            txt_edificio.Text = edificio;

            Session["SLDnroInmueble"] = nroInmueble;
           
            txt_nroHabitacion.Text = nroHabitacion;

            Session["SLDnroDormitorios"] = nroDormitorios;

            txt_Direccion.Text = direccionDep;

            txt_Ciudad.Text = ciudad;

            Session["SLDcodSimec"] = codSimec;

            gv_getDepartamentos.Visible = false;
        }

        // cargar Items al GV
        private void CargarDatos()
        {
            NA_limpiezaDep negocio = new NA_limpiezaDep();
            try
            {
                int cat_item = 0;              

                if(dd_tipoLimpieza.SelectedIndex == 0)
                {
                    cat_item = 2;
                }
                else if (dd_tipoLimpieza.SelectedIndex == 1)
                {
                    cat_item = 3;
                }
                else
                {
                    showaler("El valor seleccionado no es un numero valido" );
                }

/*                int codigoTL;
                if(int.TryParse(dd_tipoLimpieza.SelectedValue, out codigoTL))
                {*/
                    DataSet ds = negocio.get_mostrarItemRepo(cat_item);

                    gv_items.DataSource = ds;
                    gv_items.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("Error al cargar los datos: " + ex.Message);
            }
        }


        /*-----------  POST visita a dpto  ---------------*/
        // BTN registro formulario
        protected void btn_registrarLimpdep_Click(object sender, EventArgs e)
        {
            //guardarFormulario();
            guardarForm();
            limpiarCamposDpto();
            limpiarCamposGvInsumos();
        }

        /* Metodo guardar form*/
        private void guardarForm()
        {
            try
            {
                validarDatos();
                var datosFormulario = ObtenerDatosForm();
                var exito = RegistrarVisitaDpto(datosFormulario);

                if (exito)
                {
                    ProcesoInsumos();
                    showaler("Se ha registrado la visita al departamento.");
                }
                else
                {
                    showaler("No se pudo registrar el formulario.");
                }

            }
            catch (Exception ex)
            {
                showaler($"Ha ocurrido un error inesperado: {ex.Message}");
            }
        }


        /* FUNCION obtener datos de formulario */
        private (int codigo, string codSimec, string nomInmueble, string nroInmueble, int nro_dormitorios, string direccionInmueble, string ciudad, string tipoLimpieza, int codRLimpieza, string observacion, int codTipoLimpieza, string denominacion) ObtenerDatosForm()
        {
            int codigo = int.Parse(txt_codDepartamento.Text);
            string codSimec = Session["SLDcodSimec"].ToString();
            string nomInmueble = Session["SLDedificio"].ToString();
            string nroInmueble = Session["SLDnroInmueble"].ToString();
            int nro_Dormitorios = int.Parse(Session["SLDnroDormitorios"].ToString());
            string direccionInmueble = txt_Direccion.Text;
            string ciudad = txt_Ciudad.Text;
            string tipoLimpieza = dd_tipoLimpieza.SelectedItem.ToString();


            int codRLimpieza = ObtenerCodigoResponsable();
            if (codRLimpieza == 0)
            {
                showaler("No se pudo obtener el código del responsable de limpieza.");
            }

            string observacion = txt_observacion.Text;
            int codTipoLimpieza = int.Parse(dd_tipoLimpieza.SelectedValue.ToString());

            string denominacion = txt_nroHabitacion.Text;

            return (codigo, codSimec, nomInmueble, nroInmueble, nro_Dormitorios, direccionInmueble, ciudad, tipoLimpieza, codRLimpieza, observacion, codTipoLimpieza, denominacion);
        }

        /* FUNCION registrar visita a dpto */
        private bool RegistrarVisitaDpto((int coddpto, string codSimec, string nombreInmueble, string nroInmueble, int nroHabitaciones, string direccionInmueble, string dptoInmueble, string tipoLimpieza, int codRLimpieza, string observacion, int codTipoLimpieza, string denominacion) datosFormulario)
        {
            try
            {
                NA_limpiezaDep negocio = new NA_limpiezaDep();
                return negocio.insert_limpiezadpto(datosFormulario.coddpto, datosFormulario.codSimec, datosFormulario.nombreInmueble, datosFormulario.nroInmueble, datosFormulario.nroHabitaciones, datosFormulario.direccionInmueble, datosFormulario.dptoInmueble, datosFormulario.tipoLimpieza, datosFormulario.codRLimpieza, datosFormulario.observacion, datosFormulario.codTipoLimpieza, datosFormulario.denominacion);
            }
            catch (Exception ex)
            {
                showaler($"Error al registrar: {ex.Message}");
                return false;
            }
        }

        /* METODO Proceso insumos */
        private void ProcesoInsumos()
        {
            try
            {
                int codRespLimpieza = ObtenerCodigoResponsable();
                if (codRespLimpieza == 0)
                {
                    showaler("Error: No se pudo obtener el código del responsable de limpieza.");
                    return;
                }
                int codRLimpieza = codRespLimpieza;
                int ultimaVisitaDptoID = ObtenerUltimaVisitaDpto(codRLimpieza);

                foreach(GridViewRow row in gv_items.Rows)
                {
                    int codItem = Convert.ToInt32(row.Cells[0].Text);

                    TextBox txtCantidad = (TextBox)row.FindControl("txt_cantidadItem");

                    string cantidad = "0";
                    decimal cantidadValor = 0;

                    if(!string.IsNullOrEmpty(txtCantidad.Text) && decimal.TryParse(txtCantidad.Text, out cantidadValor))
                    {
                        cantidad = txtCantidad.Text;
                    }

                    if (cantidadValor > 0)
                    {
                        bool insertadoItems = InsertarInsumo(ultimaVisitaDptoID, codItem, cantidad);

                        if (!insertadoItems)
                        {
                            showaler("Error al insertar el insumo con codigo = " + codItem);
                            return;
                        }
                    }
                }
                showaler("Se ha registrado la visita al departamento");
            }
            catch(Exception ex)
            {
                showaler($"Error al procesar insumos: {ex.Message}");
            }
        }

        /* FUNCION insertar detLimpiezaDpto */
        private bool InsertarInsumo(int ultimaVisitaDptoID, int codItem, string cantidad)
        {
            try
            {
                NA_limpiezaDep NEGOCIO = new NA_limpiezaDep();
                return NEGOCIO.insert_detLimpiezaDpto(ultimaVisitaDptoID, codItem, cantidad, ObtenerCodigoResponsable());

            }
            catch(Exception ex)
            {
                showaler($"Error al insertar en detLimpieza el insumo con codigo: {codItem}: {ex.Message}");
                return false;
            }
        }
        
        // METODO GUARDAR FORMULARIO error
        /*
        private void guardarFormulario()
        {
            try
            {
                validarDatos();

                int codigo = int.Parse(txt_codDepartamento.Text);

                string codSimec = Session["SLDcodSimec"].ToString();

                string nomInmueble = Session["SLDedificio"].ToString();

                string nroInmueble = Session["SLDnroInmueble"].ToString();

                int nro_dormitorios = int.Parse(Session["SLDnroDormitorios"].ToString());

                string direccionInmueble = txt_Direccion.Text;
                
                string ciudad = txt_Ciudad.Text;

                string tipoLimpieza = dd_tipoLimpieza.SelectedItem.ToString();

                int codRespLimpieza = ObtenerCodigoResponsable();
                if(codRespLimpieza == 0)
                {
                    showaler("Error: No se pudo obtener el código del responsable de limpieza.");
                    return;
                }
                int codRLimpieza = codRespLimpieza;

                string observacion = txt_observacion.Text;

                int codtipolimpieza = int.Parse(dd_tipoLimpieza.SelectedValue.ToString());

                string nro_habitacion = (txt_nroHabitacion.Text);

                bool exito = RegistrarVisitadpto(codigo, codSimec, nomInmueble, nroInmueble, nro_dormitorios, direccionInmueble, ciudad, tipoLimpieza, codRLimpieza, observacion, codtipolimpieza, nro_habitacion);

                NA_limpiezaDep nego_lDpto = new NA_limpiezaDep();

                if (exito)
                {
                    // Obtener ultimo cod de visita registrado
                    int ultimaVisitaDptoID = ObtenerUltimaVisitaDpto(codRespLimpieza);

                    // Recorrer gv
                    foreach (GridViewRow row in gv_items.Rows)
                    {
                        int codItem = Convert.ToInt32(row.Cells[0].Text);
                        TextBox txtCantidad = (TextBox)row.FindControl("txt_cantidadItem");
                        
                        int cantidad = 0;

                        if (!int.TryParse(txtCantidad.Text, out cantidad))
                        {
                            cantidad = 0;
                        }

                        
                        bool insertado = nego_lDpto.insert_detLimpiezaDpto(ultimaVisitaDptoID, codItem, cantidad, codRLimpieza);
                        if (!insertado)
                        {
                            showaler("Error al insertar el insumo con codigo: " + codItem);
                            return;
                        }
                    }
                    showaler("Se ha registrado la visita al departamento y los insumos asociados.");
                }
                else
                {
                    showaler("No se pudo registrar el formulario.");
                }
            }
            catch (Exception ex)
            {
                showaler($"Ha ocurrido un error inesperado: {ex.Message}");
            }
        }
        */
        
        /* METODO  para Obtener codResponsable */
        private int ObtenerCodigoResponsable()
        {
            try
            {
                NA_Responsables Nresp = new NA_Responsables();
                string username = Session["NameUser"].ToString();
                string password = Session["passworuser"].ToString();

                return Nresp.getCodUsuario(username, password);
            }
            catch (Exception ex)
            {
                showaler($"Error al obtener el codigo del Responsable: {ex.Message}");
                return 0;
            }
        }

        /*  FUNCION Obtener ultimo registro de visita a dpto */
        private int ObtenerUltimaVisitaDpto(int codRespLimpieza)
        {
            try 
            {
                NA_limpiezaDep negocio_ld = new NA_limpiezaDep();
                return negocio_ld.get_ultimoRegistroLimpDpto(codRespLimpieza);
            }
            catch (Exception ex)
            {
                showaler($"Error al obtener el ultimo registro de visita a dpto: {ex.Message}");
                return 0;
            }
        }

        /* METODO cargar insumos de limpieza en GV */
        private void cargarTiposLimpieza()
        {
            NA_limpiezaDep negocio = new NA_limpiezaDep();
            DataSet dsTiposLimpieza = negocio.get_mostrarTiposLimpieza();

            if (dsTiposLimpieza != null && dsTiposLimpieza.Tables.Count > 0)
            {
                dd_tipoLimpieza.DataSource = dsTiposLimpieza.Tables[0];
                dd_tipoLimpieza.DataTextField = "nombre";
                dd_tipoLimpieza.DataValueField = "codigo";
                dd_tipoLimpieza.DataBind();
            }
        }

        /* METODO limpiar campos dpto */
        private void limpiarCamposDpto()
        {
            txt_edificio.Text = "";
            gv_getDepartamentos.DataSource = null;
            gv_getDepartamentos.DataBind();
            txt_codDepartamento.Text = "";
            txt_nroHabitacion.Text = "";
            txt_Direccion.Text = "";
            txt_Ciudad.Text = "";
            dd_tipoLimpieza.SelectedIndex = 0;
            txt_observacion.Text = "";

            Session.Remove("SLDedificio");
            Session.Remove("SLDnroInmueble");
            Session.Remove("SLDcodSimec");
            Session.Remove("SLDnroDormitorios");
        }

        /*  METODO validar datos del Dpto  */
        private void validarDatos()
        {
            if (string.IsNullOrEmpty(txt_edificio.Text) || Session["SLDedificio"] == null)
            {
                showaler("Error: Busque y seleccione un edificio válido");
                return;
            }
            if (!int.TryParse(txt_codDepartamento.Text, out int codigo) || codigo <= 0)
            {
                showaler("Error: Codigo departamento inválido");
                return;
            }
        }

        /* METODO limpiar campo GV insumos */
        private void limpiarCamposGvInsumos()
        {
            foreach(GridViewRow row in gv_items.Rows)
            {
                TextBox txtCantidad = (TextBox)row.FindControl("txt_cantidadItem");

                if(txtCantidad != null)
                {
                    txtCantidad.Text = string.Empty;
                }    
            }
        }

        /*      showAlert    */
        private void showaler(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        protected void dd_tipoLimpieza_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void btn_volverAtras_Click(object sender, EventArgs e)
        {
            limpiarCamposGvInsumos();
            limpiarCamposDpto();
            Response.Redirect("FA_MenuPorArea.aspx");
        }
    }
}
