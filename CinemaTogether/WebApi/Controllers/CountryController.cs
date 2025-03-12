using Application.Common.Dto;
using Application.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/countries")]
public class CountryController(ICountryService countryService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CountryDto>))]
    [HttpGet]
    public async Task<IResult> GetCountries()
    {
        var countries = await countryService.GetCountriesAsync();
        
        return Results.Ok(countries);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CityDto>))]
    [HttpGet("{countryId:guid}/cities")]
    public async Task<IResult> GetCities(Guid countryId)
    {
        var cities = await countryService.GetCitiesAsync(countryId);
        
        return Results.Ok(cities);
    }
}
