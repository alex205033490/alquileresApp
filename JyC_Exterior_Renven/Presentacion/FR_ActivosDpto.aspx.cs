using JyC_Exterior.Negocio;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace JyC_Exterior.Presentacion
{
    public partial class FR_ActivosDpto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(19) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                MostrarActivos();
                cargarAlmacenes();
            }
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

        // dd mostrar almacenes
        private void cargarAlmacenes()
        {
            NA_ActivosDpto negocio = new NA_ActivosDpto();
            DataSet dsAlmacenes = negocio.get_listAlmacenes();

            if (dsAlmacenes != null && dsAlmacenes.Tables.Count > 0)
            {
                dd_listAlmacen.DataSource = dsAlmacenes.Tables[0];
                dd_listAlmacen.DataTextField = "nombre";
                dd_listAlmacen.DataValueField = "codigo";
                dd_listAlmacen.DataBind();

                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione un almacén", "0");
                dd_listAlmacen.Items.Insert(0, li);
            }

        }

        // GET mostrar dpto
        protected void txt_edificio_TextChanged(object sender, EventArgs e)
        {
            string nombreEdificio = txt_edificio.Text.Trim();

            NA_ActivosDpto negocio = new NA_ActivosDpto();
            DataSet tuplas = negocio.get_buscarDpto(nombreEdificio);

            if (tuplas.Tables[0].Rows.Count > 0)
            {
                gv_getDepartamentos.DataSource = tuplas;
                gv_getDepartamentos.DataBind();
                gv_getDepartamentos.Visible = true;
            }
            else
            {
                gv_getDepartamentos.DataSource = null;
                gv_getDepartamentos.DataBind();
                limpiarCamposDpto();
            }
        }

        // AUTOCOMPLETE EDIFICIO
        [WebMethod]
        [ScriptMethod]
        public static string[] getLIstaEdificio(string prefixText, int count)
        {
            string nombre = prefixText;

            NA_ActivosDpto negocio = new NA_ActivosDpto();
            DataSet tuplas = negocio.get_edificio(nombre);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for(int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
        }
        
        // Select dpto
        protected void gv_getDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_getDepartamentos.SelectedIndex;
            GridViewRow row = gv_getDepartamentos.Rows[index];

            string codDpto = row.Cells[1].Text;
            string nomEdificio = row.Cells[2].Text;
            string denominacion = row.Cells[3].Text;
            string direccion = row.Cells[4].Text;
            string ciudad = row.Cells[5].Text;
            string codSimec = row.Cells[6].Text;
            string nroInmueble = row.Cells[7].Text;
            string nroDormitorios = row.Cells[8].Text;
            
            if ((nroInmueble) == "&nbsp;")
            {
                nroInmueble = "";
            }
            if ((nroDormitorios) == "&nbsp;")
            {
                nroDormitorios = "";
            }
            if ((denominacion) == "&nbsp;")
            {
                denominacion = "";
            }
            if ((ciudad) == "&nbsp;")
            {
                ciudad = "";
            }
            if ((nroDormitorios) == "&nbsp;")
            {
                nroDormitorios = "";
            }

            Session["SADcoddpto"] = codDpto;
            txt_edificio.Text = nomEdificio;
            Session["SADnominmueble"] = nomEdificio;
            txt_Habitacion.Text = denominacion;
            txt_Direccion.Text = direccion;
            Session["SADciudad"] = ciudad;
            Session["SADcodsimec"] = codSimec;
            Session["SADnroinmueble"] = nroInmueble;
            Session["SADnrodormitorios"] = nroDormitorios;

            gv_getDepartamentos.Visible = false;


        }
        
        // Limpiar campos Dpto
        private void limpiarCamposDpto()
        {
            Session.Remove("SADnominmueble");
            Session.Remove("SADcoddpto");
            Session.Remove("SADciudad");
            Session.Remove("SADcodsimec");
            Session.Remove("SADnroinmueble");
            Session.Remove("SADnrodormitorios");

            txt_Habitacion.Text = "";
            txt_edificio.Text = "";
            txt_Direccion.Text = "";
        }

        /*************************     ACTIVOS     *********************/

        // get mostrar Activos
        private void MostrarActivos()
        {
            string nombreActivos = "";

            NA_ActivosDpto negocio = new NA_ActivosDpto();
            DataSet tuplas = negocio.get_buscarItem(nombreActivos);

            if (tuplas.Tables[0].Rows.Count > 0)
            {
                gv_listActivos.DataSource = tuplas;
                gv_listActivos.DataBind();
                gv_listActivos.Visible = true;
            }
            else
            {
                gv_listActivos.DataSource = null;
                gv_listActivos.DataBind();
            }
        }
        //Autcomplete activos
        [WebMethod]
        [ScriptMethod]
        public static string[] GetListActivos(string prefixText, int count)
        {
            string nombreActivo = prefixText;

            NA_ActivosDpto negocio = new NA_ActivosDpto();
            DataSet tuplas = negocio.get_buscarItem(nombreActivo);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for(int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
        }

        // textbox busqueda activos
        protected void txt_activo_TextChanged(object sender, EventArgs e)
        {
            string nombreActivo = txt_activo.Text.Trim();

            NA_ActivosDpto negocio = new NA_ActivosDpto();
            DataSet tuplas = negocio.get_buscarItem(nombreActivo);

            if (tuplas.Tables[0].Rows.Count > 0)
            {
                gv_listActivos.DataSource = tuplas;
                gv_listActivos.DataBind();
                gv_listActivos.Visible = true;
            }
            else
            {
                gv_listActivos.DataSource = null;
                gv_listActivos.DataBind();
            }
        }

        //****************************  boton agregar activos al GV
        // Selecciona activo del GV
        protected void gv_listActivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_listActivos.SelectedIndex;
            GridViewRow row = gv_listActivos.Rows[index];

            string codigo = row.Cells[1].Text;
            string nombre = row.Cells[2].Text;

            Session["SAcodItem"] = codigo;
            Session["SAnombre"] = nombre;
            txt_activo.Text = nombre;


        }
        List<ActivosDTO> activos = new List<ActivosDTO>();
        public class ActivosDTO
        {
            public int codigo { get; set; }
            public string nombre { get; set; }
            public int cantidad { get; set; }
        }
        private List<ActivosDTO> obtenerActivosDeSession()
        {
            List<ActivosDTO> activos = Session["SactivosAdd"] as List<ActivosDTO>;
            if (activos == null)
            {
                activos = new List<ActivosDTO>();
            }
            return activos;
        }
        private ActivosDTO CrearNuevoActivo()
        {
            try
            {
                int codigo = int.Parse(Session["SAcodItem"].ToString());
                string nombre = (Session["SAnombre"].ToString());
                if (string.IsNullOrWhiteSpace(Session["SAcodItem"].ToString()) || string.IsNullOrWhiteSpace(Session["SAnombre"].ToString()))
                {
                    showaler("Debe buscar y seleccionar un activo válido.");
                    return null;
                }

                int cantidad = 0;
                if(!int.TryParse(txt_cantidadActivo.Text, out cantidad) || cantidad <= 0)
                {
                    showaler("La cantidad debe ser un número válido >= que cero.");
                    return null;
                }

                return new ActivosDTO
                {
                    codigo = codigo,
                    nombre = nombre,
                    cantidad = cantidad
                };
            }
            catch (Exception ex)
            {
                showaler($"Error busque y seleccione un activo válido: {ex.Message}");
                return null;
            }
        }
        private void actualizarGV(List<ActivosDTO> activos)
        {
            gv_activos.DataSource = activos;
            gv_activos.DataBind();
        }


        // BTN ADD al gridview gv_activos
        protected void btn_addActivo_Click(object sender, EventArgs e)
        {
            try
            {
                List<ActivosDTO> activos = obtenerActivosDeSession();
                ActivosDTO nuevoActivo = CrearNuevoActivo();

                if(nuevoActivo == null)
                {
                    return;
                }
                activos.Add(nuevoActivo);

                Session["SactivosAdd"] = activos;

                actualizarGV(activos);

                limpiarCamposActivo();

                MostrarActivos();
            }
            catch(Exception ex)
            {
                showaler("Ocurrio un error al agregar el activo " + ex.Message);
            }
        }
        private void limpiarCamposActivo()
        {
            txt_activo.Text = string.Empty;
            txt_cantidadActivo.Text = string.Empty;

            Session.Remove("SAcodItem");
            Session.Remove("SAnombre");

        }


        /***************    GUARDAR REGISTROS    ***************/
        // BTN registrar formulario
        protected void btn_registrarForm_Click(object sender, EventArgs e)
        {
            guardarRegistro();
        }
   
        private bool RegistrarReciboIngresoActivosDpto((int coddpto, string codsimec, string nominmueble, string nroinmueble, int nrohabitaciones, string direccioninmueble, string dptoinmueble, int codres, string denominacion) dReciboIngreso)
        {
            try
            {
                NA_ActivosDpto negocio = new NA_ActivosDpto();
                return negocio.post_reciboIngresoActivo(dReciboIngreso.coddpto, dReciboIngreso.codsimec, dReciboIngreso.nominmueble, dReciboIngreso.nroinmueble, dReciboIngreso.nrohabitaciones, dReciboIngreso.direccioninmueble, dReciboIngreso.dptoinmueble, dReciboIngreso.codres, dReciboIngreso.denominacion);
            }
            catch(Exception ex)
            {
                showaler($"Error al registrar el recibo ingreso de activos: {ex.Message}");
                return false;
            }
        }

        private bool RegistrarDetReciboIngresoActivoDpto((List<ActivosDTO> listActivos, int codRecibo, int codRes, int codAlmacen) dDetRecibo)
        {
            try
            {
                NA_ActivosDpto negocio = new NA_ActivosDpto();
                foreach(var activos in dDetRecibo.listActivos)
                {
                    bool resultado = negocio.post_detalleReciboIngresoActivoDpto(dDetRecibo.codRecibo, activos.codigo, activos.cantidad, dDetRecibo.codRes, dDetRecibo.codAlmacen);
                    if (!resultado)
                    {
                        showaler($"Error al insertar el activo con el codigo : {activos.codigo}");
                        return false;
                    }
                }
                return true;

            } catch (Exception ex)
            {
                showaler($"Error al registrar el Detalle del recibo ingreso de activos: {ex.Message}");
                return false;
            }
        }

        private bool RegistrarActivosDpto((List<ActivosDTO> listActivos, int codDpto, int codRes)dActivosDpto)
        {
            try
            {
                NA_ActivosDpto negocio = new NA_ActivosDpto();
                foreach(var activos in dActivosDpto.listActivos)
                {
                    bool resultado = negocio.insertar_activosDpto(dActivosDpto.codDpto, activos.codigo, activos.cantidad, dActivosDpto.codRes);
                    if (!resultado)
                    {
                        showaler($"Error al insertar el activo con el codigo:{activos.codigo}");
                        return false;
                    }

                }
                return true;
            } catch(Exception ex)
            {
                showaler($"Error al registrar los activos al departamento. {ex.Message}");
                return false;
            }
        }


        private void guardarRegistro()
        {
            try
            {
                validarDatos();

                int codDpto = int.Parse(Session["SADcoddpto"].ToString());
                string codSimec = Session["SADcodsimec"].ToString();
                string nomInmueble = Session["SADnominmueble"].ToString();
                string nroInmueble = Session["SADnroinmueble"].ToString();
                int nroDormitorios = int.Parse(Session["SADnrodormitorios"].ToString());
                string direccion = txt_Direccion.Text;
                string ciudad = Session["SADciudad"].ToString();
                string habitacion = txt_Habitacion.Text;
                int dd_almacen = int.Parse(dd_listAlmacen.SelectedValue);
                List<ActivosDTO> listaActivos = obtenerListActivos();
                int codResponsable = ObtenerCodigoResponsable();

                if (InsertarActivosADpto(listaActivos, codDpto, codResponsable))
                {
                    bool insertReciboIngresoActivoDpto = Insertar_ReciboIngresoActivoDpto(codDpto, codSimec, nomInmueble, nroInmueble, nroDormitorios, direccion, ciudad, codResponsable, habitacion);
                    if (insertReciboIngresoActivoDpto)
                    {
                        int ultimoReciboIngreso = ObtenerUltimoReciboIngresoActivo(codResponsable);

                        bool insertarDetalleReciboIngresoActivo = Insertar_detalleReciboIngresoActivoDpto(listaActivos, ultimoReciboIngreso, codResponsable, dd_almacen);

                        if (!insertarDetalleReciboIngresoActivo)
                        {
                            showaler($"Error al insertar el detalle del recibo Nro: {ultimoReciboIngreso}");
                            return;
                        }
                    }
                    else
                    {
                        showaler("Error al insertar en la tabla reciboIngresoActivoDpto.");
                        return;
                    }
                    showaler("El formulario se ha registrado correctamente.");
                    limpiarFormularioRegistro();
                    limpiarCamposActivo();
                }
                else
                {
                    showaler("Hubo un problema al registrar los activos");
                }
            }
            catch (Exception ex)
            {
                showaler($"Error al registrar. Datos incorrectos o incompletos: {ex.Message}");
                return ;
            }
        }
        // Validaciones
        private void validarDatos()
        {
            string nomInmueble = Session["SADnominmueble"].ToString();
            int codDpto = int.Parse(Session["SADcoddpto"].ToString());
            string edificio = txt_edificio.Text;
            int dd_almacen = int.Parse(dd_listAlmacen.SelectedValue);
            List<ActivosDTO> listaActivos = obtenerListActivos();
            int codResponsable = ObtenerCodigoResponsable();

            if (string.IsNullOrEmpty(edificio) || nomInmueble == null || string.IsNullOrEmpty(nomInmueble))
            {
                showaler("busque y seleccione un edificio valido.");
                throw new Exception("Edificio no válido");
            }
            if(listaActivos == null || listaActivos.Count == 0)
            {
                showaler("Error: Agrega al menos 1 activo a su lista.");
                throw new Exception("Lista de activos vacía");
            }
            if (codResponsable == 0)
            {
                showaler("Error: No se pudo obtener el código del Responsable.");
                throw new Exception("Código de responsable no encontrado");
            }
            if (dd_almacen <= 0)
            {
                showaler("Seleccione un almacén válido.");
                throw new Exception("Almacén no válido");
            }
        }

        /* funcion insertar recibo */
        private bool Insertar_ReciboIngresoActivoDpto(int coddpto, string codSimec, string nomInmueble, string nroInmueble, int nroHabitaciones, string direccionInmueble, string dptoInmueble, int codres, string denominacion)
        {
            try
            {
                NA_ActivosDpto negocio = new NA_ActivosDpto();
                bool resultado = negocio.post_reciboIngresoActivo(coddpto, codSimec, nomInmueble, nroInmueble, nroHabitaciones, direccionInmueble, dptoInmueble, codres, denominacion);
                if (!resultado)
                {
                    showaler($"Error al crear el recibo del ingreso del departamento : {coddpto}");
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                showaler($"Error al insertar datos a reciboIngresoActivos: {ex.Message}");
                return false;
            }
        }
        
        /* funcion insertar detalles recibo */
        private bool Insertar_detalleReciboIngresoActivoDpto(List<ActivosDTO> listActivos, int codRecibo, int codRes , int codAlmacen )
        {
            try
            {
                NA_ActivosDpto negocio = new NA_ActivosDpto();
                foreach(var activo in listActivos)
                {
                    bool resultado = negocio.post_detalleReciboIngresoActivoDpto(codRecibo, activo.codigo, activo.cantidad, codRes, codAlmacen);
                    if (!resultado)
                    {
                        showaler($"Error al insertar el activo con el codigo : {activo.codigo}");
                        return false;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                showaler($"Error al insertar datos a DetalleReciboActivos: {ex.Message}");
                return false;
            }
        }
        
        /* Obtener ultimo recibo de ingreso */
        private int ObtenerUltimoReciboIngresoActivo(int codRes)
        {
            try
            {
                NA_ActivosDpto negocio = new NA_ActivosDpto();
                return negocio.get_ultimoRegistroReciboIngresoActivo(codRes);
            }
            catch(Exception ex)
            {
                showaler($"Error al obtener el ultimo registro del recibo insertado. {ex.Message}");
                return 0;
            }
        }
       
        /* funcion insertar activos dpto */
        private bool InsertarActivosADpto(List<ActivosDTO> listActivos, int codDpto, int codRes)
        {
            try
            {
                NA_ActivosDpto negocio = new NA_ActivosDpto();
                foreach(var activo in listActivos)
                {
                    bool resultado = negocio.insertar_activosDpto(codDpto, activo.codigo, activo.cantidad, codRes);
                    if (!resultado)
                    {
                        showaler($"Error al insertar el activo con codigoo : {activo.codigo}");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex){
                showaler($"Error al insertar activos en el formulario: {ex.Message}");
                return false;
            }
        }

        /* OBTENER lista de activos */
        private List<ActivosDTO> obtenerListActivos()
        {
            return Session["SactivosAdd"] as List<ActivosDTO>;
        }

        /* Metodo obtener codigo */
        private int ObtenerCodigoResponsable()
        {
            try
            {
                NA_Responsables negocio = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                return negocio.getCodUsuario(usuarioAux, passwordAux);
            } 
            catch(Exception ex)
            {
                showaler($"Error al obtener código del responsable: {ex.Message}");
                return 0;
            }
        }

        /* METODO limpiar formulario */
        private void limpiarFormularioRegistro()
        {
            txt_edificio.Text = string.Empty;
            Session.Remove("SADnominmueble");
            Session.Remove("SADcoddpto");
            Session.Remove("SADciudad");
            Session.Remove("SADcodsimec");
            Session.Remove("SADnroinmueble");
            Session.Remove("SADnrodormitorios");

            gv_getDepartamentos.DataSource = null;
            gv_getDepartamentos.DataBind();

            txt_Habitacion.Text = string.Empty;

            txt_Direccion.Text = string.Empty;

            gv_activos.DataSource = null;
            gv_activos.DataBind();
            Session.Remove("SactivosAdd");

            dd_listAlmacen.SelectedValue = "0";
        }

        /*      OTROS    */
        private void showaler(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        // BTN delete activo del GV
        protected void gv_activosDelete(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int codigoActivo = int.Parse(e.CommandArgument.ToString());

                List<ActivosDTO> activos = Session["SactivosAdd"] as List<ActivosDTO>;

                var productoAEliminar = activos.FirstOrDefault(p => p.codigo == codigoActivo);

                if (productoAEliminar != null)
                {
                    activos.Remove(productoAEliminar);
                }
                Session["SactivosAdd"] = activos;

                gv_activos.DataSource = activos;
                gv_activos.DataBind();
            }
        }

        // BTN limpiar campos
        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            limpiarCamposActivo();
            limpiarFormularioRegistro();
            MostrarActivos();
        }

        protected void btn_volverAtras_Click(object sender, EventArgs e)
        {
            limpiarCamposActivo();
            limpiarFormularioRegistro();
            limpiarCamposDpto();
            Response.Redirect("FA_MenuPorArea.aspx");
        }
    }
}

