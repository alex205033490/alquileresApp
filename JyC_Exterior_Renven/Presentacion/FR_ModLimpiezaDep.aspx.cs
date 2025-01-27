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
                CargarDatos();
                cargarTiposLimpieza();
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

        // Seleccionar dpto
        protected void gv_getDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_getDepartamentos.SelectedIndex;
            GridViewRow row = gv_getDepartamentos.Rows[index];

            string codDep = row.Cells[1].Text;
            string edificio = row.Cells[2].Text;
            string nroInmueble = row.Cells[3].Text;
            string nroDormitorios = row.Cells[4].Text;
            string direccionDep = row.Cells[5].Text;
            string ciudad = row.Cells[6].Text;
            string codSimec = row.Cells[7].Text;

            if(nroInmueble == "&nbsp;")
            {
                nroInmueble = "";
            }
            if(nroDormitorios == "&nbsp;")
            {
                nroDormitorios = "0";
            }

            txt_codDepartamento.Text = codDep;

            Session["SLDedificio"] = edificio;
            txt_edificio.Text = edificio;

            Session["SLDnroInmueble"] = nroInmueble;
           
            txt_nroHabitaciones.Text = nroDormitorios;

            txt_Direccion.Text = direccionDep;

            txt_Ciudad.Text = ciudad;

            Session["SLDcodSimec"] = codSimec;

            gv_getDepartamentos.Visible = false;

            
        }

        // cargar itemsRepo en gv
        private void CargarDatos()
        {
            NA_limpiezaDep negocio = new NA_limpiezaDep();
            try
            {
                DataSet ds = negocio.get_mostrarItemRepo();

                gv_items.DataSource = ds;
                gv_items.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write("Error al cargar los datos: " + ex.Message);
            }

        }


        /*-----------  POST   LIMPIEZAdpto  ---------------*/

        private bool RegistrarVisitadpto(int coddpto, string codSimec, string nombreInmueble, string nroInmueble, int nroHabitaciones, string direccionInmueble, string dptoInmueble, string tipoLimpieza, int codRLimpieza, string observacion, int codTipoLimpieza)
        {
            try
            {
                NA_limpiezaDep negocio = new NA_limpiezaDep();
                return negocio.insert_limpiezadpto(coddpto, codSimec, nombreInmueble, nroInmueble, nroHabitaciones, direccionInmueble, dptoInmueble, tipoLimpieza, codRLimpieza, observacion, codTipoLimpieza);
            }
            catch (Exception ex)
            {
                showaler($"Error al registrar: {ex.Message}");
                return false;
            }
        }
       
        private void validarDatos()
        {
            if (string.IsNullOrEmpty(txt_edificio.Text)|| Session["SLDedificio"] == null)
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
        private void guardarFormulario()
        {
            try
            {
                // Validar que los valores de los TextBox sean correctos
                if (!int.TryParse(txt_codDepartamento.Text, out int codigo) || codigo <= 0)
                {
                    showaler("Error: Código de departamento inválido.");
                    return;
                }

                if (!int.TryParse(txt_nroHabitaciones.Text, out int nroHabitaciones) || nroHabitaciones < 0)
                {
                    showaler("Error: Número de habitaciones inválido.");
                    return;
                }
                

                int codRespLimpieza = ObtenerCodigoResponsable();
                if(codRespLimpieza == 0)
                {
                    showaler("Error: No se pudo obtener el código del responsable de limpieza.");
                    return;
                }
                
                string codSimec = Session["SLDcodSimec"].ToString();
                string nomInmueble = Session["SLDedificio"].ToString();
                string nroInmueble = Session["SLDnroInmueble"].ToString();
                
                string direccionInmueble = txt_Direccion.Text;
                string ciudad = txt_Ciudad.Text;
                string tipoLimpieza = dd_tipoLimpieza.SelectedItem.ToString();
                int codtipolimpieza = int.Parse(dd_tipoLimpieza.SelectedValue.ToString());
                int codRLimpieza = codRespLimpieza;
                string observacion = txt_observacion.Text;

                bool exito = RegistrarVisitadpto(codigo, codSimec, nomInmueble, nroInmueble, nroHabitaciones, direccionInmueble, ciudad, tipoLimpieza, codRLimpieza, observacion, codtipolimpieza);

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
                    showaler("No se pudo registrar la visita.");
                }
            }
            catch (Exception ex)
            {
                showaler($"Error {ex.Message}");
            }
        }
        
        /*  Obtener codResponsable */
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


        /*  Obtener ultimo registro visita dpto     */
        private int ObtenerUltimaVisitaDpto(int codRespLimpieza)
        {
            try 
            {
                NA_limpiezaDep negocio_ld = new NA_limpiezaDep();
                return negocio_ld.get_ultimoRegistroLimpDpto(codRespLimpieza);
            }
            catch (Exception ex)
            {
                showaler($"Error al obtener el ultimo registro de visita insertado. {ex.Message}");
                return 0;
            }
        }

        /* Listar tipos de limpieza */
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


        /*  Limpiar Campos  */
        private void limpiarCamposDpto()
        {
            txt_edificio.Text = "";
            gv_getDepartamentos.DataSource = null;
            gv_getDepartamentos.DataBind();
            txt_codDepartamento.Text = "";
            txt_nroHabitaciones.Text = "";
            txt_Direccion.Text = "";
            txt_Ciudad.Text = "";
            dd_tipoLimpieza.SelectedIndex = 0;
            txt_observacion.Text = "";

            Session.Remove("SLDedificio");
            Session.Remove("SLDnroInmueble");
            Session.Remove("SLDcodSimec");
        }
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



        /*      OTROS    */
        private void showaler(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        protected void btn_registrarLimpdep_Click(object sender, EventArgs e)
        {
            guardarFormulario();
            limpiarCamposDpto();
            limpiarCamposGvInsumos();
        }
    }
}
