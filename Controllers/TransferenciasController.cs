using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TransferenciasController : ControllerBase
{
    private readonly ITransferenciaRepository _transferenciaRepository;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly ILogger<TransferenciasController> _logger;

    public TransferenciasController(
        ITransferenciaRepository transferenciaRepository,
        ICuentaRepository cuentaRepository,
        ILogger<TransferenciasController> logger)
    {
        _transferenciaRepository = transferenciaRepository;
        _cuentaRepository = cuentaRepository;
        _logger = logger;
    }

    /// <summary>
    /// Realiza una transferencia de fondos entre dos cuentas.
    /// </summary>
    /// <param name="numeroCuentaOrigen">Número de la cuenta origen.</param>
    /// <param name="numeroCuentaDestino">Número de la cuenta destino.</param>
    /// <param name="monto">Monto a transferir.</param>
    /// <returns>Resultado de la operación.</returns>
    [HttpPost]
    public async Task<IActionResult> RealizarTransferencia(string numeroCuentaOrigen, string numeroCuentaDestino, decimal monto)
    {
        try
        {
            _logger.LogInformation("Iniciando transferencia: Origen={NumeroCuentaOrigen}, Destino={NumeroCuentaDestino}, Monto={Monto}",
                numeroCuentaOrigen, numeroCuentaDestino, monto);

            // Validar parámetros de entrada
            if (string.IsNullOrWhiteSpace(numeroCuentaOrigen) || string.IsNullOrWhiteSpace(numeroCuentaDestino))
            {
                _logger.LogWarning("Número de cuenta vacío: Origen={NumeroCuentaOrigen}, Destino={NumeroCuentaDestino}", 
                    numeroCuentaOrigen, numeroCuentaDestino);
                return BadRequest("Los números de cuenta no pueden estar vacíos.");
            }

            if (monto <= 0)
            {
                _logger.LogWarning("Monto inválido para la transferencia: Monto={Monto}", monto);
                return BadRequest("El monto a transferir debe ser mayor a cero.");
            }

            var cuentaOrigen = await _cuentaRepository.ObtenerCuentaPorNumeroAsync(numeroCuentaOrigen);
            var cuentaDestino = await _cuentaRepository.ObtenerCuentaPorNumeroAsync(numeroCuentaDestino);

            if (cuentaOrigen == null || cuentaDestino == null)
            {
                _logger.LogError("Una o ambas cuentas no fueron encontradas: Origen={NumeroCuentaOrigen}, Destino={NumeroCuentaDestino}",
                    numeroCuentaOrigen, numeroCuentaDestino);
                return NotFound("Una de las cuentas no fue encontrada.");
            }

            if (cuentaOrigen.Saldo < monto)
            {
                _logger.LogWarning("Saldo insuficiente en cuenta origen: Origen={NumeroCuentaOrigen}, SaldoDisponible={Saldo}, MontoRequerido={Monto}",
                    numeroCuentaOrigen, cuentaOrigen.Saldo, monto);
                return BadRequest("Saldo insuficiente.");
            }

            cuentaOrigen.Saldo -= monto;
            cuentaDestino.Saldo += monto;

            var transferencia = new Transferencia
            {
                CuentaOrigenId = cuentaOrigen.CuentaId,
                CuentaDestinoId = cuentaDestino.CuentaId,
                Monto = monto,
                FechaTransferencia = DateTime.UtcNow
            };

            await _cuentaRepository.ActualizarCuentaAsync(cuentaOrigen);
            await _cuentaRepository.ActualizarCuentaAsync(cuentaDestino);
            await _transferenciaRepository.RealizarTransferenciaAsync(transferencia);

            _logger.LogInformation("Transferencia realizada exitosamente: Origen={NumeroCuentaOrigen}, Destino={NumeroCuentaDestino}, Monto={Monto}",
                numeroCuentaOrigen, numeroCuentaDestino, monto);

            return Ok("Transferencia realizada exitosamente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado durante la transferencia: Origen={NumeroCuentaOrigen}, Destino={NumeroCuentaDestino}, Monto={Monto}",
                numeroCuentaOrigen, numeroCuentaDestino, monto);
            return StatusCode(500, "Ha ocurrido un error inesperado. Inténtelo de nuevo más tarde.");
        }
    }
}
