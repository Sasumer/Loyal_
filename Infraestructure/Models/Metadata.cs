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
        //[Required(ErrorMessage = "El Nombre es obligatorio")]
        //[MinLength(10, ErrorMessage = "El Nombre de usuario debe tener al menos 10 caracteres")]
        //public String Nombre { get; set; }

        //[Range(1, 18, ErrorMessage = "La edad debe estar entre 1 y 18")]
        //public int Edad { get; set; }

        //[EmailAddress(ErrorMessage = "Debe ingresar un mail válido")]
        //public String Email { get; set; }

        //[RegularExpression("[MmFf]", ErrorMessage = "Solo puede ingresar una M o F")]
        //public String Genero { get; set; }

        [Required(ErrorMessage = "El ID es obligatorio")]
        [Range(1, 700, ErrorMessage = "El ID debe estar entre 1 y 700")]
        public String ID { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio")]
        [Display(Name = "Precio de Venta")]
        public Nullable<decimal> PRECIO_VENTA { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [MinLength(3, ErrorMessage = "La descripcion debe tener al menos 3 caracteres")]
        [Display(Name = "Descripcion")]
        public string DESCRIPCION { get; set; }

        [Display(Name = "Imagen Producto")]
        public byte[] PHOTO { get; set; }
        //public string PHOTO { get; set; }

        [Required(ErrorMessage = "La cantidad minima es obligatoria")]
        [Range(1, 700, ErrorMessage = "La cantidad minima debe estar entre 1 y 700")]
        [Display(Name = "Cantidad Minima")]
        public Nullable<int> CANTIDAD_MINIMA { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado (Activo)")]

        [UIHint("_IsActive")]
        public Nullable<bool> LOG_ACTIVO { get; set; }

        public string ID_USUARIO_INGRESA { get; set; }
        public string FECHA_AGREGA { get; set; }
        public string ID_USUARIO_EDITA { get; set; }
        public string FECHA_EDITA { get; set; }

        [Display(Name = "Fecha de Vencimiento")]
        public string FECHA_VENCIMIENTO { get; set; }

        [Required(ErrorMessage = "El costo es obligatorio")]
        [Display(Name = "Costo")]
        public Nullable<decimal> COSTO { get; set; }

        [Required(ErrorMessage = "El Tipo de Producto es obligatorio")]
        [Display(Name = "Id Tipo Producto")]
        public Nullable<int> ID_TIPO_PRODUCTO { get; set; }

        [Display(Name = "Detalle Factura")]
        public virtual ICollection<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }

        [Display(Name = "Tipo Producto")]
        public virtual TIPO_PRODUCTO TIPO_PRODUCTO { get; set; }

        [Display(Name = "Ubicacion(es)")]
        public virtual ICollection<PRODUCTO_UBICACION> PRODUCTO_UBICACION { get; set; }



        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }

        //public virtual TIPO_PRODUCTO TIPO_PRODUCTO { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PRODUCTO_UBICACION> PRODUCTO_UBICACION { get; set; }
        //public virtual USUARIO USUARIO { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        [Required(ErrorMessage = "Proveedor(es) es obligatorio")]
        [Display(Name = "Proveedor(es)")]
        public virtual ICollection<PROVEEDOR> PROVEEDOR { get; set; }



    }

    internal partial class ProveedorMetadata
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage ="Identificación necesaria")]
        [Required(ErrorMessage = "El ID es obligatorio")]
        [Range(1, 700, ErrorMessage = "El ID debe estar entre 1 y 700")]
        [Display(Name = "Identificacion")]
        public string ID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(5, ErrorMessage = "El nombre debe tener al menos 5 caracteres")]
        [Display(Name = "Nombre de la Organizacion")]
        public string NOMBRE_ORGAN { get; set; }

        [Required(ErrorMessage = "La Direccion es obligatoria")]
        [MinLength(5, ErrorMessage = "La Direccion debe tener al menos 5 caracteres")]
        [Display(Name = "Direccion")]
        public string DIRECCION { get; set; }

        [Required(ErrorMessage = "El Pais es obligatorio")]
        [MinLength(5, ErrorMessage = "El Pais debe tener al menos 5 caracteres")]
        [Display(Name = "Pais")]
        public string PAIS { get; set; }

        public virtual ICollection<PRODUCTO> PRODUCTO { get; set; }

        public virtual ICollection<USUARIO> USUARIO { get; set; }

    }

    internal partial class EncFacturaMetadata
    {

        [Display(Name = "Numero De Factura")]
        public int ID { get; set; }
        [Display(Name = "Fecha")]
        public string FECHA { get; set; }
        [Display(Name = "Usuario")]
        public string ID_USUARIO { get; set; }
        [Display(Name = "Tipo de Factura")]
        public int ID_TIPO_FACTURA { get; set; }
        public virtual ICollection<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }
        [Display(Name = "Tipo de Factura")]
        public virtual TIPO_FACTURA TIPO_FACTURA { get; set; }
        public virtual USUARIO USUARIO { get; set; }



    }

    internal partial class UsuarioMetadata
    {
        //[Required(ErrorMessage = "El ID es obligatorio")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "* Solo se permiten números.")]
        [Display(Name = "Identificación")]
        public string ID { get; set; }

        //[EmailAddress(ErrorMessage = "Debe ingresar un mail válido")]
        [Display(Name = "Correo")]
        public string correo_electronico { get; set; }

        //[Required(ErrorMessage = "La Contraseña es obligatoria")]
        [Display(Name = "Contraseña")]
        public string contrasenna { get; set; }

        //[Required(ErrorMessage = "El nombre es obligatorio")]
        //[MinLength(5, ErrorMessage = "El nombre debe tener al menos 5 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }

        //[Required(ErrorMessage = "El Estado es obligatorio")]
        [Display(Name = "Estado")]
 
        [UIHint("IsActive")]
        public Nullable<bool> Activo { get; set; }
        public virtual ICollection<ENC_FACTURA> ENC_FACTURA { get; set; }

        public virtual ICollection<PRODUCTO> PRODUCTO { get; set; }

        public virtual ICollection<PRODUCTO> PRODUCTO1 { get; set; }



       
        public virtual ICollection<PROVEEDOR> PROVEEDOR { get; set; }

    }




}