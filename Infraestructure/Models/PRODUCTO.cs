//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductoMetadata))]
    public partial class PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTO()
        {
            this.DETALLE_FACTURA = new HashSet<DETALLE_FACTURA>();
            this.PRODUCTO_UBICACION = new HashSet<PRODUCTO_UBICACION>();
            this.PROVEEDOR = new HashSet<PROVEEDOR>();
        }
    
        public string ID { get; set; }
        public Nullable<decimal> PRECIO_VENTA { get; set; }
        public string DESCRIPCION { get; set; }
        public byte[] PHOTO { get; set; }
        public Nullable<int> CANTIDAD_MINIMA { get; set; }
        public Nullable<bool> LOG_ACTIVO { get; set; }
        public string ID_USUARIO_INGRESA { get; set; }
        public string FECHA_AGREGA { get; set; }
        public string ID_USUARIO_EDITA { get; set; }
        public string FECHA_EDITA { get; set; }
        public string FECHA_VENCIMIENTO { get; set; }
        public Nullable<decimal> COSTO { get; set; }
        public Nullable<int> ID_TIPO_PRODUCTO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }
        public virtual TIPO_PRODUCTO TIPO_PRODUCTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTO_UBICACION> PRODUCTO_UBICACION { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROVEEDOR> PROVEEDOR { get; set; }
    }
}
