using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class AppDbContext
    {
       public partial class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Login> Logins { get; set; }
            public DbSet<DTO.UsuarioGetAll> UsuarioGets { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Usuario>()
                    .HasOne(u => u.Login)
                    .WithMany()
                    .HasForeignKey(u => u.IdLogin);

                modelBuilder.Entity<DTO.UsuarioGetAll>()
                    .HasNoKey();
            }
        }
    }

}
