﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class DocenteTurma
    {
        [Key]
        [Display(Name = "ID")]
        public int DocenteTurmaId { get; set; }

        [Display(Name = "Docente")]
        [Required]
        public int UserId { get; set; }

        [Display(Name = "Turma")]
        [Required]
        public int TurmaId { get; set; }
    }
}