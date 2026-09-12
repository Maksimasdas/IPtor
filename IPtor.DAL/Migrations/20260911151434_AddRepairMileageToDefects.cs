using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPtor.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRepairMileageToDefects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KmSpeedKarRepair",
                table: "Defects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KmSpeedTO",
                table: "Defects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "MhSpeedKapRepair",
                table: "Defects",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MhSpeedTO",
                table: "Defects",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KmSpeedKarRepair",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "KmSpeedTO",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "MhSpeedKapRepair",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "MhSpeedTO",
                table: "Defects");
        }
    }
}
