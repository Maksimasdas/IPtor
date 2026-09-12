using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPtor.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRepairCountsToDefects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvarageRepair",
                table: "Defects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KapRepair",
                table: "Defects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvarageRepair",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "KapRepair",
                table: "Defects");
        }
    }
}
