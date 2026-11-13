namespace KampusPortali.Yardimcilar
{
    public static class TurkiyeSaati
    {
        private static readonly TimeZoneInfo Bolge = BolgeyiBul();

        public static DateTime Simdi() => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Bolge);

        public static DateTime UtcyeCevir(DateOnly tarih, TimeOnly saat) =>
            TimeZoneInfo.ConvertTimeToUtc(tarih.ToDateTime(saat, DateTimeKind.Unspecified), Bolge);

        public static DateTime YereleCevir(DateTime utc) =>
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(utc, DateTimeKind.Utc), Bolge);

        private static TimeZoneInfo BolgeyiBul()
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Europe/Istanbul");
            }
            catch (TimeZoneNotFoundException)
            {
                return TimeZoneInfo.Local;
            }
        }
    }
}
