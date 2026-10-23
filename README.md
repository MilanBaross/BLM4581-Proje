# BLM4581-Proje
BLM4581 Ağ Tabanlı Teknolojiler Projesi: **Ankara Üniversitesi Kampüs Portalı (AÜ Hub)**

ASP.NET Core Razor Pages (.NET 10) + Entity Framework Core (Code-First) + PostgreSQL.

## Kurulum

1. .NET 10 SDK ve PostgreSQL kurulu olmalı.
2. `KampusPortali/appsettings.json` içindeki `DefaultConnection` bağlantı cümlesini kendi PostgreSQL kullanıcı adı ve şifrenize göre düzenleyin.
3. EF Core aracını kurun (bir kere): `dotnet tool install --global dotnet-ef`
4. Veritabanını oluşturun ve çalıştırın:

```bash
cd KampusPortali
dotnet ef database update
dotnet run
```

## Veritabanı tabloları

| Tablo | İlişkiler |
|---|---|
| Kullanicilar | KutuphaneMasalari (rezerve eden), KayipEsyalar (bildiren) |
| Akademisyenler | Dersler |
| Derslikler | Dersler |
| Dersler | Derslikler, Akademisyenler |
| KutuphaneMasalari | Kullanicilar |
| Etkinlikler | - |
| KayipEsyalar | Kullanicilar |

Modeller, iOS projesindeki ([BLM4587-Proje](https://github.com/superkerem/BLM4587-Proje)) tablo taslaklarıyla aynı alan adlarını kullanır; böylece iki ders için aynı veri yapısı paylaşılabilir.
