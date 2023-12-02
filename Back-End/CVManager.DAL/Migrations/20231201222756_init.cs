using Microsoft.EntityFrameworkCore.Migrations;

namespace CVManager.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cvs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PersonalInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cvs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cvs_PersonalInformation_PersonalInformationId",
                        column: x => x.PersonalInformationId,
                        principalTable: "PersonalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyField = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CvId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceInformation_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cvs_PersonalInformationId",
                table: "Cvs",
                column: "PersonalInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceInformation_CvId",
                table: "ExperienceInformation",
                column: "CvId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperienceInformation");

            migrationBuilder.DropTable(
                name: "Cvs");

            migrationBuilder.DropTable(
                name: "PersonalInformation");
        }
    }
}
