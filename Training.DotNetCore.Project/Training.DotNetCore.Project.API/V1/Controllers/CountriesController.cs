using Microsoft.AspNetCore.Mvc;
using Training.DotNetCore.Project.API.Data;
using Training.DotNetCore.Project.API.DTO;

namespace Training.DotNetCore.Project.API.V1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var countriesDomainModel = CountriesData.Get();

            //Map domain model to DTO
            var response = new List<CountryDtoV1>();
            foreach (var countryDomain in countriesDomainModel)
            {
                response.Add(new CountryDtoV1
                {
                    Id = countryDomain.Id,
                    Name = countryDomain.Name
                });
            }
            return Ok(response);
        }
    }
}
