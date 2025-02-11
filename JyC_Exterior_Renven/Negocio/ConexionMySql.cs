using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;



namespace jycboliviaASP.net.Negocio
{
    public class conexionMySql
    {
        // Declaramos las variables:
        public MySqlConnection MySqlConexion = new MySqlConnection();
        public MySqlCommand MySqlComando = new MySqlCommand();
        public MySqlDataReader MySqlLectura;        
        //public string CadenaDeConexion = ConfigurationManager.ConnectionStrings["MySqlCuadrosXXX"].ConnectionString;
        public string CadenaDeConexion = "";
        public conexionMySql()
        {
           // string _nombreDB = NA_Global.NombreBaseDatos;
            string _nombreDB = System.Web.HttpContext.Current.Session["NombreBaseDatos"].ToString();
            switch (_nombreDB)
            {
                case "db_renven_prueba":                    
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_renven_prueba"].ConnectionString;
                    break;
                case "db_renven":                    
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_renven"].ConnectionString;
                    break;
              
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            MySqlConexion.ConnectionString = CadenaDeConexion;
        }


   
        public bool Prueba_Conexion()
        {
            MySqlConnection mysql_conexion = new MySqlConnection(CadenaDeConexion);
            try
            {                
                mysql_conexion.Open();
                mysql_conexion.Close();
                return true;
            }
            catch (Exception)
            {
                mysql_conexion.Close();
                return false;
            }
        }
        
        public DataSet consultaMySql(string consulta)
        {
            MySqlComando = new MySqlCommand(consulta);
            MySqlComando.Connection = MySqlConexion;
            MySqlDataAdapter mdatos = new MySqlDataAdapter(MySqlComando);
            MySqlConexion.Open();
            DataSet datosMysql = new DataSet();
            mdatos.Fill(datosMysql);
            MySqlConexion.Close();
            return datosMysql;
        }


        public DataSet RellenarConConsulta(DataSet tablaMySql,string consulta)
        {
            MySqlComando = new MySqlCommand(consulta);
            MySqlComando.Connection = MySqlConexion;
            MySqlDataAdapter mdatos = new MySqlDataAdapter(MySqlComando);
            MySqlConexion.Open();
            mdatos.Fill(tablaMySql);
            MySqlConexion.Close();
            return tablaMySql;
        }

        public Boolean ejecutarMySql(string consulta2)
        {
            try
            {                
                MySqlComando = new MySqlCommand(consulta2);
                MySqlComando.Connection = MySqlConexion;
                MySqlConexion.Open();
                MySqlComando.ExecuteNonQuery();
                MySqlConexion.Close();
                return true;
            }
            catch (Exception) 
            {
                MySqlConexion.Close();
                return false;
            }
        }

        public Boolean ejecutarMysql2(MySqlCommand comando)
        {
            try
            {
                comando.Connection = MySqlConexion;
                MySqlConexion.Open();
                comando.ExecuteNonQuery();
                MySqlConexion.Close();
                return true;


            }catch(Exception)
            {
                
                return false;
            }
            finally
            {
                MySqlConexion.Close();
            }
        }
    }
}