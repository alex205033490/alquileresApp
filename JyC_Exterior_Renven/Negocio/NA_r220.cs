using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JyC_Exterior.Datos;
using System.Data;

namespace JyC_Exterior.Negocio
{
    public class NA_r220
    {
        DA_r220 r220 = new DA_r220();
        public NA_r220() { }


        internal bool insertarRegistro220(int codUser, string nombreResponsable, string edificio, string exbo, int codEquipo, string observacionesGenerales, string fiscal, string instaladorFase1, string instaladorFase2, string fechaEstablecida, string estadoEquipo)
        {
            return r220.insertarRegistro220(codUser, nombreResponsable, edificio, exbo, codEquipo, observacionesGenerales, fiscal, instaladorFase1, instaladorFase2, fechaEstablecida, estadoEquipo);
        }

        internal int get_UltimoInsertado()
        {
            DataSet tupla = r220.get_UltimoInsertadoDato();
            if (tupla.Tables[0].Rows.Count > 0)
            {
                return int.Parse(tupla.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }



        internal bool insertarEncuestaR220(int codigoUltimoInsertado, int codigoPregunta, bool conforme, string observacion)
        {
            return r220.insertarEncuestaR220( codigoUltimoInsertado,  codigoPregunta,  conforme,  observacion);
        }

        internal int get_cantidadPreguntas()
        {
            DataSet tuplas = r220.get_CantidadPreguntasTotal();
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                return int.Parse(tuplas.Tables[0].Rows[0][0].ToString());
            }
            else
                return 0;
        }

        internal string get_Pregunta(int codigo)
        {
            DataSet tuplas = r220.get_Pregunta_R220(codigo);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                return tuplas.Tables[0].Rows[0][1].ToString();
            }
            else
                return "Ninguno";
        }

        internal DataSet getObservacionesConDatos(string nombreProyecto, int codEquipo)
        {
           return r220.getObservacionesConDatos(nombreProyecto, codEquipo);
        }

        internal string getObservacionesGeneral(string nombreProyecto, int codEquipo)
        {
            DataSet tupla = r220.getObservacionesGeneral(nombreProyecto, codEquipo);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                return tupla.Tables[0].Rows[0][1].ToString();
            }
            else
                return "Ninguno";
        }
    }
}