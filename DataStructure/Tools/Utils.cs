using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.DataStructure.Tools
{
    public static class Utils
    {
        public static string SlugMaker(this string slug)
        {
            return slug.Trim().Replace(' ', '-');
        }

        public static string RecoverSlug(this string slug)
        {
            return slug.Trim().Replace('-', ' ');
        }
    }
}
