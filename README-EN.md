# EksiAPI
A lightweight Eksi API.

## Informations
- Project works with .NET 5.0 but if you want, you can change it to other frameworks like .NET Standard or .NET Core.
- It based on http crawling. So, if page struct change, it can unusable for a while.

## Basics

First of all, we have to create services to retrieve data.

```
var threadService = new ThreadService();
var entryService = new EntryService();
```

### Entry Service

#### Searching

To search in entries, you can use Search() function. It returns IEnumerable<Entry> and takes an object that contains entry id.

```
var entries = entryService.Search("53401889");
```

#### Getting DEBE's

To retrieve DEBE's, you can use GetDEBE() function. It returns IEnumerable<Entry>.

```
var debes = entryService.GetDEBE();
```

#### Getting Entries From Specified URL

To retrieve entries from thread page, you can use GetEntriesFromPage() function. It returns IEnumerable<Entry> and takes link of the page.

```
var entries = entryService.GetEntriesFromPage("https://eksisozluk.com/applein-kasasindaki-nakit-203-milyar-dolar--4862121");
```

### Thread Service

#### Searching

To search in threads, you can use Search() function. It returns IEnumerable<Thread> and takes an object that contains query.

```
var threads = threadService.Search("apple'ın kasasındaki nakit 203 milyar dolar");
```

#### Getting The Thread

To get specified thread, you can use GetThread() function. It returns a single thread and takes the URL of the thread.

```
var thread = threadService.GetThread("https://eksisozluk.com/applein-kasasindaki-nakit-203-milyar-dolar--4862121");
```

#### Getting From Topics

To retrieve threads from a specified topic, you can use GetFromTopics() function. It returns IEnumerable<Thread> and two parameter (category and page limit).
Note: Page limit is 5 as default.

```
var threads1 = threadService.GetFromTopics(ThreadCategory.POPULAR);
// or
var threads2 = threadService.GetFromTopics(ThreadCategory.POPULAR, 3);
```

## Structure

### User

- **Integer** ID
- **String** Username
- **String** Photo
- **Boolean** IsNoob
- **Integer** FollowerCount
- **Integer** FollowCount
- **Integer** EntryCount

### Entry

- **Integer** ID
- **User** Writer
- **DateTime** Date
- **String** Content
- **Integer** FavCount
- **Thread** Thread

### Thread

- **Integer** ID
- **String** URL
- **Integer** PageCount
- **Integer** EntryCount
- **String** Title
- **IEnumerable<Entry>** Entries
- **DateTime** Date
