# PostgreSQL Migration Summary

## Datum migracije: 2026-07-05

Ovaj dokument opisuje sve promene napravljene tokom migracije AutoOglasi aplikacije sa SQL Server-a na PostgreSQL.

## 🔧 Promene u Paketima

### Directory.Packages.props
- **Uklonjeno**: `Microsoft.EntityFrameworkCore.SqlServer` (10.0.9)
- **Dodato**: `Npgsql.EntityFrameworkCore.PostgreSQL` (10.0.2)

### Projekti koji koriste PostgreSQL provider:
- `Data/AutoOglasi.Data/AutoOglasi.Data.csproj`
- `Web/AutoOglasi.Web/AutoOglasi.Web.csproj`

## 📝 Promene u Kodu

### 1. Program.cs (`Web/AutoOglasi.Web/Program.cs`)

- Aplikacija koristi `UseNpgsql(defaultConnection, ...)` za EF Core.
- Startup očekuje PostgreSQL connection string i odbacuje placeholder vrednosti kao što je `YOUR_PASSWORD`.
- Poruka greške sada jasno ukazuje na PostgreSQL konfiguraciju.

### 2. AutoOglasiDbContext.cs (`Data/AutoOglasi.Data/AutoOglasiDbContext.cs`)

**Decimalni tip podataka:**

**Pre:**
```csharp
builder.Entity<Car>()
	.Property(c => c.Price)
	.HasColumnType("decimal(18,2)");
```

**Posle:**
```csharp
builder.Entity<Car>()
	.Property(c => c.Price)
	.HasPrecision(18, 2);
```

**Razlog:** `HasPrecision()` je cross-platform API koji podržavaju i SQL Server i PostgreSQL, dok je `HasColumnType("decimal(18,2)")` SQL Server-specifičan.

**Komentar:**
- Promenjen komentar sa "SQL Server eksplicitne konfiguracije" na "PostgreSQL compatible Identity table configurations"

## 🗄️ Migracije

### Obrisane stare SQL Server migracije:
- `20211204115605_FinalMigrationsCombined`
- `20211207144643_RemoveUnusedImageRemoteUrlProperty`
- `20211209182657_PostEntityExtension`
- `20211209194318_MoveCarLocationPropertiesFromPostToCar`
- `20220128110234_AddingModifiedOnPropertyToPostEntity`
- `20220207130708_AddingIsCoverImagePropertyToImageEntity`
- `20220208151849_AddingIsDeletedAndDeletedOnProperties`
- `20220411184416_addingAdministratorRole`
- `20220413092939_AddingIsPublicPropertyToPostEntity`
- `20260112093313_PendingModelChanges`
- `20260605225951_AddProfileImagePathToUser`

### Nova PostgreSQL migracija:
- **Ime**: `20260705055422_InitialPostgreSQLMigration`
- **Karakteristike**:
  - Koristi `NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns()`
  - Auto-increment kolone koriste PostgreSQL `IDENTITY BY DEFAULT`
  - Svi tipovi kolona podešeni za PostgreSQL (npr. `character varying`, `integer`, `boolean`, `timestamp with time zone`)
  - Decimal kolone koriste `numeric(18,2)` tip

### Provera migracije:
```bash
dotnet ef migrations list --project Data\AutoOglasi.Data\AutoOglasi.Data.csproj --startup-project Web\AutoOglasi.Web\AutoOglasi.Web.csproj
```

**Rezultat:**
```
20260705055422_InitialPostgreSQLMigration
```

## ⚙️ Konfiguracija

### appsettings*.json (`Web/AutoOglasi.Web/appsettings*.json`)

Checked-in configuration files remain sample-safe and use placeholders only.

Use PostgreSQL-style connection strings, for example:

```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Host=localhost;Port=5432;Database=autooglasi;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

For development, use user secrets or environment variables for the real connection string.

## 📚 Dokumentacija

### README.md
- ✅ Ažurirane badge-ove (SQL Server → PostgreSQL)
- ✅ Ažurirani sistemski zahtevi
- ✅ Dodata PostgreSQL konfiguracija i primeri connection string-ova
- ✅ Dodata sekcija "Migration from SQL Server to PostgreSQL" sa detaljima o promenama

## ✅ Testiranje

### Build Status:
```
Build successful
```

### Test Status:
```
Test summary: total: 36; failed: 0; succeeded: 36; skipped: 0
```

Svi testovi prolaze jer koriste EF Core InMemory provider koji je nezavisan od konkretne baze podataka.

## 🚀 Deployment Procedure

### Za produkciju:

1. **Kreirati PostgreSQL bazu:**
   ```sql
   CREATE DATABASE autooglasi;
   ```

2. **Primeniti migracije:**
   ```bash
   dotnet ef database update --project Data/AutoOglasi.Data --startup-project Web/AutoOglasi.Web --connection "Host=YOUR_HOST;Port=5432;Database=autooglasi;Username=YOUR_USER;Password=YOUR_PASSWORD"
   ```

3. **Pokrenuti aplikaciju:**
   ```bash
   dotnet run --project Web/AutoOglasi.Web
   ```

### Za migraciju postojećih podataka:

1. Eksportovati podatke iz SQL Server-a
2. Primeniti novu PostgreSQL migraciju
3. Importovati podatke u PostgreSQL (uz eventualne transformacije ako je potrebno)

## 🔍 Verifikacija

### Provera da li je baza kreirana:
```bash
dotnet ef database update --connection "Host=45.159.230.132;Port=5432;Database=autooglasi;Username=postgres;Password=fdea0e53c01bf44deb4843a5" --project Data/AutoOglasi.Data --startup-project Web/AutoOglasi.Web
```

**Rezultat:** ✅ Done.

### Provera strukture baze:

Kreirana su sva očekivana schemata uključujući:
- ASP.NET Identity tabele (AspNetUsers, AspNetRoles, itd.)
- Tabele aplikacije (Car, Post, Category, Extra, FuelType, TransmissionType, Images, itd.)
- Many-to-many tabele (CarExtras)
- Migration history tabela (`__EFMigrationsHistory`)

## 📌 Važne napomene

1. **Verzija Npgsql paketa**: Koristi se verzija 10.0.2 jer je to najnovija stabilna verzija kompatibilna sa .NET 10.0
2. **Kompatibilnost**: `HasPrecision()` API je dostupan u EF Core 5.0+ i kompatibilan je sa svim providerima
3. **Testovi**: Testovi nastavljaju da koriste InMemory provider i nisu zavisni od PostgreSQL-a
4. **Connection string format**: PostgreSQL connection string format je drugačiji od SQL Server-a (koristi `Host` umesto `Server`)
5. **Migracije**: Sve migracije su regenerisane jer PostgreSQL koristi drugačije tipove podataka i SQL sintaksu

## 🎯 Sledeći koraci (opciono)

1. **Performance tuning**: Razmotriti dodavanje indeksa specifičnih za PostgreSQL
2. **Full-text search**: PostgreSQL ima moćne full-text search mogućnosti koje mogu poboljšati pretragu
3. **Monitoring**: Postaviti monitoring za PostgreSQL bazu
4. **Backup strategija**: Implementirati automatske backup-ove PostgreSQL baze

## ✨ Zaključak

Migracija sa SQL Server-a na PostgreSQL je uspešno završena. Aplikacija sada koristi PostgreSQL kao primarnu bazu podataka sa svim funkcionalnostima očuvanim i svim testovima prolaznim.
