using KampusPortali.Models;

namespace KampusPortali.Data
{
    public static class EtkinlikTohumVerisi
    {
        // Saatler UTC olarak yazıldı (Türkiye saati = UTC + 3).
        public static readonly Etkinlik[] Etkinlikler =
        {
            new()
            {
                Id = 1, Baslik = "Yapay Zekâya Giriş Atölyesi", ToplulukAdi = "Yazılım Topluluğu",
                Aciklama = "Python ile ilk makine öğrenmesi modelimizi birlikte kuruyoruz. Bilgisayarını getirmen yeterli, ön bilgi gerekmiyor.",
                Tarih = new DateTime(2026, 11, 18, 11, 0, 0, DateTimeKind.Utc), Konum = "Bilgisayar Mühendisliği Bölümü", MekanId = 2
            },
            new()
            {
                Id = 2, Baslik = "Kampüs Fotoğraf Yürüyüşü", ToplulukAdi = "Fotoğrafçılık Kulübü",
                Aciklama = "Gölbaşı kampüsünde sonbahar fotoğrafları çekiyoruz. Telefon kamerası da olur. Buluşma noktası Dekanlık önü.",
                Tarih = new DateTime(2026, 11, 21, 10, 30, 0, DateTimeKind.Utc), Konum = "Mühendislik Fakültesi Dekanlığı önü", MekanId = 9
            },
            new()
            {
                Id = 3, Baslik = "Satranç Turnuvası", ToplulukAdi = "Satranç Kulübü",
                Aciklama = "İsviçre sistemi 5 tur, 15+5 tempo. Kayıtlar etkinlik günü 13.30'a kadar alınır. İlk üçe kitap hediyesi var.",
                Tarih = new DateTime(2026, 12, 2, 11, 0, 0, DateTimeKind.Utc), Konum = "Point Cafe", MekanId = 12
            },
            new()
            {
                Id = 4, Baslik = "Git ve GitHub Semineri", ToplulukAdi = "Yazılım Topluluğu",
                Aciklama = "Dal açma, PR, merge ve çakışma çözme. Dönem projesi yapan herkes için pratik bir oturum.",
                Tarih = new DateTime(2026, 12, 4, 12, 0, 0, DateTimeKind.Utc), Konum = "Yazılım Mühendisliği Bölümü", MekanId = 3
            },
            new()
            {
                Id = 5, Baslik = "Gömülü Sistemler Söyleşisi", ToplulukAdi = "Elektronik Topluluğu",
                Aciklama = "Sektörden konuğumuzla mikrodenetleyiciler, IoT ve staj fırsatları üzerine söyleşi.",
                Tarih = new DateTime(2026, 12, 9, 11, 30, 0, DateTimeKind.Utc), Konum = "Elektrik-Elektronik Mühendisliği Bölümü", MekanId = 4
            },
            new()
            {
                Id = 6, Baslik = "Girişimcilik Günü", ToplulukAdi = "Girişimcilik Kulübü",
                Aciklama = "Teknokent'teki girişimlerle tanışma, kısa sunumlar ve fikir maratonu tanıtımı.",
                Tarih = new DateTime(2026, 12, 11, 10, 0, 0, DateTimeKind.Utc), Konum = "Ankara Üniversitesi Teknokent", MekanId = 14
            },
            new()
            {
                Id = 7, Baslik = "Kış Konseri", ToplulukAdi = "Müzik Topluluğu",
                Aciklama = "Topluluk grubumuzun dönem sonu konseri. Giriş ücretsiz, herkes davetli.",
                Tarih = new DateTime(2026, 12, 16, 15, 0, 0, DateTimeKind.Utc), Konum = "Spor Bilimleri Fakültesi", MekanId = 11
            },
            new()
            {
                Id = 8, Baslik = "Doğa Yürüyüşü: Mogan Gölü", ToplulukAdi = "Dağcılık Kulübü",
                Aciklama = "Mogan Gölü çevresinde 8 km kolay parkur. Servis kampüsten kalkar, kayıt için kulüp masasına uğrayın.",
                Tarih = new DateTime(2026, 12, 19, 6, 30, 0, DateTimeKind.Utc), Konum = "Kampüs girişi (servis kalkış noktası)", MekanId = null
            },
            new()
            {
                Id = 9, Baslik = "Final Haftası Çalışma Maratonu", ToplulukAdi = "Öğrenci Konseyi",
                Aciklama = "Final haftası öncesi birlikte çalışma, ücretsiz çay ve kahve ikramı.",
                Tarih = new DateTime(2026, 12, 23, 14, 0, 0, DateTimeKind.Utc), Konum = "Point Cafe", MekanId = 12
            },
        };
    }
}
