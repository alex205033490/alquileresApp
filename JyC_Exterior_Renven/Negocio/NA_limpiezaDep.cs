using JyC_Exterior.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Negocio
{
    public class NA_limpiezaDep
    {
        DA_limpiezaDep datosld = new DA_limpiezaDep();

        public DataSet get_mostrarDep(string dep)
        {
            return datosld.get_listDepartamentoInmueble(dep);
        }

        public DataSet get_mostrarItemRepo()
        {
            return datosld.get_itemReposicionDep();
        }

        internal bool insert_limpiezadpto(int coddpto, string codSimec, string nombreInmueble, string nroInmueble, int nroHabitaciones, string direccionInmueble, string dptoInmueble, string tipoLimpieza, int codRLimpieza, string observacion, int codTipoLimpiza, string denominacion)
        {
            return datosld.insert_limpiezadpto(coddpto, codSimec, nombreInmueble, nroInmueble, nroHabitaciones, direccionInmueble, dptoInmueble, tipoLimpieza, codRLimpieza, observacion, codTipoLimpiza, denominacion);
        }

        internal bool insert_detLimpiezaDpto(int codRLimpieza, int codItem, int cantidad, int codRes)
        {
            return datosld.insert_detLimpiezaDpto(codRLimpieza, codItem, cantidad, codRes);
        }

        internal int get_ultimoRegistroLimpDpto(int codRlimpieza)
        {
            DataSet datos = datosld.get_ultimoRegistroLimpiezaDpto(codRlimpieza);
            if (datos.Tables[0].Rows.Count > 0)
            {
                return int.Parse(datos.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }

        public DataSet get_mostrarTiposLimpieza()
        {
            return datosld.get_tiposLimpieza();
        }

    }
}