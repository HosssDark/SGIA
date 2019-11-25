using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IUserRepository : IRepositoryBase<User>, IDisposable
    {
        User VerificationEmail(string Email);

        IEnumerable<UserGridViewModel> Grid(string Buscar, int? StatusId = null, int? AreaAtuacaoId = null, int? TiposAcessoId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "");

        IEnumerable<UserGridViewModel> Report();
    }
}