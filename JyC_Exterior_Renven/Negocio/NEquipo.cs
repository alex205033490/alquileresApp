
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;
using System.Data.SqlTypes;

namespace jycboliviaASP.net.Negocio
{
    public class NEquipo
    {

        DEquipo equipo = new DEquipo();
        List<int> EstadosValidosProyecto = new List<int> { 4, 6, 7, 8, 9, 10 };
        List<int> EstadosValidosImportacion = new List<int> {3, 4, 5, 11, 12, 13, 14, 15, 16, 17, 18};

        public bool registrar(string exbo, string fecha, string fechaActaProvisional, string fechaActaTecnico, string fechaActaDefinitiva, int codigoActualizacion, int codigoProyecto, string fechaEquipoObra, string fechaEquipoEntregado, string tipologia, int codTipoEquipo, int codmarca, string fiscalPRoyecto, string fechaAprobacionLimitePlanos, string modelo, string pasajero, int parada, string velocidad, string fechaAprobacionPlano, string FechaEntregaCliente, string fechaHabilitacionEquipo, string fechaaproximadaembarque, string fechapagoembarque) 
        {
            return equipo.insertarEquipo(exbo, fecha, fechaActaProvisional, fechaActaTecnico, fechaActaDefinitiva, codigoActualizacion, codigoProyecto, fechaEquipoObra, fechaEquipoEntregado, tipologia, codTipoEquipo, codmarca, fiscalPRoyecto, fechaAprobacionLimitePlanos,modelo,pasajero,parada,velocidad,  fechaAprobacionPlano,  FechaEntregaCliente,  fechaHabilitacionEquipo, fechaaproximadaembarque, fechapagoembarque);
        }

        public bool modificar(int codigo, string fechaActaProvisional, string fechaActaTecnico, string fechaActaDefinitiva, int codigoActualizacion, string fechaEquipoObra, string fechaEquipoEntregado, string tipologia, int codTipoEquipo, int codmarca, string fiscalProyecto, string fechalimiteAprobacionPlanos, string modelo, string pasajero, int parada, string velocidad, string fechaAprobacionPlano, string fechaEntregaCliente, string fechaHabilitacionEquipo, string fechaaproximadaembarque, string fechapagoembarque, string fechaConfirmacionPagoEmbarque,string CLICODSIMEC)
        {
            return equipo.modificarEquipo(codigo, fechaActaProvisional, fechaActaTecnico, fechaActaDefinitiva, codigoActualizacion, fechaEquipoObra, fechaEquipoEntregado, tipologia, codTipoEquipo, codmarca, fiscalProyecto, fechalimiteAprobacionPlanos, modelo, pasajero, parada, velocidad, fechaAprobacionPlano, fechaEntregaCliente, fechaHabilitacionEquipo, fechaaproximadaembarque, fechapagoembarque, fechaConfirmacionPagoEmbarque,  CLICODSIMEC);
        }

        public bool ModificarFechaEstadoEquipo2(int codEquipo, int CodFechaEstadoEquipo, string fechalimiteplanosAprovacion, string fechaAproximadaArriboPuerto)
        {
            return equipo.ModificarFechaEstadoEquipo2(codEquipo, CodFechaEstadoEquipo, fechalimiteplanosAprovacion, fechaAproximadaArriboPuerto);
        }

        public bool ModificarFechaEstadoEquipo(int codEquipo, int CodFechaEstadoEquipo)
        {
            return equipo.ModificarFechaEstadoEquipo(codEquipo, CodFechaEstadoEquipo);
        }

        public bool actualizar_importacionJYCIA(int codigo, string nrofactura, string fechafactura, float montofactura, string fechagiro, float montogiro1, float montogiro2, float montogiro3, float montogiro4, float montogiro5, float valorfob, float valortransportemaritimo2, string nrocontenedor, string fechagiro2, string fechagiro3, string fechagiro4, string fechagiro5, bool primerpago, bool segundopago, bool tercerpago)
        { 
            return equipo.actualizar_importacionJYCIA(codigo,  nrofactura,  fechafactura,  montofactura,  fechagiro,  montogiro1,  montogiro2,  montogiro3,  montogiro4,  montogiro5,  valorfob,  valortransportemaritimo2,  nrocontenedor,  fechagiro2,  fechagiro3 ,  fechagiro4,  fechagiro5,  primerpago, segundopago,  tercerpago);
        }

