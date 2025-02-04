using JyC_Exterior.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Presentacion;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;


namespace JyC_Exterior.Presentacion
{
    public partial class FR_AdmLimpiezaDpto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                mostrarRegistrosDVisitas();
            }
        }



        private void mostrarRegistrosDVisitas()
        {
            string edificio = "";

            NA_AdmLimpiezaDpto negocio = new NA_AdmLimpiezaDpto();
            DataSet tuplas = negocio.get_ListRegistroDVisitas(edificio);

            if (tuplas.Tables[0].Rows.Count > 0)
            {
                gv_listRegistrosVisitas.DataSource = tuplas;
                gv_listRegistrosVisitas.DataBind();
                gv_listRegistrosVisitas.Visible = true;
            }
            else
            {
                gv_listRegistrosVisitas.DataSource = null;
                gv_listRegistrosVisitas.DataBind();
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] GetAutoCompletListRegistros(string prefixText, int count)
        {
            string dpto = prefixText;
            
            NA_AdmLimpiezaDpto negocio = new NA_AdmLimpiezaDpto();
            DataSet tuplas = negocio.get_ListRegistroDVisitas(dpto);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for(int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }

            return lista;


        }

        protected void txt_dpto_TextChanged(object sender, EventArgs e)
        {
            string dpto = txt_dpto.Text.Trim();

            NA_AdmLimpiezaDpto negocio = new NA_AdmLimpiezaDpto();
            DataSet tuplas = negocio.get_ListRegistroDVisitas(dpto);

            if(tuplas.Tables[0].Rows.Count > 0)
            {
                gv_listRegistrosVisitas.DataSource = tuplas;
                gv_listRegistrosVisitas.DataBind();
                gv_listRegistrosVisitas.Visible = true;
            }
            else
            {
                mostrarRegistrosDVisitas();
            }



        }

        protected void btn_buscarDpto_Click(object sender, EventArgs e)
        {
            mostrarRegistrosDVisitas();
        }

        protected void btn_anular_Click(object sender, EventArgs e)
        {

        }
    }
}