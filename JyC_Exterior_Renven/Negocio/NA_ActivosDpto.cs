using JyC_Exterior.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Negocio
{
    public class NA_ActivosDpto
    {
        DA_ActivosDpto datosActivos = new DA_ActivosDpto();

        public DataSet get_buscarDpto(string dpto)
        {
            return datosActivos.get_listDpto(dpto);
        }

        public DataSet get_buscarItem (string item)
        {
            return datosActivos.get_listItems(item);
        }

        internal bool insertar_activosDpto(int codDpto, int codItem, int cantidad, int codred)
        {
            return datosActivos.insert_activosDpto(codDpto, codItem, cantidad, codred);
        }

        public DataSet get_listAlmacenes()
        {
            return datosActivos.get_listAlmacenes();
        }
        /*     - ERROR -
        internal bool insertar_detalleAlmacen(int codAlmacen, int codItem, int cantidad, int codres)
        {
            return datosActivos.insert_detalleAlmacen(codAlmacen, codItem, cantidad, codres);
        }
        */

        internal bool post_reciboIngresoActivo(int coddpto, string codSimec, string nombreInmueble, string nroInmueble, int nrohabitaciones, string direccionInmueble, string dptoInmueble, int codres, string nroDenominacion)
        {
            return datosActivos.post_reciboIngresoActivoDpto(coddpto, codSimec, nombreInmueble, nroInmueble, nrohabitaciones, direccionInmueble, dptoInmueble, codres, nroDenominacion);
        }

        internal bool post_detalleReciboIngresoActivoDpto(int codRecibo, int codItem, int cantidad, int codRes, int codAlmacen)
        {
            return datosActivos.post_detalleReciboIngresoActivoDpto(codRecibo, codItem, cantidad, codRes, codAlmacen);
        }

        internal int get_ultimoRegistroReciboIngresoActivo(int codRes)
        {
            DataSet datos = datosActivos.get_ultimoRegistroReciboIngresoActivoDpto(codRes);
            if (datos.Tables[0].Rows.Count > 0)
            {
                return int.Parse(datos.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }
    }
}