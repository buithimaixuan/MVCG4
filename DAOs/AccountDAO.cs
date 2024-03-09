using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCG4.Models;

namespace MVCG4.DAOs
{
    public class AccountDAO
    {
        public Account GetAccLogin(string username, string pass)
        {
            Account member = null;
            try
            {
                using var context = new ProjectPRNContext();
                member = context.Accounts.AsNoTracking().FirstOrDefault(c => c.Username == username && c.Password == Md5(pass));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public static string Md5(string message)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.ASCII.GetBytes(message);
                byte[] hash = md5.ComputeHash(input);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}