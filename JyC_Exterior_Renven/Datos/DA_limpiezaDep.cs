using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Windows.Controls;

namespace JyC_Exterior.Datos
{
    public class DA_limpiezaDep
    {
        private conexionMySql conexion = new conexionMySql();


        // GET Despartamentos
        internal DataSet get_listDepartamentoInmueble(string dep)
        {
            string consulta = " select eq.codigo as codDep, eq.dg_nombreinmueble as Edificio, eq.dg_numeroinmueble as nroInmueble," +
                " eq.dg_nrodormitorios as nroDormitorios, eq.dg_direccion as direccionDep, eq.dg_departamentociudad as ciudad, " +
                "eq.dg_codigovarsimec as codSimec from tb_equipo eq where eq.dg_nombreinmueble like '%" + dep + "%' ";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_itemReposicionDep()
        {
            string consulta = " Select it.codigo, nombre from tbalq_item it inner join tbalq_detallelistcategoria" +
                " dlc on it.codigo = dlc.codItem where dlc.codCategoria = 2 AND it.estado = 1 AND dlc.estado = 1; ";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }


        internal bool insert_limpiezadpto(int coddpto, string codSimec, string nombreInmueble, string nroInmueble, int nroHabitaciones, string direccionInmueble, string dptoInmueble, string tipoLimpieza, int codRLimpieza, string observacion, int codTipoLimpieza)
        {
            string consulta = "INSERT INTO tbalq_limpiezadpto(fechagra, horagra, coddpto, codSimec, nombreInmueble, nroInmueble, nrohabitaciones, direccionInmueble, " +
                " dptoInmueble, tipoLimpieza, codreslimpieza, observacion, vaciadosimec,estado,codtipolimpieza) values " +
                " (current_date(), current_time(), "+ coddpto +" , '"+ codSimec +"', '"+ nombreInmueble + "' , '"+ nroInmueble +"' , "+ nroHabitaciones +" , '"+ direccionInmueble +"' ,'"+ dptoInmueble +"', " +
                " '"+ tipoLimpieza +"', "+ codRLimpieza +",'"+ observacion +"', 0 ,1, "+ codTipoLimpieza +"  ); ";

            return conexion.ejecutarMySql(consulta);
        }

        internal bool insert_detLimpiezaDpto(int codRLimpieza, int codItem, int cantidad, int codRes)
        {
            string consulta = "INSERT INTO tbalq_detallelimpiezadpto (codrlimpieza, coditem, fechagra, horagra, cantidad, codres) " +
                " VALUES ("+ codRLimpieza +", "+ codItem +", current_date(), current_time(), "+ cantidad +", "+ codRes +" )";

            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_ultimoRegistroLimpiezaDpto(int codRLiempieza)
        {
            string consulta = "SELECT max(codigo) from tbalq_limpiezadpto ld where " +
                " ld.codreslimpieza = "+ codRLiempieza +";";
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_tiposLimpieza()
        {
            string consulta = " select codigo, nombre from tbalq_tipolimpieza;";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

    }
}
