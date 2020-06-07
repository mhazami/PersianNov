using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("EnterPrise")]
    public sealed class Publisher : DataStructureBase<Publisher>
    {
        public Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        public string _name;
        [DbType("nvarchar(100)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }


        public string _contractNumber;
        [DbType("nvarchar(20)")]
        public string ContractNumber
        {
            get { return _contractNumber; }
            set { base.SetPropertyValue("ContractNumber", value); }
        }


        public string _address;
        [DbType("nvarchar(MAX)")]
        public string Address
        {
            get { return _address; }
            set { base.SetPropertyValue("Address", value); }
        }

        public string _manager;
        [DbType("nvarchar(50)")]
        public string Manager
        {
            get { return _manager; }
            set { base.SetPropertyValue("Manager", value); }
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


        public string _registerNumber;
        [DbType("nvarchar(20)")]
        public string RegisterNumber
        {
            get { return _registerNumber; }
            set { base.SetPropertyValue("RegisterNumber", value); }
        }

        public string _createDate;
        [DbType("varchar(10)")]
        public string CreateDate
        {
            get { return _createDate; }
            set { base.SetPropertyValue("CreateDate", value); }
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


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }
}
