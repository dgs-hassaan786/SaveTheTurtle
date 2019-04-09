using Newtonsoft.Json;

namespace Turtle.Domain.Models.Entities
{
    /// <summary>
    /// Coordinate class provides the simultation of X-Index and Y-Index
    /// </summary>
    public class Coordinates
    {
        [JsonProperty("x")]
        public int XVal { get; set; }

        [JsonProperty("y")]
        public int YVal { get; set; }

        public Coordinates(int x, int y)
        {
            XVal = x;
            YVal = y;
        }
    }
}
