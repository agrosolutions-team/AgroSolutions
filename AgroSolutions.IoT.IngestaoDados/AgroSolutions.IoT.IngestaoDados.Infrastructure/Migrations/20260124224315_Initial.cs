using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeiturasSensorTalhao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SensorId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TalhaoId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PropriedadeId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NomeTalhao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CulturaPlantada = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AreaEmHectares = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UmidadeSoloPercentual = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    TemperaturaCelsius = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    PrecipitacaoMm = table.Column<decimal>(type: "numeric(7,2)", precision: 7, scale: 2, nullable: false),
                    UmidadeArPercentual = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    VelocidadeVentoKmh = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeiturasSensorTalhao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_propriedade_id",
                table: "LeiturasSensorTalhao",
                column: "PropriedadeId");

            migrationBuilder.CreateIndex(
                name: "idx_talhao_timestamp",
                table: "LeiturasSensorTalhao",
                columns: new[] { "TalhaoId", "Timestamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeiturasSensorTalhao");
        }
    }
}
