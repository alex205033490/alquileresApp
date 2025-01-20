using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_CostosPolizaImportacion
    {
        private conexionMySql cnx = new conexionMySql();

        public DA_CostosPolizaImportacion() { }

        public bool insertcostospoliza(string nrodui, string fechafactura, string nitproveedor, string nombrerazonsocialproveedor, float impbaseparacreditofiscal,
                                       float creditofiscal , string facturacomercial , float giroalexterior ,
                                       string proveedoresporpagar , float iva_cf_poliza ,  float planillaaduanera ,
                                       float pagoplanillaaduanera , float iva_cf_planillaaduanera ,  float valornetoplanillaaduanera ,
                                       float dif_enpago_pa , float seguro , float prorrateodecostos_transporte_internacional ,
                                       float prorrateodecostos_transporte_nacional , float prorrateodecostos_nrofacttransptnaceinter ,
                                       float prorrateodecostos_logisticaparatransporte ,  float prorrateodecostos_nrofactlogistica ,
                                       float prorrateodecostos_mscltda , float prorrateodecostos_nrofactmsc , float prorrateodecostos_aspb ,
                                       float prorrateodecostos_nrodepositooplanillaaspb , float mercaderiaentransito , float totalcostopoliza ,
                                       float cantidad ,  string descripciondelproducto ,  string transexpbolivianos_moneda ,
                                       float transexpresadobolivianos_girofacturacomercial , float transexpresadobolivianos_comision ,
                                       float transexpresadobolivianos_itf , float transexpresadobolivianos_difdecambio ,
                                       float transexpresadobolivianos_total , string observaciones ) {

            string consulta = "insert into tb_polizas_costos ( "+
                               " nrodui , "+
                               " fechagra , "+
                               " horagra , "+
                               " fechafactura , "+
                               " nitproveedor ,"+
                               " nombrerazonsocialproveedor , "+
                               " impbaseparacreditofiscal , "+
                               " creditofiscal , "+
                               " facturacomercial , "+
                               " giroalexterior , "+
                               " proveedoresporpagar , "+
                               " iva_cf_poliza , "+
                               " planillaaduanera , "+
                               " pagoplanillaaduanera , "+
                               " iva_cf_planillaaduanera , "+
                               " valornetoplanillaaduanera , "+
                               " dif_enpago_pa , "+
                               " seguro , "+
                               " prorrateodecostos_transporte_internacional , "+
                               " prorrateodecostos_transporte_nacional , "+
                               " prorrateodecostos_nrofacttransptnaceinter , "+
                               " prorrateodecostos_logisticaparatransporte , "+
                               " prorrateodecostos_nrofactlogistica , "+
                               " prorrateodecostos_mscltda , "+
                               " prorrateodecostos_nrofactmsc , "+
                               " prorrateodecostos_aspb , "+
                               " prorrateodecostos_nrodepositooplanillaaspb , "+
                               " mercaderiaentransito , "+
                               " totalcostopoliza , "+
                               " cantidad , "+
                               " descripciondelproducto , "+
                               " transexpbolivianos_moneda , "+
                               " transexpresadobolivianos_girofacturacomercial , "+
                               " transexpresadobolivianos_comision , "+
                               " transexpresadobolivianos_itf , "+
                               " transexpresadobolivianos_difdecambio , "+
                               " transexpresadobolivianos_total , "+
                               " observaciones )"+                             
                               " values( "+
                               "'"+nrodui+"',"+
                               " current_date, "+
                               " current_time, "+
                               "'"+fechafactura+"',"+
                               "'"+nitproveedor+"',"+
                               "'"+nombrerazonsocialproveedor+"',"+
                               "'"+impbaseparacreditofiscal.ToString().Replace(',','.')+"',"+
                               "'"+creditofiscal.ToString().Replace(',','.')+"',"+
                               "'"+facturacomercial+"',"+
                               "'"+giroalexterior.ToString().Replace(',','.')+"',"+
                               "'"+proveedoresporpagar+"',"+
                               "'"+iva_cf_poliza.ToString().Replace(',','.')+"',"+
                               "'"+planillaaduanera.ToString().Replace(',','.')+"',"+
                               "'"+pagoplanillaaduanera.ToString().Replace(',','.')+"',"+
                               "'"+iva_cf_planillaaduanera.ToString().Replace(',','.')+"',"+
                               "'"+valornetoplanillaaduanera.ToString().Replace(',','.')+"',"+
                               "'"+dif_enpago_pa.ToString().Replace(',','.')+"',"+
                               "'"+seguro.ToString().Replace(',','.')+"',"+
                               "'"+prorrateodecostos_transporte_internacional.ToString().Replace(',','.')+"',"+
                               "'"+prorrateodecostos_transporte_nacional.ToString().Replace(',','.')+"',"+
                               "'"+prorrateodecostos_nrofacttransptnaceinter.ToString().Replace(',','.')+"',"+
                               "'"+prorrateodecostos_logisticaparatransporte.ToString().Replace(',','.')+"',"+
                               "'"+prorrateodecostos_nrofactlogistica.ToString().Replace(',','.')+"',"+
                               "'"+prorrateodecostos_mscltda.ToString().Replace(',','.')+"',"+
                               "'"+prorrateodecostos_nrofactmsc.ToString().Replace(',','.')+"',"+
                               "'"+prorrateodecostos_aspb.ToString().Replace(',','.')+"',"+
                               "'"+prorrateodecostos_nrodepositooplanillaaspb.ToString().Replace(',','.')+"',"+
                               "'"+mercaderiaentransito.ToString().Replace(',','.')+"',"+
                               "'"+totalcostopoliza.ToString().Replace(',','.')+"',"+
                               "'"+cantidad.ToString().Replace(',','.')+"',"+
                               "'"+descripciondelproducto+"',"+
                               "'"+transexpbolivianos_moneda.ToString()+"',"+
                               "'"+transexpresadobolivianos_girofacturacomercial.ToString().Replace(',','.')+"',"+
                               "'"+transexpresadobolivianos_comision.ToString().Replace(',','.')+"',"+
                               "'"+transexpresadobolivianos_itf.ToString().Replace(',','.')+"',"+
                               "'"+transexpresadobolivianos_difdecambio.ToString().Replace(',','.')+"',"+
                               "'"+transexpresadobolivianos_total.ToString().Replace(',','.')+"',"+
                               "'"+observaciones+"')";
            return cnx.ejecutarMySql(consulta);
        }


        public bool updatecostospoliza(string nrodui, string fechafactura, string nitproveedor, string nombrerazonsocialproveedor, float impbaseparacreditofiscal,
                                       float creditofiscal, string facturacomercial, float giroalexterior,
                                       string proveedoresporpagar, float iva_cf_poliza, float planillaaduanera,
                                       float pagoplanillaaduanera, float iva_cf_planillaaduanera, float valornetoplanillaaduanera,
                                       float dif_enpago_pa, float seguro, float prorrateodecostos_transporte_internacional,
                                       float prorrateodecostos_transporte_nacional, float prorrateodecostos_nrofacttransptnaceinter,
                                       float prorrateodecostos_logisticaparatransporte, float prorrateodecostos_nrofactlogistica,
                                       float prorrateodecostos_mscltda, float prorrateodecostos_nrofactmsc, float prorrateodecostos_aspb,
                                       float prorrateodecostos_nrodepositooplanillaaspb, float mercaderiaentransito, float totalcostopoliza,
                                       float cantidad, string descripciondelproducto, string transexpbolivianos_moneda,
                                       float transexpresadobolivianos_girofacturacomercial, float transexpresadobolivianos_comision,
                                       float transexpresadobolivianos_itf, float transexpresadobolivianos_difdecambio,
                                       float transexpresadobolivianos_total, string observaciones)
        {

            string consulta = "update tb_polizas_costos set " +                                                              
                               " fechafactura = '"+fechafactura+"' , " +
                               " nitproveedor = '"+nitproveedor+"',"+
                               " nombrerazonsocialproveedor = '"+nombrerazonsocialproveedor+"', " +
                               " impbaseparacreditofiscal = '" + impbaseparacreditofiscal.ToString().Replace(',', '.') + "', " +
                               " creditofiscal = '" + creditofiscal.ToString().Replace(',', '.') + "', " +
                               " facturacomercial = '"+facturacomercial+"', " +
                               " giroalexterior = '" + giroalexterior.ToString().Replace(',', '.') + "' , " +
                               " proveedoresporpagar  = '"+proveedoresporpagar+"', " +
                               " iva_cf_poliza = '" + iva_cf_poliza.ToString().Replace(',', '.') + "', " +
                               " planillaaduanera = '" + planillaaduanera.ToString().Replace(',', '.') + "' , " +
                               " pagoplanillaaduanera = '" + pagoplanillaaduanera.ToString().Replace(',', '.') + "' , " +
                               " iva_cf_planillaaduanera = '" + iva_cf_planillaaduanera.ToString().Replace(',', '.') + "' , " +
                               " valornetoplanillaaduanera = '" + valornetoplanillaaduanera.ToString().Replace(',', '.') + "' , " +
                               " dif_enpago_pa = '" + dif_enpago_pa.ToString().Replace(',', '.') + "' , " +
                               " seguro = '" + seguro.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_transporte_internacional = '" + prorrateodecostos_transporte_internacional.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_transporte_nacional = '" + prorrateodecostos_transporte_nacional.ToString().Replace(',', '.') + "' , " +
                               " prorrateodecostos_nrofacttransptnaceinter = '" + prorrateodecostos_nrofacttransptnaceinter.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_logisticaparatransporte = '" + prorrateodecostos_logisticaparatransporte.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_nrofactlogistica = '" + prorrateodecostos_nrofactlogistica.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_mscltda = '" + prorrateodecostos_mscltda.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_nrofactmsc = '" + prorrateodecostos_nrofactmsc.ToString().Replace(',', '.') + "' , " +
                               " prorrateodecostos_aspb = '" + prorrateodecostos_aspb.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_nrodepositooplanillaaspb = '" + prorrateodecostos_nrodepositooplanillaaspb.ToString().Replace(',', '.') + "', " +
                               " mercaderiaentransito = '" + mercaderiaentransito.ToString().Replace(',', '.') + "', " +
                               " totalcostopoliza = '" + totalcostopoliza.ToString().Replace(',', '.') + "' , " +
                               " cantidad = '" + cantidad.ToString().Replace(',', '.') + "', " +
                               " descripciondelproducto = '"+descripciondelproducto+"', " +
                               " transexpbolivianos_moneda = '" + transexpbolivianos_moneda.ToString() + "', " +
                               " transexpresadobolivianos_girofacturacomercial = '" + transexpresadobolivianos_girofacturacomercial.ToString().Replace(',', '.') + "', " +
                               " transexpresadobolivianos_comision = '" + transexpresadobolivianos_comision.ToString().Replace(',', '.') + "', " +
                               " transexpresadobolivianos_itf = '" + transexpresadobolivianos_itf.ToString().Replace(',', '.') + "', " +
                               " transexpresadobolivianos_difdecambio = '" + transexpresadobolivianos_difdecambio.ToString().Replace(',', '.') + "', " +
                               " transexpresadobolivianos_total = '" + transexpresadobolivianos_total.ToString().Replace(',', '.') + "', " +
                               " observaciones = '"+observaciones+"' "+
                               " where nrodui = '"+nrodui+"'";                               
            return cnx.ejecutarMySql(consulta);
        }

        public DataSet get_PolizasCostos(string NroDUI) {
            String consulta = "select "+
                               " nrodui, "+
                               " date_format(fechagra,'%d/%m/%Y') as 'fechagra' , "+
                               " horagra , "+
                               " date_format(fechafactura,'%d/%m/%Y') as 'fechafactura' , "+
                               " nitproveedor, " +
                               " nombrerazonsocialproveedor , impbaseparacreditofiscal , creditofiscal , "+
                               " facturacomercial , giroalexterior , proveedoresporpagar , "+
                               " iva_cf_poliza , planillaaduanera , pagoplanillaaduanera , "+
                               " iva_cf_planillaaduanera , valornetoplanillaaduanera , dif_enpago_pa , "+
                               " seguro , prorrateodecostos_transporte_internacional , prorrateodecostos_transporte_nacional , "+
                               " prorrateodecostos_nrofacttransptnaceinter , prorrateodecostos_logisticaparatransporte , "+
                               " prorrateodecostos_nrofactlogistica , prorrateodecostos_mscltda , prorrateodecostos_nrofactmsc , "+
                               " prorrateodecostos_aspb , prorrateodecostos_nrodepositooplanillaaspb , mercaderiaentransito , "+
                               " totalcostopoliza , cantidad , descripciondelproducto , "+
                               " transexpbolivianos_moneda , transexpresadobolivianos_girofacturacomercial , "+
                               " transexpresadobolivianos_comision , transexpresadobolivianos_itf , "+
                               " transexpresadobolivianos_difdecambio , transexpresadobolivianos_total , "+
                               " observaciones  "+
                               " from  "+
                               " tb_polizas_costos pp "+
                               " where  "+
                               " pp.nrodui = '"+NroDUI+"'";
            return cnx.consultaMySql(consulta);
        }


        public DataSet get_ALLPolizasCostos(string NroDUI)
        {
            String consulta = "select " +
                                " nrodui, " +
                               " date_format(fechagra,'%d/%m/%Y') as 'fechagra' , " +
                               " horagra , " +
                               " date_format(fechafactura,'%d/%m/%Y') as 'fechafactura' , " +
                               " nitproveedor, " +
                               " nombrerazonsocialproveedor , impbaseparacreditofiscal , creditofiscal , " +
                               " facturacomercial , giroalexterior , proveedoresporpagar , " +
                               " iva_cf_poliza , planillaaduanera , pagoplanillaaduanera , " +
                               " iva_cf_planillaaduanera , valornetoplanillaaduanera , dif_enpago_pa , " +
                               " seguro , prorrateodecostos_transporte_internacional , prorrateodecostos_transporte_nacional , " +
                               " prorrateodecostos_nrofacttransptnaceinter , prorrateodecostos_logisticaparatransporte , " +
                               " prorrateodecostos_nrofactlogistica , prorrateodecostos_mscltda , prorrateodecostos_nrofactmsc , " +
                               " prorrateodecostos_aspb , prorrateodecostos_nrodepositooplanillaaspb , mercaderiaentransito , " +
                               " totalcostopoliza , cantidad , descripciondelproducto , " +
                               " transexpbolivianos_moneda , transexpresadobolivianos_girofacturacomercial , " +
                               " transexpresadobolivianos_comision , transexpresadobolivianos_itf , " +
                               " transexpresadobolivianos_difdecambio , transexpresadobolivianos_total , " +
                               " observaciones  " +
                               " from  " +
                               " tb_polizas_costos pp " +
                               " where  " +
                               " pp.nrodui LIKE '%" + NroDUI + "%'";
            return cnx.consultaMySql(consulta);
        }


    }
}