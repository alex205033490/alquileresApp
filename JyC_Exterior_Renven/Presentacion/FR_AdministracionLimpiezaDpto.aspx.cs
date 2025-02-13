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
using AjaxControlToolkit;
using System.Globalization;
using jycboliviaASP.net.Negocio;


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




        protected void gv_listRegistrosVisitas_SelectedIndexChanged(object sender, EventArgs e)
        {
            NA_AdmLimpiezaDpto negocio = new NA_AdmLimpiezaDpto();

            int codigo = Convert.ToInt32(gv_listRegistrosVisitas.SelectedDataKey.Value);

            DataSet detalles = negocio.get_detRegistroItems(codigo);

            gv_listItemsVisita.DataSource = detalles;

            gv_listItemsVisita.DataBind();
        }

        // UPDATE CANTIDA Reposicion insumos
        protected void btn_updateInsumos_Click(object sender, EventArgs e)
        {
            NA_AdmLimpiezaDpto negocio = new NA_AdmLimpiezaDpto();

            bool resultadoGeneral = true;

            try
            {
                if (gv_listItemsVisita.Rows.Count == 0)
                {
                    showalert($"No actualizado. No hay registros para actualizar");
                    return;
                }
                int codResponsable = obtenerCodigoResponsable();

                foreach (GridViewRow row in gv_listItemsVisita.Rows)
                {
                    int codInsumo = Convert.ToInt32(row.Cells[1].Text);
                    int codRegistro = Convert.ToInt32(row.Cells[0].Text);

                    TextBox txtCantidad = (TextBox)row.FindControl("txt_cantidad");
                    string cantidadTexto = txtCantidad.Text.Replace(",", ".");
                    
                    decimal nuevaCantidad;

                    if(decimal.TryParse(cantidadTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out nuevaCantidad))
                    {
                        bool resultado = negocio.ModificarDetCantInsumos(nuevaCantidad, codResponsable, codRegistro, codInsumo);

                        if (!resultado)
                        {
                            resultadoGeneral = false;
                        }
                    }
                    else
                    {
                        showalert($"Cantidad inválida en el codigo Item: {codInsumo}.");
                        return;
                    }
                }
                if (resultadoGeneral)
                {
                    showalert("Actualización realizada con éxito");
                } else
                {
                    showalert("Hubo un error al actualizar algunos registros.");
                }
            }
            catch (FormatException)
            {
                showalert("Por favor, ingrese valores válidos.");

            }
            catch(Exception ex)
            {
                showalert($"Ocurrio un error inesperado: {ex.Message}");
            }
        }
        private int obtenerCodigoResponsable()
        {
            try
            {
                NA_Responsables negocio = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                return negocio.getCodUsuario(usuarioAux, passwordAux);
            }
            catch(Exception ex)
            {
                showalert($"Error al obtener el codigo del responsable: {ex.Message} ");
                return 0;
            }
        }

        // limpiar
        private void LimpiarForm()
        {
            txt_dpto.Text = string.Empty;
        }
        private void LimpiarGvDetalles()
        {
            gv_listItemsVisita.DataSource = null;
            gv_listItemsVisita.DataBind();
        }
        private void showalert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            LimpiarForm();
            LimpiarGvDetalles();
            mostrarRegistrosDVisitas();
        }
    }
}