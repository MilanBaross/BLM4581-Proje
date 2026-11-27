using KampusPortali.Models;

namespace KampusPortali.Data
{
    public static class YemekhaneTohumVerisi
    {
        private const int PointCafeId = 12;

        // Dört haftalık (20 günlük) menü sırası hafta içi günlere sırayla dağıtılır.
        private static readonly (string Corba, string AnaYemek, string YanYemek, string Tatli, string Vejetaryen, int Kalori)[] MenuSirasi =
        {
            ("Mercimek Çorbası", "Orman Kebabı", "Pirinç Pilavı", "Sütlaç", "Sebzeli Türlü", 1050),
            ("Ezogelin Çorbası", "Tavuk Sote", "Bulgur Pilavı", "Mevsim Meyvesi", "Zeytinyağlı Taze Fasulye", 980),
            ("Domates Çorbası", "Etli Kuru Fasulye", "Pirinç Pilavı", "Turşu", "Zeytinyağlı Kuru Fasulye", 1120),
            ("Yayla Çorbası", "Fırında Köfte", "Patates Püresi", "Revani", "Sebze Graten", 1150),
            ("Tarhana Çorbası", "Fırın Tavuk", "Şehriyeli Pilav", "Ayran", "Mantar Sote", 1010),
            ("Sebze Çorbası", "Etli Nohut", "Pirinç Pilavı", "Cacık", "Zeytinyağlı Nohut", 1080),
            ("Mercimek Çorbası", "Tavuk Şiş", "Bulgur Pilavı", "Kemalpaşa Tatlısı", "Sebzeli Bulgur", 1090),
            ("Şehriye Çorbası", "İzmir Köfte", "Pirinç Pilavı", "Mevsim Meyvesi", "Zeytinyağlı Pırasa", 1060),
            ("Ezogelin Çorbası", "Karnıyarık", "Pirinç Pilavı", "Yoğurt", "İmam Bayıldı", 1000),
            ("Yayla Çorbası", "Tavuk Döner", "Bulgur Pilavı", "Supangle", "Sebzeli Makarna", 1170),
            ("Domates Çorbası", "Etli Bezelye", "Pirinç Pilavı", "Ayran", "Zeytinyağlı Bezelye", 990),
            ("Tarhana Çorbası", "Fırında Makarna", "Mevsim Salata", "Puding", "Peynirli Makarna", 1110),
            ("Mercimek Çorbası", "Tas Kebabı", "Bulgur Pilavı", "Mevsim Meyvesi", "Ispanaklı Börek", 1070),
            ("Sebze Çorbası", "Tavuk Haşlama", "Pirinç Pilavı", "Kazandibi", "Sebzeli Pilav", 980),
            ("Ezogelin Çorbası", "Kadınbudu Köfte", "Patates Püresi", "Cacık", "Mücver", 1140),
            ("Yayla Çorbası", "Etli Kuru Fasulye", "Pirinç Pilavı", "Turşu", "Zeytinyağlı Kuru Fasulye", 1120),
            ("Şehriye Çorbası", "Tavuk Pirzola", "Bulgur Pilavı", "Şekerpare", "Mantarlı Makarna", 1160),
            ("Domates Çorbası", "Etli Taze Fasulye", "Pirinç Pilavı", "Yoğurt", "Zeytinyağlı Taze Fasulye", 960),
            ("Mercimek Çorbası", "Hasanpaşa Köfte", "Şehriyeli Pilav", "Mevsim Meyvesi", "Patlıcan Musakka", 1100),
            ("Tarhana Çorbası", "Fırın Tavuk", "Mevsim Salata", "Güllaç", "Sebze Graten", 1030),
        };

        private static readonly DateOnly IlkGun = new(2026, 11, 23);
        private static readonly DateOnly SonGun = new(2027, 1, 15);
        private static readonly DateOnly[] Tatiller = { new(2027, 1, 1) };

