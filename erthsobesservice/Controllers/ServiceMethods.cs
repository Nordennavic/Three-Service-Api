using erthsobesservice.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace erthsobesservice.Controllers
{
    public partial class ServiceController
    {
        public static MemoryStream SerializeToStream(object o)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            return stream;
        }
        public static bool IsMD5(string hash)
        {
            if (string.IsNullOrEmpty(hash)) return false;

            return Regex.IsMatch(hash, "[0-9a-fA-F]{32}$", RegexOptions.Compiled);
        }

        public static int GetCost()
        {
            var rnd = new Random();
            return rnd.Next(1, 101) * 10000;
        }

        public static Attachment GetFile()
        {
            var rnd = new Random();
            if (rnd.Next(0, 2) == 1)
            {
                var file = new Attachment { id = rnd.Next(0, 100000) };
                file.hash = MD5Hash(file.id.ToString());
                return file;
            }
            return null;
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
    }
}
