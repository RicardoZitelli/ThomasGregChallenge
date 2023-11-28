using Microsoft.EntityFrameworkCore;
using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Infrastructure.Data
{
    public sealed class SqlContext(DbContextOptions<SqlContext> options) : DbContext(options)
    {
        public DbSet<Cliente>? Cliente { get; set; }
        public DbSet<Logradouro>? Logradouro { get; set; }
    }
}
