using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DEquipo
    {
        private conexionMySql conexion = new conexionMySql();

        public DEquipo() { }

        public bool insertarEquipo(string exbo, string fecha, string fechaActaProvisional, string fechaActaTecnico, string fechaActaDefinitiva,  int codigoActualizacion, int codigoProyecto, string fechaEquipoObra, string fechaEquipoEntregado, string tipologia, int codTipoEquipo, int codmarca, string codFiscalProyecto, string fechaAprobacionLimitePlanos,string modelo,string pasajero,int parada,string velocidad, string fechaAprobacionPlano, string FechaEntregaCliente, string fechaHabilitacionEquipo, string fechaaproximadaembarque, string fechapagoembarque)
        {
            
                if (fechaActaProvisional != "null")
                {
                    fechaActaProvisional = "'" + fechaActaProvisional + "'";
                }

                if (fechaActaTecnico != "null")
                {
                    fechaActaTecnico = "'" + fechaActaTecnico + "'";
                }

                if(fechaActaDefinitiva != "null"){
                fechaActaDefinitiva = "'"+fechaActaDefinitiva+"'";
                }

             
                string codigoActualizacionAux = "null";
                if (codigoActualizacion != -1)
                {
                    codigoActualizacionAux = Convert.ToString(codigoActualizacion);
                }

                if (fechaEquipoObra != "null")
                {
                    fechaEquipoObra = "'"+fechaEquipoObra+"'";
                }

                if (fechaEquipoEntregado != "null")
                {
                    fechaEquipoEntregado = "'"+fechaEquipoEntregado+"'";
                }
                                
                if (fechaAprobacionLimitePlanos != "null")
                {
                    fechaAprobacionLimitePlanos = "'"+fechaAprobacionLimitePlanos+"'";
                }

                string codTipoEquipoAux = "null";
                if(codTipoEquipo != -1){
                    codTipoEquipoAux = Convert.ToString(codTipoEquipo);
                }

                string codMarcaAux = "null";
                if (codmarca != -1)
                {
                    codMarcaAux = Convert.ToString(codmarca);
                }

            if(fechaAprobacionPlano != "null"){
                fechaAprobacionPlano = "'"+fechaAprobacionPlano+"'";
            }

            if(FechaEntregaCliente  != "null"){
                FechaEntregaCliente = "'"+FechaEntregaCliente +"'";
            }

            if (fechaHabilitacionEquipo != "null")               
            {
                fechaHabilitacionEquipo = "'" + fechaHabilitacionEquipo + "'";
            }


                string consulta = "insert into tb_equipo(exbo,fecha,fecha_acta_provicional,fecha_acta_tecnico_ing, "+
                                  " fecha_acta_definitiva,codActualizacion,cod_proyecto,estado, "+
                                  " fecha_equipo_obra,fecha_equipo_entregado,tipologia,codtipoequipo,codmarca,codresponsable,fechaaprobacionlimite_planos,modelo,pasajero,parada,velocidad, fechaaprobacionplano, fechaentregacliente ,  fechahabilitacionequipo, fechaaproximadaembarque, fechapagoembarque) " +
                                  " values ('"+exbo+"','"+fecha+"',"+fechaActaProvisional+","+fechaActaTecnico+", "+
                                  fechaActaDefinitiva+","+codigoActualizacionAux+","+codigoProyecto+",1, "+
                                   fechaEquipoObra + "," + fechaEquipoEntregado + ",'" + tipologia + "'," + codTipoEquipoAux + "," + codMarcaAux + "," + codFiscalProyecto + "," + fechaAprobacionLimitePlanos + ",'" + modelo + "','" + pasajero + "'," + parada + ",'" + velocidad + "'," + fechaAprobacionPlano + "," + FechaEntregaCliente + "," + fechaHabilitacionEquipo + ","+fechaaproximadaembarque+" , "+fechapagoembarque+") ";

                return conexion.ejecutarMySql(consulta);        
         }



        public bool modificarEquipo(int codigo, string fechaActaProvisional, string fechaActaTecnico, string fechaActaDefinitiva, int codigoActualizacion, string fechaEquipoObra, string fechaEquipoEntregado, string tipologia, int codTipoEquipo, int codmarca, string codFiscalProyecto, string fechalimiteAprobacionPlanos, string modelo, string pasajero, int parada, string velocidad, string fechaAprobacionPlano, string fechaEntregaCliente, string fechaHabilitacionEquipo, string fechaaproximadaembarque, string fechapagoembarque, string fechaConfirmacionPagoEmbarque, string CLICODSIMEC) 
        {
            
                if (fechaActaProvisional != "null")
                {
                    fechaActaProvisional = "'" + fechaActaProvisional + "'";
                }
                if (fechaConfirmacionPagoEmbarque != "null") {
                    fechaConfirmacionPagoEmbarque = "'" + fechaConfirmacionPagoEmbarque + "'";
                }

                if (fechaActaTecnico != "null")
                {
                    fechaActaTecnico = "'" + fechaActaTecnico + "'";
                }

                if (fechaActaDefinitiva != "null")
                {
                    fechaActaDefinitiva = "'" + fechaActaDefinitiva + "'";
                }

           
                string codigoActualizacionAux = "null";
                if (codigoActualizacion != -1)
                {
                    codigoActualizacionAux = Convert.ToString(codigoActualizacion);
                }

                if (fechaEquipoObra != "null")
                {
                    fechaEquipoObra = "'" + fechaEquipoObra + "'";
                }

                if (fechaEquipoEntregado != "null")
                {
                    fechaEquipoEntregado = "'" + fechaEquipoEntregado + "'";
                }

                if (fechalimiteAprobacionPlanos != "null")
                {
                    fechalimiteAprobacionPlanos = "'" + fechalimiteAprobacionPlanos + "'";
                }

                string codTipoEquipoAux = "null";
                if (codTipoEquipo != -1)
                {
                    codTipoEquipoAux = Convert.ToString(codTipoEquipo);
                }

                string codMarcaAux = "null";
                if (codmarca != -1)
                {
                    codMarcaAux = Convert.ToString(codmarca);
                }


                if (fechaAprobacionPlano != "null")
                {
                    fechaAprobacionPlano = "'" + fechaAprobacionPlano + "'";
                }

                if (fechaEntregaCliente != "null")
                {
                    fechaEntregaCliente = "'" + fechaEntregaCliente + "'";
                }

                if (fechaHabilitacionEquipo != "null")
                {
                    fechaHabilitacionEquipo = "'" + fechaHabilitacionEquipo + "'";
                }

                if (fechaaproximadaembarque != "null")
                {
                    fechaaproximadaembarque = "'" + fechaaproximadaembarque + "'";
                }

                if (fechapagoembarque != "null")
                {
                    fechapagoembarque = "'" + fechapagoembarque + "'";
                }
                string consulta = "update tb_equipo set  tb_equipo.fecha_acta_provicional = "+fechaActaProvisional+", "+ 
                                  " tb_equipo.fecha_acta_tecnico_ing = "+fechaActaTecnico+", tb_equipo.fecha_acta_definitiva = "+fechaActaDefinitiva+","+
                                  " tb_equipo.fecha_equipo_obra = "+fechaEquipoObra+", tb_equipo.fecha_equipo_entregado = "+fechaEquipoEntregado+", "+
                                  " tb_equipo.codActualizacion = "+codigoActualizacionAux+", "+
                                  " tb_equipo.tipologia = '"+tipologia+"', "+
                                  " tb_equipo.codtipoequipo = "+codTipoEquipoAux+", tb_equipo.codmarca = "+codMarcaAux+
                                  " , tb_equipo.codresponsable = "+codFiscalProyecto+
                                  " , tb_equipo.fechaaprobacionlimite_planos = " + fechalimiteAprobacionPlanos +
                                  " , tb_equipo.modelo='" + modelo + "' " +
                                  " , tb_equipo.pasajero='" + pasajero + "' " +
                                  " , tb_equipo.parada=" + parada +
                                  " , tb_equipo.velocidad='" + velocidad + "' " +
                                  " , tb_equipo.fechaaprobacionplano= "+fechaAprobacionPlano+
                                  " , tb_equipo.fechaentregacliente= "+fechaEntregaCliente+
                                  " , tb_equipo.fechahabilitacionequipo="+fechaHabilitacionEquipo+
                                  " , tb_equipo.fechaaproximadaembarque=" + fechaaproximadaembarque +
                                  " , tb_equipo.fechapagoembarque=" + fechapagoembarque +
                                  " , tb_equipo.fechaconfirmacionpagoembarque="+fechaConfirmacionPagoEmbarque+
                                  " , tb_equipo.clicodigo = '"+CLICODSIMEC+"'"+
                                  " where tb_equipo.codigo = "+codigo;
           return conexion.ejecutarMySql(consulta);
        }
  
        public bool eliminarEquipo1(int codigo) 
        {           
                string consulta = "delete from tb_equipo where tb_equipo.codigo= " + codigo + ";";
                return conexion.ejecutarMySql(consulta);
        }

        public bool ModificarFechaEstadoEquipo(int codEquipo, int CodFechaEstadoEquipo)
        {
            string consulta = "update tb_equipo set tb_equipo.codfechaestadoequipo = " + CodFechaEstadoEquipo +                                
                                " where tb_equipo.codigo= " + codEquipo;
            return conexion.ejecutarMySql(consulta);
        }

        public bool ModificarFechaEstadoEquipo2(int codEquipo, int CodFechaEstadoEquipo, string fechalimiteplanosAprovacion, string fechaAproximadaArriboPuerto)
        {
                if(fechalimiteplanosAprovacion != "null"){
                    fechalimiteplanosAprovacion = "'"+fechalimiteplanosAprovacion+"'";
                }

                if (fechaAproximadaArriboPuerto != "null")
                {
                    fechaAproximadaArriboPuerto = "'" + fechaAproximadaArriboPuerto + "'";
                }
            
                string consulta = "update tb_equipo set tb_equipo.codfechaestadoequipo = " + CodFechaEstadoEquipo + ", "+
                                   " tb_equipo.fechaaprobacionlimite_planos = "+fechalimiteplanosAprovacion+
                                   ",tb_equipo.fechaaproximadoarribopuerto = " + fechaAproximadaArriboPuerto +
                                    " where tb_equipo.codigo= " + codEquipo;
                return conexion.ejecutarMySql(consulta);                          
        }



        public bool actualizar_importacionJYCIA(int codigo, string nrofactura, string fechafactura, float montofactura, string fechagiro, float montogiro1, float montogiro2, float montogiro3, float montogiro4, float montogiro5, float valorfob, float valortransportemaritimo2, string nrocontenedor, string fechagiro2, string fechagiro3 , string fechagiro4, string fechagiro5, bool primerpago,bool segundopago, bool tercerpago){
            string montoFacturaAux = montofactura.ToString().Replace(',', '.');
            string montoGiro1Aux = montogiro1.ToString().Replace(',', '.');
            string montoGiro2Aux = montogiro2.ToString().Replace(',', '.');
            string montoGiro3Aux = montogiro3.ToString().Replace(',', '.');
            string montoGiro4Aux = montogiro4.ToString().Replace(',', '.');
            string montoGiro5Aux = montogiro5.ToString().Replace(',', '.');
            string valorFobAux = valorfob.ToString().Replace(',', '.');
            string valortransportemaritimo2Aux = valortransportemaritimo2.ToString().Replace(',', '.');

            
            string consulta = "update "+
                               " tb_equipo "+
                               " set "+
                               " tb_equipo.nrofactura = '"+nrofactura+"', "+
                               " tb_equipo.fechafactura = "+fechafactura+", "+
                               " tb_equipo.montofactura = "+montoFacturaAux+", "+
                               " tb_equipo.fechagiro = "+fechagiro+", "+
                               " tb_equipo.montogiro1 = "+montoGiro1Aux+", "+
                               " tb_equipo.montogiro2 = "+montoGiro2Aux+", "+
                               " tb_equipo.montogiro3 = "+montoGiro3Aux+", "+ 
                               " tb_equipo.montogiro4 = "+montoGiro4Aux+", "+
                               " tb_equipo.montogiro5 = " + montoGiro5Aux + ", " +
                               " tb_equipo.valorfob = " + valorFobAux+" , "+
                               " tb_equipo.valortransportemaritimo2 = "+ valortransportemaritimo2Aux+ " , "+
                               " tb_equipo.nrocontenedor = '"+ nrocontenedor+ "' , "+
                               " tb_equipo.fechagiro2 = "+fechagiro2+ " , "+
                               " tb_equipo.fechagiro3 = "+fechagiro3+ " , "+
                               " tb_equipo.fechagiro4 = "+fechagiro4+ " , "+
                               " tb_equipo.fechagiro5 = "+fechagiro5+ " , "+
                               " tb_equipo.1erpago = "+primerpago+ " , "+
                               " tb_equipo.2dopago = "+segundopago+" , "+
                               " tb_equipo.3erpago = "+tercerpago+
                               " where  "+
                               " tb_equipo.codigo = "+codigo;
            return conexion.ejecutarMySql(consulta);  
        }



        public DataSet listarEquipo()
        {
           string consulta = "select eq.codigo, eq.exbo as 'Chasis', pro.nombre as 'NombreProyecto', "+
                             " eq.tipologia, teq.nombre as 'TipoEquipo', m.nombre as 'MarcaEquipo' , "+
                             " eeq.nombre as 'EstadoEquipo', eq.codresponsable"+
                             " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'Fecha Acta Provicional', "+
                             " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Fecha Acta Tecnico', "+
                             " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'Fecha Acta Definitiva', "+
                             " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y') as 'Fecha Equipo en Obra', "+
                             " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'Fecha Equipo Entregado' "+
                             " FROM  tb_proyecto pro, tb_equipo eq "+
                             " left join tb_tipoequipo teq on (eq.codtipoequipo = teq.codigo) "+
                             " left join tb_marca m on (eq.codmarca = m.codigo) ,"+                             
                             " tb_fechaestadoequipo feeq, tb_estado_equipo eeq "+
                             " where eq.cod_proyecto = pro.codigo and  eq.estado = 1 and eq.codfechaestadoequipo = feeq.codigo and "+
                             " feeq.codEstadoEquipo = eeq.codigo order by pro.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet cantiListaEquipo2(string exbo, string proyecto, string NombreEstado)
        {
            string consulta = "select " +
                                  "count(*) " +
                                  " FROM  tb_proyecto pro, tb_equipo eq " +
                                  " left join tb_tipoequipo teq on (eq.codtipoequipo = teq.codigo) " +
                                  " left join tb_marca m on (eq.codmarca = m.codigo) ," +
                                  " tb_fechaestadoequipo feeq, tb_estado_equipo eeq " +
                                  " where eq.cod_proyecto = pro.codigo and  eq.estado = 1 and eq.codfechaestadoequipo = feeq.codigo and " +
                                  " feeq.codEstadoEquipo = eeq.codigo " +
                                  " and eq.exbo like '%" + exbo + "%' and pro.nombre like '%" + proyecto + "%' and eeq.nombre like '%" + NombreEstado + "%' ";                                  

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarEquipo2(string exbo, string proyecto, string NombreEstado, bool exportar)
        {
            string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo as 'Chasis', "+
                               " pro.nombre as 'NombreProyecto', "+
                               " eq.tipologia as 'tipologia' , "+
                               " eq.modelo, "+
                               " eq.parada, "+
                               " eq.pasajero, "+
                               " eq.velocidad, "+
                               " teq.nombre as 'TipoEquipo', "+
                               " m.nombre as 'MarcaEquipo' ,  "+
                               " eeq.nombre as 'EstadoEquipo', "+
                               " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y') as 'Fecha Equipo en Obra (R-114)', "+
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'Fecha Acta Provicional (R-115)', "+
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Fecha Acta Tecnico (R-117)',  "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'Fecha Acta Definitiva (R-118.1)', "+ 
                               " DATE_FORMAT(eq.fechahabilitacionequipo, '%d/%m/%Y') as 'Fecha Habilitacion Equipo (R-118.2)' , "+
                               " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'Fecha Equipo Entregado Segun Contrato', "+
                               " DATE_FORMAT(eq.fechaaprobacionlimite_planos,'%d/%m/%Y') as 'FechaLimitePlanosFabrica', "+
                               " DATE_FORMAT(eq.fechaaprobacionplano,'%d/%m/%Y') as 'Fecha Aprobacion Plano', "+
                               " DATE_FORMAT(eq.fechaentregacliente , '%d/%m/%Y') as 'Fecha Entrega al Cliente', "+
                               " DATE_FORMAT(eq.fechaaproximadaembarque , '%d/%m/%Y') as 'Fecha Aprox. Embarque', " +
                               " DATE_FORMAT(eq.fechapagoembarque , '%d/%m/%Y') as 'Fecha Pago Embarque Segun Contrato', " +
                               " DATE_FORMAT(eq.fechaconfirmacionpagoembarque , '%d/%m/%Y') as 'Fecha Confirmacion Pago Embarque', " + 
                               " eq.codresponsable, "+
                               " rrin.nombre as 'Rin', "+
                               " rrcc.nombre as 'RCC', "+
                               " rtec.nombre as 'TecMantenimiento', "+
                               " rsup.nombre as 'Supervisor'  "+
                               " ,eq.clicodigo as 'CLICODIGO_SIMEC' "+
                               " FROM  tb_proyecto pro, tb_equipo eq " +
                               " left join tb_responsable rrin on (eq.cod_rin = rrin.codigo) "+
                               " left join tb_responsable rrcc on (eq.cod_rcc = rrcc.codigo) "+
                               " left join tb_responsable rsup on (eq.cod_supervisor = rsup.codigo) "+
                               " left join tb_responsable rtec on (eq.cod_tecmantenimiento  = rtec.codigo) "+
                              " left join tb_tipoequipo teq on (eq.codtipoequipo = teq.codigo) " +
                              " left join tb_marca m on (eq.codmarca = m.codigo) ," +
                              " tb_fechaestadoequipo feeq, tb_estado_equipo eeq " +                              
                              " where eq.cod_proyecto = pro.codigo and  eq.estado = 1 and eq.codfechaestadoequipo = feeq.codigo and "+
                              " feeq.codEstadoEquipo = eeq.codigo "+
                              " and eq.exbo like '%" + exbo + "%' and pro.nombre like '%" + proyecto + "%' and eeq.nombre like '%"+NombreEstado+"%' " +
                              " order by pro.nombre asc";
            if(exportar == false){
                consulta = consulta + " LIMIT 50";
            }

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarEquipo2ConFiscalProyecto(string exbo, string proyecto, int codFiscal, string NombreEstado, bool exportar)
        {
            string consulta = "select "+
                               " eq.codigo,  "+
                               " eq.exbo as 'Chasis',  "+
                               " pro.nombre as 'NombreProyecto',  "+
                               " eq.tipologia as 'tipologia' , "+
                               " eq.modelo,  "+
                               " eq.parada,  "+
                               " eq.pasajero,  "+
                               " eq.velocidad, "+
                               " teq.nombre as 'TipoEquipo',  "+
                               " m.nombre as 'MarcaEquipo' , "+
                               " eeq.nombre as 'EstadoEquipo', "+
                               " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y') as 'Fecha Equipo en Obra (R-114)', "+
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'Fecha Acta Provicional (R-115)', "+
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Fecha Acta Tecnico (R-117)', "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'Fecha Acta Definitiva (R-118.1)', "+ 
                               " DATE_FORMAT(eq.fechahabilitacionequipo, '%d/%m/%Y') as 'Fecha Habilitacion Equipo (R-118.2)' , "+
                               " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'Fecha Equipo Entregado Segun Contrato', "+
                               " DATE_FORMAT(eq.fechaaprobacionlimite_planos,'%d/%m/%Y') as 'FechaLimitePlanosFabrica', "+
                               " DATE_FORMAT(eq.fechaaprobacionplano,'%d/%m/%Y') as 'Fecha Aprobacion Plano', "+
                               " DATE_FORMAT(eq.fechaentregacliente , '%d/%m/%Y') as 'Fecha Entrega al Cliente', "+
                               " DATE_FORMAT(eq.fechaaproximadaembarque , '%d/%m/%Y') as 'Fecha Aprox. Embarque', " +
                               " DATE_FORMAT(eq.fechapagoembarque , '%d/%m/%Y') as 'Fecha Pago Embarque', " +
                               " DATE_FORMAT(eq.fechaconfirmacionpagoembarque , '%d/%m/%Y') as 'Fecha Confirmacion Pago Embarque', " + 
                               " eq.codresponsable, " +                                                              
                               " rrin.nombre as 'Rin', "+
                               " rrcc.nombre as 'RCC', "+
                               " rtec.nombre as 'TecMantenimiento', "+
                               " rsup.nombre as 'Supervisor'  "+
                               " ,eq.clicodigo as 'CLICODIGO_SIMEC' " +
                              " FROM  tb_proyecto pro, tb_equipo eq " +
                               " left join tb_responsable rrin on (eq.cod_rin = rrin.codigo) "+
                               " left join tb_responsable rrcc on (eq.cod_rcc = rrcc.codigo) "+
                               " left join tb_responsable rsup on (eq.cod_supervisor = rsup.codigo) "+
                               " left join tb_responsable rtec on (eq.cod_tecmantenimiento  = rtec.codigo)  "+
                              " left join tb_tipoequipo teq on (eq.codtipoequipo = teq.codigo) " +
                              " left join tb_marca m on (eq.codmarca = m.codigo) ," +
                              " tb_fechaestadoequipo feeq, tb_estado_equipo eeq " +
                              " where eq.cod_proyecto = pro.codigo and  eq.estado = 1 and eq.codfechaestadoequipo = feeq.codigo and " +
                              " feeq.codEstadoEquipo = eeq.codigo and eq.codresponsable = "+codFiscal+
                              " and eq.exbo like '%" + exbo + "%' and pro.nombre like '%" + proyecto + "%' and eeq.nombre like '%"+NombreEstado+"%' " +
                              " order by pro.nombre asc";

            if (exportar == false)
            {
                consulta = consulta + " LIMIT 50";
            }
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscarEquipo(string exbo) 
        {
            string consulta = "select tb_equipo.codigo, tb_equipo.exbo,  DATE_FORMAT(tb_equipo.fecha,'%d/%m/%Y') as fecha, DATE_FORMAT(tb_equipo.fecha_acta_provicional,'%d/%m/%Y') as fechaActaProvisional, DATE_FORMAT(tb_equipo.fecha_acta_tecnico_ing,'%d/%m/%Y') as fechaActaTecnico, DATE_FORMAT(tb_equipo.fecha_acta_definitiva,'%d/%m/%Y') as fechaActaDefinitiva, tb_equipo.codEstado, tb_equipo.codActualizacion, tb_equipo.cod_proyecto from tb_equipo where tb_equipo.exbo like '12345';";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarTecnicoManteniento() 
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 5 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
        
        public DataSet listarTecnicoInstalador()
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 6 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarSupervisorTecnico()
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 7 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarCobrador() 
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 8 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarEncargadoCobro()
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 9 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet getEquipo(int codigoEquipo) {
            string consulta = "select eq.codigo,eq.exbo, eq.tipologia, "+
                               " DATE_FORMAT(eq.fecha,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y'), "+
                               " feq.codEstadoEquipo, eq.codActualizacion, eq.cod_proyecto, "+
                               " eq.codtipoequipo, eq.codmarca , eq.codresponsable , "+
                               " DATE_FORMAT(eq.fechaaprobacionlimite_planos,'%d/%m/%Y'), "+
                               " eq.modelo,eq.pasajero,eq.parada,eq.velocidad ,"+
                               " DATE_FORMAT(eq.fechaaprobacionplano,'%d/%m/%Y') as 'Fecha Aprobacion Plano', " +
                               " DATE_FORMAT(eq.fechaentregacliente , '%d/%m/%Y') as 'Fecha Entrega al Cliente', " +
                               " DATE_FORMAT(eq.fechahabilitacionequipo, '%d/%m/%Y') as 'Fecha Habilitacion Equipo', " +
                               " DATE_FORMAT(eq.fechaaproximadaembarque, '%d/%m/%Y') as 'Fecha Aprox. Embarque', " +
                               " DATE_FORMAT(eq.fechapagoembarque, '%d/%m/%Y') as 'Fecha Pago Embarque', " +
                               " DATE_FORMAT(eq.fechaconfirmacionpagoembarque, '%d/%m/%Y') as 'Fecha Confirmacion Pago Embarque', "+
                               " eq.clicodigo "+
                               " from tb_equipo eq , tb_fechaestadoequipo feq where "+
                               " eq.codfechaestadoequipo = feq.codigo and "+
                               " eq.codigo = "+codigoEquipo;
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public int ultimoinsertado()
        {
            try
            {
                string consulta = "SELECT MAX(segui.codigo) FROM  tb_equipo segui";
                DataSet datoResul = conexion.consultaMySql(consulta);
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public int getCodigoEstadoEquipo_FechaEstado(int codigoFechaEstadoEquipo)
        {
            try
            {
            string consulta = " select feeq.codEstadoEquipo from tb_fechaestadoequipo feeq where feeq.codigo = "+ codigoFechaEstadoEquipo;
            DataSet datoResul = conexion.consultaMySql(consulta);
            int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
            return codUltimo;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = conexion.consultaMySql(consulta);
            return datosR;
        }

        ////-------------------------------------------

        public DataSet listarControlPedido()
        {
            string consulta = "SELECT date_format(e.fechaventa, '%d/%m/%Y') as fechaventa,p.nombre as Proyecto, e.exbo as Exbo, estadoe.nombre as 'Estado Equipo' , " +
                               " ep.nombre as EncargadoPago, "+
                               " e.tipologia, p.departamento, e.r110, e.r148, e.r106, e.r107, e.r109, "+
                               " e.r113, e.primerpago, e.pagocontrato, e.fichero, e.ventacontrato " +
                               " FROM tb_proyecto p  "+
                               " left join tb_equipo e ON e.cod_proyecto = p.codigo "+
                               " left join tb_encargado_pago ep ON ep.codigo = p.codEncargado "+
                               " left join tb_fechaestadoequipo feq on e.codfechaestadoequipo = feq.codigo "+
                               " left join tb_estado_equipo estadoe on feq.codEstadoEquipo = estadoe.codigo "+
                               " ORDER BY e.fechaventa desc, p.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public bool insertarEquipoControlPedido(string exbo, string fecha, int codigoProyecto, bool r110, bool r148, bool r106, bool r107, bool r109, bool r113, bool ventaSistema, float primerPago, string tipologia, string fichero, string fechaventa, bool ventacontrato, string modelo, int parada, string pasajero, string velocidad, string vc, int codTipoEquipo, int codmarca, string ciudadVenta, string ciudadinstalacion, string fechaAproxEmbarque, float valorTransporteMaritimo, bool inventario, string fechapagoembarque, string fechaEquipoSegunContrato, string codigoContrato,  string consignatario, bool contratofirmado)
        {
                string primerPagoaux = primerPago.ToString().Replace(',', '.');
                string valorTransporteMaritimoAux = valorTransporteMaritimo.ToString().Replace(',', '.');
                
                string codTipoEquipoAux = "null";
                if (codTipoEquipo != -1)
                {
                    codTipoEquipoAux = Convert.ToString(codTipoEquipo);
                }

                string codMarcaAux = "null";
                if (codmarca != -1)
                {
                    codMarcaAux = Convert.ToString(codmarca);
                }
                string consulta = "insert into tb_equipo(exbo,fecha, cod_proyecto, estado, " +
                                      "r110, r148, r106, r107, r109, r113, primerpago, pagocontrato, tipologia, fichero,fechaventa,ventacontrato,modelo,parada,pasajero,velocidad,vc,codtipoequipo,codmarca,vendidoenciudad,instaladoenciudad, "+
                                      " fechaaproximadaembarque, valorcfrtransportemaritimo, inventario, fechapagoembarque, fecha_equipo_entregado, codigocontrato, consignatario, contratofirmado) " +
                                      " values ('" + exbo + "', '" + fecha + "', " + codigoProyecto + ", " + 1 + ", " + r110 + ", " + r148 + ", " + r106 + ", " + r107 + ", " + r109 + ", " + r113 + ", " + ventaSistema + ", " + primerPagoaux + ",'" + tipologia + "','" + fichero + "'," + fechaventa + "," + ventacontrato + ",'" + modelo + "'," + parada + ",'" + pasajero + "','" + velocidad + "','" + vc + "'," + codTipoEquipoAux + "," + codMarcaAux + ",'" + ciudadVenta + "','" + ciudadinstalacion + "'," + fechaAproxEmbarque + "," + valorTransporteMaritimoAux + "," + inventario + "," + fechapagoembarque + ", "+fechaEquipoSegunContrato+" , '"+codigoContrato +"' , '"+ consignatario+ "' , " +contratofirmado+" ) ";
                return conexion.ejecutarMySql(consulta);
        }

        public int primerEstadoInsertado()
        {
            try
            {
                string consulta = "SELECT min(p.`codigo`) FROM `tb_proyecto` p WHERE p.`estado`=1";
                DataSet datoResul = conexion.consultaMySql(consulta);
                int codPrimero = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codPrimero;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int getCodigoEquipo(string exbo)
        {
            try
            {
                string consulta = " select e.codigo from tb_equipo e where e.exbo = '" + exbo + "' ";
                DataSet datoResul = conexion.consultaMySql(consulta);
                int codPrimero = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codPrimero;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool modificarEquipoControlPedido(int codigo, string exbo, string tipologia, bool r110, bool r148, bool r106, bool r107, bool r109, bool r113, bool ventaSistema, float primerPago, int codigoProyecto, string fichero, string fechaventa, bool ventacontrato, string modelo, int parada, string pasajero, string velocidad, string vc, int codTipoEquipo, int codmarca, string ciudadVenta, string ciudadinstalacion, string fechaAproxEmbarque, float valorTransporteMaritimo, bool inventario, int estado, string fechapagoembarque, string fechaEquipoSegunContrato, string codigoContrato,  string consignatario, bool contratofirmado)
        {
            string codTipoEquipoAux = "null";
            if (codTipoEquipo != -1)
            {
                codTipoEquipoAux = Convert.ToString(codTipoEquipo);
            }

            string codMarcaAux = "null";
            if (codmarca != -1)
            {
                codMarcaAux = Convert.ToString(codmarca);
            }
                string primerPagoaux = primerPago.ToString().Replace(',', '.');
                string valorTransporteMaritimoAux = valorTransporteMaritimo.ToString().Replace(',', '.');

                string consulta = " update tb_equipo set tb_equipo.exbo= '" + exbo + "', " +
                                  " tb_equipo.tipologia = '" + tipologia + "', " +
                                  " tb_equipo.r110 = " + r110 + ", tb_equipo.r148 = " + r148 + ", " +
                                  " tb_equipo.r106 = " + r106 + ", tb_equipo.r107 = " + r107 + ", " +
                                  " tb_equipo.r109 = " + r109 + ", tb_equipo.r113 = " + r113 + ", " +
                                  " tb_equipo.primerpago = " + ventaSistema + ", tb_equipo.pagocontrato = " + primerPagoaux + ", " +
                                  " tb_equipo.cod_proyecto = " + codigoProyecto + " , fichero = '" + fichero + "' ," +
                                  " tb_equipo.fechaventa = " + fechaventa + ", tb_equipo.ventacontrato = " + ventacontrato + ", " +
                                  " tb_equipo.modelo = '" + modelo + "', tb_equipo.parada= " + parada + ", tb_equipo.pasajero='" + pasajero + "', tb_equipo.velocidad='" + velocidad + "' " +
                                  " ,tb_equipo.vc = '"+vc+"'"+
                                  " ,tb_equipo.codtipoequipo = " + codTipoEquipoAux + " , tb_equipo.codmarca = " + codMarcaAux + 
                                  " ,tb_equipo.vendidoenciudad = '"+ciudadVenta+"' , tb_equipo.instaladoenciudad = '"+ciudadinstalacion+"' "+
                                  " ,tb_equipo.fechaaproximadaembarque = " + fechaAproxEmbarque + " , tb_equipo.valorcfrtransportemaritimo = " + valorTransporteMaritimoAux + 
                                  " , tb_equipo.inventario = "+inventario+
                                  " , tb_equipo.estado = "+estado+
                                  " , tb_equipo.fechapagoembarque = " + fechapagoembarque +
                                  " ,tb_equipo.fecha_equipo_entregado = "+fechaEquipoSegunContrato+
                                  " ,tb_equipo.contratofirmado = "+contratofirmado+ 
                                  " ,tb_equipo.codigocontrato = '"+codigoContrato+"'"+
                                  " ,tb_equipo.consignatario = '"+consignatario+"'"+
                                  " where tb_equipo.codigo = " + codigo;
                return conexion.ejecutarMySql(consulta);
        }


        public DataSet buscador(string exbo)
        {
            string consulta = "SELECT e.exbo FROM tb_equipo e WHERE e.estado = 1 AND e.exbo LIKE '%" + exbo + "%' ORDER BY e.exbo ASC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscadorNombreExbo(string exbo, string edificio)
        {
            string consulta = "SELECT e.codigo, e.exbo "+
                               " FROM tb_equipo e, tb_proyecto pp "+
                               " WHERE "+
                               " e.cod_proyecto = pp.codigo and "+
                               " e.estado = 1 AND "+
                               " e.exbo LIKE '%"+exbo+"%' and "+
                               " pp.nombre like '%"+edificio+"%' "+
                               " ORDER BY e.exbo ASC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscadorNombreExbo2(string edificio)
        {
            string consulta = "SELECT e.codigo, e.exbo " +
                               " FROM tb_equipo e, tb_proyecto pp " +
                               " WHERE " +
                               " e.cod_proyecto = pp.codigo and " +
                               " e.estado = 1 AND " +                               
                               " pp.nombre = '" + edificio + "' " +
                               " ORDER BY e.exbo ASC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public int obtenerValor(int codigoEquipo, int nro)
        {
            string consulta = "";
            try
            {
                switch (nro)
                {
                    case 1:
                        consulta = "SELECT e.`r110` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo  ;
                        break;
                    case 2:
                        consulta = "SELECT e.`r148` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 3:
                        consulta = "SELECT e.`r106` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 4:
                        consulta = "SELECT e.`r107` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 5:
                        consulta = "SELECT e.`r109` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 6:
                        consulta = "SELECT e.`r113` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 7:
                        consulta = "SELECT e.`primerpago` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 8:
                        consulta = "SELECT e.`ventacontrato` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 9:
                        consulta = "SELECT e.`inventario` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo;
                        break;
                    case 10:
                        consulta = "SELECT e.`contratofirmado` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo;
                        break;
                }

                DataSet datoResul = conexion.consultaMySql(consulta);
                int lista = Convert.ToInt32(datoResul.Tables[0].Rows[0][0]);
                return lista;
            }
            catch (Exception)
            {
                return -1;
            }

        }
/*
        public DataSet BuscarControlEquipos(string nombreProyecto, string exbo, string nombrePropietario, string pasajero, string parada, string modelo, string velocidad) {
           string consulta = "SELECT date_format(e.fechaventa, '%d/%m/%Y') as fechaventa,p.nombre as Proyecto, e.exbo as Exbo, estadoe.nombre as 'Estado Equipo' , " +
                              " ep.nombre as Propietario, " +
                              " e.tipologia, p.departamento, e.r110, e.r148, e.r106, e.r107, e.r109, " +
                              " e.r113, e.primerpago, e.pagocontrato, e.fichero, e.ventacontrato, e.codigo as CodEstado, " +
                              " e.modelo,e.parada,e.pasajero,e.velocidad "+
                              " FROM tb_proyecto p  " +
                              " left join tb_equipo e ON e.cod_proyecto = p.codigo " +
                              " left join tb_propietario ep ON ep.codigo = p.codpropietario " +
                              " left join tb_fechaestadoequipo feq on e.codfechaestadoequipo = feq.codigo " +
                              " left join tb_estado_equipo estadoe on feq.codEstadoEquipo = estadoe.codigo " +
                              " where p.nombre like '%"+nombreProyecto+"%' and e.exbo like '%"+exbo+"%' " +
                              " ORDER BY e.fechaventa desc, p.nombre asc";
           

            string consulta = "SELECT date_format(e.fechaventa, '%d/%m/%Y') as fechaventa,p.nombre as Proyecto, e.exbo as Exbo, estadoe.nombre as 'Estado Equipo' , " +
                               " ep.nombre as Propietario, " +
                               " e.tipologia, p.departamento, e.r110, e.r148, e.r106, e.r107, e.r109, " +
                               " e.r113, e.primerpago, e.pagocontrato, e.fichero, e.ventacontrato, e.codigo as CodEstado, " +
                               " e.modelo,e.parada,e.pasajero,e.velocidad " +
                               " FROM tb_proyecto p  " +
                               " left join tb_equipo e ON e.cod_proyecto = p.codigo " +
                               " left join tb_propietario ep ON ep.codigo = p.codpropietario " +
                               " left join tb_fechaestadoequipo feq on e.codfechaestadoequipo = feq.codigo " +
                               " left join tb_estado_equipo estadoe on feq.codEstadoEquipo = estadoe.codigo " +
                               " where p.nombre like '%"+nombreProyecto+"%' and e.exbo like '%"+exbo+"%' and ep.nombre like '%"+nombrePropietario+"%' and " +
                               " e.pasajero like '%"+pasajero+"%' and e.parada like '%"+parada+"%' and e.modelo like '%"+modelo+"%' and e.velocidad like '%"+velocidad+"%' " +
                               " ORDER BY e.fechaventa desc, p.nombre asc;";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        
        }*/

       public DataSet BuscarControlEquipos2(string nombreProyecto, string exbo, string nombrePropietario, string pasajero, string parada, string modelo, string velocidad, string fechaDesde, string fechahasta, string fichero)
        {

            string consulta = "SELECT date_format(e.fechaventa, '%d/%m/%Y') as fechaventa,p.nombre as Proyecto, e.exbo as Exbo, estadoe.nombre as 'Estado Equipo' , " +
                               " ep.nombre as Propietario, " +
                               " e.tipologia, p.departamento, e.r110, e.r148, e.r106, e.r107, e.r109, " +
                               " e.r113, e.primerpago, e.pagocontrato, e.fichero, e.ventacontrato, e.codigo as CodEstado, " +
                               " e.modelo,e.parada,e.pasajero,e.velocidad,e.vc " +
                               " ,tie.nombre as 'TipoEquipo', "+
                               " m.nombre as 'Marca' "+
                               " ,e.instaladoenciudad,e.vendidoenciudad,"+
                               " e.valorcfrtransportemaritimo , " +
                               " date_format(e.fechaaproximadaembarque, '%d/%m/%Y') as fechaAproxEmbarque " +    
                               " ,e.inventario "+
                               " , date_format(e.fechapagoembarque, '%d/%m/%Y') as fechaPagoEmbarque" +
                               " ,date_format(e.fecha_equipo_entregado, '%d/%m/%Y') as 'Equipo_Entregado_Segun_Contrato', " +
                               "  e.contratofirmado, e.codigocontrato, e.consignatario "+
                               " FROM tb_equipo e "+
                               " left join tb_tipoequipo tie on e.codtipoequipo = tie.codigo "+
                               " left join tb_marca m on e.codmarca = m.codigo "+
                               " ,tb_fechaestadoequipo feq, tb_estado_equipo estadoe, tb_proyecto p "+
                               " left join tb_propietario ep ON ep.codigo = p.codpropietario  "+
                               " where "+
                               " e.cod_proyecto = p.codigo and "+
                               " e.codfechaestadoequipo = feq.codigo and "+
                               " feq.codEstadoEquipo = estadoe.codigo and "+
                               " p.nombre like '%"+nombreProyecto+"%'" ;
                                
           if(exbo != ""){
           consulta = consulta + " and e.exbo like '%" + exbo + "%' ";
           }

            if (fichero != "")
            {
           consulta = consulta + " and e.fichero like '%"+fichero+"%' ";
           }

           if(nombrePropietario != ""){
           consulta = consulta + " and ep.nombre like '%" + nombrePropietario + "%' ";
           }
           if(pasajero != ""){
               consulta = consulta + "  and e.pasajero like '%" + pasajero + "%' ";
           }
           if(parada != ""){
               consulta = consulta + " and e.parada like '%" + parada + "%' ";
           }
           if(modelo != ""){
               consulta = consulta + " and e.modelo like '%" + modelo + "%' ";       
           }
           if(velocidad != ""){
               consulta = consulta + " and e.velocidad like '%" + velocidad + "%' "; 
           }
           if (fechaDesde != "null" && fechahasta != "null") {
               consulta = consulta + " and e.fechaventa BETWEEN " + fechaDesde + " and " + fechahasta + " ";
           }

           consulta = consulta + " ORDER BY e.fechaventa desc, p.nombre asc;";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;

        }

       public DataSet getEquipoJYC_pagos(int codigoEquipo)
       {
           string consulta = "select eq.1erpago, eq.2dopago, eq.3erpago from tb_equipo eq where eq.codigo = " + codigoEquipo;
           DataSet lista = conexion.consultaMySql(consulta);
           return lista;
       }

       public DataSet Buscar_ImportacionJYCIA(string Edificio, string exbo, string nrofactura, string fechafactura, string montofactura, string fechagiro, string montogiro1, string montogiro2, string montogiro3, string montogiro4, string montogiro5)
       {
           string consulta = "select eq.codigo, "+
                               " proy.nombre as 'Edificio',  "+
                               " eq.exbo, "+
                               " ee.nombre as 'tipoEquipo', "+
                               " mm.nombre as 'Marca', "+
                               " eq.parada, "+
                               " eq.pasajero, "+
                               " eq.modelo, "+
                               " eq.velocidad, "+
                               " eq.vc as 'Valor Fob (Alvaro)', " +
                               " eq.valorcfrtransportemaritimo as 'Transporte Maritimo (Alvaro)', "+
                               " date_format(eq.fechaaproximadaembarque,'%d/%m/%Y') as 'Fecha Aprox. Embarque', "+
                               " date_format(eq.fecha,'%d/%m/%Y') as 'Fecha Equipo',   " +
                               " eq.nrofactura, "+                               
                               " date_format(eq.fechafactura, '%d/%m/%Y') as 'Fecha Factura',  "+
                               " eq.montofactura,  "+
                               " date_format(eq.fechagiro, '%d/%m/%Y') as 'Fecha Giro1',  "+
                               " eq.montogiro1,  "+
                               " date_format(eq.fechagiro2, '%d/%m/%Y') as 'Fecha Giro2'," +
                               " eq.montogiro2, "+
                               " date_format(eq.fechagiro3, '%d/%m/%Y') as 'Fecha Giro3'," +
                               " eq.montogiro3,  "+
                               " date_format(eq.fechagiro4, '%d/%m/%Y') as 'Fecha Giro4'," +
                               " eq.montogiro4,  "+
                               " date_format(eq.fechagiro5, '%d/%m/%Y') as 'Fecha Giro5'," +
                               " eq.montogiro5, "+
                               " eq.valorfob, "+
                               " eq.valortransportemaritimo2, "+
                               " eq.nrocontenedor, "+
                               " eq.1erpago, "+
                               " eq.2dopago, "+
                               " eq.3erpago "+                               
                               " from  tb_proyecto proy, "+
                               " tb_equipo eq "+
                               " left join tb_tipoequipo ee on eq.codtipoequipo = ee.codigo  "+
                               " left join tb_marca mm on eq.codmarca = mm.codigo "+
                               " where   "+
                               " eq.cod_proyecto = proy.codigo  "; 
           
           if(!Edificio.Equals("")){
               consulta = consulta + " and proy.nombre like '%" + Edificio + "%'";
           }
           if(!exbo.Equals("")){
               consulta = consulta + " and eq.exbo like '%" + exbo + "%'";
           }
           if (!nrofactura.Equals("") && !nrofactura.Equals("0"))
           {
               consulta = consulta + " and eq.nrofactura = '"+nrofactura+"'";
           }
           if (!fechafactura.Equals("") && !fechafactura.Equals("null"))
           {
               consulta = consulta + " and eq.fechafactura = " + fechafactura;
           }
           if (!montofactura.Equals("") && !montofactura.Equals("0"))
           {
               consulta = consulta + " and eq.montofactura = " + montofactura;
           }
           if (!fechagiro.Equals("") && !fechagiro.Equals("null"))
           {
               consulta = consulta + " and eq.fechagiro = " + fechagiro;
           }
           if (!montogiro1.Equals("") && !montogiro1.Equals("0"))
           {
            consulta = consulta + " and eq.montogiro1 = "+montogiro1;
           }
           if (!montogiro2.Equals("") && !montogiro2.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro2 = " + montogiro2;
           }
           if (!montogiro3.Equals("") && !montogiro3.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro3 = " + montogiro3;
           }
           if (!montogiro4.Equals("") && !montogiro4.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro4 = " + montogiro4;
           }
           if (!montogiro5.Equals("") && !montogiro5.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro5 = " + montogiro5;
           }

           consulta = consulta + " order by eq.fecha desc";

           DataSet lista = conexion.consultaMySql(consulta);
           return lista;
       } 

       public int getCodigoEstado_Actual(int codEquipo)
       {
           try
           {
               string consulta = "  select  "+
                                    " ff.codEstadoEquipo "+
                                    " from tb_equipo eq , tb_fechaestadoequipo ff "+
                                    " where  "+
                                    " eq.codfechaestadoequipo = ff.codigo and "+
                                    " eq.codigo = " + codEquipo;
               DataSet datoResul = conexion.consultaMySql(consulta);
               int codEstado = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
               return codEstado;
           }
           catch (Exception )
           {
               return -1;
           }

       }



       internal bool modificarEquipo(int codEquipo, int parada, string pasajeros, string velocidad, string modelo)
       {
           string consulta = "update tb_equipo set tb_equipo.parada = "+parada+", tb_equipo.pasajero = '"+pasajeros+"', tb_equipo.velocidad = '"+velocidad+"', tb_equipo.modelo = '"+modelo+"' where tb_equipo.codigo = "+codEquipo;
           return conexion.ejecutarMySql(consulta);
       }

       public string getFechaAproxArriboPuerto(string exbo) {
           string consulta = "select date_format(eq.fechaaproximadoarribopuerto,'%d/%m/%Y') as fechaAproxPuerto from tb_equipo eq where eq.exbo = '" + exbo + "'";
           DataSet dato = conexion.consultaMySql(consulta);
           string fecha = "";
           if(dato.Tables[0].Rows.Count > 0){
               fecha = dato.Tables[0].Rows[0][0].ToString();
           }
           return fecha;
       }

       public DataSet getListaMaestraEquipos() {
           string consulta = "select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'Santa Cruz' as Ciudad "+
                           " from db_seguimientoscz_jyc.tb_proyecto proy, db_seguimientoscz_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto and "+
                           " eq.instaladoenciudad = 'Santa Cruz' "+
                           " union  "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Cochabamba' as Ciudad "+
                           " from db_seguimientocbba_jyc.tb_proyecto proy, db_seguimientocbba_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Cochabamba' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'La Paz' as Ciudad "+
                           " from db_seguimientolpz_jyc.tb_proyecto proy, db_seguimientolpz_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'La Paz' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'Sucre' as Ciudad "+
                           " from db_seguimientosucre_jyc.tb_proyecto proy, db_seguimientosucre_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Sucre' "+
                           " union  "+
                           " select  "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Tarija' as Ciudad "+
                           " from db_seguimientotarija_jyc.tb_proyecto proy, db_seguimientotarija_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Tarija' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'Beni' as Ciudad "+
                           " from db_seguimientobeni_jyc.tb_proyecto proy, db_seguimientobeni_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Beni' "+
                           " union  "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Potosi' as Ciudad "+
                           " from db_seguimientopotosi_jyc.tb_proyecto proy, db_seguimientopotosi_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto  "+
                           " and eq.instaladoenciudad = 'Potosi' "+
                           " union  "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Oruro' as Ciudad "+
                           " from db_seguimientooruro_jyc.tb_proyecto proy, db_seguimientooruro_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Oruro' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'Pando' as Ciudad "+
                           " from db_seguimientopando_jyc.tb_proyecto proy, db_seguimientopando_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Pando' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Villamontes' as Ciudad "+
                           " from db_seguimientovillamontes_jyc.tb_proyecto proy, db_seguimientovillamontes_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Villamontes' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Yacuiba' as Ciudad "+
                           " from db_seguimientoyacuiba_jyc.tb_proyecto proy, db_seguimientoyacuiba_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Yacuiba'";
           return conexion.consultaMySql(consulta);
       }


       public DataSet getConsultaCodigoDeAutenticacion(string exbo, string edificio)
       {
           string consulta = "select  " +
                               " proy.nombre as 'Edificio', " +
                               " proy.direccion, " +
                               " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'Exbo', " +
                               " eq.vendidoenciudad as 'Vendido', " +
                               " eq.instaladoenciudad as 'Instalado', " +
                               " mm.nombre as 'Marca', " +
                               " teq.nombre as 'Tipo', " +
                               " eq.parada, " +
                               " eq.pasajero, " +
                               " eq.velocidad, " +
                               " eq.modelo " +
                               " from tb_proyecto proy, " +
                               " tb_equipo eq  " +
                               " LEFT JOIN tb_marca mm ON (eq.codmarca = mm.codigo) " +
                               " LEFT JOIN tb_tipoequipo teq ON (eq.codtipoequipo = teq.codigo) " +
                               " where " +
                               " proy.codigo = eq.cod_proyecto and " +
                               " eq.estado = 1 and " +
                               " proy.nombre like '%" + edificio + "%' and " +
                               " eq.exbo like '%" + exbo + "%'";
           return conexion.consultaMySql(consulta);
       }


       public DataSet getConsultaCodigoDeAutenticacion_QR(string exbo, string edificio, string dirArchivo)
       {
           string consulta = "select  " +
                               " proy.nombre as 'Edificio', " +
                               " proy.direccion, " +
                               " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'Exbo', " +
                               " eq.vendidoenciudad as 'Vendido', " +
                               " eq.instaladoenciudad as 'Instalado', " +
                               " mm.nombre as 'Marca', " +
                               " teq.nombre as 'Tipo', " +
                               " eq.parada, " +
                               " eq.pasajero, " +
                               " eq.velocidad, " +
                               " eq.modelo , " +
                               " eq.qr_equipo, " +
                               " CAST(concat(eq.codigo,'_',eq.exbo,'_',proy.nombre) AS CHAR) as 'QR_nombreArchivo', " +
                               " CAST(concat('" + dirArchivo + "',eq.codigo,'_',eq.exbo,'_',proy.nombre,'.jpg') AS CHAR) as 'QR_DirNombreArchivo' " +
                               " from tb_proyecto proy, " +
                               " tb_equipo eq  " +
                               " LEFT JOIN tb_marca mm ON (eq.codmarca = mm.codigo) " +
                               " LEFT JOIN tb_tipoequipo teq ON (eq.codtipoequipo = teq.codigo) " +
                               " where " +
                               " proy.codigo = eq.cod_proyecto and " +
                               " eq.estado = 1 and " +
                               " proy.nombre like '%" + edificio + "%' and " +
                               " eq.exbo like '%" + exbo + "%'";
                               
           return conexion.consultaMySql(consulta);
       }


       public DataSet getequipoControlPedido(int codigoEquipo)
       {
           string consulta = "select  "+
                               " proy.nombre as Edificio, "+
                               " eq.exbo , "+
                               " eq.vc, "+
                               " eq.valorcfrtransportemaritimo "+
                               " from tb_equipo eq, tb_proyecto proy "+
                               " where  "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " eq.codigo = "+codigoEquipo;
           return conexion.consultaMySql(consulta);
       }

       internal string get_codigoClienteSimec(string exbo)
       {
           string consulta = "select "+
                               " eq.clicodigo "+
                               " from tb_equipo eq "+
                               " where "+
                               " eq.exbo = '"+exbo+"'";
          DataSet dato = conexion.consultaMySql(consulta);
          if (dato.Tables[0].Rows.Count > 0)
          {
              return dato.Tables[0].Rows[0][0].ToString();
          }
          else
              return "";
       }

       internal DataSet get_cantidadEquiposdeEdificio(string edificio)
       {
           string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo, "+
                               " proy.nombre "+
                               " from tb_equipo eq, tb_proyecto proy, tb_fechaestadoequipo feq "+
                               " where "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " eq.estado = 1 and "+
                               " eq.codfechaestadoequipo = feq.codigo and "+
                               " feq.codEstadoEquipo = 10 and "+
                               " proy.nombre = '"+edificio+"' ";
           return conexion.consultaMySql(consulta);
       }

       internal DataSet get_equipos(string nombreProyecto)
       {
          /* string consulta = "select " +
                              " eq.codigo, " +
                              " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'nombre' " +
                              " from tb_equipo eq, tb_proyecto proy" +
                              " where " +
                              " eq.cod_proyecto = proy.codigo and " +
                              " eq.estado = 1 and " +
                              " proy.nombre = '" + nombreProyecto + "' "; */
           string consulta = "select "+
                               " eq.codigo, "+
                              // " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'nombre', "+
                               " eq.exbo as 'nombre', " +
                               " res1.`nombre` as 'Fiscal ' , "+
                               " t1.nombre as 'Instalador Fase1', "+
                               " t2.nombre as 'Instalador Fase2' "+
                               " from  tb_proyecto proy ,tb_equipo eq "+ 
                               " left join tb_responsable res1 on (eq.codresponsable = res1.codigo) "+
                               " left join "+
                               " (select dta.codeq,resin.nombre "+ 
                               " from tb_detalle_tecnico_asignado dta , tb_responsable resin "+
                               " where "+
                               " dta.codresp = resin.codigo and "+
                               " dta.codcargo = 3 and dta.estado = 1 "+
                               " group by dta.codeq) as t1 on (eq.codigo = t1.codeq ) "+
                               " left join "+
                               " (select dta2.codeq,resin2.nombre "+ 
                               " from tb_detalle_tecnico_asignado dta2 , tb_responsable resin2 "+
                               " where "+
                               " dta2.codresp = resin2.codigo and "+
                               " dta2.codcargo = 4 and dta2.estado = 1 "+
                               " group by dta2.codeq) as t2 on (eq.codigo = t2.codeq ) "+
                               " where "+
                               " eq.cod_proyecto = proy.codigo and "+ 
                               " eq.estado = 1 and "+
                               " proy.nombre = '"+nombreProyecto+"'";
           return conexion.consultaMySql(consulta);
       }

       internal bool insertarEncuestaR220(int codEquipo, int codPregunta, bool Conforme, string Observaciones)
       {
           string consulta = "insert into tb_detalle_equipor220(codeq,codr220,conforme,observaciones) values "+
                           " ("+codEquipo+","+codPregunta+","+Conforme+",'"+Observaciones+"')";
           return conexion.ejecutarMySql(consulta);
       }
    }
}