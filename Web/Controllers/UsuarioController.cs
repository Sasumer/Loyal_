using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Util;
using Infraestructure.Models;
namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {

            //elemento que contiene la lista
            IEnumerable<USUARIO> lista = null;
            try
            {
                IServiceUsuario _ServiceUSUARIO = new ServiceUsuario();
                lista = _ServiceUSUARIO.GetUSUARIO();
            }
            //catch (Exception ex)
            //{
            //    //Salvar el error en un archivo
            //    Log.Error(ex, MethodBase.GetCurrentMethod());
            //}
            //return View(lista);
            //(usando cod de sem 4)
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            ViewBag.titulo = "Mantenimiento";
            return View(lista);

        }


        // GET: USUARIO/Details/5
        public ActionResult Details(int? id)
        {
            ServiceUsuario _ServiceUSUARIO = new ServiceUsuario();
            USUARIO USUARIO = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                USUARIO = _ServiceUSUARIO.GetUSUARIOByID(id.Value);
                if (USUARIO == null)
                {
                    TempData["Message"] = "No existe el USUARIO solicitado";
                    TempData["Redirect"] = "USUARIO";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(USUARIO);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "USUARIO";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: USUARIO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USUARIO/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: USUARIO/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: USUARIO/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: USUARIO/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: USUARIO/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login(USUARIO usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            USUARIO oUsuario = null;
            try
            {
                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetLoginUsuario(usuario.ID, usuario.contrasenna);

                    if (oUsuario != null)
                    {
                        Session["User"] = oUsuario;
                        Log.Info($"Accede {oUsuario.Nombre} {oUsuario.Apellido1} con el rol {oUsuario.USUARIO_ROL}");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"{usuario.ID} se intentó conectar  y falló");
                        ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", "Error al autenticarse", SweetAlertMessageType.warning);

                    }
                }

                return View("Home", "Loguearse");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult UnAuthorized()
        {
            try
            {
                ViewBag.Message = "No autorizado";

                if (Session["User"] != null)
                {
                    USUARIO oUsuario = Session["User"] as USUARIO;
                    Log.Warn($"El usuario {oUsuario.Nombre} {oUsuario.Apellido1} con el rol {oUsuario.USUARIO_ROL}, intentó acceder una página sin permisos  ");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Home";
                TempData["Redirect-Action"] = "Loguearse";
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Logout()
        {
            try
            {
                Log.Info("Usuario desconectado!");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Home";
                TempData["Redirect-Action"] = "Loguearse";
                return RedirectToAction("Default", "Error");
            }
        }


    }
}
