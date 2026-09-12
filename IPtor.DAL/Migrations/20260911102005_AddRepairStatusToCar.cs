using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPtor.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRepairStatusToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUnderRepair",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RepairReason",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUnderRepair",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RepairReason",
                table: "Cars");
        }
    }
}
