using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarMaintenance.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B135CFF-86F3-450F-ABC3-92964C28D84E");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC", "F4FA1400-6AF3-4E5A-8F25-88CA5C3C3006", false, false, "Provider", "PROVIDER" },
                    { "352AB560-5ADB-4A2A-A768-E206124E8F70", "63557FE8-153A-4E20-B2CD-44FDDE734E10", true, false, "Member", "MEMBER" },
                    { "A5066782-F090-4C8E-9372-77BB4F6A5D2D", "3BB4708A-19D7-4C76-BD01-D5109374B291", false, false, "Admin", "ADMIN" }
                });

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

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permissions", "Permissions", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 2, "Permissions", "account:view", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 3, "Permissions", "account:update", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 4, "Permissions", "account:change-password", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 5, "Permissions", "cars:view", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 6, "Permissions", "cars:add", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 7, "Permissions", "cars:update", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 8, "Permissions", "cars:delete", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 9, "Permissions", "cars:toggle-status", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 10, "Permissions", "diagnosis:use", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 11, "Permissions", "products:view", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 12, "Permissions", "products:add", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 13, "Permissions", "products:update", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 14, "Permissions", "products:delete", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 15, "Permissions", "products:toggle-status", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 16, "Permissions", "product-images:view", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 17, "Permissions", "product-images:add", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 18, "Permissions", "product-images:delete", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 19, "Permissions", "providers:view", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 20, "Permissions", "providers:add", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 21, "Permissions", "providers:update", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 22, "Permissions", "providers:delete", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 23, "Permissions", "providers:toggle-status", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 24, "Permissions", "providers:verify", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 25, "Permissions", "registration-requests:view-provider", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 26, "Permissions", "registration-requests:respond", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 27, "Permissions", "reviews:view", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 28, "Permissions", "reviews:add", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 29, "Permissions", "reviews:update", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 30, "Permissions", "reviews:delete", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 31, "Permissions", "reviews:toggle-status", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 32, "Permissions", "subscriptions:view", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 33, "Permissions", "subscriptions:add", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 34, "Permissions", "subscriptions:cancel", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 35, "Permissions", "subscriptions:renew", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 36, "Permissions", "subscriptions:upgrade", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 37, "Permissions", "subscriptions:downgrade", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 38, "Permissions", "users:view", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 39, "Permissions", "users:add", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 40, "Permissions", "users:update", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 41, "Permissions", "roles:view", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 42, "Permissions", "roles:add", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 43, "Permissions", "roles:update", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 44, "Permissions", "roles:delete", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 45, "Permissions", "roles:toggle-status", "A5066782-F090-4C8E-9372-77BB4F6A5D2D" },
                    { 46, "Permissions", "products:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 47, "Permissions", "products:add", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 48, "Permissions", "products:update", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 49, "Permissions", "products:delete", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 50, "Permissions", "products:toggle-status", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 51, "Permissions", "product-images:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 52, "Permissions", "product-images:add", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 53, "Permissions", "product-images:delete", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 54, "Permissions", "providers:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 55, "Permissions", "providers:update", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 56, "Permissions", "registration-requests:view-provider", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 57, "Permissions", "registration-requests:respond", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 58, "Permissions", "subscriptions:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 59, "Permissions", "subscriptions:add", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 60, "Permissions", "subscriptions:cancel", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 61, "Permissions", "subscriptions:renew", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 62, "Permissions", "subscriptions:upgrade", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 63, "Permissions", "subscriptions:downgrade", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 64, "Permissions", "reviews:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 65, "Permissions", "cars:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 66, "Permissions", "cars:add", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 67, "Permissions", "cars:update", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 68, "Permissions", "cars:delete", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 69, "Permissions", "cars:toggle-status", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 70, "Permissions", "diagnosis:use", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 71, "Permissions", "reviews:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 72, "Permissions", "reviews:add", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 73, "Permissions", "reviews:update", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 74, "Permissions", "reviews:delete", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 75, "Permissions", "products:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 76, "Permissions", "product-images:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" },
                    { 77, "Permissions", "providers:view", "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "A5066782-F090-4C8E-9372-77BB4F6A5D2D", "32482AA6-8A0F-4C69-90D8-E89ECCC8A770" },
                    { "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC", "B6FA1294-88A6-446C-9AF3-3185D043FF2A" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "352AB560-5ADB-4A2A-A768-E206124E8F70");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "A5066782-F090-4C8E-9372-77BB4F6A5D2D", "32482AA6-8A0F-4C69-90D8-E89ECCC8A770" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC", "B6FA1294-88A6-446C-9AF3-3185D043FF2A" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A5066782-F090-4C8E-9372-77BB4F6A5D2D");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32482AA6-8A0F-4C69-90D8-E89ECCC8A770",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEErzVQAvwapwrKMhxa775lZf2e7817buxYxVbmyIspSEFF6q6ysZJ3IgQa1kKOPcuQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B6FA1294-88A6-446C-9AF3-3185D043FF2A",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEH/DPLFLkEjjawqOi2Rcu95a1Y40Nkts61ZH/HVduqSVkcdqpfG7zvflQxBNrR9M8A==");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5B135CFF-86F3-450F-ABC3-92964C28D84E", 0, "46E47AA9-311E-4BE1-88D3-C2AFFCF4AB3F", "khaledyasser@carmaintenance.com", true, "Member-Carmaintenance", false, null, "KHALEDYASSER@CARMAINTENANCE.COM", "KHALEDYASSER@CARMAINTENANCE.COM", "AQAAAAIAAYagAAAAEEHRygUqwSxOFkS2qEh7omflNVybZPb7vzQO8iCZYLJ/a4BAQDGNja0yDwidVt6YgQ==", null, false, "DB18BC8995F8474D8EE7C3607E990339", false, "khaledyasser@carmaintenance.com" });
        }
    }
}
