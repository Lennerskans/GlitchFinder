using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace GlitchFinder.Manager
{
    public class JsonUtils
    {
        internal static readonly JsonSerializerOptions JsonSerializerOptions = new() { Converters = { new JsonStringEnumConverter() }, WriteIndented = true };

        public static T Load<T>(string fileName) 
        {
            var jsonData = File.ReadAllText(fileName);
            
            T deserialized = JsonSerializer.Deserialize<T>(jsonData, JsonSerializerOptions);
            return deserialized;
        }

        public static void Save<T>(string fileName, T deserialized)
        {
            var jsData = JsonSerializer.Serialize(deserialized, JsonSerializerOptions);
            File.WriteAllText(fileName, jsData);
        }
    }
}
