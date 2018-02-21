using Microsoft.EntityFrameworkCore;

namespace ASPNetCore.API.Boilerplate.Models
{
    public class MyDBContext:DbContext
    {
        #region DBSets
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        #endregion

        #region Constructors
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        { }
        #endregion
    }
}
