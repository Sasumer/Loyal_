using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Util;

namespace Web.ViewModel
{
    public class Carrito
    {
        public List<ViewModelOrdenDetalle> Items { get; private set; }

        //Implementación Singleton

        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
        public static readonly Carrito Instancia;

        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
        static Carrito()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Items = new List<ViewModelOrdenDetalle>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }

        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected Carrito() { }

        /**
         * AgregarItem (): agrega un artículo a la compra
         */
        public String AgregarItem(string libroId)
        {
            String mensaje = "";
            // Crear un nuevo artículo para agregar al carrito
            ViewModelOrdenDetalle nuevoItem = new ViewModelOrdenDetalle(libroId);
            // Si este artículo ya existe en lista de libros, aumente la Cantidad
            // De lo contrario, agregue el nuevo elemento a la lista
            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.IdProducto == libroId))
                {
                    ViewModelOrdenDetalle item = Items.Find(x => x.IdProducto == libroId);
                    item.Cantidad++;
                }
                else
                {
                    nuevoItem.Cantidad = 1;
                    Items.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Orden Producto", "Producto agregado a la orden", SweetAlertMessageType.success);

            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Orden Producto", "El Producto solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }


        /**
         * SetItemCantidad(): cambia la Cantidad de un artículo en el carrito
         */
        public String SetItemCantidad(string libroId, int Cantidad)
        {
            String mensaje = "";
            // Si estamos configurando la Cantidad a 0, elimine el artículo por completo
            if (Cantidad == 0)
            {
                EliminarItem(libroId);
                mensaje = SweetAlertHelper.Mensaje("Orden Producto", "Producto eliminado", SweetAlertMessageType.success);

            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad
                ViewModelOrdenDetalle actualizarItem = new ViewModelOrdenDetalle(libroId);
                if (Items.Exists(x => x.IdProducto == libroId))
                {
                    ViewModelOrdenDetalle item = Items.Find(x => x.IdProducto == libroId);
                    item.Cantidad = Cantidad;
                    mensaje = SweetAlertHelper.Mensaje("Orden Producto", "Cantidad actualizada", SweetAlertMessageType.success);

                }
            }
            return mensaje;

        }

        /**
         * EliminarItem (): elimina un artículo del carrito de compras
         */
        public String EliminarItem(string libroId)
        {
            String mensaje = "El Producto no existe";
            if (Items.Exists(x => x.IdProducto == libroId))
            {
                var itemEliminar = Items.Single(x => x.IdProducto == libroId);
                Items.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Orden Producto", "Producto eliminado", SweetAlertMessageType.success);
            }
            return mensaje;

        }

        /**
         * GetTotal() - Devuelve el precio total de todos los libros.
         */
        public decimal? GetTotal()
        {
            decimal? total = 0;
            total = Items.Sum(x => x.SubTotal);

            return total;
        }
        public int GetCountItems()
        {
            int total = 0;
            total = Items.Sum(x => x.Cantidad);

            return total;
        }
        /**
         * GetTotalPeso() - Devuelve el total de peso de todos los libros.
         */

        public void eliminarCarrito()
        {
            Items.Clear();

        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Web.Util;

//namespace Web.ViewModel
//{
//    public class Carrito
//    {
//        public List<ViewModelOrdenDetalle> Items { get; private set; }

//        //Implementación Singleton

//        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
//        public static readonly Carrito Instancia;

//        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
//        static Carrito()
//        {
//            // Si el carrito no está en la sesión, cree uno y guarde los items.
//            if (HttpContext.Current.Session["carrito"] == null)
//            {
//                Instancia = new Carrito();
//                Instancia.Items = new List<ViewModelOrdenDetalle>();
//                HttpContext.Current.Session["carrito"] = Instancia;
//            }
//            else
//            {
//                // De lo contrario, obténgalo de la sesión.
//                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
//            }
//        }

//        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
//        protected Carrito() { }

//        /**
//         * AgregarItem (): agrega un artículo a la compra
//         */
//        public String AgregarItem(String productoid)
//        {
//            String mensaje = "";
//            // Crear un nuevo artículo para agregar al carrito
//            ViewModelOrdenDetalle nuevoItem = new ViewModelOrdenDetalle(productoid);
//            // Si este artículo ya existe en lista de libros, aumente la Cantidad
//            // De lo contrario, agregue el nuevo elemento a la lista
//            if (nuevoItem != null)
//            {
//                if (Items.Exists(x => x.IdProducto == productoid)) //LinQ
//                {
//                    ViewModelOrdenDetalle item = Items.Find(x => x.IdProducto == productoid);
//                    item.Cantidad++;
//                }
//                else
//                {
//                    nuevoItem.Cantidad = 1;
//                    Items.Add(nuevoItem);
//                }
//                mensaje = SweetAlertHelper.Mensaje("Orden Producto", "Producto agregado a la orden", SweetAlertMessageType.success);

//            }
//            else
//            {
//                mensaje = SweetAlertHelper.Mensaje("Orden Producto", "El Producto solicitado no existe", SweetAlertMessageType.warning);
//            }
//            return mensaje;
//        }


//        /**
//         * SetItemCantidad(): cambia la Cantidad de un artículo en el carrito
//         */
//        public String SetItemCantidad(String productoid, int Cantidad)
//        {
//            String mensaje = "";
//            // Si estamos configurando la Cantidad a 0, elimine el artículo por completo
//            if (Cantidad == 0)
//            {
//                EliminarItem(productoid);
//                mensaje = SweetAlertHelper.Mensaje("Orden Producto", "Producto eliminado", SweetAlertMessageType.success);

//            }
//            else
//            {
//                // Encuentra el artículo y actualiza la Cantidad
//                ViewModelOrdenDetalle actualizarItem = new ViewModelOrdenDetalle(productoid);
//                if (Items.Exists(x => x.IdProducto == productoid))
//                {
//                    ViewModelOrdenDetalle item = Items.Find(x => x.IdProducto == productoid);
//                    item.Cantidad = Cantidad;
//                    mensaje = SweetAlertHelper.Mensaje("Orden Producto", "Cantidad actualizada", SweetAlertMessageType.success);

//                }
//            }
//            return mensaje;

//        }

//        /**
//         * EliminarItem (): elimina un artículo del carrito de compras
//         */
//        public String EliminarItem(String productoid)
//        {
//            String mensaje = "El Producto no existe";
//            if (Items.Exists(x => x.IdProducto == productoid))
//            {
//                var itemEliminar = Items.Single(x => x.IdProducto == productoid);
//                Items.Remove(itemEliminar);
//                mensaje = SweetAlertHelper.Mensaje("Orden Producto", "Producto eliminado", SweetAlertMessageType.success);
//            }
//            return mensaje;

//        }

//        /**
//         * GetTotal() - Devuelve el precio total de todos los libros.
//         */
//        public decimal? GetTotal()
//        {
//            decimal? total = 0;
//            total = Items.Sum(x => x.SubTotal);

//            return total;
//        }
//        public int GetCountItems()
//        {
//            int total = 0;
//            total = Items.Sum(x => x.Cantidad);

//            return total;
//        }
//        /**
//         * GetTotalPeso() - Devuelve el total de peso de todos los libros.
//         */
//        //cuando se haga click en Guardar se debe de eliminarCarrito
//        public void eliminarCarrito()
//        {
//            Items.Clear();

//        }
//    }
//}