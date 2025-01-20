using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_RutaMantenimiento
    {
        private conexionMySql ConecRes = new conexionMySql();
        
        public DA_RutaMantenimiento() { }

        public DataSet getallRutas(int mes, int anio) {
            string consulta = "select "+
                               " ru.codigo, "+
                               " ru.numero, "+
                               " ru.detalle, "+
                               " ru.fecha, "+
                               " ru.hora, "+
                               " ru.mes, "+
                               " ru.anio "+
                               " from tb_ruta ru "+
                               " where  "+
                               " ru.mes = "+mes+" and "+
                               " ru.anio = "+anio;
            return ConecRes.consultaMySql(consulta);
        }


        public DataSet getallEquiposProyectoRutaMantenimiento(string edificio)
        {
            string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo, "+
                               " proy.nombre as 'Edificio', "+
                               " te.nombre as 'Tipo', "+
                               " m.nombre as 'Marca' "+
                               " from  tb_proyecto proy, "+
                               " tb_fechaestadoequipo ff,  "+
                               " tb_equipo eq  "+
                               " left join tb_marca m on eq.codmarca = m.codigo "+
                               " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo "+
                               " where  "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " eq.codfechaestadoequipo = ff.codigo and " +
                               " ff.codEstadoEquipo = 10 and "+
                               " proy.nombre like '%"+edificio+"%'";
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getallEquiposProyecto(string edificio) {
            string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo, "+
                               " proy.nombre as 'Edificio', "+
                               " te.nombre as 'Tipo', "+
                               " m.nombre as 'Marca' "+
                               " from  tb_proyecto proy, "+
                               " tb_equipo eq  "+
                               " left join tb_marca m on eq.codmarca = m.codigo "+
                               " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo "+
                               " where  "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " proy.nombre like '%"+edificio+"%'";
            return ConecRes.consultaMySql(consulta);
        }


        public DataSet getAllFaltantesEquiposSinRutas(string Edificio, int mes, int anio) {
            string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo, "+
                               " proy.nombre as 'Edificio', "+
                               " te.nombre as 'Tipo', "+
                               " m.nombre as 'Marca' "+
                               " from  tb_proyecto proy, "+
                               " tb_seguimiento seg, "+
                               " tb_fechaestadoequipo feq, "+
                               " tb_equipo eq   "+
                               " left join tb_marca m on eq.codmarca = m.codigo "+
                               " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo "+
                               " left join "+
                               " ( "+
                               " select re.codeq from tb_cronogramavisitarutamanteminieto re  "+
                               " where re.codmes = "+mes+" and re.anio = "+anio+
                               " group by re.codeq "+
                               " ) as t1  "+
                               " ON eq.codigo = t1.codeq "+
                               " where "+
                               " t1.codeq is null and "+
                               " eq.codigo = seg.cod_equipo and "+
                               " seg.years = "+anio+" and "+
                               " eq.estado = 1 and "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " eq.codfechaestadoequipo = feq.codigo and "+
                               " feq.codEstadoEquipo = 10 and "+
                               " proy.nombre like '%"+Edificio+"%' "+
                               " order by eq.codigo asc";
            return ConecRes.consultaMySql(consulta);
        }


        public bool insertarRutaMantenimiento(int nro, string detalle, int mes, int anio)
        {
            string consulta = "insert into tb_ruta(tb_ruta.nro,tb_ruta.detalle,tb_ruta.fecha,tb_ruta.hora,tb_ruta.estado,tb_ruta.mes,tb_ruta.anio) " +
                               " values("+nro+",'"+detalle+"',now(),now(),1,"+mes+","+anio+");";
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getallRutaMantenimiento(string nombre, int mes , int anio) {
            string consulta = "select "+ 
                               " ru.codigo, "+
                               " ru.nro as 'NRO', " +
                               " ru.detalle, "+
                               " date_format(ru.fecha,'%d/%m/%Y') as 'FechaCreacion', "+
                               " ru.hora as 'HoraCreacion' "+
                               " from tb_ruta ru "+
                               " where ru.detalle like '%"+nombre+"%' "+
                               " and ru.mes = "+mes+ " and ru.anio= "+anio;
            return ConecRes.consultaMySql(consulta);
        }


        public DataSet getallRutaMantenimiento2(string codigo, string nombre, int mes, int anio)
        {
            string consulta = "select " +
                               " ru.codigo, " +
                               " ru.detalle, " +
                               " date_format(ru.fecha,'%d/%m/%Y') as 'FechaCreacion', " +
                               " ru.hora as 'HoraCreacion', " +
                               " ru.mes, ru.anio "+
                               " from tb_ruta ru where ru.detalle like '%" + nombre + "%' and "+
                               " ru.codigo like '%"+codigo+"' and "+
                               " ru.mes = " + mes + " and ru.anio= " + anio; 
            return ConecRes.consultaMySql(consulta);
        }


         public DataSet getallEquiposRutasAsignadas(int codRuta, string nombreEdificio)
        {
            string consulta = "select   "+
                               " eq.codigo,  "+
                               " proy.nombre as 'Edificio', "+
                               " eq.exbo,  "+
                               " eq.ascensor, "+ 
                               " te.nombre as 'Tipo',   "+
                               " m.nombre as 'Marca', "+
                               " dre.horaentrada,  "+
                               " dre.horasalida,  "+
                               " TIMEDIFF(dre.horasalida,dre.horaentrada) as 'TiempoTotal', "+
                               " dre.nrodia, "+ 
                               " dre.diasemana, "+  
                               " dre.nrovisita, "+
                               " dre.pasaje, "+
                               " dre.semana1, "+
                               " dre.semana2, "+
                               " dre.semana3, "+
                               " dre.semana4 "+
                               " from tb_cronogramavisitarutamanteminieto dre, tb_proyecto proy,   "+
                               " tb_equipo eq "+
                               " left join tb_marca m on eq.codmarca = m.codigo  "+
                               " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo  "+
                               " where "+
                               " dre.codeq = eq.codigo and "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " dre.codruta = "+codRuta+" and "+
                               " proy.nombre like '%"+nombreEdificio+"%'"+ 
                               " order by dre.nrodia, dre.horaentrada asc";                                 
            return ConecRes.consultaMySql(consulta);
        }


         public DataSet getallEquiposRutasAsignadas2(string exbo, string edificio, int mes , int anio)
         {
             string consulta =     "select "+
                                   " eq.codigo, "+
                                   " eq.exbo, "+
                                   " proy.nombre as 'Edificio', "+
                                   " te.nombre as 'Tipo',   "+
                                   " m.nombre as 'Marca',  "+
                                   " dre.horaentrada,  "+
                                   " dre.horasalida,  "+
                                   " TIMEDIFF(dre.horasalida,dre.horaentrada) as 'TiempoTotal', "+
                                   " dre.diasemana, "+
                                   " dre.nrovisita "+
                                   " from tb_cronogramavisitarutamanteminieto dre, tb_proyecto proy, "+
                                   " tb_equipo eq  "+
                                   " left join tb_marca m on eq.codmarca = m.codigo  "+
                                   " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo  "+
                                   " where  "+
                                   " dre.codeq = eq.codigo and "+
                                   " eq.cod_proyecto = proy.codigo and  "+                                   
                                   " dre.codmes = "+mes+" and "+
                                   " dre.anio = "+anio+
                                   " and eq.exbo like '%"+exbo+"%' and  "+
                                   " proy.nombre like '%"+edificio+"%' "+
                                   " order by dre.nrodia,dre.horaentrada asc ";
             return ConecRes.consultaMySql(consulta);
         }


         public bool modificarRutaMantenimiento(int codigo, int nro , string detalle, int mes, int anio)
         {
             string consulta = "update tb_ruta set tb_ruta.detalle = '"+detalle+"',"+
                                " tb_ruta.nro = "+nro+        
                                " where tb_ruta.codigo = "+codigo+
                                " and tb_ruta.mes= "+mes+
                                " and tb_ruta.anio="+anio;
             return ConecRes.ejecutarMySql(consulta);
         }

   /*      public bool insertarEquipoRuta(int codRuta, int codEquipo, string horaEntrada, string horasalida, string dia, int cantvisitas, int nrodia, bool semana1, bool semana2, bool semana3, bool semana4, float pasaje)
         {
             string consulta = "insert tb_detalle_rutaequipo( "+
                                " tb_detalle_rutaequipo.codruta, "+
                                " tb_detalle_rutaequipo.codequipo, "+
                                " tb_detalle_rutaequipo.horaentrada, "+
                                " tb_detalle_rutaequipo.horasalida, "+
                                " tb_detalle_rutaequipo.diasemana, "+
                                " tb_detalle_rutaequipo.cantvisita, "+
                                " tb_detalle_rutaequipo.fecha, "+
                                " tb_detalle_rutaequipo.hora, "+
                                " tb_detalle_rutaequipo.nrodia, "+
                                " tb_detalle_rutaequipo.semana1, "+
                                " tb_detalle_rutaequipo.semana2, "+
                                " tb_detalle_rutaequipo.semana3, "+
                                " tb_detalle_rutaequipo.semana4, "+
                                " tb_detalle_rutaequipo.pasaje " +
                                " ) values "+
                                " ( "+
                                codRuta+" , "+
                                codEquipo+" , "+
                                "'"+horaEntrada+"', "+
                                "'"+horasalida+"' , "+
                                "'"+dia+"' , "+
                                cantvisitas+" , "+
                                " now(), "+
                                " now(), "+
                                nrodia+ " , "+
                                semana1+ ", "+
                                semana2+", "+
                                semana3+", "+
                                semana4+", "+
                                "'"+pasaje.ToString().Replace(',','.')+"'"+
                                ")";
             return ConecRes.ejecutarMySql(consulta);
         }*/


      /*   public bool ModificarEquipoRuta(int codRuta, int codEquipo, string horaEntrada, string horasalida, string dia, int cantvisitas, int nrodia, bool semana1, bool semana2, bool semana3, bool semana4, float pasaje)
         {
             string consulta = "update tb_detalle_rutaequipo " +
                                " set " +
                                " tb_detalle_rutaequipo.horaentrada = '" + horaEntrada + "', " +
                                " tb_detalle_rutaequipo.horasalida = '" + horasalida + "', " +
                                " tb_detalle_rutaequipo.diasemana = '" + dia + "', " +
                                " tb_detalle_rutaequipo.cantvisita = " + cantvisitas + ", " +
                                " tb_detalle_rutaequipo.nrodia = " + nrodia +" , "+
                                " tb_detalle_rutaequipo.semana1 = "+semana1+" , " +
                                " tb_detalle_rutaequipo.semana2 = "+semana2+", " +
                                " tb_detalle_rutaequipo.semana3 = "+semana3+", " +
                                " tb_detalle_rutaequipo.semana4 = "+semana4+", "+
                                " tb_detalle_rutaequipo.pasaje = '" + pasaje.ToString().Replace(',','.') + "' " +
                                " where " +
                                " tb_detalle_rutaequipo.codruta = " + codRuta +
                                " and tb_detalle_rutaequipo.codequipo = " + codEquipo;
             return ConecRes.ejecutarMySql(consulta);
         }*/


         public bool insertarTecnicoRuta(int codRuta, int codTecnico, string supervisor, int mes, int anio)
         {
             string consulta = "insert into tb_detalle_rutatecnicom "+
                               " ( "+
                               " tb_detalle_rutatecnicom.codruta, "+
                               " tb_detalle_rutatecnicom.codtec, "+
                               " tb_detalle_rutatecnicom.supervisor, "+                               
                               " tb_detalle_rutatecnicom.fecha, "+
                               " tb_detalle_rutatecnicom.hora, "+
                               " tb_detalle_rutatecnicom.estado, "+
                               " tb_detalle_rutatecnicom.mes, " +
                               " tb_detalle_rutatecnicom.anio " +
                               " ) values "+
                               " ( "+
                               codRuta+" , "+
                               codTecnico+" , "+
                               "'"+ supervisor+"' , "+                              
                               " now(), "+
                               " now(), "+
                               " 1 ,"+
                               + mes+ ","+
                               anio+
                               " )";
             return ConecRes.ejecutarMySql(consulta);
         }


         public DataSet mostrarTecnicoRuta(int codRuta)
         {
             string consulta = "select res.codigo, res.nombre, " +
                               " date_format(dt.fecha,'%d/%m/%Y') as 'FechaAsignacion', " +
                               " dt.hora as 'HoraAsignacion', " +
                               " dt.supervisor " +
                               " from tb_detalle_rutatecnicom dt, tb_responsable res " +
                               " where  " +
                               " dt.codtec = res.codigo and " +
                               " dt.codruta = " + codRuta;
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet mostrarTecnicoRuta(int mes, int anio)
         {
             string consulta = "select res.codigo, res.nombre, "+
                               " date_format(dt.fecha,'%d/%m/%Y') as 'FechaAsignacion', "+
                               " dt.hora as 'HoraAsignacion', "+
                               " dt.supervisor "+
                               " from tb_detalle_rutatecnicom dt, tb_responsable res "+
                               " where  "+
                               " dt.codtec = res.codigo and "+
                               " dt.mes = "+mes+" and "+
                               " dt.anio = "+anio;
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet mostrarTecnicoRuta2(string nombre, int mes, int anio)
         {
             string consulta = "select "+
                               " T1.codigo, T1.nombre, T1.FechaAsignacion, T1.HoraAsignacion, "+
                               " T1.supervisor as 'Tipo'  "+
                               " from "+
                               " ( "+
                               " select res.codigo, res.nombre, "+
                               " date_format(dt.fecha,'%d/%m/%Y') as 'FechaAsignacion', "+
                               " dt.hora as 'HoraAsignacion', "+
                               " dt.supervisor  "+
                               " from tb_detalle_rutatecnicom dt, tb_responsable res  "+
                               " where  "+
                               " dt.codtec = res.codigo and "+                               
                               " dt.mes = "+mes+" and "+
                               " dt.anio = "+anio+" and "+
                               " res.nombre like '%"+nombre+"%' "+
                               " union "+
                               " select res.codigo, res.nombre, "+
                               " '' as 'FechaAsignacion', "+
                               " '' as 'HoraAsignacion',  "+
                               " '' as 'supervisor'  "+
                               " from  tb_responsable res "+
                               " where  "+
                               " res.nombre like '%"+nombre+"%' "+
                               " ) as T1 "+
                               " group by T1.codigo";
             return ConecRes.consultaMySql(consulta);
         }

         public bool eliminarRuta(int codRuta, int mes, int anio)
         {
             string consulta = "delete from tb_ruta "+
                               " where tb_ruta.codigo = " + codRuta+
                               " and tb_ruta.mes ="+mes+ " and tb_ruta.anio="+anio;
             return ConecRes.ejecutarMySql(consulta);
         }

         public DataSet tecnicoAsignadosRuta(int codRuta) {
             string consulta = "select * from tb_detalle_rutatecnicom dt where dt.codruta = " + codRuta;
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet equiposAsignadoRuta(int codRuta) {
             string consulta = "select * from tb_detalle_rutaequipo dt where dt.codruta =" + codRuta;
             return ConecRes.consultaMySql(consulta);
         }

         public bool eliminarEquipoRutaMantemiento(int codRuta,int codEquipo)
         {
             string consulta = "delete from tb_cronogramavisitarutamanteminieto " +
                               " where "+
                               " tb_cronogramavisitarutamanteminieto.codruta = " + codRuta + " and " +
                               " tb_cronogramavisitarutamanteminieto.codeq = " + codEquipo;
             return ConecRes.ejecutarMySql(consulta);
         }

         public bool eliminarTecnicoRutaMantemiento(int codRuta, int codtecnico)
         {
             string consulta = "delete from tb_detalle_rutatecnicom  " +
                                  " where " +
                                  " tb_detalle_rutatecnicom.codruta = " + codRuta + " and " +
                                  " tb_detalle_rutatecnicom.codtec = " + codtecnico;
             return ConecRes.ejecutarMySql(consulta);         
         }


         public bool insertarBoletaMantenimiento(int codEquipo, int codTecnico, string boleta, string detalle, bool cambiorepuesto, string fechaboleta, string horallegada, string horasalida, string recepcion, bool banderaArreglo, string tipoBoleta, bool siningresoedificio)
         {
             string consulta = "insert into tb_visitadetallerutaequipo( "+
                               " tb_visitadetallerutaequipo.fecha, "+
                               " tb_visitadetallerutaequipo.hora, "+
                             //  " tb_visitadetallerutaequipo.codruta, "+
                               " tb_visitadetallerutaequipo.codequipo, "+
                               " tb_visitadetallerutaequipo.codtecnico, "+
                               " tb_visitadetallerutaequipo.boleta, "+
                               " tb_visitadetallerutaequipo.observacion, "+
                               " tb_visitadetallerutaequipo.cambiorepuesto, " +
                               " tb_visitadetallerutaequipo.fechaboleta, " +
                               " tb_visitadetallerutaequipo.horallegada, " +
                               " tb_visitadetallerutaequipo.horasalida, " +
                               " tb_visitadetallerutaequipo.recepcion, " +
                               " tb_visitadetallerutaequipo.arreglo, " +
                               " tb_visitadetallerutaequipo.tipoboleta, " +
                               " tb_visitadetallerutaequipo.siningresoedificio, "+
                               " tb_visitadetallerutaequipo.mes, "+
                               " tb_visitadetallerutaequipo.anio "+
                               " ) values( "+
                               " now(), "+
                               " now(), "+
                          //     codRuta+" , "+
                               codEquipo+" , "+
                               codTecnico+" , "+
                               "'"+boleta+"' , "+
                               "'"+detalle+"',"+
                               cambiorepuesto +" , "+
                               fechaboleta+" , "+
                               horallegada+" , "+
                               horasalida+" , "+
                               "'"+ recepcion+"',"+
                               banderaArreglo+" , "+
                               "'"+tipoBoleta+"',"+
                               siningresoedificio+","+
                               "month("+fechaboleta+"),"+
                               "year("+fechaboleta+")"+
                               ")";
             return ConecRes.ejecutarMySql(consulta);
         }

         public bool eliminarBoletaMantenimiento(int codigoBoleta) {
             string consulta = "delete from tb_visitadetallerutaequipo "+
                               " where tb_visitadetallerutaequipo.codigo = "+codigoBoleta;

             return ConecRes.ejecutarMySql(consulta);
         }


         public DataSet mostrarBoletasMantenimiento(int codequipo, int codtecnico) { 
            string consulta = "select "+
                               " ve.codigo, "+
                               " date_format(ve.fecha,'%d/%m/%Y') as 'Fecha', " +
                               " ve.hora as 'Hora', "+                               
                               " ve.boleta, "+
                               " date_format(ve.fechaboleta,'%d/%m/%Y') as 'Fecha boleta',"+
                               " ve.horallegada,"+
                               " ve.horasalida,"+
                               " ve.recepcion," +
                               " ve.cambiorepuesto," +
                               " ve.arreglo," +
                               " ve.siningresoedificio," +
                               " ve.observacion "+
                               " from tb_visitadetallerutaequipo ve "+
                               " where "+
                              // " ve.codruta = "+codruta+" and "+
                               " ve.codequipo = "+codequipo+" and "+
                               " ve.codtecnico = "+codtecnico;
            return ConecRes.consultaMySql(consulta);
         }

         public DataSet mostrarALLPersonalAsignadoRuta(int mes, int anio)
         {
             string consulta = "select " +
                               " res.nombre as 'Nombre', " +
                               " date_format(dr.fecha, '%d/%m/%Y') as 'Fecha_Asignacion', " +
                               " dr.hora as 'Hora_Asignacion', " +
                               " ru.nro as 'NroRuta', " +
                               " ru.detalle as 'Detalle', " +
                               " dr.supervisor as 'Cargo' " +
                               " from " +
                               " tb_detalle_rutatecnicom dr, tb_ruta ru, tb_responsable res " +
                               " where " +
                               " dr.codruta = ru.codigo and " +
                               " dr.codtec = res.codigo and " +
                               " dr.mes = "+mes+" and "+
                               " dr.anio = "+anio+"  "+
                               " order by ru.codigo asc";
             return ConecRes.consultaMySql(consulta);
         }


         public DataSet mostrarALLEquiposAsignadosRutas(int mes , int anio)
         {
             string consulta = "select "+
                               " ru.nro as 'NroRuta', " +
                               " re.diasemana, "+
                               " re.horaentrada as 'Hora_Entrada', "+
                               " re.horasalida as 'Hora_Salida', "+
                               " timediff(re.horasalida , re.horaentrada) as 'Total_Hora', "+
                               " proy.nombre as 'Edificio', "+
                               " eq.exbo, "+
                               " re.nrovisita , "+ 
                               " re.nrodia "+
                               " from  "+
                               " tb_cronogramavisitarutamanteminieto re, "+
                               " tb_ruta ru, "+
                               " tb_equipo eq, "+
                               " tb_proyecto proy "+ 
                               " where  "+
                               " re.codruta = ru.codigo and "+
                               " re.codeq = eq.codigo and "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " re.codmes = "+mes+" and "+
                               " re.anio = "+anio+
                               " order by ru.codigo,re.nrodia asc";
             return ConecRes.consultaMySql(consulta);
         }


         public DataSet mostrarALLEquiposAsignadosRutasFechas(int mes, int anio)
         {
             string consulta = "select  " +
                               " ru.nro as 'NroRuta', " +
                               " cc.diasemana, " +
                               " cc.horaentrada as 'Hora_Entrada', " +
                               " cc.horasalida as 'Hora_Salida', " +
                               " timediff(cc.horasalida,cc.horaentrada) as 'Total_Hora', " +
                               " proy.nombre as 'Edificio', " +
                               " eq.exbo, " +
                               " cc.nrovisita as 'cantvisita',  " +
                               " cc.nrodia, " +
                               " date_format(cc.fechas1, '%d/%m/%Y')  as 'Semana1', " +
                               " date_format(cc.fechas2, '%d/%m/%Y')  as 'Semana2', " +
                               " date_format(cc.fechas3, '%d/%m/%Y')  as 'Semana3', " +
                               " date_format(cc.fechas4, '%d/%m/%Y')  as 'Semana4' " +
                               " from  " +
                               " tb_ruta ru, " +
                               " tb_proyecto proy,  " +
                               " tb_equipo eq,   " +
                               " tb_cronogramavisitarutamanteminieto cc  " +
                               " where  " +
                               " eq.codigo = cc.codeq and " +
                               " ru.codigo = cc.codruta and " +
                               " eq.cod_proyecto = proy.codigo and " +
                               " cc.codmes = " + mes + " and cc.anio = " + anio +
                               " order by ru.codigo,cc.nrodia,cc.horaentrada asc";
             return ConecRes.consultaMySql(consulta);
         }


         public DataSet mostrarALLEquiposAsignadosRutasFechas_Reporte(int mes, int anio)
         {
             string consulta = "select  "+
                               " ru.codigo, "+
                               " ru.nro as 'NroRuta', "+
                               " cc.diasemana, "+
                               " cc.horaentrada as 'Hora_Entrada', "+
                               " cc.horasalida as 'Hora_Salida', "+
                               " timediff(cc.horasalida,cc.horaentrada) as 'Total_Hora', "+
                               " proy.nombre as 'Edificio', "+
                               " eq.exbo, "+
                               " cc.nrovisita as 'cantvisita',  " +
                               " cc.nrodia, "+
                               " date_format(cc.fechas1, '%d/%m/%Y')  as 'Semana1', "+
                               " date_format(cc.fechas2, '%d/%m/%Y')  as 'Semana2', "+
                               " date_format(cc.fechas3, '%d/%m/%Y')  as 'Semana3', "+
                               " date_format(cc.fechas4, '%d/%m/%Y')  as 'Semana4' "+
                               " from  "+
                               " tb_ruta ru, "+
                               " tb_proyecto proy,  "+
                               " tb_equipo eq,   "+
                               " tb_cronogramavisitarutamanteminieto cc  "+
                               " where  "+
                               " eq.codigo = cc.codeq and "+
                               " ru.codigo = cc.codruta and "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " cc.codmes = "+mes+" and cc.anio = "+anio+
                               " order by ru.codigo,cc.nrodia,cc.horaentrada asc";
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet mostrarALLEquiposAsignadosRutasFechas_consulta(int mes, int anio,string codRuta, string exbo, string edificio)
         {            
             string consulta = "select  " +
                               " ru.codigo as 'NroRuta', " +
                               " cc.diasemana, " +
                               " cc.horaentrada as 'Hora_Entrada', " +
                               " cc.horasalida as 'Hora_Salida', " +
                               " timediff(cc.horasalida,cc.horaentrada) as 'Total_Hora', " +
                               " proy.nombre as 'Edificio', " +
                               " eq.exbo, " +
                               " cc.nrovisita as 'cantvisita',  " +
                               " cc.nrodia, " +
                               " date_format(cc.fechas1, '%d/%m/%Y')  as 'Semana1', " +
                               " date_format(cc.fechas2, '%d/%m/%Y')  as 'Semana2', " +
                               " date_format(cc.fechas3, '%d/%m/%Y')  as 'Semana3', " +
                               " date_format(cc.fechas4, '%d/%m/%Y')  as 'Semana4' " +
                               " from  " +
                               " tb_ruta ru, " +
                               " tb_proyecto proy,  " +
                               " tb_equipo eq,   " +
                               " tb_cronogramavisitarutamanteminieto cc  " +
                               " where  " +
                               " eq.codigo = cc.codeq and " +
                               " ru.codigo = cc.codruta and " +
                               " eq.cod_proyecto = proy.codigo and " +
                               " proy.nombre like '%"+edificio+"%' and "+
                               " eq.exbo like '%"+exbo+"%' and "+
                               " ru.codigo like '%"+codRuta+"%' and "+ 
                               " cc.codmes = " + mes + " and cc.anio = " + anio +
                               " order by ru.codigo,cc.nrodia,cc.horaentrada asc";
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet getcronogramaMesAnioRuta(int mes, int anio) {
             string consulta = "select * from tb_cronogramavisitarutamanteminieto crovi "+
                               " where crovi.codmes = "+mes+" and "+
                               " crovi.anio = "+anio;
             return ConecRes.consultaMySql(consulta);     
         }

         public DataSet getcronogramaMesAnioRuta_porNroVisita(int mes, int anio,int nrovisita)
         {
             string consulta = "select * from tb_cronogramavisitarutamanteminieto crovi " +
                               " where crovi.codmes = " + mes + " and " +
                               " crovi.anio = " + anio + " and crovi.nrovisita = "+nrovisita;
             return ConecRes.consultaMySql(consulta);
         }

       /*  public bool modificarCronogramaEquipoRutaMantemiento(int codRuta, int codEquipo, int codCronograma)
         {
             string consulta = "update tb_detalle_rutaequipo set tb_detalle_rutaequipo.codcrono = "+codCronograma+
                               " where  "+
                               " tb_detalle_rutaequipo.codruta = "+codRuta+" and "+
                               " tb_detalle_rutaequipo.codequipo = "+codEquipo;
             return ConecRes.ejecutarMySql(consulta);
         }*/


     /*    public int getCodigoCronoRutaMantenimiento(int codRuta, int codEquipo)
         {
             string consulta = "select "+
                               " rr.codcrono "+
                               " from tb_detalle_rutaequipo rr "+
                               " where "+
                               " rr.codruta = "+codRuta+" and "+
                               " rr.codequipo = "+codEquipo;
             DataSet dato = ConecRes.consultaMySql(consulta);
             int numero = 0;
             string numeroSTR = dato.Tables[0].Rows[0][0].ToString();
             bool siEsNumero = int.TryParse(numeroSTR, out numero);
             if (siEsNumero == true)
             {
                 return numero;
             }
             else
                 return -1;
         }
        */

         public DataSet getdetalleRutaEquipoCrono(int codRuta, int codEquipo)
         {
             string consulta = "select "+
                               " dd.codruta, "+
                               " dd.codequipo, "+
                               " dd.horaentrada, "+
                               " dd.horasalida, "+
                               " dd.nrodia, "+
                               " dd.cantvisita, "+
                               " dd.semana1, "+
                               " dd.semana2, "+
                               " dd.semana3, "+
                               " dd.semana4, "+
                               " cc.fechas1, "+
                               " cc.fechas2, "+
                               " cc.fechas3, "+
                               " cc.fechas4  "+
                               " from tb_detalle_rutaequipo dd "+
                               " left join tb_cronogramavisitarutamanteminieto cc on dd.codcrono = cc.codigo "+
                               " where "+
                               " dd.codruta = " + codRuta + " and " +
                               " dd.codequipo = " + codEquipo;
             return ConecRes.consultaMySql(consulta);
         }

 public DataSet getdetalleRutaEquipoTodos(int mes, int anio)
         {
             string consulta = "select "+
                               " dd.codruta,  "+
                               " dd.codeq, "+
                               " dd.horaentrada, "+
                               " dd.horasalida, "+
                               " dd.diasemana, "+
                               " dd.nrodia, "+
                               " dd.nrovisita, "+ 
                               " dd.semana1, "+
                               " dd.semana2, "+
                               " dd.semana3, "+
                               " dd.semana4, "+
                               " dd.codigo, "+
                               " dd.pasaje "+
                               " from tb_cronogramavisitarutamanteminieto dd "+
                               " where "+
                               " dd.codmes = "+mes+" and "+
                               " dd.anio = "+anio;
                               
             return ConecRes.consultaMySql(consulta);
         }  

         public int getcantidadColumnasFechasTotalRutas(int mes, int anio) {
             string consulta = "select "+
                               " max(t1.cantvisita) "+
                               " from "+
                               " ( "+
                               " select "+
                               " count(*) as 'cantvisita' "+
                               " from "+
                               " tb_visitadetallerutaequipo vv "+
                               " where  "+
                               " month(vv.fechaboleta) = "+mes+" and "+
                               " year(vv.fechaboleta) = "+anio+
                               " group by "+
                               " (vv.codequipo) "+
                               " ) as t1";
             DataSet dato = ConecRes.consultaMySql(consulta);
             if (dato.Tables[0].Rows.Count > 0)
             {
                 return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
             }
             else
                 return 0;
         }

         public DataSet getFechasBoletasRutas(int codRuta, int codEquipo, int mes, int anio, string tipoBoleta)
         {

             string consulta = "select " +
                               " Concat(date_format(vv.fechaboleta, '%d/%m/%Y'),' ',res.nombre,' ',if(vv.siningresoedificio=1,' No_Ingreso','')) as 'fecha_boleta' " +
                               " from " +
                               " tb_visitadetallerutaequipo vv " +
                               " , tb_responsable res "+
                               " where  " +
                               " vv.codtecnico = res.codigo and "+
                            //   " vv.codruta = " + codRuta + " and " +
                               " vv.codequipo = " + codEquipo + " and " +
                               " vv.tipoboleta = '" + tipoBoleta + "' and " +
                               " month(vv.fechaboleta) = " + mes + " and " +
                               " year(vv.fechaboleta) = " + anio +
                               " order by (vv.fechaboleta) asc ";
             DataSet dato = ConecRes.consultaMySql(consulta);
             return dato;
         }


         public DataSet  getFechasBastones(int codEquipo, int mes, int anio, string tipoBoleta)
         {

             string consulta = "select " +
                               " CAST(CONCAT(date_format(bb.fechabaston, '%d/%m/%Y'),' ',bb.horabaston,' ',res.nombre) AS CHAR) as fecha_boleta " +
                               " from " +
                               " tb_bastones bb, tb_responsable res, tb_equipo eq " +
                               " where " +
                               " bb.codequipo = eq.codigo and " +
                               " bb.codtecnico = res.codigo and " +
                               " bb.horabaston <> 0  and " +
                               " bb.codequipo = " + codEquipo + " and " +
                               " month(bb.fechabaston) = " + mes + " and " +
                               " year(bb.fechabaston) = " + anio +
                               " order by TIMESTAMP(bb.fechabaston,bb.horabaston) asc ";
                               
                               
             DataSet dato = ConecRes.consultaMySql(consulta);
             return dato;
         }


         public DataSet mostrarConsultaEquiposAsignadosRutasFechas(string codRuta, string exbo, int mes, int anio, string nombreProyecto, string personalAsignado)
         {
             string consulta = "select  "+
                                   " ru.codigo as 'NroRuta', "+
                                   " re.diasemana, "+
                                   " re.horaentrada as 'Hora_Entrada', "+
                                   " re.horasalida as 'Hora_Salida', "+
                                   " timediff(re.horasalida,re.horaentrada) as 'Total_Hora', "+
                                   " proy.nombre as 'Edificio', "+
                                   " eq.exbo, "+
                                   " re.cantvisita,  "+
                                   " date_format(cc.fechas1, '%d/%m/%Y')  as 'Semana1', "+
                                   " date_format(cc.fechas2, '%d/%m/%Y')  as 'Semana2', "+
                                   " date_format(cc.fechas3, '%d/%m/%Y')  as 'Semana3', "+
                                   " date_format(cc.fechas4, '%d/%m/%Y')  as 'Semana4', "+
                                   " T1.Supervisor as 'Personal Asignado', "+
                                   " T1.tipo "+
                                   " from  "+
                                   " tb_proyecto proy, "+
                                   " tb_detalle_rutaequipo re, "+
                                   " tb_equipo eq   "+
                                   " left join tb_cronogramavisitarutamanteminieto cc on (cc.codeq = eq.codigo), "+
                                   " tb_ruta ru "+
                                   " LEFT JOIN "+
                                   " ( "+
                                   " select rt.codruta, res.nombre as 'Supervisor', rt.supervisor as 'tipo' "+
                                   " from tb_detalle_rutatecnicom rt, tb_responsable res "+
                                   " where "+
                                   " rt.codtec = res.codigo "+                                   
                                   " )AS T1 ON (T1.codruta = ru.codigo) "+                                   
                                   " where  "+
                                   " re.codruta = ru.codigo and "+
                                   " re.codequipo = eq.codigo and "+
                                   " eq.cod_proyecto = proy.codigo and "+
                                   " eq.exbo like '%"+exbo+"%' and "+
                                   " ru.codigo like '%"+codRuta+"%' and "+
                                   " proy.nombre like '%"+nombreProyecto+"%' and "+
                                   " T1.Supervisor like '%"+personalAsignado+"%' and "+                                   
                                   " cc.codmes = "+mes+" and cc.anio = "+anio+                               
                                   " order by ru.codigo,re.nrodia asc ";
             return ConecRes.consultaMySql(consulta);
         }


          public DataSet getAllPersonalAsignadoRuta() {
              string consulta = "select " +
                               " res.codigo, " +
                               " res.nombre  " +
                               " from  " +
                               " tb_detalle_rutatecnicom dr, tb_responsable res " +
                               " where " +
                               " dr.codtec = res.codigo  " +
                               " group by res.codigo " +
                               " order by res.codigo asc";
              return ConecRes.consultaMySql(consulta);
          }


          public bool existeEquipoRuta(int codRuta, int codEquipo)
          {
              string consulta = "select * from tb_cronogramavisitarutamanteminieto ee "+
                                   " where "+
                                   " ee.codruta = "+codRuta+" and "+
                                   " ee.codeq = "+codEquipo;
              DataSet dato = ConecRes.consultaMySql(consulta);
              if (dato.Tables[0].Rows.Count > 0)
              {
                  return true;
              }
              else
                  return false;
          }

          public bool insertarExcelBastones(string direccionRuta, string nombreArchivo)
          {
              string consulta = "truncate tb_cargarbastones_aux;";
              bool bandera1 = ConecRes.ejecutarMySql(consulta);
              consulta = "LOAD DATA LOCAL INFILE '"+direccionRuta+"/"+nombreArchivo+"' "+
                           " INTO TABLE tb_cargarbastones_aux "+
                           " FIELDS TERMINATED BY ';'  "+                           
                           " LINES TERMINATED BY '\r\n' "+
                           " IGNORE 1 LINES "+
                           " (codtecnico, codequipo, hora, fecha, tipoevento);";
              bool bandera2 = ConecRes.ejecutarMySql(consulta);

              consulta = "delete from tb_cargarbastones_aux where tb_cargarbastones_aux.codtecnico = 0;";
              bool bandera3 = ConecRes.ejecutarMySql(consulta);
              consulta = "insert into tb_bastones( "+
                           " tb_bastones.fecha, "+
                           " tb_bastones.hora, "+
                           " tb_bastones.codtecnico, "+
                           " tb_bastones.codequipo, "+
                           " tb_bastones.horabaston, "+
                           " tb_bastones.fechabaston, "+
                           " tb_bastones.tipoevento "+
                           " )  "+
                           " select "+
                           " now(), "+
                           " now(), "+
                           " tb_cargarbastones_aux.codtecnico, "+
                           " tb_cargarbastones_aux.codequipo, "+
                           " tb_cargarbastones_aux.hora, "+
                           " tb_cargarbastones_aux.fecha, "+
                           " tb_cargarbastones_aux.tipoevento "+
                           " from tb_cargarbastones_aux;";
              bool bandera4 = ConecRes.ejecutarMySql(consulta);

              if (bandera1 == true && bandera2 == true && bandera3 == true && bandera4 == true)
              {
                  return true;
              }
              else
                  return false;              
          }


       

          public bool insert_generarRutasMantenimiento(int mes, int anio, int coduser, int mesCopiar, int anioCopiar)
          {
              string delete1 = "delete from tb_cronogramavisitarutamanteminieto where tb_cronogramavisitarutamanteminieto.codmes = "+mes+" and tb_cronogramavisitarutamanteminieto.anio = "+anio;
              string delete2 = "delete from tb_detalle_rutatecnicom where tb_detalle_rutatecnicom.mes = "+mes+" and tb_detalle_rutatecnicom.anio = "+anio;
              string delete3 = "delete from tb_ruta where tb_ruta.mes = " + mes + " and tb_ruta.anio = " + anio;

              bool banderadelete1 = ConecRes.ejecutarMySql(delete1);
              bool banderadelete2 = ConecRes.ejecutarMySql(delete2);
              bool banderadelete3 = ConecRes.ejecutarMySql(delete3);

              string consulta1 = "insert into tb_ruta( "+
                                   " tb_ruta.nro, "+
                                   " tb_ruta.detalle, "+
                                   " tb_ruta.fecha, "+
                                   " tb_ruta.hora, "+
                                   " tb_ruta.estado, "+
                                   " tb_ruta.mes, "+
                                   " tb_ruta.anio) "+
                                   " select  "+
                                   " ru.nro, "+
                                   " ru.detalle, "+
                                   " now(), "+
                                   " now(), "+
                                   " ru.estado, "+
                                     mes+" , "+
                                     anio+
                                   " from tb_ruta ru "+
                                   " where  "+
                                   " ru.mes = "+mesCopiar+" and ru.anio = "+anioCopiar;
              
              bool bandera1 = ConecRes.ejecutarMySql(consulta1);


              string consulta2 = "insert into tb_cronogramavisitarutamanteminieto( "+
                                   " tb_cronogramavisitarutamanteminieto.codeq, "+
                                   " tb_cronogramavisitarutamanteminieto.codruta, "+
                                   " tb_cronogramavisitarutamanteminieto.codmes, "+
                                   " tb_cronogramavisitarutamanteminieto.anio, "+
                                   " tb_cronogramavisitarutamanteminieto.fecha, "+
                                   " tb_cronogramavisitarutamanteminieto.hora, "+
                                   " tb_cronogramavisitarutamanteminieto.nrovisita, "+
                                   " tb_cronogramavisitarutamanteminieto.semana1, "+
                                   " tb_cronogramavisitarutamanteminieto.semana2, "+
                                   " tb_cronogramavisitarutamanteminieto.semana3, "+
                                   " tb_cronogramavisitarutamanteminieto.semana4, "+
                                   " tb_cronogramavisitarutamanteminieto.fechas1, "+
                                   " tb_cronogramavisitarutamanteminieto.fechas2, "+
                                   " tb_cronogramavisitarutamanteminieto.fechas3, "+
                                   " tb_cronogramavisitarutamanteminieto.fechas4, "+
                                   " tb_cronogramavisitarutamanteminieto.codresp, "+
                                   " tb_cronogramavisitarutamanteminieto.horaentrada , "+
                                   " tb_cronogramavisitarutamanteminieto.diasemana, "+
                                   " tb_cronogramavisitarutamanteminieto.horasalida, "+
                                   " tb_cronogramavisitarutamanteminieto.nrodia, "+
                                   " tb_cronogramavisitarutamanteminieto.pasaje  "+
                                   " ) "+
                                   " select  "+
                                   " cc.codeq, "+
                                   " ru2.codigo, "+
                                     mes+" , "+
                                     anio+" , "+
                                   " now(), "+
                                   " now(), "+
                                   " cc.nrovisita, "+
                                   " cc.semana1, "+
                                   " cc.semana2, "+
                                   " cc.semana3, "+
                                   " cc.semana4, "+
                                   " cc.fechas1, "+
                                   " cc.fechas2, "+
                                   " cc.fechas3, "+
                                   " cc.fechas4, "+
                                   coduser+" , "+
                                   " cc.horaentrada, "+
                                   " cc.diasemana, "+
                                   " cc.horasalida, "+
                                   " cc.nrodia, "+                                   
                                   " cc.pasaje "+ 
                                   " from "+
                                   " tb_cronogramavisitarutamanteminieto cc, "+
                                   " tb_ruta ru1, tb_ruta ru2 "+
                                   " where "+
                                   " ru1.mes = "+mesCopiar+" and ru1.anio = "+anioCopiar+" and "+
                                   " ru1.nro = ru2.nro and "+
                                   " ru2.mes = "+mes+" and ru2.anio = "+anio+" and "+
                                   " cc.codruta = ru1.codigo and  "+
                                   " cc.codmes = "+mesCopiar+" and cc.anio = "+anioCopiar;  
                                   
              bool bandera2 = ConecRes.ejecutarMySql(consulta2);

              string consulta3 = "insert into tb_detalle_rutatecnicom( "+
                                   " tb_detalle_rutatecnicom.codruta, "+
                                   " tb_detalle_rutatecnicom.codtec, "+
                                   " tb_detalle_rutatecnicom.fecha, "+
                                   " tb_detalle_rutatecnicom.hora, "+
                                   " tb_detalle_rutatecnicom.estado, "+
                                   " tb_detalle_rutatecnicom.supervisor, "+
                                   " tb_detalle_rutatecnicom.mes, "+
                                   " tb_detalle_rutatecnicom.anio "+ 
                                   " ) "+
                                   " select  "+
                                   " r2.codigo, "+
                                   " dd.codtec, "+
                                   " now(), "+
                                   " now(), " +
                                   " dd.estado, "+
                                   " dd.supervisor, "+
                                   " month(now()), "+
                                   " year(now()) "+
                                   " from "+
                                   " tb_detalle_rutatecnicom dd, "+
                                   " tb_ruta r1, "+
                                   " tb_ruta r2 "+
                                   " where "+
                                   " r1.mes = "+mesCopiar+" and "+
                                   " r1.anio = "+anioCopiar+" and "+
                                   " r1.nro = r2.nro and "+
                                   " r2.mes = "+mes+" and "+
                                   " r2.anio = "+anio+" and "+
                                   " dd.codruta = r1.codigo and "+
                                   " dd.mes = "+mesCopiar+" and dd.anio = "+anio;

              bool bandera3 = ConecRes.ejecutarMySql(consulta3);

              if (banderadelete1&&banderadelete2&&banderadelete3&&bandera1 && bandera2 && bandera3)
              {
                  return true;
              }
              else
                  return false;
          }


          public bool update_generarRutasMantenimiento(int mes, int anio, int coduser, int mesCopiar, int anioCopiar)
          {
              string delete1 = "delete from tb_cronogramavisitarutamanteminieto where tb_cronogramavisitarutamanteminieto.codmes = " + mes + " and tb_cronogramavisitarutamanteminieto.anio = " + anio;
              string delete2 = "delete from tb_detalle_rutatecnicom where tb_detalle_rutatecnicom.mes = " + mes + " and tb_detalle_rutatecnicom.anio = " + anio;              

              bool banderadelete1 = ConecRes.ejecutarMySql(delete1);
              bool banderadelete2 = ConecRes.ejecutarMySql(delete2);              

              string consulta2 = "insert into tb_cronogramavisitarutamanteminieto( " +
                                   " tb_cronogramavisitarutamanteminieto.codeq, " +
                                   " tb_cronogramavisitarutamanteminieto.codruta, " +
                                   " tb_cronogramavisitarutamanteminieto.codmes, " +
                                   " tb_cronogramavisitarutamanteminieto.anio, " +
                                   " tb_cronogramavisitarutamanteminieto.fecha, " +
                                   " tb_cronogramavisitarutamanteminieto.hora, " +
                                   " tb_cronogramavisitarutamanteminieto.nrovisita, " +
                                   " tb_cronogramavisitarutamanteminieto.semana1, " +
                                   " tb_cronogramavisitarutamanteminieto.semana2, " +
                                   " tb_cronogramavisitarutamanteminieto.semana3, " +
                                   " tb_cronogramavisitarutamanteminieto.semana4, " +
                                   " tb_cronogramavisitarutamanteminieto.fechas1, " +
                                   " tb_cronogramavisitarutamanteminieto.fechas2, " +
                                   " tb_cronogramavisitarutamanteminieto.fechas3, " +
                                   " tb_cronogramavisitarutamanteminieto.fechas4, " +
                                   " tb_cronogramavisitarutamanteminieto.codresp, " +
                                   " tb_cronogramavisitarutamanteminieto.horaentrada , " +
                                   " tb_cronogramavisitarutamanteminieto.diasemana, " +
                                   " tb_cronogramavisitarutamanteminieto.horasalida, " +
                                   " tb_cronogramavisitarutamanteminieto.nrodia, " +
                                   " tb_cronogramavisitarutamanteminieto.pasaje  " +
                                   " ) " +
                                   " select  " +
                                   " cc.codeq, " +
                                   " ru2.codigo, " +
                                     mes + " , " +
                                     anio + " , " +
                                   " now(), " +
                                   " now(), " +
                                   " cc.nrovisita, " +
                                   " cc.semana1, " +
                                   " cc.semana2, " +
                                   " cc.semana3, " +
                                   " cc.semana4, " +
                                   " cc.fechas1, " +
                                   " cc.fechas2, " +
                                   " cc.fechas3, " +
                                   " cc.fechas4, " +
                                   coduser + " , " +
                                   " cc.horaentrada, " +
                                   " cc.diasemana, " +
                                   " cc.horasalida, " +
                                   " cc.nrodia, " +
                                   " cc.pasaje " +
                                   " from " +
                                   " tb_cronogramavisitarutamanteminieto cc, " +
                                   " tb_ruta ru1, tb_ruta ru2 " +
                                   " where " +
                                   " ru1.mes = " + mesCopiar + " and ru1.anio = " + anioCopiar + " and " +
                                   " ru1.nro = ru2.nro and " +
                                   " ru2.mes = " + mes + " and ru2.anio = " + anio + " and " +
                                   " cc.codruta = ru1.codigo and  " +
                                   " cc.codmes = " + mesCopiar + " and cc.anio = " + anioCopiar;

              bool bandera2 = ConecRes.ejecutarMySql(consulta2);

              string consulta3 = "insert into tb_detalle_rutatecnicom( " +
                                   " tb_detalle_rutatecnicom.codruta, " +
                                   " tb_detalle_rutatecnicom.codtec, " +
                                   " tb_detalle_rutatecnicom.fecha, " +
                                   " tb_detalle_rutatecnicom.hora, " +
                                   " tb_detalle_rutatecnicom.estado, " +
                                   " tb_detalle_rutatecnicom.supervisor, " +
                                   " tb_detalle_rutatecnicom.mes, " +
                                   " tb_detalle_rutatecnicom.anio " +
                                   " ) " +
                                   " select  " +
                                   " r2.codigo, " +
                                   " dd.codtec, " +
                                   " now(), " +
                                   " now(), " +
                                   " dd.estado, " +
                                   " dd.supervisor, " +
                                   " month(now()), " +
                                   " year(now()) " +
                                   " from " +
                                   " tb_detalle_rutatecnicom dd, " +
                                   " tb_ruta r1, " +
                                   " tb_ruta r2 " +
                                   " where " +
                                   " r1.mes = " + mesCopiar + " and " +
                                   " r1.anio = " + anioCopiar + " and " +
                                   " r1.nro = r2.nro and " +
                                   " r2.mes = " + mes + " and " +
                                   " r2.anio = " + anio + " and " +
                                   " dd.codruta = r1.codigo and " +
                                   " dd.mes = " + mesCopiar + " and dd.anio = " + anio;

              bool bandera3 = ConecRes.ejecutarMySql(consulta3);

              if (banderadelete1 && banderadelete2 && bandera2 && bandera3)
              {
                  return true;
              }
              else
                  return false;


          }

        //----------------------------------------- exterior --------------------------------------

          internal DataSet getRutasTecnicoMantenimietoDiaHoy(string fechaHoy, int codTecnicoMantenimiento)
          {
              string consulta = "select "+
                               " T3.codigo, "+
                               " T3.Edificio, "+
                               " T3.Exbo, "+
                               " T3.diasemana, T3.horaentrada, date_format(T3.fecha, '%d/%m/%Y') as 'FechaR' " +
                               " ,'' as 'Deuda_Moroso' "+
                               " from "+ 
                               " ( "+
                               " (select "+
                               " cv.codigo, "+
                               " proy.nombre as 'Edificio', "+
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', "+
                               " cv.diasemana, cv.horaentrada, cv.fechas1 as 'fecha' " +
                               " from  "+
                               " tb_detalle_rutatecnicom dr, "+
                               " tb_cronogramavisitarutamanteminieto cv, "+
                               " tb_equipo eq, tb_proyecto proy "+
                               " where "+
                               " cv.codeq = eq.codigo and "+
                               " eq.cod_proyecto = proy.codigo and  "+
                               " dr.codruta = cv.codruta and "+
                               " cv.fechas1 = "+fechaHoy+" and "+
                               " cv.fecha1cumplida is not true and "+
                              // " dr.supervisor = 'Tecnico Mantenimiento' and "+
                               " dr.codtec = "+codTecnicoMantenimiento+")  "+
                               " UNION ALL "+
                               " (select  "+
                               " cv.codigo, "+
                               " proy.nombre as 'Edificio', "+
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', "+
                               " cv.diasemana, cv.horaentrada, cv.fechas2 as 'fecha' " +
                               " from  "+
                               " tb_detalle_rutatecnicom dr, "+
                               " tb_cronogramavisitarutamanteminieto cv, "+
                               " tb_equipo eq, tb_proyecto proy "+
                               " where "+
                               " cv.codeq = eq.codigo and "+
                               " eq.cod_proyecto = proy.codigo and  "+
                               " dr.codruta = cv.codruta and "+
                               " cv.fechas2 = "+fechaHoy+" and "+
                               " cv.fecha2cumplida is not true and "+
                              // " dr.supervisor = 'Tecnico Mantenimiento' and "+
                               " dr.codtec = "+codTecnicoMantenimiento+") "+
                               " UNION ALL "+
                               " (select "+
                               " cv.codigo, "+
                               " proy.nombre as 'Edificio', "+
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', "+
                               " cv.diasemana, cv.horaentrada, cv.fechas3 as 'fecha' " +
                               " from "+
                               " tb_detalle_rutatecnicom dr, "+
                               " tb_cronogramavisitarutamanteminieto cv, "+
                               " tb_equipo eq, tb_proyecto proy "+
                               " where "+
                               " cv.codeq = eq.codigo and "+
                               " eq.cod_proyecto = proy.codigo and  "+
                               " dr.codruta = cv.codruta and "+
                               " cv.fechas3 = "+fechaHoy+" and "+
                               " cv.fecha3cumplida is not true and "+
                             //  " dr.supervisor = 'Tecnico Mantenimiento' and "+
                               " dr.codtec = "+codTecnicoMantenimiento+") "+
                               " UNION ALL "+
                               " (select  "+
                               " cv.codigo, "+
                               " proy.nombre as 'Edificio', "+
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', "+
                               " cv.diasemana, cv.horaentrada, cv.fechas4 as 'fecha' " +
                               " from "+
                               " tb_detalle_rutatecnicom dr, "+
                               " tb_cronogramavisitarutamanteminieto cv, "+
                               " tb_equipo eq, tb_proyecto proy "+
                               " where "+
                               " cv.codeq = eq.codigo and "+
                               " eq.cod_proyecto = proy.codigo and  "+
                               " dr.codruta = cv.codruta and "+
                               " cv.fechas4 = "+fechaHoy+" and "+
                               " cv.fecha4cumplida is not true and "+
                             //  " dr.supervisor = 'Tecnico Mantenimiento' and "+
                               " dr.codtec = "+codTecnicoMantenimiento+") "+
                               " ) as T3 "+
                               " order by TIMESTAMP(T3.fecha, T3.horaentrada) asc ";
              return ConecRes.consultaMySql(consulta);
          }

          internal DataSet getRutasTecnicoMantenimietoDiaAtras(string fechaHoy, int codTecnicoMantenimiento) {
              string consulta = "select " +
                               " T3.codigo, " +
                               " T3.Edificio, " +
                               " T3.Exbo, " +
                               " T3.diasemana, T3.horaentrada, date_format(T3.fecha, '%d/%m/%Y') as 'FechaR' " +
                               " ,'' as 'Deuda_Moroso' " +
                               " from " +
                               " ( " +
                               " (select " +
                               " cv.codigo, " +
                               " proy.nombre as 'Edificio', " +
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', " +
                               " cv.diasemana, cv.horaentrada, cv.fechas1 as 'fecha' " +
                               " from  " +
                               " tb_detalle_rutatecnicom dr, " +
                               " tb_cronogramavisitarutamanteminieto cv, " +
                               " tb_equipo eq, tb_proyecto proy " +
                               " where " +
                               " cv.codeq = eq.codigo and " +
                               " eq.cod_proyecto = proy.codigo and  " +
                               " dr.codruta = cv.codruta and " +
                               " cv.fechas1 < " + fechaHoy + " and " +
                               " cv.fecha1cumplida is not true and " +
                              // " dr.supervisor = 'Tecnico Mantenimiento' and " +
                               " dr.codtec = " + codTecnicoMantenimiento + ")  " +
                               " UNION ALL " +
                               " (select  " +
                               " cv.codigo, " +
                               " proy.nombre as 'Edificio', " +
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', " +
                               " cv.diasemana, cv.horaentrada, cv.fechas2 as 'fecha' " +
                               " from  " +
                               " tb_detalle_rutatecnicom dr, " +
                               " tb_cronogramavisitarutamanteminieto cv, " +
                               " tb_equipo eq, tb_proyecto proy " +
                               " where " +
                               " cv.codeq = eq.codigo and " +
                               " eq.cod_proyecto = proy.codigo and  " +
                               " dr.codruta = cv.codruta and " +
                               " cv.fechas2 < " + fechaHoy + " and " +
                               " cv.fecha2cumplida is not true and " +
                             //  " dr.supervisor = 'Tecnico Mantenimiento' and " +
                               " dr.codtec = " + codTecnicoMantenimiento + ") " +
                               " UNION ALL " +
                               " (select " +
                               " cv.codigo, " +
                               " proy.nombre as 'Edificio', " +
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', " +
                               " cv.diasemana, cv.horaentrada, cv.fechas3 as 'fecha' " +
                               " from " +
                               " tb_detalle_rutatecnicom dr, " +
                               " tb_cronogramavisitarutamanteminieto cv, " +
                               " tb_equipo eq, tb_proyecto proy " +
                               " where " +
                               " cv.codeq = eq.codigo and " +
                               " eq.cod_proyecto = proy.codigo and  " +
                               " dr.codruta = cv.codruta and " +
                               " cv.fechas3 < " + fechaHoy + " and " +
                               " cv.fecha3cumplida is not true and " +
                             //  " dr.supervisor = 'Tecnico Mantenimiento' and " +
                               " dr.codtec = " + codTecnicoMantenimiento + ") " +
                               " UNION ALL " +
                               " (select  " +
                               " cv.codigo, " +
                               " proy.nombre as 'Edificio', " +
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', " +
                               " cv.diasemana, cv.horaentrada, cv.fechas4 as 'fecha' " +
                               " from " +
                               " tb_detalle_rutatecnicom dr, " +
                               " tb_cronogramavisitarutamanteminieto cv, " +
                               " tb_equipo eq, tb_proyecto proy " +
                               " where " +
                               " cv.codeq = eq.codigo and " +
                               " eq.cod_proyecto = proy.codigo and  " +
                               " dr.codruta = cv.codruta and " +
                               " cv.fechas4 < " + fechaHoy + " and " +
                               " cv.fecha4cumplida is not true and " +
                             //  " dr.supervisor = 'Tecnico Mantenimiento' and " +
                               " dr.codtec = " + codTecnicoMantenimiento + ") " +
                               " ) as T3 " +
                               " order by TIMESTAMP(T3.fecha, T3.horaentrada) asc ";
              return ConecRes.consultaMySql(consulta);
          }


          internal bool insertarBoletaExterior(int mes, int anio,int codequipo,
                                                bool eq_ascensorelectrico,bool eq_ascensorhidraulico,
                                                bool eq_escaleramecanica,bool eq_plataforma,bool eq_montacoches,
                                                bool eq_minicarga,bool ee_ff,bool ee_fp,
                                                bool ee_pf, bool ee_pp, bool ee_pec,
                                                bool sc_motor,bool sc_poleas,
                                                bool sc_aceitemotor,bool sc_cabletraccion,bool sc_ventilador,
                                                bool sc_freno,bool sc_bobina,bool sc_lvelocidad,
                                                bool sc_reduccionjuego,bool sc_cpu,bool sc_tarjetas,
                                                bool sc_conectores,bool sc_auxiliares,bool sc_aelectrica,
                                                bool sc_reguladordevelocidad,bool sc_unidadhidraulica,bool sc_valvulahidraulica,
                                                bool sc_cadenaprincipal,bool sc_sistemalubricacion,bool sc_contyseriedeseguridad,
                                                bool sc_accesos,bool sc_limpieza,bool c_botonera,
                                                bool c_indicadores,bool c_iluminacion,bool c_puertacabina,
                                                bool c_ajusteenviaje,bool c_ventilador,bool c_barrerafotoelec,
                                                bool c_holguradecab,bool c_guias,bool c_vidrioespejopaneles,
                                                bool c_operadordepuertas,bool c_contyseriedeseguridad,bool c_pasamanos,
                                                bool c_limpieza,bool a_botonera,bool a_indicadores,
                                                bool a_puerta,bool a_guiapatines,bool a_cerrojos,
                                                bool a_padeenclavamiento,bool a_sensores,bool a_peines,
                                                bool a_peldanosfaldon,bool a_demarcaciones,bool a_botondeemergencia,
                                                bool a_contyseriedeseguridad,bool a_senales,bool a_limpieza,
                                                bool f_cablesdetraccion,bool f_cablelimitador,bool f_cableviajero,
                                                bool f_contrapeso,bool f_fdecarrerasuperior,bool f_fdecarrerainferior,
                                                bool f_paracaidas,bool f_topespistones,bool f_poleatensora,
                                                bool f_poleas,bool f_rieles,bool f_aceiteras,
                                                bool f_stopdefosa,bool f_resortes,bool f_tensiondecadena,
                                                bool f_contyseriedeseguridad,bool f_mordazas,bool f_limpieza,
                                                string materialesyrepuesto,bool i_fusiblecontactos,bool i_botoneradepisoencorte,
                                                bool i_limites,bool i_reguladordevelocidad,bool i_frenobalataselectroiman,
                                                bool i_motordetraccion,bool i_poleas,bool i_filtraciondeaguaensalademaquinas,
                                                bool i_accesoirregularasalademaquinas,bool i_corteensenalizadoropulsadordepiso,bool i_ruidooajusteenpuertaspisocabina,
                                                bool i_iluminaciondecabina,bool i_operadordepuertas,bool i_motordeoperador,
                                                bool i_ventiladordecabina,bool i_cerrojo,bool i_sensordecabinabarrerafotocelula,
                                                bool i_filtraciondeaguaenhuecoyfoso,bool i_bajasocortedetension,bool i_sensores,
                                                bool i_malusoporusuario,bool i_iluminacionirregularensalademaquinasyfoso,bool i_otros,
                                                string receptor_ci,string receptor_cargo,string recepcion,
                                                bool cambiorepuesto,string tipoboleta,bool siningresoedificio,
                                                int codtecnico, string horallegada, string horasalida, string observacion, float costotransporte,
                                                bool  vml_am_superiorizquierdo, bool vml_am_superiorderecho, bool vml_am_inferiorizquierdo, bool vml_am_inferiorderecho,
                                                bool vml_lct_bueno, bool vml_lct_malo, bool vml_nm_bueno, bool vml_nm_malo, string vml_obs, int codresgra)
          {
              string consulta = "insert into tb_visitadetallerutaequipo( "+
                               " fechagra,horagra, "+
                               " fecha,hora,boleta, "+
                               " fechaboleta,mes,anio, "+
                               " codequipo,eq_ascensorelectrico,eq_ascensorhidraulico, "+
                               " eq_escaleramecanica,eq_plataforma,eq_montacoches, "+
                               " eq_minicarga,ee_ff,ee_fp, "+
                               " ee_pf,ee_pp,ee_pec, "+
                               " sc_motor,sc_poleas, "+
                               " sc_aceitemotor,sc_cabletraccion,sc_ventilador, "+
                               " sc_freno,sc_bobina,sc_lvelocidad, "+
                               " sc_reduccionjuego,sc_cpu,sc_tarjetas, "+
                               " sc_conectores,sc_auxiliares,sc_aelectrica, "+
                               " sc_reguladordevelocidad,sc_unidadhidraulica,sc_valvulahidraulica, "+
                               " sc_cadenaprincipal,sc_sistemalubricacion,sc_contyseriedeseguridad, "+
                               " sc_accesos,sc_limpieza,c_botonera, "+
                               " c_indicadores,c_iluminacion,c_puertacabina, "+
                               " c_ajusteenviaje,c_ventilador,c_barrerafotoelec, "+
                               " c_holguradecab,c_guias,c_vidrioespejopaneles, "+
                               " c_operadordepuertas,c_contyseriedeseguridad,c_pasamanos, "+
                               " c_limpieza,a_botonera,a_indicadores, "+
                               " a_puerta,a_guiapatines,a_cerrojos, "+
                               " a_padeenclavamiento,a_sensores,a_peines, "+
                               " a_peldanosfaldon,a_demarcaciones,a_botondeemergencia, "+
                               " a_contyseriedeseguridad,a_senales,a_limpieza, "+
                               " f_cablesdetraccion,f_cablelimitador,f_cableviajero, "+
                               " f_contrapeso,f_fdecarrerasuperior,f_fdecarrerainferior, "+
                               " f_paracaidas,f_topespistones,f_poleatensora, "+
                               " f_poleas,f_rieles,f_aceiteras, "+
                               " f_stopdefosa,f_resortes,f_tensiondecadena, "+
                               " f_contyseriedeseguridad,f_mordazas,f_limpieza, "+
                               " materialesyrepuesto,i_fusiblecontactos,i_botoneradepisoencorte, "+
                               " i_limites,i_reguladordevelocidad,i_frenobalataselectroiman, "+
                               " i_motordetraccion,i_poleas,i_filtraciondeaguaensalademaquinas, "+
                               " i_accesoirregularasalademaquinas,i_corteensenalizadoropulsadordepiso,i_ruidooajusteenpuertaspisocabina, "+
                               " i_iluminaciondecabina,i_operadordepuertas,i_motordeoperador, "+
                               " i_ventiladordecabina,i_cerrojo,i_sensordecabinabarrerafotocelula, "+
                               " i_filtraciondeaguaenhuecoyfoso,i_bajasocortedetension,i_sensores, "+
                               " i_malusoporusuario,i_iluminacionirregularensalademaquinasyfoso,i_otros, "+
                               " receptor_ci,receptor_cargo,recepcion, "+
                               " cambiorepuesto,tipoboleta,siningresoedificio, "+
                               " codtecnico,horallegada,horasalida,observacion,costotransporte, codresgra, " +
                               " vml_am_superiorizquierdo, "+
                               " vml_am_superiorderecho, "+
                               " vml_am_inferiorizquierdo, "+
                               " vml_am_inferiorderecho, "+
                               " vml_lct_bueno, "+
                               " vml_lct_malo, "+
                               " vml_nm_bueno, "+
                               " vml_nm_malo, "+
                               " vml_obs) " +
                               " values "+
                               " ( "+
                               " current_date, current_time, "+
                               " current_date, current_time, concat(year(current_date), month(current_date), (select max(vv.codigo)+1 from tb_visitadetallerutaequipo vv)), "+
                               " current_date, "+mes+","+anio+", "+
                                 codequipo+" , "+eq_ascensorelectrico+","+eq_ascensorhidraulico+", "+
                                eq_escaleramecanica+","+eq_plataforma+","+eq_montacoches+", "+
                                eq_minicarga+","+ee_ff+","+ee_fp+", "+
                                ee_pf+","+ee_pp+","+ee_pec+", "+
                                sc_motor+","+sc_poleas+", "+
                                sc_aceitemotor+","+sc_cabletraccion+","+sc_ventilador+", "+
                                sc_freno+","+sc_bobina+","+sc_lvelocidad+", "+
                                sc_reduccionjuego+","+sc_cpu+","+sc_tarjetas+", "+
                                sc_conectores+","+sc_auxiliares+","+sc_aelectrica+", "+
                                sc_reguladordevelocidad+","+sc_unidadhidraulica+","+sc_valvulahidraulica+", "+
                                sc_cadenaprincipal+","+sc_sistemalubricacion+","+sc_contyseriedeseguridad+", "+
                                sc_accesos+","+sc_limpieza+","+c_botonera+", "+
                                c_indicadores+","+c_iluminacion+","+c_puertacabina+", "+
                                c_ajusteenviaje+","+c_ventilador+","+c_barrerafotoelec+", "+
                                c_holguradecab+","+c_guias+","+c_vidrioespejopaneles+", "+
                                c_operadordepuertas+","+c_contyseriedeseguridad+","+c_pasamanos+", "+
                                c_limpieza+","+a_botonera+","+a_indicadores+", "+
                                a_puerta+","+a_guiapatines+","+a_cerrojos+", "+
                                a_padeenclavamiento+","+a_sensores+","+a_peines+", "+
                                a_peldanosfaldon+","+a_demarcaciones+","+a_botondeemergencia+", "+
                                a_contyseriedeseguridad+","+a_senales+","+a_limpieza+", "+
                                f_cablesdetraccion+","+f_cablelimitador+","+f_cableviajero+", "+
                                f_contrapeso+","+f_fdecarrerasuperior+","+f_fdecarrerainferior+", "+
                                f_paracaidas+","+f_topespistones+","+f_poleatensora+", "+
                                f_poleas+","+f_rieles+","+f_aceiteras+", "+
                                f_stopdefosa+","+f_resortes+","+f_tensiondecadena+", "+
                                f_contyseriedeseguridad+","+f_mordazas+","+f_limpieza+", "+
                                "'"+materialesyrepuesto+"',"+i_fusiblecontactos+","+i_botoneradepisoencorte+", "+
                                i_limites+","+i_reguladordevelocidad+","+i_frenobalataselectroiman+", "+
                                i_motordetraccion+","+i_poleas+","+i_filtraciondeaguaensalademaquinas+", "+
                                i_accesoirregularasalademaquinas+","+i_corteensenalizadoropulsadordepiso+","+i_ruidooajusteenpuertaspisocabina+", "+
                                i_iluminaciondecabina+","+i_operadordepuertas+","+i_motordeoperador+", "+
                                i_ventiladordecabina+","+i_cerrojo+","+i_sensordecabinabarrerafotocelula+", "+
                                i_filtraciondeaguaenhuecoyfoso+","+i_bajasocortedetension+","+i_sensores+", "+
                                i_malusoporusuario+","+i_iluminacionirregularensalademaquinasyfoso+","+i_otros+", "+
                                "'"+receptor_ci+"','"+receptor_cargo+"','"+recepcion+"', "+
                                cambiorepuesto+",'"+tipoboleta+"',"+siningresoedificio+", "+
                                codtecnico + ",'" + horallegada + "',current_time,'" + observacion + "','"+costotransporte.ToString().Replace(',','.')+"', "+
                                codresgra+" , "+
                                vml_am_superiorizquierdo+", "+
                                vml_am_superiorderecho +", "+
                                vml_am_inferiorizquierdo+", "+
                                vml_am_inferiorderecho+", "+
                                vml_lct_bueno+", "+
                                vml_lct_malo+", "+
                                vml_nm_bueno+", "+
                                vml_nm_malo+", "+
                               "'"+ vml_obs+"') ";
              return ConecRes.ejecutarMySql(consulta);
          }

          internal DataSet getcodigoEquipoCronogramaRuta(int codigoCronogramaRuta)
          {
              string consulta = "select cc.codeq from tb_cronogramavisitarutamanteminieto cc "+
                               " where cc.codigo = "+codigoCronogramaRuta;
              return ConecRes.consultaMySql(consulta);
          }

          internal bool esRutafecha1(int codigoCronogramaRuta, string fechaOrigBoletaOK)
          {
              string consulta = "select * from tb_cronogramavisitarutamanteminieto cc " +
                               " where cc.codigo = "+codigoCronogramaRuta+" and  cc.fechas1 = "+fechaOrigBoletaOK;
              DataSet tupla = ConecRes.consultaMySql(consulta);
              if (tupla.Tables[0].Rows.Count > 0)
              {
                  return true;
              }
              else
                  return false;
          }

          internal bool esRutafecha2(int codigoCronogramaRuta, string fechaOrigBoletaOK)
          {
              string consulta = "select * from tb_cronogramavisitarutamanteminieto cc " +
                               " where cc.codigo = " + codigoCronogramaRuta + " and  cc.fechas2 = " + fechaOrigBoletaOK;
              DataSet tupla = ConecRes.consultaMySql(consulta);
              if (tupla.Tables[0].Rows.Count > 0)
              {
                  return true;
              }
              else
                  return false;
          }

          internal bool esRutafecha3(int codigoCronogramaRuta, string fechaOrigBoletaOK)
          {
              string consulta = "select * from tb_cronogramavisitarutamanteminieto cc " +
                               " where cc.codigo = " + codigoCronogramaRuta + " and  cc.fechas3 = " + fechaOrigBoletaOK;
              DataSet tupla = ConecRes.consultaMySql(consulta);
              if (tupla.Tables[0].Rows.Count > 0)
              {
                  return true;
              }
              else
                  return false;
          }

          internal bool esRutafecha4(int codigoCronogramaRuta, string fechaOrigBoletaOK)
          {
              string consulta = "select * from tb_cronogramavisitarutamanteminieto cc " +
                               " where cc.codigo = " + codigoCronogramaRuta + " and  cc.fechas4 = " + fechaOrigBoletaOK;
              DataSet tupla = ConecRes.consultaMySql(consulta);
              if (tupla.Tables[0].Rows.Count > 0)
              {
                  return true;
              }
              else
                  return false;
          }


          internal bool marcarFecha1Realizada(int codigoCronogramaRuta)
          {
              string consulta = "update tb_cronogramavisitarutamanteminieto set "+
                               " tb_cronogramavisitarutamanteminieto.fecha1cumplida = true "+
                               " where "+
                               " tb_cronogramavisitarutamanteminieto.codigo ="+codigoCronogramaRuta;
              return ConecRes.ejecutarMySql(consulta);
          }

          internal bool marcarFecha2Realizada(int codigoCronogramaRuta)
          {
              string consulta = "update tb_cronogramavisitarutamanteminieto set " +
                               " tb_cronogramavisitarutamanteminieto.fecha2cumplida = true " +
                               " where " +
                               " tb_cronogramavisitarutamanteminieto.codigo =" + codigoCronogramaRuta;
              return ConecRes.ejecutarMySql(consulta);
          }

          internal bool marcarFecha3Realizada(int codigoCronogramaRuta)
          {
              string consulta = "update tb_cronogramavisitarutamanteminieto set " +
                               " tb_cronogramavisitarutamanteminieto.fecha3cumplida = true " +
                               " where " +
                               " tb_cronogramavisitarutamanteminieto.codigo =" + codigoCronogramaRuta;
              return ConecRes.ejecutarMySql(consulta);
          }

          internal bool marcarFecha4Realizada(int codigoCronogramaRuta)
          {
              string consulta = "update tb_cronogramavisitarutamanteminieto set " +
                               " tb_cronogramavisitarutamanteminieto.fecha4cumplida = true " +
                               " where " +
                               " tb_cronogramavisitarutamanteminieto.codigo =" + codigoCronogramaRuta;
              return ConecRes.ejecutarMySql(consulta);
          }

          internal DataSet getUltimaBoletaInsertada()
          {
              string consulta = "select vv.codigo, vv.fecha, vv.boleta, vv.horallegada, vv.horasalida  " +
                                " from tb_visitadetallerutaequipo vv "+ 
                                " order by vv.codigo desc limit 1";
              return ConecRes.consultaMySql(consulta);
          }

          internal bool insertardevolucionderepuestoalcliente(int codboletavisitaruta, string codedificio, string edificio, string cliente, string observaciondevolucion, string realizadopor, string realizadoporcago, string recepcionadopor, string recepcionadoporcago, string recepcionadoporci)
          {
              string consulta = "insert into tb_boletadevolucionderepuestoalcliente( "+
                                " fechagra, horagra, observaciondevolucion, "+
                                " realizadopor, realizadoporcago, "+
                                " recepcionadopor, recepcionadoporcago, recepcionadoporci, "+
                                " codedificio, edificio, cliente, codboletavisitaruta) "+
                                " values(current_date(), current_time(), '"+observaciondevolucion+"', "+
                                " '"+realizadopor+"', '"+realizadoporcago+"', "+ 
                                " '"+recepcionadopor+"', '"+recepcionadoporcago+"', '"+recepcionadoporci+"', "+
                                codedificio+", '"+edificio+"', '"+cliente+"', "+codboletavisitaruta+")";
              return ConecRes.ejecutarMySql(consulta);
          }
    }
}