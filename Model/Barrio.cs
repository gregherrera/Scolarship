using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Escolaridad.Data;
//using MySql.Data.EntityFrameworkCore.DataAnnotations;

namespace Escolaridad.Model
{
    //[MySqlCharset("utf8"), MySqlCollation("utf8_spanish_ci")]
    public class Barrio
    {
        [Key]
        public int Id { get; set; }
        
		[Required]
        public int Id_Sector { get; set; }
        
		[Required]
        [StringLength(50)]
        public string Descripcion { get; set; }
        
		[Required]
        public int Creado_Por { get; set; }
        
		[Required]
        [DataType(DataType.DateTime)]
        public DateTime Creado_En { get; set; }
        
		public int? Actualizado_Por { get; set; }
        
		[DataType(DataType.DateTime)]
        public DateTime? Actualizado_En { get; set; }

        [ForeignKey("Id_Sector")]
        public Sector Sector { get; set; }

        [ForeignKey("Creado_Por")]
        public ApplicationUser UsuarioCrea { get; set; }

        [ForeignKey("Actualizado_Por")]
        public ApplicationUser UsuarioModifica { get; set; }
    }
}