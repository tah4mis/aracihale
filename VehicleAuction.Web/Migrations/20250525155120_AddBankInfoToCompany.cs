using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleAuction.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddBankInfoToCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountHolder",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IBAN",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Bids",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bids_CompanyId",
                table: "Bids",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Companies_CompanyId",
                table: "Bids",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Companies_CompanyId",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_CompanyId",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "AccountHolder",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IBAN",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Bids");
        }
    }
}
