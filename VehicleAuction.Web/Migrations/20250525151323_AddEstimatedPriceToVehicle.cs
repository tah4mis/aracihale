using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleAuction.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddEstimatedPriceToVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedPrice",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedPrice",
                table: "Vehicles");
        }
    }
}
