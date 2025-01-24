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

    }
}