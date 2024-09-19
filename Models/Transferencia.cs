public class Transferencia
{
    public int TransferenciaId { get; set; }
    public int CuentaOrigenId { get; set; }
    public int CuentaDestinoId { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaTransferencia { get; set; }

    // Navegaciones
    public Cuenta CuentaOrigen { get; set; }
    public Cuenta CuentaDestino { get; set; }
}