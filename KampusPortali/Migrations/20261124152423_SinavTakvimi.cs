using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KampusPortali.Migrations
{
    /// <inheritdoc />
    public partial class SinavTakvimi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Derslikler",
                columns: new[] { "Id", "Ad", "Blok", "Kapasite", "Kat", "MekanId", "MusaitMi", "Not" },
                values: new object[,]
                {
                    { 13, "G1 Dersliği", null, null, null, 5, true, "Yapay Zeka Mühendisliği binası" },
                    { 14, "G2 Dersliği", null, null, null, 5, true, "Yapay Zeka Mühendisliği binası" },
                    { 15, "G3 Dersliği", null, null, null, 5, true, "Yapay Zeka Mühendisliği binası" },
                    { 16, "G4 Dersliği", null, null, null, 5, true, "Yapay Zeka Mühendisliği binası" },
                    { 17, "F1 Dersliği", null, null, null, 5, true, "Yapay Zeka Mühendisliği binası (eski J1)" },
                    { 18, "F2 Dersliği", null, null, null, 5, true, "Yapay Zeka Mühendisliği binası (eski J2)" },
                    { 19, "BME1 Dersliği", null, null, null, 6, true, "Biyomedikal Mühendisliği Bölümü dersliği" }
                });

            migrationBuilder.InsertData(
                table: "Sinavlar",
                columns: new[] { "Id", "BaslangicSaati", "BitisSaati", "DersKodu", "DerslikId", "Tarih", "Tur" },
                values: new object[,]
                {
                    { 1001, new TimeOnly(13, 0, 0), new TimeOnly(14, 30, 0), "BLM4559", 4, new DateOnly(2026, 11, 16), "Vize" },
                    { 1002, new TimeOnly(13, 0, 0), new TimeOnly(14, 30, 0), "COM3067", 1, new DateOnly(2026, 11, 16), "Vize" },
                    { 1003, new TimeOnly(13, 0, 0), new TimeOnly(14, 30, 0), "COM3067", 2, new DateOnly(2026, 11, 16), "Vize" },
                    { 1004, new TimeOnly(13, 0, 0), new TimeOnly(14, 30, 0), "COM3067", 3, new DateOnly(2026, 11, 16), "Vize" },
                    { 1005, new TimeOnly(15, 0, 0), new TimeOnly(16, 30, 0), "BLM2039", 1, new DateOnly(2026, 11, 16), "Vize" },
                    { 1006, new TimeOnly(15, 0, 0), new TimeOnly(16, 30, 0), "BLM2039", 2, new DateOnly(2026, 11, 16), "Vize" },
                    { 1007, new TimeOnly(15, 0, 0), new TimeOnly(16, 30, 0), "BLM2039", 4, new DateOnly(2026, 11, 16), "Vize" },
                    { 1008, new TimeOnly(16, 45, 0), new TimeOnly(18, 15, 0), "BLM3551", 1, new DateOnly(2026, 11, 16), "Vize" },
                    { 1009, new TimeOnly(16, 45, 0), new TimeOnly(18, 15, 0), "BLM3551", 4, new DateOnly(2026, 11, 16), "Vize" },
                    { 1010, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "COM1007", 1, new DateOnly(2026, 11, 17), "Vize" },
                    { 1011, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "COM1007", 2, new DateOnly(2026, 11, 17), "Vize" },
                    { 1012, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "COM1007", 3, new DateOnly(2026, 11, 17), "Vize" },
                    { 1013, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM3533", 4, new DateOnly(2026, 11, 17), "Vize" },
                    { 1014, new TimeOnly(11, 15, 0), new TimeOnly(12, 45, 0), "COM3023", 1, new DateOnly(2026, 11, 17), "Vize" },
                    { 1015, new TimeOnly(11, 15, 0), new TimeOnly(12, 45, 0), "COM3023", 2, new DateOnly(2026, 11, 17), "Vize" },
                    { 1017, new TimeOnly(13, 0, 0), new TimeOnly(14, 30, 0), "COM2077", 1, new DateOnly(2026, 11, 17), "Vize" },
                    { 1018, new TimeOnly(13, 0, 0), new TimeOnly(14, 30, 0), "COM2077", 2, new DateOnly(2026, 11, 17), "Vize" },
                    { 1019, new TimeOnly(13, 0, 0), new TimeOnly(14, 30, 0), "BLM4583", 1, new DateOnly(2026, 11, 17), "Vize" },
                    { 1020, new TimeOnly(14, 45, 0), new TimeOnly(16, 15, 0), "BLM2077", 1, new DateOnly(2026, 11, 17), "Vize" },
                    { 1021, new TimeOnly(14, 45, 0), new TimeOnly(16, 15, 0), "BLM2077", 2, new DateOnly(2026, 11, 17), "Vize" },
                    { 1022, new TimeOnly(14, 45, 0), new TimeOnly(16, 15, 0), "BLM2077", 3, new DateOnly(2026, 11, 17), "Vize" },
                    { 1024, new TimeOnly(15, 0, 0), new TimeOnly(16, 15, 0), "ENG201", 10, new DateOnly(2026, 11, 17), "Vize" },
                    { 1026, new TimeOnly(16, 30, 0), new TimeOnly(17, 45, 0), "ENG101", 10, new DateOnly(2026, 11, 17), "Vize" },
                    { 1027, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM1007", 1, new DateOnly(2026, 11, 18), "Vize" },
                    { 1028, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM1007", 2, new DateOnly(2026, 11, 18), "Vize" },
                    { 1029, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM1007", 4, new DateOnly(2026, 11, 18), "Vize" },
                    { 1030, new TimeOnly(11, 30, 0), new TimeOnly(13, 0, 0), "COM1015", 1, new DateOnly(2026, 11, 18), "Vize" },
                    { 1031, new TimeOnly(11, 30, 0), new TimeOnly(13, 0, 0), "COM1015", 2, new DateOnly(2026, 11, 18), "Vize" },
                    { 1032, new TimeOnly(11, 30, 0), new TimeOnly(13, 0, 0), "COM1015", 3, new DateOnly(2026, 11, 18), "Vize" },
                    { 1033, new TimeOnly(13, 30, 0), new TimeOnly(15, 0, 0), "COM2043", 1, new DateOnly(2026, 11, 18), "Vize" },
                    { 1035, new TimeOnly(13, 30, 0), new TimeOnly(15, 0, 0), "BLM3023", 2, new DateOnly(2026, 11, 18), "Vize" },
                    { 1036, new TimeOnly(13, 30, 0), new TimeOnly(15, 0, 0), "BLM3023", 4, new DateOnly(2026, 11, 18), "Vize" },
                    { 1037, new TimeOnly(15, 30, 0), new TimeOnly(17, 0, 0), "BLM2043", 1, new DateOnly(2026, 11, 18), "Vize" },
                    { 1038, new TimeOnly(15, 30, 0), new TimeOnly(17, 0, 0), "BLM2043", 2, new DateOnly(2026, 11, 18), "Vize" },
                    { 1039, new TimeOnly(15, 30, 0), new TimeOnly(17, 0, 0), "BLM2043", 4, new DateOnly(2026, 11, 18), "Vize" },
                    { 1040, new TimeOnly(17, 15, 0), new TimeOnly(18, 45, 0), "COM4573", 1, new DateOnly(2026, 11, 18), "Vize" },
                    { 1041, new TimeOnly(17, 15, 0), new TimeOnly(18, 45, 0), "COM4573", 2, new DateOnly(2026, 11, 18), "Vize" },
                    { 1042, new TimeOnly(9, 30, 0), new TimeOnly(10, 30, 0), "COM4569", 2, new DateOnly(2026, 11, 19), "Vize" },
                    { 1043, new TimeOnly(9, 30, 0), new TimeOnly(10, 30, 0), "COM4569", 4, new DateOnly(2026, 11, 19), "Vize" },
                    { 1044, new TimeOnly(9, 0, 0), new TimeOnly(10, 30, 0), "BLM4561", 1, new DateOnly(2026, 11, 19), "Vize" },
                    { 1045, new TimeOnly(10, 45, 0), new TimeOnly(12, 15, 0), "MAT0143", 1, new DateOnly(2026, 11, 19), "Vize" },
                    { 1046, new TimeOnly(10, 45, 0), new TimeOnly(12, 15, 0), "MAT0143", 2, new DateOnly(2026, 11, 19), "Vize" },
                    { 1047, new TimeOnly(10, 45, 0), new TimeOnly(12, 15, 0), "MAT0143", 3, new DateOnly(2026, 11, 19), "Vize" },
                    { 1048, new TimeOnly(12, 30, 0), new TimeOnly(13, 30, 0), "BLM3557", 1, new DateOnly(2026, 11, 19), "Vize" },
                    { 1049, new TimeOnly(12, 30, 0), new TimeOnly(13, 30, 0), "BLM3557", 2, new DateOnly(2026, 11, 19), "Vize" },
                    { 1052, new TimeOnly(14, 45, 0), new TimeOnly(16, 15, 0), "COM3073", 1, new DateOnly(2026, 11, 19), "Vize" },
                    { 1053, new TimeOnly(14, 45, 0), new TimeOnly(16, 15, 0), "COM3073", 3, new DateOnly(2026, 11, 19), "Vize" },
                    { 1056, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM2067", 1, new DateOnly(2026, 11, 20), "Vize" },
                    { 1057, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM2067", 2, new DateOnly(2026, 11, 20), "Vize" },
                    { 1058, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM2067", 4, new DateOnly(2026, 11, 20), "Vize" },
                    { 1061, new TimeOnly(11, 30, 0), new TimeOnly(13, 0, 0), "BLM3033", 1, new DateOnly(2026, 11, 20), "Vize" },
                    { 1062, new TimeOnly(11, 30, 0), new TimeOnly(13, 0, 0), "BLM3033", 2, new DateOnly(2026, 11, 20), "Vize" },
                    { 1063, new TimeOnly(11, 30, 0), new TimeOnly(13, 0, 0), "BLM3033", 3, new DateOnly(2026, 11, 20), "Vize" },
                    { 1064, new TimeOnly(13, 30, 0), new TimeOnly(15, 0, 0), "MTH0143", 1, new DateOnly(2026, 11, 20), "Vize" },
                    { 1065, new TimeOnly(13, 30, 0), new TimeOnly(15, 0, 0), "MTH0143", 2, new DateOnly(2026, 11, 20), "Vize" },
                    { 1066, new TimeOnly(13, 30, 0), new TimeOnly(15, 0, 0), "MTH0143", 3, new DateOnly(2026, 11, 20), "Vize" },
                    { 1067, new TimeOnly(15, 15, 0), new TimeOnly(16, 15, 0), "COM3533", 1, new DateOnly(2026, 11, 20), "Vize" },
                    { 1068, new TimeOnly(17, 0, 0), new TimeOnly(18, 15, 0), "PHY0105", 1, new DateOnly(2026, 11, 20), "Vize" },
                    { 1069, new TimeOnly(17, 0, 0), new TimeOnly(18, 15, 0), "PHY0105", 2, new DateOnly(2026, 11, 20), "Vize" },
                    { 1070, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM1015", 1, new DateOnly(2026, 11, 21), "Vize" },
                    { 1071, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM1015", 2, new DateOnly(2026, 11, 21), "Vize" },
                    { 1072, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "BLM1015", 3, new DateOnly(2026, 11, 21), "Vize" },
                    { 1075, new TimeOnly(11, 30, 0), new TimeOnly(12, 30, 0), "FZM0105", 1, new DateOnly(2026, 11, 21), "Vize" },
                    { 1076, new TimeOnly(11, 30, 0), new TimeOnly(12, 30, 0), "FZM0105", 2, new DateOnly(2026, 11, 21), "Vize" },
                    { 1077, new TimeOnly(11, 30, 0), new TimeOnly(12, 30, 0), "FZM0105", 3, new DateOnly(2026, 11, 21), "Vize" },
                    { 1078, new TimeOnly(13, 0, 0), new TimeOnly(14, 30, 0), "COM2039", 1, new DateOnly(2026, 11, 21), "Vize" },
                    { 1079, new TimeOnly(13, 0, 0), new TimeOnly(14, 30, 0), "COM2039", 2, new DateOnly(2026, 11, 21), "Vize" },
                    { 1080, new TimeOnly(14, 45, 0), new TimeOnly(16, 15, 0), "BLM3067", 1, new DateOnly(2026, 11, 21), "Vize" },
                    { 1081, new TimeOnly(14, 45, 0), new TimeOnly(16, 15, 0), "BLM3067", 2, new DateOnly(2026, 11, 21), "Vize" },
                    { 1082, new TimeOnly(16, 25, 0), new TimeOnly(17, 45, 0), "COM4567", 1, new DateOnly(2026, 11, 21), "Vize" },
                    { 1083, new TimeOnly(16, 25, 0), new TimeOnly(17, 45, 0), "COM4567", 2, new DateOnly(2026, 11, 21), "Vize" },
                    { 1016, new TimeOnly(11, 15, 0), new TimeOnly(12, 45, 0), "BLM2501", 14, new DateOnly(2026, 11, 17), "Vize" },
                    { 1023, new TimeOnly(15, 0, 0), new TimeOnly(16, 15, 0), "ENG201", 19, new DateOnly(2026, 11, 17), "Vize" },
                    { 1025, new TimeOnly(16, 30, 0), new TimeOnly(17, 45, 0), "ENG101", 19, new DateOnly(2026, 11, 17), "Vize" },
                    { 1034, new TimeOnly(13, 30, 0), new TimeOnly(15, 0, 0), "COM2043", 14, new DateOnly(2026, 11, 18), "Vize" },
                    { 1050, new TimeOnly(14, 0, 0), new TimeOnly(15, 30, 0), "BLM3073", 17, new DateOnly(2026, 11, 19), "Vize" },
                    { 1051, new TimeOnly(14, 0, 0), new TimeOnly(15, 30, 0), "BLM3073", 18, new DateOnly(2026, 11, 19), "Vize" },
                    { 1054, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "COM2067", 14, new DateOnly(2026, 11, 20), "Vize" },
                    { 1055, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "COM2067", 15, new DateOnly(2026, 11, 20), "Vize" },
                    { 1059, new TimeOnly(11, 30, 0), new TimeOnly(13, 0, 0), "COM3033", 14, new DateOnly(2026, 11, 20), "Vize" },
                    { 1060, new TimeOnly(11, 30, 0), new TimeOnly(13, 0, 0), "COM3033", 15, new DateOnly(2026, 11, 20), "Vize" },
                    { 1073, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "COM3551", 14, new DateOnly(2026, 11, 21), "Vize" },
                    { 1074, new TimeOnly(9, 30, 0), new TimeOnly(11, 0, 0), "COM3551", 16, new DateOnly(2026, 11, 21), "Vize" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1027);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1028);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1029);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1038);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1039);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1044);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1046);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1047);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1048);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1049);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1054);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1055);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1056);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1057);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1058);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1059);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1060);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1061);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1062);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1063);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1064);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1065);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1066);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1067);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1068);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1069);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1070);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1071);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1072);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1073);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1074);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1075);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1076);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1077);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1078);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1079);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1080);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1081);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1082);

            migrationBuilder.DeleteData(
                table: "Sinavlar",
                keyColumn: "Id",
                keyValue: 1083);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Derslikler",
                keyColumn: "Id",
                keyValue: 19);
        }
    }
}
