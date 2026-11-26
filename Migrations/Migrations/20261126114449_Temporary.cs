using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.Migrations;

/// <inheritdoc />
public partial class _20261126114449_Temporary : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "Tenants",
            type: "character varying(256)",
            maxLength: 256,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Initials",
            table: "AspNetUsers",
            type: "character(2)",
            fixedLength: true,
            maxLength: 2,
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "TenantId",
            table: "AspNetUsers",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Name",
            table: "Tenants");

        migrationBuilder.DropColumn(
            name: "Initials",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "TenantId",
            table: "AspNetUsers");
    }
}
