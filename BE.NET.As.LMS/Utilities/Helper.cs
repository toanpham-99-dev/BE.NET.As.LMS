using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace BE.NET.As.LMS.Utilities
{
    public class Helper
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static ICollection<T> Paging<T>(List<T> list, int pageSize, int pageIndex)
        {
            return list.Skip(pageSize * (pageIndex - 1))
            .Take(pageSize)
            .ToList();
        }

        public static bool ValidateObject<T>(T item)
        {
            ValidationContext validationContext = new ValidationContext(item, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(item, validationContext, validationResults, true);
            if (!valid)
            {
                foreach (var vr in validationResults)
                {
                    Console.WriteLine($"Error: {vr.ErrorMessage}");
                }
                return false;
            }
            return true;
        }
        public static string ToAlias(string value)
        {
            value = value.ToLowerInvariant();
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);

            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            value = value.Trim('-', '_');

            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            value = value.Substring(0, value.Length <= 250 ? value.Length : 75).Trim();

            return value;
        }

        public static long GetCurrentUserId(ClaimsPrincipal claimsPrincipal)
        {
            return Int64.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        public static string GetCurrentUserHashCode(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Hash).Value;
        }
        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
    }
}
