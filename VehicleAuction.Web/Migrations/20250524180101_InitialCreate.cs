using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleAuction.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ABS = table.Column<bool>(type: "bit", nullable: false),
                    ASR = table.Column<bool>(type: "bit", nullable: false),
                    CocukKilidi = table.Column<bool>(type: "bit", nullable: false),
                    Distronic = table.Column<bool>(type: "bit", nullable: false),
                    ESP = table.Column<bool>(type: "bit", nullable: false),
                    GeceGorusSistemi = table.Column<bool>(type: "bit", nullable: false),
                    HavaYastigiSurucu = table.Column<bool>(type: "bit", nullable: false),
                    HavaYastigiYolcu = table.Column<bool>(type: "bit", nullable: false),
                    Immobilizer = table.Column<bool>(type: "bit", nullable: false),
                    Isofix = table.Column<bool>(type: "bit", nullable: false),
                    KorNoktaUyariSistemi = table.Column<bool>(type: "bit", nullable: false),
                    MerkeziKilit = table.Column<bool>(type: "bit", nullable: false),
                    SeritTakipSistemi = table.Column<bool>(type: "bit", nullable: false),
                    YokusKalkisDesteği = table.Column<bool>(type: "bit", nullable: false),
                    YorgunlukTespitSistemi = table.Column<bool>(type: "bit", nullable: false),
                    ZirhliArac = table.Column<bool>(type: "bit", nullable: false),
                    HidrolikDireksiyon = table.Column<bool>(type: "bit", nullable: false),
                    UcuncuSiraKoltuklar = table.Column<bool>(type: "bit", nullable: false),
                    DeriKoltuk = table.Column<bool>(type: "bit", nullable: false),
                    KumasKoltuk = table.Column<bool>(type: "bit", nullable: false),
                    ElektrikliCamlar = table.Column<bool>(type: "bit", nullable: false),
                    Klima = table.Column<bool>(type: "bit", nullable: false),
                    OtmKararanDikizAynasi = table.Column<bool>(type: "bit", nullable: false),
                    OnGorusKamerasi = table.Column<bool>(type: "bit", nullable: false),
                    OnKoltukKolDayamasi = table.Column<bool>(type: "bit", nullable: false),
                    AnahtarsizGiris = table.Column<bool>(type: "bit", nullable: false),
                    FonksiyonelDireksiyon = table.Column<bool>(type: "bit", nullable: false),
                    IsitmaliDireksiyon = table.Column<bool>(type: "bit", nullable: false),
                    KoltuklarElektrikli = table.Column<bool>(type: "bit", nullable: false),
                    KoltuklarHafizali = table.Column<bool>(type: "bit", nullable: false),
                    KoltuklarIsitmali = table.Column<bool>(type: "bit", nullable: false),
                    KoltuklarSogutmali = table.Column<bool>(type: "bit", nullable: false),
                    HizSabitlemeSistemi = table.Column<bool>(type: "bit", nullable: false),
                    SogutmaliTorpido = table.Column<bool>(type: "bit", nullable: false),
                    YolBilgisayari = table.Column<bool>(type: "bit", nullable: false),
                    HeadUpDisplay = table.Column<bool>(type: "bit", nullable: false),
                    StartStop = table.Column<bool>(type: "bit", nullable: false),
                    GeriGorusKamerasi = table.Column<bool>(type: "bit", nullable: false),
                    AyaklaAcilanBagajKapagi = table.Column<bool>(type: "bit", nullable: false),
                    Hardtop = table.Column<bool>(type: "bit", nullable: false),
                    FarAdaptif = table.Column<bool>(type: "bit", nullable: false),
                    AynalarElektrikli = table.Column<bool>(type: "bit", nullable: false),
                    AynalarIsitmali = table.Column<bool>(type: "bit", nullable: false),
                    AynalarHafizali = table.Column<bool>(type: "bit", nullable: false),
                    ParkSensoruArka = table.Column<bool>(type: "bit", nullable: false),
                    ParkSensoruOn = table.Column<bool>(type: "bit", nullable: false),
                    ParkAsistani = table.Column<bool>(type: "bit", nullable: false),
                    Sunroof = table.Column<bool>(type: "bit", nullable: false),
                    AkilliBagajKapagi = table.Column<bool>(type: "bit", nullable: false),
                    PanoramikCamTavan = table.Column<bool>(type: "bit", nullable: false),
                    RomorkCekiDemiri = table.Column<bool>(type: "bit", nullable: false),
                    AndroidAuto = table.Column<bool>(type: "bit", nullable: false),
                    AppleCarPlay = table.Column<bool>(type: "bit", nullable: false),
                    Bluetooth = table.Column<bool>(type: "bit", nullable: false),
                    UsbAux = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    StartingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumIncrement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auctions_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AccountHolder = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuctionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuthorizedPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AuthorizedPersonPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_VehicleId",
                table: "Auctions",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_UserId",
                table: "BankAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AuctionId",
                table: "Bids",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_UserId",
                table: "Bids",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_UserId1",
                table: "Bids",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Users_UserId",
                table: "BankAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_UserId",
                table: "Bids",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_UserId1",
                table: "Bids",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
