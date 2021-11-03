using System;
using System.Collections.Generic;
using System.Text;

namespace FCM.DAO
{
    public class StringFormat
    {
        private static StringFormat instance;
        public static StringFormat Instance
        {
            get { if (instance == null) instance = new StringFormat(); return instance; }
            set => instance = value;
        }
        public string FomartSpace(string s)
        {
            string result = "";
            while (s.Length>0 && s[0] == ' ')
                s = s.Remove(0, 1);
            while (s.Length>0 && s[s.Length-1] == ' ')
                s = s.Remove(s.Length-1, 1);
            result = s;
            return result;
        }
    }
}
