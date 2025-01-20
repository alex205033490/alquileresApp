using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;


namespace jycboliviaASP.net.Datos
{
    public class DA_Evento
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_Evento() { }

        public bool insertar(int semana, string hora, string fecha, string cliente, string telefono, string celular, string nombreEdificio, string dirEdificio, string ascensor, string estadoEvento, string Observacion, int codtipoevento, int codEdificio, int ascensorParado, int personasatrapadas, int prioridad, int UserInicio, bool cambioRepuesto, bool areacotirepuesto, bool solicitudrepuestobandera, bool areacallcenter, bool arearin, bool arearcc, bool areacliente)
        {  
                string codEdificio_aux = "null";
                if(codEdificio > -1){
                    codEdificio_aux = codEdificio.ToString();
                }

                string consulta = "insert into tb_evento(semana, hora, fecha, cliente, telefono, celular, nombreEdificio, dirEdificio, ascensor, estadoEvento, Observacion, codtipoevento, codEdificio, ascensorparado, personasatrapadas, prioridad, inicioeventouser, cambiorepuesto , areacotirepuesto, solicitudrepuestobandera, areacallcenter, arearin, arearcc, areacliente) " +
                                    " values(" + semana + ", '" + hora + "', '" + fecha + "', '" + cliente + "', '" + telefono + "', '" + celular + "', '" + nombreEdificio + "', '" + dirEdificio + "', '" + ascensor + "', '" + estadoEvento + "', '" + Observacion + "', " + codtipoevento + ", " + codEdificio_aux + "," + ascensorParado + "," + personasatrapadas + "," + prioridad + " , " + UserInicio + ","+cambioRepuesto+"," + areacotirepuesto + ", "+solicitudrepuestobandera+", "+areacallcenter+" , "+ arearin+" , "+arearcc+" , "+areacliente+" ) ";
               return ConecRes.ejecutarMySql(consulta);
        }

