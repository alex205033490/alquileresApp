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

            string edificio = txt_dpto.Text;
            if(edificio == null)
            {
                edificio = "";
            }

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
                LimpiarGvDetalles();
            }
            else
            {
                mostrarRegistrosDVisitas();
                LimpiarGvDetalles();
            }
        }

        protected void btn_buscarDpto_Click(object sender, EventArgs e)
        {
            mostrarRegistrosDVisitas();
        }
        // btn Anular registros
        protected void btn_anular_Click(object sender, EventArgs e)
        {
            NA_AdmLimpiezaDpto negocio = new NA_AdmLimpiezaDpto();
            List<int> seleccionados = new List<int>();

            foreach(GridViewRow row in gv_listRegistrosVisitas.Rows)
            {
                CheckBox chkAnular = (CheckBox)row.FindControl("chk_anularVisita");
                
                if (chkAnular != null && chkAnular.Checked)
                {
                    int nroRegistro = Convert.ToInt32(gv_listRegistrosVisitas.DataKeys[row.RowIndex].Value);
                    seleccionados.Add(nroRegistro);
                }
            }

            if (seleccionados.Count > 0)
            {
                bool exito = negocio.update_estadoRegistroDVisita(seleccionados);

                if (exito)
                {
                    LimpiarForm();
                    LimpiarGvDetalles();
                    gv_listRegistrosVisitas.DataBind();
                    showalert("El registro se ha anulado correctamente.");
                    mostrarRegistrosDVisitas();
                }
                else
                {
                    showalert($"hubo un error al anular el registro. {seleccionados}");
                }
            }
            else
            {
                showalert("Por favor, seleccione al menos un registro");
            }

        }
        private void showalert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
        private void LimpiarForm()
        {
            txt_dpto.Text = string.Empty;
        }

        private void LimpiarGvDetalles()
        {
            gv_listItemsVisita.DataSource = null;
            gv_listItemsVisita.DataBind();
        }



        protected void gv_listRegistrosVisitas_SelectedIndexChanged(object sender, EventArgs e)
        {
            NA_AdmLimpiezaDpto negocio = new NA_AdmLimpiezaDpto();

            int codigo = Convert.ToInt32(gv_listRegistrosVisitas.SelectedDataKey.Value);

            DataSet detalles = negocio.get_detRegistroItems(codigo);

            gv_listItemsVisita.DataSource = detalles;

            gv_listItemsVisita.DataBind();
        }
    }
}