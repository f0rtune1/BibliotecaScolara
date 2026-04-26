````markdown name=README.md
# Biblioteca Școlară - Sistem de Gestiune

Un sistem complet de gestiune pentru biblioteci școlare, dezvoltat în C# cu Windows Forms și SQL Server.

## 📋 Descriere Proiect

Aplicația permite gestionarea completă a unei biblioteci școlare, incluzând:
- Gestiunea cărților și exemplarelor
- Înregistrarea autorilor și editurilor
- Administrarea elevilor
- Înregistrarea și urmărirea împrumuturilor
- Rapoarte și statistici

## 🎯 Funcionalități Principale

### 1. **Gestiune Cărți**
- Adăugare, editare, ștergere de cărți
- Asociere autor-carte și editură-carte
- Categorisare cărți
- Căutare avansată după titlu, autor, ISBN

### 2. **Gestiune Exemplare**
- Înregistrare exemplare (copii) pentru fiecare carte
- Coduri de inventar unice
- Urmărire stare exemplar (Bună, Deteriorată, Pierdută)
- Preț și data achiziției

### 3. **Gestiune Elevi**
- Înregistrare și administrare elevi
- Clase și contacte
- Status elev (Activ, Inactiv, Suspendat)
- Verificare active loans

### 4. **Împrumuturi**
- Înregistrare împrumuturi
- Termen implicit 30 zile
- Verificare exemplare disponibile
- Returnare cărți cu observații
- Prelungire termene
- Rapoarte pentru cărți întârziate

### 5. **Rapoarte**
- Raport cărți active
- Raport împrumuturi active și întârziate
- Statistici biblotecă
- Disponibilitate exemplare

## 🛠️ Tehnologii Utilizate

- **Limbaj**: C# .NET Framework 4.7.2
- **Interfață**: Windows Forms
- **Bază de Date**: SQL Server Express
- **ORM**: ADO.NET (Direct SQL)
- **Pattern**: Manager Pattern para operații CRUD

## 📦 Structură Proiect

```
BibliotecaScolara/
├── Models/                 # Clase de model
│   ├── Autor.cs
│   ├── Editura.cs
│   ├── Carte.cs
│   ├── Exemplar.cs
│   ├── Elev.cs
│   ├── Imprumut.cs
│   └── Categorie.cs
├── Database/              # Conexiune și scripts
│   ├── DatabaseConnection.cs
│   └── CreateDatabase.sql
├── Managers/              # Logică CRUD
│   ├── AutorManager.cs
│   ├── EdituraManager.cs
│   ├── CarteManager.cs
│   ├── CategorieManager.cs
│   ├── ExemplarManager.cs
│   ├── ElevManager.cs
│   └── ImprumutManager.cs
├── Utilities/            # Clase utility
│   ├── Constante.cs
│   ├── Mesaje.cs (viitor)
│   └── Validari.cs (viitor)
├── UI/                   # Forme Windows Forms (viitor)
│   ├── FrmAutori.cs
│   ├── FrmCarti.cs
│   ├── FrmElevi.cs
│   ├── FrmImprumturi.cs
│   └── FrmMain.cs
├── App.config           # Configurare aplicație
├── Program.cs           # Entry point
└── BibliotecaScolara.csproj
```

## 🚀 Instrucțiuni Instalare

### Cerințe Sistem
- Windows 7 sau mai nou
- SQL Server Express 2019 sau mai nou
- .NET Framework 4.7.2 sau mai nou
- Visual Studio 2019+ (pentru dezvoltare)

### Pași Instalare

1. **Clonează repository-ul**
   ```bash
   git clone https://github.com/f0rtune1/BibliotecaScolara.git
   cd BibliotecaScolara
   ```

2. **Creează baza de date**
   - Deschide SQL Server Management Studio
   - Deschide fișierul `Database/CreateDatabase.sql`
   - Execută script-ul (F5)
   - Baza de date `BibliotecaScolara` va fi creată cu toate tabelele

3. **Configurează connection string**
   - Edită `App.config`
   - Modifica linia:
   ```xml
   <add name="BibliotecaDB" 
        connectionString="Server=YOUR_SERVER;Database=BibliotecaScolara;Trusted_Connection=true;" 
        providerName="System.Data.SqlClient" />
   ```
   - Înlocuiește `YOUR_SERVER` cu instanța SQL Server (ex: `localhost\SQLEXPRESS`)

4. **Compilează proiectul**
   - Deschide `BibliotecaScolara.sln` în Visual Studio
   - Build > Build Solution (Ctrl+Shift+B)

5. **Rulează aplicația**
   - Debug > Start Debugging (F5)

## 📊 Schema Bază de Date

### Tabel Autori
```sql
- IDAutor (PK)
- Nume, Prenume
- DataNasterii
- Nationalitate
- BiografieBrieșă
- DataAdaugarii
```

### Tabel Edituri
```sql
- IDEditura (PK)
- NumeEditura (UNIQUE)
- Adresa, Telefon, Email
- DataAdaugarii
```

### Tabel Categorii
```sql
- IDCategorie (PK)
- NumeCategorie (UNIQUE)
- Descriere
```

### Tabel Cărți
```sql
- IDCarte (PK)
- Titlu
- IDAutor (FK), IDEditura (FK), IDCategorie (FK)
- AnPublicarii, ISBN (UNIQUE), NrPagini
- DataAdaugarii
```

### Tabel Exemplare
```sql
- IDExemplar (PK)
- IDCarte (FK)
- CoduInventar (UNIQUE)
- StareExemplar (Bună, Deteriorată, Pierdută)
- DataAchizitiei, Pret
- DataAdaugarii
```

