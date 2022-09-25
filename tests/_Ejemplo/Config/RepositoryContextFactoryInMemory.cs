using Inventario.Entities.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace _Ejemplo.Config
{
    public static class RepositoryContextFactoryInMemory
    {
        public static RepositoryContextFactory Get()
        {
            var options = new DbContextOptionsBuilder<RepositoryContextFactory>()
                .UseInMemoryDatabase(databaseName: $"Inventario.Db")
                .Options;
            return new RepositoryContextFactory(options);
        }
    }
}
