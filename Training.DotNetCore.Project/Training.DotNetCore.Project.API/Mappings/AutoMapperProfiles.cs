using AutoMapper;

namespace Training.DotNetCore.Project.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //DTO to Domain Model Mapping when number of Properties and its types match with source and Target Object
            CreateMap<UserDTO, UserDomainModel>();

            //Domain Model to DTO Mapping or Reverse Mapping
            //CreateMap<UserDomainModel, UserDTO>();
            CreateMap<UserDTO, UserDomainModel>().ReverseMap();


        }
        public class UserDTO
        {
            public string FullName { get; set; }
        }
        public class UserDomainModel
        {
            public string FullName { get; set; }
        }
    }
}
