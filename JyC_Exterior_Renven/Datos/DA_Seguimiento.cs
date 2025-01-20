using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_Seguimiento
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_Seguimiento() { }

        public bool insertar(string detalle, string hora_Cobro, string dia_cobro, string lugarPago, string fechaContrato, int mesesGratis, string mg_ini, string mg_fin, int codTipoPago, int codEquipo, int estado, int year)
        {
           
                string consulta = "insert into tb_seguimiento(detalle,hora_cobro,dia_cobro,lugar_pago,fecha_contrato,mes_gratis,mg_ini,mg_fin,cod_tipopago,cod_equipo,estado,years) values('" + detalle + "'," + hora_Cobro + "," + dia_cobro + ",'" + lugarPago + "'," + fechaContrato + "," + mesesGratis + "," + mg_ini + "," + mg_fin + "," + codTipoPago + "," + codEquipo + "," + estado + "," + year + ")";
                return ConecRes.ejecutarMySql(consulta);
                
           
        }

        public bool modificar(int codigo, string detalle, string hora_Cobro, string dia_cobro, string lugarPago, string fechaContrato, int mesesGratis, string mg_ini, string mg_fin, int codTipoPago, int year, int codfechaEstadoMan)
        {
           
                string consulta = "update tb_seguimiento set " +
                                "Detalle = '" + detalle + "'," +
                                "hora_cobro = " + hora_Cobro + "," +
                                "dia_cobro  = " + dia_cobro + "," +
                                "lugar_pago = '" + lugarPago + "'," +                                
                                "fecha_contrato = " + fechaContrato + "," +
                                "mes_gratis =  " + mesesGratis + "," +
                                "mg_ini  =  " + mg_ini + "," +
                                "mg_fin  =  " + mg_fin + "," +
                                "cod_tipopago = " + codTipoPago + "," +
                                "years = " + year + ","+
                                "codfechaestadoman = " + codfechaEstadoMan +
                                " where codigo = " + codigo;
                return ConecRes.ejecutarMySql(consulta);
          
            
        }

        public bool modificarFechaEstadoMan(int codSeguimiento, int codfechaEstadoMan) {
           
                string consulta = "update tb_seguimiento set " +                               
                               "codfechaestadoman = " + codfechaEstadoMan +
                               " where codigo = " + codSeguimiento;
                return ConecRes.ejecutarMySql(consulta);
                              
        }

        public bool eliminar(int codigo, int estado)
        {
                string consulta = "update tb_seguimiento set estado="+estado+" where codigo="+codigo;
                return ConecRes.ejecutarMySql(consulta);
            
            
        }

        public DataSet getDatos(string consulta)
        {            
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public DataSet getTablaMySqlRellenada(DataSet TablaMySql, string consulta)
        {
            DataSet datosR = ConecRes.RellenarConConsulta(TablaMySql,consulta);
            return datosR;
        }

        public DataSet getCuadrosXXX_Mantenimiento(int year, string Exbo, string nombreProyecto)
        {
            string consulta = "select eq1.exbo, proy1.nombre as 'Edificio', eq1.parada, eq1.pasajero, eq1.velocidad ,se.years , rr.nombre as 'Cobrador' , " +                                                                
                                " rrin.nombre as 'Rin', "+
                                " rrcc.nombre as 'RCC', "+
                                " rtec.nombre as 'TecMantenimiento', "+
                                " rsup.nombre as 'Supervisor' , "+
                                " date_format(fechacon.fechafirmacontrato,'%d/%m/%Y') as 'FechaContratoFirmado' ," +

                                 "if(" +
                                 "(" +
                                 " select sum(t1.deuda) " +
                                 " from tb_equipo eq, tb_proyecto proy, " +
                                 " (select  " +
                                 " segme.codSeg,seg.cod_equipo, sum(segme.monto_pagar)-sum(segme.monto_pago) as 'deuda' " +
                                 " from tb_detalle_segme segme, tb_seguimiento seg " +                                 
                                 " where  " +
                                 " segme.codSeg = seg.codigo and " +
                                 " seg.years < " + year +
                                 " group by segme.codSeg) as t1 " +
                                 " where  " +                                 
                                 " eq.cod_proyecto = proy.codigo and " +
                                 " eq.codigo = t1.cod_equipo and " +
                                 " eq.codigo = eq1.codigo " +
                                 " group by eq.codigo  " +
                                  " )" +
                                 " > 0 && em.nombre!='Perdido'," +
                                 "'Critico', em.nombre) as 'EstadoMantenimiento'," +

                                 // se modifico para eliminar los null
                                 "IFNULL((" +                                 
                                 " select sum(t1.deuda) " +
                                 " from tb_equipo eq, tb_proyecto proy, " +
                                 " (select  " +
                                 " segme.codSeg,seg.cod_equipo, sum(segme.monto_pagar)-sum(segme.monto_pago) as 'deuda' " +
                                 " from tb_detalle_segme segme, tb_seguimiento seg " +
                                 " where  " +
                                 " segme.codSeg = seg.codigo and " +
                                 " seg.years < " + year +
                                 " group by segme.codSeg) as t1 " +
                                 " where  " +
                                 " eq.cod_proyecto = proy.codigo and " +
                                 " eq.codigo = t1.cod_equipo and " +
                                 " eq.codigo = eq1.codigo " +
                                 " group by eq.codigo  " +                                
                                  " ) ,0) as 'DeudaGestionAnterior'," +
                                  /// fin de deuda anterior
                                 " enero.monto_pagar as 'EneroPagar', enero.monto_pago as 'EneroPago', " +
                                 " febrero.monto_pagar as 'FebreroPagar', febrero.monto_pago as 'FebreroPago', " +
                                 " marzo.monto_pagar as 'MarzoPagar', marzo.monto_pago as 'MarzoPago', " +
                                 " abril.monto_pagar as 'AbrilPagar', abril.monto_pago as 'AbrilPago', " +
                                 " mayo.monto_pagar as 'MayoPagar', mayo.monto_pago as 'MayoPago', " +
                                 " junio.monto_pagar as 'JunioPagar', junio.monto_pago as 'JunioPago', " +
                                 " julio.monto_pagar as 'JulioPagar', julio.monto_pago as 'JulioPago', " +
                                 " agosto.monto_pagar as 'AgostoPagar', agosto.monto_pago as 'AgostoPago', " +
                                 " septiembre.monto_pagar as 'SeptiembrePagar', septiembre.monto_pago as 'SeptiembrePago', " +
                                 " octubre.monto_pagar as 'OctubrePagar', octubre.monto_pago as 'OctubrePago', " +
                                 " noviembre.monto_pagar as 'NoviembrePagar', noviembre.monto_pago as 'NoviembrePago', " +
                                 " diciembre.monto_pagar as 'DiciembrePagar', diciembre.monto_pago as 'DiciembrePago', " +
                                 " (enero.monto_pagar + " +
                                 " febrero.monto_pagar + " +
                                 " marzo.monto_pagar  + " +
                                 " abril.monto_pagar + " +
                                 " mayo.monto_pagar + " +
                                 " junio.monto_pagar + " +
                                 " julio.monto_pagar + " +
                                 " agosto.monto_pagar + " +
                                 " septiembre.monto_pagar + " +
                                 " octubre.monto_pagar + " +
                                 " noviembre.monto_pagar + " +
                                 " diciembre.monto_pagar )  " +
                                 " - " +
                                 " (enero.monto_pago + " +
                                 " febrero.monto_pago + " +
                                 " marzo.monto_pago  + " +
                                 " abril.monto_pago + " +
                                 " mayo.monto_pago + " +
                                 " junio.monto_pago + " +
                                 " julio.monto_pago + " +
                                 " agosto.monto_pago + " +
                                 " septiembre.monto_pago + " +
                                 " octubre.monto_pago + " +
                                 " noviembre.monto_pago + " +
                                 " diciembre.monto_pago ) "+
                                 "+"+
                                //------------suma la deuda anterior 
                                 "IFNULL((" +                                 
                                 " select sum(t1.deuda) " +
                                 " from tb_equipo eq, tb_proyecto proy, " +
                                 " (select  " +
                                 " segme.codSeg,seg.cod_equipo, sum(segme.monto_pagar)-sum(segme.monto_pago) as 'deuda' " +
                                 " from tb_detalle_segme segme, tb_seguimiento seg " +
                                 " where  " +
                                 " segme.codSeg = seg.codigo and " +
                                 " seg.years < " + year +
                                 " group by segme.codSeg) as t1 " +
                                 " where  " +
                                 " eq.cod_proyecto = proy.codigo and " +
                                 " eq.codigo = t1.cod_equipo and " +
                                 " eq.codigo = eq1.codigo " +
                                 " group by eq.codigo  " +                                
                                  " ) ,0) "+
                                //------------fin de la suma de deuda anterior                                  
                                 " as Deuda " +
                                 //---------- el from donde actualizar
                                 " from tb_seguimiento se, tb_fechaestadomantenimiento fm , tb_estado_mantenimiento em, " +
                                 " tb_equipo eq1 " +
                                 " left join tb_responsable rrin on (eq1.cod_rin = rrin.codigo) "+
                                 " left join tb_responsable rrcc on (eq1.cod_rcc = rrcc.codigo) "+
                                 " left join tb_responsable rsup on (eq1.cod_supervisor = rsup.codigo) "+
                                 " left join tb_responsable rtec on (eq1.cod_tecmantenimiento  = rtec.codigo) "+
                                 " left join tb_fechacontrato_firmado fechacon ON fechacon.codigo = eq1.codfechacontratofirmado , " +
                                 " tb_detalle_segme enero, " +
                                 " tb_detalle_segme febrero, " +
                                 " tb_detalle_segme marzo, " +
                                 " tb_detalle_segme abril, " +
                                 " tb_detalle_segme mayo, " +
                                 " tb_detalle_segme junio, " +
                                 " tb_detalle_segme julio, " +
                                 " tb_detalle_segme agosto, " +
                                 " tb_detalle_segme septiembre, " +
                                 " tb_detalle_segme octubre, " +
                                 " tb_detalle_segme noviembre, " +
                                 " tb_detalle_segme diciembre, " +
                                 " tb_proyecto proy1 " +
                                 " left join tb_responsable rr on proy1.codcobradorasignado = rr.codigo " +
                                 " where se.codfechaestadoman = fm.codigo and " +
                                 " fm.codEstadoMan = em.codigo and " +
                                 " se.cod_equipo = eq1.codigo and " +
                                 " eq1.estado = 1 and "+
                                 " eq1.cod_proyecto = proy1.codigo and " +
                                 " se.codigo = enero.codSeg and enero.codMe = 1 and " +
                                 " se.codigo = febrero.codSeg and febrero.codMe = 2 and " +
                                 " se.codigo = marzo.codSeg and marzo.codMe = 3 and " +
                                 " se.codigo = abril.codSeg and abril.codMe = 4 and " +
                                 " se.codigo = mayo.codSeg and mayo.codMe = 5 and " +
                                 " se.codigo = junio.codSeg and junio.codMe = 6 and " +
                                 " se.codigo = julio.codSeg and julio.codMe = 7 and " +
                                 " se.codigo = agosto.codSeg and agosto.codMe = 8 and " +
                                 " se.codigo = septiembre.codSeg and septiembre.codMe = 9 and " +
                                 " se.codigo = octubre.codSeg and octubre.codMe = 10 and " +
                                 " se.codigo = noviembre.codSeg and noviembre.codMe = 11 and " +
                                 " se.codigo = diciembre.codSeg and diciembre.codMe = 12 and " +
                                 " se.years = " + year + " and proy1.nombre like '%" + nombreProyecto + "%' and eq1.exbo like '%" + Exbo + "%' ";
            return getDatos(consulta);        
        }

        public DataSet RellenarCuadrosXXX_SIN_Mantenimiento(DataSet tablaMySql,int year, string Exbo, string nombreProyecto)
        {
            string consulta = "select "+
                               " eq.exbo, "+ 
                               " pp.nombre as 'Edificio', "+
                               " eq.parada, "+
                               " eq.pasajero, "+
                               " eq.velocidad, "+
                               " '0' as 'years',  "+
                               " res.nombre as 'Cobrador', "+
                               //" ee.nombre as 'EstadoMantenimiento', "+
                               " 'Sin Prevision' as 'EstadoMantenimiento', " +
                               " '0'  as 'DeudaGestionAnterior', " +
                               " '0'  as 'EneroPagar', '0'  as 'EneroPago', "+
                               " '0'  as 'FebreroPagar', '0'  as 'FebreroPago', "+
                               " '0'  as 'MarzoPagar', '0'  as 'MarzoPago',  "+
                               " '0'  as 'AbrilPagar', '0'  as 'AbrilPago',  "+
                               " '0'  as 'MayoPagar', '0'  as 'MayoPago',  "+
                               " '0'  as 'JunioPagar', '0'  as 'JunioPago',  "+
                               " '0'  as 'JulioPagar', '0'  as 'JulioPago',  "+
                               " '0'  as 'AgostoPagar', '0'  as 'AgostoPago',  "+
                               " '0'  as 'SeptiembrePagar', '0'  as 'SeptiembrePago',  "+
                               " '0'  as 'OctubrePagar', '0'  as 'OctubrePago',  "+
                               " '0'  as 'NoviembrePagar', '0'  as 'NoviembrePago', "+ 
                               " '0'  as 'DiciembrePagar', '0'  as 'DiciembrePago', "+
                               " '0' as 'Deuda' "+
                               " from tb_equipo eq, tb_fechaestadoequipo ff, tb_estado_equipo ee, "+
                               " tb_proyecto pp "+
                               " left join tb_responsable res on pp.codcobradorasignado = res.codigo  "+
                               " where "+
                               " eq.estado = 1 and "+
                               " eq.codfechaestadoequipo = ff.codigo and "+
                               " ff.codEstadoEquipo = ee.codigo and "+
                               " eq.cod_proyecto = pp.codigo and "+
                               " pp.nombre like '%"+nombreProyecto+"%' and "+
                               " eq.exbo like '%"+Exbo+"%' and "+
                               " ee.nombre = 'Parado por el Cliente' and "+
                               " eq.codigo not in  "+
                               " ( "+
                               " select seg.cod_equipo  "+
                               " from tb_seguimiento seg "+
                               " where "+
                               " seg.years = "+year+
                               " group by seg.cod_equipo "+
                               " )";
            return getTablaMySqlRellenada(tablaMySql, consulta);
        }

        public DataSet getDatosSeguimientoMantenimiento(int codSeguimiento)
        {
            string consulta = "select "+
                               " seg.codigo, seg.Detalle, seg.cod_equipo,seg.years, seg.codfechaestadoman, "+
                               " festado.codEstadoMan "+
                               " from tb_seguimiento seg, tb_fechaestadomantenimiento festado "+
                               " where "+
                               " seg.codfechaestadoman = festado.codigo and "+
                               " seg.codigo = "+codSeguimiento;
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getSeguimientosConDeudaAnterior(int codSeguimiento, int anioSeg)
        {
            string consulta = " select "+
                              " seg1.codigo as 'codSeg' "+
                              " from "+
                              " tb_seguimiento seg1, "+
                              " ( "+
                              "/*-------saca todo codigo de equipo que deba alguna gestion---*/ "+               
                              " select seg.cod_equipo "+
                              " from tb_detalle_segme det, tb_seguimiento seg "+
                              " ,tb_fechaestadomantenimiento fm  "+
                              " where  "+
                              " seg.codigo = det.codSeg and "+
                              " det.pago = 'NO' and seg.years < "+anioSeg+
                              " and det.codMe <= 12 "+
                              " and seg.codfechaestadoman = fm.codigo "+
                              " /* and fm.codEstadoMan not in (1,3,4,6,7,8) */  "+
                              " group by seg.cod_equipo  having count(det.pago) > 0 "+
                              " /*---------------------*/ "+
                              " ) as t1 "+
                              " where  "+
                              " seg1.cod_equipo = t1.cod_equipo and "+
                              " seg1.years = "+anioSeg+" and "+
                              " seg1.codigo = "+codSeguimiento;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet getDatosaPagar_duranteTodaslasGestiones(string exbo)
        {
            string consulta = "select "+
                               " seg.codigo as 'codseg', "+
                               " mes.codigo as 'codmes', "+
                               " seg.years ,  "+
                               " mes.nombre,  "+
                               " segme.monto_pagar as 'Deuda', "+
                               " segme.monto_pago as 'Pagado', "+                                                          
                               " '0' as 'Pagar' , "+                               
                               " 'Ninguno' as 'Cheque', "+
                               " 'Ninguno' as 'Factura', "+
                               " 'Ninguno' as 'Recibo' "+
                               " from "+
                               " tb_equipo eq, "+
                               " tb_seguimiento seg, "+
                               " tb_detalle_segme segme, "+
                               " tb_mes mes "+
                               " where "+
                               " eq.codigo = seg.cod_equipo and "+
                               " seg.codigo = segme.codSeg and "+
                               " segme.codMe = mes.codigo and "+
                               " segme.pago in ('No', 'Error') and "+
                               " eq.exbo = '"+exbo+"' "+
                               " order by seg.years,mes.codigo asc";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_TipoUltimoTipoCambio()
        {
            string consulta = "select " +
                                " tc.codigo, " +
                                " DATE_FORMAT(tc.fecha,'%d/%m/%Y') as fecha ,  " +
                                " tc.hora as 'hora1', tc.TC " +
                                " from tb_tipocambiodolar tc  " +
                                " order by tc.CODIGO desc limit 1";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_moneda()
        {
            string consulta = "select mm.CODIGO, mm.MONEDA, mm.ABREVIATURA, mm.SIMBOLO from tb_moneda mm";
            return ConecRes.consultaMySql(consulta);
        }

        internal bool generar_Cobranza(string DOCUM, string GLOSA, bool ANULADA, float tipocambio, int codmoneda, int coduser, string clicodigo, string  codvendedor, string fechaPago)
        {
            string consulta = "insert into tb_cobranza_recibio( " +
                               " tb_cobranza_recibio.FECHA, " +
                               " tb_cobranza_recibio.HORAGRA, " +
                               " tb_cobranza_recibio.FECHAGRA, " +
                               " tb_cobranza_recibio.DOCUM, " +
                               " tb_cobranza_recibio.GLOSA, " +
                               " tb_cobranza_recibio.ANULADA, " +
                               " tb_cobranza_recibio.tipocambio, " +
                               " tb_cobranza_recibio.codmoneda, " +
                               " tb_cobranza_recibio.coduser,"+
                               " tb_cobranza_recibio.clicodigo,"+
                               " tb_cobranza_recibio.codvendedor,"+
                               " tb_cobranza_recibio.vaciarsimec "+
                               " ) " +
                               " values(" + fechaPago + ", current_time() ,current_date(), " +
                                "'"+ DOCUM +"', " +
                               " '"+GLOSA +"', " +
                                ANULADA+", " +
                               "'"+tipocambio.ToString().Replace(',','.')+"', " +
                                codmoneda+"," +
                                coduser+","+
                                "'"+clicodigo+"',"+
                                "'"+codvendedor+"',false)";
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet get_ultimocobroIngresado()
        {
            string consulta = "select " +
                               " cc.codigo, " +
                               " cc.fecha, " +
                               " cc.docum, " +
                               " cc.glosa, " +
                               " cc.anulada, " +
                               " cc.horagra, " +
                               " cc.fechagra, " +
                               " cc.tipocambio, " +
                               " cc.codmoneda, " +
                               " cc.coduser " +
                               " from tb_cobranza_recibio cc " +
                               " order by cc.CODIGO desc limit 1;";
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet get_cobranzadeldia(int codigoCobranza)
        {
            string consulta = "select "+
                               " cr.codigo, "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as fecha, " +
                               " cr.docum, "+
                               " cr.tipocambio, "+
                               " mm.MONEDA, "+
                               " cr.glosa, "+
                               " res.nombre as 'Cobrado por'  "+
                               " from tb_cobranza_recibio cr, tb_moneda mm, tb_responsable res "+
                               " where  "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " cr.coduser = res.codigo and "+
                               " cr.codigo = "+codigoCobranza;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_Recibo_cobranzadeldia(int codigoCobranza)
        {
            string consulta = "select "+
                               " eq.clicodigo as 'codigo_cliente', "+
                               " concat(pp.nombre,'_',eq.exbo) as 'edificio_exbo', "+
                               " re.recibo, "+
                               " DATE_FORMAT(re.fecha,'%d/%m/%Y') as 'fecha_pago', " +
                               " re.banco, "+
                               " re.nrocheque, "+
                               " re.factura, "+
                               " re.pago as 'importe', "+
                               " re.pagobs as 'importe_Bs', "+
                               " re.detalle as 'observaciones' "+
                               " from  "+
                               " tb_recibopago re, tb_moneda mm, "+
                               " tb_seguimiento seg, tb_equipo eq, tb_proyecto pp "+
                               " where "+
                               " re.codmoneda = mm.CODIGO and "+
                               " re.codseg = seg.codigo and "+
                               " seg.cod_equipo = eq.codigo and "+
                               " eq.cod_proyecto = pp.codigo and "+
                               " re.codcobranza = "+codigoCobranza;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet CobrosGeneralesRecibo()
        {
           /* string consulta = "select "+
                               " cr.codigo, "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as 'fecha', "+
                               " cr.docum, "+
                               " cr.glosa, "+
                               " cr.anulada, "+
                               " DATE_FORMAT(cr.fechagra,'%d/%m/%Y') as 'fechaGra', "+
                               " cr.horagra, "+
                               " cr.tipocambio, "+
                               " mm.MONEDA, "+
                               " cr.coduser "+
                               " from  "+
                               " tb_cobranza_recibio cr, tb_moneda mm "+
                               " where "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " (cr.vaciarsimec = false or cr.vaciarsimec is null);";  */
            string consulta = "select "+
                               " cr.codigo, "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as 'fecha', "+
                               " cr.docum, "+
                               " cr.glosa, "+
                               " cr.anulada, "+
                               " DATE_FORMAT(cr.fechagra,'%d/%m/%Y') as 'fechaGra', "+
                               " cr.horagra, "+
                               " cr.tipocambio, "+ 
                               " mm.MONEDA, "+
                               " (select Sum(re.pago) from tb_recibopago re where re.codcobranza = cr.codigo) as 'Dolares', "+
                               " (select Sum(re1.pagobs) from tb_recibopago re1 where re1.codcobranza = cr.codigo) as 'Bolivianos', "+
                               " (select count(*) from tb_recibopago re2 where re2.codcobranza = cr.codigo) as 'Meses', "+
                               " res.nombre as 'Cobrador'  "+
                               " from  "+
                               " tb_moneda mm , "+
                               " tb_cobranza_recibio cr "+
                               " LEFT JOIN tb_responsable res ON (cr.coduser = res.codigo) "+
                               " where "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " (cr.vaciarsimec = false or cr.vaciarsimec is null) order by cr.codigo asc";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet fechasNoVaciadasAlSimec()
        {
            string consulta = "select  "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as 'fecha' " +
                               " from  "+
                               " tb_cobranza_recibio cr, tb_moneda mm "+
                               " where "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " (cr.vaciarsimec = false or cr.vaciarsimec is null) "+
                               " group by cr.fecha";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet CobrosGeneralesRecibo_porDocumPorFecha(string DOCUM,string fechaDeterminada) {
            string consulta = "select "+
                                " cr.codigo, "+
                                " cr.glosa, "+
                                " cr.coduser, "+
                                " cr.clicodigo, "+
                                " cr.codvendedor, "+
                                " re.codigo as 'codRecibo', "+
                                " re.detalle, "+
                                " re.fecha, "+
                                " re.hora, "+
                                " re.codseg, "+
                                " re.codmes, "+
                                " re.codresp, "+
                                " re.efectivo, "+
                                " re.deposito, "+
                                " re.nrocheque, "+
                                " re.banco, "+
                                " re.recibo, "+
                                " re.factura, "+
                                " re.codcobranza, "+
                                " re.codmoneda, "+
                                " re.tipocambio, "+
                                " re.pago, "+
                                " re.pagobs, "+
                                " seg.years, re.transferencia  " +
                                " from   "+
                                " tb_cobranza_recibio cr, tb_recibopago re "+
                                " , tb_seguimiento seg "+
                                " where "+
                                " re.codseg = seg.codigo and "+
                                " re.codcobranza = cr.codigo and "+
                                " cr.docum = '"+DOCUM+"' and "+
                                " cr.fecha = " + fechaDeterminada;
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet CobrosGeneralesRecibo_porDocumPorClienteSimecPorFecha(string DOCUM, string CodClienteSimec, string fechaDeterminada)
        {
            string consulta = "";
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet todosCobrosRecibo_porFecha(string DOCUM,string fechaDeterminada)
        {
            string consulta = "select  "+
                               " cr.codigo,  "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as 'fecha', "+
                               " cr.docum,  "+
                               " cr.glosa, "+
                               " cr.anulada,  "+
                               " DATE_FORMAT(cr.fechagra,'%d/%m/%Y') as 'fechagra', "+
                               " cr.horagra, "+
                               " cr.tipocambio, "+  
                               " mm.MONEDA,  "+
                               " cr.coduser,  "+
                               " cr.clicodigo,  "+
                               " cr.codvendedor  "+
                               " from   "+
                               " tb_cobranza_recibio cr, tb_moneda mm  "+
                               " where  "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " cr.fecha = "+fechaDeterminada+" and "+
                               " cr.docum = '"+DOCUM+"'";
            return ConecRes.consultaMySql(consulta);
        }

    
     /*     
        /// <summary>
        /// este procedimiento actualiza todos los cobros con el Docum en una fecha determinada
        /// siempre y cuando no haya sido vaciado antes
        /// </summary>
        /// <param name="fechaDeterminada"> fecha en la cual se van actuazlizar todos los registros que no hayan sido vaciados a simec</param>
        /// <param name="DOCUM">la variable de simec que van a compartir en un solo registro</param>
        /// <returns></returns>
        internal bool updateTodosDOCUM_porFecha(string fechaDeterminada, string DOCUM)
        {
            string consulta = "update tb_cobranza_recibio set tb_cobranza_recibio.docum = '"+DOCUM+"'"+
                               ", tb_cobranza_recibio.vaciarsimec = true "+
                               " where  "+
                               " (tb_cobranza_recibio.vaciarsimec = false  "+
                               " or  "+
                               " tb_cobranza_recibio.vaciarsimec is null) and "+
                               " tb_cobranza_recibio.fecha = "+fechaDeterminada;
            return ConecRes.ejecutarMySql(consulta);
        }
        */

        internal bool updateDOCUM(int codigoCobroRecibo, string DOCUM)
        {
            string consulta = "update tb_cobranza_recibio set tb_cobranza_recibio.docum = '"+DOCUM+"'"+
                               " where tb_cobranza_recibio.codigo = "+codigoCobroRecibo;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet get_recibosCobradosDelaCobranza(int codCobranzaRealizada)
        {
            string consulta = "select "+
                               " re.codigo, "+
                               " re.detalle, "+
                               " re.fecha, "+
                               " re.hora, "+
                               " re.codseg, "+
                               " re.codmes, "+
                               " re.codresp, "+
                               " re.efectivo, "+
                               " re.deposito, "+
                               " re.nrocheque, "+
                               " re.banco, "+
                               " re.recibo, "+
                               " re.factura, "+
                               " re.codcobranza, "+
                               " re.codmoneda, "+
                               " re.tipocambio, "+
                               " re.pagobs "+
                               " from tb_recibopago re "+
                               " where re.codcobranza = "+codCobranzaRealizada;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet SumaTodoslosRecibosdelCobro(int codigoCobro)
        {
            string consulta = "select "+
                                " re.codcobranza, "+
                                " re.banco, "+
                                " re.codmoneda, "+
                                " re.tipocambio, "+
                                " sum(re.pago) as 'SumaDolares', "+
                                " sum(re.pagobs) as 'SumaBolivianos' "+
                                " from  "+ 
                                " tb_recibopago re "+
                                " where "+  
                                " re.codcobranza = "+codigoCobro+
                                " group by re.codcobranza ";
            return ConecRes.consultaMySql(consulta);
        }

        internal string get_detalleMttoCobro(int codigoCobro)
        {
            string consulta = "select  re.codcobranza, "+
                               " re.banco,  re.codmoneda, "+ 
                               " re.tipocambio,  re.pago,  "+
                               " re.pagobs, "+  
                               " m.nombre as mes, "+
                               " seg.years "+ 
                               " from   tb_recibopago re, tb_mes m, tb_seguimiento seg "+
                               " where   re.codmes = m.codigo and  re.codseg = seg.codigo "+
                               " and  re.codcobranza = "+codigoCobro;
           DataSet detalleFilas = ConecRes.consultaMySql(consulta);
           string detalle = "";
            if(detalleFilas.Tables[0].Rows.Count > 0){
                detalle = "(";
                for (int i = 0; i < detalleFilas.Tables[0].Rows.Count; i++)
                {
                    detalle = detalle + detalleFilas.Tables[0].Rows[i][6].ToString() + "-" + detalleFilas.Tables[0].Rows[i][7].ToString() + ",";
                }                
                detalle = detalle + ")";
            }else
                detalle = "Ninguno";
            return detalle;
        }

        internal DataSet tuplas_TodosRecibosdelCobroRealizado(int codigoCobro)
        {
            string consulta = "select "+
                                " re.codigo as 'codRecibo', "+
                                " re.detalle, "+
                                " re.fecha, "+
                                " re.hora, "+
                                " re.codseg, "+
                                " re.codmes, "+
                                " re.codresp, "+
                                " re.efectivo, "+
                                " re.deposito, "+
                                " re.nrocheque, "+
                                " re.banco, "+
                                " re.recibo, "+
                                " re.factura, "+
                                " re.codcobranza, "+
                                " re.codmoneda, "+
                                " re.tipocambio, "+
                                " re.pago, "+
                                " re.pagobs  "+
                                " from  "+
                                " tb_recibopago re "+
                                " where "+
                                " re.codcobranza = "+codigoCobro;
            return ConecRes.consultaMySql(consulta);
           }

        //--------------------por cliente Simect 

        /// <summary>
        /// este procedimiento actualiza todos los cobros con el Docum en una fecha determinada
        /// siempre y cuando no haya sido vaciado antes
        /// </summary>
        /// <param name="fechaDeterminada"> fecha en la cual se van actuazlizar todos los registros que no hayan sido vaciados a simec</param>
        /// <param name="DOCUM">la variable de simec que van a compartir en un solo registro</param>
        /// <returns></returns>
        internal bool updateTodosDOCUM_porFecha2(string fechaDeterminada, string DOCUM,int CodMonedaCliente)
        {
            string consulta = " update tb_cobranza_recibio, tb_equipo " +
                               " set tb_cobranza_recibio.docum = '" + DOCUM + "', tb_cobranza_recibio.vaciarsimec = true " +
                               " where " +
                               " (tb_cobranza_recibio.vaciarsimec = false or tb_cobranza_recibio.vaciarsimec is null) and " +
                               " tb_cobranza_recibio.clicodigo = tb_equipo.clicodigo and " +
                               " tb_equipo.monedaprevision_simec = "+CodMonedaCliente+" and " +
                               " tb_cobranza_recibio.fecha = " + fechaDeterminada;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal bool get_hayCobranzadeCliente_conLaMonedaDeterminada(string fechaDeterminada, int CodMonedaCliente)
        {
            string consulta = "select co.* from tb_cobranza_recibio co, tb_equipo eq " +
                               " where " +
                               " co.clicodigo = eq.clicodigo and " +
                               " (co.vaciarsimec = false or co.vaciarsimec is null) and " +
                               " eq.monedaprevision_simec = "+CodMonedaCliente+" and " +
                               " co.fecha = " + fechaDeterminada;
            DataSet filas = ConecRes.consultaMySql(consulta);
             if (filas.Tables[0].Rows.Count > 0)
             {
                 return true;
             }
             else
                 return false;

        }


        internal bool anularCobroReciboGeneral(int CodigoCobroRecibo, bool anulado)
        {
            string consulta = "update tb_cobranza_recibio set "+
                               " tb_cobranza_recibio.vaciarsimec = "+anulado+", "+
                               " tb_cobranza_recibio.anulada = "+anulado+
                               " where  "+
                               " tb_cobranza_recibio.codigo = "+CodigoCobroRecibo;
            return ConecRes.ejecutarMySql(consulta);
        }


        internal DataSet DeudasApagarEquipoPorEquipo(int codEquipo, float tipoCambio)
        {
            string consulta = "select "+ 
                               " seg.codigo, "+                               
                               " dseg.codMe, "+
                               " seg.years, " +
                               " m.nombre, "+
                               " (dseg.monto_pagar - dseg.monto_pago) as 'DeudaSUS', "+
                               " ((dseg.monto_pagar - dseg.monto_pago)* '"+tipoCambio.ToString().Replace(',','.')+"') as 'DeudaBS', " +
                               " '"+tipoCambio.ToString().Replace(',','.')+"' " +
                               " from "+
                               " tb_seguimiento seg, tb_detalle_segme dseg, tb_mes m "+
                               " where "+
                               " seg.codigo = dseg.codSeg and "+
                               " dseg.codMe = m.codigo and "+
                               " (dseg.monto_pagar - dseg.monto_pago) > 0 and "+
                               " seg.years <= year(current_date()) and "+
                               " seg.cod_equipo = '"+codEquipo+"' "+
                               " order by seg.years,dseg.codMe asc";
            return ConecRes.consultaMySql(consulta);
        }

        internal bool generar_Cobranza(string DOCUM, string GLOSA, bool ANULADA, double tipocambio, int codmoneda, int coduser, string clicodigo, string codvendedor, string fechaPago, float costotransporte)
        {
            string consulta = "insert into tb_cobranza_recibio( " +
                               " tb_cobranza_recibio.FECHA, " +
                               " tb_cobranza_recibio.HORAGRA, " +
                               " tb_cobranza_recibio.FECHAGRA, " +
                               " tb_cobranza_recibio.DOCUM, " +
                               " tb_cobranza_recibio.GLOSA, " +
                               " tb_cobranza_recibio.ANULADA, " +
                               " tb_cobranza_recibio.tipocambio, " +
                               " tb_cobranza_recibio.codmoneda, " +
                               " tb_cobranza_recibio.coduser," +
                               " tb_cobranza_recibio.clicodigo," +
                               " tb_cobranza_recibio.codvendedor," +
                               " tb_cobranza_recibio.vaciarsimec, " +
                               " tb_cobranza_recibio.costotransporte "+
                               " ) " +
                               " values(" + fechaPago + ", current_time() ,current_date(), " +
                                "'" + DOCUM + "', " +
                               " '" + GLOSA + "', " +
                                ANULADA + ", " +
                               "'" + tipocambio.ToString().Replace(',', '.') + "', " +
                                codmoneda + "," +
                                coduser + "," +
                                "'" + clicodigo + "'," +
                                "'" + codvendedor + "',false,'" + costotransporte.ToString().Replace(',', '.') + "')";
            return ConecRes.ejecutarMySql(consulta);
        }

      
    }
}