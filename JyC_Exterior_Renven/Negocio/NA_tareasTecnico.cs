using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_tareasTecnico
    {
        private DA_tareasTecnicos Dtecnico = new DA_tareasTecnicos();
        public NA_tareasTecnico() { }

        public bool insertTareas(string detalleTarea, int codUserInicio, int codEdificio, string nombreEdificio)
        {
            return Dtecnico.insertTareas(detalleTarea, codUserInicio, codEdificio,  nombreEdificio);
        }

        public bool deleteTareas(int codTarea)
        {
            return Dtecnico.deleteTareas(codTarea);
        }

        public bool updateTarea(int codTarea, string detalleTarea)
        {
            return Dtecnico.updateTarea(codTarea, detalleTarea);
        }

        public bool updateTareaTecnicoAsignado(int codTarea, int codUserAsignado)
        {
            return Dtecnico.updateTareaTecnicoAsignado(codTarea, codUserAsignado);
        }

        public bool insertarDetalleTareaTecnico(int codTarea, int codresponsable)
        {
            return Dtecnico.insertarDetalleTareaTecnico(codTarea, codresponsable);
        }


        public int codultimaTareaInsertada()
        {
            try
            {
                string consulta = "select max(t.codigo) from tb_tareastecnico t";
                DataSet dato = Dtecnico.getDatos(consulta);
                int resultado = Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
                return resultado;
            }
            catch {
                return -1;
            }
        }


        public DataSet mostrarTareas(string detalleTarea, string nombreTecnico, string nombreEdificio, bool estado) {
           /* string consulta = "select t.codigo,t.nombreEdificio,date_format(t.fecha,'%d/%m/%y') as 'fecha',t.hora, t.detalle, res.nombre as 'TecnicoAsignado' " +
                               " from  " +
                               " tb_tareastecnico t " +
                               " left join tb_responsable res  on t.coduserultimoasignado = res.codigo " +
                               " where  " +
                               " t.estado = "+estado+" and "+                               
                               " t.detalle like '%" + detalleTarea + "%' "; */
            string consulta = "select t.codigo,t.nombreEdificio,date_format(t.fecha,'%d/%m/%y') as 'fecha',t.hora, t.detalle, " +
                               " res.nombre as 'PersonalAsignado' , " +
                               " res1.nombre as 'InicioTarea',  " +
                               " t.detallecierre, " +
                               " res2.nombre as 'CierreTarea', " +
                               " date_format(t.fechacierre,'%d/%m/%y') as 'CierreFecha', " +
                               " t.horacierre " +
                               " from    " +
                               " tb_tareastecnico t  " +
                               " left join tb_responsable res  on t.coduserultimoasignado = res.codigo  " +
                               " left join tb_responsable res1  on t.coduserinicio = res1.codigo " +
                               " left join tb_responsable res2  on t.codusercierre = res2.codigo " +
                               " where " +
                               " t.estado = " + estado + " and " +
                               " t.detalle like '%" + detalleTarea + "%' ";
                               
            if(!nombreTecnico.Equals("")){
                consulta = consulta + " and res.nombre like '%" + nombreTecnico + "%'";
            }

            if (!nombreEdificio.Equals("")) {
               consulta = consulta + "and t.nombreEdificio like '%" + nombreEdificio + "%' ";
            }
                               
            return Dtecnico.getDatos(consulta);
        }


        public DataSet getTecnicosAsignados(int codTarea) {
            string consulta = "select res.codigo, res.nombre " +
                               " from tb_detalle_tareatecnico tec, tb_responsable res " +
                               " where  " +
                               " tec.codres = res.codigo and " +
                               " tec.codtar = " + codTarea +
                               " order by TIMESTAMP(tec.fechaasig,tec.horaasig) desc";
              return Dtecnico.getDatos(consulta);
        }

        public DataSet getTecnicoAsignado(int codTarea, int codTecnico)
        {
            string consulta = "select date_format(tec.fechaasig,'%d/%m/%Y'),tec.horaasig, tec.detalle "+
                               " from tb_detalle_tareatecnico tec "+
                               " where "+
                               " tec.codres = "+codTecnico+" and "+
                               " tec.codtar = "+codTarea;
            return Dtecnico.getDatos(consulta);
        }


        public bool updateObservacionTecnico(int codtarea, int codtecnico, string detalle)
        {
            return Dtecnico.updateObservacionTecnico(codtarea, codtecnico, detalle);
        }

        public bool updateCierreTareaTecnico(int codTarea, string detalleCierre, int codUserCierre)
        {

            return Dtecnico.updateCierreTareaTecnico(codTarea, detalleCierre, codUserCierre);
        }


        public bool updateDetalleTareaCoti(int codTarea, int codCoti)
        {
            return Dtecnico.updateDetalleTareaCoti( codTarea,  codCoti);
        }


    }
}