        public bool modificar(int codigoEvento,string cliente, string telefono, string celular, string nombreEdificio, string dirEdificio, string ascensor, string Observacion,  int ascensorParado, int personasatrapadas )
        {
           

            string consulta = "update tb_evento set cliente = '" + cliente + "', telefono = '" + telefono + "', celular = '" + celular + "', nombreEdificio = '" + nombreEdificio + "', dirEdificio ='" + dirEdificio + "', ascensor = '" + ascensor + "', Observacion = '" + Observacion + "', ascensorparado = " + ascensorParado + ", personasatrapadas = " + personasatrapadas + " where tb_evento.codigo = " + codigoEvento;
                                    
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public bool modificarDatosEvento(int codEvento, string estadoEvento, string observacion, string defectoConstatado, string observacionNecesidadRepuesto, string solicitudRepuesto, string envioProforma, string aceptacionProforma, string verificacionCambio, bool cambioRepuesto, int codCierreUser, string fechaEven, string HoraEven, bool solicitudRepuestobandera, bool areaCliente, bool areaCallcenter, bool areaRin, bool areaRcc, int prioridad)
        {
            string auxTexto = "";
            if(estadoEvento.Equals("Cerrado")){
                auxTexto = " , tb_evento.cierreeventouser = " + codCierreUser + ", tb_evento.fechacierreevento = now() , tb_evento.horacierreevento = now() ";
            }

            string consulta = "update tb_evento set tb_evento.estadoEvento = '"+estadoEvento+"' ,tb_evento.observacion_evento = '"+observacion+"', tb_evento.defectoconstatado = '"+defectoConstatado+"', tb_evento.observacion_necesidadrepuesto = '"+observacionNecesidadRepuesto+"', "+
                               " tb_evento.solicitudRepuesto = "+solicitudRepuesto+", tb_evento.envioproforma = "+envioProforma+", tb_evento.aceptacionproforma = "+aceptacionProforma+", "+
                               " tb_evento.verificacion_cambio = '" + verificacionCambio + "', tb_evento.cambiorepuesto = " + cambioRepuesto +" " + auxTexto +  
                               " ,tb_evento.fecha = "+fechaEven+" , tb_evento.hora = "+HoraEven+" , tb_evento.solicitudrepuestobandera = "+solicitudRepuestobandera+
                               " ,tb_evento.arearin = "+areaRin+", "+
                               " tb_evento.arearcc = "+areaRcc+", "+
                               " tb_evento.areacliente = "+areaCliente+", "+
                               " tb_evento.areacallcenter = "+areaCallcenter+" , "+
                               " tb_evento.prioridad = "+prioridad+
                               " where tb_evento.codigo = "+codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool modificarDatosEvento_FechaContactoCliente(int codEvento, string fechaContactoCliente, string detalleContactoCliente)
        {
            string consulta = "update tb_evento set tb_evento.fechacontactocliente = " + fechaContactoCliente + ", " +
                               " tb_evento.detalle_contactocliente = '" + detalleContactoCliente + "', " +
                               " tb_evento.horacontactocliente = now() " +
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool modificarDatosEvento2(int codEvento, string estadoEvento, string observacion, string defectoConstatado, string observacionNecesidadRepuesto, string solicitudRepuesto, string envioProforma, string aceptacionProforma, string verificacionCambio, bool cambioRepuesto, int codCierreUser, string fechaEven, string HoraEven, bool solicitudRepuestobandera)
        {
            string auxTexto = "";
            if (estadoEvento.Equals("Cerrado"))
            {
                auxTexto = " , tb_evento.cierreeventouser = " + codCierreUser + ", tb_evento.fechacierreevento = '" + DateTime.Now.ToString("yyyy-MM-dd") + "', tb_evento.horacierreevento = '" + DateTime.Now.ToString("HH:mm:ss") + "' ";
            }

            string consulta = "update tb_evento set tb_evento.estadoEvento = '" + estadoEvento + "' ,tb_evento.observacion_evento = '" + observacion + "', tb_evento.defectoconstatado = '" + defectoConstatado + "', tb_evento.observacion_necesidadrepuesto = '" + observacionNecesidadRepuesto + "', " +
                              " tb_evento.solicitudRepuesto = " + solicitudRepuesto + ", tb_evento.envioproforma = " + envioProforma + ", tb_evento.aceptacionproforma = " + aceptacionProforma + ", " +
                              " tb_evento.verificacion_cambio = '" + verificacionCambio + "', tb_evento.cambiorepuesto = " + cambioRepuesto + " " + auxTexto +
                              " ,tb_evento.fecha = " + fechaEven + " , tb_evento.hora = " + HoraEven + " , tb_evento.solicitudrepuestobandera = " + solicitudRepuestobandera +
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }

        //---------------------nuevo detalle de callcenter y cotizaciones enlace
        public bool updateEventoCallcenter_CotiRepuesto(int codEvento, int CodCoti, int codPrioridad)
        {
            string consulta = "update tb_cotizacionrepuesto set tb_cotizacionrepuesto.codevento = "+codEvento+" , "+
                              " tb_cotizacionrepuesto.prioridad =  "+codPrioridad+ 
                              " where "+
                              " tb_cotizacionrepuesto.codigo = "+CodCoti;

            return ConecRes.ejecutarMySql(consulta);
        }

        public bool updatefechaEnvioProformaEvento(int codEvento)
        {
            string consulta = "update tb_evento set tb_evento.envioproforma = now() "+
                               " where tb_evento.codigo = "+codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool updatefechaAceptacionProformaEvento(int codEvento)
        {
            string consulta = "update tb_evento "+
                               " set tb_evento.envioproforma = now(), "+
                               " tb_evento.aceptacionproforma = now(), "+
                               " tb_evento.solicitudrepuestobandera = 0, "+
                               " tb_evento.areacotirepuesto = 1, "+
                               " tb_evento.areacallcenter = 0, "+
                               " tb_evento.areacliente = 0, "+
                               " tb_evento.arearcc = 0, "+
                               " tb_evento.arearin = 0, "+
                               " tb_evento.areaera = 1 "+
                               " where tb_evento.codigo =" + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool pasarEventoalAreaRIN(int codEvento)
        {
            string consulta = "update tb_evento set "+
                               " tb_evento.areacotirepuesto = 0, "+
                               " tb_evento.areaera = 0, "+
                               " tb_evento.arearin = 1 " +
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool updateCerrarEventoCotizacion(int codEvento, int codUser, string detalleCierre)
        {
            string aux1 = "select even.observacion_evento "+
                           " from tb_evento even  "+
                           " where even.codigo = "+codEvento;
            DataSet dato = ConecRes.consultaMySql(aux1);
          
                string observacion = dato.Tables[0].Rows[0][0].ToString();

                string consulta = "";
                if (!observacion.Equals(""))
                {
                    consulta = "update tb_evento set tb_evento.observacion_evento = concat(tb_evento.observacion_evento,' ','" + detalleCierre + "') , " +
                                       " tb_evento.estadoEvento = 'Cerrado', tb_evento.fechacierreevento = now(), tb_evento.horacierreevento = now(), " +
                                       " tb_evento.cierreeventouser = " + codUser +
                                       " where tb_evento.codigo = " + codEvento;
                }
                else
                {
                    consulta = "update tb_evento set tb_evento.observacion_evento = '" + detalleCierre + "' , " +
                                           " tb_evento.estadoEvento = 'Cerrado', tb_evento.fechacierreevento = now(), tb_evento.horacierreevento = now(), " +
                                           " tb_evento.cierreeventouser = " + codUser +
                                           " where tb_evento.codigo = " + codEvento;
                }

                return ConecRes.ejecutarMySql(consulta);            

        }


        public bool updateCerrarEventoCotizacionAreaCliente(int codEvento, string detalleCierre)
        {
            string consulta = "update tb_evento set tb_evento.observacion_evento = concat(tb_evento.observacion_evento,' ','" + detalleCierre + "') , " +
                               " tb_evento.estadoEvento = 'Abierto' , "+
                               " tb_evento.solicitudrepuestobandera = 0, " +
                               " tb_evento.areacotirepuesto = 1, " +
                               " tb_evento.areacallcenter = 0, " +
                               " tb_evento.areacliente = 1, " +
                               " tb_evento.arearcc = 0, " +
                               " tb_evento.arearin = 0 " +
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool modificarDatosEventoSolicitudRepuesto(int codEvento, string defectoConstatado, string observacionNecesidadRepuesto, string solicitudRepuesto, bool cambioRepuesto, bool solicitudRepuestobandera)
        {
         

            string consulta = "update tb_evento set  tb_evento.defectoconstatado = '" + defectoConstatado + "', tb_evento.observacion_necesidadrepuesto = '" + observacionNecesidadRepuesto + "', " +
                              " tb_evento.solicitudRepuesto = " + solicitudRepuesto + ", " +
                              " tb_evento.cambiorepuesto = " + cambioRepuesto + " , "+
                              " tb_evento.solicitudrepuestobandera = " + solicitudRepuestobandera +                              
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public DataSet getCantCoticonElMismoEvento(int codEvento)
        {
            string consulta = "select count(*) as 'cant' from tb_cotizacionrepuesto coti " +
                               " where "+
                               " coti.codr144_padre is not null and "+
                               " coti.codevento = "+codEvento;
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getCantCoticonElMismoEvento_estadoCerrado(int codEvento)
        {
            string consulta = "select count(*) as 'cant' from tb_cotizacionrepuesto coti " +
                               " where "+
                               " coti.codr144_padre is not null and "+
                               " coti.estadocoti = 'Cerrado' and "+
                               " coti.codevento = "+codEvento;
            return ConecRes.consultaMySql(consulta);
        }

        //-------------------------------------nuevo----------------------

        internal DataSet getEmergenciaTecnico(int codTecnico)
        {
           /* string consulta = "select "+
                               " deven.codigo, "+ 
                               " even.nombreEdificio as 'Edificio', "+
                               " even.Observacion as '____________Detalle____________', " +
                               " if(even.ascensorparado,'Si','No') as 'Ascensores_Parado', "+
                               " if(even.personasatrapadas,'Si','No') as 'Personas Atrapadas', "+
                               " deven.hora_asignacion, "+
                               " DATE_FORMAT(deven.fecha_asignacion,'%d/%m/%Y') as 'fecha_asignacion' "+
                               " from tb_evento even,tb_detalle_teceven deven "+
                               " where "+
                               " even.codigo = deven.codeven and "+
                               " even.estadoEvento = 'Abierto' and "+                               
                               " deven.cod_boletaemergenciacallcenter = 0 and "+ 
                               " deven.codtec = "+codTecnico; */
            string consulta = "select "+
                               " bcc.codigo, "+
                               " even.nombreEdificio as 'Edificio', "+
                               " bcc.tipoboleta as 'Trabajo', "+
                               " bcc.tarea_objetivo as '____________Tarea____________', "+
                               " if(even.ascensorparado,'Si','No') as 'Ascensores_Parado', "+ 
                               " if(even.personasatrapadas,'Si','No') as 'Personas Atrapadas', "+ 
                               " dtec.hora_asignacion, "+ 
                               " DATE_FORMAT(dtec.fecha_asignacion ,'%d/%m/%Y') as 'fecha_asignacion' "+
                               " from tb_evento even, tb_detalle_teceven dtec, tb_boletaemergenciacallcenter bcc "+
                               " where  "+
                               " even.codigo = dtec.codeven and "+
                               " bcc.cod_detalleEventoCallcenter = dtec.codigo and "+
                               " bcc.estado = 'Abierto' and "+
                               " bcc.codresptecnico = "+codTecnico;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet getDatosEmergenciaCallcenter(int codigoDetalleEmergencia)
        {
            string consulta = "select "+
                               " deven.codigo, "+
                               " even.nombreEdificio as 'Edificio', "+
                               " even.cliente, "+
                               " concat(even.telefono,'-',even.celular) as 'Celular', "+
                               " bb.tarea_objetivo  as 'Tarea_Objetivo', "+
                               " if(even.ascensorparado,'Si','No') as 'Ascensores_Parado', "+
                               " if(even.personasatrapadas,'Si','No') as 'Personas Atrapadas', "+ 
                               " deven.hora_asignacion, "+
                               " DATE_FORMAT(deven.fecha_asignacion,'%d/%m/%Y') as 'fecha_asignacion' "+                                
                               " from tb_evento even,tb_detalle_teceven deven , tb_boletaemergenciacallcenter bb "+
                               " where "+
                               " even.codigo = deven.codeven and "+
                               " bb.cod_detalleEventoCallcenter = deven.codigo and "+
                               " bb.estado = 'Abierto' and "+
                               " bb.codigo = "+codigoDetalleEmergencia; 
            return ConecRes.consultaMySql(consulta);
        }

        internal bool guardarDatosEmergenciaCallcenter( string fechallegada , string horallegada ,                                                
                                                        bool i_fusiblecontactos ,bool i_botoneradepisoencorte ,bool i_limites,
                                                        bool i_reguladordevelocidad ,bool i_frenobalataselectroiman ,
                                                        bool i_motordetraccion ,bool i_poleas ,bool i_filtraciondeaguaensalademaquinas ,
                                                        bool i_accesoirregularasalademaquinas ,bool i_corteensenalizadoropulsadordepiso ,
                                                        bool i_ruidooajusteenpuertaspisocabina ,bool i_iluminaciondecabina ,bool i_operadordepuertas ,
                                                        bool i_motordeoperador ,bool i_ventiladordecabina ,bool i_cerrojo ,
                                                        bool i_sensordecabinabarrerafotocelula ,bool i_filtraciondeaguaenhuecoyfoso ,
                                                        bool i_bajasocortedetension ,bool i_sensores ,bool i_malusoporusuario ,
                                                        bool i_iluminacionirregularensalademaquinasyfoso ,bool i_otros ,
                                                        bool cambiorepuesto ,bool arreglo , string recepcion ,string receptor_ci ,
                                                        string receptor_cargo ,string observacioncierre ,
                                                        int cod_Tarea_Tecnico, int codresgra, bool siningresoedificio, string estadoboleta, string ascensor, float costoPasaje)
        {
            string consulta = "update tb_boletaemergenciacallcenter set "+                                              
                               " tb_boletaemergenciacallcenter.fechagraboleta = current_date(),"+ 
                               " tb_boletaemergenciacallcenter.horagraboleta = current_time(),"+ 
                               " tb_boletaemergenciacallcenter.fechallegada = "+fechallegada+","+
                               " tb_boletaemergenciacallcenter.horallegada = '"+horallegada+"',"+ 
                               " tb_boletaemergenciacallcenter.fechasalida = current_date(),"+ 
                               " tb_boletaemergenciacallcenter.horasalida = current_time(),"+
                               " tb_boletaemergenciacallcenter.i_fusiblecontactos = "+i_fusiblecontactos+","+ 
                               " tb_boletaemergenciacallcenter.i_botoneradepisoencorte = "+i_botoneradepisoencorte+","+ 
                               " tb_boletaemergenciacallcenter.i_limites = "+i_limites+","+ 
                               " tb_boletaemergenciacallcenter.i_reguladordevelocidad = "+i_reguladordevelocidad+","+ 
                               " tb_boletaemergenciacallcenter.i_frenobalataselectroiman = "+i_frenobalataselectroiman+","+ 
                               " tb_boletaemergenciacallcenter.i_motordetraccion = "+i_motordetraccion+","+ 
                               " tb_boletaemergenciacallcenter.i_poleas = "+i_poleas+","+ 
                               " tb_boletaemergenciacallcenter.i_filtraciondeaguaensalademaquinas = "+i_filtraciondeaguaensalademaquinas+","+ 
                               " tb_boletaemergenciacallcenter.i_accesoirregularasalademaquinas = "+i_accesoirregularasalademaquinas+","+ 
                               " tb_boletaemergenciacallcenter.i_corteensenalizadoropulsadordepiso = "+i_corteensenalizadoropulsadordepiso+","+ 
                               " tb_boletaemergenciacallcenter.i_ruidooajusteenpuertaspisocabina = "+i_ruidooajusteenpuertaspisocabina+","+ 
                               " tb_boletaemergenciacallcenter.i_iluminaciondecabina = "+i_iluminaciondecabina+","+ 
                               " tb_boletaemergenciacallcenter.i_operadordepuertas = "+i_operadordepuertas+","+ 
                               " tb_boletaemergenciacallcenter.i_motordeoperador = "+i_motordeoperador+","+ 
                               " tb_boletaemergenciacallcenter.i_ventiladordecabina = "+i_ventiladordecabina+","+ 
                               " tb_boletaemergenciacallcenter.i_cerrojo = "+i_cerrojo+","+ 
                               " tb_boletaemergenciacallcenter.i_sensordecabinabarrerafotocelula = "+i_sensordecabinabarrerafotocelula+","+ 
                               " tb_boletaemergenciacallcenter.i_filtraciondeaguaenhuecoyfoso = "+i_filtraciondeaguaenhuecoyfoso+","+ 
                               " tb_boletaemergenciacallcenter.i_bajasocortedetension = "+i_bajasocortedetension+","+ 
                               " tb_boletaemergenciacallcenter.i_sensores = "+i_sensores+","+
                               " tb_boletaemergenciacallcenter.i_malusoporusuario = "+i_malusoporusuario+","+ 
                               " tb_boletaemergenciacallcenter.i_iluminacionirregularensalademaquinasyfoso = "+i_iluminacionirregularensalademaquinasyfoso+","+ 
                               " tb_boletaemergenciacallcenter.i_otros = "+i_otros+","+ 
                               " tb_boletaemergenciacallcenter.cambiorepuesto = "+cambiorepuesto+"," +
                               " tb_boletaemergenciacallcenter.arreglo = "+arreglo+","+ 
                               " tb_boletaemergenciacallcenter.recepcion = '"+recepcion+"',"+ 
                               " tb_boletaemergenciacallcenter.receptor_ci = '"+receptor_ci+"',"+ 
                               " tb_boletaemergenciacallcenter.receptor_cargo = '"+receptor_cargo+"',"+ 
                               " tb_boletaemergenciacallcenter.observacioncierre = '"+observacioncierre+"',"+                                
                               " tb_boletaemergenciacallcenter.codresgra = "+codresgra+","+ 
                               " tb_boletaemergenciacallcenter.siningresoedificio = "+siningresoedificio+","+ 
                               " tb_boletaemergenciacallcenter.estadoboleta = '"+estadoboleta+"',"+
                               " tb_boletaemergenciacallcenter.ascensor = '"+ascensor+"',"+
                               " tb_boletaemergenciacallcenter.fechacierre = current_date(),"+
                               " tb_boletaemergenciacallcenter.horacierre = current_time(),"+
                               " tb_boletaemergenciacallcenter.estado = 'Cerrado', "+
                               " tb_boletaemergenciacallcenter.pasajecosto = '" +costoPasaje.ToString().Replace(',','.')+"' "+
                               " where "+
                               " tb_boletaemergenciacallcenter.codigo =  " + cod_Tarea_Tecnico;


            bool bandera = ConecRes.ejecutarMySql(consulta);
            consulta = "select b.codigo, "+
                       " b.boleta, "+
                       " b.horagraboleta, " +
                       " date_format(b.fechagraboleta, '%Y/%m/%d') as fechaGra, " +
                       " b.horallegada, "+
                       " date_format(b.fechallegada, '%Y/%m/%d') as fechaLLegada, "+
                       " b.horasalida , "+
                       " date_format(b.fechasalida, '%Y/%m/%d') as fechaSalida, "+
                       " b.estadoboleta, "+
                       " b.ascensor, "+
                       " b.observacioncierre, "+
                       " b.cod_detalleEventoCallcenter "+
                       " from tb_boletaemergenciacallcenter b "+
                       " where b.codigo = " + cod_Tarea_Tecnico;
            DataSet tupla = ConecRes.consultaMySql(consulta);
            bool bandera2 = false;
            if(tupla.Tables[0].Rows.Count > 0){
                int codigo = Convert.ToInt32(tupla.Tables[0].Rows[0][0].ToString());
                consulta = "update tb_detalle_teceven set tb_detalle_teceven.cod_boletaemergenciacallcenter =  " + codigo +" , "+
                           " tb_detalle_teceven.nroboleta = '"+tupla.Tables[0].Rows[0][1].ToString()+"',"+
                           " tb_detalle_teceven.hora_reporte = '"+tupla.Tables[0].Rows[0][2].ToString()+"',"+
                           " tb_detalle_teceven.fecha_horareporte = '"+tupla.Tables[0].Rows[0][3].ToString()+"',"+
                           " tb_detalle_teceven.hora_llegadaEdificio = '"+tupla.Tables[0].Rows[0][4].ToString()+"',"+
                           " tb_detalle_teceven.fecha_llegadaedificio = '"+tupla.Tables[0].Rows[0][5].ToString()+"',"+
                           " tb_detalle_teceven.hora_salidaEdificio = '"+tupla.Tables[0].Rows[0][6].ToString()+"',"+
                           " tb_detalle_teceven.fecha_salidaedificio = '"+tupla.Tables[0].Rows[0][7].ToString()+"',"+
                           " tb_detalle_teceven.estadoEquipo_llegadasalida = '"+tupla.Tables[0].Rows[0][8].ToString()+"',"+
                           " tb_detalle_teceven.ascensorreparado = '"+ascensor+"',"+
                           " tb_detalle_teceven.observacion =  '"+tupla.Tables[0].Rows[0][10].ToString()+"'"+
                           " where " +
                           " tb_detalle_teceven.codigo = " + tupla.Tables[0].Rows[0][11].ToString();
                bandera2 = ConecRes.ejecutarMySql(consulta);            
            }

            if (bandera == true && bandera2 == true)
            {
                return true;
            }
            else
                return false;
        }



        internal DataSet get_CodigoEmergenciaCallcenter(int cod_detalleEventoCallcenter)
        {
            string consulta = "select "+
                               " deven.cod_boletaemergenciacallcenter "+
                               " from "+
                               " tb_detalle_teceven deven "+
                               " where "+
                               " deven.codigo = "+cod_detalleEventoCallcenter;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_DatosEmergenciaCallcenter(int cod_Tarea_TecnicoBoleta)
        {
            string consulta = "select b.codigo, " +
                       " b.boleta, " +
                       " b.horagraboleta, " +
                       " date_format(b.fechagraboleta, '%d/%m/%Y') as fechaGra, " +
                       " b.horallegada, " +
                       " date_format(b.fechallegada, '%d/%m/%Y') as fechaLLegada, " +
                       " b.horasalida , " +
                       " date_format(b.fechasalida, '%d/%m/%Y') as fechaSalida, " +
                       " b.estadoboleta, " +
                       " b.ascensor, " +
                       " b.observacioncierre, "+
                       " b.recepcion, "+
                       " b.receptor_ci, "+
                       " b.receptor_cargo "+
                       " from tb_boletaemergenciacallcenter b " +
                       " where " +
                       " b.codigo = " + cod_Tarea_TecnicoBoleta;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_cod_detalleEventoCallcenter(int cod_Tarea_Tecnico)
        {
           string consulta = "select "+
                              " b.cod_detalleEventoCallcenter " +
                              " from tb_boletaemergenciacallcenter b " +
                              " where b.codigo = " + cod_Tarea_Tecnico;
            DataSet tupla = ConecRes.consultaMySql(consulta);
            return tupla;
        }
    }
}