using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KampusPortali.Migrations
{
    /// <inheritdoc />
    public partial class DersProgramiVeSinavlar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Kat",
                table: "Derslikler",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "Kapasite",
                table: "Derslikler",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Blok",
                table: "Derslikler",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Derslikler",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Derslikler",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "MekanId",
                table: "Derslikler",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Not",
                table: "Derslikler",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DerslikId",
                table: "Dersler",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AkademisyenId",
                table: "Dersler",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Dersler",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Grup",
                table: "Dersler",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Kod",
                table: "Dersler",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "LabMi",
                table: "Dersler",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Sinavlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DersKodu = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Tur = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Tarih = table.Column<DateOnly>(type: "date", nullable: false),
                    BaslangicSaati = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    BitisSaati = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    DerslikId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinavlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sinavlar_Derslikler_DerslikId",
                        column: x => x.DerslikId,
                        principalTable: "Derslikler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Dersler",
                columns: new[] { "Id", "AkademisyenId", "BaslangicSaati", "BitisSaati", "DersAdi", "DerslikId", "Grup", "Gun", "Kod", "LabMi" },
                values: new object[,]
                {
                    { 5, null, new TimeOnly(9, 30, 0), new TimeOnly(11, 20, 0), "COMPUTER PROGRAMMING I", null, "İngilizce 1. Sınıf", 4, "COM1007", true },
                    { 13, null, new TimeOnly(16, 30, 0), new TimeOnly(18, 20, 0), "COM2067 Laboratuvarı", null, "İngilizce 2. Sınıf", 2, "COM2067", true },
                    { 14, null, new TimeOnly(9, 30, 0), new TimeOnly(11, 20, 0), "COM2077 Laboratuvarı", null, "İngilizce 2. Sınıf", 3, "COM2077", true },
                    { 17, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "COM2043 Laboratuvarı", null, "İngilizce 2. Sınıf", 5, "COM2043", true },
                    { 23, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "COM3033 Laboratuvarı", null, "İngilizce 3. Sınıf", 4, "COM3033", true },
                    { 26, null, new TimeOnly(16, 30, 0), new TimeOnly(18, 20, 0), "COM3067 Laboratuvarı", null, "İngilizce 3. Sınıf", 5, "COM3067", true },
                    { 38, null, new TimeOnly(13, 30, 0), new TimeOnly(15, 20, 0), "Bilgisayar Programlama I", null, "Türkçe 1. Sınıf", 5, "BLM1007", true },
                    { 39, null, new TimeOnly(9, 30, 0), new TimeOnly(11, 20, 0), "BLM2077 Laboratuvarı", null, "Türkçe 2. Sınıf", 2, "BLM2077", true },
                    { 42, null, new TimeOnly(13, 30, 0), new TimeOnly(15, 20, 0), "BLM2043 Laboratuvarı", null, "Türkçe 2. Sınıf", 3, "BLM2043", true },
                    { 43, null, new TimeOnly(15, 30, 0), new TimeOnly(17, 20, 0), "BLM2067 Laboratuvarı", null, "Türkçe 2. Sınıf", 3, "BLM2067", true },
                    { 48, null, new TimeOnly(9, 30, 0), new TimeOnly(11, 20, 0), "BLM3023 Laboratuvarı", null, "Türkçe 3. Sınıf", 1, "BLM3023", true },
                    { 49, null, new TimeOnly(13, 30, 0), new TimeOnly(15, 20, 0), "BLM3033 Laboratuvarı", null, "Türkçe 3. Sınıf", 1, "BLM3033", true },
                    { 50, null, new TimeOnly(15, 30, 0), new TimeOnly(17, 20, 0), "BLM3067 Laboratuvarı", null, "Türkçe 3. Sınıf", 1, "BLM3067", true }
                });

            migrationBuilder.InsertData(
                table: "Derslikler",
                columns: new[] { "Id", "Ad", "Blok", "Kapasite", "Kat", "MekanId", "MusaitMi", "Not" },
                values: new object[,]
                {
                    { 1, "Derslik 1", null, null, null, 2, true, null },
                    { 2, "Derslik 2", null, null, null, 2, true, null },
                    { 3, "Derslik 3", null, null, null, 2, true, null },
                    { 4, "Derslik 4", null, null, null, 2, true, null },
                    { 5, "LAB 1", null, null, null, 2, true, null },
                    { 6, "LAB 3", null, null, null, 2, true, null },
                    { 7, "Android Dersliği", null, null, null, 2, true, null },
                    { 8, "Yapay Zeka Dersliği H1", null, null, null, 5, true, "Yapay Zeka Bölümü dersliği" },
                    { 9, "Yapay Zeka Dersliği J2", null, null, null, 5, true, "Yapay Zeka Bölümü dersliği" },
                    { 10, "BME2 Dersliği", null, null, null, 6, true, "Biyomedikal Mühendisliği Bölümü dersliği" },
                    { 11, "Yazılım Mühendisliği Dersliği 7", null, null, null, 3, true, null },
                    { 12, "D Blok Servis Lab", null, null, null, null, true, "Tandoğan Yerleşkesi" }
                });

            migrationBuilder.InsertData(
                table: "Dersler",
                columns: new[] { "Id", "AkademisyenId", "BaslangicSaati", "BitisSaati", "DersAdi", "DerslikId", "Grup", "Gun", "Kod", "LabMi" },
                values: new object[,]
                {
                    { 1, null, new TimeOnly(9, 30, 0), new TimeOnly(11, 20, 0), "COMPUTER PROGRAMMING I", 1, "İngilizce 1. Sınıf", 1, "COM1007", false },
                    { 2, null, new TimeOnly(13, 30, 0), new TimeOnly(17, 20, 0), "DEVELOPMENT OF READING AND WRITING SKILLS IN ENGLISH I", 4, "İngilizce 1. Sınıf", 1, "ENG101", false },
                    { 3, null, new TimeOnly(15, 30, 0), new TimeOnly(17, 20, 0), "PHYSICS I", 12, "İngilizce 1. Sınıf", 2, "PHY0105", true },
                    { 4, null, new TimeOnly(9, 30, 0), new TimeOnly(11, 20, 0), "COMPUTER PROGRAMMING I", 10, "İngilizce 1. Sınıf", 3, "COM1007", false },
                    { 6, null, new TimeOnly(15, 30, 0), new TimeOnly(18, 20, 0), "COM1015", 2, "İngilizce 1. Sınıf", 4, "COM1015", false },
                    { 7, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "PHYSICS I", 8, "İngilizce 1. Sınıf", 5, "PHY0105", false },
                    { 8, null, new TimeOnly(13, 30, 0), new TimeOnly(17, 20, 0), "CALCULUS I", 8, "İngilizce 1. Sınıf", 5, "MTH0143", false },
                    { 9, null, new TimeOnly(8, 30, 0), new TimeOnly(12, 20, 0), "ADVANCED READING AND COMMUNICATION TECHNIQUES IN ENGLISH I", 4, "İngilizce 2. Sınıf", 1, "ENG201", false },
                    { 10, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "COM2077", 1, "İngilizce 2. Sınıf", 1, "COM2077", false },
                    { 11, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "COM2039", 1, "İngilizce 2. Sınıf", 2, "COM2039", false },
                    { 12, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "COM2067", 2, "İngilizce 2. Sınıf", 2, "COM2067", false },
                    { 15, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "COM2501", 2, "İngilizce 2. Sınıf", 3, "COM2501", false },
                    { 16, null, new TimeOnly(15, 30, 0), new TimeOnly(18, 20, 0), "COM2043", 1, "İngilizce 2. Sınıf", 4, "COM2043", false },
                    { 18, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "FILE ORGANIZATION", 11, "İngilizce 2. Sınıf", 5, "COM2553", false },
                    { 19, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "COM3533", 3, "İngilizce 3. Sınıf", 1, "COM3533", false },
                    { 20, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "COM3023", 4, "İngilizce 3. Sınıf", 3, "COM3023", false },
                    { 21, null, new TimeOnly(14, 0, 0), new TimeOnly(16, 20, 0), "COM3033", 1, "İngilizce 3. Sınıf", 3, "COM3033", false },
                    { 22, null, new TimeOnly(10, 30, 0), new TimeOnly(12, 20, 0), "COM3073", 9, "İngilizce 3. Sınıf", 4, "COM3073", false },
                    { 24, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "COM3551", 1, "İngilizce 3. Sınıf", 5, "COM3551", false },
                    { 25, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "COM3067", 2, "İngilizce 3. Sınıf", 5, "COM3067", false },
                    { 27, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "INFORMATION SYSTEMS", 3, "İngilizce 4. Sınıf", 2, "COM4567", false },
                    { 28, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "COM4541", 4, "İngilizce 4. Sınıf", 2, "COM4541", false },
                    { 29, null, new TimeOnly(10, 0, 0), new TimeOnly(12, 20, 0), "ARTIFICIAL NEURAL NETWORKS", 4, "İngilizce 4. Sınıf", 4, "COM4573", false },
                    { 30, null, new TimeOnly(15, 30, 0), new TimeOnly(18, 20, 0), "DATA MINING", 4, "İngilizce 4. Sınıf", 4, "COM4569", false },
                    { 31, null, new TimeOnly(10, 30, 0), new TimeOnly(12, 20, 0), "Matematik I", 2, "Türkçe 1. Sınıf", 1, "MAT0143", false },
                    { 32, null, new TimeOnly(13, 30, 0), new TimeOnly(15, 20, 0), "Matematik I", 2, "Türkçe 1. Sınıf", 1, "MAT0143", false },
                    { 33, null, new TimeOnly(15, 30, 0), new TimeOnly(17, 20, 0), "Bilgisayar Programlama I", 2, "Türkçe 1. Sınıf", 1, "BLM1007", false },
                    { 34, null, new TimeOnly(9, 30, 0), new TimeOnly(11, 20, 0), "Bilgisayar Programlama I", 2, "Türkçe 1. Sınıf", 2, "BLM1007", false },
                    { 35, null, new TimeOnly(13, 30, 0), new TimeOnly(15, 20, 0), "Fizik I", 12, "Türkçe 1. Sınıf", 2, "FZM0105", true },
                    { 36, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "Fizik I", 9, "Türkçe 1. Sınıf", 3, "FZM0105", false },
                    { 37, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM1015", 1, "Türkçe 1. Sınıf", 4, "BLM1015", false },
                    { 40, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "BLM2501", 6, "Türkçe 2. Sınıf", 2, "BLM2501", false },
                    { 41, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM2043", 2, "Türkçe 2. Sınıf", 3, "BLM2043", false },
                    { 44, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM2067", 2, "Türkçe 2. Sınıf", 4, "BLM2067", false },
                    { 45, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "BLM2537", 1, "Türkçe 2. Sınıf", 4, "BLM2537", false },
                    { 46, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM2077", 2, "Türkçe 2. Sınıf", 5, "BLM2077", false },
                    { 47, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "BLM2039", 4, "Türkçe 2. Sınıf", 5, "BLM2039", false },
                    { 51, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM3557", 4, "Türkçe 3. Sınıf", 2, "BLM3557", false },
                    { 52, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "BLM3033", 1, "Türkçe 3. Sınıf", 2, "BLM3033", false },
                    { 53, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM3067", 1, "Türkçe 3. Sınıf", 3, "BLM3067", false },
                    { 54, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "BLM3533", 4, "Türkçe 3. Sınıf", 3, "BLM3533", false },
                    { 55, null, new TimeOnly(14, 30, 0), new TimeOnly(16, 20, 0), "BLM3073", 9, "Türkçe 3. Sınıf", 4, "BLM3073", false },
                    { 56, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM3551", 4, "Türkçe 3. Sınıf", 5, "BLM3551", false },
                    { 57, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "BLM3023", 1, "Türkçe 3. Sınıf", 5, "BLM3023", false },
                    { 58, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM4551", 7, "Türkçe 4. Sınıf", 1, "BLM4551", false },
                    { 59, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "Yapay Sinir Ağları", 3, "Türkçe 4. Sınıf", 1, "BLM4573", false },
                    { 60, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM4583", 5, "Türkçe 4. Sınıf", 2, "BLM4583", false },
                    { 61, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "BLM4541", 3, "Türkçe 4. Sınıf", 2, "BLM4541", false },
                    { 62, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "Ağ Tabanlı Teknolojiler ve Uygulamaları", 3, "Türkçe 4. Sınıf", 3, "BLM4581", false },
                    { 63, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "BLM4587", 3, "Türkçe 4. Sınıf", 3, "BLM4587", false },
                    { 64, null, new TimeOnly(9, 30, 0), new TimeOnly(12, 20, 0), "BLM4559", 3, "Türkçe 4. Sınıf", 5, "BLM4559", false },
                    { 65, null, new TimeOnly(13, 30, 0), new TimeOnly(16, 20, 0), "BLM4561", 3, "Türkçe 4. Sınıf", 5, "BLM4561", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Derslikler_MekanId",
                table: "Derslikler",
                column: "MekanId");

            migrationBuilder.CreateIndex(
                name: "IX_Dersler_Kod",
                table: "Dersler",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_Sinavlar_DersKodu",
                table: "Sinavlar",
                column: "DersKodu");

            migrationBuilder.CreateIndex(
                name: "IX_Sinavlar_DerslikId",
                table: "Sinavlar",
                column: "DerslikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Derslikler_Mekanlar_MekanId",
                table: "Derslikler",
                column: "MekanId",
                principalTable: "Mekanlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derslikler_Mekanlar_MekanId",
                table: "Derslikler");

            migrationBuilder.DropTable(
                name: "Sinavlar");

            migrationBuilder.DropIndex(
                name: "IX_Derslikler_MekanId",
                table: "Derslikler");

            migrationBuilder.DropIndex(
                name: "IX_Dersler_Kod",
                table: "Dersler");

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DropColumn(
                name: "MekanId",
                table: "Derslikler");

            migrationBuilder.DropColumn(
                name: "Not",
                table: "Derslikler");

            migrationBuilder.DropColumn(
                name: "Grup",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "Kod",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "LabMi",
                table: "Dersler");

            migrationBuilder.AlterColumn<string>(
                name: "Kat",
                table: "Derslikler",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Kapasite",
                table: "Derslikler",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Blok",
                table: "Derslikler",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Derslikler",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Derslikler",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "DerslikId",
                table: "Dersler",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AkademisyenId",
                table: "Dersler",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Dersler",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
