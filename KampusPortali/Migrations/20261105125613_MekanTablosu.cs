using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KampusPortali.Migrations
{
    /// <inheritdoc />
    public partial class MekanTablosu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mekanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Tur = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Enlem = table.Column<double>(type: "double precision", nullable: false),
                    Boylam = table.Column<double>(type: "double precision", nullable: false),
                    Aciklama = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mekanlar", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mekanlar_Ad",
                table: "Mekanlar",
                column: "Ad",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mekanlar");
        }
    }
}
