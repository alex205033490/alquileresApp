using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Datos
{
    public class DA_ActivosDpto
    {
        private conexionMySql conexion = new conexionMySql();

        internal DataSet get_listDpto(string dep)
        {
            string consulta = "SELECT eq.codigo, eq.dg_nombreinmueble as edificio, " +
                " eq.dg_direccion as direccion, eq.dg_denominacion as denominacion, eq.dg_codigovarsimec as codSimec, " +
                "eq.dg_numeroinmueble as nroInmueble, eq.dg_nrodormitorios as nrohabitaciones, " +
                "eq.dg_departamentociudad as ciudad FROM tb_equipo eq where eq.estado = 1 AND " +
                " eq.dg_nombreinmueble like '%" + dep + "%' ";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_listItems(string item)
        {
            string consulta = "SELECT it.codigo, it.nombre FROM tbalq_item it " +
                " inner join tbalq_detallelistcategoria dcat on it.codigo = dcat.codItem " +
                " where dcat.codCategoria = 1 AND dcat.estado = 1 AND it.estado = 1 " +
                " and it.nombre like '%" + item + "%' order by it.nombre asc;";

            DataSet lista = conexion.consultaMySql( consulta); 
            return lista;
        }

        internal bool insert_activosDpto(int coddpto, int coditem, int cantidad, int codres)
        {
            string consulta = "INSERT INTO tbalq_detalleactivodpto(coddpto, coditem, fechagra, horagra, cantidad, codres) " +
                " values ("+ coddpto + ", " + coditem + ", current_date(), current_time(), " + cantidad + ", " + codres + ") " +
                "ON DUPLICATE KEY UPDATE cantidad = cantidad + "+cantidad+" " +
                ",fechagra = current_date() " +
                ",horagra = current_time(); ";

            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_listAlmacenes()
        {
            string consulta = "SELECT al.codigo, al.nombre, al.codSimec from tbalq_almacen al";
            return conexion.consultaMySql(consulta);
        }

        /*         - ERROR -
        internal bool insert_detalleAlmacen(int codAlmacen, int codItem, int cantidad, int codres)
        {
            string consulta = "insert into tbalq_detallealmacen (codalmacen, coditem, fechagra, horagra, cantidad, codres) " +
                " values (" + codAlmacen + ", " + codItem + ", current_date(), current_time(), " + cantidad + ", " + codres + "); ";

            return conexion.ejecutarMySql(consulta);
        }
        */
        internal bool post_reciboIngresoActivoDpto(int coddpto, string codSimec, string nombreInmueble, string nroInmueble, int nrohabitaciones, string direccionInmueble, string dptoInmueble, int codres, string nrodenominacion)
        {
            string consulta = "insert into tbalq_reciboingresoegresoactivodpto(fechagra, horagra, coddpto, codSimec, nombreInmueble, nroInmueble, nrohabitaciones, direccionInmueble, dptoInmueble, tiporecibo, codres, vaciadosimec, estado, nrodenominacioninmueble)" +
                " values (current_date(), current_time(), " + coddpto + ", '" + codSimec + "', '" + nombreInmueble + "', '" + nroInmueble + "', " + nrohabitaciones + ", '" + direccionInmueble + "','" + dptoInmueble + "', 'ingreso', " + codres + ", 0, 1, '"+nrodenominacion+"')";

            return conexion.ejecutarMySql(consulta);               
        }

        internal bool post_detalleReciboIngresoActivoDpto(int codRecibo, int codItem, int cantidad, int codRes, int codAlmacen)
        {
            string consulta = "insert into tbalq_detallereciboingresoegresoactivodpto " +
                "(codrecibo, coditem, fechagra, horagra, cantidad, codres, codalmacen) " +
                "values (" + codRecibo + ", " + codItem + ", current_date(), current_time(), " + cantidad + ", " + codRes + ", " + codAlmacen + ")";

            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_ultimoRegistroReciboIngresoActivoDpto(int codRes)
        {
            string consulta = "select max(codigo) from tbalq_reciboingresoegresoactivodpto re " +
                " where re.codres = " + codRes + "; ";

            return conexion.consultaMySql(consulta);
        }
    }
}
