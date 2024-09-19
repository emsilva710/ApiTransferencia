using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TransferenciaAPI.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaCuentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    CuentaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroCuenta = table.Column<string>(type: "text", nullable: false),
                    Saldo = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.CuentaId);
                });

            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    TransferenciaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CuentaOrigenId = table.Column<int>(type: "integer", nullable: false),
                    CuentaDestinoId = table.Column<int>(type: "integer", nullable: false),
                    Monto = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaTransferencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.TransferenciaId);
                    table.ForeignKey(
                        name: "FK_Transferencias_Cuentas_CuentaDestinoId",
                        column: x => x.CuentaDestinoId,
                        principalTable: "Cuentas",
                        principalColumn: "CuentaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transferencias_Cuentas_CuentaOrigenId",
                        column: x => x.CuentaOrigenId,
                        principalTable: "Cuentas",
                        principalColumn: "CuentaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_CuentaDestinoId",
                table: "Transferencias",
                column: "CuentaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_CuentaOrigenId",
                table: "Transferencias",
                column: "CuentaOrigenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transferencias");

            migrationBuilder.DropTable(
                name: "Cuentas");
        }
    }
}
