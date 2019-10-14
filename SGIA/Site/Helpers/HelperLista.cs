using Microsoft.AspNetCore.Mvc.Rendering;
using Site.Models;
using System.Collections.Generic;
using System.Linq;

namespace Site
{
    public class HelperLista
    {
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

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }

        public static List<SelectListItem> Turmas(int? TurmaId = null, bool Ativo = true)
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

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }

        public static List<SelectListItem> Editoras(int? EditoraId = null, int StatusId = 1)
        {
            using (Contexto bd = new Contexto())
            {
                var Editoras = bd.Editoras.Where(a => a.StatusId == StatusId).ToList();

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

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }

        public static List<SelectListItem> Livros(int? EditoraId = null, int StatusId = 1)
        {
            using (Contexto bd = new Contexto())
            {
                var Editoras = bd.Editoras.Where(a => a.StatusId == StatusId).ToList();

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

                return Lista.OrderBy(a => a.Text).ToList();
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

        public static List<SelectListItem> Docentes(int? DocenteId = null, int StatusId = 1)
        {
            using (Contexto bd = new Contexto())
            {
                var Docentes = bd.Docentes.Where(a => a.StatusId == StatusId).ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in Docentes)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.StatusId.ToString(),
                        Selected = (DocenteId != null && item.DocenteId == StatusId)
                    });
                }

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }

        public static List<SelectListItem> Dicentes(int? DicenteId = null, int StatusId = 1)
        {
            using (Contexto bd = new Contexto())
            {
                var Dicentes = bd.Dicentes.Where(a => a.StatusId == StatusId).ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in Dicentes)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.StatusId.ToString(),
                        Selected = (DicenteId != null && item.DicenteId == DicenteId)
                    });
                }

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }

        public static List<SelectListItem> AreaAtuacao(int? AreaAtuacaoId = null)
        {
            using (Contexto bd = new Contexto())
            {
                var AreaAtuacao = bd.AreasAtuacao.ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in AreaAtuacao)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Descricao,
                        Value = item.AreaAtuacaoId.ToString(),
                        Selected = (AreaAtuacaoId != null && item.AreaAtuacaoId == AreaAtuacaoId)
                    });
                }

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }

        public static List<SelectListItem> Titulos(int? TituloId = null)
        {
            using (Contexto bd = new Contexto())
            {
                var Titulos = bd.Titulos.ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in Titulos)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Descricao,
                        Value = item.TituloId.ToString(),
                        Selected = (TituloId != null && item.TituloId == TituloId)
                    });
                }

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }

        public static List<SelectListItem> TipoDocentes(int? TipoDocenteId = null)
        {
            using (Contexto bd = new Contexto())
            {
                var Docentes = bd.TiposDocente.ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in Docentes)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Descricao,
                        Value = item.TipoDocenteId.ToString(),
                        Selected = (TipoDocenteId != null && item.TipoDocenteId == TipoDocenteId)
                    });
                }

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }

        public static List<SelectListItem> Diciplinas(int? DiciplinaId = null, int StatusId = 1)
        {
            using (Contexto bd = new Contexto())
            {
                var Diciplinas = bd.Diciplinas.Where(a => a.StatusId == StatusId).ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in Diciplinas)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.StatusId.ToString(),
                        Selected = (DiciplinaId != null && item.DiciplinaId == DiciplinaId)
                    });
                }

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }

        public static List<SelectListItem> Projetos(int? ProjetoId = null, int StatusId = 1)
        {
            using (Contexto bd = new Contexto())
            {
                var Projetos = bd.Projetos.Where(a => a.StatusId == StatusId).ToList();

                var Lista = new List<SelectListItem>();

                foreach (var item in Projetos)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.StatusId.ToString(),
                        Selected = (ProjetoId != null && item.ProjetoId == ProjetoId)
                    });
                }

                return Lista.OrderBy(a => a.Text).ToList();
            }
        }
    }
}