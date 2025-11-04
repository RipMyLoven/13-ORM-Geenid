using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _13_ORM_Geenid.Migrations
{
    /// <inheritdoc />
    public partial class NewMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alleelid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimetus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Positiivne = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alleelid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Geenid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alleel1Id = table.Column<int>(type: "int", nullable: false),
                    Alleel2Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geenid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Geenid_Alleelid_Alleel1Id",
                        column: x => x.Alleel1Id,
                        principalTable: "Alleelid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Geenid_Alleelid_Alleel2Id",
                        column: x => x.Alleel2Id,
                        principalTable: "Alleelid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alleelid_Nimetus",
                table: "Alleelid",
                column: "Nimetus");

            migrationBuilder.CreateIndex(
                name: "IX_Geenid_Alleel1Id",
                table: "Geenid",
                column: "Alleel1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Geenid_Alleel2Id",
                table: "Geenid",
                column: "Alleel2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Geenid");

            migrationBuilder.DropTable(
                name: "Alleelid");
        }
    }
}
