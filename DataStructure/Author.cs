using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;
using static PersianNov.DataStructure.Tools.Enums;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("EnterPrise")]
    public sealed class Author : DataStructureBase<Author>
    {
        public Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        public string _firstName;
        [DbType("nvarchar(50)")]
        public string FirstName
        {
            get { return _firstName; }
            set { base.SetPropertyValue("FirstName", value); }
        }


        public string _lastName;
        [DbType("nvarchar(50)")]
        public string LastName
        {
            get { return _lastName; }
            set { base.SetPropertyValue("LastName", value); }
        }


        public string _password;
        [DbType("nvarchar(50)")]
        public string Password
        {
            get { return _password; }
            set { base.SetPropertyValue("Password", value); }
        }


        public string _username;
        [DbType("nvarchar(50)")]
        public string Username
        {
            get { return _username; }
            set { base.SetPropertyValue("Username", value); }
        }


        public string _email;
        [DbType("nvarchar(250)")]
        public string Email
        {
            get { return _email; }
            set { base.SetPropertyValue("Email", value); }
        }

        public string _phone;
        [DbType("varchar(11)")]
        public string Phone
        {
            get { return _phone; }
            set { base.SetPropertyValue("Phone", value); }
        }


        public string _website;
        [DbType("nvarchar(250)")]
        public string Website
        {
            get { return _website; }
            set { base.SetPropertyValue("Website", value); }
        }

        public bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        public RegisterStatus _registerStatus;
        [DbType("tinyint")]
        public RegisterStatus RegisterStatus
        {
            get { return _registerStatus; }
            set { base.SetPropertyValue("RegisterStatus", value); }
        }

        public bool _enabledShow;
        [DbType("bit")]
        public bool EnabledShow
        {
            get { return _enabledShow; }
            set { base.SetPropertyValue("EnabledShow", value); }
        }


        


  
    }
}
