using AutoMapper;
using Training.DotNetCore.Project.API.DTO;
using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //DTO to Domain Model Mapping when number of Properties and its types match with source and Target Object
            //CreateMap<UserDTO, UserDomainModel>();

            //Domain Model to DTO Mapping or Reverse Mapping
            //CreateMap<UserDomainModel, UserDTO>();
            //CreateMap<UserDTO, UserDomainModel>().ReverseMap();

            //Map Objects when its name of properties differ from source and target
            //Source Field Name: "Full Name" and Target Name:"Name"
            //CreateMap<UserDTO, UserDomainModel>().ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName));


            //Actual Implementation : Map Region object with RegionDto: This allows two way mappings.
            //Automapper having Capability to convert list to list and object to object with below mapping

            #region RegionController
            //Used at GetAll , GetById and Delete API Method
            CreateMap<Region, RegionDto>().ReverseMap();

            //Used at Create API Method to map values from AddRegionRequestDto to Region and vice versa
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            //Used at Update API Method to map values from UpdateRegionRequestDto to Region and vice versa
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            #endregion

            #region WalkController
            //Walks Controller used

            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            #endregion


        }
        //public class UserDTO
        //{
        //    public string FullName { get; set; }
        //}
        //public class UserDomainModel
        //{
        //    //public string FullName { get; set; }
        //    public string Name { get; set; }
        //}
    }
}
