using System;
using Escolaridad.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
//using MySql.Data.EntityFrameworkCore.DataAnnotations;
using System.Collections.Generic;

namespace Escolaridad.Model
{
    //[MySqlCharset("utf8"), MySqlCollation("utf8_spanish_ci")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_Recinto { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public int Nivel { get; set; }
        
		[Required]
        public int Orden { get; set; }
        
        public int? Id_Padre { get; set; }
        
		[Required]
        public int Creado_Por { get; set; }
        
		[Required]
        [DataType(DataType.DateTime)]
        public DateTime Creado_En { get; set; }
        
		public int? Actualizado_Por { get; set; }
        [DataType(DataType.DateTime)]
        
		public DateTime? Actualizado_En { get; set; }

        [ForeignKey("Id_Padre")]
        public Menu MenuPadre { get; set; } 
        
		[ForeignKey("Id_Recinto")]
        public Recinto Recinto { get; set; }
        
		[ForeignKey("Creado_Por")]
        public ApplicationUser UsuarioCrea { get; set; }
        
		[ForeignKey("Actualizado_Por")]
        public ApplicationUser UsuarioModifica { get; set; }
        
		public virtual ICollection<Menu> Menues { get; set; }
    }
}