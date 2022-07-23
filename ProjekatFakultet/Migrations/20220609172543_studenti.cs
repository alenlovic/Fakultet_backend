using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjekatFakultet.Migrations
{
    public partial class studenti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    BrojIndeksa = table.Column<int>(type: "int", nullable: false),
                    GodinaStudija = table.Column<int>(type: "int", nullable: false),
                    Smjer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProsjekOcjena = table.Column<double>(type: "float", nullable: false),
                    RedovnaNastava = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Studenti");
        }
    }
}
