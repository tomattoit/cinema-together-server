using Application.Common.Dto;
using Application.Common.Services;
using Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CountryService(IApplicationDbContext context) : ICountryService
{
    public async Task<List<CountryDto>> GetCountriesAsync()
    {
        var countries = await context.Countries
            .AsNoTracking()
            .Select(c => new CountryDto(c.Id, c.Name))
            .ToListAsync();
        
        return countries;
    }

    public async Task<List<CityDto>> GetCitiesAsync(Guid countryId)
    {
        var cities = await context.Cities
            .AsNoTracking()
            .Where(c => c.CountryId == countryId)
            .Select(c => new CityDto(c.Id, c.Name))
            .ToListAsync();
        
        return cities;
    }
}