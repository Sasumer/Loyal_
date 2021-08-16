using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelOrdenDetalle
    {

        public string IdProducto { get; set; }
        //public byte[] Imagen { get; set; }
        public int Cantidad { get; set; }

        public string ubicacion { get; set; }



        public decimal? Precio
        {
            get { return Producto.PRECIO_VENTA; }

        }
        public virtual PRODUCTO Producto { get; set; }

        public decimal? SubTotal
        {
            get
            {
                return calculoSubtotal();
            }
        }
        private decimal? calculoSubtotal()
        {
            return this.Precio * this.Cantidad;
        }


        //Constructor que crea cada línea
        public ViewModelOrdenDetalle(string IdProducto)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            this.IdProducto = IdProducto;
            this.Producto = _ServiceProducto.GetProductoByID(IdProducto);
        }
    }
}