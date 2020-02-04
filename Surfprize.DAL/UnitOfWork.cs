using System.Threading.Tasks;

namespace Surfprize.DAL
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly SurfprizeDbContext dbContext;

        public UnitOfWork(SurfprizeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
