using System.Text.Json.Serialization;

namespace StarWarsAPI.Domain.Models.PlanetsAggregate
{
    public class PlanetsModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rotation_period")]
        public string RotationPeriod { get; set; }

        [JsonPropertyName("orbital_period")]
        public string OrbitalPeriod { get; set; }

        [JsonPropertyName("diameter")]
        public string Diameter { get; set; }

        [JsonPropertyName("climate")]
        public string Climate { get; set; }

        [JsonPropertyName("gravity")]
        public string Gravity { get; set; }

        [JsonPropertyName("terrain")]
        public string Terrain { get; set; }

        [JsonPropertyName("surface_water")]
        public string SurfaceWater { get; set; }

        [JsonPropertyName("population")]
        public string Population { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("edited")]
        public string Edited { get; set; }
    }
}
