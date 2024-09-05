using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Persistence.Contextos
{
    public class ProEventosContext: IdentityDbContext<User, Role, int, 
                                                       IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                                       IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options): base(options){}

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId});

                    userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                    userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
                }
            );

            //Informa quais IDs externos vão criar relacionamento entre o evento e palestrante(N:N)
            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new {PE.EventoId, PE.PalestranteId});

            //Para deletar em cascata as redes sociais
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais) //evento possui redes sociais
                .WithOne(rs => rs.Evento) //Cada rede social com um evento
                .OnDelete(DeleteBehavior.Cascade); //Deletou o evento, deletou a rede social em cascata

             modelBuilder.Entity<Palestrante>()
                .HasMany(p => p.RedesSociais) //palestrante possui redes sociais
                .WithOne(rs => rs.Palestrante) //Cada rede social com um palestrante
                .OnDelete(DeleteBehavior.Cascade); //Deletou Palestrante, deletou a rede social em cascata
        }
    }
}
