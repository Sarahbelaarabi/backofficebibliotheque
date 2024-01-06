
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace backfofficegestiondebibliotheque.classes
{
    public class Admin : Person
    {
        public Admin(
                    string userName,
                    string firstName,
                    string lastName,
                    string phoneNumber,
                    string email,
                    string password,
                    string imageAddress

        )
         : base(userName, firstName, lastName, Role.Admin, phoneNumber, email, password,  imageAddress)
        {

        }

        public ObservableCollection<Employee> showEmployees()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            DataTable dataTable = PersonnesTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][PersonnesTable.indexRole].ToString() == Role.Employee.ToString())
                {
                    Employee employee = new Employee(
                        dataTable.Rows[i][PersonnesTable.indexUserName].ToString(),
                        dataTable.Rows[i][PersonnesTable.indexFirstName].ToString(),
                        dataTable.Rows[i][PersonnesTable.indexLastName].ToString(),
                        dataTable.Rows[i][PersonnesTable.indexPhoneNumber].ToString(),
                        dataTable.Rows[i][PersonnesTable.indexEmail].ToString(),
                        dataTable.Rows[i][PersonnesTable.indexPassword].ToString(),
                 
                        dataTable.Rows[i][PersonnesTable.indexImageAddress].ToString());
                    employees.Add(employee);
                }
            }
            return employees;
        }

        public ObservableCollection<Livres> showBooks()
        {
            ObservableCollection<Livres> books = new ObservableCollection<Livres>();
            DataTable dataTable = LivresTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Livres book = new Livres(
                                    dataTable.Rows[i][LivresTable.indexName].ToString(),
                                    dataTable.Rows[i][LivresTable.indexWriter].ToString(),
                                    dataTable.Rows[i][LivresTable.indexIsbn].ToString(),
                                   (DateTime) dataTable.Rows[i][LivresTable.indexDatePublication],
                                    (int)dataTable.Rows[i][LivresTable.indexCount],
                                     dataTable.Rows[i][LivresTable.indexDescription].ToString(),
                                       dataTable.Rows[i][LivresTable.indexImageAddressl].ToString());
                books.Add(book);
            }
            return books;
        }

        public void deleteEmployee(string userName)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "delete from Personnes where  NomUtilisateur = '" + userName + "'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        public void addEmployee(Employee employee)
        {
            PersonnesTable.write(employee);
        }

        public void addBook(Livres book)
        {
            DataTable dataTable = LivresTable.read();
            bool exists = false;
            int count = 0;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][LivresTable.indexName].ToString() == book.Titre)
                {
                    exists = true;
                    count = (int)dataTable.Rows[i][LivresTable.indexCount];
                    break;
                }
            }

            if (exists)
            {
                book.QuantiteDisponible = count + 1;
                LivresTable.update(book.Titre, book);
            }
            else
            {
                LivresTable.write(book);
            }
        }

        /// <summary>
        /// pay salary of projectInfo.salaryPerEmployee * employees count
        /// chech admin.moneyBag!
        /// </summary>
  

    }
}
