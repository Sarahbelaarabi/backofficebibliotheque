using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Logique d'interaction pour Membre.xaml
    /// </summary>
    public partial class Membre : Window
    {
        private BibliothequeDBEntities1 dbContext = new BibliothequeDBEntities1();
        public Membre()
        {
            InitializeComponent();
            RefreshListView();
        }
        private void RefreshListView()
        {
            ListViewuser.ItemsSource = dbContext.Personnes
                                    .Where(p => p.Role.Equals("USER", StringComparison.OrdinalIgnoreCase))
                                    .ToList();
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

        private void Creer_Adhrent(object sender, RoutedEventArgs e)
        {
            var nouveauEmployee = new Personne
            {
                NomUtilisateur = txtNomUtilisateur.Text,
                Prenom = txtPrenom.Text,
                Nom = txtNom.Text,
                Role = "USER",
                NumeroTelephone = txtNumeroTelephone.Text,
                Email = txtEmail.Text,
                MotDePasse = pwdMotDePasse.Password,
                // AdresseImage = txtAdresseImage.Text
            };

            dbContext.Personnes.Add(nouveauEmployee);
            dbContext.SaveChanges();
            RefreshListView();
        }

        private void Modfier_Adhrent(object sender, RoutedEventArgs e)
        {   
            var selectedEmployee = ListViewuser.SelectedItem as Personne;
            if (selectedEmployee != null)
            {
                selectedEmployee.NomUtilisateur = txtNomUtilisateur.Text;
                selectedEmployee.Prenom = txtPrenom.Text;
                selectedEmployee.Nom = txtNom.Text;
                selectedEmployee.NumeroTelephone = txtNumeroTelephone.Text;
                selectedEmployee.Email = txtEmail.Text;
                selectedEmployee.MotDePasse = pwdMotDePasse.Password;
                //selectedEmployee.AdresseImage = txtAdresseImage.Text;
                dbContext.SaveChanges();
                RefreshListView();
            }

        }

        private void Supprimer_Adhrent(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = ListViewuser.SelectedItem as Personne;
            if (selectedEmployee != null)
            {
                dbContext.Personnes.Remove(selectedEmployee);
                dbContext.SaveChanges();
                RefreshListView();
            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmployee = ListViewuser.SelectedItem as Personne;
            if (selectedEmployee != null)
            {
                txtNomUtilisateur.Text = selectedEmployee.NomUtilisateur;
                txtPrenom.Text = selectedEmployee.Prenom;
                txtNom.Text = selectedEmployee.Nom;
                txtNumeroTelephone.Text = selectedEmployee.NumeroTelephone;
                txtEmail.Text = selectedEmployee.Email;
                pwdMotDePasse.Password = selectedEmployee.MotDePasse;
                //Image.Text = selectedEmployee.AdresseImage;
            }
        }
    }
}
