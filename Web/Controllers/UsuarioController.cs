using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Util;
using Infraestructure.Models;
using Web.Security;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        //AMANDA=======================================================================================
        // GET: Login
        public ActionResult IndexGestionUsuarios()
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            IEnumerable<USUARIO> lista = null;
            try
            {
                IServiceUsuario _SeviceUsuario = new ServiceUsuario();
                //Listado de libros
                lista = _SeviceUsuario.GetUSUARIO();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }
        //FIN AMANDA===================================================================================
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
        private MultiSelectList listaRoles(ICollection<USUARIO_ROL> usuRol)
        {
            //Lista de Categorias
            IServiceRol _ServiceRol = new ServiceRol();
            IEnumerable<ROL> listaRoles = _ServiceRol.GetROLs();
            string[] listaRolesSelect = null;

            if (usuRol != null)
            {

                listaRolesSelect = usuRol.Select(c => c.ID_ROL+"").ToArray();
            }

            return new MultiSelectList(listaRoles, "ID", "DESCRIPCION", listaRolesSelect);

        }
        private MultiSelectList listaTelefonos(ICollection<Telefono> telefono)
        {
            //Lista de Categorias
            IServiceTelefono _ServiceTelefono = new ServiceTelefono();
            IEnumerable<Telefono> listaTelefonos = _ServiceTelefono.GetTelefono();
            string[] listaTelefonoSelect = null;

            if (telefono != null)
            {

                listaTelefonoSelect = telefono.Select(c => c.ID + "").ToArray();
            }

            return new MultiSelectList(listaTelefonos, "ID", "Numero_telefono", listaTelefonoSelect);

        }
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            ViewBag.IdRol = listaRoles(null);
            return View();
        }

        // POST: USUARIO/Create
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            USUARIO usuario = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                usuario = _ServiceUsuario.GetUSUARIOByID(id.Value);
                if (usuario == null)
                {
                    TempData["Message"] = "No existe el usuario solicitado";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //sem 5: =============================
                ViewBag.IdUsuarioRol = listaRoles(usuario.USUARIO_ROL);
                ViewBag.IdTelefono = listaTelefonos(usuario.Telefono);
                //fin sem 5===========================
                return View(usuario);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
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
                        Log.Info($"Accede {oUsuario.Nombre} {oUsuario.Apellido1} "); //con el rol {oUsuario.USUARIO_ROL}");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"{usuario.ID} se intentó conectar  y falló");
                        ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", "Error al autenticarse", SweetAlertMessageType.warning);
                        return RedirectToAction("Loguearse", "Home");

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
                    //Log.Warn($"El usuario {oUsuario.Nombre} {oUsuario.Apellidos} con el rol {oUsuario.Rol.IdRol}-{oUsuario.Rol.Descripcion}, intentó acceder una página sin permisos  ");
                    //Log.Warn($"El usuario {oUsuario.Nombre} {oUsuario.Apellido1} con el rol {oUsuario.USUARIO_ROL}, intentó acceder una página sin permisos  ");
                    Log.Warn($"El usuario {oUsuario.Nombre} {oUsuario.Apellido1}, intentó acceder una página sin permisos  ");
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
                return RedirectToAction("Loguearse", "Home");
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

        [HttpPost]
        public ActionResult Save(USUARIO uSUARIO, String[] selectedRol)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {
                // TODO: Add update logic here



                if (ModelState.IsValid)
                {
                    USUARIO oUSUARIO = _ServiceUsuario.Save(uSUARIO, selectedRol);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.Util.ValidateErrors(this);
                    ViewBag.IdRol = listaRoles(uSUARIO.USUARIO_ROL);
                    return View("Create", uSUARIO);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "USUARIO";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }



    }
}
