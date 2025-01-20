using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JyC_Exterior.Datos;
using System.Data;

namespace JyC_Exterior.Negocio
{
    public class NA_R244
    {
        DA_R244 rd244 = new DA_R244();
        public NA_R244() { }



        internal bool insertarRegistro244(string fiscal, string RCC, int codresgra, string responsable, int codedificio, string edificio,
                                         int cantEquipos, string realizadopor, string recibiconforme, string recibici, string recibicargo,
                                         string observacionesgenerales)
        {
            return rd244.insertarRegistro244( fiscal,  RCC,  codresgra,  responsable,  codedificio,  edificio,
                                          cantEquipos,  realizadopor,  recibiconforme,  recibici,  recibicargo,
                                          observacionesgenerales);
        }

        internal int get_UltimoInsertado(string nombreResponsable, string edificio)
        {
            int codigo = 0;
            DataSet dato = rd244.get_UltimoInsertado( nombreResponsable,  edificio);
            if(dato.Tables[0].Rows.Count > 0){
                int.TryParse(dato.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }else
                return codigo;
        }

        internal int get_cantidadPreguntas()
        {
            DataSet tuplas = rd244.get_cantidadPreguntas();
            int cantidad = 0;
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                int.TryParse(tuplas.Tables[0].Rows[0][0].ToString(), out cantidad);
                return cantidad;
            }
            else
                return cantidad;
        }

        internal bool insertarRespuestaEncuestaR244(int codpregunta, int codigoRegistro, bool p_1, bool p_2, bool p_3, string observacionDetalle)
        {
            return rd244.insertarRespuestaEncuestaR244(codpregunta, codigoRegistro, p_1, p_2, p_3, observacionDetalle);
        }
    }
}