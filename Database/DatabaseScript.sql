-- =====================================================
-- SCRIPT BAZĂ DE DATE - BIBLIOTECĂ ȘCOLARĂ
-- =====================================================

-- Crearea bazei de date
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BibliotecaScolara')
BEGIN
    CREATE DATABASE BibliotecaScolara;
END
GO

USE BibliotecaScolara;
GO

-- =====================================================
-- TABEL 0: UTILIZATORI (pentru autentificare)
-- =====================================================
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
    INSERT INTO Utilizatori (NumeUtilizator, ParolaHash, Rol, Email)
    VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'admin', 'admin@biblioteca.ro');
END
GO

-- =====================================================
-- TABEL 1: AUTORI
-- =====================================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Autori')
BEGIN
    CREATE TABLE Autori (
        IDAutor INT PRIMARY KEY IDENTITY(1,1),
        Nume NVARCHAR(100) NOT NULL,
        Prenume NVARCHAR(100) NOT NULL,
        DataNasterii DATE NULL,
        Nationalitate NVARCHAR(100) NULL,
        BiografieBrieșă NVARCHAR(500) NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE()
    );
    CREATE INDEX IX_Autori_Nume ON Autori(Nume, Prenume);
END
GO

-- =====================================================
-- TABEL 2: EDITURI
-- =====================================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Edituri')
BEGIN
    CREATE TABLE Edituri (
        IDEditura INT PRIMARY KEY IDENTITY(1,1),
        NumeEditura NVARCHAR(150) NOT NULL UNIQUE,
        Adresa NVARCHAR(200) NULL,
        Telefon NVARCHAR(20) NULL,
        Email NVARCHAR(100) NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE()
    );
    CREATE INDEX IX_Edituri_Nume ON Edituri(NumeEditura);
END
GO

-- =====================================================
-- TABEL 3: CATEGORII (Nomenclator)
-- =====================================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Categorii')
BEGIN
    CREATE TABLE Categorii (
        IDCategorie INT PRIMARY KEY IDENTITY(1,1),
        NumeCategorie NVARCHAR(100) NOT NULL UNIQUE,
        Descriere NVARCHAR(300) NULL
    );
END
GO

-- =====================================================
-- TABEL 4: CĂRȚI
-- =====================================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Carti')
BEGIN
    CREATE TABLE Carti (
        IDCarte INT PRIMARY KEY IDENTITY(1,1),
        Titlu NVARCHAR(200) NOT NULL,
        IDAutor INT NOT NULL,
        IDEditura INT NOT NULL,
        IDCategorie INT NOT NULL,
        AnPublicarii INT NULL,
        ISBN NVARCHAR(20) UNIQUE NOT NULL,
        NrPagini INT NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (IDAutor) REFERENCES Autori(IDAutor),
        FOREIGN KEY (IDEditura) REFERENCES Edituri(IDEditura),
        FOREIGN KEY (IDCategorie) REFERENCES Categorii(IDCategorie)
    );
    CREATE INDEX IX_Carti_Titlu ON Carti(Titlu);
    CREATE INDEX IX_Carti_ISBN ON Carti(ISBN);
    CREATE INDEX IX_Carti_Autor ON Carti(IDAutor);
END
GO

-- =====================================================
-- TABEL 5: EXEMPLARE (Copii fizice)
-- =====================================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Exemplare')
BEGIN
    CREATE TABLE Exemplare (
        IDExemplar INT PRIMARY KEY IDENTITY(1,1),
        IDCarte INT NOT NULL,
        CoduInventar NVARCHAR(50) UNIQUE NOT NULL,
        StareExemplar NVARCHAR(20) DEFAULT 'Bună',
        DataAchizitiei DATE NOT NULL,
        Pret DECIMAL(10, 2) NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (IDCarte) REFERENCES Carti(IDCarte) ON DELETE CASCADE
    );
    CREATE INDEX IX_Exemplare_Carte ON Exemplare(IDCarte);
    CREATE INDEX IX_Exemplare_Cod ON Exemplare(CoduInventar);
END
GO

-- =====================================================
-- TABEL 6: ELEVI
-- =====================================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Elevi')
BEGIN
    CREATE TABLE Elevi (
        IDElev INT PRIMARY KEY IDENTITY(1,1),
        Nume NVARCHAR(100) NOT NULL,
        Prenume NVARCHAR(100) NOT NULL,
        Clasa NVARCHAR(20) NOT NULL,
        Email NVARCHAR(100) UNIQUE NULL,
        Telefon NVARCHAR(20) NULL,
        DataInscrierii DATE NOT NULL,
        Status NVARCHAR(20) DEFAULT 'Activ',
        DataAdaugarii DATETIME DEFAULT GETDATE()
    );
    CREATE INDEX IX_Elevi_Nume ON Elevi(Nume, Prenume);
    CREATE INDEX IX_Elevi_Clasa ON Elevi(Clasa);
END
GO

