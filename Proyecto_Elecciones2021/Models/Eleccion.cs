using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Elecciones2021.Models
{
    public class Eleccion
    {
        public int nroEleccion { get; set; }
        public int codPersona { get; set; }
        public DateTime fechaVoto { get; set; }
    }
}