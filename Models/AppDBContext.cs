using Microsoft.EntityFrameworkCore;

namespace Kudvenkat_.NET_Core.Models
{
    public class AppDBContext : DbContext
    {
       
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }//--->DbContext uses DbContextOptions to handle connection strings and for other functionalities...

        public DbSet<Employee> Employees { get; set; } //---> we will be using this property to query and save instances of the Employee Class

        //---> linq queries for operations will be translated into queries against the underlying database. 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.seed();
        }

    }
}
