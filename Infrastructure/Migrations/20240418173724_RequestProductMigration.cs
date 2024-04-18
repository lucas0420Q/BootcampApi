using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequestProductMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "ApplicationDate",
                table: "Requests",
                newName: "RequestDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "Requests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "Requests",
                newName: "ApplicationDate");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Requests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
