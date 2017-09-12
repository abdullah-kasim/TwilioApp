using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;

namespace TwilioApp.Backend.Common.Libraries
{
    public class Json
    {
        public static T DeserializeFromStream<T>(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                var deserialized = serializer.Deserialize<T>(jsonTextReader);
                return deserialized;
            }
        }
    }
}