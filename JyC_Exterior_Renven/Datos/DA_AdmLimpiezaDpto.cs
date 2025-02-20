﻿using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Globalization;
using MySql.Data.MySqlClient;

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
                "AND ld.nombreInmueble like '%" + edificio + "%' " +
                " order by ld.fechagra desc";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal bool update_EstadoRegistroDVisita(List<int> codigo)
        {
            string codigosStr = string.Join(",", codigo);
            string consulta = "UPDATE tbalq_limpiezadpto ld SET ld.estado = 0 WHERE ld.codigo IN ("+ codigosStr +");";
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_detRegistroItems(int codigo)
        {
            string consulta = "SELECT ld.codigo as 'codRegistro', i.codigo as 'codItem', i.nombre as 'item', dld.cantidad " +
                "from tbalq_limpiezadpto ld INNER JOIN tbalq_detallelimpiezadpto dld ON ld.codigo = dld.codrlimpieza " +
                "LEFT JOIN tbalq_item i ON dld.coditem = i.codigo WHERE ld.estado=1 AND i.estado=1 AND dld.cantidad > 0 AND ld.codigo = " + codigo + ";";
            DataSet list = conexion.consultaMySql(consulta);
            return list;
        }

        internal bool update_cantInsumosRegistro(decimal cantidad, int codRes, int codRlimpieza, int codItem)
        {
            string consulta = "UPDATE tbalq_detallelimpiezadpto ld SET ld.cantidad = @cantidad, ld.codres = @codRes, " +
                "ld.fechagra = current_date(), ld.horagra = current_time() WHERE ld.codrlimpieza = @codRlimpieza " +
                "and ld.coditem = @codItem;";

            using (MySqlCommand comand = new MySqlCommand(consulta))
            {
                comand.Parameters.AddWithValue("@cantidad", cantidad);
                comand.Parameters.AddWithValue("@codRes", codRes);
                comand.Parameters.AddWithValue("@codRlimpieza", codRlimpieza);
                comand.Parameters.AddWithValue("@codItem", codItem);
                
                return conexion.ejecutarMysql2(comand);
            }
        }
    }
}