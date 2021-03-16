//using MySql.Data.EntityFrameworkCore.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Escolaridad.Data;

namespace Escolaridad.Model
{
    //[MySqlCharset("utf8"), MySqlCollation("utf8_spanish_ci")]
    public class AccionesTabla
    {
        [Key]
        public int Id { get; set; }
        
		[Required, MaxLength(50)]
        public string Tabla { get; set; }
        
		[Required, MaxLength(1)]
        public string Accion { get; set; }
        
		[Required]
        public int Creado_Por { get; set; }
        
		[Required]
        [DataType(DataType.DateTime)]
        public DateTime Creado_En { get; set; }

        public int? Actualizado_Por { get; set; }
        
		[DataType(DataType.DateTime)]
        public DateTime? Actualizado_En { get; set; }

        [ForeignKey("Creado_Por")]
        public ApplicationUser UsuarioCrea { get; set; }
        
		[ForeignKey("Actualizado_Por")]
        public ApplicationUser UsuarioModifica { get; set; }
    }
}