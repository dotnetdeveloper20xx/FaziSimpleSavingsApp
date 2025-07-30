using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaziSimpleSavings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupSavingsGoalEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupSavingsGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSaved = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSavingsGoals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupGoalMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupGoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContributedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupGoalMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupGoalMembers_GroupSavingsGoals_GroupGoalId",
                        column: x => x.GroupGoalId,
                        principalTable: "GroupSavingsGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupGoalMembers_GroupGoalId",
                table: "GroupGoalMembers",
                column: "GroupGoalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupGoalMembers");

            migrationBuilder.DropTable(
                name: "GroupSavingsGoals");
        }
    }
}
