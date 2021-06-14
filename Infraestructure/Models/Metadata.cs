using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class ProductoMetadata
    {
        public String ID { get; set; }
        [Display(Name = "Precio Venta")]
        public Nullable<decimal> PRECIO_VENTA { get; set; }
        [Display(Name = "Descripcion")]
        public string DESCRIPCION { get; set; }
        [Display(Name = "Imagen Producto")]
        public byte[] PHOTO { get; set; }
        //public string PHOTO { get; set; }
        [Display(Name = "Cantidad Minima")]
        public Nullable<int> CANTIDAD_MINIMA { get; set; }

        [Display(Name = "Estado (Activo)")]
        public Nullable<bool> LOG_ACTIVO { get; set; }
        public string ID_USUARIO_INGRESA { get; set; }
        public string FECHA_AGREGA { get; set; }
        public string ID_USUARIO_EDITA { get; set; }
        public string FECHA_EDITA { get; set; }

        [Display(Name = "Fecha de Vencimiento")]
        public string FECHA_VENCIMIENTO { get; set; }

        [Display(Name = "Costo")]
        public Nullable<decimal> COSTO { get; set; }

        [Display(Name = "Id Tipo Producto")]
        public Nullable<int> ID_TIPO_PRODUCTO { get; set; }

        [Display(Name = "Detalle Factura")]
        public virtual ICollection<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }

        [Display(Name = "Tipo Producto")]
        public virtual TIPO_PRODUCTO TIPO_PRODUCTO { get; set; }

        [Display(Name = "Ubicacion(es)")]
        public virtual ICollection<PRODUCTO_UBICACION> PRODUCTO_UBICACION { get; set; }












        //
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }

        //public virtual TIPO_PRODUCTO TIPO_PRODUCTO { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PRODUCTO_UBICACION> PRODUCTO_UBICACION { get; set; }
        //public virtual USUARIO USUARIO { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        [Display(Name = "Proveedor(es)")]
        public virtual ICollection<PROVEEDOR> PROVEEDOR { get; set; }
    }

    internal partial class ProveedorMetadata
    {

        [Display(Name = "Nombre de la Organizacion")]
        public string NOMBRE_ORGAN { get; set; }

        [Display(Name = "Direccion")]
        public string DIRECCION { get; set; }

        [Display(Name = "Pais")]
        public string PAIS { get; set; }

        public virtual ICollection<PRODUCTO> PRODUCTO { get; set; }

        public virtual ICollection<USUARIO> USUARIO { get; set; }

    }
}