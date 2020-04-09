using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Data
{
    // https://www.npgsql.org/efcore/index.html
    // https://medium.com/faun/asp-net-core-entity-framework-core-with-postgresql-code-first-d99b909796d7
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options) : base(options)
        {
            // TODO : Burdasın -- postresql ayrı class lib olmadı -- o zaman bunu catalog da yap burda sql server olsun
            // olmuyor degistir
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");

        public DbSet<Order.Core.Entities.Order> Orders { get; set; }        
    }
}
