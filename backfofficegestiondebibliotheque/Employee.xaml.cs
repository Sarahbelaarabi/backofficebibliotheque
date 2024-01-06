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
    /// Logique d'interaction pour Employee.xaml
    /// </summary>

    public partial class Employee : Window
    {
        private BibliothequeDBEntities1 dbContext = new BibliothequeDBEntities1();
        public Employee()
        {
            InitializeComponent();
            RefreshListView();
        }
        private void RefreshListView()
        {
            ListViewEmployee.ItemsSource = dbContext.Personnes
                                    .Where(p =>! p.Role.Equals("USER", StringComparison.OrdinalIgnoreCase) )
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



        private void creer_Employee(object sender, RoutedEventArgs e)
        {
            var nouveauEmployee = new Personne
            {
                NomUtilisateur = txtNomUtilisateur.Text,
                Prenom = txtPrenom.Text,
                Nom = txtNom.Text,
                Role = ((ComboBoxItem)cmbRole.SelectedItem).Content.ToString(),
                NumeroTelephone = txtNumeroTelephone.Text,
                Email = txtEmail.Text,
                MotDePasse = pwdMotDePasse.Password,
               // AdresseImage = txtAdresseImage.Text
            };

            dbContext.Personnes.Add(nouveauEmployee);
            dbContext.SaveChanges();
            RefreshListView();
        }  





        private void modfier_Employee(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = ListViewEmployee.SelectedItem as Personne;
            if (selectedEmployee != null)
            {
                selectedEmployee.NomUtilisateur = txtNomUtilisateur.Text;
                selectedEmployee.Prenom = txtPrenom.Text;
                selectedEmployee.Nom = txtNom.Text;
                selectedEmployee.Role = ((ComboBoxItem)cmbRole.SelectedItem).Content.ToString();
                selectedEmployee.NumeroTelephone = txtNumeroTelephone.Text;
                selectedEmployee.Email = txtEmail.Text;
                selectedEmployee.MotDePasse = pwdMotDePasse.Password;
               // selectedEmployee.AdresseImage = txtAdresseImage.Text;

                dbContext.Entry(selectedEmployee).State = EntityState.Modified;
                dbContext.SaveChanges();
                RefreshListView();
            }
        }

        private void supprimer_Employee(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = ListViewEmployee.SelectedItem as Personne;
            if (selectedEmployee != null)
            {
                dbContext.Personnes.Remove(selectedEmployee);
                dbContext.SaveChanges();
                RefreshListView();
            }
        }

        private void ListViewEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmployee = ListViewEmployee.SelectedItem as Personne;
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

        private void btnTelechargerImage_Click(object sender, RoutedEventArgs e)
        {       
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                btnTelechargerImage.Content = filename;
            }
           
        }


    }
    }
