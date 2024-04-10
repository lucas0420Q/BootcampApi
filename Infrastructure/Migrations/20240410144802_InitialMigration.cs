using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    Phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mail = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Address = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Bank_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Holder = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Account_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperationalLimit = table.Column<decimal>(type: "numeric", maxLength: 400, nullable: false),
                    MonthAverage = table.Column<decimal>(type: "numeric", maxLength: 100, nullable: false),
                    Interest = table.Column<decimal>(type: "numeric", maxLength: 300, nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CurrentAccount_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentAccount_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Destination = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", maxLength: 100, nullable: false),
                    TransferredDateTime = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 300, nullable: false),
                    TransferStatus = table.Column<int>(type: "integer", maxLength: 150, nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Movemen_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movement_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavingAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SavingType = table.Column<int>(type: "integer", nullable: false),
                    HolderName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SavingAccount_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingAccounts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 100, nullable: false),
                    ExpirationDT = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 100, nullable: false),
                    CardNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Cvv = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    CreditCardStatus = table.Column<int>(type: "integer", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    AvailableCredit = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    CurrentDebt = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CreditCard_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BuyValue = table.Column<decimal>(type: "numeric(20,5)", nullable: false),
                    SellValue = table.Column<decimal>(type: "numeric(20,5)", nullable: false),
                    CreditCardId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Currency_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currency_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(type: "text", precision: 20, scale: 5, nullable: true),
                    DocumentNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "text", precision: 20, scale: 5, nullable: true),
                    Mail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "text", precision: 20, scale: 5, nullable: true),
                    CustomerStatus = table.Column<int>(type: "integer", nullable: false),
                    BankId = table.Column<int>(type: "integer", nullable: false),
                    Birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreditCardId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Customers_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CurrencyId",
                table: "CreditCards",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CustomerId",
                table: "CreditCards",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_CreditCardId",
                table: "Currency",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccount_AccountId",
                table: "CurrentAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BankId",
                table: "Customers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreditCardId",
                table: "Customers",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_AccountId",
                table: "Movement",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Currency_CurrencyId",
                table: "Accounts",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerId",
                table: "Accounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Currency_CurrencyId",
                table: "CreditCards",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Currency_CurrencyId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards");

            migrationBuilder.DropTable(
                name: "CurrentAccount");

            migrationBuilder.DropTable(
                name: "Movement");

            migrationBuilder.DropTable(
                name: "SavingAccounts");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "CreditCards");
        }
    }
}
