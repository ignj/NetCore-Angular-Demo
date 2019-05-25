using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
