using System.Threading.Tasks;

namespace NorthWind.Entities.Interfaces
{
    //Patron UnitOfWork
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
