using EFCoreSqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSqlServer.Data
{
    public class ContextProdutos : DbContext
    {
        public ContextProdutos(DbContextOptions<ContextProdutos> options) : base(options) 
        { 
            this.Database.EnsureCreated();
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
