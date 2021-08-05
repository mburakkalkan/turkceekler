# Türkçe Ekler .NET

Bu sınıf Türkçe bir kelimeyi istenilen bir hale, zamana, kipe ve kişiye göre çekimler.

Ses uyumları, yumuşama, benzeşme, ünlü türemesi, ünlü düşmesi gibi ses olaylarını dikkate alır.

[turkcehaller] (https://code.google.com/archive/p/turkcehaller) sınıfı taban alınarak geliştirilmiştir.

## Basit İsim Çekimlemeleri
```C#
string a = TurkceEkler.Uygula("kapı", TurkceEkler.IsminHali.Bulunma); // kapıda
string b = TurkceEkler.Uygula("kitap", TurkceEkler.IsminHali.Yonelme); // kitaba
string c = TurkceEkler.Uygula("kalem", TurkceEkler.IsminHali.Ayrilma); // kalemden
string d = TurkceEkler.Uygula("köpek", TurkceEkler.IsminHali.Tamlama); // köpeğin
```

## Basit Fiil Çekimlemeleri
```C#
string a = TurkceEkler.Uygula("yap", TurkceEkler.FiilKipi.GecmisZaman, TurkceEkler.Kisiler.BirinciTekil); // yaptım
string b = TurkceEkler.Uygula("gel", TurkceEkler.FiilKipi.GecmisZaman, TurkceEkler.Kisiler.UcuncuCogul); // geldik
string c = TurkceEkler.Uygula("git", TurkceEkler.FiilKipi.GelecekZaman, TurkceEkler.Kisiler.BirinciTekil); // gideceğim
string d = TurkceEkler.Uygula("ver", TurkceEkler.FiilKipi.GenisZamanOlumsuz, TurkceEkler.Kisiler.IkinciTekil); // yapmazsın
string e = TurkceEkler.Uygula("koş", TurkceEkler.FiilKipi.Yeterlilik, TurkceEkler.Kisiler.UcuncuCogul); // koşabilirler
string f = TurkceEkler.Uygula("sor", TurkceEkler.FiilKipi.Istek, TurkceEkler.Kisiler.IkinciTekil); // sorsan
string g = TurkceEkler.Uygula("yaz", TurkceEkler.FiilKipi.MisliGecmisZamanOlumsuz, TurkceEkler.Kisiler.UcuncuTekil); // yazmamış
string h = TurkceEkler.Uygula("ol", TurkceEkler.FiilKipi.SimdikiZaman, TurkceEkler.Kisiler.UcuncuCogul); // oluyoruz
string i = TurkceEkler.Uygula("kaç", TurkceEkler.FiilKipi.Emir, TurkceEkler.Kisiler.IkinciTekil); // kaçsın
```

## Birleşik Zaman Çekimlemeleri

```C#
string a = TurkceEkler.Uygula("yap", TurkceEkler.FiilKipi.MisliGecmisZaman, TurkceEkler.FiilKipi.GecmisZaman, TurkceEkler.Kisiler.BirinciTekil); // yapmıştım
string b = TurkceEkler.Uygula("gel", TurkceEkler.FiilKipi.SimdikiZaman, TurkceEkler.FiilKipi.MisliGecmisZaman, TurkceEkler.Kisiler.UcuncuCogul); // geliyormuşuz
string c = TurkceEkler.Uygula("git", TurkceEkler.FiilKipi.GelecekZaman, TurkceEkler.FiilKipi.GecmisZaman, TurkceEkler.Kisiler.BirinciTekil); // gidecektim
```
