using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NorthWind.Repositories.EFCore.DataContext
{
    public class NorthWindContextFactory : IDesignTimeDbContextFactory<NorthWindContext>
    {
        public NorthWindContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<NorthWindContext>();
            optionBuilder.UseSqlServer("Server=LAPTOP-LMAR;Database=NorthWindDB;User Id=sa;Pwd=sql");
            return new NorthWindContext(optionBuilder.Options);
        }
    }
}
