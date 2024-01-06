
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace backfofficegestiondebibliotheque.classes
{
    static class DbClasses
    {
        /// <summary>
        /// Closes connection with 4 second delay.
        /// use this only after writing data
        /// </summary>
        /// <param name="sqlConnection"></param>
        public static void delayedClose(this SqlConnection sqlConnection)
        {
            void close()
            {
                Thread.Sleep(4000);
                sqlConnection.Close();
            }

            Thread thread = new Thread(close);
            thread.Start();
        }
    }

    /// <summary>
    /// contains methods for manipulating People table
    /// </summary>
    static class PersonnesTable
    {
        public const int indexUserName = 0;
        public const int indexFirstName = 1;
        public const int indexLastName = 2;
        public const int indexRole = 3;
        public const int indexPhoneNumber = 4;
        public const int indexEmail = 5;
        public const int indexPassword = 6;
       
        public const int indexImageAddress = 7;

        /// <summary>
        /// returns all of the data
        /// </summary>
        /// <returns></returns>
        public static DataTable read()
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "select * from Personnes";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        /// <summary>
        /// use for employee and 1 table of user 
        /// </summary>
        /// <param name="person"></param>
        public static void write(Person person)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "insert into Personnes values ('"+ person.userName +"', '"+ person.firstName +"', '"+ person.lastName +"', '"+ person.role.ToString() +"', '"+ person.phoneNumber +"', '"+ person.email +"', '"+ person.password +"', '"+person.imageAddress+"')";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        /// <summary>
        /// delete from People table by userName
        /// </summary>
        /// <param name="userName"></param>
        public static void delete(string userName)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "delete from Personnes where  NomUtilisateur = '"+ userName +"'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        /// <summary>
        /// !before update check person exists!
        /// </summary>
        /// <param name="person"></param>
        public static void Update(string userName, Person person)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "update Personnes SET  Prenom   = '" + person.firstName + "', Nom = '" + person.lastName + "', Role = '" + person.role.ToString() + "', NumeroTelephone  = '" + person.phoneNumber + "', Email = '" + person.email + "', MotDePasse  = '" + person.password + "', AdresseImage = '" + person.imageAddress + "' where    NomUtilisateur ='" + userName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }
    }

    /// <summary>
    /// contains methods for manipulating Books table
    /// </summary>
    static class LivresTable
    {
        public const int indexName = 0;
        public const int indexWriter = 1;
        public const int indexIsbn = 2;
        public const int indexDatePublication = 3;
        public const int indexCount = 4;
        public const int indexDescription = 5;
        public const int indexImageAddressl = 6;

        /// <summary>
        /// returns all books data
        /// </summary>
        /// <returns></returns>
        public static DataTable read()
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "select * from Livres";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        public static void write(Livres book)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "insert into Livres values ('" + book.Titre + "', '" + book.Auteur + "', '" + book.ISBN + "', '" + book.DatePublication + "', '" + book.QuantiteDisponible + "','"+ book.Description+"', '"+ book.AdresseImage + "')";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        /// <summary>
        /// delete by printing number
        /// </summary>
        /// <param name="printingNumber"></param>
        public static void Delete(string printingNumber)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "delete from Livres where  ISBN  = '" + printingNumber + "'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        public static void update(string oldBookName, Livres book)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "update Livres SET QuantiteDisponible = '" + book.QuantiteDisponible + "' where Titre ='" + oldBookName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }
    }

   
}