-- =====================================================
-- TABEL 7: ÎMPRUMUTURI
-- =====================================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Imprumturi')
BEGIN
    CREATE TABLE Imprumturi (
        IDImprumut INT PRIMARY KEY IDENTITY(1,1),
        IDElev INT NOT NULL,
        IDExemplar INT NOT NULL,
        DataImprumut DATE NOT NULL,
        DataScadenta DATE NOT NULL,
        DataRestituire DATE NULL,
        Status NVARCHAR(20) DEFAULT 'Activ',
        Observatii NVARCHAR(300) NULL,
        DataAdaugarii DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (IDElev) REFERENCES Elevi(IDElev),
        FOREIGN KEY (IDExemplar) REFERENCES Exemplare(IDExemplar) ON DELETE CASCADE
    );
    CREATE INDEX IX_Imprumturi_Elev ON Imprumturi(IDElev);
    CREATE INDEX IX_Imprumturi_Exemplar ON Imprumturi(IDExemplar);
    CREATE INDEX IX_Imprumturi_Status ON Imprumturi(Status);
END
GO

-- =====================================================
-- INSERARE DATE INIȚIALE
-- =====================================================

-- Categorii (nomenclator)
IF NOT EXISTS (SELECT * FROM Categorii WHERE NumeCategorie = 'Ficțiune')
BEGIN
    INSERT INTO Categorii (NumeCategorie, Descriere) VALUES
    ('Ficțiune', 'Romane și cărți de ficțiune'),
    ('Non-ficțiune', 'Cărți educative și informative'),
    ('Manuale', 'Manuale școlare'),
    ('Referință', 'Enciclopedii și dicționare'),
    ('Povestea Copiilor', 'Cărți pentru copii și adolescenți'),
    ('Istorie', 'Cărți de istorie'),
    ('Știință', 'Cărți de știință și tehnologie');
END
GO

-- Autori exemplu
IF NOT EXISTS (SELECT * FROM Autori WHERE Nume = 'Caragiale')
BEGIN
    INSERT INTO Autori (Nume, Prenume, Nationalitate, BiografieBrieșă) VALUES
    ('Caragiale', 'Ion Luca', 'Română', 'Dramaturg și scriitor român'),
    ('Stendhal', '', 'Franceză', 'Scriitor francez'),
    ('Tolstoi', 'Lev', 'Rusă', 'Scriitor rus, autorul Războiului și păcii'),
    ('Asimov', 'Isaac', 'Americană', 'Scriitor de science fiction');
END
GO

-- Edituri exemplu
IF NOT EXISTS (SELECT * FROM Edituri WHERE NumeEditura = 'Humanitas')
BEGIN
    INSERT INTO Edituri (NumeEditura, Adresa, Telefon, Email) VALUES
    ('Humanitas', 'București', '021-1234567', 'contact@humanitas.ro'),
    ('Polirom', 'Iași', '0232-123456', 'info@polirom.ro'),
    ('Nemira', 'București', '021-9876543', 'contact@nemira.ro');
END
GO

-- Cărți exemplu
IF NOT EXISTS (SELECT * FROM Carti WHERE ISBN = '978-3-16-148410-0')
BEGIN
    INSERT INTO Carti (Titlu, IDAutor, IDEditura, IDCategorie, AnPublicarii, ISBN, NrPagini) VALUES
    ('O Scrisoare Pierdută', 1, 1, 1, 1884, '978-3-16-148410-0', 256),
    ('Roșu și Negru', 2, 2, 1, 1830, '978-3-16-148410-1', 400),
    ('Război și Pace', 3, 3, 1, 1869, '978-3-16-148410-2', 1200);
END
GO

-- Exemplare
IF NOT EXISTS (SELECT * FROM Exemplare WHERE CoduInventar = 'INV-001-001')
BEGIN
    INSERT INTO Exemplare (IDCarte, CoduInventar, StareExemplar, DataAchizitiei, Pret) VALUES
    (1, 'INV-001-001', 'Bună', '2023-01-15', 45.99),
    (1, 'INV-001-002', 'Bună', '2023-01-15', 45.99),
    (2, 'INV-002-001', 'Bună', '2023-02-20', 52.50),
    (3, 'INV-003-001', 'Bună', '2023-03-10', 89.99);
END
GO

-- Elevi exemplu
IF NOT EXISTS (SELECT * FROM Elevi WHERE Email = 'ana.popescu@school.com')
BEGIN
    INSERT INTO Elevi (Nume, Prenume, Clasa, Email, Telefon, DataInscrierii, Status) VALUES
    ('Popescu', 'Ana', '9A', 'ana.popescu@school.com', '0721234567', '2023-09-01', 'Activ'),
    ('Ionescu', 'Mihai', '10B', 'mihai.ionescu@school.com', '0722345678', '2023-09-01', 'Activ'),
    ('Radu', 'Elena', '11C', 'elena.radu@school.com', '0723456789', '2023-09-01', 'Activ');
END
GO

PRINT '✓ Baza de date a fost creată cu succes!';
