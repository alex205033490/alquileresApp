
using Clases.ApiRest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Windows;



namespace jycboliviaASP.net.Negocio
{
    public class NA_PruebaAPI
    {
        DBApi api = new DBApi();
        public NA_PruebaAPI() { }

        public string pruebaProducto() {
            Persona datoP = new Persona
            {
                Username = "adm",
                Password = "123"
            };

            string json = JsonConvert.SerializeObject(datoP);            
            dynamic respuesta = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login",json);
            string codVendedor = respuesta.Resultado.CodVendedor.ToString();
            string login = respuesta.Resultado.UserName.ToString();
            //MessageBox.Show(nombre + " " + apellido);
            return "codigoVendedor="+ codVendedor + " y login=" + login;
           // return respuesta.ToString();
        }

        internal string pruebaProducto2()
        {
            Persona datoP = new Persona
            {
                Username = "adm",
                Password = "123"
            };

            string json = JsonConvert.SerializeObject(datoP);
            dynamic respuesta = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", json);
             string token = respuesta.Resultado.Token.ToString();

            string web = "http://192.168.11.62/ServcioUponApi/api/v1/almacenes/productos/Busqueda?criterio=nac&usuario=adm";
            dynamic respuesta2 = api.Get_2(web, token);
            string codigo = respuesta2.Resultado[1].Codigo.ToString();
            string producto = respuesta2.Resultado[1].Descripcion.ToString();
            return "codigoP=" + codigo + " y producto=" + producto; 
            //return respuesta2.ToString();
        }

       
    }
}

public class Persona { 
    public string Username { get; set; }
    public string Password { get; set; }

}