using API_Lawyer.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Lawyer.Assets.Data
{
    public class LawyerContext : DbContext
    {
        public LawyerContext(DbContextOptions<LawyerContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transicao>()
                .HasKey(transicao => new { transicao.OrigemId, transicao.ProcessoId });

            builder.Entity<Transicao>()
                .HasOne(transicao => transicao.Origem)
                .WithMany(origem => origem.Transicoes)
                .HasForeignKey(transicao => transicao.OrigemId);

            builder.Entity<Transicao>()
                .HasOne(transicao => transicao.Processo)
                .WithMany(processo => processo.Transicoes)
                .HasForeignKey(transicao => transicao.ProcessoId);

            builder.Entity<Movimentacao>()
                .HasOne(movimentacao => movimentacao.Processo)
                .WithMany(processo => processo.Movimentacoes)
                .HasForeignKey(movimentacao => movimentacao.ProcessoId);

        }

        public DbSet<Processo> Processos { get; set; }
        public DbSet<Origem> Origens { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Transicao> Transicoes { get; set; }
    }
}
