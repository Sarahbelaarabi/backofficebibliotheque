
using System;
using System.Data;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace backfofficegestiondebibliotheque.classes
{
    public class User : Person
    {
        public DateTime subRegisterDate { get; set; }
        public DateTime subRenewalDate { get; set; }
        public DateTime subExpireDate { get; set; }

        public User(string userName,
                    string firstName,
                    string lastName,
                    string phoneNumber,
                    string email,
                    string password,
       
                    string imageAddress,
                    DateTime subRegisterDate,
                    DateTime subRenewalDate,
                    DateTime subExpireDate)
                : base(userName, firstName, lastName, Role.User, phoneNumber, email, password,  imageAddress)
        {
            this.subRegisterDate = subRegisterDate;
            this.subRenewalDate = subRenewalDate;
            this.subExpireDate = subExpireDate;
        }

        /// <summary>
        /// returns remaining days as int
        /// </summary>
        /// <returns></returns>
        public int remainingDays()
        {
            TimeSpan timeSpan = subExpireDate.Subtract(subRegisterDate);
            return timeSpan.Days;
        }

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
        /// returns a list of borrowed books by this user
        /// </summary>
        /// <returns></returns>
      /*  public ObservableCollection<Livres> showBorrowedBooks()
        {
            ObservableCollection<Livres> books = new ObservableCollection<Livres>();

            DataTable dataTable = UsersInfosTable.read();
            ObservableCollection<string> bookNames = new ObservableCollection<string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][UsersInfosTable.indexUserName].ToString() == this.userName)
                {
                    for (int j = 1; j < 10; j += 2)
                    {
                        if (dataTable.Rows[i][j].ToString() != "" && dataTable.Rows[i][j] != null)
                        {
                            bookNames.Add(dataTable.Rows[i][j].ToString());
                        }
                    }
                    break;
                }
            }

            dataTable = LivresTable.read();
            for (int i = 0; i <bookNames.Count; i++)
            {
                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    if (dataTable.Rows[j][LivresTable.indexName].ToString() == bookNames[i])
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
                        break;
                    }
                }
            }
            return books;
        }*/

        /// <summary>
        /// shows true if one of the borrowed books is delayed
        /// </summary>
        /// <returns></returns>
     /*   public bool isDelayed()
        {
            DataTable dataTable = UsersInfosTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                //finds userName
                if (dataTable.Rows[i][UsersInfosTable.indexUserName].ToString() == this.userName)
                {
                    //check books
                    for (int j = 1; j < 10; j += 2)
                    {
                        if (dataTable.Rows[i][j].ToString() != "" && dataTable.Rows[i][j] != null)
                        {
                            DateTime expirationDate = (DateTime)dataTable.Rows[i][j + 1];
                            if (DateTime.Compare(expirationDate, DateTime.Now) < 0)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                }
            }
            return false;
        }*/

        /// <summary>
        /// only updates database, check input!
        /// only call it when you know borrow count is less than 5
        /// </summary>
        /// <param name="bookName"></param>
      /*  public void borrowBook(string bookName)
        {
            //update Books Table
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "update Books SET count = '" + (bookCount(bookName) - 1) + "' where name ='" + bookName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();


            //update UsersInfos
            int indexEmpty = 0;
            DataTable dataTable = UsersInfosTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][UsersInfosTable.indexUserName].ToString() == this.userName)
                {
                    for (int j = 1; j < 10; j += 2)
                    {
                        if (dataTable.Rows[i][j].ToString() == "" || dataTable.Rows[i][j] == null)
                        {
                            indexEmpty = j;
                            break;
                        }
                    }
                    break;
                }
            }

            SqlConnection sqlConnection1 = new SqlConnection(projectInfo.connectionString);
            string command1 = "";
            if (indexEmpty == 1)
            {
                command1 = "update UsersInfos SET book1 = '" + bookName + "', expireDate1 = '" + DateTime.Now.AddDays(projectInfo.borrowBookDays) + "' where userName = '" + this.userName + "' ";
            }
            else if (indexEmpty == 3)
            {
                command1 = "update UsersInfos SET book2 = '" + bookName + "', expireDate2 = '" + DateTime.Now.AddDays(projectInfo.borrowBookDays) + "' where userName = '" + this.userName + "' ";
            }
            else if (indexEmpty == 5)
            {
                command1 = "update UsersInfos SET book3 = '" + bookName + "', expireDate3 = '" + DateTime.Now.AddDays(projectInfo.borrowBookDays) + "' where userName = '" + this.userName + "' ";
            }
            else if (indexEmpty == 7)
            {
                command1 = "update UsersInfos SET book4 = '" + bookName + "', expireDate4 = '" + DateTime.Now.AddDays(projectInfo.borrowBookDays) + "' where userName = '" + this.userName + "' ";
            }
            else if (indexEmpty == 9)
            {
                command1 = "update UsersInfos SET book5 = '" + bookName + "', expireDate5 = '" + DateTime.Now.AddDays(projectInfo.borrowBookDays) + "' where userName = '" + this.userName + "' ";
            }

            sqlConnection1.Open();
            SqlCommand sqlCommand1 = new SqlCommand(command1, sqlConnection1);
            sqlCommand1.BeginExecuteNonQuery();
            sqlConnection1.delayedClose();
        }*/


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

        /// <summary>
        /// searchbook by bookName
        /// only one book will return
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public ObservableCollection<Livres> searchBookName(string bookName)
        {
            ObservableCollection<Livres> findedBooks = new ObservableCollection<Livres>();

            DataTable datatable = LivresTable.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][LivresTable.indexName].ToString() == bookName)
                {
                    Livres book = new Livres(
                                        datatable.Rows[i][LivresTable.indexName].ToString(),
                                        datatable.Rows[i][LivresTable.indexWriter].ToString(),
                                        datatable.Rows[i][LivresTable.indexIsbn].ToString(),
                                        (DateTime)datatable.Rows[i][LivresTable.indexDatePublication],
                                        (int)datatable.Rows[i][LivresTable.indexCount],
                                         datatable.Rows[i][LivresTable.indexDescription].ToString(),
                                          datatable.Rows[i][LivresTable.indexImageAddressl].ToString()
                                        );

                    findedBooks.Add(book);
                    break;
                }
            }

            return findedBooks;
        }


        /// <summary>
        /// searchbook by writerName
        /// </summary>
        /// <param name="writerName"></param>
        /// <returns></returns>
        public ObservableCollection<Livres> searchBookWriter(string writerName)
        {
            ObservableCollection<Livres> findedBooks = new ObservableCollection<Livres>();

            DataTable datatable = LivresTable.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][LivresTable.indexWriter].ToString() == writerName)
                {
                    Livres book = new Livres(
                                        datatable.Rows[i][LivresTable.indexName].ToString(),
                                        datatable.Rows[i][LivresTable.indexWriter].ToString(),
                                        datatable.Rows[i][LivresTable.indexIsbn].ToString(),
                                        (DateTime)datatable.Rows[i][LivresTable.indexDatePublication],
                                        (int)datatable.Rows[i][LivresTable.indexCount],
                                        datatable.Rows[i][LivresTable.indexDescription].ToString(),
                                        datatable.Rows[i][LivresTable.indexImageAddressl].ToString());

                    findedBooks.Add(book);
                }
            }

            return findedBooks;
        }

        public void editInfo(User user)
        {
            //same usernames
            PersonnesTable.Update(this.userName, user);
            this.firstName = user.firstName;
            this.lastName = user.lastName;
            this.phoneNumber = user.phoneNumber;
            this.email = user.email;
            this.imageAddress = user.imageAddress;
        }

        /// <summary>
        /// adds one month
        /// udates this user fields
        /// updates renewal date
        /// updates sub expire date
        /// updates user money bag
        /// database update
        /// </summary>
       
      






        /// <summary>
        /// baraye tamdid eshterak, 0:kar anjam shode -1:money null boode -2:karbari ba oon esm nist   adadmosbat:meqdar money kam oomade+bayad mojodi kafi nis chap she
        /// </summary>
        //public double tamdidEshterak()
        //{
        //    double t = 0, adad = 0;
        //    DataTable datatable = PeopleTable.read();
        //    DataTable dataTable2 = UsersInfosTable.read();
        //    for (int i = 0; i < datatable.Rows.Count; i++)
        //    {
        //        if (datatable.Rows[i][PeopleTable.indexUserName].ToString() == this.userName)
        //        {
        //            if (datatable.Rows[i][PeopleTable.indexMoneyBag] == null)
        //            {
        //                t = 0;
        //                return -1;
        //            }
        //            else
        //            {
        //                t = (double)datatable.Rows[i][PeopleTable.indexMoneyBag];
        //                if (t < 1000)
        //                {
        //                    adad = 1000 - t;
        //                    return adad;
        //                }
        //                if (t >= 1000)
        //                {
        //                    adad = t - 1000;
        //                    for (int j = 0; j < dataTable2.Rows.Count; j++)
        //                    {
        //                        if (dataTable2.Rows[j][UsersInfosTable.indexUserName].ToString() == this.userName)
        //                        {
        //                            datatable.Rows[j][UsersInfosTable.indexSubRenewalDate] = DateTime.Today;
        //                            DateTime a = (DateTime)datatable.Rows[j][UsersInfosTable.indexSubRenewalDate];
        //                            datatable.Rows[j][UsersInfosTable.indexSubExpireDate] = a.AddDays(10);
        //                        }
        //                    }
        //                }
        //                datatable.Rows[i][PeopleTable.indexMoneyBag] = adad;
        //                return 0;
        //            }
        //        }

        //    }
        //    return -2;

        //}



        //public int count(Livres a)
        //{
        //    int tedad = 0;
        //    DataTable b = UsersInfosTable.read();
        //    for (int i = 0; i < b.Rows.Count; i++)
        //    {
        //        if (b.Rows[i][UsersInfosTable.indexBook1] != null)
        //        {
        //            tedad++;
        //        }
        //        if (b.Rows[i][UsersInfosTable.indexBook2] != null)
        //        {
        //            tedad++;
        //        }
        //        if (b.Rows[i][UsersInfosTable.indexBook3] != null)
        //        {
        //            tedad++;
        //        }
        //        if (b.Rows[i][UsersInfosTable.indexBook4] != null)
        //        {
        //            tedad++;
        //        }
        //        if (b.Rows[i][UsersInfosTable.indexBook5] != null)
        //        {
        //            tedad++;
        //        }
        //    }
        //    return tedad;
        //}

        /// <summary>
        /// update money bag of this user in ram and database
        /// </summary>
       
    }
}
