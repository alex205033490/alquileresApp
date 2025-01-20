using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Datos
{
    public class DA_SolicitudPedido
    {
        private conexionMySql conexion = new conexionMySql();
        internal DataSet get_mostrarProductos(string producto) 
        {
            NA_VariablesGlobales vlocal = new NA_VariablesGlobales();
            string consultaStockActual = vlocal.get_consultaStockProductosActual();

            string consulta = "select pp.codigo, pp.producto, pp.medida, 0 as 'precio', t1.StockAlmacen, t1.StockPackFerial " +
                                " from tbcorpal_producto pp " +
                                "left join (" +
                                consultaStockActual +
                                " ) as t1 on t1.codigo = pp.codigo " +
                                " where pp.producto like '%" + producto + "%' " +
                                " and pp.estado = 1 ";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
        internal DataSet get_mostrarClientes(string cliente)
        {
            string consulta = "select cli.codigo, cli.tiendaname from tbcorpal_cliente cli where cli.tiendaname like '%" + cliente + "%'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
        internal bool set_guardarSolicitud(string nroboleta, string fechaentrega, string horaentrega, string personalsolicitud, int codpersolicitante, bool estado, int codcliente)
        {
            string consulta = "insert into tbcorpal_solicitudentregaproducto ( " +
                             " nroboleta, fechaGRA, horaGRA, fechaentrega, horaentrega,  personalsolicitud, " +
                             " codpersolicitante,   estado, estadosolicitud, codcliente, vaciadoupon) " +
                             " values( " +
                             " '" + nroboleta + "', current_date(), current_time(), " + fechaentrega + ", '" + horaentrega + "',  '" + personalsolicitud + "'," +
                              codpersolicitante + ", " + estado + ", 'Abierto'," + codcliente + ", '0')";
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet getultimaSolicitudproductoInsertado(int codpersolicitante)
        {
            string consulta = "select max(pp.codigo) from tbcorpal_solicitudentregaproducto pp where " +
                              " pp.codpersolicitante = " + codpersolicitante;
            return conexion.consultaMySql(consulta);
        }

        internal bool insertarDetalleSolicitudProducto(int ultimoinsertado, int codProducto, double cantidad, double preciocompra, double total, string Tipo, string Medida)
        {
            string consulta = "insert into tbcorpal_detalle_solicitudproducto( " +
                           " codsolicitud,codproducto,cant,precio,precioTotal,tiposolicitud,medida) " +
                           " values(" + ultimoinsertado + "," + codProducto + ",'" + cantidad.ToString().Replace(',', '.') + "','" + preciocompra.ToString().Replace(',', '.') + "','" + total.ToString().Replace(',', '.') + "','" + Tipo + "','" + Medida + "')";
            return conexion.ejecutarMySql(consulta);
        }

        internal bool actualizarmontoTotal(int ultimoinsertado, double montoTotal)
        {
            string consulta = "update tbcorpal_solicitudentregaproducto " +
                               " set tbcorpal_solicitudentregaproducto.montototal = '" + montoTotal.ToString().Replace(',', '.') + "'" +
                               " where tbcorpal_solicitudentregaproducto.codigo = " + ultimoinsertado;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_siguentenumeroRecibo(int codUser)
        {
            string consulta = "select CONCAT( " +
                                " substring(res.nombre,1,1), " +
                                " substring(REVERSE(LEFT(REVERSE(res.nombre), locate(' ', REVERSE(res.nombre))-1 )),1,1),'_', " +
                                " count(ss.codigo) + 1 " +
                                " ) as 'NroRecibo' " +
                                " from tbcorpal_solicitudentregaproducto ss, tb_responsable res " +
                                " where " +
                                " ss.codpersolicitante = res.codigo and " +
                                " ss.codpersolicitante = " + codUser;
            return conexion.consultaMySql(consulta);
        }
    }
}