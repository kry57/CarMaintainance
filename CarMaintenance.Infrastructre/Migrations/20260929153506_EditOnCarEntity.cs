using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMaintenance.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class EditOnCarEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Cars");
        }
    }
}
