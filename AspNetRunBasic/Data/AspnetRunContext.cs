using AspNetRunBasic.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetRunBasic.Data
{
    public class AspnetRunContext : DbContext
    {
        public AspnetRunContext(DbContextOptions<AspnetRunContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
