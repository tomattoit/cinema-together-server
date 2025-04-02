using System.Text.Json;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Infrastructure;

public class DatabaseSeeder(ApplicationDbContext context, IConfiguration config)
{
    public void Seed()
    {
        if (!context.Countries.Any())
        {
            LoadCountries();
        }

        if (!context.Cities.Any())
        {
            LoadCities();
        }

        if (!context.Movies.Any())
        {
            LoadMovies();
        }
    }

    public string GetApiContent(RestClient client, RestRequest request)
    {
        string json = string.Empty;
        try
        {
            json = client.Get(request).Content;
        }
        catch (Exception e)
        {
            Thread.Sleep(250);
            json = GetApiContent(client, request);
        }
        return json;
    }
    
    private void LoadMovies()
    {
        var token = config["MoviesApi:Key"];
        var ids = new List<int>();
        for (int page = 1; page <= 10; page++)
        {
            var options =
                new RestClientOptions(
                    $"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page={page}&sort_by=title.asc&vote_count.gte=309");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = GetApiContent(client, request);
            
            using JsonDocument jsonDoc = JsonDocument.Parse(response!);
            var root = jsonDoc.RootElement.GetProperty("results");

            foreach (var movie in root.EnumerateArray())
            {
                ids.Add(movie.GetProperty("id").GetInt32());
            }
        }

        foreach (var id in ids)
        {
            var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{id}?language=en-US");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = GetApiContent(client, request);

            Movie movie;

            using (JsonDocument jsonDoc = JsonDocument.Parse(response!))
            {
                var movieId = Guid.NewGuid();
                var movieApiId = jsonDoc.RootElement.GetProperty("id").GetInt32();
                movie = new Movie
                {
                    Id = movieId,
                    Title = jsonDoc.RootElement.GetProperty("title").GetString(),
                    Duration = jsonDoc.RootElement.GetProperty("runtime").GetInt32(),
                    Description = jsonDoc.RootElement.GetProperty("overview").GetString(),
                    ReleaseDate = jsonDoc.RootElement.GetProperty("release_date").GetDateTime(),
                    PosterPath = jsonDoc.RootElement.GetProperty("poster_path").GetString(),
                    RatingTmdb = jsonDoc.RootElement.GetProperty("vote_average").GetDecimal(),
                    Rating = 0,
                    RatingCount = 0
                };

                foreach (var genreJson in jsonDoc.RootElement.GetProperty("genres").EnumerateArray())
                {
                    var genre = context.Genres
                        .SingleOrDefault(x => x.ApiId == genreJson.GetProperty("id").GetInt32());

                    var movieGenre = new MovieGenre
                    {
                        MovieId = movieId,
                        Movie = movie,
                        GenreId = genre.Id,
                        Genre = genre
                    };

                    context.MovieGenres.Add(movieGenre);
                }
            }

            options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{id}/credits?language=en-US");
            client = new RestClient(options);
            request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            response = GetApiContent(client, request);

            using (JsonDocument jsonDoc = JsonDocument.Parse(response!))
            {
                var actors = new List<string>();
                var directors = new List<string>();

                foreach (var actorJson in jsonDoc.RootElement.GetProperty("cast").EnumerateArray())
                {
                    if (actorJson.GetProperty("known_for_department").GetString() == "Actor")
                        actors.Add(actorJson.GetProperty("name").GetString());
                    
                    if (actorJson.GetProperty("known_for_department").GetString() == "Directing")
                        directors.Add(actorJson.GetProperty("name").GetString());
                }
                movie.Actors = String.Join(", ", actors);
                movie.Director = String.Join(", ", directors);
            }
            
            context.Movies.Add(movie);
            context.SaveChanges();
        }
    }

    private void LoadCountries()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Data/GeoNames/countries.txt");

        if (!File.Exists(path))
        {
            return;
        }

        var lines = File.ReadAllLines(path)
            .Where(line => !line.StartsWith("#") && !string.IsNullOrWhiteSpace(line))
            .ToList();

        var countries = lines.Select(line =>
        {
            var parts = line.Split('\t');
            return new Country
            {
                Id = Guid.NewGuid(),
                Name = parts[4],
                Iso2 = parts[0]
            };
        }).ToList();

        context.Countries.AddRange(countries);
        context.SaveChanges();
    }

    private void LoadCities()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Data/GeoNames/cities500.txt");

        if (!File.Exists(path))
        {
            return;
        }

        var countryDict = context.Countries.ToDictionary(c => c.Iso2, c => c.Id);

        var lines = File.ReadAllLines(path)
                        .Where(line => !string.IsNullOrWhiteSpace(line))
                        .ToList();

        var cities = lines.Select(line =>
        {
            var parts = line.Split('\t');
            var countryIso2 = parts[8];

            if (!countryDict.TryGetValue(countryIso2, out var countryId))
                return null;

            return new City
            {
                Id = Guid.NewGuid(),
                Name = parts[1],
                CountryId = countryId
            };
        })
        .Where(city => city != null)
        .ToList();

        context.Cities.AddRange(cities);
        context.SaveChanges();
    }
    
    
}
