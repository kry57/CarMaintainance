using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMaintenance.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32482AA6-8A0F-4C69-90D8-E89ECCC8A770",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEA2FOQ15UfM60fGLSlLCbMm0WZJE15VGafGBtQQKFx0FYLMe9ExpWE73eheiqk+xGw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B6FA1294-88A6-446C-9AF3-3185D043FF2A",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEA2FOQ15UfM60fGLSlLCbMm0WZJE15VGafGBtQQKFx0FYLMe9ExpWE73eheiqk+xGw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32482AA6-8A0F-4C69-90D8-E89ECCC8A770",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEB/NLlElb8eZUq6Sy1MUQCIbWMlt4xv3/T2DpvSw42VzhwQtWklI+0qBN491HZ55iQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B6FA1294-88A6-446C-9AF3-3185D043FF2A",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBuWFzt+KUPEJuDroIJifui2f9ft5FXdslM6ykfg479wAn4b1uMAoCg6lZqnkYUgCg==");
        }
    }
}
