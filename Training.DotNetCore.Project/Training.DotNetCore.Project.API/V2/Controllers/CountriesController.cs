using Microsoft.AspNetCore.Mvc;
using Training.DotNetCore.Project.API.Data;
using Training.DotNetCore.Project.API.DTO;

namespace Training.DotNetCore.Project.API.V2.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var countriesDomainModel = CountriesData.Get();

            //Map domain model to DTO
            var response = new List<CountryDtoV2>();
            foreach (var countryDomain in countriesDomainModel)
            {
                response.Add(new CountryDtoV2
                {
                    Id = countryDomain.Id,
                    CountryName = countryDomain.Name
                });
            }
            return Ok(response);
        }
    }
}
