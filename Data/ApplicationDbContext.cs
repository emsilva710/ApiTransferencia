using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<Cuenta> Cuentas { get; set; }
    public DbSet<Transferencia> Transferencias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
   // Configuración de las relaciones
    modelBuilder.Entity<Transferencia>()
        .HasOne(t => t.CuentaOrigen)
        .WithMany(c => c.TransferenciasEnviadas)
        .HasForeignKey(t => t.CuentaOrigenId)
        .OnDelete(DeleteBehavior.Restrict); // Asegurarse de que la eliminación no cause problemas

    modelBuilder.Entity<Transferencia>()
        .HasOne(t => t.CuentaDestino)
        .WithMany(c => c.TransferenciasRecibidas)
        .HasForeignKey(t => t.CuentaDestinoId)
        .OnDelete(DeleteBehavior.Restrict); // Asegurarse de que la eliminación no cause problemas
              // Datos semilla para la tabla Cuentas
    modelBuilder.Entity<Cuenta>().HasData(
        new Cuenta { CuentaId = 1, NumeroCuenta = "1234567890", Saldo = 1000.00M },
        new Cuenta { CuentaId = 2, NumeroCuenta = "0987654321", Saldo = 500.00M }
    );
    }
}
