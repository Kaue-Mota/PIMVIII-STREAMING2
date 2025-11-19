// File: Data/StreamingContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StreamApi.Data
{
    public class StreamingContextFactory : IDesignTimeDbContextFactory<StreamingContext>
    {
        public StreamingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StreamingContext>();

            // Connection string usada apenas em tempo de design; ajuste se quiser outro DB
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StreamApiDb;Trusted_Connection=True;");

            return new StreamingContext(optionsBuilder.Options);
        }
    }
}
