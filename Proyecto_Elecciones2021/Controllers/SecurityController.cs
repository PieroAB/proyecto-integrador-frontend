using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Elecciones2021.Models;
using Proyecto_Elecciones2021.Utils;
using System.Data;
using System.Data.SqlClient;
using CaptchaMvc.HtmlHelpers;
using System.Net.Http;

namespace Proyecto_Elecciones2021.Controllers
{
    public class SecurityController : Controller
    {
            readonly string uri = new Url().hostUri();

            [HttpGet]
            public ActionResult Login()
            {
                var obj = new Logueo();
                return View(obj);
            }

            [HttpPost]
            public ActionResult Login(Logueo obj)
            {
                Persona reg = null;
                if (ModelState.IsValid == false) return View(obj);
                if (!this.IsCaptchaValid(""))
                {
                    ViewBag.ErrorMessage = "CAPTCHA INCORRECTO";
                    return View(obj);
                }
                else
                {
                    using (var servicio = new HttpClient())
                    {
                        servicio.BaseAddress = new Uri(uri);
                        var tarea = servicio.PostAsJsonAsync("api/Login/ingreso/", obj);
                        tarea.Wait();
                        var resultado = tarea.Result;
                        if (resultado.IsSuccessStatusCode)
                        {
                            var reg1 = resultado.Content.ReadAsAsync<Persona>();
                            reg1.Wait();
                            reg = reg1.Result;
                        }

                        if (reg == null)
                        {
                            ViewBag.Message = "INTENTELO NUEVAMENTE";
                            return View("Login", reg);
                        }
                        else if (reg.voto == 1)
                        {
                            ViewBag.Message = "USTED YA VOTÓ";
                            return View();
                        }
                        else
                        {
                            Session["login"] = reg;
                            return RedirectToAction("Sufragio", "Votacion");
                        }

                    }
                }
            }
     }
}