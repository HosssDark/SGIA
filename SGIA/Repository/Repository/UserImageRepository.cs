using Domain;
using System.IO;
using System.Linq;

namespace Repository
{
    public class UserImageRepository: RepositoryBase<UserImage>, IUserImageRepository
    {
        public void SalvarArquivo(Stream InputStream, string DirectOrigin, string NameFile, int Id, string Extesion)
        {
            IParamDirectoryRepository param = new ParamDirectoryRepository();

            var Direct = param.Get(a => a.Directory == DirectOrigin).FirstOrDefault();

            if (!Directory.Exists(Direct.Path))
                Directory.CreateDirectory(Direct.Path);

            string path = string.Format("{0}\\{1}", Direct.Path, string.Format("{0}{1}{2}", DirectOrigin, Id, Extesion));

            using (FileStream fileStream = File.Create(path))
            {
                InputStream.Seek(0, SeekOrigin.Begin);
                InputStream.CopyTo(fileStream);
            }
        }
    }
}