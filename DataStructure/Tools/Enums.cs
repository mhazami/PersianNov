using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PersianNov.DataStructure.Tools
{
    public class Enums
    {
        public enum RegisterStatus : byte
        {
            [Description("")]
            None = 0,
            [Description("تایید شده")]
            Accept = 1,
            [Description("درحال بررسی")]
            Prossecing = 2,
            [Description("عدم تایید")]
            NonAccept = 3
        }

        public enum PaymentRole : byte
        {
            [Description("برداشت")]
            Success = 1,
            [Description("واریز")]
            Failed = 2,
        }

        public enum PaymentTaskStatus : byte
        {
            [Description("")]
            None = 0,
            [Description("درحال بررسی")]
            Prossecing = 1,
            [Description("درانتظار پرداخت")]
            WaitForPayment = 2,
            [Description("پرداخت شده")]
            Paied = 3
        }

        public enum PaymentStatus : byte
        {
            [Description("موفق")]
            Success = 1,
            [Description("ناموفق")]
            Failed = 2,

        }

        public enum PaymentType : byte
        {
            [Description("فیش بانکی")]
            Bank = 1,
            [Description("کارت به کارت")]
            Card = 2,
            [Description("آنلاین")]
            Online = 3,
            [Description("نقدی")]
            Cash = 4
        }

        public enum Janre : byte
        {
            [Description("عاشقانه")]
            Romantic = 1,
            [Description("اجتماعی")]
            Social = 2,
            [Description("معمایی")]
            Puzzle = 3,
            [Description("ترسناک")]
            Scary = 4,
            [Description("طنز")]
            Humor = 5,
            [Description("شعر")]
            Poetry = 6,

        }


    }
}
