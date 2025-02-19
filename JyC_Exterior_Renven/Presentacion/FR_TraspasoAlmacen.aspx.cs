using JyC_Exterior.Negocio;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JyC_Exterior.Presentacion
{
    public partial class FR_TraspasoAlmacen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(20) == false)
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
                dd_almacenOrigen.DataSource = dsAlmacenes.Tables[0];
                dd_almacenOrigen.DataTextField = "nombre";
                dd_almacenOrigen.DataValueField = "codigo";
                dd_almacenDestino.DataSource = dsAlmacenes.Tables[0];
                dd_almacenDestino.DataTextField = "nombre";
                dd_almacenDestino.DataValueField = "codigo";

                dd_almacenOrigen.DataBind();
                dd_almacenDestino.DataBind();

                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione un almacén", "0");
                dd_almacenOrigen.Items.Insert(0, li);
                dd_almacenDestino.Items.Insert(0, li);
            }
        }

        // obtener valores del almacen OD
        
        protected void dd_almacenO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codigoSeleccionado = dd_almacenOrigen.SelectedValue;

            NA_ActivosDpto negocio = new NA_ActivosDpto();
            DataSet dsAlmacenes = negocio.get_listAlmacenes();

            DataRow almacenSeleccionado = dsAlmacenes.Tables[0]
                .AsEnumerable()
                .FirstOrDefault(row => row["codigo"].ToString() == codigoSeleccionado);

            if (almacenSeleccionado != null)
            {
                Session["SalmacenOrigen"] = new almacenDTO
                {
                    codigo = int.Parse(almacenSeleccionado["codigo"].ToString()),
                    codSimec = almacenSeleccionado["codSimec"].ToString(),
                    nombre = almacenSeleccionado["nombre"].ToString()
                };
            }
        }
        
        protected void dd_almacenD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codigoSeleccionado = dd_almacenDestino.SelectedValue;

            NA_ActivosDpto negocio = new NA_ActivosDpto();
            DataSet dsAlmacenes = negocio.get_listAlmacenes();

            DataRow almacenSeleccionado = dsAlmacenes.Tables[0]
                .AsEnumerable()
                .FirstOrDefault(row => row["codigo"].ToString() == codigoSeleccionado);

            if (almacenSeleccionado != null)
            {
                Session["SalmacenDestino"] = new almacenDTO
                {
                    codigo = int.Parse(almacenSeleccionado["codigo"].ToString()),
                    codSimec = almacenSeleccionado["codSimec"].ToString(),
                    nombre = almacenSeleccionado["nombre"].ToString()
                };
            }
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

            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++)
            {
                string codigo = tuplas.Tables[0].Rows[i]["codigo"].ToString();
                string item = tuplas.Tables[0].Rows[i]["nombre"].ToString();
                lista[i] = $"{codigo}|{item}";
            }
            return lista;
        }

        
        /*public static string[] GetListActivos(string prefixText, int count)
        {
            string nombreActivo = prefixText;

            NA_ActivosDpto negocio = new NA_ActivosDpto();
            DataSet tuplas = negocio.get_buscarItem(nombreActivo);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
        }*/

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

            txt_codActivo.Text = codigo;
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
            List<ActivosDTO> activos = Session["SactivosAddTraspaso"] as List<ActivosDTO>;
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
                int codigo = int.Parse(txt_codActivo.Text.Trim());
                string nombre = (txt_activo.Text);
                if (string.IsNullOrWhiteSpace(txt_codActivo.Text) || string.IsNullOrWhiteSpace(txt_activo.Text))
                {
                    showaler("Seleciona un activo válido.");
                    return null;
                }

                int cantidad = 0;
                if (!int.TryParse(txt_cantidadActivo.Text, out cantidad) || cantidad <= 0)
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
                showaler($"Error al insertar el activo: {ex.Message}");
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

                if (nuevoActivo == null)
                {
                    return;
                }
                activos.Add(nuevoActivo);

                Session["SactivosAddTraspaso"] = activos;

                actualizarGV(activos);

                limpiarCamposActivo();

                MostrarActivos();
            }
            catch (Exception ex)
            {
                showaler("Ocurrio un error al agregar el activo " + ex.Message);
            }
        }
        private void limpiarCamposActivo()
        {
            txt_codActivo.Text = string.Empty;
            txt_activo.Text = string.Empty;
            txt_cantidadActivo.Text = string.Empty;
        }

        private List<ActivosDTO> obtenerListActivos()
        {
            return Session["SactivosAddTraspaso"] as List<ActivosDTO>;
        }
        /* Metodo obtener codigo */


        /*      OTROS    */
        private void showaler(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        private void validarDatosForm()
        {
            int dd_almacenO = int.Parse(dd_almacenOrigen.SelectedValue);
            int dd_almacenD = int.Parse(dd_almacenDestino.SelectedValue);

            List<ActivosDTO> listaActivos = obtenerListActivos();

            if (dd_almacenO <= 0)
            {
                showaler("Seleccione un almacén origen válido.");
                throw new Exception("Almacén no válido");
            }
            if (dd_almacenD <= 0)
            {
                showaler("Seleccione un almacén destino válido.");
                throw new Exception("Almacén no válido");
            }
            if (dd_almacenDestino.SelectedIndex == dd_almacenOrigen.SelectedIndex)
            {
                showaler("El almacén de origen y destino deben ser diferentes.");
                throw new Exception("Almacén origen y destino deben ser diferentes");
            }


            if (listaActivos == null || listaActivos.Count == 0)
            {
                showaler("Error: Agrega al menos 1 activo a su lista.");
                throw new Exception("Lista de activos vacía");
            }
        }

        // BTN delete activo del GV
        protected void gv_activosDelete(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int codigoActivo = int.Parse(e.CommandArgument.ToString());

                List<ActivosDTO> activos = Session["SactivosAddTraspaso"] as List<ActivosDTO>;

                var productoAEliminar = activos.FirstOrDefault(p => p.codigo == codigoActivo);

                if (productoAEliminar != null)
                {
                    activos.Remove(productoAEliminar);
                }
                Session["SactivosAddTraspaso"] = activos;

                gv_activos.DataSource = activos;
                gv_activos.DataBind();
            }
        }





        // BTNs
        protected void btn_volverAtras_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Response.Redirect("FA_MenuPorArea.aspx");
        }

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
        private void LimpiarFormulario()
        {
            dd_almacenOrigen.SelectedIndex = 0;
            dd_almacenDestino.SelectedIndex = 0;

            gv_activos.DataSource = null;
            gv_activos.DataBind();

            Session.Remove("SactivosAddTraspaso");
            limpiarCamposActivo();
        }

        // REGISTRAR FORMULARIO TRASPASO
        // BTN registrar
        public class almacenDTO
        {
            public int codigo { get; set; }
            public string codSimec { get; set; }
            public string nombre { get; set; }
        }
        protected void btn_registrarForm_Click(object sender, EventArgs e)
        {
            guardarRegistro();
        }

        private void guardarRegistro()
        {
            try
            {
                validarDatosForm();

                var almacenOrigen = Session["SalmacenOrigen"] as almacenDTO;
                if (almacenOrigen == null)
                {
                    showaler("No se ha seleccionado un almacén de origen.");
                    return;
                }
                int cod = almacenOrigen.codigo;
                string simec = almacenOrigen.codSimec;
                string nombre = almacenOrigen.nombre;
                
                var almacenDestino = Session["SalmacenDestino"] as almacenDTO;
                if (almacenDestino == null)
                {
                    showaler($"No se ha seleccionado un almacén de destino.");
                    return;
                }
                int codD = almacenDestino.codigo;
                string simecD = almacenDestino.codSimec;
                string nombreD = almacenDestino.nombre;

                int codResp = ObtenerCodResponsable();

                //lista activos
                List<ActivosDTO> listaActivos = obtenerListActivos();


                bool insertarReciboTraspasoAlm = insertar_ReciboTraspasoAlm(cod, simec, nombre, codD, simecD, nombreD, codResp);
                if (insertarReciboTraspasoAlm)
                {
                    int ultimoRegistroTraspaso = ObtenerUltimoReciboTraspasoActivo(codResp);

                    bool insertarDetReciboTraspasoAlm = insertar_DetReciboTraspasoAlm(listaActivos, ultimoRegistroTraspaso, codResp, codD);

                    if (insertarDetReciboTraspasoAlm)
                    {
                        showaler($"El traspaso se ha registrado exitosamente.");
                        LimpiarFormulario();
                        return;
                    }
                    else
                    {
                        showaler("Ha ocurrido un error al registrar detalles del recibo.");
                        return;
                    }

                }
                else
                {
                    showaler("Error al insertar en la tabla Recibo de traspaso");
                    return;
                }
            } catch (Exception ex)
            {
                showaler($"Error al registrar. Datos incorrectos o incompletos: {ex.Message}");
                return;
            }
        }


        // funcion insertarRecibo
        private bool insertar_ReciboTraspasoAlm(int codalm_origen, string codSimec_origen, string Almacen_origen, int codalm_destino, string codSimec_destino, string Almacen_destino, int codRes)
        {
            try
            {
                NA_RenvenTraspasoAlmacen negocio = new NA_RenvenTraspasoAlmacen();
                bool resultado = negocio.POST_TraspasoAlmacen(codalm_origen, codSimec_origen, Almacen_origen, codalm_destino, codSimec_destino, Almacen_destino, codRes);

                if (!resultado)
                {
                    showaler($"Error al crear el recibo del traspasooo: {codalm_origen}, {codSimec_origen}, {codalm_origen},{codalm_destino}, {codSimec_destino}, {codalm_destino}, {codRes}");
                    return false;
                }
                return true;

            } catch (Exception ex)
            {
                showaler($"Error al insertar datos al Recibo de Traspaso: {ex.Message}");
                return false;
            }
        }
        private bool insertar_DetReciboTraspasoAlm(List<ActivosDTO> listActivos, int codRecibo, int codRes, int codAlmacen)
        {
            try
            {
                NA_RenvenTraspasoAlmacen negocio = new NA_RenvenTraspasoAlmacen();
                foreach(var activo in listActivos)
                {
                    bool resultado = negocio.POST_DetalleTraspasoAlmacen(codRecibo, activo.codigo, activo.cantidad, codRes, codAlmacen);
                    if (!resultado)
                    {
                        showaler($"Error al insertar el activo con el codigo ; {activo.codigo}");
                        return false;
                    }
                }
                return true;
            }catch (Exception ex)
            {
                showaler($"Error al insertar datos a DetalleReciboTraspasoAlm. {ex.Message}");
                return false;
            }

        }
        private int ObtenerUltimoReciboTraspasoActivo(int codRes)
        {
            try
            {
                NA_RenvenTraspasoAlmacen negocio = new NA_RenvenTraspasoAlmacen();
                return negocio.get_ultimoRegistroTraspaso(codRes);
            } catch (Exception ex)
            {
                showaler($"error al obtener el ultimo registro del trapaso de activos. {ex.Message}");
                return 0;
            }
        }

        //METODO OBTENER COD RESPONSABLE
        private int ObtenerCodResponsable()
        {
            try
            {
                NA_Responsables negocio = new NA_Responsables();
                string usu = Session["NameUser"].ToString();
                string pass = Session["passworuser"].ToString();
                return negocio.getCodUsuario(usu, pass);
            } catch (Exception ex)
            {
                showaler($"Error al obtener el codigo del responsable. {ex.Message}");
                return 0;
            }

        }
    }
}