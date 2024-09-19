public class TransferenciaDto
{
    public int TransferenciaId { get; set; }
    public string NumeroCuentaOrigen { get; set; }
    public string NumeroCuentaDestino { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaTransferencia { get; set; }
}
