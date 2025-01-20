using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using JyC_Exterior.Negocio;
using jycboliviaASP.net.Negocio;


namespace JyC_Exterior.Presentacion
{
    public partial class FCorpal_SolicitudPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)            
            {
                List<Productos> datoListaProductos = new List<Productos>();
                gv_listaProductosPedido.DataSource = datoListaProductos;
                gv_listaProductosPedido.DataSource = datoListaProductos;
                gv_listaProductosPedido.DataBind();
                Session["Productos"] = datoListaProductos;
            }
           
            NA_Responsables Nresp = new NA_Responsables ();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();

            NA_SolicitudPedido Nsol = new NA_SolicitudPedido ();

            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            txt_Nro.Text = Nsol.get_siguentenumeroRecibo(codUser);
            txt_Solicitante.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
        }

        // *************************** PRODUCTOS
        protected void txt_nomProducto_TextChanged(object sender, EventArgs e)
        {
            string nombreProducto = txt_nomProducto.Text.Trim();

            NA_SolicitudPedido sp = new NA_SolicitudPedido();
            DataSet tuplas = sp.get_mostrarProductos(nombreProducto);

            if (tuplas.Tables[0].Rows.Count > 0)
            {
                gv_productosPedidos.DataSource = tuplas;
                gv_productosPedidos.DataBind();
                gv_productosPedidos.Visible = true;
            }
            else
            {
                gv_productosPedidos.DataSource = null;
                gv_productosPedidos.DataBind();
            }

        }
        private void CargarProductos(string criterio)
        {
            try
            {
                NA_SolicitudPedido sp = new NA_SolicitudPedido();
                DataSet tuplas = sp.get_mostrarProductos(criterio);
                
                gv_productosPedidos.DataSource = tuplas;
                gv_productosPedidos.DataBind();

                gv_productosPedidos.Visible = productos.Count > 0;
            }
            catch(Exception ex)
            {
                showalert($"Error al realizar la busqueda: {ex.Message}");
            }
        }
        protected void gv_productosPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_productosPedidos.SelectedIndex;
            GridViewRow row = gv_productosPedidos.Rows[index];

            string codigo = (row.Cells[1].Text);
            string producto = row.Cells[2].Text;
            string medida = row.Cells[3].Text;
            string precio = (row.Cells[4].Text);
            string stockAlmacen = (row.Cells[5].Text);

            Session["ScodigoProducto"] = codigo;
            txt_nomProducto.Text = producto;
            Session["SmedidaProducto"] = medida;
            Session["SprecioProducto"] = precio;
            Session["SstockAlmacen"] = stockAlmacen;

            gv_productosPedidos.Visible = false;
        }


        //******************************  CLIENTES
        protected void txt_cliente_TextChanged1(object sender, EventArgs e)
        {
            string nombreCliente = txt_cliente.Text.Trim();

            NA_SolicitudPedido sp = new NA_SolicitudPedido();
            DataSet tuplas = sp.get_mostrarClientes(nombreCliente);

            if (tuplas.Tables[0].Rows.Count > 0)
            {
                gv_clientesPedido.DataSource = tuplas;
                gv_clientesPedido.DataBind();
                gv_clientesPedido.Visible = true;
            }
            else
            {
                gv_clientesPedido.DataSource = null;
                gv_clientesPedido.DataBind();
            }
        }
        protected void gv_clientesPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_clientesPedido.SelectedIndex;
            GridViewRow row = gv_clientesPedido.Rows[index];

            string codigo = row.Cells[1].Text;
            string tiendaName = row.Cells[2].Text;

            txt_cliente.Text = tiendaName;
            Session["ScodigoCliente"] = codigo;

            gv_clientesPedido.Visible = false;
        }



        //********************************** BOTON AGREGAR PRODUCTO AL GV
        List<Productos> productos = new List<Productos>();
        public class Productos
        {
            public int codigo {  get; set; }
            public string producto {  get; set; }
            public float precio {  get; set; }
            public float cantidad { get; set; }
            public string tipoSolicitud { get; set; }
            public string medida {  get; set; }
            public float precioTotal{  get; set; }
        }
        private List<Productos> ObtenerProductosDesdeSession()
        {
            List<Productos> productos = Session["Productos"] as List<Productos>;
            if (productos == null)
            {
                productos = new List<Productos>();
            }
            return productos;
        }
        private Productos CrearNuevoProducto()
        {
            try
            {
                string producto = txt_nomProducto.Text.Trim();
                if (string.IsNullOrEmpty(producto))
                {
                    showalert("Debe buscar y seleccionar el nombre del producto.");
                    return null;
                }

                if (Session["ScodigoProducto"] == null || string.IsNullOrWhiteSpace(Session["ScodigoProducto"].ToString()))
                {
                    showalert("El código del producto no está disponible en la sesión");
                    return null;
                }
                int codigo = int.Parse(Session["ScodigoProducto"].ToString());

                if (Session["SmedidaProducto"] == null || string.IsNullOrWhiteSpace(Session["SmedidaProducto"].ToString()))
                {
                    showalert("La medida del producto no está disponible en la sesión.");
                    return null;
                }
                string medida = Session["SmedidaProducto"].ToString();

                if (Session["SprecioProducto"] == null || string.IsNullOrWhiteSpace(Session["SprecioProducto"].ToString()))
                {
                    showalert("El precio del producto no está disponible en la sesión");
                    return null;
                }
                float precio = float.Parse(Session["SprecioProducto"].ToString());

                float cantidad = 0;
                if (!float.TryParse(txt_cantidadProducto.Text, out cantidad) || cantidad <= 0)
                {
                    showalert("La cantidad del producto debe ser un número válido mayor que cero.");
                    return null;
                }

                float precioTotal = precio * cantidad;

                string tipoSolicitud = dd_tipoSolicitud.SelectedValue;
                if (string.IsNullOrEmpty(tipoSolicitud))
                {
                    showalert("Debe seleccionar un tipo de solicitud.");
                    return null;
                }

                return new Productos
                {
                    codigo = codigo,
                    producto = producto,
                    medida = medida,
                    precio = precio,
                    cantidad = cantidad,
                    tipoSolicitud = tipoSolicitud,
                    precioTotal = precioTotal
                };
            }
            catch (Exception ex)
            {
                showalert($"Error al crear el producto: " + ex.Message);
                return null;
            }
        }
        private void ActualizarGridView(List<Productos> productos)
        {
            gv_listaProductosPedido.DataSource = productos;
            gv_listaProductosPedido.DataBind();
        }
        protected void btn_addProducto_Click(object sender, EventArgs e)
        {
            try
            {
                List<Productos> productos = ObtenerProductosDesdeSession();

                Productos nuevoProducto = CrearNuevoProducto();

                if (nuevoProducto == null)
                {
                    return;
                }

                productos.Add(nuevoProducto);
            
                Session["Productos"] = productos;

                ActualizarGridView(productos);

                LimpiaCamposProducto();
            }
            catch (Exception ex)
            {
                showalert("Ocurrio un error al agregar el producto: " + ex.Message);
            }
        }
        private void LimpiaCamposProducto()
        {
            txt_nomProducto.Text = string.Empty;
            txt_cantidadProducto.Text = string.Empty;
            Session.Remove("ScodigoProducto");
            Session.Remove("SmedidaProducto");
            Session.Remove("SprecioProducto");
        }


        //**********************************    BOTON REGISTRAR PEDIDO
        private void guardarSolicitud()
        {
            try
            {
                
                if (string.IsNullOrEmpty(txt_Nro.Text.Trim()))
                {
                    showalert("Ingrese un Nro de boleta válida.");
                    return;
                }
                if (string.IsNullOrEmpty(txt_fechaEntrega.Text.Trim()))
                {
                    showalert("Ingrese una fecha de entrega válida.");
                    return;
                }
                if (string.IsNullOrEmpty(txt_horaEntrega.Text.Trim()))
                {
                    showalert("Ingrese una hora de entrega válida.");
                    return;
                }
                

                List<Productos> listaProductos = ObtenerListaProductos();
                if (listaProductos == null || listaProductos.Count == 0)
                {
                    showalert("Error: no tiene productos en el pedido");
                    return;
                }

                int codPerSolicitante = ObtenerCodigoSolicitante();
                if (codPerSolicitante == 0)
                {
                    showalert("Error: No se pudo obtener el código del solicitante.");
                    return;
                }

                string nroBoleta = txt_Nro.Text.Trim();
                string personalSolicitud = txt_Solicitante.Text.Trim();
                string fechaEntrega = convertidorFecha(txt_fechaEntrega.Text).Trim();
                string horaEntrega = txt_horaEntrega.Text.Trim();
                int codCliente = ObtenerCodigoCliente();

                if (RegistrarSolicitud(nroBoleta, fechaEntrega, horaEntrega, personalSolicitud, codPerSolicitante, codCliente))
                {
                    int ultimaSolicitudId = ObtenerUltimaSolicitudInsertada(codPerSolicitante);
                    double montoTotal = InsertarProductosDeSolicitud(listaProductos, ultimaSolicitudId);
                    showalert("El pedido se ha registrado correctamente.");
                    limpiarCamposPedido();

                    if (montoTotal >= 0)
                    {
                        ActualizarMontoTotal(ultimaSolicitudId, montoTotal);
                        Session["codigoSolicitudProducto"] = ultimaSolicitudId;
                        //RecargarPagina();
                    }
                    else
                    {   
                        showalert("Error: no se pudieron insertar Producto.");
                    }

                }
                else
                {

                    showalert("Error: No se pudo realizar la solicitud");
                }
            }
            catch (Exception ex)
            {
                showalert($"Error inesperado al guardar la solicitud: {ex.Message}");
            }
        }
       
        protected void btn_POSTPedido_Click(object sender, EventArgs e)
        {
            guardarSolicitud();
        }

        private List<Productos> ObtenerListaProductos()
        {
            return Session["Productos"] as List<Productos>;
        }
        private int ObtenerCodigoSolicitante()
        {
            try
            {
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                return Nresp.getCodUsuario(usuarioAux, passwordAux);

            } catch (Exception ex)
            {
                showalert($"Error al obtener código del solicitante: {ex.Message}");
                return 0;
            }
        }
        private int ObtenerCodigoCliente()
        {
            return int.TryParse(Session["ScodigoCliente"].ToString(), out int codCliente) ? codCliente : 0;
        }
        private bool RegistrarSolicitud(string nroBoleta, string fechaEntrega, string horaEntrega, string personalSolicitud, int codPerSolicitante, int codCliente)
        {
            try
            {
                NA_SolicitudPedido nsp = new NA_SolicitudPedido();
                return nsp.set_guardarSolicitud(nroBoleta, fechaEntrega, horaEntrega, personalSolicitud, codPerSolicitante, true, codCliente);
            } catch (Exception ex)
            {
                showalert($"Error al registrar solicitud: {ex.Message}");
                return false;
            }
        }
        private int ObtenerUltimaSolicitudInsertada(int codPerSolicitante)
        {
            try
            {
                NA_SolicitudPedido nsp = new NA_SolicitudPedido();
                return nsp.getultimaSolicitudproductoInsertado(codPerSolicitante);
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener última solicitud insertada: {ex.Message}");
                return 0;
            }
        }
        private double InsertarProductosDeSolicitud(List<Productos> listaProductos, int solicitudId)
        {
            double montoTotal = 0;
            try
            {
                NA_SolicitudPedido nsp = new NA_SolicitudPedido();
                foreach(var producto in listaProductos)
                {
                    double total = producto.precio * producto.cantidad;
                    nsp.insertarDetalleSolicitudProducto(solicitudId, producto.codigo, producto.cantidad, producto.precio, total, producto.tipoSolicitud, producto.medida);
                    montoTotal += total;
                }

            }catch(Exception ex)
            {
                showalert($"Error al insertar productos en la solicitud: {ex.Message}");
            }
            return montoTotal;
        }
        private void ActualizarMontoTotal(int solicitudId, double montoTotal)
        {
            try
            {
                NA_SolicitudPedido nsp = new NA_SolicitudPedido();
                nsp.actualizarmontoTotal(solicitudId, montoTotal);
            }
            catch (Exception ex)
            {
                showalert($"Error al actualizar el monto total: {ex.Message}");
            }
        }

        private void RecargarPagina()
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        private void limpiarCamposPedido()
        {
            NA_SolicitudPedido Nsol = new NA_SolicitudPedido();
            NA_Responsables Nresp = new NA_Responsables();

            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            txt_Nro.Text = Nsol.get_siguentenumeroRecibo(codUser);

            txt_fechaEntrega.Text = string.Empty;
            txt_horaEntrega.Text = string.Empty;
            txt_cliente.Text = string.Empty;

            gv_clientesPedido.DataSource = null;
            gv_clientesPedido.DataBind();

            gv_productosPedidos.DataSource = null;
            gv_productosPedidos.DataBind();

            gv_listaProductosPedido.DataSource = null;
            gv_listaProductosPedido.DataBind();            

            Session.Remove("ScodigoCliente");
            Session.Remove("Productos");
        }

        protected void gv_listaProductosPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName =="Eliminar")
            {
                int codigoProducto = int.Parse(e.CommandArgument.ToString());

                List<Productos> productos = Session["Productos"] as List<Productos>;

                var productoAEliminar = productos.FirstOrDefault(p => p.codigo == codigoProducto);

                if(productoAEliminar != null)
                {
                    productos.Remove(productoAEliminar);
                }
                Session["Productos"] = productos;

                gv_listaProductosPedido.DataSource = productos;
                gv_listaProductosPedido.DataBind();
            }

        }
        protected void btn_Inicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("FA_MenuPorArea.aspx");
        }
        public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }
        private void showalert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);

        }

        
    }
}