### Tabel Elevi
```sql
- IDElev (PK)
- Nume, Prenume, Clasa
- Email, Telefon
- DataInscrierii, Status
- DataAdaugarii
```

### Tabel Împrumuturi
```sql
- IDImprumut (PK)
- IDElev (FK), IDExemplar (FK)
- DataImprumut, DataScadenta, DataRestituire
- Status (Activ, Încheiat, Întârziat)
- Observatii
- DataAdaugarii
```

## 💻 Utilizare API Managers

### AutorManager
```csharp
// Obține toți autorii
List<Autor> autori = AutorManager.GetAll();

// Adaugă autor
Autor autor = new Autor { Nume = "Popescu", Prenume = "Ion" };
AutorManager.Insert(autor);

// Actualizează
AutorManager.Update(autor);

// Șterge (verifică dacă are cărți)
AutorManager.Delete(autorId);

// Caută
List<Autor> rezultate = AutorManager.Search("Popescu");
```

### CarteManager
```csharp
// Obține toate cărțile
List<Carte> carti = CarteManager.GetAll();

// Adaugă carte nouă
Carte carte = new Carte 
{ 
    Titlu = "1984", 
    IDAutor = 1, 
    IDEditura = 1, 
    IDCategorie = 1,
    ISBN = "978-1-234-56789-0"
};
CarteManager.Insert(carte);

// Cărți din categorie
List<Carte> stiinta = CarteManager.GetByCategorie(1);
```

### ElevManager
```csharp
// Obține elevi activi
List<Elev> elevi = ElevManager.GetActive();

// Elevi din clasa
List<Elev> clasa9A = ElevManager.GetByClasa("9A");

// Caută elev
List<Elev> rezultate = ElevManager.Search("Popescu");
```

### ImprumutManager
```csharp
// Crezează împrumut
Imprumut imprumut = new Imprumut 
{ 
    IDElev = 1, 
    IDExemplar = 1,
    DataImprumut = DateTime.Today
};
ImprumutManager.Insert(imprumut);

// Împrumuturi active ale elevului
List<Imprumut> active = ImprumutManager.GetActiveByElev(elevId);

// Cărți întârziate
List<Imprumut> intarziate = ImprumutManager.GetOverdueLoans();

// Returnează carte
ImprumutManager.ReturnBook(imprumutId, "Carte în stare bună");

// Prelungește termen
ImprumutManager.ExtendLoan(imprumutId);
```

## 🔐 Validări și Verificări

- **ISBN unic** în cărți
- **Cod inventar unic** pentru exemplare
- **Exemplar disponibil** pentru împrumut (nu e deja împrumutat)
- **Elev activ** pentru a putea împrumuta
- **Exemplar în stare bună** pentru împrumut
- **Autor cu cărți** nu poate fi șters
- **Editura cu cărți** nu poate fi ștearsă
- **Elev cu împrumuturi active** nu poate fi șters

## 📝 Constante Disponibile

```csharp
// Stări exemplar
Constante.StareExemplar.BUNA
Constante.StareExemplar.DETERIORATA
Constante.StareExemplar.PIERDUTA

// Stări împrumut
Constante.StareImprumut.ACTIV
Constante.StareImprumut.INCHEIAT
Constante.StareImprumut.INTARZIAT

// Stări elev
Constante.StareElev.ACTIV
Constante.StareElev.INACTIV
Constante.StareElev.SUSPENDAT

// Zile
Constante.ZILE_IMPRUMUT_DEFAULT = 30
```

## 🔄 Codebase Flow

1. **UI Layer** (Viitor) → Primește input utilizator
2. **Manager Layer** → Logică CRUD și validări
3. **Database Layer** → DatabaseConnection execută queries SQL
4. **Model Layer** → Clase de date

## 📋 Proceduri Stocate SQL

- `sp_RaportImprumturiActive` - Raport împrumuturi active
- `sp_RaportCarter` - Raport cărți cu statistici exemplare

## 🚧 Viitoare Îmbunătățiri

- [ ] Interfață Windows Forms completă
- [ ] Sistem de login și roluri (admin, bibliotecar)
- [ ] Notificări email pentru cărți întârziate
- [ ] Export rapoarte în PDF/Excel
- [ ] Cod bare pentru exemplare
- [ ] Rezervare cărți
- [ ] Amendă pentru întârziere
- [ ] Backup/Restore bază de date
- [ ] API REST pentru integrări
- [ ] Sincronizare cu sistemul de notare școlar

## 🤝 Contribuții

Contribuțiile sunt binevenite! Vă rugăm:
1. Faceți fork la repository
2. Creați branch pentru feature (`git checkout -b feature/AmazingFeature`)
3. Commit schimbări (`git commit -m 'Add some AmazingFeature'`)
4. Push la branch (`git push origin feature/AmazingFeature`)
5. Deschideți Pull Request

## 📄 Licență

Acest proiect este sub licență MIT. Vedeți fișierul LICENSE pentru detalii.

## 👤 Autor

**Fortune** - [@f0rtune1](https://github.com/f0rtune1)

## 📧 Contact și Support

Pentru probleme, întrebări sau sugestii:
- Deschideți o problemă pe GitHub Issues
- Contactați autorul pe email: papraileanuadrian@gmail.com

## 📚 Resurse Utile

- [Documentație SQL Server](https://docs.microsoft.com/sql/)
- [Documentație C# și .NET](https://docs.microsoft.com/dotnet/)
- [Windows Forms Tutorials](https://docs.microsoft.com/dotnet/desktop/winforms/)
- [ADO.NET Data Access](https://docs.microsoft.com/dotnet/framework/data/adonet/)

---

**Versiune**: 1.0.0  
**Status**: În dezvoltare  
**Ultima actualizare**: 2026-04-26
````
