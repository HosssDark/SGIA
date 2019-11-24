using Domain;
using System.IO;

namespace Repository
{
    public class ParamDirectoryRepository : RepositoryBase<ParamDirectory>, IParamDirectoryRepository
    {
        public void SalvarArquivo(Stream InputStream, string DirectOrigin, string Folder, string Name, int Id, string Extesion, string PathVirtual)
        {
            string path = string.Format("{0}\\{1}\\{2}", PathVirtual, DirectOrigin, Folder);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles(Name + Id + ".*");

            if (files.Length > 0)
            {
                foreach (FileInfo file in files)
                {
                    file.Delete();
                }
            }

            Name = string.Format("{0}{1}{2}", Name, Id, Extesion);
            path = string.Format("{0}\\{1}", path, Name);

            using (FileStream fileStream = File.Create(path))
            {
                InputStream.Seek(0, SeekOrigin.Begin);
                InputStream.CopyTo(fileStream);
            }
        }

        public string GetImageUser(int UserId, string DirectOrigin, string Folder, string Name, string PathVirtual)
        {
            Name = string.Format("{0}{1}.*", Name, UserId);
            var Info = new FileInfo(Name);

            string path = string.Format("{0}\\{1}\\{2}", PathVirtual, DirectOrigin, Folder);

            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles(Name);

                if (files.Length > 0)
                {
                    string Return = "";

                    foreach (FileInfo file in files)
                    {
                        Return = string.Format("/{0}/{1}/{2}", DirectOrigin, Folder, file.Name);
                    }

                    return Return;
                }
                else
                    return "/images/sem-imagem-avatar.png";
            }
            else
                return "/images/sem-imagem-avatar.png";
        }

        public string GetImage(int Id, string DirectOrigin, string Folder, string Name, string PathVirtual)
        {
            Name = string.Format("{0}{1}.*", Name, Id);
            var Info = new FileInfo(Name);

            string path = string.Format("{0}\\{1}\\{2}", PathVirtual, DirectOrigin, Folder);

            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles(Name);

                if (files.Length > 0)
                {
                    string Return = "";

                    foreach (FileInfo file in files)
                    {
                        Return = string.Format("/{0}/{1}/{2}", DirectOrigin, Folder, file.Name);
                    }

                    return Return;
                }
                else
                    return "/images/sem-imagem.jpg";
            }
            else
                return "/images/sem-imagem.jpg";
        }
    }
}