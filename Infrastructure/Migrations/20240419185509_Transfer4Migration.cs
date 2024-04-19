using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Transfer4Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_Accounts_AccountId",
                table: "Movement");

            migrationBuilder.DropPrimaryKey(
                name: "Movemen_pkey",
                table: "Movement");

            migrationBuilder.DropIndex(
                name: "IX_Movement_AccountId",
                table: "Movement");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Movement");

            migrationBuilder.RenameTable(
                name: "Movement",
                newName: "Movements");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Movements",
                newName: "OriginalAccountId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransferredDateTime",
                table: "Movements",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Movements",
                type: "numeric(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movements",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DestinationAccountId",
                table: "Movements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementType",
                table: "Movements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "Movements_pkey",
                table: "Movements",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_DestinationAccountId",
                table: "Movements",
                column: "DestinationAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Accounts_DestinationAccountId",
                table: "Movements",
                column: "DestinationAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Accounts_DestinationAccountId",
                table: "Movements");

            migrationBuilder.DropPrimaryKey(
                name: "Movements_pkey",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_DestinationAccountId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "DestinationAccountId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "MovementType",
                table: "Movements");

            migrationBuilder.RenameTable(
                name: "Movements",
                newName: "Movement");

            migrationBuilder.RenameColumn(
                name: "OriginalAccountId",
                table: "Movement",
                newName: "AccountId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransferredDateTime",
                table: "Movement",
                type: "timestamp with time zone",
                maxLength: 300,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Movement",
                type: "numeric",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Movement",
                type: "character varying(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "Movemen_pkey",
                table: "Movement",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_AccountId",
                table: "Movement",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_Accounts_AccountId",
                table: "Movement",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
