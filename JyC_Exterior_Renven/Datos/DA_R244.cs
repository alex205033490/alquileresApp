using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace JyC_Exterior.Datos
{
    public class DA_R244
    {
        private conexionMySql cnx = new conexionMySql();
        public DA_R244() { }

        internal bool insertarRegistro244(string fiscal, string RCC, int codresgra, string responsable, int codedificio, string edificio, int cantEquipos, string realizadopor, string recibiconforme, string recibici, string recibicargo, string observacionesgenerales)
        {
            string consulta = "insert into tb_r244registro( "+
                               " tb_r244registro.fechagra,tb_r244registro.horagra, "+
                               " tb_r244registro.fiscal,tb_r244registro.RCC, "+
                               " tb_r244registro.codresgra,tb_r244registro.responsable, "+
                               " tb_r244registro.codedificio,tb_r244registro.edificio, "+
                               " tb_r244registro.cantEquipos,tb_r244registro.realizadopor, "+
                               " tb_r244registro.recibiconforme,tb_r244registro.recibici, "+
                               " tb_r244registro.recibicargo,tb_r244registro.observacionesgenerales, "+
                               " tb_r244registro.fecha_establecida,tb_r244registro.estado, "+
                               " tb_r244registro.estadoequipo "+
                               " ) values( "+
                               " current_date(), current_time() , "+
                               " '"+fiscal+"', '"+RCC+"', "+
                                codresgra+", '"+responsable+"', "+
                                codedificio+", '"+edificio+"', "+
                                cantEquipos+", '"+realizadopor+"', "+
                               " '"+recibiconforme+"' , '"+recibici+"', "+
                               " '"+recibicargo+"', '"+observacionesgenerales+"', "+
                               " current_date(),1, 'Realizado')";
            return cnx.ejecutarMySql(consulta);

        }

        internal DataSet get_UltimoInsertado(string nombreResponsable, string edificio)
        {
            string consulta = "select max(codigo) from tb_r244registro where "+
                               " tb_r244registro.responsable = '"+nombreResponsable+"' and "+
                               " tb_r244registro.edificio = '"+edificio+"'";
            return cnx.consultaMySql(consulta);
        }

        internal DataSet get_cantidadPreguntas()
        {
            string consulta = "select count(*) from tb_r244preguntas";
            return cnx.consultaMySql(consulta);
        }

        internal bool insertarRespuestaEncuestaR244(int codpregunta, int codigoRegistro, bool p_1, bool p_2, bool p_3, string observacionDetalle)
        {
            string consulta = "insert into tb_r244respuesta(codr244respuesta,codr244registro, " +
                               " bueno, regular, malo,observacion) values(" + codpregunta + "," + codigoRegistro + ", " +
                               p_1 + " ," + p_2 + ", " + p_3 + ", '" + observacionDetalle + "')";
            return cnx.ejecutarMySql(consulta);
        }
    }
}