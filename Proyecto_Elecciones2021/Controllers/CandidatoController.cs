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
using System.Diagnostics;
using System.Net.Http;

namespace Proyecto_Elecciones2021.Controllers
{
    public class CandidatoController : Controller
    {
            readonly TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            readonly string uri = new Url().hostUri();
            readonly string cadena = new Conexion().GetConexion();

            IEnumerable<Departamento> Departamentos()
            {
                List<Departamento> temporal = new List<Departamento>();
                using (var servicio = new HttpClient())
                {
                    servicio.BaseAddress = new Uri(uri);
                    var tarea = servicio.GetAsync("api/Candidato/departamentos");
                    tarea.Wait();

                    var resultado = tarea.Result;
                    if (resultado.IsSuccessStatusCode)
                    {

                        var lista = resultado.Content.ReadAsAsync<List<Departamento>>();
                        lista.Wait();
                        temporal = lista.Result;
                    }
                }
                return temporal.ToList();
            }

            private PartidoPolitico PartidoPoliticoPorId(int id)
            {
                PartidoPolitico temporal = null;
                using (var servicio = new HttpClient())
                {
                    servicio.BaseAddress = new Uri(uri);
                    var tarea = servicio.GetAsync("api/Partido/partidos/" + id);
                    tarea.Wait();

                    var resultado = tarea.Result;
                    if (resultado.IsSuccessStatusCode)
                    {
                        var lista = resultado.Content.ReadAsAsync<PartidoPolitico>();
                        lista.Wait();
                        temporal = lista.Result;
                    }
                }
                return temporal;
            }

            private IEnumerable<PlanGobierno> PlanDeGobiernoPorId(int id)
            {
                List<PlanGobierno> temporal = new List<PlanGobierno>();
                using (var servicio = new HttpClient())
                {
                    servicio.BaseAddress = new Uri(uri);
                    var tarea = servicio.GetAsync("api/Partido/planesGobierno/" + id);
                    tarea.Wait();

                    var resultado = tarea.Result;
                    if (resultado.IsSuccessStatusCode)
                    {

                        var lista = resultado.Content.ReadAsAsync<List<PlanGobierno>>();
                        lista.Wait();
                        temporal = lista.Result;
                    }
                }
                return temporal.ToList();

            }



            IEnumerable<Candidato> CandidatosPresidenciales(int id)
            {
                List<Candidato> temporal = new List<Candidato>();
                using (var servicio = new HttpClient())
                {
                    servicio.BaseAddress = new Uri(uri);
                    var tarea = servicio.GetAsync("api/Candidato/presidencial/" + id);
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

            [HttpGet]
            public JsonResult CandidatosCongresistas(int id, string depar = "")
            {
                List<Candidato> temporal = new List<Candidato>();
                using (var servicio = new HttpClient())
                {
                    servicio.BaseAddress = new Uri(uri);
                    var tarea = servicio.GetAsync("api/Candidato/congresal/" + id + "/" + depar);
                    tarea.Wait();

                    var resultado = tarea.Result;
                    if (resultado.IsSuccessStatusCode)
                    {

                        var lista = resultado.Content.ReadAsAsync<List<Candidato>>();
                        lista.Wait();
                        temporal = lista.Result;
                    }
                }
                return Json(temporal.ToList(), JsonRequestBehavior.AllowGet);
            }

            IEnumerable<Candidato> CandidatosParlamentarios(int id)
            {
                List<Candidato> temporal = new List<Candidato>();
                using (var servicio = new HttpClient())
                {
                    servicio.BaseAddress = new Uri(uri);
                    var tarea = servicio.GetAsync("api/Candidato/parlamental/" + id);
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

            public ActionResult CandidatosPorPartido(int? id = null)
            {
                if (id == null)
                {
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    ViewBag.partido = PartidoPoliticoPorId((int)id);
                    ViewBag.plan = PlanDeGobiernoPorId((int)id);
                    ViewBag.codigoPart = (int)id;
                    ViewBag.presidenciales = CandidatosPresidenciales((int)id);
                    /* ViewBag.congresistas = CandidatosCongresistas((int)id, depar);*/
                    ViewBag.parlamentarios = CandidatosParlamentarios((int)id);
                    ViewBag.departamentos = new SelectList(Departamentos(), "idDepartamento", "nombreDepartamento");
                }
                return View();
            }

        }
    }