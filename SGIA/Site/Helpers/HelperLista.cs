using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Site
{
    public class HelperLista
    {
        public static List<SelectListItem> DiaSemana(string Dia = "")
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

        public static List<SelectListItem> Periodo(string Periodo = "")
        {
            string[] Periodos = new string[2];

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

        public static List<SelectListItem> Turmas(int? TurmaId = null, bool Ativo = true)
        {
            ITurmaRepository turRep = new TurmaRepository();

            var Turmas = turRep.GetAll();

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

        public static List<SelectListItem> Editoras(int? EditoraId = null)
        {
            IEditoraRepository ediRep = new EditoraRepository();

            var Editoras = ediRep.GetAll();

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

        public static List<SelectListItem> Livros(int? LivroId = null)
        {
            ILivroRepository livRep = new LivroRepository();

            var Livros = livRep.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Livros)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Titulo,
                    Value = item.LivroId.ToString(),
                    Selected = (LivroId != null && item.LivroId == LivroId)
                });
            }

            return Lista.OrderBy(a => a.Text).ToList();
        }

        public static List<SelectListItem> Status(int? StatusId = null)
        {
            IStatusRepository staRep = new StatusRepository();

            var Status = staRep.GetAll();

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

        public static List<SelectListItem> Docentes(int? UserId = null)
        {
            IUserRepository userRep = new UserRepository();

            var Docentes = userRep.Get(a => a.TipoAcessoId == 2 && a.StatusId == 1);

            var Lista = new List<SelectListItem>();

            foreach (var item in Docentes)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Nome,
                    Value = item.UserId.ToString(),
                    Selected = (UserId != null && item.UserId == UserId)
                });
            }

            return Lista.OrderBy(a => a.Text).ToList();
        }

        public static List<SelectListItem> Dicentes(int? DicenteId = null, int StatusId = 1)
        {
            IDicenteRepository dicRep = new DicenteRepository();

            var Dicentes = dicRep.GetAll();

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

        public static List<SelectListItem> AreaAtuacao(int? AreaAtuacaoId = null)
        {
            IAreaAtuacaoRepository areRep = new AreaAtuacaoRepository();

            var AreaAtuacao = areRep.GetAll();

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

        public static List<SelectListItem> Titulos(int? TituloId = null)
        {
            ITituloRepository titRep = new TituloRepository();

            var Titulos = titRep.GetAll();

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

        public static List<SelectListItem> TipoDocentes(int? TipoDocenteId = null)
        {
            ITipoDocenteRepository tipRep = new TipoDocenteRepository();

            var Tipos = tipRep.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Tipos)
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

        public static List<SelectListItem> TiposAcesso(int? TipoDocenteId = null)
        {
            ITipoAcessoRepository tipRep = new TipoAcessoRepository();

            var Tipos = tipRep.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Tipos)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Descricao,
                    Value = item.TipoAcessoId.ToString(),
                    Selected = (TipoDocenteId != null && item.TipoAcessoId == TipoDocenteId)
                });
            }

            return Lista.OrderBy(a => a.Text).ToList();
        }

        public static List<SelectListItem> Diciplinas(int? DiciplinaId = null, int StatusId = 1)
        {
            IDiciplinaRepository dicRep = new DiciplinaRepository();

            var Diciplinas = dicRep.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Diciplinas)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Nome,
                    Value = item.DiciplinaId.ToString(),
                    Selected = (DiciplinaId != null && item.DiciplinaId == DiciplinaId)
                });
            }

            return Lista.OrderBy(a => a.Text).ToList();
        }

        public static List<SelectListItem> Projetos(int? ProjetoId = null, int StatusId = 1)
        {
            IProjetoRepository proRep = new ProjetoRepository();

            var Projetos = proRep.GetAll();

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

        public static List<SelectListItem> Municipios(int? MunicipioId = null)
        {
            IMunicipioRepository munRep = new MunicipioRepository();

            var Municipios = munRep.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Municipios)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Nome,
                    Value = item.MunicipioId.ToString(),
                    Selected = (MunicipioId != null && item.MunicipioId == MunicipioId)
                });
            }

            return Lista.OrderBy(a => a.Text).ToList();
        }

        public static List<SelectListItem> Estados(string Estado = "")
        {
            IMunicipioRepository munRep = new MunicipioRepository();

            var Municipios = munRep.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Municipios)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Uf,
                    Value = item.Uf,
                    Selected = (Estado != null && item.Uf == Estado)
                });
            }

            return Lista.OrderBy(a => a.Text).ToList();
        }
    }
}