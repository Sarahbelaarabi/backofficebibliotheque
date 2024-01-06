using backfofficegestiondebibliotheque.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace backfofficegestiondebibliotheque.classes
{
    public class Employee : Person
    {
        public Employee(string userName,
                        string firstName,
                        string lastName,
                        string phoneNumber,
                        string email,
                        string password,
                        
                        string imageAddress)
        : base(userName, firstName, lastName, Role.Employee, phoneNumber, email, password,  imageAddress)
        {
        }

        /// <summary>
        /// returns all of the books
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Livres> showAllBooks()
        {
            ObservableCollection<Livres> books = new ObservableCollection<Livres>();
            DataTable dataTable = LivresTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Livres book = new Livres(
                                 dataTable.Rows[i][LivresTable.indexName].ToString(),
                                    dataTable.Rows[i][LivresTable.indexWriter].ToString(),
                                    dataTable.Rows[i][LivresTable.indexIsbn].ToString(),
                                   (DateTime)dataTable.Rows[i][LivresTable.indexDatePublication],
                                    (int)dataTable.Rows[i][LivresTable.indexCount],
                                     dataTable.Rows[i][LivresTable.indexDescription].ToString(),
                                       dataTable.Rows[i][LivresTable.indexImageAddressl].ToString());
                books.Add(book);
            }
            return books;
        }

        /// <summary>
        /// ketab haye qarzi ro barmigardoone
        /// </summary>
        /// <returns></returns>
       

        private bool bookExistInList(ObservableCollection<Livres> books, string bookName)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (bookName == books[i].Titre)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// ketabaye mojood ro barmigardoone
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Livres> showAvailableBooks()
        {
            ObservableCollection<Livres> books = new ObservableCollection<Livres>();
            DataTable dataTable = LivresTable.read();
            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                if ((int)dataTable.Rows[j][LivresTable.indexCount] > 0)
                {
                    Livres book = new Livres(
                                     dataTable.Rows[j][LivresTable.indexName].ToString(),
                                    dataTable.Rows[j][LivresTable.indexWriter].ToString(),
                                    dataTable.Rows[j][LivresTable.indexIsbn].ToString(),
                                   (DateTime)dataTable.Rows[j][LivresTable.indexDatePublication],
                                    (int)dataTable.Rows[j][LivresTable.indexCount],
                                     dataTable.Rows[j][LivresTable.indexDescription].ToString(),
                                       dataTable.Rows[j][LivresTable.indexImageAddressl].ToString());
                    books.Add(book);
                }
            }
            return books;
        }

        /// <summary>
        /// kol karbaraye az noe user ro bar migardoone
        /// </summary>
        /// <returns></returns>
      
        /// <summary>
        /// user haii ke hadaqal yek ketab ba time expire darand ro namayesh mide
        /// </summary>
      

        /// <summary>
        /// user haii ke haq ozviat nadadan ro bar migardoone
        /// </summary>
        /// <returns></returns>
      

        /// <summary>
        /// didan etalaat yek fard khas ba search kardan esmesh 
        /// null:yani fardi ba oon esm nis dar qeir in soorat:avalin nafarr ba oon username ro bar migardone
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
       
        public void editInfo(Employee employee)
        {
            //same usernames
            PersonnesTable.Update(this.userName, employee);
            this.firstName = employee.firstName;
            this.lastName = employee.lastName;
            this.phoneNumber = employee.phoneNumber;
            this.email = employee.email;
            this.imageAddress = employee.imageAddress;
        }

        public void removeUser(User user)
        {
            //delete from People Table
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "delete from People where userName = '" + user.userName + "'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();

            //update Books Table
           // DataTable dataTable = UsersInfosTable.read();
            List<string> bookNames = new List<string>();

            //for (int i = 0; i < dataTable.Rows.Count; i++)
            {
               /* if (dataTable.Rows[i][UsersInfosTable.indexUserName].ToString() == user.userName)
                {
                    for (int j = 1; j < 10; j += 2)
                    {
                        if (dataTable.Rows[i][j].ToString() != "" &&
                            dataTable.Rows[i][j] != null)
                        {
                            bookNames.Add(dataTable.Rows[i][j].ToString());
                        }
                    }
                    break;
                }*/
            }//

            for (int i = 0; i < bookNames.Count; i++)
            {
                SqlConnection sqlConnection1 = new SqlConnection(projectInfo.connectionString);
                sqlConnection1.Open();
                string command1 = "update Books SET count = '" + (bookCount(bookNames[i]) + 1) + "' where name ='" + bookNames[i] + "' ";
                SqlCommand sqlCommand1 = new SqlCommand(command1, sqlConnection1);
                sqlCommand1.BeginExecuteNonQuery();
                sqlConnection1.delayedClose();
            }

            //delete from UsersInfos Table
            SqlConnection sqlConnection2 = new SqlConnection(projectInfo.connectionString);
            sqlConnection2.Open();
            string command2 = "delete from UsersInfos where userName = '" + user.userName + "'";
            SqlCommand sqlCommand2 = new SqlCommand(command2, sqlConnection2);
            sqlCommand2.BeginExecuteNonQuery();
            sqlConnection2.delayedClose();
        }

        /// <summary>
        /// returns book count
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        private int bookCount(string bookName)
        {
            DataTable booksData = LivresTable.read();
            for (int i = 0; i < booksData.Rows.Count; i++)
            {
                if (booksData.Rows[i][LivresTable.indexName].ToString() == bookName)
                {
                    return (int)booksData.Rows[i][LivresTable.indexCount];
                }
            }
            return 0;
        }
    }

}
