using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Web.Util;
using System.IO;
using Web.Security;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        //public ActionResult ActionMethordNam()
        //{
        //    List<Systems> systems;
        //    var query = db.SystemFamily.Select(c => c.SystemFamilyID).Tolist();
        //    foreach (var sid in query)
        //    {
        //        systems = db.Systems.Select(c => c.SystemFamilyID == sid).Tolist();
        //    }
        //    int count = systems.Count();//Here you will  get count

        //    Viewbag.Counts = count;
        //    return View();
        //}
        // GET: Producto
        public ActionResult Index()
        {
            
            int countProducto = 0;
            int countSalidas = 0;
            int countEntradas = 0;
            //2 FORMA CON EL FOREACH: int conteo = 0;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                var query = ctx.PRODUCTO.Select(c => c.ID).ToList();
                //foreach (var sid in query)
                //{
                //    conteo++;
                //}
                //int count = listaPro.Count();
                countProducto = query.Count();

                
                var salidas = ctx.ENC_FACTURA.Where(e => e.ID_TIPO_FACTURA == 1)
                    .Select(c => c.ID)
                    .ToList();
                countSalidas = salidas.Count();

                var entradas = ctx.ENC_FACTURA.Where(e => e.ID_TIPO_FACTURA == 2)
                    .Select(c => c.ID)
                    .ToList();
                countEntradas = entradas.Count();
            }
            ViewBag.CountProducto = countProducto;
            //ViewBag.Conteo = conteo;
            ViewBag.CountSalidas = countSalidas;
            ViewBag.CountEntradas = countEntradas;


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
        //SEM 5 ====================================================================================
        //autor = 1:1       Proyecto: tipoClasificacion
        //categorias = 1:N  Proyecto: Proveedores, ubicaciones
        private SelectList listaTipoProducto(Nullable<int> idTip = 0) //public Nullable<int> ID_TIPO_PRODUCTO { get; set; }
        {
            //Lista de autores
            IServiceTipoProducto _ServiceTipo = new ServiceTipoProducto();
            IEnumerable<TIPO_PRODUCTO> listaTipo = _ServiceTipo.GetTipoProducto();
            //Otra logica, pero al final la profe no la implementa, FALTAA***: Autor SelectAutor = listaAutores.Where(c => c.IdAutor == idAutor).FirstOrDefault();
            return new SelectList(listaTipo, "ID", "DESCRIPCION", idTip);
        }
        //MultiSelectList: Se puede seleccionar más de una categoria.
        private MultiSelectList listaProveedores(ICollection<PROVEEDOR> proveedores)
        {
            //Lista de Categorias
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            IEnumerable<PROVEEDOR> listaProveedores = _ServiceProveedor.GetPROVEEDOR();
            String[] listaProveedoresSelect = null;

            if (proveedores != null)
            {
                listaProveedoresSelect = proveedores.Select(c => c.ID).ToArray();
            }

            return new MultiSelectList(listaProveedores, "ID", "NOMBRE_ORGAN", listaProveedoresSelect);

        }

        private MultiSelectList listaUbicaciones(ICollection<UBICACION> ubicaciones)
        {
            IServiceUbicacion _ServiceUbicacion = new ServiceUbicacion();
            IEnumerable<UBICACION> listaUbicaciones = _ServiceUbicacion.GetUbicacion();
            String[] listaUbicacionesSelect = null;

            if (ubicaciones != null)
            {
                listaUbicacionesSelect = ubicaciones.Select(c => c.ID).ToArray();
            }

            return new MultiSelectList(listaUbicaciones, "ID", "DESCRIPCION", listaUbicacionesSelect);

        }
        private MultiSelectList listaProductoUbicaciones(ICollection<PRODUCTO_UBICACION> proUbicaciones)
        {
            IServiceUbicacion _ServiceUbicacion = new ServiceUbicacion();
            IEnumerable<UBICACION> listaUbicaciones = _ServiceUbicacion.GetUbicacion();
            String[] listaUbicacionesSelect = null;

            if (proUbicaciones != null)
            {
                listaUbicacionesSelect = proUbicaciones.Select(c => c.ID_UBICACION).ToArray();
            }

            return new MultiSelectList(listaUbicaciones, "ID", "DESCRIPCION", listaUbicacionesSelect);

        }
        // GET: Libro/Create
        // Vamos a tener solo un método que haga ambas acciones. (Cargar los datos en el form y guardarlos en la BD).
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            ViewBag.IdTipoProducto = listaTipoProducto();
            ViewBag.IdProveedores = listaProveedores(null);
            ViewBag.IdUbicaciones = listaUbicaciones(null);
            return View();
        }

        // GET: Libro/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(String id)
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
                //sem 5: =============================
                ViewBag.IdTipoProducto = listaTipoProducto(producto.ID_TIPO_PRODUCTO);
                ViewBag.IdProveedores = listaProveedores(producto.PROVEEDOR);
                //listaProductoUbicaciones
                ViewBag.IdUbicaciones = listaProductoUbicaciones(producto.PRODUCTO_UBICACION);
                //fin sem 5===========================
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
        //sem 5: ==========================================================
        // POST: Libro/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(PRODUCTO producto, HttpPostedFileBase ImageFile, string[] selectedProveedores, string[] selectedUbicaciones)
        //AQUI SE ENVIAN LAS CATEGORIAS AL DropDownList EN Create.cshtml (string[] selectedCategorias).
        {
            MemoryStream target = new MemoryStream();
            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                // Cuando es Insert Image viene en null porque se pasa diferente
                if (producto.PHOTO == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        producto.PHOTO = target.ToArray();
                        ModelState.Remove("PHOTO");
                    }

                }
                if (ModelState.IsValid)
                {
                    PRODUCTO oLProductoI = _ServiceProducto.Save(producto, selectedProveedores, selectedUbicaciones); //la logica la hacemos en el Repositorio
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.Util.ValidateErrors(this);
                    ViewBag.IdTipoProducto = listaTipoProducto(producto.ID_TIPO_PRODUCTO);
                    ViewBag.IdProveedores = listaProveedores(producto.PROVEEDOR);
                    //listaProductoUbicaciones
                    ViewBag.IdUbicaciones = listaProductoUbicaciones(producto.PRODUCTO_UBICACION);
                    return View("Create", producto);
                }

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        //Fin sem 5============================================================================
        // GET: Libro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Libro/Delete/5
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

        //ENTREGA FINAAAAAAAAAAAAAAALLL   =======================================================================================
        public ActionResult EditarXOrden(string id) //Es llamado del Index (de Producto)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            PRODUCTO producto = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                producto = _ServiceProducto.GetProductoByID(id);
                if (producto == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //sem 5: =============================
                //ViewBag.IdTipoProducto = listaTipoProducto(producto.ID_TIPO_PRODUCTO);
                ViewBag.IdProveedores = listaProveedores(producto.PROVEEDOR);
                //listaProductoUbicaciones
                ViewBag.IdUbicaciones = listaProductoUbicaciones(producto.PRODUCTO_UBICACION);
                //fin sem 5===========================
                return View(producto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult SaveXOrden(PRODUCTO producto, string[] selectedProveedores, string[] selectedUbicaciones)
        //AQUI SE ENVIAN LAS CATEGORIAS AL DropDownList EN Create.cshtml (string[] selectedCategorias).
        {
            MemoryStream target = new MemoryStream();
            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                //// Cuando es Insert Image viene en null porque se pasa diferente
                //if (producto.PHOTO == null)
                //{
                //    if (ImageFile != null)
                //    {
                //        ImageFile.InputStream.CopyTo(target);
                //        producto.PHOTO = target.ToArray();
                //        ModelState.Remove("PHOTO");
                //    }

                //}
                //if (ModelState.IsValid)
                //{
                    PRODUCTO oLProductoI = _ServiceProducto.SaveXOrden(producto, selectedProveedores, selectedUbicaciones); //la logica la hacemos en el Repositorio
                //}
                //else
                //{
                //    // Valida Errores si Javascript está deshabilitado
                //    Util.Util.ValidateErrors(this);
                //    ViewBag.IdTipoProducto = listaTipoProducto(producto.ID_TIPO_PRODUCTO);
                //    ViewBag.IdProveedores = listaProveedores(producto.PROVEEDOR);
                //    //listaProductoUbicaciones
                //    ViewBag.IdUbicaciones = listaProductoUbicaciones(producto.PRODUCTO_UBICACION);
                //    return View("Create", producto);
                //}
                
                int cantidadLibros = Carrito.Instancia.Items.Count();
                ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem(producto.ID);

                return PartialView("_OrdenCantidad");
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        //Ordenar un libro y agregarlo al carrito
        public ActionResult ordenarLibro(string idLibro)
        {
            int cantidadLibros = Carrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem(idLibro);

            return PartialView("_OrdenCantidad");
            //return RedirectToAction("Edit");

        }

    }
}