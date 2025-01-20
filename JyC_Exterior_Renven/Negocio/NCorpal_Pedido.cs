using JyC_Exterior.Datos;
using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Negocio{    
    public class NCorpal_Pedido
    {
        DCorpal_Pedido Dpedido = new DCorpal_Pedido();
        public NCorpal_Pedido() { }

        internal bool actualizarDatosVenta(int codigoPedido, string cliente, string direccion, string ci, string telefono, string razonSocial, string nitEmisor, string correo, decimal montoTotal, decimal tipocambio, int coduser, string responsable, bool factura)
        {
            return Dpedido.actualizarDatosVenta( codigoPedido,  cliente,  direccion,  ci,  telefono,  razonSocial,  nitEmisor,  correo,  montoTotal,  tipocambio, coduser,  responsable,  factura);
        }

        internal DataSet get_detalleProductoVenta(int codigoPedido)
        {
            return Dpedido.get_detalleProductoVenta( codigoPedido);
        }

        internal DataSet get_detalleProductoVenta_boleta(int codigoVenta)
        {
            return Dpedido.get_detalleProductoVenta_boleta(codigoVenta);
        }
            internal DataSet get_PedidosAprobadosPorAlmacen(string cliente, int coduser)
        {
            return Dpedido.get_PedidosAprobadosPorAlmacen(cliente,  coduser);
        }

        internal DataSet get_VentaPedido(int codigoPedido)
        {
            return Dpedido.get_VentaPedido(codigoPedido);
        }

        internal DataSet get_VentaPedido_boleta(int codigoV)
        {
            return Dpedido.get_VentaPedido_boleta(codigoV);
         } 

        internal bool update_poductosVenta(int codigoPedido, int codProducto, decimal cantidad)
        {
            return Dpedido.update_poductosVenta( codigoPedido,  codProducto,  cantidad);
        }
    }
}