public interface ICuentaRepository
{
    /// <summary>
    /// Obtiene una cuenta bancaria por su identificador.
    /// </summary>
    /// <param name="cuentaId">El identificador de la cuenta.</param>
    /// <returns>La cuenta bancaria correspondiente.</returns>
    Task<Cuenta> ObtenerCuentaPorIdAsync(int cuentaId);
    /// <summary>
    /// Obtiene una cuenta bancaria por su número.
    /// </summary>
    /// <param name="numeroCuenta">El número de la cuenta.</param>
    /// <returns>La cuenta bancaria correspondiente.</returns>
    Task<Cuenta> ObtenerCuentaPorNumeroAsync(string numeroCuenta);
    /// <summary>
    /// Actualiza una cuenta bancaria.
    /// </summary>
    /// <param name="cuenta">La cuenta bancaria con los datos actualizados.</param>
    Task ActualizarCuentaAsync(Cuenta cuenta);

}

public interface ITransferenciaRepository
{
    Task<List<Transferencia>> ObtenerTransferenciasPorCuentaIdAsync(int cuentaId);
    Task RealizarTransferenciaAsync(Transferencia transferencia);
    
}

