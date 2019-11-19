using System.Text.Json.Serialization;

namespace StarWarsAPI.Domain.Models.VehiclesAggregate
{

    public class VehicleModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonPropertyName("cost_in_credits")]
        public string CostInCredits { get; set; }

        [JsonPropertyName("length")]
        public string Length { get; set; }

        [JsonPropertyName("max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; }

        [JsonPropertyName("crew")]
        public string Crew { get; set; }

        [JsonPropertyName("passengers")]
        public string Passengers { get; set; }

        [JsonPropertyName("cargo_capacity")]
        public string CargoCapacity { get; set; }

        [JsonPropertyName("consumables")]
        public string Consumables { get; set; }

        [JsonPropertyName("vehicle_class")]
        public string VehicleClass { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("edited")]
        public string Edited { get; set; }
    }
}
