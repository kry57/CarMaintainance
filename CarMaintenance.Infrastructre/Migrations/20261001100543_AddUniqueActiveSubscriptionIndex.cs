using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMaintenance.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueActiveSubscriptionIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ProviderId",
                table: "Subscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ProviderId",
                table: "Subscriptions",
                column: "ProviderId",
                unique: true,
                filter: "[SubscriptionStatus] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ProviderId",
                table: "Subscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ProviderId",
                table: "Subscriptions",
                column: "ProviderId");
        }
    }
}
