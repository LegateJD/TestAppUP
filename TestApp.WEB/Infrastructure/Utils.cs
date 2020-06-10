using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestApp.WEB.Infrastructure
{
    public class Utils
    {
        public static string ObjectToString(object obj)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static object StringToObject(string base64String)
        {
            var bytes = Convert.FromBase64String(base64String);

            using (var ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;

                return new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
