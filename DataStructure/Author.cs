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
        public Author()
        {
            Enabled = false;
            EnabledShow = false;
            RegisterStatus = RegisterStatus.Prossecing;
        }

        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
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

        private string _phone;
        [DbType("varchar(11)")]
        public string Phone
        {
            get { return _phone; }
            set { base.SetPropertyValue("Phone", value); }
        }


        private string _website;
        [DbType("nvarchar(250)")]
        public string Website
        {
            get { return _website; }
            set { base.SetPropertyValue("Website", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        private RegisterStatus _registerStatus;
        [DbType("tinyint")]
        public RegisterStatus RegisterStatus
        {
            get { return _registerStatus; }
            set { base.SetPropertyValue("RegisterStatus", value); }
        }

        private bool _enabledShow;
        [DbType("bit")]
        public bool EnabledShow
        {
            get { return _enabledShow; }
            set { base.SetPropertyValue("EnabledShow", value); }
        }

        [DisableAction(DiableAllAction = true)]
        public string RepeatPassword { get; set; }




    }
}
