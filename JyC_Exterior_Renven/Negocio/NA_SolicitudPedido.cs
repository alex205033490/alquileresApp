using JyC_Exterior.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Negocio
{
    public class NA_SolicitudPedido
    {
        DA_SolicitudPedido dsp = new DA_SolicitudPedido();

        public DataSet get_mostrarProductos(string producto)
        {
            return dsp.get_mostrarProductos(producto);
        }

        public DataSet get_mostrarClientes(string cliente)
        {
            return dsp.get_mostrarClientes(cliente);
        }

        internal bool set_guardarSolicitud(string nroboleta, string fechaentrega, string horaentrega, string personalsolicitud, int codpersolicitante, bool estado, int codcliente)
        {
            return dsp.set_guardarSolicitud(nroboleta, fechaentrega, horaentrega, personalsolicitud, codpersolicitante, estado, codcliente);
        }

        internal int getultimaSolicitudproductoInsertado(int codpersolicitante)
        {
            DataSet dato = dsp.getultimaSolicitudproductoInsertado(codpersolicitante);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }

        internal bool insertarDetalleSolicitudProducto(int ultimoinsertado, int codProducto, double cantidad, double preciocompra, double total, string Tipo, string Medida)
        {
            return dsp.insertarDetalleSolicitudProducto(ultimoinsertado, codProducto, cantidad, preciocompra, total, Tipo, Medida);
        }

        internal bool actualizarmontoTotal(int ultimoinsertado, double montoTotal)
        {
            return dsp.actualizarmontoTotal(ultimoinsertado, montoTotal);
        }

        internal string get_siguentenumeroRecibo(int codUser)
        {
            DataSet dato = dsp.get_siguentenumeroRecibo(codUser);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows[0][0].ToString();
            }
            else
                return "Ninguno";
        }
    }
}