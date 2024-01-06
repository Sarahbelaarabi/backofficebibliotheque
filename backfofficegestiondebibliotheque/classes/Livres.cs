using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace backfofficegestiondebibliotheque.classes
{

    public class Livres
    {
        public int ID_Livre { get; set; } // Ajouté pour correspondre à la clé primaire auto-incrémentée
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public string ISBN { get; set; } // Ajouté pour correspondre à la table SQL
        public DateTime DatePublication { get; set; } // Type modifié pour correspondre à la table SQL
        public int QuantiteDisponible { get; set; }
        public string Description { get; set; } // Ajouté pour correspondre à la nouvelle colonne
        public string AdresseImage { get; set; } // Ajouté pour correspondre à la nouvelle colonne

        // Constructeur mis à jour avec les nouveaux champs
        public Livres(string titre, string auteur, string isbn, DateTime datePublication,
                     int quantiteDisponible, string description, string adresseImage)
        {
            Titre = titre;
            Auteur = auteur;
            ISBN = isbn;
            DatePublication = datePublication;
            QuantiteDisponible = quantiteDisponible;
            Description = description;
            AdresseImage = adresseImage;
        }
    }

}