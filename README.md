# BLM4581-Proje
BLM4581 Ağ Tabanlı Teknolojiler Projesi: **Ankara Üniversitesi Kampüs Portalı (AÜ Hub)**

ASP.NET Core Razor Pages (.NET 10) + Entity Framework Core (Code-First) + PostgreSQL.

## Kurulum

1. .NET 10 SDK ve PostgreSQL kurulu olmalı.
2. EF Core aracını kurun (bir kere): `dotnet tool install --global dotnet-ef`
3. PostgreSQL bağlantı bilgilerinizi **User Secrets** ile kaydedin (bir kere). Şifre depoya değil, bilgisayarınızdaki kullanıcı klasörüne kaydedilir; `appsettings.json` dosyasını düzenlemeyin.

```bash
cd KampusPortali
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=KampusPortaliDb;Username=postgres;Password=SIFRENIZ"
```

4. İlk yönetici hesabının bilgilerini de **User Secrets** ile kaydedin. Uygulama her açılışta rolleri (`Yonetici`, `Kullanici`) oluşturur ve bu hesap yoksa açıp `Yonetici` rolüne ekler:

```bash
cd KampusPortali
dotnet user-secrets set "YoneticiHesabi:Email" "yonetici@ankara.edu.tr"
dotnet user-secrets set "YoneticiHesabi:Sifre" "GucluSifre123"
```

5. Veritabanını oluşturun ve çalıştırın:

```bash
cd KampusPortali
dotnet ef database update
dotnet run
```

## Veritabanı tabloları

| Tablo | İlişkiler |
|---|---|
| Kullanicilar (Identity) | KutuphaneMasalari (rezerve eden), KayipEsyalar (bildiren) |
| Roller (Identity) | KullaniciRolleri |
| KullaniciRolleri (Identity) | Kullanicilar, Roller (çoka-çok ara tablo) |
| Akademisyenler | Dersler |
| Derslikler | Dersler |
| Dersler | Derslikler, Akademisyenler |
| KutuphaneMasalari | Kullanicilar |
| Etkinlikler | - |
| KayipEsyalar | Kullanicilar |

Modeller, iOS projesindeki ([BLM4587-Proje](https://github.com/superkerem/BLM4587-Proje)) tablo taslaklarıyla aynı alan adlarını kullanır; böylece iki ders için aynı veri yapısı paylaşılabilir.

## Kimlik doğrulama ve roller (2. hafta)

- **ASP.NET Core Identity** kullanılır. `Kullanici` sınıfı `IdentityUser<int>`'ten türetilir; şifreler Identity tarafından hash'lenerek saklanır.
- Roller: `Yonetici` ve `Kullanici`. Kayıt olan herkes `Kullanici` rolüne eklenir; yönetici hesabı User Secrets'tan okunarak oluşturulur.
- Sayfalar: `/Kayit`, `/Giris` (öğrenci no veya e-posta ile), `/Cikis`, `/ErisimEngellendi`, `/Yonetim` (yalnızca `Yonetici` rolü).
- 5 hatalı girişte hesap 5 dakika kilitlenir.
- Formlarda **Data Annotations** ile hem sunucu (`ModelState.IsValid`) hem istemci tarafı (jQuery Unobtrusive Validation) doğrulama yapılır.
