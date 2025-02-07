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

        //get edificio
        internal DataSet get_listEdificios(string edificio)
        {
            string consulta = "select e.codigo, e.dg_nombreinmueble from tb_equipo e " +
                "where e.estado = 1 and e.dg_nombreinmueble LIKE '%" + edificio + "%' group by e.dg_nombreinmueble order by e.dg_nombreinmueble";
            DataSet list = conexion.consultaMySql(consulta);
            return list;

        }

        // GET Despartamentos
        internal DataSet get_listDepartamentoInmueble(string dep)
        {
            string consulta = " select eq.codigo as codDep, eq.dg_nombreinmueble as Edificio, eq.dg_numeroinmueble as nroInmueble," +
                " eq.dg_nrodormitorios as nroDormitorios, eq.dg_direccion as direccionDep, eq.dg_departamentociudad as ciudad, " +
                "eq.dg_codigovarsimec as codSimec, eq.dg_denominacion as nro_habitacion from tb_equipo eq where eq.estado=1 and eq.dg_nombreinmueble like '%" + dep + "%' ";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_itemReposicionDep(int codigo)
        {
            string consulta = " Select it.codigo, nombre from tbalq_item it inner join tbalq_detallelistcategoria" +
                " dlc on it.codigo = dlc.codItem where dlc.codCategoria = "+codigo+"  AND it.estado = 1 AND dlc.estado = 1; ";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }


        internal bool insert_limpiezadpto(int coddpto, string codSimec, string nombreInmueble, string nroInmueble, int nroHabitaciones, string direccionInmueble, string dptoInmueble, string tipoLimpieza, int codRLimpieza, string observacion, int codTipoLimpieza, string denominacion)
        {
            string consulta = "INSERT INTO tbalq_limpiezadpto(fechagra, horagra, coddpto, codSimec, nombreInmueble, nroInmueble, nrohabitaciones, direccionInmueble, " +
                " dptoInmueble, tipoLimpieza, codreslimpieza, observacion, vaciadosimec,estado,codtipolimpieza, nrodenominacioninmueble) values " +
                " (current_date(), current_time(), "+ coddpto +" , '"+ codSimec +"', '"+ nombreInmueble + "' , '"+ nroInmueble +"' , "+ nroHabitaciones +" , '"+ direccionInmueble +"' ,'"+ dptoInmueble +"', " +
                " '"+ tipoLimpieza +"', "+ codRLimpieza +",'"+ observacion +"', 0 ,1, "+ codTipoLimpieza +", '"+denominacion+"'  ); ";

            return conexion.ejecutarMySql(consulta);
        }

        internal bool insert_detLimpiezaDpto(int codRLimpieza, int codItem, string cantidad, int codRes)
        {
            string consulta = "INSERT INTO tbalq_detallelimpiezadpto (codrlimpieza, coditem, fechagra, horagra, cantidad, codres) " +
                " VALUES ("+ codRLimpieza +", "+ codItem +", current_date(), current_time(), '"+ cantidad +"', "+ codRes +" )";

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
            string consulta = "select codigo, nombre from tbalq_tipolimpieza;";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

    }
}
