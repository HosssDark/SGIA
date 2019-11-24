using Domain;
using System.IO;

namespace Repository
{
    public class ParamDirectoryRepository : RepositoryBase<ParamDirectory>, IParamDirectoryRepository
    {
        public void SalvarArquivo(Stream InputStream, string DirectOrigin, string Folder, string NameFile, int Id, string Extesion, string PathVirtual)
        {
            string path = PathVirtual;

            string Name = string.Format("{0}{1}{2}", DirectOrigin, Id, ".jpg");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = string.Format("{0}\\{1}", path, Name);

            using (FileStream fileStream = File.Create(path))
            {
                InputStream.Seek(0, SeekOrigin.Begin);
                InputStream.CopyTo(fileStream);
            }
        }

        public string GetImageUser(int UserId, string DirectOrigin, string Folder, string Name, string PathVirtual)
        {
            Name = string.Format("{0}{1}.jpg", Name, UserId);
            var Info = new FileInfo(Name);

            string path = string.Format("{0}\\{1}", PathVirtual, DirectOrigin);

            if (Directory.Exists(path))
            {
                path = string.Format("/{0}/{1}/{2}", DirectOrigin, Folder, Info.Name);

                if (File.Exists(path))
                    return path;
                else
                    return "/images/sem-imagem-avatar.png";
            }
            else
                return "/images/sem-imagem-avatar.png";
        }

        public string GetImage(int Id, string DirectOrigin, string Folder, string Name, string PathVirtual)
        {
            Name = string.Format("{0}{1}.jpg", Name, Id);
            var Info = new FileInfo(Name);

            string path = string.Format("{0}\\{1}", PathVirtual, DirectOrigin);

            if (Directory.Exists(path))
            {
                path = string.Format("/{0}/{1}/{2}", DirectOrigin, Folder, Info.Name);

                if (File.Exists(path))
                    return path;
                else
                    return "/images/sem-imagem.jpg";
            }
            else
                return "/images/sem-imagem.jpg";
        }
    }
}