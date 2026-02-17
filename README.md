# 🏎️ F1 Taraftar (F1 Fan & Prediction API)

> Bu repo, **ASP.NET Core ile geliştirilmiş**, **JWT tabanlı kimlik doğrulama**, **oylama & tahmin sistemi** ve **blockchain ile NFT ödül entegrasyonu** içeren kapsamlı bir **backend API projesidir**.

Proje, Formula 1 sezonu boyunca kullanıcıların:

- favori sürücülerini oylayabildiği,
- yarışlar için tahminde bulunabildiği,
- doğru tahmin yapanlara NFT ödülleri verdiği

gerçek dünya seviyesinde bir sistem olarak tasarlanmıştır.
## 🚀 Özellikler

### 🔐 Kimlik Doğrulama ve Güvenlik
* **JWT (JSON Web Token):** Güvenli giriş ve kayıt işlemleri.
* **Refresh Token Mekanizması:** Kullanıcı oturumu süresi dolduğunda, tekrar giriş yapmaya gerek kalmadan sessizce yeni token alma özelliği.
* **Rol Bazlı Yetkilendirme (RBAC):** Yarışları sonuçlandırma ve ödül dağıtma gibi işlemler sadece **Admin** yetkisine sahip kullanıcılar tarafından yapılabilir.

### 🏁 Yarış ve Tahmin Sistemi
* **Yarış Yönetimi:** Adminler yarış takvimini yönetebilir.
* **Podyum Tahmini:** Kullanıcılar belirli bir yarış için **1., 2. ve 3.** sıradaki pilotları tahmin edebilir.
* **Akıllı Validasyon (FluentValidation):**
    * Bir kullanıcının aynı tahmin içinde aynı pilotu birden fazla kez seçmesini engelleyen özel mantık.
    * Yarış başladıktan sonra tahmin yapılmasını engelleyen zaman kısıtlaması kontrolleri.

### 🏆 Ödül Sistemi ve İş Mantığı
* **Otomatik Puanlama:** Admin yarışı sonlandırdığında sistem gerçek sonuçlarla tahminleri karşılaştırır.
* **Kazanan Ayrıştırma:** Sadece ilk 3'ü **tam sırasıyla** bilen kullanıcılar veritabanı seviyesinde optimize edilmiş LINQ sorguları ile çekilir.
* **NFT Ödül Kuyruğu:** Kazananlar belirlenir ve ödül tablosuna "Beklemede (Pending)" olarak eklenir.
* **Asenkron İşleme (Background Processing):** Blokzincir (Blockchain) gibi yavaş işlemlerde API'nin kilitlenmemesi için `Task.Run` ve Background Service mantığı kullanılarak ödül dağıtımı arka planda (Fire-and-Forget) yapılır.

## 🛠️ Teknoloji Yığını

* **Framework:** .NET 8 (ASP.NET Core Web API)
* **Veritabanı:** MS SQL Server
* **ORM:** Entity Framework Core (Code First)
* **Validasyon:** FluentValidation
* **Kimlik Doğrulama:** Microsoft.Identity / JWT Bearer
* **Dokümantasyon:** Scalar / Swagger UI

## 🏗️ Mimari ve Tasarım

Proje, sorumlulukların ayrılması (SoC) ilkesine dayanan **Clean Architecture** prensiplerini takip eder:
* **Controllers:** Sadece HTTP isteklerini karşılar.
* **Services:** Tüm iş mantığını (Business Logic), kuralları ve hesaplamaları içerir.
* **Repositories/Data:** Veritabanı işlemlerini yönetir.
* **DTOs:** Veri transferi için kullanılan modellerdir, veritabanı varlıklarını (Entity) dış dünyadan gizler.

## ⚡ Kurulum ve Başlangıç

1.  **Projeyi Klonlayın**
    ```bash
    git clone [https://github.com/capanoglu-hus/f1api.git](https://github.com/capanoglu-hus/f1api.git)
    ```

2.  **Veritabanı Ayarları**
    `appsettings.json` dosyasındaki "DefaultConnection" alanına kendi SQL Server bağlantı cümlenizi yazın.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=...;Database=F1ApiDb;..."
    }
    ```

3.  **Migration İşlemleri**
    Veritabanını oluşturmak için terminalde şu komutu çalıştırın:
    ```bash
    dotnet ef database update
    ```

4.  **Projeyi Ayağa Kaldırın**
    ```bash
    dotnet run
    ```

## 🔌 Önemli API Uçları (Endpoints)

| Metot | Endpoint | Açıklama | Yetki |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/auth/login` | Giriş yap, Access ve Refresh Token al | ✅ (User) |
| `POST` | `/api/prediction` | Yarış için ilk 3 tahminini gönder | ✅ (User) |
| `POST` | `/api/vote/driver` | Favori sürücüye oy ver | ✅ (User) |
| `POST` | `/api/race/finish-race` | **(Admin)** Yarışı bitir, sonuçları hesapla ve ödül dağıtımını başlat | ✅ (Admin) |

## 🗺️ Yol Haritası (Roadmap)

- [x] Backend Mimarisi ve Veritabanı Kurulumu
- [x] Oylama ve Tahmin Mantığı (Validasyonlar Dahil)
- [x] JWT ve Refresh Token Entegrasyonu
- [x] Kazanan Hesaplama Algoritması
- [ ] **Frontend Entegrasyonu (React + TypeScript + Vite)** *[Geliştiriliyor]*
- [ ] Web3 / Akıllı Sözleşme Entegrasyonu (Gerçek NFT Mintleme)

