using backfofficegestiondebibliotheque.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backfofficegestiondebibliotheque
{
    public enum Role
    {
        Admin,
        Employee,
        User,
        Unknown

    }
    public delegate void PageChangerNoArg();
    public delegate void PageChanger(Person person);
    public delegate void ChangeAdminPageToIncreaseBudgetPage(Admin admin, double increaseMoney);
    public delegate void ChangeEmployeePageToUserInfoPage(Employee employee, User user);
    public delegate void ChangeUserPageToUserPaymentPage(User user, double money);

    public static class projectInfo
    {
        //yek user nmitavand az yek ketab 2 bar amanat bgir


        
        
            public static string connectionString;
            public static DateTime dateTimeMinValue;

            static projectInfo()
            {
                // Création de la chaîne de connexion pour se connecter à une instance locale de SQL Server
                // Remplacez 'YourDatabaseName' par le nom de votre base de données
                connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=BibliothequeDB;Integrated Security=True;Connect Timeout=30";

                // Définir une valeur minimale pour DateTime
                string temp = "1/1/1900 12:00:00 AM";
                dateTimeMinValue = DateTime.Parse(temp);
            }
        }
    }

