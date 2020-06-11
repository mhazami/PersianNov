using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("EnterPrise")]
    public sealed class Customer : DataStructureBase<Customer>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        private string _firstName;
        [DbType("nvarchar(50)")]
        public string FirstName
        {
            get { return _firstName; }
            set { base.SetPropertyValue("FirstName", value); }
        }


        private string _lastName;
        [DbType("nvarchar(50)")]
        public string LastName
        {
            get { return _lastName; }
            set { base.SetPropertyValue("LastName", value); }
        }


        private string _password;
        [DbType("nvarchar(50)")]
        public string Password
        {
            get { return _password; }
            set { base.SetPropertyValue("Password", value); }
        }


        private string _username;
        [DbType("nvarchar(50)")]
        public string Username
        {
            get { return _username; }
            set { base.SetPropertyValue("Username", value); }
        }


        private string _email;
        [DbType("nvarchar(250)")]
        public string Email
        {
            get { return _email; }
            set { base.SetPropertyValue("Email", value); }
        }


        [DisableAction(DiableAllAction = true)]
        public string RepeatPassword { get; set; }


    }
}
