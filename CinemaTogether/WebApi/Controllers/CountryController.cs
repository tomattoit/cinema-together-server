using Application.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/countries")]
public class CountryController(ICountryService countryService) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetCountries()
    {
        var countries = await countryService.GetCountriesAsync();
        
        return Results.Ok(countries);
    }
    
    [HttpGet("{countryId:guid}/cities")]
    public async Task<IResult> GetCities(Guid countryId)
    {
        var cities = await countryService.GetCitiesAsync(countryId);
        
        return Results.Ok(cities);
    }
}
