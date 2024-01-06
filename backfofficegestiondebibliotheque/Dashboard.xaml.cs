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
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Button_Click(object Sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
            this.Close();

        }

        private void Button_Click_1(object Sender, RoutedEventArgs e)
        {
            Livres livres = new Livres();
            livres.Show();
            this.Close();
        
        }

        private void Button_Click_2(object Sender, RoutedEventArgs e)
        {
            Membre membre = new Membre();
            membre.Show();
           this.Close();
        }

        private void Button_Click_3(object Sender, RoutedEventArgs e)
        {
            Reservation Reservation = new Reservation();
            Reservation.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }
    }
}
