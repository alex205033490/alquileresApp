using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Cargo
    {

        public NA_Cargo() { }
        private DA_Cargo Dcargo = new DA_Cargo();

        public bool insertar(string nombre, string detalle, int estado)
        {
            return Dcargo.insertar(nombre,detalle,estado);
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public string get_CargoResponsable(int codigo)
        {
            string consulta = "select c.codigo, c.nombre from tb_cargo c where c.codigo = "+codigo;
            DataSet dato = Dcargo.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows[0][1].ToString();
            }
            else
                return "Ninguno";
        }

        public DataSet mostrarAllDatos()
        {
            string consulta = "select c.codigo, c.nombre, c.detalle from tb_cargo c";
            return Dcargo.getDatos(consulta);
        }
    }
}