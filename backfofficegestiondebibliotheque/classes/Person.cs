using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace backfofficegestiondebibliotheque.classes
{
    public abstract class Person
    {
        public string userName { get; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Role role { get; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
      
        public string imageAddress { get; set; }

        public Person(string userName, string firstName, string lastName,
                      Role role, string phoneNumber, string email, string password,  string imageAddress)
        {
            this.userName = userName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.role = role;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.password = password;
    
            this.imageAddress = imageAddress;
        }
    }
}