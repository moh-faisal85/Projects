using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.DotNetCore.Project.API.DTO;
using Training.DotNetCore.Project.API.Models.Domain;
using Training.DotNetCore.Project.API.Repositories;

namespace Training.DotNetCore.Project.API.Controllers
{
    //https://localhost:port/api/controllerName
    //https://localhost:7251/api/regions

    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        //public RegionsController(NZWalksDbContext _dbContext, IRegionRepository _regionRepository)
        public RegionsController(IRegionRepository _regionRepository, IMapper _mapper)
        {
            //this.dbContext = _dbContext;
            this.regionRepository = _regionRepository;
            this.mapper = _mapper;
        }

        //POST To Create New Region 
        //POST: https:..localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            //var regionDomainModel = new Region
            //{
            //    code = addRegionRequestDto.code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
            //};

            //Map or Convert DTO to Domain Model - Source: addRegionRequestDto and Target: Region
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            //Call Repository method instead of calling dbcontext method directly.
            //await dbContext.Regions.AddAsync(regionDomainModel);
            //await dbContext.SaveChangesAsync();
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            //Map Domain Model back to DTO

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    code = regionDomainModel.code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            //Map or Convert Domain Model to DTO- Source:RegionDomainModel  and Target: RegionDto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        ////GET ALL REGION
        ////GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll() //async Task Added to make this action async
        {
            //Get Data From Database - Domain Models
            //var regionDomainModels = await dbContext.Regions.ToListAsync();//await added to enable this async operation
            var regionDomainModels = await regionRepository.GetAllAsync();//await added to enable this async operation

            // Map Domain Models to DTOs
            //var regionDto = new List<RegionDto>();
            //foreach (var regionDomainModel in regionDomainModels)
            //{
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = regionDomainModel.Id,
            //        Name = regionDomainModel.Name,
            //        code = regionDomainModel.code,
            //        RegionImageUrl = regionDomainModel.RegionImageUrl
            //    });
            //}

            //Map object regionDomainModels to RegionDto
            var regionDtos = mapper.Map<List<RegionDto>>(regionDomainModels);
            return Ok(regionDtos);
        }

        // GET SINGLE REGION (Get Region By ID) 
        // GET: http://localhost:portnumber/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Data From Database - Domain Models

            //var region = dbContext.Regions.Find(id);//Find mehtod used to get record based on PRIMARY KEY field column value like Id
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);//We can use this approaches to filter data based on ANY column value
            var regionDomainModel = await regionRepository.GetByIdAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain Models to DTOs
            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    code = regionDomainModel.code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        // Update Exisitng Region
        // PUT: http://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check region exists
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);//We can use this approaches to filter data based on ANY column value

            //var regionDomainModel = new Region
            //{
            //    Id = id,//From Id Parameter
            //    code = updateRegionRequestDto.code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};
            //Map DTO to Domain Model Object - Source:updateRegionRequestDto, Target: Region
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //// Map DTO  to Domain Models
            //regionDomainModel.Name = updateRegionRequestDto.Name;
            //regionDomainModel.code = updateRegionRequestDto.code;
            //regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            ////Save Changes
            //await dbContext.SaveChangesAsync();

            //Convert Domain Model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    code = regionDomainModel.code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            //Map Domain Model  to DTO Object - Source:Region (regionDomainModel), Target: RegionDto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        //Detele Region 
        // DELETE: http://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Check region exists
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);//We can use this approaches to filter data based on ANY column value
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Delete Region if exists
            //dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();
             
            //Return Deleted Region back
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    code = regionDomainModel.code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            //Map Domain Model  to DTO Object - Source:Region (regionDomainModel), Target: RegionDto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
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