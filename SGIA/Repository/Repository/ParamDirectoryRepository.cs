using Domain;
using System.IO;

namespace Repository
{
    public class ParamDirectoryRepository : RepositoryBase<ParamDirectory>, IParamDirectoryRepository
    {
        public string SetDirectory(int UsuarioId, string image)
        {
            var param = this.GetById(1);

            if (!Directory.Exists(param.Path))
                Directory.CreateDirectory(param.Path);






            return "";
        }
 
    }
}