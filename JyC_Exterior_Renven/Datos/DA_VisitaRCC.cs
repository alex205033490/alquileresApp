using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace JyC_Exterior.Datos
{
    public class DA_VisitaRCC
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_VisitaRCC() { }

        internal DataSet get_RutaVisitasRCC(int codUser)
        {
            string consulta = "select "+
                               " cc.codigo, "+
                               " date_format(cc.fechavisita,'%d/%m/%Y') as 'fecha_Visita', " +
                               " cc.horavisita, "+
                               " cc.edificio, "+
                               " cc.detalle, "+
                               " cc.cantequipos, "+
                               " cc.mediaMesesAtrazados "+
                               " from tb_rutarcc_cobro cc "+
                               " where "+ 
                               " cc.coduser = "+codUser+" and "+
                               " cc.fechacierre is null";
            return ConecRes.consultaMySql(consulta) ;
        }

        internal DataSet get_RutaVisitasRCC_codigoRuta(int codRutaRCC)
        {
            string consulta = "select "+
                               " cc.codigo, "+
                               " cc.horavisita, "+
                               " date_format(cc.fechavisita,'%d/%m/%Y') as 'fecha_Visita', "+
                               " cc.encargadodelpago, "+ 
                               " cc.edificio, "+
                               " cc.cantequipos, "+ 
                               " cc.mediaMesesAtrazados, "+
                               " cc.nombrepersonal "+
                               " from tb_rutarcc_cobro cc "+
                               " where "+
                               " cc.codigo = "+codRutaRCC;
            return ConecRes.consultaMySql(consulta);
        }

        internal bool actualizarDatosVisitaRCC(int codRutaRCC, string ClienteDato, string horaInicio, string horaFin, string observacionesReclamos, string observacionesAsuntosTratados, int codUserCierre)
        {
            string consulta = "UPDATE tb_rutarcc_cobro set "+
                               " tb_rutarcc_cobro.clientevisita = '"+ClienteDato+"', "+
                               " tb_rutarcc_cobro.fechainiciovisita = current_date(), "+
                               " tb_rutarcc_cobro.horainiciovisita = '"+horaInicio+"', "+
                               " tb_rutarcc_cobro.fechafinalizacionvisita = current_date(), "+
                               " tb_rutarcc_cobro.horafinalizacionvisita = '"+horaFin+"', "+
                               " tb_rutarcc_cobro.fechacierre = current_date(), "+
                               " tb_rutarcc_cobro.horacierre = current_time(), "+
                               " tb_rutarcc_cobro.observacionesreclamossugerenciacliente = '"+observacionesReclamos+"', "+
                               " tb_rutarcc_cobro.asuntostratadosyacordados = '"+observacionesAsuntosTratados+"', "+
                               " tb_rutarcc_cobro.codusercierre =  "+ codUserCierre+                               
                               " where  "+
                               " tb_rutarcc_cobro.codigo = "+codRutaRCC;
            return ConecRes.ejecutarMySql(consulta);
        }
    }
}