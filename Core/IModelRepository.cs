using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Core
{
    public interface IModelRepository
    {
        Task<Model> GetModel(int id);
    }
}
