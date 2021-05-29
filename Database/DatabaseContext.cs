using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProvaDevNet
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Caminhao> Caminhoes { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Blogging;Integrated Security=True");
            optionsBuilder.UseSqlite("Data Source=Caminhoes.db;");
        }
    }

}
