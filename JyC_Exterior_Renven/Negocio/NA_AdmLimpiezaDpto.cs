using JyC_Exterior.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Negocio
{
    public class NA_AdmLimpiezaDpto
    {
        DA_AdmLimpiezaDpto datosLD = new DA_AdmLimpiezaDpto();

        public DataSet get_ListRegistroDVisitas(string edificio)
        {
            return datosLD.get_ListRegistroDVisitas(edificio);
        }

        internal bool update_estadoRegistroDVisita(int codigo)
        {
            return datosLD.update_EstadoRegistroDVisita(codigo);
        }


    }
}