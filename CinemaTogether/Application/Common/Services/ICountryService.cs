using Application.Common.Dto;

namespace Application.Common.Services;

public interface ICountryService
{
    Task<List<CountryDto>> GetCountriesAsync();
    
    Task<List<CityDto>> GetCitiesAsync(Guid countryId);
}