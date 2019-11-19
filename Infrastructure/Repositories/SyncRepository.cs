using StarWarsAPI.Domain.Models.CharactersAggregate;
using StarWarsAPI.Domain.Models.PlanetsAggregate;
using StarWarsAPI.Domain.Models.SpeciesAggregate;
using StarWarsAPI.Domain.Models.StarshipsAggregate;
using StarWarsAPI.Domain.Models.SyncAggregate;
using StarWarsAPI.Domain.Models.VehiclesAggregate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StarWarsAPI.Infrastructure.Repositories
{
    public class SyncRepository : ISyncRepository
    {

        private readonly HttpClient _httpClient;

        public SyncRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Get all applications from enum service
        /// </summary>
        /// <returns></returns>
        public async Task<bool> FillDataText()
        {
            int count = 0;
            List<CharacterModel> characters = new List<CharacterModel>();
            List<PlanetsModel> planets = new List<PlanetsModel>();
            List<StarshipsModel> starships = new List<StarshipsModel>();
            List<SpeciesModel> species = new List<SpeciesModel>();
            List<VehicleModel> vehicles = new List<VehicleModel>();
            using HttpResponseMessage httpResponse = await _httpClient.GetAsync(new Uri("https://swapi.co/api/films/"));
            if (httpResponse.StatusCode == HttpStatusCode.OK && httpResponse.Content != null)
            {
                Model model = await JsonSerializer.DeserializeAsync<Model>(await httpResponse.Content.ReadAsStreamAsync());
                if (model != default && model.Results != null && model.Results.Any())
                {
                    foreach (Result result in model.Results)
                    {
                        count = TotalCount(count, result);
                        characters = await GetAllCharacters(characters, result);
                        planets = await GetAllPlanets(planets, result);
                        starships = await GetAllStarships(starships, result);
                        species = await GetAllSpecies(species, result);
                        vehicles = await GetAllVehicles(vehicles, result);
                    }

                    if (characters.Count > 0) File.WriteAllText("./StubData/Data/Requests/Characters.json", JsonSerializer.Serialize(characters));
                    if (planets.Count > 0) File.WriteAllText("./StubData/Data/Requests/Planets.json", JsonSerializer.Serialize(planets));
                    if (starships.Count > 0) File.WriteAllText("./StubData/Data/Requests/Species.json", JsonSerializer.Serialize(starships));
                    if (species.Count > 0) File.WriteAllText("./StubData/Data/Requests/Starships.json", JsonSerializer.Serialize(species));
                    if (vehicles.Count > 0) File.WriteAllText("./StubData/Data/Requests/Vehicles.json", JsonSerializer.Serialize(vehicles));
                }
            }
            return IsTransferOK(count, characters, planets, starships, species, vehicles);
        }

        #region PrivateMethods
        private int TotalCount(int count, Result result)
        {
            count = count + result.Characters.Count + result.Planets.Count + result.Starships.Count + result.Vehicles.Count + result.Species.Count;
            return count;
        }

        private bool IsTransferOK(int count, List<CharacterModel> characters, List<PlanetsModel> planets, List<StarshipsModel> starships, List<SpeciesModel> species, List<VehicleModel> vehicles)
        {
            return characters.Count + planets.Count + starships.Count + species.Count + vehicles.Count == count;
        }
        #endregion

        #region Characters
        private async Task<List<CharacterModel>> GetAllCharacters(List<CharacterModel> characters, Result result)
        {
            if (result?.Characters != default)
            {
                foreach (Uri uri in result.Characters)
                {
                    CharacterModel character = await GetCharacter(uri);
                    if (character != default)
                    {
                        characters.Add(character);
                    }

                }
            }
            return characters;
        }

        private async Task<CharacterModel> GetCharacter(Uri uri)
        {
            CharacterModel characterModel = new CharacterModel();
            if (int.TryParse(uri.ToString().Split('/')[5], out int id))
            {
                using HttpResponseMessage httpResponseCharacter = await _httpClient.GetAsync(uri);
                if (httpResponseCharacter.StatusCode == HttpStatusCode.OK && httpResponseCharacter.Content != null)
                {
                    characterModel = await JsonSerializer.DeserializeAsync<CharacterModel>(await httpResponseCharacter.Content.ReadAsStreamAsync());
                    characterModel.Id = id;
                }
            }
            return characterModel;
        }
        #endregion

        #region Planets
        private async Task<List<PlanetsModel>> GetAllPlanets(List<PlanetsModel> planets, Result result)
        {
            if (result?.Planets != default)
            {
                foreach (Uri uri in result.Planets)
                {
                    PlanetsModel planet = await GetPlanet(uri);
                    if (planet != default)
                    {
                        planets.Add(planet);
                    }

                }
            }
            return planets;
        }

        private async Task<PlanetsModel> GetPlanet(Uri uri)
        {
            PlanetsModel planetModel = new PlanetsModel();
            if (int.TryParse(uri.ToString().Split('/')[5], out int id))
            {
                using HttpResponseMessage httpResponse = await _httpClient.GetAsync(uri);
                if (httpResponse.StatusCode == HttpStatusCode.OK && httpResponse.Content != null)
                {
                    planetModel = await JsonSerializer.DeserializeAsync<PlanetsModel>(await httpResponse.Content.ReadAsStreamAsync());
                    planetModel.Id = id;
                }
            }
            return planetModel;
        }
        #endregion

        #region Starships
        private async Task<List<StarshipsModel>> GetAllStarships(List<StarshipsModel> starships, Result result)
        {
            if (result?.Starships != default)
            {
                foreach (Uri uri in result.Starships)
                {
                    StarshipsModel starship = await GetStarship(uri);
                    if (starship != default)
                    {
                        starships.Add(starship);
                    }

                }
            }
            return starships;
        }

        private async Task<StarshipsModel> GetStarship(Uri uri)
        {
            StarshipsModel starship = new StarshipsModel();
            if (int.TryParse(uri.ToString().Split('/')[5], out int id))
            {
                using HttpResponseMessage httpResponse = await _httpClient.GetAsync(uri);
                if (httpResponse.StatusCode == HttpStatusCode.OK && httpResponse.Content != null)
                {
                    starship = await JsonSerializer.DeserializeAsync<StarshipsModel>(await httpResponse.Content.ReadAsStreamAsync());
                    starship.Id = id;
                }
            }
            return starship;
        }
        #endregion

        #region Species
        private async Task<List<SpeciesModel>> GetAllSpecies(List<SpeciesModel> species, Result result)
        {
            if (result?.Species != default)
            {
                foreach (Uri uri in result.Species)
                {
                    SpeciesModel specie = await GetSpecies(uri);
                    if (specie != default)
                    {
                        species.Add(specie);
                    }

                }
            }
            return species;
        }

        private async Task<SpeciesModel> GetSpecies(Uri uri)
        {
            SpeciesModel specie = new SpeciesModel();
            if (int.TryParse(uri.ToString().Split('/')[5], out int id))
            {
                using HttpResponseMessage httpResponse = await _httpClient.GetAsync(uri);
                if (httpResponse.StatusCode == HttpStatusCode.OK && httpResponse.Content != null)
                {
                    specie = await JsonSerializer.DeserializeAsync<SpeciesModel>(await httpResponse.Content.ReadAsStreamAsync());
                    specie.Id = id;
                }
            }
            return specie;
        }
        #endregion

        #region VehicleModel
        private async Task<List<VehicleModel>> GetAllVehicles(List<VehicleModel> vehicles, Result result)
        {
            if (result?.Vehicles != default)
            {
                foreach (Uri uri in result.Vehicles)
                {
                    VehicleModel vehicle = await GetVehicle(uri);
                    if (vehicle != default)
                    {
                        vehicles.Add(vehicle);
                    }

                }
            }
            return vehicles;
        }

        private async Task<VehicleModel> GetVehicle(Uri uri)
        {
            VehicleModel vehicleModel = new VehicleModel();
            if (int.TryParse(uri.ToString().Split('/')[5], out int id))
            {
                using HttpResponseMessage httpResponse = await _httpClient.GetAsync(uri);
                if (httpResponse.StatusCode == HttpStatusCode.OK && httpResponse.Content != null)
                {
                    vehicleModel = await JsonSerializer.DeserializeAsync<VehicleModel>(await httpResponse.Content.ReadAsStreamAsync());
                    vehicleModel.Id = id;
                }
            }
            return vehicleModel;
        }
        #endregion
    }
}
