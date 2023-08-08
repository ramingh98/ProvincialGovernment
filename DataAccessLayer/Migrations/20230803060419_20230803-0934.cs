using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class _202308030934 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CaseNumber = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseHistory_CaseStatus_CaseStatusId",
                        column: x => x.CaseStatusId,
                        principalTable: "CaseStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistory_CaseStatusId",
                table: "CaseHistory",
                column: "CaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistory_Id",
                table: "CaseHistory",
                column: "Id");
        }
    }
}
