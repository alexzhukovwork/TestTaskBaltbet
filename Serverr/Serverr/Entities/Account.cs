using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace Server.Entities
{
    [DataContract]
    public class Account
    {
        [DataMember]
        private int id;
        [DataMember]
        private string lastName;
        [DataMember]
        private string firstName;
        [DataMember]
        private string patronymicl;
        [DataMember]
        private DateTime birthday;
        [DataMember]
        private float balance;

        public Account()
        {

        }

        public Account(int id, string lastName, string firstName,
            string patronymicl, DateTime birthday, float balance)
        {
            this.id = id;
            this.lastName = lastName;
            this.firstName = firstName;
            this.patronymicl = patronymicl;
            this.birthday = birthday;
            this.balance = balance;
        }

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string LastName { get => lastName; set => lastName = value; }
        [DataMember]
        public string FirstName { get => firstName; set => firstName = value; }
        [DataMember]
        public string Patronymicl { get => patronymicl; set => patronymicl = value; }
        [DataMember]
        public DateTime Birthday { get => birthday; set => birthday = value; }
        [DataMember]
        public float Balance { get => balance; set => balance = value; }

        [OperationContract]
        public void DepositeBalance(float money)
        {
            if (money > 0)
                balance += money;
        }

        [OperationContract]
        public void WithdrawBalance(float money)
        {
            if (money > 0)
                balance -= money;
        }
    }
}