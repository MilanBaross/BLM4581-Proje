namespace KampusPortali.Yardimcilar
{
    public static class KutuphaneKurallari
    {
        public static readonly TimeOnly Acilis = new(8, 0);
        public static readonly TimeOnly Kapanis = new(22, 0);
        public static readonly TimeSpan TekSeferdeEnFazlaSure = TimeSpan.FromHours(3);
        public static readonly TimeSpan GunlukEnFazlaSure = TimeSpan.FromHours(5);
        public const int AdimDakika = 30;
        public const int EnFazlaGunIleri = 7;

        public static string KatAdi(int kat) => kat == 0 ? "Zemin Kat" : $"{kat}. Kat";

        public static bool AdimaUygunMu(TimeOnly saat) => saat.Second == 0 && saat.Minute % AdimDakika == 0;

        public static TimeOnly AdimaYuvarla(TimeOnly saat) => new(saat.Hour, saat.Minute - saat.Minute % AdimDakika);
    }
}
