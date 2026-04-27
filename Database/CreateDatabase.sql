-- Script creație bază de date Biblioteca Școlară
-- Creează baza de date și toate tabelele necesare

-- Creează baza de date dacă nu există
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BibliotecaScolara')
BEGIN
    CREATE DATABASE BibliotecaScolara;
END
GO

USE BibliotecaScolara;
GO

-- Tabel Utilizatori (pentru autentificare)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Utilizatori')
BEGIN
    CREATE TABLE Utilizatori (
        IDUtilizator INT PRIMARY KEY IDENTITY(1,1),
        NumeUtilizator NVARCHAR(50) NOT NULL UNIQUE,
        ParolaHash NVARCHAR(255) NOT NULL,
        Rol NVARCHAR(20) NOT NULL DEFAULT 'bibliotecar',
        Email NVARCHAR(100) NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE()
    );
    
    -- Insert default admin user (password: admin123)
    -- SHA256 of 'admin123' = 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9
    INSERT INTO Utilizatori (NumeUtilizator, ParolaHash, Rol, Email)
    VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'admin', 'admin@biblioteca.ro');
END
GO

-- Tabel Autori
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Autori')
BEGIN
    CREATE TABLE Autori (
        IDAutor INT PRIMARY KEY IDENTITY(1,1),
        Nume NVARCHAR(100) NOT NULL,
        Prenume NVARCHAR(100) NOT NULL,
        DataNasterii DATE NULL,
        Nationalitate NVARCHAR(50) NULL,
        BiografieBrieșă NVARCHAR(500) NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE()
    );
    
    CREATE INDEX IX_Autori_Nume ON Autori(Nume, Prenume);
END
GO

-- Tabel Edituri
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Edituri')
BEGIN
    CREATE TABLE Edituri (
        IDEditura INT PRIMARY KEY IDENTITY(1,1),
        NumeEditura NVARCHAR(200) NOT NULL UNIQUE,
        Adresa NVARCHAR(200) NULL,
        Telefon NVARCHAR(20) NULL,
        Email NVARCHAR(100) NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE()
    );
    
    CREATE INDEX IX_Edituri_Nume ON Edituri(NumeEditura);
END
GO

-- Tabel Categorii
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Categorii')
BEGIN
    CREATE TABLE Categorii (
        IDCategorie INT PRIMARY KEY IDENTITY(1,1),
        NumeCategorie NVARCHAR(100) NOT NULL UNIQUE,
        Descriere NVARCHAR(300) NULL
    );
    
    INSERT INTO Categorii (NumeCategorie, Descriere) VALUES
        ('Științe', 'Cărți despre știință și natura'),
        ('Literatură', 'Cărți de beletristică și literatură'),
        ('Matematică', 'Cărți despre matematică'),
        ('Istorie', 'Cărți despre istorie'),
        ('Geografie', 'Cărți despre geografie'),
        ('Limbă Română', 'Cărți pentru studiul limbii române'),
        ('Educație Fizică', 'Cărți despre sport și educație fizică'),
        ('Arte', 'Cărți despre arte și design'),
        ('Referință', 'Dicționare, enciclopedii, atlase'),
        ('Ficțiune Științifică', 'Cărți de ficțiune științifică');
END
GO

-- Tabel Cărți
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Carti')
BEGIN
    CREATE TABLE Carti (
        IDCarte INT PRIMARY KEY IDENTITY(1,1),
        Titlu NVARCHAR(200) NOT NULL,
        IDAutor INT NOT NULL,
        IDEditura INT NOT NULL,
        IDCategorie INT NOT NULL,
        AnPublicarii INT NULL,
        ISBN NVARCHAR(20) UNIQUE NULL,
        NrPagini INT NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (IDAutor) REFERENCES Autori(IDAutor),
        FOREIGN KEY (IDEditura) REFERENCES Edituri(IDEditura),
        FOREIGN KEY (IDCategorie) REFERENCES Categorii(IDCategorie)
    );
    
    CREATE INDEX IX_Carti_Titlu ON Carti(Titlu);
    CREATE INDEX IX_Carti_ISBN ON Carti(ISBN);
    CREATE INDEX IX_Carti_Autor ON Carti(IDAutor);
    CREATE INDEX IX_Carti_Categorie ON Carti(IDCategorie);
END
GO

-- Tabel Exemplare
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Exemplare')
BEGIN
    CREATE TABLE Exemplare (
        IDExemplar INT PRIMARY KEY IDENTITY(1,1),
        IDCarte INT NOT NULL,
        CoduInventar NVARCHAR(50) NOT NULL UNIQUE,
        StareExemplar NVARCHAR(50) DEFAULT 'Bună',
        DataAchizitiei DATE NOT NULL,
        Pret DECIMAL(8,2) NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (IDCarte) REFERENCES Carti(IDCarte) ON DELETE CASCADE
    );
    
    CREATE INDEX IX_Exemplare_Cod ON Exemplare(CoduInventar);
    CREATE INDEX IX_Exemplare_Carte ON Exemplare(IDCarte);
    CREATE INDEX IX_Exemplare_Stare ON Exemplare(StareExemplar);
END
GO

