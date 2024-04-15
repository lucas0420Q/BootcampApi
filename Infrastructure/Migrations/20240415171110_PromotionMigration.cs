using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PromotionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccount_Accounts_AccountId",
                table: "CurrentAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Bank_BankId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CreditCards_CreditCardId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "Customers_pkey",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "CurrentAccount",
                newName: "CurrentAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CreditCardId",
                table: "Customer",
                newName: "IX_Customer_CreditCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BankId",
                table: "Customer",
                newName: "IX_Customer_BankId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentAccount_AccountId",
                table: "CurrentAccounts",
                newName: "IX_CurrentAccounts_AccountId");

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "Customer",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardId",
                table: "Customer",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationalLimit",
                table: "CurrentAccounts",
                type: "numeric(20,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldMaxLength: 400);

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthAverage",
                table: "CurrentAccounts",
                type: "numeric(20,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "CurrentAccounts",
                type: "numeric(10,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldMaxLength: 300);

            migrationBuilder.AddPrimaryKey(
                name: "Customer_pkey",
                table: "Customer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "businesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Business_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DurationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PercentageOff = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: true),
                    BusinessId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Promotion_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotions_businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_BusinessId",
                table: "Promotions",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customer_CustomerId",
                table: "Accounts",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Customer_CustomerId",
                table: "CreditCards",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Accounts_AccountId",
                table: "CurrentAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Bank_BankId",
                table: "Customer",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CreditCards_CreditCardId",
                table: "Customer",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customer_CustomerId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Customer_CustomerId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Accounts_AccountId",
                table: "CurrentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Bank_BankId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CreditCards_CreditCardId",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "businesses");

            migrationBuilder.DropPrimaryKey(
                name: "Customer_pkey",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "CurrentAccounts",
                newName: "CurrentAccount");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_CreditCardId",
                table: "Customers",
                newName: "IX_Customers_CreditCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_BankId",
                table: "Customers",
                newName: "IX_Customers_BankId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentAccounts_AccountId",
                table: "CurrentAccount",
                newName: "IX_CurrentAccount_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "Customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardId",
                table: "Customers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationalLimit",
                table: "CurrentAccount",
                type: "numeric",
                maxLength: 400,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthAverage",
                table: "CurrentAccount",
                type: "numeric",
                maxLength: 100,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "CurrentAccount",
                type: "numeric",
                maxLength: 300,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "Customers_pkey",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerId",
                table: "Accounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccount_Accounts_AccountId",
                table: "CurrentAccount",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Bank_BankId",
                table: "Customers",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CreditCards_CreditCardId",
                table: "Customers",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id");
        }
    }
}
