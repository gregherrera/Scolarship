﻿//using MySql.Data.EntityFrameworkCore.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Escolaridad.Data;

namespace Escolaridad.Model
{
    //[MySqlCharset("utf8"), MySqlCollation("utf8_spanish_ci")]
    public class UserPais
    {
        [Key]
        public int Id { get; set; }
        
		[Required]
        public int Id_Pais { get; set; }
        
		[Required]
        public int Id_User { get; set; }
        
		[Required]
        public int Creado_Por { get; set; }
        
		[Required]
        [DataType(DataType.DateTime)]
        public DateTime Creado_En { get; set; }

        public int? Actualizado_Por { get; set; }
        
		[DataType(DataType.DateTime)]
        
		public DateTime? Actualizado_En { get; set; }

        [ForeignKey("Id_Pais")]
        public Pais Pais { get; set; }
        
		[ForeignKey("Id_User")]
        public ApplicationUser Usuario { get; set; }
        
		[ForeignKey("Creado_Por")]
        public ApplicationUser UsuarioCrea { get; set; }
        [ForeignKey("Actualizado_Por")]
        public ApplicationUser UsuarioModifica { get; set; }
    }
}