        public static readonly YemekMenusu[] Menuler = MenuleriOlustur();

        public static readonly KantinUrunu[] Urunler =
        {
            new() { Id = 1, MekanId = PointCafeId, Ad = "Çay", Kategori = UrunKategorisi.SicakIcecek, Fiyat = 15m },
            new() { Id = 2, MekanId = PointCafeId, Ad = "Türk Kahvesi", Kategori = UrunKategorisi.SicakIcecek, Fiyat = 60m },
            new() { Id = 3, MekanId = PointCafeId, Ad = "Filtre Kahve", Kategori = UrunKategorisi.SicakIcecek, Fiyat = 70m },
            new() { Id = 4, MekanId = PointCafeId, Ad = "Latte", Kategori = UrunKategorisi.SicakIcecek, Fiyat = 85m },
            new() { Id = 5, MekanId = PointCafeId, Ad = "Sıcak Çikolata", Kategori = UrunKategorisi.SicakIcecek, Fiyat = 75m },
            new() { Id = 6, MekanId = PointCafeId, Ad = "Su (0,5 L)", Kategori = UrunKategorisi.SogukIcecek, Fiyat = 10m },
            new() { Id = 7, MekanId = PointCafeId, Ad = "Ayran", Kategori = UrunKategorisi.SogukIcecek, Fiyat = 25m },
            new() { Id = 8, MekanId = PointCafeId, Ad = "Meyve Suyu", Kategori = UrunKategorisi.SogukIcecek, Fiyat = 40m },
            new() { Id = 9, MekanId = PointCafeId, Ad = "Gazlı İçecek", Kategori = UrunKategorisi.SogukIcecek, Fiyat = 45m },
            new() { Id = 10, MekanId = PointCafeId, Ad = "Kaşarlı Tost", Kategori = UrunKategorisi.Yiyecek, Fiyat = 90m },
            new() { Id = 11, MekanId = PointCafeId, Ad = "Karışık Tost", Kategori = UrunKategorisi.Yiyecek, Fiyat = 110m },
            new() { Id = 12, MekanId = PointCafeId, Ad = "Tavuklu Sandviç", Kategori = UrunKategorisi.Yiyecek, Fiyat = 120m },
            new() { Id = 13, MekanId = PointCafeId, Ad = "Simit", Kategori = UrunKategorisi.Atistirmalik, Fiyat = 25m },
            new() { Id = 14, MekanId = PointCafeId, Ad = "Peynirli Poğaça", Kategori = UrunKategorisi.Atistirmalik, Fiyat = 30m },
            new() { Id = 15, MekanId = PointCafeId, Ad = "Çikolata", Kategori = UrunKategorisi.Atistirmalik, Fiyat = 40m },
            new() { Id = 16, MekanId = PointCafeId, Ad = "Kek", Kategori = UrunKategorisi.Tatli, Fiyat = 50m },
            new() { Id = 17, MekanId = PointCafeId, Ad = "Cheesecake", Kategori = UrunKategorisi.Tatli, Fiyat = 110m, MevcutMu = false },
        };

        private static YemekMenusu[] MenuleriOlustur()
        {
            var menuler = new List<YemekMenusu>();
            var sira = 0;

            for (var gun = IlkGun; gun <= SonGun; gun = gun.AddDays(1))
            {
                if (gun.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday || Tatiller.Contains(gun))
                {
                    continue;
                }

                var m = MenuSirasi[sira % MenuSirasi.Length];
                menuler.Add(new YemekMenusu
                {
                    Id = sira + 1,
                    Tarih = gun,
                    Corba = m.Corba,
                    AnaYemek = m.AnaYemek,
                    YanYemek = m.YanYemek,
                    Tatli = m.Tatli,
                    VejetaryenSecenek = m.Vejetaryen,
                    Kalori = m.Kalori
                });
                sira++;
            }

            return menuler.ToArray();
        }
    }
}
