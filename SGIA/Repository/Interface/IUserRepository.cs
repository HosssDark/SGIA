using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IUserRepository : IRepositoryBase<User>, IDisposable
    {
        User VerificationEmail(string Email);

        IEnumerable<UserViewModel> Grid(string Buscar, int? StatusId = null, int? AreaAtuacaoId = null, DateTime? DataInicial = null, DateTime? DataFinal = null);

        IEnumerable<UserViewModel> Report();
    }
}