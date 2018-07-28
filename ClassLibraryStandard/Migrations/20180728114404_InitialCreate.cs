using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassLibraryStandard.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "detalle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 25, nullable: true),
                    IdMaster = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detalle_master_IdMaster",
                        column: x => x.IdMaster,
                        principalTable: "master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalle_IdMaster",
                table: "detalle",
                column: "IdMaster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalle");

            migrationBuilder.DropTable(
                name: "master");
        }
    }
}
