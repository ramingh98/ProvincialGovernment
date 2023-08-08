using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class _202307291000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TitleEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDegree",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDegree", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    TitleEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLocation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyField",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfEmployment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfEmployment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfPosition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfPosition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Family = table.Column<string>(type: "nvarchar(70)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    BirthCertificateNumber = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    CaseNumber = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    ComputerCode = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TypeOfEmploymentId = table.Column<long>(type: "bigint", nullable: false),
                    EducationDegreeId = table.Column<long>(type: "bigint", nullable: false),
                    StudyFieldId = table.Column<long>(type: "bigint", nullable: false),
                    LastPositionId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceLocationId = table.Column<long>(type: "bigint", nullable: false),
                    MaritalStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CaseStatusId = table.Column<long>(type: "bigint", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnels_CaseStatus_CaseStatusId",
                        column: x => x.CaseStatusId,
                        principalTable: "CaseStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_EducationDegree_EducationDegreeId",
                        column: x => x.EducationDegreeId,
                        principalTable: "EducationDegree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_MaritalStatus_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_ServiceLocation_ServiceLocationId",
                        column: x => x.ServiceLocationId,
                        principalTable: "ServiceLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_StudyField_StudyFieldId",
                        column: x => x.StudyFieldId,
                        principalTable: "StudyField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_TypeOfEmployment_TypeOfEmploymentId",
                        column: x => x.TypeOfEmploymentId,
                        principalTable: "TypeOfEmployment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_TypeOfPosition_LastPositionId",
                        column: x => x.LastPositionId,
                        principalTable: "TypeOfPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseStatus_Id",
                table: "CaseStatus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EducationDegree_Id",
                table: "EducationDegree",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatus_Id",
                table: "MaritalStatus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_CaseStatusId",
                table: "Personnels",
                column: "CaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_EducationDegreeId",
                table: "Personnels",
                column: "EducationDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_LastPositionId",
                table: "Personnels",
                column: "LastPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_MaritalStatusId",
                table: "Personnels",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_NationalCode",
                table: "Personnels",
                column: "NationalCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_ServiceLocationId",
                table: "Personnels",
                column: "ServiceLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_StudyFieldId",
                table: "Personnels",
                column: "StudyFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_TypeOfEmploymentId",
                table: "Personnels",
                column: "TypeOfEmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceLocation_Id",
                table: "ServiceLocation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudyField_Id",
                table: "StudyField",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfEmployment_Id",
                table: "TypeOfEmployment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfPosition_Id",
                table: "TypeOfPosition",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personnels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CaseStatus");

            migrationBuilder.DropTable(
                name: "EducationDegree");

            migrationBuilder.DropTable(
                name: "MaritalStatus");

            migrationBuilder.DropTable(
                name: "ServiceLocation");

            migrationBuilder.DropTable(
                name: "StudyField");

            migrationBuilder.DropTable(
                name: "TypeOfEmployment");

            migrationBuilder.DropTable(
                name: "TypeOfPosition");
        }
    }
}
