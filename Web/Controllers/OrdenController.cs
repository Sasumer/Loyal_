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
using Web.ViewModel;

namespace Web.Controllers
{
    public class OrdenController : Controller
    {
        // GET: Orden
        public ActionResult Index()
        {
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }
            ViewBag.idCliente = listaClientes();
            ViewBag.idTipoFactura = listaTipoFactura();

            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            return View();
        }
        private SelectList listaClientes()
        {
            //Lista de Clientes
            IServiceUsuario _ServiceCliente = new ServiceUsuario();
            IEnumerable<USUARIO> listaClientes = _ServiceCliente.GetUSUARIO();

            return new SelectList(listaClientes, "ID", "Nombre");
        }

        private SelectList listaTipoFactura()
        {
            //Lista de Clientes
            IServiceTipoFactura _ServiceTipo = new ServiceTipoFactura();
            IEnumerable<TIPO_FACTURA> listatipos = _ServiceTipo.GetTipoFactura();

            return new SelectList(listatipos, "ID", "DESCRIPCION");
        }
        //Actualizar Vista parcial detalle carrito
        private ActionResult DetalleCarrito()
        {

            return PartialView("_DetalleOrden", Carrito.Instancia.Items);
        }
        //Actualizar cantidad de libros en el carrito
        public ActionResult actualizarCantidad(string idLibro, int cantidad)
        {
            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idLibro, cantidad);
            TempData.Keep();
            return PartialView("_DetalleOrden", Carrito.Instancia.Items);

        }
        //Ordenar un libro y agregarlo al carrito
        public ActionResult ordenarLibro(string idLibro)
        {
            int cantidadLibros = Carrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem(idLibro);
            return PartialView("_OrdenCantidad");

        }

        //Actualizar solo la cantidad de libros que se muestra en el menú
        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadLibros = Carrito.Instancia.Items.Count();
            return PartialView("_OrdenCantidad");

        }
        public ActionResult eliminarLibro(string idLibro)
        {
            ViewBag.NotificationMessage = Carrito.Instancia.EliminarItem(idLibro);
            return PartialView("_DetalleOrden", Carrito.Instancia.Items);
        }

        public ActionResult IndexAdmin()
        {
            IEnumerable<ENC_FACTURA> lista = null;

            try
            {
                IServiceOrden _ServiceOrden = new ServiceOrden();
                lista = _ServiceOrden.GetOrden();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }



        // GET: Orden/Details/5
        public ActionResult Details(int? id)
        {
            ServiceOrden _ServiceOrden = new ServiceOrden();
            ENC_FACTURA orden = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                orden = _ServiceOrden.GetOrdenByID(id.Value);
                if (orden == null)
                {
                    TempData["Message"] = "No existe la orden solicitado";
                    TempData["Redirect"] = "Orden";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View(orden);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Empleado)]
        public ActionResult Save(ENC_FACTURA orden)
        {

            try
            {

                // Si no existe la sesión no hay nada
                if (Carrito.Instancia.Items.Count() <= 0)
                {
                    // Validaciones de datos requeridos.
                    TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Orden", "Seleccione los productos a ordenar", SweetAlertMessageType.warning);
                    return RedirectToAction("Index");
                }
                else
                {

                    var listaDetalle = Carrito.Instancia.Items;

                    foreach (var item in listaDetalle)
                    {
                        DETALLE_FACTURA ordenDetalle = new DETALLE_FACTURA();
                        ordenDetalle.ID_PRODUCTO = item.IdProducto;
                        ordenDetalle.PRECIO = item.Precio;
                        ordenDetalle.CANTIDAD = item.Cantidad;
                        ordenDetalle.ID_UBICACION = "1";
                        orden.DETALLE_FACTURA.Add(ordenDetalle);
                    }
                }

                IServiceOrden _ServiceOrden = new ServiceOrden();
                ENC_FACTURA ordenSave = _ServiceOrden.Save(orden);

                // Limpia el Carrito de compras
                Carrito.Instancia.eliminarCarrito();
                TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Orden", "Orden guardada satisfactoriamente!", SweetAlertMessageType.success);
                // Reporte orden
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error  
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult graficoOrden()
        {
            //Documentación chartjs https://www.chartjs.org/docs/latest/
            IServiceOrden _ServiceOrden = new ServiceOrden();
            ViewModelGrafico grafico = new ViewModelGrafico();
            string etiquetas = "";
            string valores = "";
            _ServiceOrden.GetOrdenCountDate(out etiquetas, out valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Ordenes por fecha";
            grafico.tituloEtiquetas = "Cantidad de Ordenes";

            grafico.tipo = "polarArea";
            ViewBag.grafico = grafico;
            return View();
        }


    }
}