        public bool eliminar1(int codigo)
        {
            return equipo.eliminarEquipo1(codigo);
        }

        public DataSet buscar(string exbo)
        {
            DataSet lista = equipo.buscarEquipo(exbo);
            return lista;
        }

        public DataSet listar()
        {
            DataSet lista = equipo.listarEquipo();
            return lista;
        }

        public int cantiListaEquipo2(string exbo, string proyecto, string NombreEstado)
        {
            DataSet lista = equipo.cantiListaEquipo2( exbo,  proyecto,  NombreEstado);
            int result;
            bool sw = int.TryParse(lista.Tables[0].Rows[0][0].ToString(),out result);
            if (sw == true)
            {
                return result;
            }
            else
                return 0;
            
        }

        public DataSet listarEquipo2(string exbo, string proyecto, string NombreEstado, bool exportar)
        {
            return equipo.listarEquipo2(exbo, proyecto,NombreEstado, exportar);        
        }


        public string aFecha(string fecha)
        {
            if (fecha == "" || fecha == "&nbsp;")
            {
                return fecha = "null";   
            }
            
           else {
               DateTime fecha_ = Convert.ToDateTime(fecha);
               int dia = fecha_.Day;
               int mes = fecha_.Month;
               int anio = fecha_.Year;
               string _fecha = anio + "/" + mes + "/" + dia;
               return _fecha;
           }

        }

