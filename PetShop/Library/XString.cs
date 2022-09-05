using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
namespace PetShop.Library
{
    public class XString
    {
        public static string Str_Slug(string s)
        {
            String[][] symbols =
            {
                new string [] {"[áàảạăắằẳặâấầẩậãẵẫ]","a"},
                new string [] {"[đ]","đ"},
                new string [] {"[éèẻẽẹêếềểễệ]","e"},
                new string [] {"[òóỏõọôốồổộỗ]","o"},
                new string [] {"[ùúủũụưừứửữự]","u"},
                new string [] {"ýỳỷỹỵ","y"},
                new string [] {"[\\s'\",]","-"},
            };
            s = s.ToLower();
            foreach(var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
    }

}