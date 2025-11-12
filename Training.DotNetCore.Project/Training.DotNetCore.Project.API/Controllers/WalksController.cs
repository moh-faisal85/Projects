using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.DotNetCore.Project.API.DTO;
using Training.DotNetCore.Project.API.Models.Domain;
using Training.DotNetCore.Project.API.Repositories;

namespace Training.DotNetCore.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CREATE WALK
        //POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map DTO to DomainModel
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            //Map DomainModel to DTO and return to client
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        //GET WALKS
        //GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

            return Ok(walksDto);
        }

        //GET WALK
        //GET: /api/walks/id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get data from database - Domain Model
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //Convert Domain Model to Dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            //Return dto to client
            return Ok(walkDto);
        }

        //PUT WALK
        //PUT: /api/walks/id
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Map DTO to Domain Model Object
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            //Invoke Repository Method to perform Update
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            //Return NotFound - 404 if value is null
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model  to DTO Object 
            var WalkDto = mapper.Map<WalkDto>(walkDomainModel);

            //Return Updated data with Success flag
            return Ok(WalkDto);
        }

        //DELETE WALK by Id
        //DELETE /api/walks/id
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain Model to Dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            //Return Value to client
            return Ok(walkDto);
        }
    }
}
