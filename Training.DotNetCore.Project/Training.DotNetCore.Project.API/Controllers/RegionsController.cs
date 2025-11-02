using Microsoft.AspNetCore.Mvc;
using Training.DotNetCore.Project.API.Data;
using Training.DotNetCore.Project.API.DTO;
using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.Controllers
{
    //https://localhost:port/api/controllerName
    //https://localhost:7251/api/regions

    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext nZWalksDbContext)
        {
            this.dbContext = nZWalksDbContext;
        }

        ////GET ALL REGION
        ////GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data From Database - Domain Models
            var regionDomainModels = dbContext.Regions.ToList();

            // Map Domain Models to DTOs
            var regionDto = new List<RegionDto>();
            foreach (var regionDomainModel in regionDomainModels)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = regionDomainModel.Id,
                    Name = regionDomainModel.Name,
                    code = regionDomainModel.code,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                });
            }
            return Ok(regionDto);
        }

        // GET SINGLE REGION (Get Region By ID) 
        // GET: http://localhost:portnumber/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //Get Data From Database - Domain Models

            //var region = dbContext.Regions.Find(id);//Find mehtod used to get record based on PRIMARY KEY field column value like Id
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);//We can use this approaches to filter data based on ANY column value
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain Models to DTOs
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                code = regionDomainModel.code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

        //POST To Create New Region 
        //POST: https:..localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                code = addRegionRequestDto.code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map Domain Model back to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                code = regionDomainModel.code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update Exisitng Region
        // PUT: http://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check region exists
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);//We can use this approaches to filter data based on ANY column value
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map DTO  to Domain Models
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.code = updateRegionRequestDto.code;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            
            //Save Changes
            dbContext.SaveChanges();

            //Convert Domain Model to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                code =  regionDomainModel.code,
                RegionImageUrl= regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

        //Detele Region 
        // DELETE: http://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            //Check region exists
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);//We can use this approaches to filter data based on ANY column value
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Delete Region if exists
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            //Return Deleted Region back
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                code = regionDomainModel.code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }
    }
}
#region GetAll-Regions-Using-Hard-Coded-Data
////GET ALL REGION
////GET: https://localhost:portnumber/api/regions
//[HttpGet]
//public IActionResult GetAll()
//{
//    var region = new List<Region>
//    {
//        new Region
//        {
//            Id = Guid.NewGuid(),
//            Name = "Auckland Region",
//            code = "AKL",
//            RegionImageUrl = "https://images.pexels.com/photos/18884939/pexels-photo-18884939.jpeg"
//        },
//        new Region
//        {
//            Id = Guid.NewGuid(),
//            Name = "Wellington Region",
//            code = "WLG",
//            RegionImageUrl = "https://images.pexels.com/photos/2777898/pexels-photo-2777898.jpeg"
//        }
//    };
//    return Ok(region);
//}
#endregion