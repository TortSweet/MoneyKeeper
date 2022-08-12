using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceKeeper.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialOperation",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinancialCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialOperation", x => x.OperationId);
                    table.ForeignKey(
                        name: "FK_FinancialOperation_FinancialCategory_FinancialCategoryId",
                        column: x => x.FinancialCategoryId,
                        principalTable: "FinancialCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialOperation_FinancialCategoryId",
                table: "FinancialOperation",
                column: "FinancialCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialOperation");

            migrationBuilder.DropTable(
                name: "FinancialCategory");
        }
    }
}
