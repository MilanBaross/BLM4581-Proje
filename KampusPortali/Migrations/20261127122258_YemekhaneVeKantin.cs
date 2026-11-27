using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KampusPortali.Migrations
{
    /// <inheritdoc />
    public partial class YemekhaneVeKantin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Etkinlikler",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "MekanId",
                table: "Etkinlikler",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KantinUrunleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MekanId = table.Column<int>(type: "integer", nullable: false),
                    Ad = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Kategori = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Fiyat = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    MevcutMu = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KantinUrunleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KantinUrunleri_Mekanlar_MekanId",
                        column: x => x.MekanId,
                        principalTable: "Mekanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YemekMenuleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tarih = table.Column<DateOnly>(type: "date", nullable: false),
                    Corba = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    AnaYemek = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    YanYemek = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Tatli = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    VejetaryenSecenek = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Kalori = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekMenuleri", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Etkinlikler",
                columns: new[] { "Id", "Aciklama", "Baslik", "Konum", "MekanId", "Tarih", "ToplulukAdi" },
                values: new object[,]
                {
                    { 1, "Python ile ilk makine öğrenmesi modelimizi birlikte kuruyoruz. Bilgisayarını getirmen yeterli, ön bilgi gerekmiyor.", "Yapay Zekâya Giriş Atölyesi", "Bilgisayar Mühendisliği Bölümü", 2, new DateTime(2026, 11, 18, 11, 0, 0, 0, DateTimeKind.Utc), "Yazılım Topluluğu" },
                    { 2, "Gölbaşı kampüsünde sonbahar fotoğrafları çekiyoruz. Telefon kamerası da olur. Buluşma noktası Dekanlık önü.", "Kampüs Fotoğraf Yürüyüşü", "Mühendislik Fakültesi Dekanlığı önü", 9, new DateTime(2026, 11, 21, 10, 30, 0, 0, DateTimeKind.Utc), "Fotoğrafçılık Kulübü" },
                    { 3, "İsviçre sistemi 5 tur, 15+5 tempo. Kayıtlar etkinlik günü 13.30'a kadar alınır. İlk üçe kitap hediyesi var.", "Satranç Turnuvası", "Point Cafe", 12, new DateTime(2026, 12, 2, 11, 0, 0, 0, DateTimeKind.Utc), "Satranç Kulübü" },
                    { 4, "Dal açma, PR, merge ve çakışma çözme. Dönem projesi yapan herkes için pratik bir oturum.", "Git ve GitHub Semineri", "Yazılım Mühendisliği Bölümü", 3, new DateTime(2026, 12, 4, 12, 0, 0, 0, DateTimeKind.Utc), "Yazılım Topluluğu" },
                    { 5, "Sektörden konuğumuzla mikrodenetleyiciler, IoT ve staj fırsatları üzerine söyleşi.", "Gömülü Sistemler Söyleşisi", "Elektrik-Elektronik Mühendisliği Bölümü", 4, new DateTime(2026, 12, 9, 11, 30, 0, 0, DateTimeKind.Utc), "Elektronik Topluluğu" },
                    { 6, "Teknokent'teki girişimlerle tanışma, kısa sunumlar ve fikir maratonu tanıtımı.", "Girişimcilik Günü", "Ankara Üniversitesi Teknokent", 14, new DateTime(2026, 12, 11, 10, 0, 0, 0, DateTimeKind.Utc), "Girişimcilik Kulübü" },
                    { 7, "Topluluk grubumuzun dönem sonu konseri. Giriş ücretsiz, herkes davetli.", "Kış Konseri", "Spor Bilimleri Fakültesi", 11, new DateTime(2026, 12, 16, 15, 0, 0, 0, DateTimeKind.Utc), "Müzik Topluluğu" },
                    { 8, "Mogan Gölü çevresinde 8 km kolay parkur. Servis kampüsten kalkar, kayıt için kulüp masasına uğrayın.", "Doğa Yürüyüşü: Mogan Gölü", "Kampüs girişi (servis kalkış noktası)", null, new DateTime(2026, 12, 19, 6, 30, 0, 0, DateTimeKind.Utc), "Dağcılık Kulübü" },
                    { 9, "Final haftası öncesi birlikte çalışma, ücretsiz çay ve kahve ikramı.", "Final Haftası Çalışma Maratonu", "Point Cafe", 12, new DateTime(2026, 12, 23, 14, 0, 0, 0, DateTimeKind.Utc), "Öğrenci Konseyi" }
                });

            migrationBuilder.InsertData(
                table: "KantinUrunleri",
                columns: new[] { "Id", "Ad", "Fiyat", "Kategori", "MekanId", "MevcutMu" },
                values: new object[,]
                {
                    { 1, "Çay", 15m, "SicakIcecek", 12, true },
                    { 2, "Türk Kahvesi", 60m, "SicakIcecek", 12, true },
                    { 3, "Filtre Kahve", 70m, "SicakIcecek", 12, true },
                    { 4, "Latte", 85m, "SicakIcecek", 12, true },
                    { 5, "Sıcak Çikolata", 75m, "SicakIcecek", 12, true },
                    { 6, "Su (0,5 L)", 10m, "SogukIcecek", 12, true },
                    { 7, "Ayran", 25m, "SogukIcecek", 12, true },
                    { 8, "Meyve Suyu", 40m, "SogukIcecek", 12, true },
                    { 9, "Gazlı İçecek", 45m, "SogukIcecek", 12, true },
                    { 10, "Kaşarlı Tost", 90m, "Yiyecek", 12, true },
                    { 11, "Karışık Tost", 110m, "Yiyecek", 12, true },
                    { 12, "Tavuklu Sandviç", 120m, "Yiyecek", 12, true },
                    { 13, "Simit", 25m, "Atistirmalik", 12, true },
                    { 14, "Peynirli Poğaça", 30m, "Atistirmalik", 12, true },
                    { 15, "Çikolata", 40m, "Atistirmalik", 12, true },
                    { 16, "Kek", 50m, "Tatli", 12, true },
                    { 17, "Cheesecake", 110m, "Tatli", 12, false }
                });

            migrationBuilder.UpdateData(
                table: "Mekanlar",
                keyColumn: "Id",
                keyValue: 13,
                column: "Tur",
                value: "Yemekhane");

            migrationBuilder.InsertData(
                table: "YemekMenuleri",
                columns: new[] { "Id", "AnaYemek", "Corba", "Kalori", "Tarih", "Tatli", "VejetaryenSecenek", "YanYemek" },
                values: new object[,]
                {
                    { 1, "Orman Kebabı", "Mercimek Çorbası", 1050, new DateOnly(2026, 11, 23), "Sütlaç", "Sebzeli Türlü", "Pirinç Pilavı" },
                    { 2, "Tavuk Sote", "Ezogelin Çorbası", 980, new DateOnly(2026, 11, 24), "Mevsim Meyvesi", "Zeytinyağlı Taze Fasulye", "Bulgur Pilavı" },
                    { 3, "Etli Kuru Fasulye", "Domates Çorbası", 1120, new DateOnly(2026, 11, 25), "Turşu", "Zeytinyağlı Kuru Fasulye", "Pirinç Pilavı" },
                    { 4, "Fırında Köfte", "Yayla Çorbası", 1150, new DateOnly(2026, 11, 26), "Revani", "Sebze Graten", "Patates Püresi" },
                    { 5, "Fırın Tavuk", "Tarhana Çorbası", 1010, new DateOnly(2026, 11, 27), "Ayran", "Mantar Sote", "Şehriyeli Pilav" },
                    { 6, "Etli Nohut", "Sebze Çorbası", 1080, new DateOnly(2026, 11, 30), "Cacık", "Zeytinyağlı Nohut", "Pirinç Pilavı" },
                    { 7, "Tavuk Şiş", "Mercimek Çorbası", 1090, new DateOnly(2026, 12, 1), "Kemalpaşa Tatlısı", "Sebzeli Bulgur", "Bulgur Pilavı" },
                    { 8, "İzmir Köfte", "Şehriye Çorbası", 1060, new DateOnly(2026, 12, 2), "Mevsim Meyvesi", "Zeytinyağlı Pırasa", "Pirinç Pilavı" },
                    { 9, "Karnıyarık", "Ezogelin Çorbası", 1000, new DateOnly(2026, 12, 3), "Yoğurt", "İmam Bayıldı", "Pirinç Pilavı" },
                    { 10, "Tavuk Döner", "Yayla Çorbası", 1170, new DateOnly(2026, 12, 4), "Supangle", "Sebzeli Makarna", "Bulgur Pilavı" },
                    { 11, "Etli Bezelye", "Domates Çorbası", 990, new DateOnly(2026, 12, 7), "Ayran", "Zeytinyağlı Bezelye", "Pirinç Pilavı" },
                    { 12, "Fırında Makarna", "Tarhana Çorbası", 1110, new DateOnly(2026, 12, 8), "Puding", "Peynirli Makarna", "Mevsim Salata" },
                    { 13, "Tas Kebabı", "Mercimek Çorbası", 1070, new DateOnly(2026, 12, 9), "Mevsim Meyvesi", "Ispanaklı Börek", "Bulgur Pilavı" },
                    { 14, "Tavuk Haşlama", "Sebze Çorbası", 980, new DateOnly(2026, 12, 10), "Kazandibi", "Sebzeli Pilav", "Pirinç Pilavı" },
                    { 15, "Kadınbudu Köfte", "Ezogelin Çorbası", 1140, new DateOnly(2026, 12, 11), "Cacık", "Mücver", "Patates Püresi" },
                    { 16, "Etli Kuru Fasulye", "Yayla Çorbası", 1120, new DateOnly(2026, 12, 14), "Turşu", "Zeytinyağlı Kuru Fasulye", "Pirinç Pilavı" },
                    { 17, "Tavuk Pirzola", "Şehriye Çorbası", 1160, new DateOnly(2026, 12, 15), "Şekerpare", "Mantarlı Makarna", "Bulgur Pilavı" },
                    { 18, "Etli Taze Fasulye", "Domates Çorbası", 960, new DateOnly(2026, 12, 16), "Yoğurt", "Zeytinyağlı Taze Fasulye", "Pirinç Pilavı" },
                    { 19, "Hasanpaşa Köfte", "Mercimek Çorbası", 1100, new DateOnly(2026, 12, 17), "Mevsim Meyvesi", "Patlıcan Musakka", "Şehriyeli Pilav" },
                    { 20, "Fırın Tavuk", "Tarhana Çorbası", 1030, new DateOnly(2026, 12, 18), "Güllaç", "Sebze Graten", "Mevsim Salata" },
                    { 21, "Orman Kebabı", "Mercimek Çorbası", 1050, new DateOnly(2026, 12, 21), "Sütlaç", "Sebzeli Türlü", "Pirinç Pilavı" },
                    { 22, "Tavuk Sote", "Ezogelin Çorbası", 980, new DateOnly(2026, 12, 22), "Mevsim Meyvesi", "Zeytinyağlı Taze Fasulye", "Bulgur Pilavı" },
                    { 23, "Etli Kuru Fasulye", "Domates Çorbası", 1120, new DateOnly(2026, 12, 23), "Turşu", "Zeytinyağlı Kuru Fasulye", "Pirinç Pilavı" },
                    { 24, "Fırında Köfte", "Yayla Çorbası", 1150, new DateOnly(2026, 12, 24), "Revani", "Sebze Graten", "Patates Püresi" },
                    { 25, "Fırın Tavuk", "Tarhana Çorbası", 1010, new DateOnly(2026, 12, 25), "Ayran", "Mantar Sote", "Şehriyeli Pilav" },
                    { 26, "Etli Nohut", "Sebze Çorbası", 1080, new DateOnly(2026, 12, 28), "Cacık", "Zeytinyağlı Nohut", "Pirinç Pilavı" },
                    { 27, "Tavuk Şiş", "Mercimek Çorbası", 1090, new DateOnly(2026, 12, 29), "Kemalpaşa Tatlısı", "Sebzeli Bulgur", "Bulgur Pilavı" },
                    { 28, "İzmir Köfte", "Şehriye Çorbası", 1060, new DateOnly(2026, 12, 30), "Mevsim Meyvesi", "Zeytinyağlı Pırasa", "Pirinç Pilavı" },
                    { 29, "Karnıyarık", "Ezogelin Çorbası", 1000, new DateOnly(2026, 12, 31), "Yoğurt", "İmam Bayıldı", "Pirinç Pilavı" },
                    { 30, "Tavuk Döner", "Yayla Çorbası", 1170, new DateOnly(2027, 1, 4), "Supangle", "Sebzeli Makarna", "Bulgur Pilavı" },
                    { 31, "Etli Bezelye", "Domates Çorbası", 990, new DateOnly(2027, 1, 5), "Ayran", "Zeytinyağlı Bezelye", "Pirinç Pilavı" },
                    { 32, "Fırında Makarna", "Tarhana Çorbası", 1110, new DateOnly(2027, 1, 6), "Puding", "Peynirli Makarna", "Mevsim Salata" },
                    { 33, "Tas Kebabı", "Mercimek Çorbası", 1070, new DateOnly(2027, 1, 7), "Mevsim Meyvesi", "Ispanaklı Börek", "Bulgur Pilavı" },
                    { 34, "Tavuk Haşlama", "Sebze Çorbası", 980, new DateOnly(2027, 1, 8), "Kazandibi", "Sebzeli Pilav", "Pirinç Pilavı" },
                    { 35, "Kadınbudu Köfte", "Ezogelin Çorbası", 1140, new DateOnly(2027, 1, 11), "Cacık", "Mücver", "Patates Püresi" },
                    { 36, "Etli Kuru Fasulye", "Yayla Çorbası", 1120, new DateOnly(2027, 1, 12), "Turşu", "Zeytinyağlı Kuru Fasulye", "Pirinç Pilavı" },
                    { 37, "Tavuk Pirzola", "Şehriye Çorbası", 1160, new DateOnly(2027, 1, 13), "Şekerpare", "Mantarlı Makarna", "Bulgur Pilavı" },
                    { 38, "Etli Taze Fasulye", "Domates Çorbası", 960, new DateOnly(2027, 1, 14), "Yoğurt", "Zeytinyağlı Taze Fasulye", "Pirinç Pilavı" },
                    { 39, "Hasanpaşa Köfte", "Mercimek Çorbası", 1100, new DateOnly(2027, 1, 15), "Mevsim Meyvesi", "Patlıcan Musakka", "Şehriyeli Pilav" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_MekanId",
                table: "Etkinlikler",
                column: "MekanId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_Tarih",
                table: "Etkinlikler",
                column: "Tarih");

            migrationBuilder.CreateIndex(
                name: "IX_KantinUrunleri_MekanId",
                table: "KantinUrunleri",
                column: "MekanId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekMenuleri_Tarih",
                table: "YemekMenuleri",
                column: "Tarih",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Etkinlikler_Mekanlar_MekanId",
                table: "Etkinlikler",
                column: "MekanId",
                principalTable: "Mekanlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Etkinlikler_Mekanlar_MekanId",
                table: "Etkinlikler");

            migrationBuilder.DropTable(
                name: "KantinUrunleri");

            migrationBuilder.DropTable(
                name: "YemekMenuleri");

            migrationBuilder.DropIndex(
                name: "IX_Etkinlikler_MekanId",
                table: "Etkinlikler");

            migrationBuilder.DropIndex(
                name: "IX_Etkinlikler_Tarih",
                table: "Etkinlikler");

            migrationBuilder.DeleteData(
                table: "Etkinlikler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Etkinlikler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Etkinlikler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Etkinlikler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Etkinlikler",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Etkinlikler",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Etkinlikler",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Etkinlikler",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Etkinlikler",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "MekanId",
                table: "Etkinlikler");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Etkinlikler",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "Mekanlar",
                keyColumn: "Id",
                keyValue: 13,
                column: "Tur",
                value: "Diger");
        }
    }
}
