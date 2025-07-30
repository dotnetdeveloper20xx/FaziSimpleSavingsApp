using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaziSimpleSavings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupTransactionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupGoalMembers_GroupSavingsGoals_GroupGoalId",
                table: "GroupGoalMembers");

            migrationBuilder.CreateTable(
                name: "GroupTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupGoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTransactions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupGoalMembers_GroupSavingsGoals_GroupGoalId",
                table: "GroupGoalMembers",
                column: "GroupGoalId",
                principalTable: "GroupSavingsGoals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupGoalMembers_GroupSavingsGoals_GroupGoalId",
                table: "GroupGoalMembers");

            migrationBuilder.DropTable(
                name: "GroupTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupGoalMembers_GroupSavingsGoals_GroupGoalId",
                table: "GroupGoalMembers",
                column: "GroupGoalId",
                principalTable: "GroupSavingsGoals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
