using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Elecciones2021.Utils
{
    public class Conexion
    {
        public string GetConexion() {
            //string cadena = "server=LAPTOP-1SEI7VDO\\PRUEBAS2019;database=Elecciones_2021; integrated security=true";
            string cadena = "server=.;database=Elecciones_2021; integrated security=true";
            return cadena;
        }
    }
}