using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_EncuestaMantenimiento
    {
        private conexionMySql cnx = new conexionMySql();
        public DA_EncuestaMantenimiento() { }

        public bool insertarEncuestaMantenimiento(int codproyecto, int cumplimientofechasplanificadasmantenimiento,
                                                    int funcionamientodelosequipos ,
                                                    int rapidezdelasreparaciones ,
                                                    int resolucionefectivadelacausadereparacion ,
                                                    int asesoramientoyrapidezenlaentregadecotizacionesinformes ,
                                                    int tiempoderespuestaanteunaemergencia ,
                                                    int resolucionefectivadelasemergencias ,
                                                    int cordialidadyatenciondelpersonaldecobranza ,
                                                    int tratoyatenciondelpersonalasministrativo ,
                                                    int cordialidadyatenciondelpersonaltecnico ,
                                                    int tratoyatenciondelpersonaldeingenieria ,
                                                    int tratoatencionyrespuestadelpersonaldecallcenter, string sugerenciademejora, int codresp)
        {
            string consulta = "insert into tb_encuestamantenimiento( "+
                               " fecha ,hora ,fechagra ,horagra , "+
                               " cumplimientofechasplanificadasmantenimiento, "+
                               " funcionamientodelosequipos , "+
                               " rapidezdelasreparaciones , "+
                               " resolucionefectivadelacausadereparacion , "+
                               " asesoramientoyrapidezenlaentregadecotizacionesinformes , "+
                               " tiempoderespuestaanteunaemergencia , "+
                               " resolucionefectivadelasemergencias , "+
                               " cordialidadyatenciondelpersonaldecobranza , "+
                               " tratoyatenciondelpersonalasministrativo , "+
                               " cordialidadyatenciondelpersonaltecnico , "+
                               " tratoyatenciondelpersonaldeingenieria , "+
                               " tratoatencionyrespuestadelpersonaldecallcenter , "+
                               " codproyecto, sugerenciademejora, codresp) values( " +
                               " current_date , current_time , current_date , current_time , "+
                                cumplimientofechasplanificadasmantenimiento +" , "+
                                funcionamientodelosequipos+" , "+
                                rapidezdelasreparaciones+" , "+
                                resolucionefectivadelacausadereparacion+" , "+
                                asesoramientoyrapidezenlaentregadecotizacionesinformes +" , "+
                                tiempoderespuestaanteunaemergencia +" , "+
                                resolucionefectivadelasemergencias + " , "+
                                cordialidadyatenciondelpersonaldecobranza + " , "+
                                tratoyatenciondelpersonalasministrativo + " , "+
                                cordialidadyatenciondelpersonaltecnico + " , "+
                                tratoyatenciondelpersonaldeingenieria +" , "+
                                tratoatencionyrespuestadelpersonaldecallcenter +" , "+
                                codproyecto + ",'" + sugerenciademejora + "'," + codresp + ")";
            return cnx.ejecutarMySql(consulta);       

        }
    }
}