using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechHiringLinks.Migrations
{
    /// <inheritdoc />
    public partial class AddStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationStatus",
                columns: table => new
                {
                    ApplicationStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatus", x => x.ApplicationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "LinkApplicationStatus",
                columns: table => new
                {
                    LinkId = table.Column<int>(type: "int", nullable: false),
                    ApplicationStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkApplicationStatus", x => new { x.LinkId, x.ApplicationStatusId });
                    table.ForeignKey(
                        name: "FK_LinkApplicationStatus_ApplicationStatus_ApplicationStatusId",
                        column: x => x.ApplicationStatusId,
                        principalTable: "ApplicationStatus",
                        principalColumn: "ApplicationStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkApplicationStatus_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "LinkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkApplicationStatus_ApplicationStatusId",
                table: "LinkApplicationStatus",
                column: "ApplicationStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkApplicationStatus");

            migrationBuilder.DropTable(
                name: "ApplicationStatus");
        }
    }
}
