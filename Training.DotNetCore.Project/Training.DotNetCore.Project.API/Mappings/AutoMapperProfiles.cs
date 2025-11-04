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
            
            //Map Objects when its name of properties differ from source and target
            //Source Field Name: "Full Name" and Target Name:"Name"
            CreateMap<UserDTO, UserDomainModel>().ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName));
        }
        public class UserDTO
        {
            public string FullName { get; set; }
        }
        public class UserDomainModel
        {
            //public string FullName { get; set; }
            public string Name { get; set; }
        }
    }
}
