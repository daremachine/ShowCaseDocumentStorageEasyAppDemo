using DocumentStorageEasyAppDemo.DocumentStorageModule;
using Microsoft.EntityFrameworkCore;

namespace DocumentStorageEasyAppDemo.Core
{
    public class InMemoryDb : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "DocumentDb");
        }
        public DbSet<Document> Documents { get; set; }
    }
}
