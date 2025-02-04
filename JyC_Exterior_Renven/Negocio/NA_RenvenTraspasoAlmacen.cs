using JyC_Exterior.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Negocio
{
    public class NA_RenvenTraspasoAlmacen
    {
        DA_RenvenTraspasoAlmacenes datos = new DA_RenvenTraspasoAlmacenes();

        internal bool POST_TraspasoAlmacen(int codalm_origen, string codSimec_origen, string Almacen_origen, int codalm_destino, string codSimec_destino, string Almacen_destino, int codRes)
        {
            return datos.Post_traspasoAlmacen(codalm_origen, codSimec_origen, Almacen_origen, codalm_destino, codSimec_destino, Almacen_destino, codRes);
        }

        internal bool POST_DetalleTraspasoAlmacen(int codRecibo, int codItem, int cantidad, int codRes, int codAlmacen)
        {
            return datos.POST_detalleTraspasoAlmacen(codRecibo, codItem, cantidad, codRes, codAlmacen);
        }

        internal int get_ultimoRegistroTraspaso(int codRes)
        {
            DataSet datoss = datos.get_ultimoRegistroTraspaso(codRes);
            if (datoss.Tables[0].Rows.Count > 0)
            {
                return int.Parse(datoss.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }
    }
}