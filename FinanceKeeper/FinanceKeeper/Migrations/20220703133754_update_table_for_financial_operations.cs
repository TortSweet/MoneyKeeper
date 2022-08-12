using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceKeeper.Migrations
{
    public partial class update_table_for_financial_operations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialOperation_FinancialCategory_FinancialCategoryId",
                table: "FinancialOperation");

            migrationBuilder.DropIndex(
                name: "IX_FinancialOperation_FinancialCategoryId",
                table: "FinancialOperation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FinancialOperation_FinancialCategoryId",
                table: "FinancialOperation",
                column: "FinancialCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialOperation_FinancialCategory_FinancialCategoryId",
                table: "FinancialOperation",
                column: "FinancialCategoryId",
                principalTable: "FinancialCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
