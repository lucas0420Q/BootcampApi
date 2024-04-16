using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PromotionEnterpriseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customer_CustomerId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Customer_CustomerId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Currency_CreditCards_CreditCardId",
                table: "Currency");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Bank_BankId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CreditCards_CreditCardId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_businesses_BusinessId",
                table: "Promotions");

            migrationBuilder.DropTable(
                name: "businesses");

            migrationBuilder.DropPrimaryKey(
                name: "Promotion_pkey",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_BusinessId",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Currency_CreditCardId",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CreditCardId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreditCardId",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Promotions",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "Cvv",
                table: "CreditCards",
                newName: "CVV");

            migrationBuilder.RenameColumn(
                name: "AvailableCredit",
                table: "CreditCards",
                newName: "AvaibleCredit");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_BankId",
                table: "Customers",
                newName: "IX_Customers_BankId");

            migrationBuilder.AlterColumn<string>(
                name: "PercentageOff",
                table: "Promotions",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,5)",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Promotions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DurationTime",
                table: "Promotions",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Promotions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Promotions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionEnterprises",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "integer", nullable: false),
                    EnterpriseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionEnterprises", x => new { x.PromotionId, x.EnterpriseId });
                    table.ForeignKey(
                        name: "FK_PromotionEnterprises_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionEnterprises_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionEnterprises_EnterpriseId",
                table: "PromotionEnterprises",
                column: "EnterpriseId");

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
                name: "FK_Customers_Bank_BankId",
                table: "Customers",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Bank_BankId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "PromotionEnterprises");

            migrationBuilder.DropTable(
                name: "Enterprises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Promotions");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Promotions",
                newName: "BusinessId");

            migrationBuilder.RenameColumn(
                name: "CVV",
                table: "CreditCards",
                newName: "Cvv");

            migrationBuilder.RenameColumn(
                name: "AvaibleCredit",
                table: "CreditCards",
                newName: "AvailableCredit");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BankId",
                table: "Customer",
                newName: "IX_Customer_BankId");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageOff",
                table: "Promotions",
                type: "numeric(20,5)",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Promotions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DurationTime",
                table: "Promotions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditCardId",
                table: "Currency",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customer",
                type: "text",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Customer",
                type: "text",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customer",
                type: "text",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "Promotion_pkey",
                table: "Promotions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "businesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Business_pkey", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_BusinessId",
                table: "Promotions",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_CreditCardId",
                table: "Currency",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CreditCardId",
                table: "Customer",
                column: "CreditCardId");

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
                name: "FK_Currency_CreditCards_CreditCardId",
                table: "Currency",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_businesses_BusinessId",
                table: "Promotions",
                column: "BusinessId",
                principalTable: "businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
