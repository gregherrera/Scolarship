//using MySql.Data.EntityFrameworkCore.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Escolaridad.Data;

namespace Escolaridad.Model
{
    //[MySqlCharset("utf8"), MySqlCollation("utf8_spanish_ci")]
    public class Recinto
    {
        [Required]
        public int Id { get; set; }
        
		[Required]
        [StringLength(100)]
        [Display(Name = "Nombre del Recinto")]
        public string Descripcion { get; set; }
        
		[Required]
        [StringLength(100)]
        [Display(Name = "Calle")]
        public string Direccion { get; set; }
        
		[Required]
        [StringLength(25)]
        [RegularExpression(@"^\(\d{3}\)\s{0,1}\d{3}-\d{4}$", ErrorMessage = "Dígite un número de teléfono válido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        
		[Required]
        [StringLength(100)]
        [Display(Name = "Correo Electrónico")]
        public string Correo_Electronico { get; set; }
        
		[Display(Name = "R.N.C.")]
        public string RNC { get; set; }
        
		[Required]
        [StringLength(100)]
        public string Lema { get; set; }

		//[Display(Name = "Logo"), Column(TypeName = "MediumBlob")]
		[Display(Name = "Logo")]
		public byte[] Logo { get; set; }

        [Required]
        public int Id_Barrio { get; set; }
        
		[Required]
        public int Creado_Por { get; set; }
        
		[Required]
        [DataType(DataType.DateTime)]
        public DateTime Creado_En { get; set; }
        
		public int? Actualizado_Por { get; set; }
        
		[DataType(DataType.DateTime)]
        public DateTime? Actualizado_En { get; set; }

        [ForeignKey("Id_Barrio")]
        public Barrio Barrio { get; set; }
        
		[ForeignKey("Creado_Por")]
        public ApplicationUser UsuarioCrea { get; set; }
        
		[ForeignKey("Actualizado_Por")]
        public ApplicationUser UsuarioModifica { get; set; }
    }
}