using Microsoft.EntityFrameworkCore;

public class CuentaRepository : ICuentaRepository
{
    private readonly ApplicationDbContext _context;

    public CuentaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cuenta> ObtenerCuentaPorIdAsync(int cuentaId)
    {
        return await _context.Cuentas.FindAsync(cuentaId);
    }

public async Task<Cuenta> ObtenerCuentaPorNumeroAsync(string numeroCuenta)
{
    var cuenta = await _context.Cuentas
        .Include(c => c.TransferenciasEnviadas)
            .ThenInclude(t => t.CuentaDestino)
        .Include(c => c.TransferenciasRecibidas)
            .ThenInclude(t => t.CuentaOrigen)
        .Where(c => c.NumeroCuenta == numeroCuenta)
        .FirstOrDefaultAsync();

    return cuenta;
}



    public async Task ActualizarCuentaAsync(Cuenta cuenta)
    {
        _context.Cuentas.Update(cuenta);
        await _context.SaveChangesAsync();
    }
}


public class TransferenciaRepository : ITransferenciaRepository
{
    private readonly ApplicationDbContext _context;
    public TransferenciaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transferencia>> ObtenerTransferenciasPorCuentaIdAsync(int cuentaId)
    {
        return await _context.Transferencias
            .Where(t => t.CuentaOrigenId == cuentaId || t.CuentaDestinoId == cuentaId)
            .ToListAsync();
    }

    public async Task RealizarTransferenciaAsync(Transferencia transferencia)
    {
        _context.Transferencias.Add(transferencia);
        await _context.SaveChangesAsync();
    }
}
