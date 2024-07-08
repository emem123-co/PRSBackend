using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRSBackend.Models;

namespace PRSBackend.Data
{
    public class PRSBackendContext : DbContext
    {
        public PRSBackendContext (DbContextOptions<PRSBackendContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
    }
}
