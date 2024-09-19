public class CuentaDto
{
    public int CuentaId { get; set; }
    public string NumeroCuenta { get; set; }
    public decimal Saldo { get; set; }
    public List<TransferenciaDto> TransferenciasEnviadas { get; set; }
    public List<TransferenciaDto> TransferenciasRecibidas { get; set; }
}