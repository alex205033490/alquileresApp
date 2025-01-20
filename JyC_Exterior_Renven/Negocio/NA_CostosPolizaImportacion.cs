using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_CostosPolizaImportacion
    {
        DA_CostosPolizaImportacion dcosto = new DA_CostosPolizaImportacion();

        public NA_CostosPolizaImportacion() { }

        public bool insertcostospoliza(string nrodui, string fechafactura, string nitproveedor, string nombrerazonsocialproveedor, float impbaseparacreditofiscal,
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
            return dcosto.insertcostospoliza(nrodui, fechafactura, nitproveedor, nombrerazonsocialproveedor, impbaseparacreditofiscal,
                                        creditofiscal,  facturacomercial,  giroalexterior,
                                        proveedoresporpagar,  iva_cf_poliza,  planillaaduanera,
                                        pagoplanillaaduanera,  iva_cf_planillaaduanera,  valornetoplanillaaduanera,
                                        dif_enpago_pa,  seguro,  prorrateodecostos_transporte_internacional,
                                        prorrateodecostos_transporte_nacional,  prorrateodecostos_nrofacttransptnaceinter,
                                        prorrateodecostos_logisticaparatransporte,  prorrateodecostos_nrofactlogistica,
                                        prorrateodecostos_mscltda,  prorrateodecostos_nrofactmsc,  prorrateodecostos_aspb,
                                        prorrateodecostos_nrodepositooplanillaaspb,  mercaderiaentransito,  totalcostopoliza,
                                        cantidad,  descripciondelproducto,  transexpbolivianos_moneda,
                                        transexpresadobolivianos_girofacturacomercial,  transexpresadobolivianos_comision,
                                        transexpresadobolivianos_itf,  transexpresadobolivianos_difdecambio,
                                        transexpresadobolivianos_total,  observaciones);
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
            return dcosto.updatecostospoliza(nrodui, fechafactura, nitproveedor, nombrerazonsocialproveedor, impbaseparacreditofiscal,
                                       creditofiscal,  facturacomercial,  giroalexterior,
                                       proveedoresporpagar,  iva_cf_poliza,  planillaaduanera,
                                       pagoplanillaaduanera,  iva_cf_planillaaduanera,  valornetoplanillaaduanera,
                                       dif_enpago_pa,  seguro,  prorrateodecostos_transporte_internacional,
                                       prorrateodecostos_transporte_nacional,  prorrateodecostos_nrofacttransptnaceinter,
                                       prorrateodecostos_logisticaparatransporte,  prorrateodecostos_nrofactlogistica,
                                       prorrateodecostos_mscltda,  prorrateodecostos_nrofactmsc,  prorrateodecostos_aspb,
                                       prorrateodecostos_nrodepositooplanillaaspb,  mercaderiaentransito,  totalcostopoliza,
                                       cantidad,  descripciondelproducto,  transexpbolivianos_moneda,
                                       transexpresadobolivianos_girofacturacomercial,  transexpresadobolivianos_comision,
                                       transexpresadobolivianos_itf,  transexpresadobolivianos_difdecambio,
                                       transexpresadobolivianos_total,  observaciones);
        }


        public DataSet get_PolizasCostos(string NroDUI) {
            return dcosto.get_PolizasCostos(NroDUI);
        }

        public DataSet get_ALLPolizasCostos(string NroDUI)
        {
            return dcosto.get_ALLPolizasCostos(NroDUI);
        }

        internal bool existeNroDUI(string NroDUI)
        {
            DataSet tuplas = get_PolizasCostos(NroDUI);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}