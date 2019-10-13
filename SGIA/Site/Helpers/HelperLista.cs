using Microsoft.AspNetCore.Mvc.Rendering;
using Site.Models;
using System.Collections.Generic;
using System.Linq;

namespace Site
{
    public class HelperLista
    {
        public static List<SelectListItem> Turmas(int? TurmaId = null)
        {
            using (Contexto bd = new Contexto())
            {
                var Turmas = bd.Turmas.ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in Turmas)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.TurmaId.ToString(),
                        Selected = (TurmaId != null && item.TurmaId == TurmaId)
                    });
                }

                return Lista;
            }
        }

        public static List<SelectListItem> Editoras(int? EditoraId = null)
        {
            using (Contexto bd = new Contexto())
            {
                var Editoras = bd.Editoras.ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in Editoras)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.EditoraId.ToString(),
                        Selected = (EditoraId != null && item.EditoraId == EditoraId)
                    });
                }

                return Lista;
            }
        }

        public static List<SelectListItem> Status(int? StatusId = null)
        {
            using (Contexto bd = new Contexto())
            {
                var Status = bd.Status.ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in Status)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Descricao,
                        Value = item.StatusId.ToString(),
                        Selected = (StatusId != null && item.StatusId == StatusId)
                    });
                }

                return Lista;
            }
        }

        public static List<SelectListItem> DiaSemana(string Dia = "")
        {
            using (Contexto bd = new Contexto())
            {
                string[] DiaSemana = new string[5];

                DiaSemana[0] = "Segunda-Feira";
                DiaSemana[1] = "Terça-Feira";
                DiaSemana[2] = "Quarta-Feira";
                DiaSemana[3] = "Quinta-Feira";
                DiaSemana[4] = "Sexta-Feira";

                var Lista = new List<SelectListItem>();

                foreach (var item in DiaSemana)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item,
                        Value = item,
                        Selected = (Dia != "" && item == Dia)
                    });
                }

                return Lista;
            }
        }

        public static List<SelectListItem> Periodo(string Periodo = "")
        {
            using (Contexto bd = new Contexto())
            {
                string[] Periodos = new string[5];

                Periodos[0] = "Matutino";
                Periodos[1] = "Noturno";

                var Lista = new List<SelectListItem>();

                foreach (var item in Periodos)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item,
                        Value = item,
                        Selected = (Periodo != "" && item == Periodo)
                    });
                }

                return Lista;
            }
        }
    }
}