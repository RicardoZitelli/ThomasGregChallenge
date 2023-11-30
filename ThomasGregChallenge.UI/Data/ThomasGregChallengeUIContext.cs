using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThomasGregChallenge.UI.Models;

namespace ThomasGregChallenge.UI.Data
{
    public class ThomasGregChallengeUIContext : DbContext
    {
        public ThomasGregChallengeUIContext (DbContextOptions<ThomasGregChallengeUIContext> options)
            : base(options)
        {
        }

        public DbSet<ThomasGregChallenge.UI.Models.ClienteModel> ClienteModel { get; set; } = default!;
    }
}
