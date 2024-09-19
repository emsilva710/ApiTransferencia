using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TransferenciaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCuentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cuentas",
                columns: new[] { "CuentaId", "NumeroCuenta", "Saldo" },
                values: new object[,]
                {
                    { 1, "1234567890", 1000.00m },
                    { 2, "0987654321", 500.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cuentas",
                keyColumn: "CuentaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cuentas",
                keyColumn: "CuentaId",
                keyValue: 2);
        }
    }
}