-- Tabel Elevi
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Elevi')
BEGIN
    CREATE TABLE Elevi (
        IDElev INT PRIMARY KEY IDENTITY(1,1),
        Nume NVARCHAR(100) NOT NULL,
        Prenume NVARCHAR(100) NOT NULL,
        Clasa NVARCHAR(10) NOT NULL,
        Email NVARCHAR(100) NULL,
        Telefon NVARCHAR(20) NULL,
        DataInscrierii DATE DEFAULT CAST(GETDATE() AS DATE),
        Status NVARCHAR(50) DEFAULT 'Activ',
        DataAdaugarii DATETIME DEFAULT GETDATE()
    );
    
    CREATE INDEX IX_Elevi_Nume ON Elevi(Nume, Prenume);
    CREATE INDEX IX_Elevi_Clasa ON Elevi(Clasa);
    CREATE INDEX IX_Elevi_Status ON Elevi(Status);
END
GO

-- Tabel Împrumuturi
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Imprumturi')
BEGIN
    CREATE TABLE Imprumturi (
        IDImprumut INT PRIMARY KEY IDENTITY(1,1),
        IDElev INT NOT NULL,
        IDExemplar INT NOT NULL,
        DataImprumut DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
        DataScadenta DATE NOT NULL,
        DataRestituire DATE NULL,
        Status NVARCHAR(50) DEFAULT 'Activ',
        Observatii NVARCHAR(300) NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (IDElev) REFERENCES Elevi(IDElev),
        FOREIGN KEY (IDExemplar) REFERENCES Exemplare(IDExemplar)
    );
    
    CREATE INDEX IX_Imprumturi_Elev ON Imprumturi(IDElev);
    CREATE INDEX IX_Imprumturi_Exemplar ON Imprumturi(IDExemplar);
    CREATE INDEX IX_Imprumturi_Status ON Imprumturi(Status);
    CREATE INDEX IX_Imprumturi_DataScadenta ON Imprumturi(DataScadenta);
    CREATE INDEX IX_Imprumturi_Activ ON Imprumturi(DataRestituire) WHERE DataRestituire IS NULL;
END
GO

-- Crează proceduri stocate pentru rapoarte

-- Procedură pentru raportul de împrumuturi active
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_RaportImprumturiActive')
    DROP PROCEDURE sp_RaportImprumturiActive;
GO

CREATE PROCEDURE sp_RaportImprumturiActive
AS
BEGIN
    SELECT 
        i.IDImprumut,
        e.Nume + ' ' + e.Prenume AS NumeElev,
        e.Clasa,
        c.Titlu,
        ex.CoduInventar,
        i.DataImprumut,
        i.DataScadenta,
        DATEDIFF(DAY, GETDATE(), i.DataScadenta) AS ZileRamase,
        CASE 
            WHEN DATEDIFF(DAY, GETDATE(), i.DataScadenta) < 0 THEN 'ÎNTÂRZIAT'
            WHEN DATEDIFF(DAY, GETDATE(), i.DataScadenta) <= 3 THEN 'URGENT'
            ELSE 'OK'
        END AS Statut
    FROM Imprumturi i
    JOIN Elevi e ON i.IDElev = e.IDElev
    JOIN Exemplare ex ON i.IDExemplar = ex.IDExemplar
    JOIN Carti c ON ex.IDCarte = c.IDCarte
    WHERE i.DataRestituire IS NULL
    ORDER BY i.DataScadenta;
END
GO

-- Procedură pentru cărți din bibliotecă
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_RaportCarter')
    DROP PROCEDURE sp_RaportCarter;
GO

CREATE PROCEDURE sp_RaportCarter
AS
BEGIN
    SELECT 
        c.IDCarte,
        c.Titlu,
        a.Nume + ' ' + a.Prenume AS Autor,
        ed.NumeEditura,
        cat.NumeCategorie,
        c.AnPublicarii,
        c.ISBN,
        c.NrPagini,
        COUNT(ex.IDExemplar) AS TotalExemplare,
        SUM(CASE WHEN ex.StareExemplar = 'Bună' THEN 1 ELSE 0 END) AS ExemplareBune,
        SUM(CASE WHEN ex.StareExemplar = 'Deteriorată' THEN 1 ELSE 0 END) AS ExemplareDeteriorate,
        SUM(CASE WHEN ex.IDExemplar IN (
            SELECT IDExemplar FROM Imprumturi WHERE DataRestituire IS NULL
        ) THEN 1 ELSE 0 END) AS ExemplareImprumutate
    FROM Carti c
    JOIN Autori a ON c.IDAutor = a.IDAutor
    JOIN Edituri ed ON c.IDEditura = ed.IDEditura
    JOIN Categorii cat ON c.IDCategorie = cat.IDCategorie
    LEFT JOIN Exemplare ex ON c.IDCarte = ex.IDCarte
    GROUP BY c.IDCarte, c.Titlu, a.Nume, a.Prenume, ed.NumeEditura, cat.NumeCategorie, 
             c.AnPublicarii, c.ISBN, c.NrPagini
    ORDER BY c.Titlu;
END
GO

PRINT 'Baza de date BibliotecaScolara a fost creata cu succes!'
PRINT 'Tabelele create: Utilizatori, Autori, Edituri, Categorii, Carti, Exemplare, Elevi, Imprumturi'
PRINT 'Utilizator implicit: admin / Parola: admin123'
