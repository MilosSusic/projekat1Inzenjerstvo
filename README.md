# Projekat 1 - Softversko Inženjerstvo 🎬

## 📝 Opis projekta
Ovaj projekat predstavlja desktop aplikaciju razvijenu u **C#** programskom jeziku koristeći **WPF (Windows Presentation Foundation)** tehnologiju. Aplikacija simulira sistem za upravljanje evidencijom filmova i dizajnirana je kao deo zadatka iz predmeta Softversko inženjerstvo. 

Projekat obuhvata osnovne inženjerske principe, objektno-orijentisano programiranje i grafički korisnički interfejs (GUI), uz implementaciju trajnog čuvanja podataka.

## ✨ Ključne funkcionalnosti (Features)
Sistem nudi korisnicima sledeće funkcionalnosti:
- 🔐 **Prijava (Logovanje):** Autentifikacija korisnika putem zasebnog prozora (`Logovanje.xaml`).
- ➕ **Dodavanje filmova:** Mogućnost unosa novih filmova u bazu/kolekciju podataka (`Dodavanje.xaml`).
- ✏️ **Izmena postojećih filmova:** Ažuriranje podataka o filmovima koji se već nalaze u sistemu (`Izmjena.xaml`).
- 🔍 **Detaljan prikaz:** Pregled svih specifičnih informacija o odabranom filmu (`DetaljniPrikazFilm.xaml`).
- 💾 **Serijalizacija podataka:** Učitavanje i čuvanje stanja i unosa koristeći poseban modul (`DataIO.cs`).
- 🔔 **Notifikacije:** Prikaz obaveštenja unutar aplikacije (Toast notifications) za akcije korisnika (`ToastNotification.cs`).

## 🛠️ Korišćene tehnologije
- **Jezik:** C# (97.8% koda)
- **Framework:** .NET (WPF za kreiranje grafičkog interfejsa)
- **Dizajn:** XAML (za opis UI elemenata)
- **Arhitektura:** Odvajanje prikaza (Views) od modela (Models)

## 📂 Struktura projekta
Najznačajniji direktorijumi i fajlovi u repozitorijumu uključuju:
* `pr14_2022.sln` - Solution fajl koji povezuje ceo projekat.
* `pr14_2022/` - Glavni folder izvornog koda aplikacije.
  * `MainWindow.xaml` - Početni i glavni ekran aplikacije.
  * `MODELS/` - Modeli domena aplikacije (sadrži klase kao što su `Film.cs`, `User.cs`, `DataIO.cs` i dr.).
  * `SLIKE/` - Resursi i ikonice korišćene u UI-ju aplikacije.
* `specifikacija Projekta/` - Sadrži PDF dokument sa originalnom specifikacijom i zahtevima projekta.

## 🚀 Pokretanje projekta na lokalnoj mašini

### Preduslovi
Da bi uspešno pokrenuo projekat, potrebno je da imaš instalirano:
- [Visual Studio](https://visualstudio.microsoft.com/) (preporučeno 2019 ili 2022) sa podrškom za **.NET desktop development**.

### Instalacija i pokretanje
1. Kloniraj repozitorijum na svoj računar koristeći Git:
   ```sh
   git clone https://github.com/MilosSusic/projekat1Inzenjerstvo.git
   ```
2. Pozicioniraj se u folder projekta ili odmah otvori solution fajl:
   * Dvoklik na `pr14_2022.sln` kako bi se projekat otvorio u Visual Studio-u.
3. Sačekaj da Visual Studio učita sve `NuGet` pakete (ako ih ima).
4. Pokreni aplikaciju pritiskom na dugme **Start** ili taster `F5` na tastaturi.

## 🤝 Doprinosi (Contributing)
Ovaj projekat je prvenstveno kreiran u edukativne svrhe. Ukoliko imate sugestije za unapređenje koda ili želite da ispravite eventualne greške:
1. Forkujte repozitorijum
2. Kreirajte vašu `Feature` granu (`git checkout -b feature/NovaFunkcionalnost`)
3. Komitujte izmene (`git commit -m 'Dodata nova funkcionalnost'`)
4. Pušujte promene (`git push origin feature/NovaFunkcionalnost`)
5. Otvorite *Pull Request*

## 📜 Licenca
Distribuirano pod MIT licencom.
