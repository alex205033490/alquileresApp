using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Datos
{
    public class DCorpal_Pedido
    {
        private conexionMySql cnx = new conexionMySql();
        public DCorpal_Pedido() { }

        internal bool actualizarDatosVenta(int codigoPedido, string cliente, string direccion, string ci, string telefono, string razonSocial, string nitEmisor, string correo, decimal montoTotal, decimal tipocambio, int coduser, string responsable, bool factura)
        {
            string consulta = "update tbcorpal_venta set  " +
                " tbcorpal_venta.codigoCliente = NULL, " +
                " tbcorpal_venta.cliente =  '"+cliente+"', " +
                " tbcorpal_venta.correoCliente = '"+correo+"', " +
                " tbcorpal_venta.nitEmisor = 'nitEmisor', " +
                " tbcorpal_venta.razonSocialEmisor = 'razonSocialEmisor', " +
                " tbcorpal_venta.municipio = 'Santa Cruz', " +
                " tbcorpal_venta.telefono = '"+telefono+"', " +
                " tbcorpal_venta.numeroFactura = '0', " +
                " tbcorpal_venta.cuf = '0', " +
                " tbcorpal_venta.cufd = '0', " +
                " tbcorpal_venta.direccion = '"+direccion+"', " +
                " tbcorpal_venta.fechaEmision = current_date(), " +
                " tbcorpal_venta.nombreRazonSocial = '"+razonSocial+"', " +
                " tbcorpal_venta.numeroDocumento ='"+nitEmisor+"', " +
                " tbcorpal_venta.codigoMetodoPago = '0', " +
                " tbcorpal_venta.numeroTarjeta = '0', " +
                " tbcorpal_venta.montoTotal = '"+montoTotal.ToString().Replace(',','.')+"', " +
                " tbcorpal_venta.montoTotalSujetoIva = '0', " +
                " tbcorpal_venta.codigoMoneda = '1', " +
                " tbcorpal_venta.tipoCambio = '"+tipocambio.ToString().Replace(',','.')+"', " +
                " tbcorpal_venta.montoTotalMoneda = '"+montoTotal.ToString().Replace(',','.')+"', " +
                " tbcorpal_venta.descuentoAdicional = '0', " +
                " tbcorpal_venta.leyendaF = 'Leyenda', " +
                " tbcorpal_venta.codusercierre = " + coduser+", " +
                " tbcorpal_venta.responsablecierre = '" + responsable+"', " +
                " tbcorpal_venta.factura = "+factura+","+
                " tbcorpal_venta.estadoventa = 'Cerrado', " +
                " tbcorpal_venta.vaciadoupon = false " +
                " where tbcorpal_venta.codigo = " +codigoPedido;
            return cnx.ejecutarMySql(consulta);
        }

        internal DataSet get_detalleProductoVenta(int codigoPedido)
        {
            string consulta = "select dd.codprod, pp.producto, format(dd.cantidad,0) as 'cantidad', pp.medida, format(dd.precioUnitario,2) as 'precioUnitario', format(dd.precioTotal,2) as 'precioTotal'  from tbcorpal_detalleventasproducto dd, tbcorpal_producto pp, tbcorpal_venta vv  where dd.codventa = vv.codigo and  dd.codprod = pp.codigo and vv.estadoventa = 'Abierto' and vv.estado = 1 and vv.codigo = " + codigoPedido;
            return cnx.consultaMySql(consulta);
        }

        internal DataSet get_detalleProductoVenta_boleta(int codigoVenta) {
            string consulta = "select pp.producto, format(dd.cantidad,0) as 'cantidad',   " +
                " format(dd.precioUnitario,2) as 'PrecioUnidad',   " +
                " format(dd.precioTotal,2) as 'PrecioTotal',  " +
                " dd.codprod as 'codigo'    " +
                " from tbcorpal_detalleventasproducto dd,   " +
                " tbcorpal_producto pp, tbcorpal_venta vv    " +
                " where dd.codventa = vv.codigo and    " +
                " dd.codprod = pp.codigo and    " +
                " vv.estado = 1 and   vv.codigo = "+codigoVenta;
            return cnx.consultaMySql(consulta);
        }

        internal DataSet get_PedidosAprobadosPorAlmacen(string cliente, int coduser)
        {
            string consulta = "select codigo, date_format(fechaentrega,'%d/%m/%Y') as 'Fecha_Entrega', " +
                " cliente,numeroDocumento,nombreRazonSocial,telefono " +
                " from tbcorpal_venta vv where " +
                " vv.codresp = "+coduser+" and "+
                " vv.cliente like '%" +cliente+ "%'  and vv.estadoventa = 'Abierto' and vv.estado = 1 " +
                " order by codigo desc ";
            return cnx.consultaMySql(consulta);
        }

        internal DataSet get_VentaPedido(int codigoPedido)
        {
            string consulta = "select vv.codigo, date_format(vv.fechaentrega,'%d/%m/%Y') as 'Fecha_Entrega', " +
                " vv.cliente, vv.direccion, vv.cicliente, vv.telefono, vv.nombreRazonSocial,  vv.numeroDocumento, " +
                " vv.correoCliente, vv.montoTotal, vv.tipoCambio from tbcorpal_venta vv where vv.codigo =  " +codigoPedido+
                " order by vv.codigo desc ";
            return cnx.consultaMySql(consulta);
        }

        internal DataSet get_VentaPedido_boleta(int codigoPedido)
        {
            string consulta = "select vv.codigo, date_format(vv.fechagra,'%d/%m/%Y') as 'FechaGrabacion',  " +
                " vv.horagra, vv.cliente, vv.nombreRazonSocial,  vv.numeroDocumento,   " +
                " vv.correoCliente, vv.montoTotal, vv.responsable   " +
                " from tbcorpal_venta vv " +
                " where vv.codigo =  " + codigoPedido +
                " order by vv.codigo desc ";
            return cnx.consultaMySql(consulta);
        }

        internal bool update_poductosVenta(int codigoPedido, int codProducto, decimal cantidad)
        {
            string consulta = "update tbcorpal_detalleventasproducto set " +                
                " tbcorpal_detalleventasproducto.cantidad = '"+cantidad.ToString().Replace(',','.')+"', " +
                " tbcorpal_detalleventasproducto.precioTotal = (tbcorpal_detalleventasproducto.precioUnitario * '"+cantidad.ToString().Replace(',','.')+"') " +
                " where  tbcorpal_detalleventasproducto.codventa = "+codigoPedido+" and " +
                " tbcorpal_detalleventasproducto.codprod = "+codProducto;
            return cnx.ejecutarMySql(consulta);
        }
    }
}