        public string obtenerFechaActual()
        {
            return System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public DataSet listaTecnicoMantenimiento()
        {
            DataSet lista = equipo.listarTecnicoManteniento();
            return lista;
        }

        public DataSet listaTecnicoInstalador() 
        {
            DataSet lista = equipo.listarTecnicoInstalador();
            return lista;
        }

        public DataSet listaSupervisorTecnico()
        {
            DataSet lista = equipo.listarSupervisorTecnico();
            return lista;
        }

        public DataSet listaCobrador()
        {
            DataSet lista = equipo.listarCobrador();
            return lista;
        }

        public DataSet listaEncargadoCobro()
        {
            DataSet lista = equipo.listarEncargadoCobro();
            return lista;
        }

      

        public bool bandera1erpago(int codEquipo) {
            DataSet tuplaRes = equipo.getEquipoJYC_pagos(codEquipo);
            string dato = tuplaRes.Tables[0].Rows[0][0].ToString();
            bool bandera = false;

            if (dato.Equals("True"))
            {
                bandera = true;
            }
            else
                bandera = false;

            return bandera;        
        }

        public bool bandera2dopago(int codEquipo)
        {
            DataSet tuplaRes = equipo.getEquipoJYC_pagos(codEquipo);
            string dato = tuplaRes.Tables[0].Rows[0][1].ToString();
            bool bandera = false;

            if (dato.Equals("True"))
            {
                bandera = true;
            }
            else
                bandera = false;

            return bandera;        

        }

        public bool bandera3erpago(int codEquipo)
        {
            DataSet tuplaRes = equipo.getEquipoJYC_pagos(codEquipo);
            string dato = tuplaRes.Tables[0].Rows[0][2].ToString();
            bool bandera = false;

            if (dato.Equals("True"))
            {
                bandera = true;
            }
            else
                bandera = false;

            return bandera;        

        }


        public int ultimoinsertado() {
            return equipo.ultimoinsertado();
        }

        public bool existeEquipo(string Exbo) {
            DataSet tuplaUsuario = getEquipo2(Exbo);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public int getCodigoEstadoEquipo_fechaEstado(int codigoFechaEstadoEquipo) {
            return equipo.getCodigoEstadoEquipo_FechaEstado(codigoFechaEstadoEquipo);
        }

        public DataSet getEquipo2(string Exbo)
        {
            string consulta = "select * from tb_equipo eq where eq.exbo = '"+Exbo+"'";
            DataSet resultado = equipo.getDatos(consulta);
            return resultado;
        }


        ////--------------------------------------------
        public DataSet listaControlPedido()
        {
            DataSet lista = equipo.listarControlPedido();
            return lista;
        }

        public bool registrarEquipoControlPedido(string exbo, string fecha, int codigoProyecto, bool r110, bool r148, bool r106, bool r107, bool r109, bool r113, bool ventaSistema, float primerPago, string tipologia, string fichero, string fechaventa, bool ventacontrato, string modelo, int parada, string pasajero, string velocidad, string vc, int codTipoEquipo, int codmarca, string ciudadVenta, string ciudadinstalacion, string fechaAproxEmbarque, float valorTransporteMaritimo, bool inventario, string fechapagoembarque, string fechaEquipoSegunContrato, string codigoContrato, string consignatario, bool contratofirmado)
        {
            return equipo.insertarEquipoControlPedido(exbo, fecha, codigoProyecto, r110, r148, r106, r107, r109, r113, ventaSistema, primerPago, tipologia, fichero, fechaventa, ventacontrato, modelo, parada, pasajero, velocidad, vc, codTipoEquipo, codmarca, ciudadVenta, ciudadinstalacion, fechaAproxEmbarque, valorTransporteMaritimo, inventario, fechapagoembarque, fechaEquipoSegunContrato,  codigoContrato,   consignatario,  contratofirmado);
        }

        public int primerEstadoInsertado()
        {
            return equipo.primerEstadoInsertado();
        }

        public int obtenerCodigoEquipo(string nombreEquipo)
        {
            return equipo.getCodigoEquipo(nombreEquipo);
        }


        public bool modificarEquipoControlPedido(int codigo, string exbo, string tipologia, bool r110, bool r148, bool r106, bool r107, bool r109, bool r113, bool ventaSistema, float primerPago, int codigoProyecto, string fichero, string fechaventa, bool ventacontrato, string modelo, int parada, string pasajero, string velocidad, string vc, int codTipoEquipo, int codmarca, string ciudadVenta, string ciudadinstalacion, string fechaAproxEmbarque, float valorTransporteMaritimo, bool inventario, int estado, string fechapagoembarque, string fechaEquipoSegunContrato, string codigoContrato, string consignatario, bool contratofirmado)
        {
            return equipo.modificarEquipoControlPedido(codigo, exbo, tipologia, r110, r148, r106, r107, r109, r113, ventaSistema, primerPago, codigoProyecto, fichero, fechaventa, ventacontrato, modelo, parada, pasajero, velocidad, vc, codTipoEquipo, codmarca, ciudadVenta, ciudadinstalacion, fechaAproxEmbarque, valorTransporteMaritimo, inventario, estado,  fechapagoembarque,  fechaEquipoSegunContrato,  codigoContrato,   consignatario,  contratofirmado);
        }


        public DataSet buscadorEquipo(string exbo)
        {
            DataSet lista = equipo.buscador(exbo);
            return lista;
        }

        public DataSet buscadorNombreExbo(string exbo, string edificio)
        {
            DataSet lista = equipo.buscadorNombreExbo(exbo,edificio);
            return lista;
        }

        public DataSet buscadorNombreExbo2(string edificio)
        {
            DataSet lista = equipo.buscadorNombreExbo2(edificio);
            return lista;
        }

        public bool obtenerValor(int codigoEquipo, int nro)
        {
            int i = equipo.obtenerValor(codigoEquipo, nro);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      /*  public DataSet BuscarControlEquipos(string nombreProyecto, string exbo, string nombrePropietario, string pasajero, string parada, string modelo, string velocidad)
        {
            return equipo.BuscarControlEquipos(nombreProyecto, exbo,nombrePropietario,pasajero,parada,modelo,velocidad);        
        }
        */
        public DataSet BuscarControlEquipos2(string nombreProyecto, string exbo, string nombrePropietario, string pasajero, string parada, string modelo, string velocidad, string fechaDesde, string fechahasta, string fichero) {
            return equipo.BuscarControlEquipos2(nombreProyecto, exbo, nombrePropietario, pasajero, parada, modelo, velocidad,fechaDesde,fechahasta,fichero);        
        }

        public DataSet Buscar_ImportacionJYCIA(string Edificio, string exbo, string nrofactura, string fechafactura, string montofactura, string fechagiro, string montogiro1, string montogiro2, string montogiro3, string montogiro4, string montogiro5)
        {
            return equipo.Buscar_ImportacionJYCIA( Edificio,  exbo,  nrofactura,  fechafactura,  montofactura,  fechagiro,  montogiro1,  montogiro2,  montogiro3,  montogiro4,  montogiro5);
        }

        public DataSet getEquipo(int codigoEquipo)
        {
            return equipo.getEquipo(codigoEquipo);
        }

        public DataSet listarEquipo2ConFiscalProyecto(string exbo, string proyecto, int codFiscal, string NombreEstado, bool exportar)
        {
            return equipo.listarEquipo2ConFiscalProyecto(exbo, proyecto, codFiscal, NombreEstado, exportar);
        }

        /** estados permitidos para Proyecto */
        public bool estaPermitidoEstadoProyecto(int estado) {
            int index = EstadosValidosProyecto.IndexOf(estado);
            if (index > -1)
            {
                return true;
            }
            else
                return false;
        }


        /** estados permitidos para Importacion */
        public bool estaPermitidoEstadoImportacion(int estado)
        {
            int index = EstadosValidosImportacion.IndexOf(estado);
            if (index > -1)
            {
                return true;
            }
            else
                return false;
        }


        public int getCodigoEstadoActual(int codEquipo) {
            return equipo.getCodigoEstado_Actual(codEquipo);
        }

        public bool modificarEquipo(int codEquipo,int parada, string pasajeros, string velocidad, string modelo) {
            return equipo.modificarEquipo(codEquipo, parada,  pasajeros,  velocidad,  modelo);   
        }

        public int getcodigoProyecto(int codEquipo )
        {
            DataSet dato = equipo.getEquipo(codEquipo);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][11].ToString());
            }
            else
                return -1;
        }

