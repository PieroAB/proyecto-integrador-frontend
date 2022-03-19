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
    public class CompararController : Controller
    {


        readonly string uri = new Url().hostUri();
        readonly TextInfo myTI = new CultureInfo("en-US", false).TextInfo;


        PartidoPolitico ObtenerResumenPartidoPolitico(int id)
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

        Persona ObtenerResumenPersona(int id)
        {
            Persona temporal = null;
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/Candidato/" + id + "/resumen");
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


        PartidoPresidente ObtenerPresidentexPartido(int id)
        {
            PartidoPresidente temporal = null;
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/Partido/" + id + "/presidente");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var lista = resultado.Content.ReadAsAsync<PartidoPresidente>();
                    lista.Wait();
                    temporal = lista.Result;
                }
            }
            return temporal;
        }


        IEnumerable<TipoEleccion> Tipoelecciones()
        {
            var temporal = new List<TipoEleccion>();

            TipoEleccion obj0 = new TipoEleccion()
            {
                idTipEleccion = 0,
                descTipEleccion = "Seleccione"
            };
            TipoEleccion obj1 = new TipoEleccion()
            {
                idTipEleccion = 1,
                descTipEleccion = "Resumen de plan de Gobierno"
            };
            TipoEleccion obj2 = new TipoEleccion()
            {
                idTipEleccion = 2,
                descTipEleccion = "Hojas de vida - presidente"
            };

            temporal.Add(obj0);
            temporal.Add(obj1);
            temporal.Add(obj2);

            return temporal;
        }

        IEnumerable<PlanGobierno> PlanDeGobiernoPorId(int id)
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
                if (temporal.Count() == 0)
                {
                    var reg = new PlanGobierno()
                    {
                        codPartido = 0,
                        nroPro = 0,
                        socPro = "",
                        ecoPro = "",
                        natPro = "",
                        insPro = ""
                    };
                    temporal.Add(reg);
                }
            }
            return temporal.ToList();

        }

        EducacionBasicaHV ObtenerEducacionBasicaHV(int id)
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
            var temporal = new List<EstudiosTecnicosHV>();
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
            var temporal = new List<EstudiosNoUniversitariosHV>();
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
            var temporal = new List<EstudiosUniversitariosHV>();
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
            var temporal = new List<EstudiosPostgradoHV>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/HojaVida/candidato/" + id + "/estPosgrado");
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
            var temporal = new List<TrabajosHV>();
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
            var temporal = new List<AmbitosPenalesHV>();
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
            var temporal = new List<OtrasPenalidadesHV>();
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


        IEnumerable<PartidoPolitico> PartidosConPresidente()
        {
            var temporal = new List<PartidoPolitico>();
            using (var servicio = new HttpClient())
            {
                servicio.BaseAddress = new Uri(uri);
                var tarea = servicio.GetAsync("api/Partido/partidoypresidente");
                tarea.Wait();

                var resultado = tarea.Result;
                if (resultado.IsSuccessStatusCode)
                {

                    var lista = resultado.Content.ReadAsAsync<List<PartidoPolitico>>();
                    lista.Wait();
                    temporal = lista.Result;
                }
                return temporal.ToList();
            }
        }


        public ActionResult Busqueda(int seleccion = 0, int part1 = 0, int part2 = 0)
        {

            ViewBag.tipoEleccion = new SelectList(Tipoelecciones(), "idTipEleccion", "descTipEleccion");
            ViewBag.partidos = new SelectList(PartidosConPresidente(), "codPartido", "nombrePartido");
            if (seleccion == 0)
            {
                ViewBag.mensaje = "Seleccione el tipo de elección";
            }
            else if (seleccion == 1)
            {
                if (part1 == 0 || part2 == 0)
                {
                    ViewBag.mensaje = "Seleccione el partido político a comparar";
                }
                else
                {
                    if (part1 == part2)
                    {

                        ViewBag.mensaje = "Seleccione partidos distintos";
                    }
                    else
                    {
                        int idpar1 = part1;
                        int idpar2 = part2;
                        return RedirectToAction("ResumenPlanGobierno", "Comparar", new { idpar1, idpar2 });
                    }

                }
            }
            else if (seleccion == 2)
            {
                if (part1 == 0 || part2 == 0)
                {
                    ViewBag.mensaje = "Seleccione el partido político a comparar";
                }
                else
                {
                    if (part1 == part2)
                    {
                        ViewBag.mensaje = "Seleccione partidos distintos";
                    }
                    else
                    {
                        int idpar1 = part1;
                        int idpar2 = part2;
                        return RedirectToAction("ComparaCandidatos", "Comparar", new { idcan1 = idpar1, idcan2 = idpar2 });
                    }

                }
            }
            return View();
        }

        public ActionResult ResumenPlanGobierno(int? idpar1 = null, int? idpar2 = null)
        {
            if (idpar1 == null || idpar2 == null)
            {
                return RedirectToAction("Busqueda", "Comparar");
            }
            else
            {
                ViewBag.partido1 = ObtenerResumenPartidoPolitico((int)idpar1);
                ViewBag.plan1 = PlanDeGobiernoPorId((int)idpar1);

                ViewBag.partido2 = ObtenerResumenPartidoPolitico((int)idpar2);
                ViewBag.plan2 = PlanDeGobiernoPorId((int)idpar2);
            }
            return View();
        }

        public ActionResult ComparaCandidatos(int? idcan1 = null, int? idcan2 = null)
        {

            if (idcan1 == null || idcan2 == null)
            {
                return RedirectToAction("Busqueda", "Comparar");
            }
            else
            {

                int codper1 = ObtenerPresidentexPartido((int)idcan1).codigo;
                int codper2 = ObtenerPresidentexPartido((int)idcan2).codigo;

                ViewBag.persona1 = ObtenerResumenPersona(codper1);
                ViewBag.edubashv1 = ObtenerEducacionBasicaHV(codper1);
                ViewBag.esttechv1 = ObtenerEstudiosTecnicosHV(codper1);
                ViewBag.estnounihv1 = ObtenerEstudiosNoUniversitariosHV(codper1);
                ViewBag.estunihv1 = ObtenerEstudiosUniversitariosHV(codper1);
                ViewBag.estposhv1 = ObtenerEstudiosPostgradoHV(codper1);
                ViewBag.trabajoshv1 = ObtenerTrabajosHV(codper1);
                ViewBag.ambpenhv1 = ObtenerAmbitosPenalesHV(codper1);
                ViewBag.otrapenhv1 = ObtenerOtrasPenalidadesHV(codper1);

                ViewBag.persona2 = ObtenerResumenPersona(codper2);
                ViewBag.edubashv2 = ObtenerEducacionBasicaHV(codper2);
                ViewBag.esttechv2 = ObtenerEstudiosTecnicosHV(codper2);
                ViewBag.estnounihv2 = ObtenerEstudiosNoUniversitariosHV(codper2);
                ViewBag.estunihv2 = ObtenerEstudiosUniversitariosHV(codper2);
                ViewBag.estposhv2 = ObtenerEstudiosPostgradoHV(codper2);
                ViewBag.trabajoshv2 = ObtenerTrabajosHV(codper2);
                ViewBag.ambpenhv2 = ObtenerAmbitosPenalesHV(codper2);
                ViewBag.otrapenhv2 = ObtenerOtrasPenalidadesHV(codper2);
            }
            return View();
        }

    }
}