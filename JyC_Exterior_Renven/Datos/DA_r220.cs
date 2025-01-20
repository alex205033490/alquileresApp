using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace JyC_Exterior.Datos
{
    public class DA_r220
    {
        private conexionMySql Cnx = new conexionMySql();
        public DA_r220() { }




        internal bool insertarRegistro220(int codUser, string nombreResponsable, string edificio, string exbo, int codEquipo, string observacionesGenerales, string fiscal, string instaladorFase1, string instaladorFase2, string fechaEstablecida, string estadoEquipo)
        {
            string consulta = "insert into tb_r220registro( "+
                               " fechagra ,horagra ,resgra ,codequipo ,edificio ,exbo , "+
                               " realizadopor ,observacionesgenerales , fiscal, instalador, instalador2, fecha_establecida,  estadoequipo " +
                               " ) values( "+
                               " current_date() , current_time() ,"+codUser+" ,"+codEquipo+" , '"+edificio+"' , '"+exbo+"' , "+
                               " '" + nombreResponsable + "' , '" + observacionesGenerales + "' , '" + fiscal + "', '" + instaladorFase1 + "', '" + instaladorFase2 + "', " + fechaEstablecida + ",'"+estadoEquipo+"')";
            return Cnx.ejecutarMySql(consulta);
        }

        internal DataSet get_UltimoInsertadoDato()
        {
            string consulta = "select max(codigo) from tb_r220registro";
            return Cnx.consultaMySql(consulta);
        }

        internal bool insertarEncuestaR220(int codigoUltimoInsertado, int codigoPregunta, bool conforme, string observacion)
        {
            string consulta = "insert into tb_detalle_equipor220(codr220registro,codr220pregunta, "+
                               " conforme,observaciones) values("+codigoUltimoInsertado+","+codigoPregunta+", "+
                               conforme+" ,'"+observacion+"')";
            return Cnx.ejecutarMySql(consulta);
        }

        internal DataSet get_CantidadPreguntasTotal()
        {
            string consulta = "select count(*) from tb_r220preguntas ";
            return Cnx.consultaMySql(consulta);
        }

        internal DataSet get_Pregunta_R220(int codigo)
        {
            string consulta = "select codigo, nombre from tb_r220preguntas pp "+
                               " where pp.codigo ="+codigo;
            return Cnx.consultaMySql(consulta);
        }

        internal DataSet getObservacionesConDatos(string nombreProyecto, int codEquipo)
        {
            string consulta = "select dr.codr220registro, dr.codr220pregunta, "+
                               " dr.conforme, dr.observaciones "+ 
                               " from tb_detalle_equipor220 dr "+
                               " where "+
                               " dr.observaciones is not null and "+
                               " dr.observaciones <> '' and "+
                               " dr.codr220registro in "+
                               " (select max(rr.codigo) from tb_r220registro rr "+
                               " where "+
                               " rr.codequipo = '"+codEquipo+"' and " +
                               " rr.edificio = '"+nombreProyecto+"')";
            return Cnx.consultaMySql(consulta);
        }

        internal DataSet getObservacionesGeneral(string nombreProyecto, int codEquipo)
        {
            string consulta = "select rr.codigo, rr.observacionesgenerales from tb_r220registro rr " +
                             " where " +
                             " rr.codequipo = '"+codEquipo+"' and " +
                             " rr.edificio = '"+nombreProyecto+"' "+
                             "  order by rr.codigo desc limit 1 ";
            return Cnx.consultaMySql(consulta);
        }
    }
}