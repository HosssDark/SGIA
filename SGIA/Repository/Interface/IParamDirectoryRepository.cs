using Domain;
using System;
using System.IO;

namespace Repository
{
    public interface IParamDirectoryRepository : IRepositoryBase<ParamDirectory>, IDisposable
    {
        void SalvarArquivo(Stream InputStream, string DirectOrigin, string Folder, string NameFile, int Id, string Extesion, string PathVirtual);

        string GetImageUser(int UserId, string DirectOrigin, string Folder, string Name, string PathVirtual);

        string GetImage(int Id, string DirectOrigin, string Folder, string Name, string PathVirtual);
    }
}