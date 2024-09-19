using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly ILogger<CuentasController> _logger;

    public CuentasController(ICuentaRepository cuentaRepository, ILogger<CuentasController> logger)
    {
        _cuentaRepository = cuentaRepository;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene el estado de una cuenta y su historial de transferencias por número de cuenta.
    /// </summary>
    /// <param name="numeroCuenta">El número de la cuenta.</param>
    /// <returns>El estado de la cuenta y el historial de transferencias.</returns>
    [HttpGet("EstadoDeCuentaHistorial/{numeroCuenta}")]
    public async Task<IActionResult> GetCuentaByNumero(string numeroCuenta)
    {
        if (string.IsNullOrWhiteSpace(numeroCuenta))
        {
            _logger.LogWarning("Número de cuenta no proporcionado.");
            return BadRequest("El número de cuenta no puede estar vacío.");
        }

        try
        {
            _logger.LogInformation("Obteniendo el estado de la cuenta y su historial para el número de cuenta: {NumeroCuenta}", numeroCuenta);

            var cuenta = await _cuentaRepository.ObtenerCuentaPorNumeroAsync(numeroCuenta);
            if (cuenta == null)
            {
                _logger.LogWarning("Cuenta no encontrada para el número de cuenta: {NumeroCuenta}", numeroCuenta);
                return NotFound("Cuenta no encontrada");
            }

            var cuentaDto = new CuentaDto
            {
                CuentaId = cuenta.CuentaId,
                NumeroCuenta = cuenta.NumeroCuenta,
                Saldo = cuenta.Saldo,
                TransferenciasEnviadas = cuenta.TransferenciasEnviadas?
                    .Select(t => new TransferenciaDto
                    {
                        TransferenciaId = t.TransferenciaId,
                        NumeroCuentaOrigen = t.CuentaOrigen?.NumeroCuenta,
                        NumeroCuentaDestino = t.CuentaDestino?.NumeroCuenta,
                        Monto = t.Monto,
                        FechaTransferencia = t.FechaTransferencia
                    }).ToList(),
                TransferenciasRecibidas = cuenta.TransferenciasRecibidas?
                    .Select(t => new TransferenciaDto
                    {
                        TransferenciaId = t.TransferenciaId,
                        NumeroCuentaOrigen = t.CuentaOrigen?.NumeroCuenta,
                        NumeroCuentaDestino = t.CuentaDestino?.NumeroCuenta,
                        Monto = t.Monto,
                        FechaTransferencia = t.FechaTransferencia
                    }).ToList()
            };

            _logger.LogInformation("Estado de la cuenta y el historial obtenidos exitosamente para el número de cuenta: {NumeroCuenta}", numeroCuenta);
            return Ok(cuentaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el estado de la cuenta y el historial para el número de cuenta: {NumeroCuenta}", numeroCuenta);
            return StatusCode(500, "Ha ocurrido un error inesperado. Inténtelo de nuevo más tarde.");
        }
    }
}
