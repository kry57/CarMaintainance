using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMaintenance.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class AddPendingPlanName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PendingPlanName",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PendingPlanName",
                table: "Subscriptions");
        }
    }
}
