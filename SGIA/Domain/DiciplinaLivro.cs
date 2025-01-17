﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class DiciplinaLivro
    {
        [Key]
        [Display(Name = "ID")]
        public int DiciplinaLivroId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Diciplina")]
        [Required(ErrorMessage = "Obrigatório")]
        public int DiciplinaId { get; set; }

        [Display(Name = "Livro")]
        [Required(ErrorMessage = "Obrigatório")]
        public int LivroId { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Obrigatório")]
        public int StatusId { get; set; }
    }
}