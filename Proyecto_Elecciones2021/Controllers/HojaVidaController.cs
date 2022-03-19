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
    public class HojaVidaController : Controller
    {
        readonly string uri = new Url().hostUri();
        readonly TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        private Persona ObtenerPersona(int id)
        {
            Persona temporal = null;
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id);
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<Persona>();
                    lista.Wait();
                    temporal = lista.Result;
                }
            }
            return temporal;
        }

        private EducacionBasicaHV ObtenerEducacacionBasicaHV(int id)
        {
            EducacionBasicaHV temporal = null;
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id + "/eduBasica");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<EducacionBasicaHV>();
                    lista.Wait();
                    temporal = lista.Result;
                }
                else
                {
                    temporal = new EducacionBasicaHV();
                }
            }
            return temporal;
        }

        IEnumerable<EstudiosTecnicosHV> ObtenerEstudiosTecnicosHV(int id)
        {
            List<EstudiosTecnicosHV> temporal = new List<EstudiosTecnicosHV>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id + "/estTecnicos");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<List<EstudiosTecnicosHV>>();
                    lista.Wait();
                    temporal = lista.Result;
                }
                if (temporal.Count() == 0)
                {
                    var element = new EstudiosTecnicosHV();
                    temporal.Add(element);
                }
            }
            return temporal.ToList();
        }

        IEnumerable<EstudiosNoUniversitariosHV> ObtenerEstudiosNoUniversitariosHV(int id)
        {
            List<EstudiosNoUniversitariosHV> temporal = new List<EstudiosNoUniversitariosHV>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id + "/estNoUniversitarios");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<List<EstudiosNoUniversitariosHV>>();
                    lista.Wait();
                    temporal = lista.Result;
                }
                if (temporal.Count() == 0)
                {
                    var element = new EstudiosNoUniversitariosHV();
                    temporal.Add(element);
                }
            }
            return temporal.ToList();
        }

        IEnumerable<EstudiosUniversitariosHV> ObtenerEstudiosUniversitariosHV(int id)
        {
            List<EstudiosUniversitariosHV> temporal = new List<EstudiosUniversitariosHV>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id + "/estUniversitarios");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<List<EstudiosUniversitariosHV>>();
                    lista.Wait();
                    temporal = lista.Result;
                }
                if (temporal.Count() == 0)
                {
                    var element = new EstudiosUniversitariosHV();
                    temporal.Add(element);
                }

            }
            return temporal.ToList();
        }

        IEnumerable<EstudiosPostgradoHV> ObtenerEstudiosPostgradoHV(int id)
        {
            List<EstudiosPostgradoHV> temporal = new List<EstudiosPostgradoHV>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id + "/estPostgrado");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<List<EstudiosPostgradoHV>>();
                    lista.Wait();
                    temporal = lista.Result;
                }
                if (temporal.Count() == 0)
                {
                    var element = new EstudiosPostgradoHV();
                    temporal.Add(element);
                }

            }
            return temporal.ToList();
        }

        IEnumerable<TrabajosHV> ObtenerTrabajosHV(int id)
        {
            List<TrabajosHV> temporal = new List<TrabajosHV>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id + "/trabajos");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<List<TrabajosHV>>();
                    lista.Wait();
                    temporal = lista.Result;
                }
                if (temporal.Count() == 0)
                {
                    var element = new TrabajosHV();
                    temporal.Add(element);
                }
            }
            return temporal.ToList();
        }

        IEnumerable<AmbitosPenalesHV> ObtenerAmbitosPenalesHV(int id)
        {
            List<AmbitosPenalesHV> temporal = new List<AmbitosPenalesHV>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id + "/ambitosPenales");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<List<AmbitosPenalesHV>>();
                    lista.Wait();
                    temporal = lista.Result;
                }
                if (temporal.Count() == 0)
                {
                    var element = new AmbitosPenalesHV();
                    temporal.Add(element);
                }
            }
            return temporal.ToList();
        }

        IEnumerable<OtrasPenalidadesHV> ObtenerOtrasPenalidadesHV(int id)
        {
            List<OtrasPenalidadesHV> temporal = new List<OtrasPenalidadesHV>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id + "/otrasPenalidades");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<List<OtrasPenalidadesHV>>();
                    lista.Wait();
                    temporal = lista.Result;
                }
                if (temporal.Count() == 0)
                {
                    var element = new OtrasPenalidadesHV();
                    temporal.Add(element);
                }
            }
            return temporal.ToList();
        }

        public ActionResult HojaVida(int? idper = null)
        {
            if (idper == null)
            {
                return RedirectToAction("CandidatosPorPartido", "Candidato");
            }
            else
            {
                ViewBag.personahv = ObtenerPersona((int)idper);
                ViewBag.edubashv = ObtenerEducacacionBasicaHV((int)idper);
                ViewBag.esttechv = ObtenerEstudiosTecnicosHV((int)idper);
                ViewBag.estnounihv = ObtenerEstudiosNoUniversitariosHV((int)idper);
                ViewBag.estunihv = ObtenerEstudiosUniversitariosHV((int)idper);
                ViewBag.estposhv = ObtenerEstudiosPostgradoHV((int)idper);
                ViewBag.trabajoshv = ObtenerTrabajosHV((int)idper);
                ViewBag.ambpenhv = ObtenerAmbitosPenalesHV((int)idper);
                ViewBag.otrapenhv = ObtenerOtrasPenalidadesHV((int)idper);
            }
            return View();
        }
    }
}