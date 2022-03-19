using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Elecciones2021.Models
{
    public class Distrito
    {
        public string idDepartamento { get; set; }
        public string codProvincia { get; set; }
        public string idDistrito { get; set; }
        public string nombreDistrito { get; set; }
    }
}