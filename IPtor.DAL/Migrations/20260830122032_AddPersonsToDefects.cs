using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPtor.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonsToDefects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_Cars_CarId",
                table: "RepairRequests");

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "DefectsTOs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KmSpeedometrStart",
                table: "DefectsTOs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "MhSpeedStart",
                table: "DefectsTOs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ResponcePersonId",
                table: "DefectsTOs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SheffPassServiceId",
                table: "DefectsTOs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AcceptUtoPersonId",
                table: "Defects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalPersonId",
                table: "Defects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Defects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KmSpeedStart",
                table: "Defects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "MhSpeedStart",
                table: "Defects",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ResponcePersonId",
                table: "Defects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SheffPassServiceId",
                table: "Defects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DefectsTOs_DriverId",
                table: "DefectsTOs",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DefectsTOs_ResponcePersonId",
                table: "DefectsTOs",
                column: "ResponcePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DefectsTOs_SheffPassServiceId",
                table: "DefectsTOs",
                column: "SheffPassServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_AcceptUtoPersonId",
                table: "Defects",
                column: "AcceptUtoPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_AdditionalPersonId",
                table: "Defects",
                column: "AdditionalPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_DriverId",
                table: "Defects",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_ResponcePersonId",
                table: "Defects",
                column: "ResponcePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_SheffPassServiceId",
                table: "Defects",
                column: "SheffPassServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Persons_AcceptUtoPersonId",
                table: "Defects",
                column: "AcceptUtoPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Persons_AdditionalPersonId",
                table: "Defects",
                column: "AdditionalPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Persons_DriverId",
                table: "Defects",
                column: "DriverId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Persons_ResponcePersonId",
                table: "Defects",
                column: "ResponcePersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Persons_SheffPassServiceId",
                table: "Defects",
                column: "SheffPassServiceId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_DefectsTOs_Persons_DriverId",
                table: "DefectsTOs",
                column: "DriverId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_DefectsTOs_Persons_ResponcePersonId",
                table: "DefectsTOs",
                column: "ResponcePersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_DefectsTOs_Persons_SheffPassServiceId",
                table: "DefectsTOs",
                column: "SheffPassServiceId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_Cars_CarId",
                table: "RepairRequests",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Persons_AcceptUtoPersonId",
                table: "Defects");

            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Persons_AdditionalPersonId",
                table: "Defects");

            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Persons_DriverId",
                table: "Defects");

            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Persons_ResponcePersonId",
                table: "Defects");

            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Persons_SheffPassServiceId",
                table: "Defects");

            migrationBuilder.DropForeignKey(
                name: "FK_DefectsTOs_Persons_DriverId",
                table: "DefectsTOs");

            migrationBuilder.DropForeignKey(
                name: "FK_DefectsTOs_Persons_ResponcePersonId",
                table: "DefectsTOs");

            migrationBuilder.DropForeignKey(
                name: "FK_DefectsTOs_Persons_SheffPassServiceId",
                table: "DefectsTOs");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_Cars_CarId",
                table: "RepairRequests");

            migrationBuilder.DropIndex(
                name: "IX_DefectsTOs_DriverId",
                table: "DefectsTOs");

            migrationBuilder.DropIndex(
                name: "IX_DefectsTOs_ResponcePersonId",
                table: "DefectsTOs");

            migrationBuilder.DropIndex(
                name: "IX_DefectsTOs_SheffPassServiceId",
                table: "DefectsTOs");

            migrationBuilder.DropIndex(
                name: "IX_Defects_AcceptUtoPersonId",
                table: "Defects");

            migrationBuilder.DropIndex(
                name: "IX_Defects_AdditionalPersonId",
                table: "Defects");

            migrationBuilder.DropIndex(
                name: "IX_Defects_DriverId",
                table: "Defects");

            migrationBuilder.DropIndex(
                name: "IX_Defects_ResponcePersonId",
                table: "Defects");

            migrationBuilder.DropIndex(
                name: "IX_Defects_SheffPassServiceId",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "DefectsTOs");

            migrationBuilder.DropColumn(
                name: "KmSpeedometrStart",
                table: "DefectsTOs");

            migrationBuilder.DropColumn(
                name: "MhSpeedStart",
                table: "DefectsTOs");

            migrationBuilder.DropColumn(
                name: "ResponcePersonId",
                table: "DefectsTOs");

            migrationBuilder.DropColumn(
                name: "SheffPassServiceId",
                table: "DefectsTOs");

            migrationBuilder.DropColumn(
                name: "AcceptUtoPersonId",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "AdditionalPersonId",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "KmSpeedStart",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "MhSpeedStart",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "ResponcePersonId",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "SheffPassServiceId",
                table: "Defects");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_Cars_CarId",
                table: "RepairRequests",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
