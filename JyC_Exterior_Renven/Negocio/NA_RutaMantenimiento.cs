using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_RutaMantenimiento
    {
        DA_RutaMantenimiento nruta = new DA_RutaMantenimiento(); 

        public NA_RutaMantenimiento() { }

        public int getsiguienteRutaMatenimiento(int mes , int anio) {
            DataSet dato = nruta.getallRutas(mes, anio);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows.Count;
            }
            else
                return 0;
        }


        public DataSet getallEquiposProyecto(string edificio) {
            return nruta.getallEquiposProyecto(edificio);
        }

        public DataSet getallEquiposProyectoRutaMantenimiento(string edificio)
        {
            return nruta.getallEquiposProyectoRutaMantenimiento(edificio);
        }

        public DataSet getAllFaltantesEquiposSinRutas(string Edificio, int mes, int anio) {
            return nruta.getAllFaltantesEquiposSinRutas(Edificio, mes, anio);
        }

        public bool insertarRutaMantenimiento(int nro, string detalle, int mes, int anio) {
            return nruta.insertarRutaMantenimiento(nro, detalle, mes, anio);
        }

        public DataSet getallRutaMantenimiento(string nombre, int mes,int anio) {
            return nruta.getallRutaMantenimiento(nombre, mes, anio);      
        }

        public DataSet getallRutaMantenimiento2(string codigo, string nombre, int mes, int anio)
        {
            return nruta.getallRutaMantenimiento2(codigo, nombre,mes,anio);
        }

        public DataSet getallEquiposRutasAsignadas(int codRuta, string nombreEdificio)
        {
            return nruta.getallEquiposRutasAsignadas(codRuta,nombreEdificio);
        }

        public DataSet getallEquiposRutasAsignadas2(string exbo, string edificio, int mes, int anio) {
            return nruta.getallEquiposRutasAsignadas2(exbo, edificio, mes, anio);
        }

        public bool modificarRutaMantenimiento(int codigo, int nro , string detalle, int mes, int anio)
        {
            return nruta.modificarRutaMantenimiento(codigo, nro, detalle, mes , anio);
        }

        public bool insertarEquipoRuta(int codRuta, int codEquipo, string horaEntrada, string horasalida, string dia, int cantvisitas, int nrodia, bool semana1, bool semana2, bool semana3, bool semana4, int mes, int anio, string fechaS1, string fechaS2, string fechaS3, string fechaS4, int codResp, float pasaje)
        {
            if (existeEquipoRuta(codRuta, codEquipo) == false)
            {
                NA_CronogramaVisitaRutaMantenimiento crono = new NA_CronogramaVisitaRutaMantenimiento();
                bool bandera = crono.insertarCronogramaVisitaRutaM(codRuta, codEquipo, cantvisitas, mes, anio, semana1, semana2, semana3, semana4, fechaS1, fechaS2, fechaS3, fechaS4, codResp,horaEntrada,horasalida,dia,nrodia,pasaje);
                return bandera;                  
            }
            else
                return false;
        }


     /*   public bool modificarCronogramaEquipoRutaMantemiento(int codRuta, int codEquipo, int codCronograma)
        {            
            return nruta.modificarCronogramaEquipoRutaMantemiento(codRuta,  codEquipo,  codCronograma);
        }*/


        public bool ModificarEquipoRuta(int codRuta, int codEquipo, string horaEntrada, string horasalida, string dia, int cantvisitas, int nrodia, bool semana1, bool semana2, bool semana3, bool semana4, int codmes, int anio, string fechaS1, string fechaS2, string fechaS3, string fechaS4, int codResp, float pasaje)
        { 
          
            NA_CronogramaVisitaRutaMantenimiento crono = new NA_CronogramaVisitaRutaMantenimiento();
            int codigoCronoMesAnio = crono.getCodigoCronoVisitaRutaEquipo(codRuta, codEquipo, codmes, anio);
            bool banderaCrono = crono.UpdateCronogramaVisitaRutaM(codigoCronoMesAnio, cantvisitas, semana1, semana2, semana3, semana4, fechaS1, fechaS2, fechaS3, fechaS4, codResp, horaEntrada,horasalida,dia,nrodia,pasaje);                                          
            return banderaCrono;
        }

        public bool insertarTecnicoRuta(int codRuta, int codTecnico, string supervisor, int mes, int anio)
        {
            return nruta.insertarTecnicoRuta( codRuta,  codTecnico,  supervisor, mes,anio);
        }

        public DataSet mostrarTecnicoRuta(int codRuta)
        {
            return nruta.mostrarTecnicoRuta(codRuta);
        }

        public DataSet mostrarTecnicoRuta(int mes, int anio)
        {
            return nruta.mostrarTecnicoRuta(mes, anio);
        }

        public DataSet mostrarTecnicoRuta2(string nombre, int mes, int anio)
        {
            return nruta.mostrarTecnicoRuta2(nombre, mes, anio);
        }


        public bool eliminarRuta(int codRuta, int mes, int anio)
        {
            return nruta.eliminarRuta(codRuta, mes, anio);
        }

        public bool tieneEquiposAsignadosRuta(int codRuta) {

            DataSet dato = nruta.equiposAsignadoRuta(codRuta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool tieneTecnicosAsignadosRuta(int codRuta)
        {

            DataSet dato = nruta.tecnicoAsignadosRuta(codRuta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool eliminarEquipoRutaMantemiento(int codRuta, int codEquipo)
        {
            return nruta.eliminarEquipoRutaMantemiento(codRuta,codEquipo);
        }

        public bool eliminarTecnicoRutaMantemiento(int codRuta, int codTecnico)
         {
             return nruta.eliminarTecnicoRutaMantemiento(codRuta, codTecnico);
        }

        public bool insertarBoletaMantenimiento(int codEquipo, int codTecnico, string boleta, string detalle, bool cambiorepuesto, string fechaboleta, string horallegada, string horasalida, string recepcion, bool banderaArreglo, string tipoBoleta, bool siningresoedificio)
        {
            return nruta.insertarBoletaMantenimiento( codEquipo,  codTecnico,  boleta,  detalle,  cambiorepuesto,  fechaboleta,  horallegada,  horasalida,  recepcion,  banderaArreglo,  tipoBoleta,  siningresoedificio);
        }

        public bool eliminarBoletaMantenimiento(int codigoBoleta) {
            return nruta.eliminarBoletaMantenimiento(codigoBoleta);
        }

        public DataSet mostrarBoletasMantenimiento(int codequipo, int codtecnico) {
            return nruta.mostrarBoletasMantenimiento( codequipo,  codtecnico);
        }

        public DataSet mostrarALLPersonalAsignadoRuta(int mes, int anio)
        {
            return nruta.mostrarALLPersonalAsignadoRuta( mes,  anio);
        }

        public DataSet mostrarALLEquiposAsignadosRutas(int mes, int anio)
        {
            return nruta.mostrarALLEquiposAsignadosRutas(mes,anio);
         
        }


        public DateTime getfechaAsignadaRutaMantenimiento(int diaInicial, int mes, int anio, int diadefecha) {
           
            DateTime date = new DateTime(anio, mes, diaInicial);

            System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
            System.Globalization.Calendar cal = norwCulture.Calendar;
            int weekNo = cal.GetWeekOfYear(date, norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
            int nrodiaActual = (int)date.DayOfWeek;

            if (nrodiaActual == diadefecha)
            {
               return  date;
            }
            else
            {
                int auxdiaActual = nrodiaActual;
                if (nrodiaActual == 0) { auxdiaActual = 7; }

                if (auxdiaActual > diadefecha)
                {
                    int calDia = (7 - auxdiaActual) + diadefecha;
                    DateTime auxdate = date.AddDays(calDia);
                    return auxdate;
                }
                else {
                    int calDia = (diadefecha - auxdiaActual);
                    DateTime auxdate = date.AddDays(calDia);
                    return auxdate;                
                }

            }

         }

        public DataSet mostrarALLEquiposAsignadosRutas_conFechas(int mes, int anio)
        {
            return nruta.mostrarALLEquiposAsignadosRutasFechas(mes, anio);
        }

      /*  public DataTable mostrarALLEquiposAsignadosRutas_conFechas(int mes, int anio)
        {
            DataSet Rutas = nruta.mostrarALLEquiposAsignadosRutas( mes, anio);
            

            DataTable TablaRutas = Rutas.Tables[0];
            TablaRutas.Columns.Add("Semana1", typeof(string));
            TablaRutas.Columns.Add("Semana2", typeof(string));
            TablaRutas.Columns.Add("Semana3", typeof(string));
            TablaRutas.Columns.Add("Semana4", typeof(string));

            for (int i = 0; i < TablaRutas.Rows.Count; i++)
            {
                int cantVisita = Convert.ToInt32(TablaRutas.Rows[i][7].ToString());
                int nroDia = Convert.ToInt32(TablaRutas.Rows[i][8].ToString());
                DataSet cronogramaNroVisitas = nruta.getcronogramaMesAnioRuta_porNroVisita(mes, anio, cantVisita);

                if (cronogramaNroVisitas.Tables[0].Rows.Count > 0)
                {
                    bool semana1 = (bool)cronogramaNroVisitas.Tables[0].Rows[0][3];
                    bool semana2 = (bool)cronogramaNroVisitas.Tables[0].Rows[0][4];
                    bool semana3 = (bool)cronogramaNroVisitas.Tables[0].Rows[0][5];
                    bool semana4 = (bool)cronogramaNroVisitas.Tables[0].Rows[0][6];

                    DateTime dateInicial = new DateTime(anio, mes, nroDia);
                    //primera pasada------------------------------------------------------
                    DateTime dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana1 == true)
                    {                        
                        TablaRutas.Rows[i]["Semana1"] = dateAux.ToString("dd/MM/yyyy");                        
                    }
                    else
                        TablaRutas.Rows[i]["Semana1"] = "";
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------segunda pasada ----------------------------
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana2 == true) {
                        TablaRutas.Rows[i]["Semana2"] = dateAux.ToString("dd/MM/yyyy");                        
                    }else
                        TablaRutas.Rows[i]["Semana2"] = "";
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------tercera pasada ----------------------------
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana3 == true) {
                        TablaRutas.Rows[i]["Semana3"] = dateAux.ToString("dd/MM/yyyy");                        
                    }else
                        TablaRutas.Rows[i]["Semana3"] = "";
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------cuarta pasada ----------------------------
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana4 == true) {
                        TablaRutas.Rows[i]["Semana4"] = dateAux.ToString("dd/MM/yyyy");                        
                    }else
                        TablaRutas.Rows[i]["Semana4"] = ""; 
                }

            }
            return TablaRutas;
        }
        */


        public DataTable getcalculoFechasporEquipo(int mes, int anio, int nroDia, bool semana1, bool semana2 , bool semana3, bool semana4)
        {

            DataTable TablaRutas = new DataTable();
            TablaRutas.Columns.Add("Semana1", typeof(string));
            TablaRutas.Columns.Add("Semana2", typeof(string));
            TablaRutas.Columns.Add("Semana3", typeof(string));
            TablaRutas.Columns.Add("Semana4", typeof(string));

    //----------------------------------------------------------
                    DateTime dateInicial = new DateTime(anio, mes, nroDia);
                    //primera pasada------------------------------------------------------
                    string fechaSemana1 = "";
                    DateTime dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);                    
                    if (semana1 == true)
                    {
                       fechaSemana1 = dateAux.ToString("dd/MM/yyyy");
                    }
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------segunda pasada ----------------------------
                    string fechaSemana2 = "";
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana2 == true)
                    {
                        fechaSemana2 = dateAux.ToString("dd/MM/yyyy");
                    }
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------tercera pasada ----------------------------
                    string fechaSemana3 = "";
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana3 == true)
                    {
                        fechaSemana3 = dateAux.ToString("dd/MM/yyyy");
                    }
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------cuarta pasada ----------------------------
                    string fechaSemana4 = "";
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana4 == true)
                    {
                        fechaSemana4 = dateAux.ToString("dd/MM/yyyy");
                    }

                    TablaRutas.Rows.Add(fechaSemana1, fechaSemana2, fechaSemana3, fechaSemana4);
                                       
            return TablaRutas;
        }

        public DataSet getdetalleRutaEquipoCrono(int codRuta, int codEquipo)
        {
            return nruta.getdetalleRutaEquipoCrono(codRuta,codEquipo);
        }

        public DataSet getdetalleRutaEquipoTodos(int mes, int anio)
        {
            return nruta.getdetalleRutaEquipoTodos(mes, anio);
        }

       public void generarFechasCronogramaRutaEquipo(int mes, int anio, int coduser)
        {
            DataSet rutas = getdetalleRutaEquipoTodos(mes, anio);
            for (int i = 0; i < rutas.Tables[0].Rows.Count; i++)
            {
                int codRuta = Convert.ToInt32(rutas.Tables[0].Rows[i][0].ToString());
                int codEquipo = Convert.ToInt32(rutas.Tables[0].Rows[i][1].ToString());
                string horaentrada = rutas.Tables[0].Rows[i][2].ToString();
                string horasalida = rutas.Tables[0].Rows[i][3].ToString();
                string diaSemana = rutas.Tables[0].Rows[i][4].ToString();
                int nrodia = Convert.ToInt32(rutas.Tables[0].Rows[i][5].ToString());
                float pasaje = Convert.ToSingle(rutas.Tables[0].Rows[i][12].ToString());

                if(nrodia == 0){
                    nrodia = 7;
                }
                int cantvisita = Convert.ToInt32(rutas.Tables[0].Rows[i][6].ToString());
                bool banderaSemana1 = Convert.ToBoolean(rutas.Tables[0].Rows[i][7].ToString());
                bool banderaSemana2 = Convert.ToBoolean(rutas.Tables[0].Rows[i][8].ToString());
                bool banderaSemana3 = Convert.ToBoolean(rutas.Tables[0].Rows[i][9].ToString());
                bool banderaSemana4 = Convert.ToBoolean(rutas.Tables[0].Rows[i][10].ToString());

                DataTable tablafechas = getcalculoFechasporEquipo(mes, anio, nrodia, banderaSemana1, banderaSemana2, banderaSemana3, banderaSemana4);
                string FechaSemana1 = tablafechas.Rows[0][0].ToString();
                string FechaSemana2 = tablafechas.Rows[0][1].ToString();
                string FechaSemana3 = tablafechas.Rows[0][2].ToString();
                string FechaSemana4 = tablafechas.Rows[0][3].ToString();

                string semana1 = "null";
                if (!FechaSemana1.Equals(""))
                {
                    DateTime fecha1 = Convert.ToDateTime(FechaSemana1);
                    semana1 = "'" + fecha1.ToString("yyyy/MM/dd") + "'";
                }

                string semana2 = "null";
                if (!FechaSemana2.Equals(""))
                {
                    DateTime fecha2 = Convert.ToDateTime(FechaSemana2);
                    semana2 = "'" + fecha2.ToString("yyyy/MM/dd") + "'";
                }

                string semana3 = "null";
                if (!FechaSemana3.Equals(""))
                {
                    DateTime fecha3 = Convert.ToDateTime(FechaSemana3);
                    semana3 = "'" + fecha3.ToString("yyyy/MM/dd") + "'";
                }

                string semana4 = "null";
                if (!FechaSemana4.Equals(""))
                {
                    DateTime fecha4 = Convert.ToDateTime(FechaSemana4);
                    semana4 = "'" + fecha4.ToString("yyyy/MM/dd") + "'";
                }  

                ModificarEquipoRuta(codRuta, codEquipo, horaentrada, horasalida, diaSemana, cantvisita, nrodia, banderaSemana1, banderaSemana2, banderaSemana3, banderaSemana4, mes, anio, semana1, semana2, semana3, semana4, coduser, pasaje);
            }
        }

        public bool tieneRutas(int mes, int anio)
        {
            DataSet dato = nruta.getallRutaMantenimiento("", mes, anio);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

      

        public bool generarRutasMantenimiento(int mes, int anio, int coduser, int mesCopiar, int anioCopiar)
        {
            bool bandera = false;
            if (tieneRutas(mes, anio) == false)
            {
                bandera = nruta.insert_generarRutasMantenimiento(mes, anio, coduser, mesCopiar, anioCopiar);
                generarFechasCronogramaRutaEquipo(mes, anio, coduser);
            }
            else
            {
                bandera = nruta.update_generarRutasMantenimiento(mes, anio, coduser, mesCopiar, anioCopiar);
                generarFechasCronogramaRutaEquipo(mes, anio, coduser);
            }
            return bandera;          
        }


        public DataTable mostrarALLEquiposAsignadosRutas_consulta3( int mes, int anio, string tipoBoleta)
        {
            DataSet Rutas = nruta.mostrarALLEquiposAsignadosRutasFechas_Reporte(mes, anio);
           // int cantidadColumnasFechas = nruta.getcantidadColumnasFechasTotalRutas(mes, anio);

            DataTable TablaRutas = Rutas.Tables[0];
            TablaRutas.Columns.Add("Fecha1", typeof(string));
            TablaRutas.Columns.Add("Fecha2", typeof(string));
            TablaRutas.Columns.Add("Fecha3", typeof(string));
            TablaRutas.Columns.Add("Fecha4", typeof(string));
            TablaRutas.Columns.Add("Fecha5", typeof(string));
            TablaRutas.Columns.Add("Fecha6", typeof(string));
            TablaRutas.Columns.Add("Fecha7", typeof(string));
            TablaRutas.Columns.Add("Fecha8", typeof(string));
            TablaRutas.Columns.Add("Fecha9", typeof(string));
            TablaRutas.Columns.Add("Fecha10", typeof(string));
            TablaRutas.Columns.Add("Fecha11", typeof(string));
            TablaRutas.Columns.Add("Fecha12", typeof(string));
            TablaRutas.Columns.Add("Fecha13", typeof(string));
            TablaRutas.Columns.Add("Fecha14", typeof(string));
            TablaRutas.Columns.Add("Fecha15", typeof(string));

            for (int i = 0; i < TablaRutas.Rows.Count; i++)
            {
                int codRuta = Convert.ToInt32(Rutas.Tables[0].Rows[i][0].ToString());
                string exbo = Rutas.Tables[0].Rows[i][7].ToString();
                NEquipo neq = new NEquipo();
                int codEquipo = Convert.ToInt32(neq.getEquipo2(exbo).Tables[0].Rows[0][0].ToString());
                
                DataSet fechasBoletas = nruta.getFechasBoletasRutas(codRuta, codEquipo, mes, anio, tipoBoleta);

                for (int j = 0; j < fechasBoletas.Tables[0].Rows.Count; j++)
                {
                    string fecha = fechasBoletas.Tables[0].Rows[j][0].ToString();
                    string columna = "Fecha" + (j + 1);
                    TablaRutas.Rows[i][columna] = fecha;
                    
                }
            }
            return TablaRutas;
        }


        public DataTable mostrarALLEquiposAsignadosRutas_Bastones(int mes, int anio,string codRutaAux, string exboAux, string edificio ,string tipoBoleta)
        {
            DataSet Rutas = nruta.mostrarALLEquiposAsignadosRutasFechas_consulta(mes, anio, codRutaAux,exboAux, edificio);
            

            DataTable TablaRutas = Rutas.Tables[0];
            TablaRutas.Columns.Add("Fecha1", typeof(string));
            TablaRutas.Columns.Add("Fecha2", typeof(string));
            TablaRutas.Columns.Add("Fecha3", typeof(string));
            TablaRutas.Columns.Add("Fecha4", typeof(string));
            TablaRutas.Columns.Add("Fecha5", typeof(string));
            TablaRutas.Columns.Add("Fecha6", typeof(string));
            TablaRutas.Columns.Add("Fecha7", typeof(string));
            TablaRutas.Columns.Add("Fecha8", typeof(string));
            TablaRutas.Columns.Add("Fecha9", typeof(string));
            TablaRutas.Columns.Add("Fecha10", typeof(string));
            TablaRutas.Columns.Add("Fecha11", typeof(string));
            TablaRutas.Columns.Add("Fecha12", typeof(string));
            TablaRutas.Columns.Add("Fecha13", typeof(string));
            TablaRutas.Columns.Add("Fecha14", typeof(string));
            TablaRutas.Columns.Add("Fecha15", typeof(string));

            for (int i = 0; i < TablaRutas.Rows.Count; i++)
            {
                int codRuta = Convert.ToInt32(Rutas.Tables[0].Rows[i][0].ToString());
                string exbo = Rutas.Tables[0].Rows[i][6].ToString();
                NEquipo neq = new NEquipo();
                int codEquipo = Convert.ToInt32(neq.getEquipo2(exbo).Tables[0].Rows[0][0].ToString());

                DataSet fechasBoletas = nruta.getFechasBastones(codEquipo, mes, anio, tipoBoleta);

                for (int j = 0; j < fechasBoletas.Tables[0].Rows.Count; j++)
                {
                    string fecha = fechasBoletas.Tables[0].Rows[j][0].ToString();
                    string columna = "Fecha" + (j + 1);
                    TablaRutas.Rows[i][columna] = fecha;
                }
            }
            return TablaRutas;
        }


        public DataSet mostrarConsultaEquiposAsignadosRutasFechas(string codRuta, string exbo, int mes, int anio, string nombreProyecto, string personalAsignado)
        {
            return nruta.mostrarConsultaEquiposAsignadosRutasFechas( codRuta,  exbo,  mes,  anio,  nombreProyecto,  personalAsignado);
        }

        public DataSet getAllPersonalAsignadoRuta() {
            return nruta.getAllPersonalAsignadoRuta(); 
        }

        public bool existeEquipoRuta(int codRuta, int codEquipo ) {
            return nruta.existeEquipoRuta(codRuta, codEquipo);
        }

        public bool insertarExcelBastones(string direccionRuta, string nombreArchivo)
        {
            return nruta.insertarExcelBastones(direccionRuta,nombreArchivo);
        }
       

        //------------------  exterior -------------------------------------

        public DataSet getRutasTecnicoMantenimietoDia(string fechaHoy, int codTecnicoMantenimiento) { 
            return nruta.getRutasTecnicoMantenimietoDiaHoy(fechaHoy, codTecnicoMantenimiento);
        }

        public DataSet getRutasTecnicoMantenimietoAtras(string fechaHoy, int codTecnicoMantenimiento)
        {
            return nruta.getRutasTecnicoMantenimietoDiaAtras(fechaHoy, codTecnicoMantenimiento);
        }


        internal bool insertarBoletaExterior(int mes, int anio, int codequipo,
                                                bool eq_ascensorelectrico, bool eq_ascensorhidraulico,
                                                bool eq_escaleramecanica, bool eq_plataforma, bool eq_montacoches,
                                                bool eq_minicarga, bool ee_ff, bool ee_fp,
                                                bool ee_pf, bool ee_pp, bool ee_pec,
                                                bool sc_motor, bool sc_poleas,
                                                bool sc_aceitemotor, bool sc_cabletraccion, bool sc_ventilador,
                                                bool sc_freno, bool sc_bobina, bool sc_lvelocidad,
                                                bool sc_reduccionjuego, bool sc_cpu, bool sc_tarjetas,
                                                bool sc_conectores, bool sc_auxiliares, bool sc_aelectrica,
                                                bool sc_reguladordevelocidad, bool sc_unidadhidraulica, bool sc_valvulahidraulica,
                                                bool sc_cadenaprincipal, bool sc_sistemalubricacion, bool sc_contyseriedeseguridad,
                                                bool sc_accesos, bool sc_limpieza, bool c_botonera,
                                                bool c_indicadores, bool c_iluminacion, bool c_puertacabina,
                                                bool c_ajusteenviaje, bool c_ventilador, bool c_barrerafotoelec,
                                                bool c_holguradecab, bool c_guias, bool c_vidrioespejopaneles,
                                                bool c_operadordepuertas, bool c_contyseriedeseguridad, bool c_pasamanos,
                                                bool c_limpieza, bool a_botonera, bool a_indicadores,
                                                bool a_puerta, bool a_guiapatines, bool a_cerrojos,
                                                bool a_padeenclavamiento, bool a_sensores, bool a_peines,
                                                bool a_peldanosfaldon, bool a_demarcaciones, bool a_botondeemergencia,
                                                bool a_contyseriedeseguridad, bool a_senales, bool a_limpieza,
                                                bool f_cablesdetraccion, bool f_cablelimitador, bool f_cableviajero,
                                                bool f_contrapeso, bool f_fdecarrerasuperior, bool f_fdecarrerainferior,
                                                bool f_paracaidas, bool f_topespistones, bool f_poleatensora,
                                                bool f_poleas, bool f_rieles, bool f_aceiteras,
                                                bool f_stopdefosa, bool f_resortes, bool f_tensiondecadena,
                                                bool f_contyseriedeseguridad, bool f_mordazas, bool f_limpieza,
                                                string materialesyrepuesto, bool i_fusiblecontactos, bool i_botoneradepisoencorte,
                                                bool i_limites, bool i_reguladordevelocidad, bool i_frenobalataselectroiman,
                                                bool i_motordetraccion, bool i_poleas, bool i_filtraciondeaguaensalademaquinas,
                                                bool i_accesoirregularasalademaquinas, bool i_corteensenalizadoropulsadordepiso, bool i_ruidooajusteenpuertaspisocabina,
                                                bool i_iluminaciondecabina, bool i_operadordepuertas, bool i_motordeoperador,
                                                bool i_ventiladordecabina, bool i_cerrojo, bool i_sensordecabinabarrerafotocelula,
                                                bool i_filtraciondeaguaenhuecoyfoso, bool i_bajasocortedetension, bool i_sensores,
                                                bool i_malusoporusuario, bool i_iluminacionirregularensalademaquinasyfoso, bool i_otros,
                                                string receptor_ci, string receptor_cargo, string recepcion,
                                                bool cambiorepuesto, string tipoboleta, bool siningresoedificio,
                                                int codtecnico, string horallegada, string horasalida, string observaciones, float costotransporte,
                                                bool vml_am_superiorizquierdo, bool vml_am_superiorderecho, bool vml_am_inferiorizquierdo, bool vml_am_inferiorderecho,
                                                bool vml_lct_bueno, bool vml_lct_malo, bool vml_nm_bueno, bool vml_nm_malo, string vml_obs, int codresgra)
        {
            return nruta.insertarBoletaExterior( mes,  anio, codequipo,
                                                 eq_ascensorelectrico, eq_ascensorhidraulico,
                                                 eq_escaleramecanica, eq_plataforma, eq_montacoches,
                                                 eq_minicarga, ee_ff, ee_fp,
                                                 ee_pf,  ee_pp,  ee_pec,
                                                 sc_motor, sc_poleas,
                                                 sc_aceitemotor, sc_cabletraccion, sc_ventilador,
                                                 sc_freno, sc_bobina, sc_lvelocidad,
                                                 sc_reduccionjuego, sc_cpu, sc_tarjetas,
                                                 sc_conectores, sc_auxiliares, sc_aelectrica,
                                                 sc_reguladordevelocidad, sc_unidadhidraulica, sc_valvulahidraulica,
                                                 sc_cadenaprincipal, sc_sistemalubricacion, sc_contyseriedeseguridad,
                                                 sc_accesos, sc_limpieza, c_botonera,
                                                 c_indicadores, c_iluminacion, c_puertacabina,
                                                 c_ajusteenviaje, c_ventilador, c_barrerafotoelec,
                                                 c_holguradecab, c_guias, c_vidrioespejopaneles,
                                                 c_operadordepuertas, c_contyseriedeseguridad, c_pasamanos,
                                                 c_limpieza, a_botonera, a_indicadores,
                                                 a_puerta, a_guiapatines, a_cerrojos,
                                                 a_padeenclavamiento, a_sensores, a_peines,
                                                 a_peldanosfaldon, a_demarcaciones, a_botondeemergencia,
                                                 a_contyseriedeseguridad, a_senales, a_limpieza,
                                                 f_cablesdetraccion, f_cablelimitador, f_cableviajero,
                                                 f_contrapeso, f_fdecarrerasuperior, f_fdecarrerainferior,
                                                 f_paracaidas, f_topespistones, f_poleatensora,
                                                 f_poleas, f_rieles, f_aceiteras,
                                                 f_stopdefosa, f_resortes, f_tensiondecadena,
                                                 f_contyseriedeseguridad, f_mordazas, f_limpieza,
                                                 materialesyrepuesto, i_fusiblecontactos, i_botoneradepisoencorte,
                                                 i_limites, i_reguladordevelocidad, i_frenobalataselectroiman,
                                                 i_motordetraccion, i_poleas, i_filtraciondeaguaensalademaquinas,
                                                 i_accesoirregularasalademaquinas, i_corteensenalizadoropulsadordepiso, i_ruidooajusteenpuertaspisocabina,
                                                 i_iluminaciondecabina, i_operadordepuertas, i_motordeoperador,
                                                 i_ventiladordecabina, i_cerrojo, i_sensordecabinabarrerafotocelula,
                                                 i_filtraciondeaguaenhuecoyfoso, i_bajasocortedetension, i_sensores,
                                                 i_malusoporusuario, i_iluminacionirregularensalademaquinasyfoso, i_otros,
                                                 receptor_ci, receptor_cargo, recepcion,
                                                 cambiorepuesto, tipoboleta, siningresoedificio,
                                                 codtecnico, horallegada, horasalida, observaciones, costotransporte,
                                                   vml_am_superiorizquierdo,  vml_am_superiorderecho,  vml_am_inferiorizquierdo,  vml_am_inferiorderecho,
                                                 vml_lct_bueno, vml_lct_malo, vml_nm_bueno, vml_nm_malo, vml_obs, codresgra);                         
        }

        internal int getcodigoEquipoCronogramaRuta(int codigoCronogramaRuta)
        {
          DataSet tupla =  nruta.getcodigoEquipoCronogramaRuta( codigoCronogramaRuta);
          if (tupla.Tables[0].Rows.Count > 0)
          {
              int codigo;
              int.TryParse(tupla.Tables[0].Rows[0][0].ToString(), out codigo);
              return codigo;
          }
          else
              return -1;
        }

        internal bool marcarboletadeRutaRealizada(int codigoCronogramaRuta, string fechaOrigBoletaOK)
        {
            bool banderaFecha1 = nruta.esRutafecha1(codigoCronogramaRuta, fechaOrigBoletaOK);
            bool banderaFecha2 = nruta.esRutafecha2(codigoCronogramaRuta, fechaOrigBoletaOK);
            bool banderaFecha3 = nruta.esRutafecha3(codigoCronogramaRuta, fechaOrigBoletaOK);
            bool banderaFecha4 = nruta.esRutafecha4(codigoCronogramaRuta, fechaOrigBoletaOK);
            if (banderaFecha1 == true)
            {
              return  nruta.marcarFecha1Realizada(codigoCronogramaRuta);
            }
            else
                if (banderaFecha2 == true)
                {
                  return  nruta.marcarFecha2Realizada(codigoCronogramaRuta);                  
                }
                else
                    if (banderaFecha3 == true)
                    {
                      return  nruta.marcarFecha3Realizada(codigoCronogramaRuta);
                    }
                    else
                        if (banderaFecha4 == true)
                        {
                          return  nruta.marcarFecha4Realizada(codigoCronogramaRuta);
                        }
                        else
                            return false;
            
        }

        internal DataSet getUltimaBoletaInsertada()
        {
           return nruta.getUltimaBoletaInsertada();
        }

        internal bool insertardevolucionderepuestoalcliente(int codboletavisitaruta, string codedificio, string edificio, string cliente, string observaciondevolucion, string realizadopor, string realizadoporcago, string recepcionadopor, string recepcionadoporcago, string recepcionadoporci)
        {
           return nruta.insertardevolucionderepuestoalcliente( codboletavisitaruta,  codedificio,  edificio,  cliente,  observaciondevolucion,  realizadopor,  realizadoporcago,  recepcionadopor,  recepcionadoporcago,  recepcionadoporci);
        }

        internal int get_ultimaboletadeDevolucionalcliente(int codboletavisitaruta)
        {
            throw new NotImplementedException();
        }
    }
}