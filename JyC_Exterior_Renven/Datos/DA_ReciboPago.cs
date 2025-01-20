using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_ReciboPago
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_ReciboPago() { }

        public bool insertar(string detalle, string fecha, string hora, float pago, int codSeguimiento, int codMes, int codresp)
        {

            string pago_aux = pago.ToString();
                pago_aux = pago_aux.Replace(',', '.');
                string consulta = "insert into tb_recibopago(detalle,fecha,hora,pago,codseg,codmes,codresp) " +
                                    " values('" + detalle + "','" + fecha + "','" + hora + "'," + pago_aux + "," + codSeguimiento + "," + codMes + "," + codresp + "); ";

                return ConecRes.ejecutarMySql(consulta);
        }
                

        public bool modificar(int codigoRecibo, int codSeguimiento, int codmes, int codresponsable, string detalle, bool efectivo, bool deposito, string nroCheque)
        {  
                     string consulta = "update tb_recibopago set tb_recibopago.detalle = '"+detalle+"' "+ " , tb_recibopago.efectivo = "+efectivo+" , tb_recibopago.deposito = "+deposito+" , tb_recibopago.nrocheque = '"+nroCheque +"' "+
                                        " where codigo = "+codigoRecibo+"  and codseg = "+codSeguimiento+"  and codmes = "+codmes+"  and codresp = "+ codresponsable;

                     return ConecRes.ejecutarMySql(consulta);
           
        }

        
        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public bool insertarReciboMTTO(int codseg, int codmes, string detalle, float pago, bool efectivo, bool deposito, string nrocheque, string banco, string factura, string recibo, int codresp, int codcobranza, float tipoCambio, int codmoneda, bool transferencia, string fechaPago, float costotransporte)
        {
            float monto_pagoSus = 0;
            if (codmoneda == 1)    // cuando el monto es en bolivianos
            {
                monto_pagoSus = (pago / tipoCambio);
            }
            else
                if (codmoneda == 2)
                    monto_pagoSus = pago;


          float montoPagoBs = monto_pagoSus * tipoCambio;

            string consulta = "insert into tb_recibopago( "+
                               " tb_recibopago.codseg, "+
                               " tb_recibopago.codmes, "+
                               " tb_recibopago.fecha, "+
                               " tb_recibopago.hora, "+
                               " tb_recibopago.detalle, "+
                               " tb_recibopago.pago, "+
                               " tb_recibopago.efectivo, "+
                               " tb_recibopago.deposito, "+
                               " tb_recibopago.nrocheque, "+
                               " tb_recibopago.banco, "+
                               " tb_recibopago.factura, "+
                               " tb_recibopago.recibo, "+
                               " tb_recibopago.codresp, "+
                               " tb_recibopago.codcobranza, "+
                               " tb_recibopago.tipoCambio, " +
                               " tb_recibopago.codmoneda, " +
                               " tb_recibopago.pagobs, "+
                               " tb_recibopago.transferencia, "+
                               " tb_recibopago.costotransporte) "+
                               " values( "+
                               codseg +", "+
                                codmes+", "+
                               fechaPago + " , " +
                               " current_time(), "+
                               "'"+ detalle+"', "+
                               "'" + monto_pagoSus.ToString().Replace(',', '.') + "', " +
                                efectivo+", "+
                                deposito+", "+
                               "'"+nrocheque+"', "+
                               "'"+ banco+"', "+
                               "'"+ factura+"', "+
                               "'"+ recibo+"', "+
                                codresp+", "+
                               codcobranza+","+
                               "'"+ tipoCambio.ToString().Replace(',','.')+"', " +
                               +codmoneda+", " +
                              "'" + montoPagoBs.ToString().Replace(',', '.') + "'," +
                               transferencia+
                               ", '"+costotransporte.ToString().Replace(',', '.')+"' )";
            return ConecRes.ejecutarMySql(consulta);

        }


        internal bool darDeBajaelCobro(int codRuta, string detalleRecibo, int codUserCierre)
        {
            string consulta = "update tb_rutacobrador set "+
                               " tb_rutacobrador.codusercierre = "+codUserCierre+", "+
                               " tb_rutacobrador.fechacierre = current_date(), "+
                               " tb_rutacobrador.horacierre = current_time(), "+
                               " tb_rutacobrador.estadoCobro = 'Cerrado', "+
                               " tb_rutacobrador.cobronegativo = true, "+
                               " tb_rutacobrador.detallerecibo = '"+detalleRecibo+"' "+
                               " where "+
                               " tb_rutacobrador.codigo = "+codRuta;
            return ConecRes.ejecutarMySql(consulta);
        }
    }
}