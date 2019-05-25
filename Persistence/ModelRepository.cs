using NetCore_Angular_Demo.Core;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Persistence
{
    public class ModelRepository : IModelRepository
    {
        private readonly AppDbContext context;

        public ModelRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Task<Model> GetModel(int id)
        {
            return context.Models.FindAsync(id);
        }
    }
}
