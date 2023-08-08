using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class _202308021001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CaseNumber",
                table: "Personnels",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_CaseNumber",
                table: "Personnels",
                column: "CaseNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Personnels_CaseNumber",
                table: "Personnels");

            migrationBuilder.AlterColumn<string>(
                name: "CaseNumber",
                table: "Personnels",
                type: "nvarchar(60)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
