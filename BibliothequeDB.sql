CREATE DATABASE BibliothequeDB;
use BibliothequeDB;




CREATE TABLE Personnes (
    ID_Personne INT PRIMARY KEY IDENTITY(1,1), -- Commence à 1 et s'incrémente de 1
    NomUtilisateur VARCHAR(50),
    Prenom VARCHAR(100),
    Nom VARCHAR(100),
    Role VARCHAR(50), -- Rôles : Admin, Employé, Utilisateur
    NumeroTelephone VARCHAR(20),
    Email VARCHAR(100),
    MotDePasse VARCHAR(100),
    AdresseImage VARCHAR(255) -- En supposant que c'est une URL ou un chemin de fichier
);



--INSERT INTO Personnes (NomUtilisateur, Prenom, Nom, Role, NumeroTelephone, Email, MotDePasse, AdresseImage)
--VALUES ('admin', 'PrenomExemple', 'NomExemple', 'admin', '0123456789', 'exemple@email.com', 'admin', '');


CREATE TABLE Livres (
    ID_Livre INT PRIMARY KEY IDENTITY(1,1), -- Commence à 1 et s'incrémente de 1
    Titre VARCHAR(255),
    Auteur VARCHAR(100),
    ISBN VARCHAR(20),
    DatePublication DATE,
    QuantiteDisponible INT,
    Description TEXT, -- Champ ajouté pour la description du livre
    AdresseImage VARCHAR(255) -- Champ ajouté pour l'URL de l'image ou le chemin de fichier
);

CREATE TABLE Emprunts (
    ID_Emprunt INT PRIMARY KEY IDENTITY(1,1), -- Commence à 1 et s'incrémente de 1
    ID_Personne INT,
    ID_Livre INT,
    DateEmprunt DATE,
    DateRetour DATE,
    FOREIGN KEY (ID_Personne) REFERENCES Personnes(ID_Personne),
    FOREIGN KEY (ID_Livre) REFERENCES Livres(ID_Livre)
);

