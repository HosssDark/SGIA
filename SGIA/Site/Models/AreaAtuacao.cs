﻿using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class AreaAtuacao
    {
        [Key]
        [Display(Name = "ID")]
        public int AreaAtuacaoId { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}