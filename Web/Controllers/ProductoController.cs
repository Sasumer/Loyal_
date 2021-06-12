using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Web.Util;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            //elemento que contiene la lista
            IEnumerable<PRODUCTO> lista = null;
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto();
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
        public ActionResult IndexAdmin()
        {
            IEnumerable<PRODUCTO> lista = null;
            try
            {
                IServiceProducto _SeviceProducto = new ServiceProducto();
                //Listado de libros
                lista = _SeviceProducto.GetProducto();
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
        //(prof, video) public ViewResult: solo puede retornar vistas
        //con actionResult puede retornar más. Hasta un json.
        //Una VISTA: es una forma para que el usuario pueda ver los resultados/una respuesta
        public ActionResult Details(String id)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            PRODUCTO producto = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                producto = _ServiceProducto.GetProductoByID(id);
                if (producto == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(producto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}