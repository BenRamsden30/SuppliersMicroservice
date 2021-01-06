using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestockingMicroService.Data;

namespace RestockingMicroService.Data
{
    public class RestockingMicroServiceContext : DbContext
    {
        public RestockingMicroServiceContext (DbContextOptions<RestockingMicroServiceContext> options)
            : base(options)
        {
        }

        public DbSet<RestockingMicroService.Data.Suppliers> Suppliers { get; set; }

        public DbSet<RestockingMicroService.Data.Restocks> Restocks { get; set; }
    }
}
