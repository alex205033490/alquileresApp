using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JyC_Exterior.Datos
{
    public class DA_RenvenTraspasoAlmacenes
    {
        private conexionMySql conexion = new conexionMySql();
        internal bool Post_traspasoAlmacen(int codalm_origen, string codSimec_origen, string Almacen_origen, int codalm_destino, string codSimec_destino, string Almacen_destino, int codRes)
        {
            string consulta = "INSERT INTO tbalq_recibotraspasoalmacen(fechagra, horagra, codalm_origen, codSimec_origen, " +
                "Almacen_origen, ubicacionAlm_origen, dptoAlmacen_origen, codalm_destino, codSimec_destino, Almacen_destino, " +
                "ubicacionAlm_destino, dptoAlmacen_destino, tiporecibo, codres, vaciadosimec, estado) Values " +
                "(current_date(), current_time(), "+codalm_origen+", '"+codSimec_origen+"', '"+Almacen_origen+"','','', "+codalm_destino+", " +
                " '"+codSimec_destino+"', '"+Almacen_destino+"', '', '', 'Traspaso', "+codRes+", 0, 1)";

            return conexion.ejecutarMySql(consulta);
        }

        internal bool POST_detalleTraspasoAlmacen(int codRecibo, int codItem, int cantidad, int codRes, int codAlmacen)
        {
            string consulta = "INSERT INTO tbalq_detallerecibotraspasoalmacen (codrecibo, coditem, fechagra, horagra, cantidad, codres, codalmacen) " +
                " VALUES ("+codRecibo+","+codItem+",current_date(), current_time(), "+cantidad+", "+codRes+","+codAlmacen+");";
            return conexion.ejecutarMySql(consulta);

        }

        internal DataSet get_ultimoRegistroTraspaso(int codRes)
        {
            string consulta = "select max(codigo) from tbalq_recibotraspasoalmacen re " +
                "where re.codres = " + codRes + ";";
            return conexion.consultaMySql(consulta);
        }






    }
}