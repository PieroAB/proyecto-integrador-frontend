using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Elecciones2021.Models;
using Proyecto_Elecciones2021.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Http;

namespace Proyecto_Elecciones2021.Controllers
{
    public class VotacionController : Controller
    {
        readonly string uri = new Url().hostUri();
        public IEnumerable<FichaPresidencial> CargaFichaPresidencial()
        {
            var temporal = new List<FichaPresidencial>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/Votacion/fichapresidencial");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {

                    var lista = resultado.Content.ReadAsAsync<List<FichaPresidencial>>();
                    lista.Wait();

                    temporal = lista.Result;
                }
            }
            return temporal.ToList();
        }

        public IEnumerable<FichaCongresal> CargaFichaCongresal(string dep)
        {
            List<FichaCongresal> temporal = new List<FichaCongresal>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/Votacion/fichacongresal/" + dep);
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {

                    var lista = resultado.Content.ReadAsAsync<List<FichaCongresal>>();
                    lista.Wait();

                    temporal = lista.Result;
                }
            }
            return temporal.ToList();
        }

        IEnumerable<Candidato> CargaNroCongresistasPorPartidoPorDepartamento(string iddep, int codpar)
        {
            var temporal = new List<Candidato>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/Votacion/nroCongresistas/" + codpar + "/" + iddep);
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {

                    var lista = resultado.Content.ReadAsAsync<List<Candidato>>();
                    lista.Wait();

                    temporal = lista.Result;
                }
            }
            return temporal.ToList();
        }



        public IEnumerable<FichaParlamental> CargaFichaParlamental()
        {
            List<FichaParlamental> temporal = new List<FichaParlamental>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/Votacion/fichaparlamental");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {

                    var lista = resultado.Content.ReadAsAsync<List<FichaParlamental>>();
                    lista.Wait();

                    temporal = lista.Result;
                }
            }
            return temporal.ToList();
        }

        IEnumerable<Candidato> CargaNroParlamentariosPorPartido(int codpar)
        {
            var temporal = new List<Candidato>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/Votacion/nroParlamentarios/" + codpar);
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {

                    var lista = resultado.Content.ReadAsAsync<List<Candidato>>();
                    lista.Wait();

                    temporal = lista.Result;
                }
            }
            return temporal.ToList();
        }

        public ActionResult Sufragio()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Security");
            }
            var persona = (Persona)Session["login"];

            ViewBag.fpresidencial = CargaFichaPresidencial();
            ViewBag.fcongresal = CargaFichaCongresal(persona.codDepartamento);
            ViewBag.fparlamental = CargaFichaParlamental();
            var votacion = new Votacion();

            return View(votacion);
        }

        [HttpPost]
        public ActionResult Sufragio(Votacion votacion)
        {
            if (Session["login"] == null)
            {

                return RedirectToAction("Login", "Security");
            }
            if (ModelState.IsValid == false)
            {
                return View(votacion);
            }
            var persona = (Persona)Session["login"];
            votacion.idDepVotante = persona.codDepartamento;
            votacion.idVotante = persona.idPersona;
            var mensaje = "";
            using (var servicio = new HttpClient())
            {

                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.PostAsJsonAsync("api/Votacion/nuevoVoto", votacion);
                tarea.Wait();
                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var mensajerecibido = resultado.Content.ReadAsAsync<string>();
                    mensajerecibido.Wait();
                    mensaje = mensajerecibido.Result;
                }
            }
            return RedirectToAction("Felicidades", "Votacion", new { msg = mensaje });
        }

        public ActionResult Felicidades()
        {
            Session.Clear();
            return View();
        }
    }
}