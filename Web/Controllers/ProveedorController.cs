using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Util;



namespace Web.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {

            //elemento que contiene la lista
            IEnumerable<PROVEEDOR> lista = null;
            try
            {
                IServiceProveedor _ServiceProveedor = new ServiceProveedor();
                lista = _ServiceProveedor.GetPROVEEDOR();
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


        // GET: Proveedor/Details/5
        public ActionResult Details(int? id)
        {
            ServiceProveedor _ServicePROVEEDOR = new ServiceProveedor();
            PROVEEDOR PROVEEDOR = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                PROVEEDOR = _ServicePROVEEDOR.GetPROVEEDORByID(id.Value);
                if (PROVEEDOR == null)
                {
                    TempData["Message"] = "No existe el PROVEEDOR solicitado";
                    TempData["Redirect"] = "PROVEEDOR";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(PROVEEDOR);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PROVEEDOR";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private MultiSelectList listaUsuarios(ICollection<USUARIO> usuario)
        {
            //Lista de Categorias
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<USUARIO> listaUsuarios = _ServiceUsuario.GetUSUARIO();
            string[] listaUsuarioSelect = null;

            if (usuario != null)
            {

                listaUsuarioSelect = usuario.Select(c => c.ID).ToArray();
            }

            return new MultiSelectList(listaUsuarios, "ID", "Nombre", listaUsuarioSelect);

        }


        // GET: Proveedor/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {

            ViewBag.IdUsuarios = listaUsuarios(null);
            return View();
        }


        // GET: Proveedor/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            ServiceProveedor _ServicePROVEEDOR = new ServiceProveedor();
            PROVEEDOR PROVEEDOR = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                PROVEEDOR = _ServicePROVEEDOR.GetPROVEEDORByID(id.Value);
                if (PROVEEDOR == null)
                {
                    TempData["Message"] = "No existe el PROVEEDOR solicitado";
                    TempData["Redirect"] = "PROVEEDOR";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                ViewBag.IdUsuarios = listaUsuarios(PROVEEDOR.USUARIO);
                return View(PROVEEDOR);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PROVEEDOR";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }







    // POST: Proveedor/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(PROVEEDOR pROVEEDOR, String[] selectedUsuarios)
        {
            IServiceProveedor _ServicePROVEEDOR = new ServiceProveedor();

            try
            {
                // TODO: Add update logic here



                if (ModelState.IsValid)
                {
                    PROVEEDOR oPROVEEDORi = _ServicePROVEEDOR.Save(pROVEEDOR, selectedUsuarios);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.Util.ValidateErrors(this);
                    ViewBag.IdUsuario = listaUsuarios(pROVEEDOR.USUARIO);
                    return View("Create", pROVEEDOR);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PROVEEDOR";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proveedor/Delete/5
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


        public ActionResult buscarLibroxNombre(string filtro)
        {
            IEnumerable<PROVEEDOR> lista = null;
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();

            // Error porque viene en blanco 
            if (string.IsNullOrEmpty(filtro))
            {
                lista = _ServiceProveedor.GetPROVEEDOR();
            }
            else
            {
                lista = _ServiceProveedor.GetNombrePROVEEDOR(filtro);
            }


            // Retorna un Partial View
            return PartialView("_PartialViewProveedorAdmin", lista);
        }

    }
}
