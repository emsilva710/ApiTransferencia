using System.ComponentModel.DataAnnotations;

public class Cuenta
{
    public int CuentaId { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 10)]
    public string NumeroCuenta { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Saldo { get; set; }

    public ICollection<Transferencia> TransferenciasEnviadas { get; set; }
    public ICollection<Transferencia> TransferenciasRecibidas { get; set; }
}
