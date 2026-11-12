using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KampusPortali.Migrations
{
    /// <inheritdoc />
    public partial class KutuphaneRezervasyonlari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KutuphaneMasalari_Kullanicilar_RezerveEdenKullaniciId",
                table: "KutuphaneMasalari");

            migrationBuilder.DropIndex(
                name: "IX_KutuphaneMasalari_RezerveEdenKullaniciId",
                table: "KutuphaneMasalari");

            migrationBuilder.DropColumn(
                name: "DoluMu",
                table: "KutuphaneMasalari");

            migrationBuilder.DropColumn(
                name: "RezervasyonBitis",
                table: "KutuphaneMasalari");

            migrationBuilder.DropColumn(
                name: "RezerveEdenKullaniciId",
                table: "KutuphaneMasalari");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "KutuphaneMasalari",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "MasaRezervasyonlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MasaId = table.Column<int>(type: "integer", nullable: false),
                    KullaniciId = table.Column<int>(type: "integer", nullable: false),
                    Baslangic = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Bitis = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IptalEdildiMi = table.Column<bool>(type: "boolean", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasaRezervasyonlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasaRezervasyonlari_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasaRezervasyonlari_KutuphaneMasalari_MasaId",
                        column: x => x.MasaId,
                        principalTable: "KutuphaneMasalari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KutuphaneMasalari",
                columns: new[] { "Id", "Kapasite", "Kat", "MasaNo", "PrizVarMi" },
                values: new object[,]
                {
                    { 1, 1, 0, "Z-01", false },
                    { 2, 1, 0, "Z-02", true },
                    { 3, 1, 0, "Z-03", false },
                    { 4, 1, 0, "Z-04", true },
                    { 5, 1, 0, "Z-05", false },
                    { 6, 1, 0, "Z-06", true },
                    { 7, 1, 0, "Z-07", false },
                    { 8, 1, 0, "Z-08", true },
                    { 9, 1, 0, "Z-09", false },
                    { 10, 1, 0, "Z-10", true },
                    { 11, 1, 0, "Z-11", false },
                    { 12, 1, 0, "Z-12", true },
                    { 13, 4, 0, "Z-13", true },
                    { 14, 4, 0, "Z-14", true },
                    { 15, 4, 0, "Z-15", true },
                    { 16, 4, 0, "Z-16", true },
                    { 17, 1, 1, "1-01", true },
                    { 18, 1, 1, "1-02", true },
                    { 19, 1, 1, "1-03", true },
                    { 20, 1, 1, "1-04", true },
                    { 21, 1, 1, "1-05", true },
                    { 22, 1, 1, "1-06", true },
                    { 23, 1, 1, "1-07", true },
                    { 24, 1, 1, "1-08", true },
                    { 25, 1, 1, "1-09", true },
                    { 26, 1, 1, "1-10", true },
                    { 27, 1, 1, "1-11", false },
                    { 28, 1, 1, "1-12", false },
                    { 29, 1, 1, "1-13", false },
                    { 30, 1, 1, "1-14", false },
                    { 31, 1, 1, "1-15", false },
                    { 32, 1, 1, "1-16", false },
                    { 33, 1, 1, "1-17", false },
                    { 34, 1, 1, "1-18", false },
                    { 35, 1, 1, "1-19", false },
                    { 36, 1, 1, "1-20", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasaRezervasyonlari_KullaniciId",
                table: "MasaRezervasyonlari",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_MasaRezervasyonlari_MasaId_Baslangic_Bitis",
                table: "MasaRezervasyonlari",
                columns: new[] { "MasaId", "Baslangic", "Bitis" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasaRezervasyonlari");

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "KutuphaneMasalari",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "KutuphaneMasalari",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "DoluMu",
                table: "KutuphaneMasalari",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RezervasyonBitis",
                table: "KutuphaneMasalari",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RezerveEdenKullaniciId",
                table: "KutuphaneMasalari",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KutuphaneMasalari_RezerveEdenKullaniciId",
                table: "KutuphaneMasalari",
                column: "RezerveEdenKullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_KutuphaneMasalari_Kullanicilar_RezerveEdenKullaniciId",
                table: "KutuphaneMasalari",
                column: "RezerveEdenKullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
