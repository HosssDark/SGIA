using Domain;
using System;
using System.IO;

namespace Repository
{
    public interface IUserImageRepository : IRepositoryBase<UserImage>, IDisposable
    {
        void SalvarArquivo(Stream InputStream, string DirectOrigin, string NameFile, int Id, string Extesion);
    }
}