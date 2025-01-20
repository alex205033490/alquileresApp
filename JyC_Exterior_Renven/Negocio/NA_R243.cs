using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JyC_Exterior.Datos;

namespace JyC_Exterior.Negocio
{
    public class NA_R243
    {
        DA_R243 r243 = new DA_R243();
        public NA_R243() { }


        internal bool insertarRegistro220(int codUser, string nombreResponsable, string edificio, string exbo, int codEquipo, string observacionesGenerales, string fiscal, string instaladorFase1, string instaladorFase2)
        {
            return r243.insertarRegistro220(codUser, nombreResponsable, edificio, exbo, codEquipo, observacionesGenerales, fiscal, instaladorFase1, instaladorFase2);
        }

        internal int get_UltimoInsertado()
        {
            DataSet tupla = r243.get_UltimoInsertadoDato();
            if (tupla.Tables[0].Rows.Count > 0)
            {
                return int.Parse(tupla.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }

        internal bool insertarEncuestaR243(int codigoUltimoInsertado, int codigoPregunta, bool cumple, bool nocumple, bool noaplica, string observacion)
        {
            return r243.insertarEncuestaR243(codigoUltimoInsertado, codigoPregunta, cumple, nocumple, noaplica, observacion);
        }

        internal int get_cantidadPreguntas()
        {
            DataSet tuplas = r243.get_CantidadPreguntasTotal();
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                return int.Parse(tuplas.Tables[0].Rows[0][0].ToString());
            }
            else
                return 0;
        }

        internal string get_Pregunta(int codigo)
        {
            DataSet tuplas = r243.get_Pregunta_R220(codigo);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                return tuplas.Tables[0].Rows[0][1].ToString();
            }
            else
                return "Ninguno";
        }

        internal DataSet getObservacionesConDatos(string nombreProyecto, int codEquipo)
        {
            return r243.getObservacionesConDatos(nombreProyecto, codEquipo);
        }

        internal string getObservacionesGeneral(string nombreProyecto, int codEquipo)
        {
            DataSet tupla = r243.getObservacionesGeneral(nombreProyecto, codEquipo);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                return tupla.Tables[0].Rows[0][1].ToString();
            }
            else
                return "Ninguno";
        }

    }
}