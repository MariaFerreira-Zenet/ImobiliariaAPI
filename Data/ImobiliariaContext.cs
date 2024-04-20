using ImobiliariaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ImobiliariaApi.Data
{
    public class ImobiliariaContext : DbContext
    {
        public ImobiliariaContext(DbContextOptions<ImobiliariaContext> options)
           : base(options)
        {
        }


        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Corretor> Corretores { get; set; }
        public DbSet<Inquilino> Inquilinos { get; set; }
        public DbSet<ProprietarioCorretor> ProprietarioCorretor { get; set; } // Adiciona a tabela de junção ProprietarioCorretor
        public DbSet<CorretorInquilino> CorretorInquilino { get; set; } // Adiciona a tabela de junção CorretorInquilino

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ImobiliariaDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração das chaves primárias compostas
            modelBuilder.Entity<ProprietarioCorretor>()
                .HasKey(pc => new { pc.ProprietarioId, pc.CorretorId });

            modelBuilder.Entity<ProprietarioCorretor>()
                .HasOne(pc => pc.Proprietario)
                .WithMany(p => p.Corretores)
                .HasForeignKey(pc => pc.ProprietarioId);

            modelBuilder.Entity<ProprietarioCorretor>()
                .HasOne(pc => pc.Corretor)
                .WithMany(c => c.Proprietarios)
                .HasForeignKey(pc => pc.CorretorId);

            modelBuilder.Entity<CorretorInquilino>()
                .HasKey(ci => new { ci.CorretorId, ci.InquilinoId });

            modelBuilder.Entity<CorretorInquilino>()
                .HasOne(ci => ci.Corretor)
                .WithMany(c => c.Inquilinos)
                .HasForeignKey(ci => ci.CorretorId);

            modelBuilder.Entity<CorretorInquilino>()
                .HasOne(ci => ci.Inquilino)
                .WithMany(i => i.Corretores)
                .HasForeignKey(ci => ci.InquilinoId);
        }
    }
}
