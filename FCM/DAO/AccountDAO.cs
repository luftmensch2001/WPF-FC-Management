using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FCM.DTO;
using System.Data;

namespace FCM.DAO
{
    class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            set => instance = value;
        }
        public List<Account> GetListAccount()
        {
            List<Account> users= new List<Account>();

            string query = "Select* " +
                           "From Users  ";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            
            foreach (DataRow row in tb.Rows)
            {
                Account user = new Account(row);
                users.Add(user);
            }    
            
            return users;
        }


        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
