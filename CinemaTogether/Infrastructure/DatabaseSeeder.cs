using Domain.Entities;

namespace Infrastructure;

public class DatabaseSeeder(ApplicationDbContext context)
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
    }

    private void LoadCountries()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Data\\GeoNames\\countries.txt");

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
