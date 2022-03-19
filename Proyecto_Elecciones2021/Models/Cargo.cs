using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Elecciones2021.Models
{
    public class Cargo
    {
        public int idCargo { get; set; }

        [MaxLength(50)]
        public string desCargo { get; set; }
    }
}