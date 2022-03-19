using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Elecciones2021.Models;
using Proyecto_Elecciones2021.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;

namespace Proyecto_Elecciones2021.Controllers
{
    public class HomeController : Controller
    {
        readonly string uri = new Url().hostUri();

        IEnumerable<PartidoPolitico> Partidos()
        {
            List<PartidoPolitico> temporal = new List<PartidoPolitico>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/Partido/partidos");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {

                    var lista = resultado.Content.ReadAsAsync<List<PartidoPolitico>>();
                    lista.Wait();

                    temporal = lista.Result;
                }
            }
            return temporal.ToList();

        }


        public ActionResult Home()
        {
            return View();
        }


        public ActionResult ListaPartidos()
        {
            var lista = Partidos();
            return View(lista);
        }

    }
}