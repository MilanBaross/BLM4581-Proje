namespace KampusPortali.Models
{
    public record Modul(string Baslik, string Ikon, string Sayfa, string Aciklama);

    public static class Moduller
    {
        public static readonly Modul Harita = new(
            "Kampüs Haritası", "🗺️", "/Kampus/Harita",
            "Fakülteleri, binaları ve önemli noktaları harita üzerinde görüntüleyin.");

        public static readonly Modul DerslikBul = new(
            "Derslik Bulucu", "🚪", "/Kampus/DerslikBul",
            "Dersliklerin hangi binada ve katta olduğunu kolayca bulun.");

        public static readonly Modul Kutuphane = new(
            "Kütüphane", "📚", "/Kampus/Kutuphane",
            "Kütüphane doluluk durumunu görün ve çalışma masası ayırın.");

        public static readonly Modul Yemekhane = new(
            "Yemekhane/Kantin", "🍽️", "/Kampus/Yemekhane",
            "Günlük yemek menüsüne ve kantin bilgilerine göz atın.");

        public static readonly Modul Etkinlikler = new(
            "Etkinlikler", "📅", "/Kampus/Etkinlikler",
            "Kampüsteki konferans, seminer ve kulüp etkinliklerini takip edin.");

        public static readonly Modul KayipEsya = new(
            "Kayıp Eşya", "🔍", "/Kampus/KayipEsya",
            "Kaybettiğiniz eşyaları bildirin, bulunan eşyaları inceleyin.");

        public static readonly Modul[] Tumu =
            { Harita, DerslikBul, Kutuphane, Yemekhane, Etkinlikler, KayipEsya };
    }
}
