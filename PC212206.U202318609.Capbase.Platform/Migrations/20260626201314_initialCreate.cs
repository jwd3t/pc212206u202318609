using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PC212206.U202318609.Capbase.Platform.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "covenants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    document_identifier = table.Column<Guid>(type: "char(36)", nullable: false),
                    client_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    period_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    period_end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    monetary_amount_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    monetary_amount_currency = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    footnotes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_covenants", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_covenants_document_identifier",
                table: "covenants",
                column: "document_identifier",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "covenants");
        }
    }
}
