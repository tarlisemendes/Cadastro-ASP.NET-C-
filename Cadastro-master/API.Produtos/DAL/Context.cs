using API.Produtos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Produtos.DAL
{
    public class Context : DbContext
    {
        //public DbSet<Produto> Produtos { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>().HasKey(t => t.Id);
            modelBuilder.Entity<Produto>().HasMany(t => t.Fonecedores).WithOne(t => t.Produto);


            modelBuilder.Entity<Fornecedor>().HasKey(t => t.Id);
            modelBuilder.Entity<Fornecedor>().HasOne(t => t.Produto);


            modelBuilder.Entity<Usuario>().HasKey(t => t.Id);
            



        }


    }
}
