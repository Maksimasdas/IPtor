using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPtor.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Service = table.Column<int>(type: "int", nullable: false),
                    Vin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certificate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Formular = table.Column<int>(type: "int", nullable: false),
                    FormularDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    InventoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Service = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UndPosition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Batteries",
                columns: table => new
                {
                    BatteryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingCurrent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Polarity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batteries", x => x.BatteryId);
                    table.ForeignKey(
                        name: "FK_Batteries_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    DefectsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineDefect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Electro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clutch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kpp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Steering = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontGear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackGear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frame = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecEq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalCond = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberWheel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelAvail = table.Column<float>(type: "real", nullable: true),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.DefectsId);
                    table.ForeignKey(
                        name: "FK_Defects_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefectsTOs",
                columns: table => new
                {
                    DefectsTOId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Engine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontGear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackGear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Electro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PTV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Water = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralView = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Break = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PuskRuk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domkrat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nasos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SHpric = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Molotok = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ploskogubci = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kluchi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cluchi2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cluchi3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cluchi4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chehli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aptechka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Znak = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ognetush = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Radio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Protivougon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gromkogov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Protivotuman = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mayak = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fara = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prochee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fuel = table.Column<float>(type: "real", nullable: false),
                    Zakl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pred = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectsTOs", x => x.DefectsTOId);
                    table.ForeignKey(
                        name: "FK_DefectsTOs_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tires",
                columns: table => new
                {
                    TireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Profile = table.Column<float>(type: "real", nullable: true),
                    Diameter = table.Column<float>(type: "real", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tires", x => x.TireId);
                    table.ForeignKey(
                        name: "FK_Tires_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batteries_CarId",
                table: "Batteries",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_CarId",
                table: "Defects",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_DefectsTOs_CarId",
                table: "DefectsTOs",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Tires_CarId",
                table: "Tires",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Batteries");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "DefectsTOs");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Tires");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
