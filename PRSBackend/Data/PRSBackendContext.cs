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
        public DbSet<Vendor> Vendors { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Request> Requests { get; set; } = default!;
        public DbSet<RequestLine> RequestLines { get; set; } = default!;
        //public DbSet<Po> Pos { get; set; } = default!;
        //public DbSet<Poline> Polines { get; set; } = default!;
    }
}
