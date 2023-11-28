using Microsoft.EntityFrameworkCore;
using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Infrastructure.Data
{
    public sealed class SqlContext : DbContext
    {
        public DbSet<Cliente>? Cliente { get; set; }
        public DbSet<Logradouro>? Logradouro { get; set; }

        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }
              
    }
}
