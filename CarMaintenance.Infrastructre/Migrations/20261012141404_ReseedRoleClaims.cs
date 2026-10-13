using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMaintenance.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class ReseedRoleClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        INSERT INTO [dbo].[AspNetRoleClaims]
            ([RoleId], [ClaimType], [ClaimValue])
        SELECT r.[Id], N'Permissions', p.[ClaimValue]
        FROM [dbo].[AspNetRoles] AS r
        CROSS JOIN (VALUES
            (N'account:view'),
            (N'account:update'),
            (N'account:change-password'),
            (N'cars:view'),
            (N'cars:add'),
            (N'cars:update'),
            (N'cars:delete'),
            (N'cars:toggle-status'),
            (N'diagnosis:use'),
            (N'products:view'),
            (N'products:add'),
            (N'products:update'),
            (N'products:delete'),
            (N'products:toggle-status'),
            (N'product-images:view'),
            (N'product-images:add'),
            (N'product-images:delete'),
            (N'providers:view'),
            (N'providers:add'),
            (N'providers:update'),
            (N'providers:delete'),
            (N'providers:toggle-status'),
            (N'providers:verify'),
            (N'registration-requests:view-provider'),
            (N'registration-requests:respond'),
            (N'reviews:view'),
            (N'reviews:add'),
            (N'reviews:update'),
            (N'reviews:delete'),
            (N'reviews:toggle-status'),
            (N'subscriptions:view'),
            (N'subscriptions:add'),
            (N'subscriptions:cancel'),
            (N'subscriptions:renew'),
            (N'subscriptions:upgrade'),
            (N'subscriptions:downgrade'),
            (N'users:view'),
            (N'users:add'),
            (N'users:update'),
            (N'roles:view'),
            (N'roles:add'),
            (N'roles:update'),
            (N'roles:delete'),
            (N'roles:toggle-status')
        ) AS p([ClaimValue])
        WHERE r.[Name] = N'Admin';

        INSERT INTO [dbo].[AspNetRoleClaims]
            ([RoleId], [ClaimType], [ClaimValue])
        SELECT r.[Id], N'Permissions', p.[ClaimValue]
        FROM [dbo].[AspNetRoles] AS r
        CROSS JOIN (VALUES
            (N'products:view'),
            (N'products:add'),
            (N'products:update'),
            (N'products:delete'),
            (N'products:toggle-status'),
            (N'product-images:view'),
            (N'product-images:add'),
            (N'product-images:delete'),
            (N'providers:view'),
            (N'providers:update'),
            (N'registration-requests:view-provider'),
            (N'registration-requests:respond'),
            (N'subscriptions:view'),
            (N'subscriptions:add'),
            (N'subscriptions:cancel'),
            (N'subscriptions:renew'),
            (N'subscriptions:upgrade'),
            (N'subscriptions:downgrade'),
            (N'reviews:view')
        ) AS p([ClaimValue])
        WHERE r.[Name] = N'Provider';

        INSERT INTO [dbo].[AspNetRoleClaims]
            ([RoleId], [ClaimType], [ClaimValue])
        SELECT r.[Id], N'Permissions', p.[ClaimValue]
        FROM [dbo].[AspNetRoles] AS r
        CROSS JOIN (VALUES
            (N'cars:view'),
            (N'cars:add'),
            (N'cars:update'),
            (N'cars:delete'),
            (N'cars:toggle-status'),
            (N'diagnosis:use'),
            (N'reviews:view'),
            (N'reviews:add'),
            (N'reviews:update'),
            (N'reviews:delete'),
            (N'products:view'),
            (N'product-images:view'),
            (N'providers:view')
        ) AS p([ClaimValue])
        WHERE r.[Name] = N'Member';
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        DELETE FROM [dbo].[AspNetRoleClaims]
        WHERE [ClaimType] = N'Permissions';
    ");
        }
    }
}
