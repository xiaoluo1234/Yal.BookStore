using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yal.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class jiangji_net_version : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpSessions");

            migrationBuilder.DropIndex(
                name: "IX_AbpTenants_NormalizedName",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AbpRoles");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AbpClaimTypes");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "OpenIddictTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "OpenIddictTokens",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "OpenIddictTokens",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "OpenIddictTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OpenIddictTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "OpenIddictTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "OpenIddictTokens",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "OpenIddictAuthorizations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "OpenIddictAuthorizations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "OpenIddictAuthorizations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "OpenIddictAuthorizations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OpenIddictAuthorizations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "OpenIddictAuthorizations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "OpenIddictAuthorizations",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "OpenIddictAuthorizations");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "AbpTenants",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AbpRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AbpClaimTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AbpSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Device = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DeviceInfo = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    IpAddresses = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SessionId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SignedIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSessions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_NormalizedName",
                table: "AbpTenants",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSessions_Device",
                table: "AbpSessions",
                column: "Device");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSessions_SessionId",
                table: "AbpSessions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSessions_TenantId_UserId",
                table: "AbpSessions",
                columns: new[] { "TenantId", "UserId" });
        }
    }
}