        public string getFechaAproxArriboPuerto(string exbo) {
            return equipo.getFechaAproxArriboPuerto(exbo);
        }

        public DataSet getListaMaestraEquipos() {
            return equipo.getListaMaestraEquipos();
        }


        public DataSet getConsultaCodigoDeAutenticacion(string exbo, string edificio)
        {
            return equipo.getConsultaCodigoDeAutenticacion( exbo,  edificio);
        }

        public DataSet getConsultaCodigoDeAutenticacion_QR(string exbo, string edificio, string dirArchivo)
        {
            return equipo.getConsultaCodigoDeAutenticacion_QR(exbo,edificio,dirArchivo);
        }

        public DataSet getequipoControlPedido(int codigoEquipo)
        {
            return equipo.getequipoControlPedido(codigoEquipo);
        }

        internal string get_codigoClienteSimec(string exbo)
        {
            return equipo.get_codigoClienteSimec(exbo);
        }

        internal int get_cantidadEquiposdeEdificio(string edificio)
        {
            DataSet tupla = equipo.get_cantidadEquiposdeEdificio(edificio);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                int cantEquipos = tupla.Tables[0].Rows.Count;
                return cantEquipos;
            }
            else
                return 0;
        }

        internal DataSet get_equipos(string nombreProyecto)
        {
           DataSet tuplas = equipo.get_equipos(nombreProyecto);
           return tuplas;
        }

        internal bool insertarEncuestaR220(int codEquipo, int codPregunta, bool Conforme, string Observaciones)
        {
            return equipo.insertarEncuestaR220( codEquipo,  codPregunta,  Conforme,  Observaciones);
        }
    }
}