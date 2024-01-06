using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace backfofficegestiondebibliotheque
{
    /// <summary>
    /// Logique d'interaction pour Livres.xaml
    /// </summary>
    public partial class Livres : Window
    {
        private BibliothequeDBEntities1 dbContext = new BibliothequeDBEntities1();


        public Livres()
        {
            InitializeComponent();
            RefreshListView();
        }
        private void RefreshListView()
        {
            ListViewLivres.ItemsSource = dbContext.Livres.ToList();
        }

     

  

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewLivres.SelectedItem is Livre livreASupprimer)
            {
                dbContext.Livres.Remove(livreASupprimer);
                dbContext.SaveChanges();
                RefreshListView();
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Livres livres = new Livres();
            livres.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Membre membre = new Membre();
            membre.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Employee employee1 = new Employee();
            employee1.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Reservation reservation = new Reservation();
            reservation.Show();
            this.Close();
        }

        private void Creer_Clic(object sender, RoutedEventArgs e)
        {

            var nouveauLivre = new Livre
            {
                Titre = txtTitre.Text,
                Auteur = txtAuteur.Text,
                ISBN = txtISBN.Text,
                DatePublication = dpDatePublication.SelectedDate ?? DateTime.Now,
                QuantiteDisponible = int.Parse(txtQuantiteDisponible.Text),
                Description = txtDescription.Text,
                // AdresseImage = // Gérer le chemin de l'image si nécessaire
            };

            dbContext.Livres.Add(nouveauLivre);
            dbContext.SaveChanges();
            RefreshListView();
        }

        private void Modifier_Clic(object sender, RoutedEventArgs e)
        {
            if (ListViewLivres.SelectedItem is Livre livreAModifier)
            {
                livreAModifier.Titre = txtTitre.Text;
                livreAModifier.Auteur = txtAuteur.Text;
                livreAModifier.ISBN = txtISBN.Text;
                livreAModifier.DatePublication = dpDatePublication.SelectedDate ?? DateTime.Now;
                livreAModifier.QuantiteDisponible = int.Parse(txtQuantiteDisponible.Text);
                livreAModifier.Description = txtDescription.Text;
                // livreAModifier.AdresseImage = // Gérer le chemin de l'image si nécessaire

                dbContext.SaveChanges();
                RefreshListView();
            }

        }

        private void ListViewLivres_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var livresList = dbContext.Livres;
            if (ListViewLivres.SelectedItem is Livre livreSelectionne)
            {
                txtTitre.Text = livreSelectionne.Titre;
                txtAuteur.Text = livreSelectionne.Auteur;
                txtISBN.Text = livreSelectionne.ISBN;
                dpDatePublication.SelectedDate = livreSelectionne.DatePublication;
                txtQuantiteDisponible.Text = livreSelectionne.QuantiteDisponible.ToString();
                txtDescription.Text = livreSelectionne.Description;
                // Charger l'image si nécessaire
            }
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                btnUploadImage.Content = filename; // Utilisez 'Content' pour définir le texte du bouton.
            }
        }

    }
}

