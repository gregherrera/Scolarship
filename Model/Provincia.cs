using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Escolaridad.Data;
//using MySql.Data.EntityFrameworkCore.DataAnnotations;

namespace Escolaridad.Model
{
    //[MySqlCharset("utf8"), MySqlCollation("utf8_spanish_ci")]
    public class Provincia
    {
        [Key]
        public int Id { get; set; }
        
		[Required]
        public int Id_Pais { get; set; }
        
		[Required]
        [StringLength(50)]
        [Display(Name = "Nombre de la Provincia")]
        public string Descripcion { get; set; }
        
		[Required]
        public int Creado_Por { get; set; }
        
		[Required]
        [DataType(DataType.DateTime)]
        public DateTime Creado_En { get; set; }
        
		public int? Actualizado_Por { get; set; }
        
		public DateTime? Actualizado_En { get; set; }

        [ForeignKey("Id_Pais")]
        public Pais Pais { get; set; }
        
		[ForeignKey("Creado_Por")]
        public ApplicationUser UsuarioCrea { get; set; }
        
		[ForeignKey("Actualizado_Por")]        
		public ApplicationUser UsuarioModifica { get; set; }
        
		public ICollection<Municipio> Municipios { get; set; }
    }
}