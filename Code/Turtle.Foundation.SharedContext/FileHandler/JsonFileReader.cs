using Newtonsoft.Json;
using System.IO;

namespace Turtle.Foundation.SharedContext.FileHandler
{

    public static class JsonFileReader 
    {
        public static T IRead<T>(string file)
        {
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
    }
}
