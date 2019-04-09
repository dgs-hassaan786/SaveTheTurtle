using Newtonsoft.Json;
using System.Collections.Generic;
using Turtle.Domain.Models.Entities;
using Turtle.Domain.Models.Enums;

namespace Turtle.Domain.Data.Builder
{
    public class Configurations
    {
        [JsonProperty("matrix_rows")]
        public int MatrixX { get; set; } 

        [JsonProperty("matrix_columns")]
        public int MatrixY { get; set; } 

        [JsonProperty("mines")]
        public List<Coordinates> Mines { get; set; } 

        [JsonProperty("exit_point")]
        public Coordinates Exit { get; set; } 

        [JsonProperty("starting_point")]
        public Coordinates EntryPoint { get; set; } 

        [JsonProperty("start_direction")]
        public Directions EntryDirection { get; set; } 

    }

}