using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StarWarsAPI.Domain.Models.SyncAggregate
{
    public class Model
    {
        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("episode_id")]
        public string EpisodeId { get; set; }

        [JsonPropertyName("opening_crawl")]
        public string OpeningCrawl { get; set; }

        [JsonPropertyName("director")]
        public string Director { get; set; }

        [JsonPropertyName("producer")]
        public string Producer { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("characters")]
        public List<Uri> Characters { get; set; }

        [JsonPropertyName("planets")]
        public List<Uri> Planets { get; set; }

        [JsonPropertyName("starships")]
        public List<Uri> Starships { get; set; }

        [JsonPropertyName("vehicles")]
        public List<Uri> Vehicles { get; set; }

        [JsonPropertyName("species")]
        public List<Uri> Species { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("edited")]
        public string Edited { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }
    }
}
