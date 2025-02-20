﻿using JyC_Exterior.Datos;
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

        internal bool update_estadoRegistroDVisita(List<int> codigo)
        {
            return datosLD.update_EstadoRegistroDVisita(codigo);
        }
        public DataSet get_detRegistroItems(int codigo)
        {
            return datosLD.get_detRegistroItems(codigo);
        }

        public bool ModificarDetCantInsumos(decimal cantidad, int codRes, int codRlimpieza, int codItem)
        {
            return datosLD.update_cantInsumosRegistro(cantidad, codRes, codRlimpieza, codItem);
        }

    }
}