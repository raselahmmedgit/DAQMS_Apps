using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAQMS.Core
{
    public static class Password
    {
        public static byte[] GetBytes(string password)
        {
            byte[] bytPassword = Encoding.ASCII.GetBytes(password); ;
            return bytPassword;
        }

        public static string GetString(byte[] password)
        {
            string strPassword = Encoding.ASCII.GetString(password);
            return strPassword;
        }

        // a 32 character hexadecimal string.
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
                //sBuilder.Append(Convert.ToString(data[i], 2).PadLeft(8, '0')); //Convert into binary
            }

            // Return the hexadecimal string.
            return sBuilder.ToString().ToLower();
        }

        // Verify a hash against a string.
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string GetRandomAlphanumericString(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";
            return GetRandomString(length, alphanumericCharacters);
        }

        public static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
        //public static string GetModelStateErrorMessage(ModelStateDictionary modelStateDictionary)
        //{
        //    string message = @"<div id='ErrorMessage'>";

        //    foreach (var modelStateValues in modelStateDictionary.Values)
        //    {
        //        if (modelStateValues.Errors.Any())
        //        {
        //            foreach (var modelError in modelStateValues.Errors)
        //            {
        //                message += @"<p>";
        //                message += modelError.ErrorMessage;
        //                message += "</p>";
        //            }
        //        }
        //    }

        //    message += "</div>";

        //    return message;
        //}

    }
}
