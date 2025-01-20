using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;
using JyC_Exterior.Datos;

namespace JyC_Exterior.Negocio
{
    public class NA_VisitaRCC
    {
        private DA_VisitaRCC Dvc = new DA_VisitaRCC();

        public NA_VisitaRCC() { }
                
        internal DataSet get_RutaVisitasRCC(int codUser)
        {
            return Dvc.get_RutaVisitasRCC(codUser);
        }

        internal DataSet get_RutaVisitasRCC_codigoRuta(int codRutaRCC)
        {
            return Dvc.get_RutaVisitasRCC_codigoRuta( codRutaRCC);
        }

        internal bool actualizarDatosVisitaRCC(int codRutaRCC, string ClienteDato, string horaInicio, string horaFin, string observacionesReclamos, string observacionesAsuntosTratados, int codUserCierre)
        {
            return Dvc.actualizarDatosVisitaRCC(codRutaRCC, ClienteDato, horaInicio, horaFin, observacionesReclamos, observacionesAsuntosTratados, codUserCierre);
        }
    }
}