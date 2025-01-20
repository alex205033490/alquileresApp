using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NA_VariablesGlobales
    {
        public static int meseslimitesdeAtrazadosPermitidosMantenimiento = 2;



        public static string fechaInicialProduccion = "'2024-03-11'";

        internal string get_consultaStockProductosActual()
        {
            /* string consultaStock = "SELECT pp.codigo, pp.producto, pp.medida, ifnull(t1.ingreso,0) as 'Ingreso1', ifnull(t2.salida,0) as 'Salida1', " +
                                       " (ifnull(t1.ingreso,0) - ifnull(t2.salida,0)) as 'StockAlmacen' " +
                                       " FROM tbcorpal_producto pp " +
                                       " LEFT JOIN " +
                                       " ( " +
                                       " select " +
                                       " oo.codProductonax, sum(oo.cantcajas) as 'ingreso', " +
                                       " sum(oo.pack_ferial) as 'itempackferial' " +
                                       " from tbcorpal_entregasordenproduccion oo " +
                                       " where " +
                                       " oo.estado = 1 and " +
                                       " oo.fechagra between " + NA_VariablesGlobales.fechaInicialProduccion + " and current_date() " +
                                       " group by oo.codProductonax " +
                                       " ) as t1  ON pp.codigo = t1.codProductonax " +
                                       " LEFT JOIN " +
                                       " ( " +
                                       " select dss.codproducto, sum(dss.cantentregada) as 'salida' " +                                      
                                       " from tbcorpal_solicitudentregaproducto ss, " +
                                       " tbcorpal_detalle_solicitudproducto dss " +
                                       " where " +
                                       " ss.codigo = dss.codsolicitud and " +
                                       " ss.estado = 1 and " +
                                       " ss.estadosolicitud = 'Cerrado' and " +
                                       " dss.itempackferial is not true and " +
                                       " ss.fechaentrega between " + NA_VariablesGlobales.fechaInicialProduccion + " and current_date() " +
                                       " group by dss.codproducto " +
                                       " ) as t2 ON pp.codigo = t2.codproducto " +
                                       " WHERE " +
                                       " pp.estado = 1"; */
            string consultaStock = "SELECT pp.codigo, pp.producto, pp.medida, ifnull(t1.ingreso,0) as 'Ingreso1', ifnull(t2.salida,0) as 'Salida1',   " +
                " (ifnull(t1.ingreso,0) - ifnull(t2.salidaCajas,0)) as 'StockAlmacen',   " +
                " (ifnull(t1.ingresopackferial,0) - ifnull(t2.salidaPackFerial,0)) as 'StockPackFerial'  " +
                " FROM tbcorpal_producto pp   " +
                " LEFT JOIN    " +
                " (   " +
                " select   " +
                " oo.codProductonax, sum(oo.cantcajas) as 'ingreso',   " +
                " sum(oo.pack_ferial) as 'ingresopackferial'   " +
                " from tbcorpal_entregasordenproduccion oo   " +
                " where   " +
                " oo.estado = 1 and   " +
                " oo.fechagra between " + NA_VariablesGlobales.fechaInicialProduccion + " and current_date()   " +
                " group by oo.codProductonax   " +
                " ) as t1  ON pp.codigo = t1.codProductonax   " +
                " LEFT JOIN   " +
                " (   " +
                " select dss.codproducto,  " +
                " sum(dss.cantentregada) as 'salida',  " +
                " sum(  " +
                " case    " +
                " when dss.tiposolicitud <> 'ITEM PACK FERIAL' then dss.cantentregada  " +
                " else 0  " +
                " end)   " +
                " as 'salidaCajas' ,  " +
                " sum(  " +
                " case dss.tiposolicitud   " +
                " when 'ITEM PACK FERIAL' then dss.cantentregada  " +
                " else 0  " +
                " end)   " +
                " as 'salidaPackFerial'                                          " +
                " from tbcorpal_solicitudentregaproducto ss,   " +
                " tbcorpal_detalle_solicitudproducto dss   " +
                " where   " +
                " ss.codigo = dss.codsolicitud and   " +
                " ss.estado = 1 and   " +
                " ss.estadosolicitud = 'Cerrado' and  " +
                " ss.fechaentrega between " + NA_VariablesGlobales.fechaInicialProduccion + " and current_date()   " +
                " group by dss.codproducto   " +
                " ) as t2 ON pp.codigo = t2.codproducto   " +
                " WHERE   " +
                " pp.estado = 1 ";

            return consultaStock;
        }
    }
}