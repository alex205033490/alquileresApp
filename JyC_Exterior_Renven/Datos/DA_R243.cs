using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace JyC_Exterior.Datos
{
    public class DA_R243
    {

        private conexionMySql Cnx = new conexionMySql();
        public DA_R243() { }

        internal bool insertarRegistro220(int codUser, string nombreResponsable, string edificio, string exbo, int codEquipo, string observacionesGenerales, string fiscal, string instaladorFase1, string instaladorFase2)
        {
            string consulta = "insert into tb_R243repuesta( " +
                               " fechagra ,horagra ,resgra ,codequipo ,edificio ,exbo , "+
                               " realizadopor ,observacionesgenerales , fiscal, instalador, instalador2  " +
                               " ) values( "+
                               " current_date() , current_time() ,"+codUser+" ,"+codEquipo+" , '"+edificio+"' , '"+exbo+"' , "+
                               " '" + nombreResponsable + "' , '" + observacionesGenerales + "' , '" + fiscal + "', '" + instaladorFase1 + "', '" + instaladorFase2 + "')";
            return Cnx.ejecutarMySql(consulta);
        }

        internal DataSet get_UltimoInsertadoDato()
        {
            string consulta = "select max(codigo) from tb_R243repuesta";
            return Cnx.consultaMySql(consulta);
        }

        internal bool insertarEncuestaR243(int codr243formulario, int codr243respuesta, bool cumple, bool nocumple, bool noaplica, string observacion)
        {
            string consulta = "insert into tb_detalle_r243respuesta(codr243formulario,codr243respuesta, " +
                               " cumple, nocumple, noaplica,observacion) values(" + codr243formulario + "," + codr243respuesta + ", " +
                               cumple + " ," + nocumple + ", " + noaplica + ", '" + observacion + "')";
            return Cnx.ejecutarMySql(consulta);
        }

        internal DataSet get_CantidadPreguntasTotal()
        {
            string consulta = "select count(*) from tb_R243hojadecontroldeinstalacion ";
            return Cnx.consultaMySql(consulta);
        }

        internal DataSet get_Pregunta_R220(int codigo)
        {
            string consulta = "select codigo, nombre from tb_R243hojadecontroldeinstalacion pp " +
                               " where pp.codigo ="+codigo;
            return Cnx.consultaMySql(consulta);
        }

        internal DataSet getObservacionesConDatos(string nombreProyecto, int codEquipo)
        {
            string consulta = "select dr.codr243formulario, dr.codr243respuesta, " +
                               " dr.cumple, dr.nocumple, dr.noaplica " + 
                               " from tb_detalle_equipor220 dr "+
                               " where "+
                               " dr.codr243formulario in " +
                               " (select max(rr.codigo) from tb_R243hojadecontroldeinstalacion rr " +
                               " where "+
                               " rr.codequipo = '"+codEquipo+"' and " +
                               " rr.edificio = '"+nombreProyecto+"')";
            return Cnx.consultaMySql(consulta);
        }

        internal DataSet getObservacionesGeneral(string nombreProyecto, int codEquipo)
        {
            string consulta = "select rr.codigo, rr.observacionesgenerales from tb_R243hojadecontroldeinstalacion rr " +
                             " where " +
                             " rr.codequipo = '"+codEquipo+"' and " +
                             " rr.edificio = '"+nombreProyecto+"' "+
                             "  order by rr.codigo desc limit 1 ";
            return Cnx.consultaMySql(consulta);
        }

    }
}