using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarMaintenance.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "32482AA6-8A0F-4C69-90D8-E89ECCC8A770", 0, "2A3C06F8-D0A2-4781-9FD7-AA0B89B5172A", "admin@carmaintenance.com", true, "Admin-Carmaintenance", false, null, "ADMIN@CARMAINTENANCE.COM", "ADMIN@CARMAINTENANCE.COM", "AQAAAAIAAYagAAAAEErzVQAvwapwrKMhxa775lZf2e7817buxYxVbmyIspSEFF6q6ysZJ3IgQa1kKOPcuQ==", null, false, "E2BAF3A1EDB74ABB8E1D28E3FB795B18", false, "admin@carmaintenance.com" },
                    { "5B135CFF-86F3-450F-ABC3-92964C28D84E", 0, "46E47AA9-311E-4BE1-88D3-C2AFFCF4AB3F", "khaledyasser@carmaintenance.com", true, "Member-Carmaintenance", false, null, "KHALEDYASSER@CARMAINTENANCE.COM", "KHALEDYASSER@CARMAINTENANCE.COM", "AQAAAAIAAYagAAAAEEHRygUqwSxOFkS2qEh7omflNVybZPb7vzQO8iCZYLJ/a4BAQDGNja0yDwidVt6YgQ==", null, false, "DB18BC8995F8474D8EE7C3607E990339", false, "khaledyasser@carmaintenance.com" },
                    { "B6FA1294-88A6-446C-9AF3-3185D043FF2A", 0, "F2E6B77B-75E7-4055-BF5F-0A5403E34FC5", "bydprovider@carmaintenance.com", true, "Provider-Carmaintenance", false, null, "BYDPROVIDER@CARMAINTENANCE.COM", "BYDPROVIDER@CARMAINTENANCE.COM", "AQAAAAIAAYagAAAAEH/DPLFLkEjjawqOi2Rcu95a1Y40Nkts61ZH/HVduqSVkcdqpfG7zvflQxBNrR9M8A==", null, false, "0318D273BF28464984D3460300D57575", false, "bydprovider@carmaintenance.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32482AA6-8A0F-4C69-90D8-E89ECCC8A770");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B135CFF-86F3-450F-ABC3-92964C28D84E");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B6FA1294-88A6-446C-9AF3-3185D043FF2A");
        }
    }
}
