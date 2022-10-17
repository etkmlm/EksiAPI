# EksiAPI
Hafif bir Ekşi API'si.

## Genel Bilgilendirme
- Proje .NET 5.0 ile çalışıyor fakat isterseniz .NET Standard veya .NET Core gibi diğer çerçevelere de geçirebilirsiniz.
- Proje, HTTP tarama metoduna dayanmaktadır. Yani sayfa yapısı değişirse bir süreliğine kullanılamayabilir.

## Temeller

Hepsinden önce, başlıkları ve girdileri çekmek için servisleri oluşturmamız gerekiyor.

```
var threadService = new ThreadService();
var entryService = new EntryService();
var userService = new UserService();
```

### Girdi Servisi (EntryService)

#### Arama

Girdilerde aramak için Search() fonksiyonunu kullanabilirsiniz. IEnumerable<Entry> döndürür ve girdinin id'sini içeren bir parametre alır.

```
var entries = entryService.Search("53401889");
```

#### DEBE'leri Çekme

DEBE'leri çekmek için GetDEBE() fonksiyonunu kullanabilirsiniz. IEnumerable<Entry> döndürür.

```
var debes = entryService.GetDEBE();
```

#### Özel Bir URL'den Girdileri Çekme

Başlıktan girdileri çekmek için GetEntriesFromPage() fonksiyonunu kullanabilirsiniz. IEnumerable<Entry> döndürür ve başlığın URL'sini alır.

```
var entries = entryService.GetEntriesFromPage("https://eksisozluk.com/applein-kasasindaki-nakit-203-milyar-dolar--4862121");
```

### Başlık Servisi (ThreadService)

#### Arama

Başlıklarda aramak için Search() fonksiyonunu kullanabilirsiniz. IEnumerable<Thread> döndürür ve sorguyu içeren bir parametre alır.

```
var threads = threadService.Search("apple'ın kasasındaki nakit 203 milyar dolar");
```

#### Başlığı Çekme

Özel bir başlığı çekmek için GetThread() fonksiyonunu kullanabilirsiniz. Tek bir başlık döndürür ve başlığın URL'sini alır.

```
var thread = threadService.GetThread("https://eksisozluk.com/applein-kasasindaki-nakit-203-milyar-dolar--4862121");
```

#### Gündem Başlıklarından Çekme

Başlıkları gündem başlıklarından çekmek için GetFromTopics() fonksiyonunu kullanabilirsiniz. IEnumerable<Thread> döndürür ve iki parametre alır (kategori ve sayfa sayısı limiti).
Not: Sayfa limiti varsayılan olarak 5'tir.

```
var threads1 = threadService.GetFromTopics(ThreadCategory.POPULAR);
// veya
var threads2 = threadService.GetFromTopics(ThreadCategory.POPULAR, 3);
```

### Kullanıcı Servisi (UserService)
  
#### Arama
  
Kullanıcılar içinde aramak için Search() fonksiyonunu kullanabilirsiniz. Tek bir kullanıcı döndürür ve arama sorgusunu alır.

```
var user = userService.Search("ssg");
```

## Yapı

### Kullanıcı (User)

- **Integer** ID
- **String** Username
- **String** Photo
- **Boolean** IsNoob
- **Integer** FollowerCount
- **Integer** FollowCount
- **Integer** EntryCount

### Girdi (Entry)

- **Integer** ID
- **User** Writer
- **DateTime** Date
- **String** Content
- **Integer** FavCount
- **Thread** Thread

### Başlık (Thread)

- **Integer** ID
- **String** URL
- **Integer** PageCount
- **Integer** EntryCount
- **String** Title
- **IEnumerable<Entry>** Entries
- **DateTime** Date

### Başlık Kategorisi (ThreadCategory) (Enum)

- POPULAR
- TODAY (ESKİDİ)

### Girdi Kategorisi (EntryCategory) (Enum)

- DEBE
