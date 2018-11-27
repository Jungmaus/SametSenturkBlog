using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Business.Library
{
    public static class SeoLib
    {

        public static string GetSeoLink(string link)
        {
            Random random = new Random();

            link = link.Replace("!", "");
            link = link.Replace("'", "");
            link = link.Replace("?", "");
            link = link.Replace(",", "");
            link = link.Replace(".", "");
            link = link.Replace("İ", "I");
            link = link.Replace("ı", "i");
            link = link.Replace("Ş", "S");
            link = link.Replace("ş", "s");
            link = link.Replace("Ü", "U");
            link = link.Replace("ü", "u");
            link = link.Replace("Ç", "C");
            link = link.Replace("ç", "c");
            link = link.Replace("Ğ", "G");
            link = link.Replace("ğ", "g");
            link = link.Replace("Ö", "O");
            link = link.Replace("ö", "o");
            link = link.Replace(" ", "-");
            link = link.Replace("_", "-");

            link += "-";

            link += random.Next(1000, 9999).ToString();

            link = link.ToLower();

            return link;
        }

        public static string GetSeoName(string link)
        {
            link = link.Replace("!", "");
            link = link.Replace("'", "");
            link = link.Replace("?", "");
            link = link.Replace(",", "");
            link = link.Replace(".", "");
            link = link.Replace("İ", "I");
            link = link.Replace("ı", "i");
            link = link.Replace("Ş", "S");
            link = link.Replace("ş", "s");
            link = link.Replace("Ü", "U");
            link = link.Replace("ü", "u");
            link = link.Replace("Ç", "C");
            link = link.Replace("ç", "c");
            link = link.Replace("Ğ", "G");
            link = link.Replace("ğ", "g");
            link = link.Replace("Ö", "O");
            link = link.Replace("ö", "o");
            link = link.Replace(" ", "-");
            link = link.Replace("_", "-");
            return link;
        }
    }
}