using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_RutaCobrador
    {
        private conexionMySql sql = new conexionMySql();
        public DA_RutaCobrador() { }

        public bool insertarRutaCobrador(int codEquipo, string edificio, string exbo, int codcobrador, int codcliente, string cobrador, string fechacobro, string horacobro, string detalle, float montocobrar,string estadoCobro,int coduserInicio)
        {
            string consulta = "insert into tb_rutacobrador( "+
                               " tb_rutacobrador.codcliente, tb_rutacobrador.codusercobrador, "+
                               " tb_rutacobrador.codequipo, tb_rutacobrador.edificio, "+
                               " tb_rutacobrador.fechagra, tb_rutacobrador.horagra, "+
                               " tb_rutacobrador.fechacobro, tb_rutacobrador.horacobro, "+ 
                               " tb_rutacobrador.exbo, tb_rutacobrador.detalle, "+
                               " tb_rutacobrador.coduserinicio, tb_rutacobrador.estadoCobro, "+
                               " tb_rutacobrador.estado, tb_rutacobrador.nrorecibo, "+
                               " tb_rutacobrador.montocobrar  " +
                               " ) values( "+
                               codcliente + " , " + codcobrador + ", " +
                               codEquipo+" , '"+edificio+"', "+
                               " current_date(), current_time(), "+
                               fechacobro+" , '"+horacobro+"',  "+
                               " '"+exbo+"', '"+detalle+"',  "+
                               coduserInicio + ", '" + estadoCobro + "', " +
                               " 1, concat( year(current_date()), month(current_date()), (select IFNULL(max(vv.codigo),0)+1 from tb_rutacobrador vv)  ), " +
                               " '"+montocobrar.ToString().Replace(',','.')+"' )";
            return sql.ejecutarMySql(consulta);

        }

        public bool eliminar(int codRutaCobro) {
            string consulta = "delete from tb_rutacobrador where tb_rutacobrador.codigo =" + codRutaCobro;
            return sql.ejecutarMySql(consulta);
        }

        public bool modificarRutaCobro(int codRutaCobro, int codEquipo, string edificio, string exbo, int codcobrador, string cobrador, string fechacobro, string horacobro, float montocobrar) {
            return false;
        }

        internal DataSet getrutasAsignadas(string edificio, string cobradornombre)
        {
            string consulta = "select "+
                               " rr.codigo, "+
                               " rr.nrorecibo, "+
                               " rr.edificio, "+  
                               " rr.exbo, "+ 
                               " rr.detalle, "+
                               " ep.nombre as 'Cliente', "+
                               " date_format(rr.fechacobro,'%d/%m/%Y') as 'fecha_Cobro', "+
                               " rr.horacobro, "+
                               " rr.estadoCobro, "+
                               " rr.montocobrar, "+
                               " res.nombre as 'Cobrador' "+ 
                               " from tb_rutacobrador rr, tb_encargado_pago ep,tb_responsable res "+
                               " where "+
                               " rr.codusercobrador = res.codigo and "+
                               " rr.codcliente = ep.codigo and "+
                               " rr.edificio like '%"+edificio+"%' and "+
                               " res.nombre like '%"+cobradornombre+"%'";
            return sql.consultaMySql(consulta);
        }

      

        internal DataSet getrutasAsignadasCobrador(int codCobrador)
        {
            string consulta = "select "+
                               " rr.codigo, "+
                               " rr.nrorecibo, "+
                               " rr.edificio, "+ 
                               " rr.exbo, "+ 
                               " rr.detalle, "+
                               " ep.nombre as 'Cliente', "+
                               " date_format(rr.fechacobro,'%d/%m/%Y') as 'fecha_Cobro',  "+
                               " rr.horacobro,"+
                               " rr.estadoCobro,"+
                               " rr.montocobrar,"+
                               " res.nombre as 'Cobrador'  "+
                               " from tb_rutacobrador rr "+
                               " LEFT JOIN tb_encargado_pago ep ON (rr.codcliente = ep.codigo),"+ 
                               " tb_responsable res " +
                               " where "+
                               " rr.estadoCobro = 'Abierto' and "+
                               " rr.codusercobrador = res.codigo and "+                               
                               " rr.codusercobrador = "+codCobrador;
            return sql.consultaMySql(consulta);
        }



        internal DataSet getrutasAsignadasporCodigo(int codrutaCobro)
        {
            string consulta = "select  rr.codigo,  "+
                               " rr.nrorecibo, "+
                               " rr.edificio, "+
                               " rr.exbo, "+ 
                               " rr.detalle, "+  
                               " ep.nombre as 'Cliente', "+  
                               " date_format(rr.fechacobro,'%d/%m/%Y') as 'fecha_Cobro', "+   
                               " rr.horacobro, "+ 
                               " rr.estadoCobro, "+ 
                               " rr.montocobrar, "+ 
                               " res.nombre as 'Cobrador', "+ 
                               " rr.codequipo "+  
                               " from tb_rutacobrador rr "+ 
                               " left join tb_encargado_pago ep on (rr.codcliente = ep.codigo) "+
                               " left join tb_responsable res on (rr.codusercobrador = res.codigo) "+  
                               " where   rr.codigo = " + codrutaCobro;
            return sql.consultaMySql(consulta);
        }

        internal bool pagarReciboCobro(int CodRutaCobro,int codusercierre , string detallerecibo,                                       
                                       string codcliente , string  cliente, string  cicliente, string cargocliente,
                                       float montopagado_bs,  string modopago_bs, float tipoCambio_bs,
                                        float montopagado2_bs,  string modopago2_bs, float tipoCambio2_bs,
                                        float montopagado_sus,  string modopago_sus, float tipoCambio_sus,
                                        float montopagado2_sus, string modopago2_sus, float tipoCambio2_sus
                                        , string numerocheque_bs, string numerocheque2_bs, string numerocheque_us, string numerocheque2_us, string nroFactura, float costotransporte)
        {
            string consulta = "update tb_rutacobrador " +
                               " set " +
                                "  tb_rutacobrador.codusercierre = " + codusercierre + ", " +
                                "  tb_rutacobrador.fechacierre = current_date(), " +
                                "  tb_rutacobrador.horacierre = current_time(), " +
                                "  tb_rutacobrador.estadoCobro = 'Cerrado', " +
                                "  tb_rutacobrador.detallerecibo = '" + detallerecibo + "', " +                                
                                "  tb_rutacobrador.codcliente = '" + codcliente + "'," +
                                "  tb_rutacobrador.cliente = '" + cliente + "'," +
                                "  tb_rutacobrador.cicliente = '" + cicliente + "'," +
                                "  tb_rutacobrador.cargocliente = '" + cargocliente + "'" +

                                "  ,tb_rutacobrador.montopagado_bs= '" + montopagado_bs.ToString().Replace(',', '.') + "'" +
                                "  ,tb_rutacobrador.moneda_bs = 'Bolivianos'" +
                                "  ,tb_rutacobrador.modopago_bs='" + modopago_bs + "'" +
                                "  ,tb_rutacobrador.tipocambio_bs='" + tipoCambio_bs.ToString().Replace(',', '.') + "'" +

                                "  ,tb_rutacobrador.montopagado2_bs= '" + montopagado2_bs.ToString().Replace(',', '.') + "'" +
                                "  ,tb_rutacobrador.moneda2_bs = 'Bolivianos'" +
                                "  ,tb_rutacobrador.modopago2_bs='" + modopago2_bs + "'" +
                                "  ,tb_rutacobrador.tipocambio2_bs='" + tipoCambio2_bs.ToString().Replace(',', '.') + "'" +

                                "  ,tb_rutacobrador.montopagado_us= '" + montopagado_sus.ToString().Replace(',', '.') + "'" +
                                "  ,tb_rutacobrador.moneda_us = 'Dolares'" +
                                "  ,tb_rutacobrador.modopago_us='" + modopago_sus + "'" +
                                "  ,tb_rutacobrador.tipocambio_us='" + tipoCambio_sus.ToString().Replace(',', '.') + "'" +

                                "  ,tb_rutacobrador.montopagado2_us= '" + montopagado2_sus.ToString().Replace(',', '.') + "'" +
                                "  ,tb_rutacobrador.moneda2_us = 'Dolares'" +
                                "  ,tb_rutacobrador.modopago2_us='" + modopago2_sus + "'" +
                                "  ,tb_rutacobrador.tipocambio2_us='" + tipoCambio2_sus.ToString().Replace(',', '.') + "'" +

                                 " ,tb_rutacobrador.numerocheque_bs = '"+numerocheque_bs+"' "+
                                 " ,tb_rutacobrador.numerocheque2_bs='"+numerocheque2_bs+"' "+
                                 " ,tb_rutacobrador.numerocheque_us='"+numerocheque_us+"' "+
                                 " ,tb_rutacobrador.numerocheque2_us ='"+numerocheque2_us+"'"+
                                 " ,tb_rutacobrador.nrofactura = '"+nroFactura+"'"+
                                 " ,tb_rutacobrador.costotransporte = '" + costotransporte.ToString().Replace(',','.') + "'" +
                                 
                                "  where " +
                                "  tb_rutacobrador.codigo =" + CodRutaCobro;
            return sql.ejecutarMySql(consulta);
        }

        internal DataSet getreciboCerrado(int CodRutaCobro)
        {
            string consulta = "select "+
                               " rr.codigo, "+
                               " date_format(rr.fechacierre, '%d/%m/%Y') as 'fecha', "+
                               " rr.horacierre, "+
                               " rr.nrorecibo, "+
                               " rr.edificio, "+
                               " eq.clicodigo, " +
                               " rr.cliente, "+
                               " rr.cargocliente, "+
                               " res.nombre as 'cobrador', "+
                               " rr.detallerecibo, "+
                              " rr.montopagado_bs, "+
                              " rr.montopagado2_bs, "+
                              " rr.montopagado_us, "+
                              " rr.montopagado2_us,  "+
                              " rr.modopago_bs, "+
                              " rr.modopago2_bs, "+
                              " rr.modopago_us, "+
                              " rr.modopago2_us, "+
                              " rr.nrofactura "+
                               " from tb_rutacobrador rr, tb_responsable res, tb_equipo eq " +
                               " where  "+
                               " rr.codusercobrador = res.codigo and "+
                               " rr.codigo = "+CodRutaCobro;
            return sql.consultaMySql(consulta);
        }

        internal string getCodiClienteSimec(int codrutaCobro)
        {
            string consulta = "select eq.clicodigo "+
                               " from tb_rutacobrador ru , tb_equipo eq "+
                               " where  "+
                               " ru.codequipo = eq.codigo and "+
                               " ru.codigo =  "+codrutaCobro;
            DataSet tupla = sql.consultaMySql(consulta);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                return tupla.Tables[0].Rows[0][0].ToString();
            }
            else
                return "Ninguno";
        }
    }
}