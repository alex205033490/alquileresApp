using JyC_Exterior.Negocio;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Documents;

namespace JyC_Exterior.Presentacion
{
    public partial class FR_ActivosDpto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarActivos();
                cargarAlmacenes();
            }
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
        // Select dpto
        protected void gv_getDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_getDepartamentos.SelectedIndex;
            GridViewRow row = gv_getDepartamentos.Rows[index];

            string codDpto = row.Cells[1].Text;
            string edificio = row.Cells[2].Text;
            string direccion = row.Cells[3].Text;

            txt_codDepartamento.Text = codDpto;
            txt_edificio.Text = edificio;
            txt_Direccion.Text = direccion;

            gv_getDepartamentos.Visible = false;

        }
        // Limpiar campos Dpto
        private void limpiarCamposDpto()
        {
            txt_codDepartamento.Text = "";
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
                showaler($"Error Busque y seleccion un activo válido: {ex.Message}");
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
        private void guardarRegistro()
        {
            try 
            {
                
                if (string.IsNullOrWhiteSpace(txt_codDepartamento.Text))
                {
                    showaler("Seleccione un departamento válido");
                    return;
                }

                int dd_almacen = int.Parse(dd_listAlmacen.SelectedValue);
                if (dd_almacen <= 0)
                {
                    showaler("Seleccione un almacén válido");
                }

                List<ActivosDTO> listaActivos = obtenerListActivos();
                if (listaActivos == null || listaActivos.Count == 0)
                {
                    showaler("Error : agrega al menos 1 activo a su lista.");
                    return;
                }

                int codResponsable = ObtenerCodigoResponsable();
                if(codResponsable == 0)
                {
                    showaler("Error: No se pudo obtener el código del solicitante.");
                    return;
                }

                int codDpto = int.Parse(txt_codDepartamento.Text.Trim());

                if (InsertarActivosADpto(listaActivos, codDpto, codResponsable))
                {


                    bool insertDetAlmacen = InsertarDetalleAlmacen(listaActivos, dd_almacen, codResponsable);
                    if (!insertDetAlmacen)
                    {
                        showaler("error al insertar datos en detalle almacen");
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
            catch(Exception ex)
            {
                showaler($"Error: {ex.Message}");
            }
        }
        private bool InsertarDetalleAlmacen(List<ActivosDTO> listActivos, int codAlmacen, int codres)
        {
            try
            {
                NA_ActivosDpto negocio = new NA_ActivosDpto();
                foreach(var activo in listActivos)
                {
                    bool resultado = negocio.insertar_detalleAlmacen(codAlmacen, activo.codigo, activo.cantidad, codres);

                    if (!resultado)
                    {
                        showaler($"Error da al insertar el activo con el codigo : {activo.codigo}");
                        return false;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                showaler($"Error al insertar datos a DetalleAlmacen: {ex.Message}");
                return false;
            }

        }
       
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
                        showaler($"Error al insertar el activo con codigo : {activo.codigo}");
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


        private List<ActivosDTO> obtenerListActivos()
        {
            return Session["SactivosAdd"] as List<ActivosDTO>;
        }
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
        // BTN registrar formulario
        protected void btn_registrarForm_Click(object sender, EventArgs e)
        {
            guardarRegistro();
        }

        private void limpiarFormularioRegistro()
        {
            txt_edificio.Text = string.Empty;

            gv_getDepartamentos.DataSource = null;
            gv_getDepartamentos.DataBind();

            txt_codDepartamento.Text = string.Empty;

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

        // BTN Eliminar del GV
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

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            limpiarCamposActivo();
            limpiarFormularioRegistro();
            MostrarActivos();
        }
    }
}

