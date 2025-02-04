using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Datos
{
    public class DA_AdmLimpiezaDpto
    {
        private conexionMySql conexion = new conexionMySql();

        internal DataSet get_ListRegistroDVisitas(string edificio)
        {
            string consulta = " select ld.codigo as nro, ld.nombreInmueble as Edificio, " +
                "ld.nrodenominacioninmueble as nroHabitacion, ld.direccionInmueble as Direccion, " +
                "ld.tipoLimpieza, ld.dptoInmueble as Ciudad, ld.fechagra as fecha, ld.horagra as hora, " +
                "ld.observacion as Detalles from tbalq_limpiezadpto ld WHERE ld.estado = 1 " +
                "AND ld.nombreInmueble like '%"+edificio+"%' " +
                " order by ld.fechagra desc";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal bool update_EstadoRegistroDVisita(int codigo)
        {
            string consulta = " UPDATE tbalq_limpiezadpto ld SET ld.estado = 0 WHERE ld.codigo like '%" + codigo + "%' ";
            return conexion.ejecutarMySql(consulta);
        }


    }
}