using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class AppDbContext : DbContext {
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Folha> Folhas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração do relacionamento entre ConversaModel e UsuarioModel
        modelBuilder.Entity<Folha>()
            .HasOne(folha => folha.Funcionario)
            .WithMany(usuario => usuario.Folhas)
            .HasForeignKey(folha => folha.FuncionarioId)
            .OnDelete(DeleteBehavior.Cascade);
            
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite("Data Source=diego.db");
    }
}