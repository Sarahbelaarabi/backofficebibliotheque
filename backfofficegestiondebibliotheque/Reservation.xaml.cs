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
    /// Logique d'interaction pour Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {
        private BibliothequeDBEntities1 dbContext = new BibliothequeDBEntities1();
        public Reservation()
        {
            InitializeComponent();
            RefreshListView();
        }


        private void RefreshListView()
        {
            var empruntsUsers = from emprunt in dbContext.Emprunts
                                join personne in dbContext.Personnes on emprunt.ID_Personne equals personne.ID_Personne
                                where personne.Role.Equals("user", StringComparison.OrdinalIgnoreCase)
                                select new
                                {
                                    ID_Emprunt = emprunt.ID_Emprunt,
                                    ID_Personne = personne.ID_Personne,  // Cette propriété représente l'ID de la personne avec le rôle "user"
                                    ID_Livre = emprunt.ID_Livre,
                                    DateEmprunt = emprunt.DateEmprunt,
                                    DateRetour = emprunt.DateRetour
                                };

            ListViewEmprunts.ItemsSource = empruntsUsers.ToList();
            try
            {
                var empruntsUserss = from emprunt in dbContext.Emprunts
                                    join personne in dbContext.Personnes on emprunt.ID_Personne equals personne.ID_Personne
                                    where personne.Role.Equals("user", StringComparison.OrdinalIgnoreCase)
                                    select new
                                    {
                                        ID_Emprunt = emprunt.ID_Emprunt,
                                        ID_Personne = personne.ID_Personne,  // Cette propriété représente l'ID de la personne avec le rôle "user"
                                        ID_Livre = emprunt.ID_Livre,
                                        DateEmprunt = emprunt.DateEmprunt,
                                        DateRetour = emprunt.DateRetour
                                    };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Livres livres = new Livres();
            livres.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ListViewEmprunts.SelectedItem as Emprunt;
            if (item != null)
            {
                dbContext.Emprunts.Remove(item);
                dbContext.SaveChanges();
                RefreshListView();
            }

        }
    }
}
