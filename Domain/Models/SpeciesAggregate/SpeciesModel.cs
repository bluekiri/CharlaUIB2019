using System.Text.Json.Serialization;

namespace StarWarsAPI.Domain.Models.SpeciesAggregate
{
    public class SpeciesModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("classification")]
        public string Classification { get; set; }

        [JsonPropertyName("designation")]
        public string Designation { get; set; }

        [JsonPropertyName("average_height")]
        public string AverageHeight { get; set; }

        [JsonPropertyName("skin_colors")]
        public string SkinColors { get; set; }

        [JsonPropertyName("hair_colors")]
        public string HairColors { get; set; }

        [JsonPropertyName("eye_colors")]
        public string EyeColors { get; set; }

        [JsonPropertyName("average_lifespan")]
        public string AverageLifespan { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("edited")]
        public string Edited { get; set; }
    }
}
