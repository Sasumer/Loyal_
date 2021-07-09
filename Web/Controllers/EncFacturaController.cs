using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Util;

namespace Web.Controllers
{
    public class EncFacturaController : Controller
    {
        // GET: EncFactura
        public ActionResult Index()
        { //(proyecto_ Copiado del IndexAdmin de ProductoController)
            IEnumerable<ENC_FACTURA> lista = null;
            try
            {
                IServiceEncFactura _SeviceEncFactura = new ServiceEncFactura();
                //Listado de libros
                lista = _SeviceEncFactura.GetEncFactura();
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
    }
}