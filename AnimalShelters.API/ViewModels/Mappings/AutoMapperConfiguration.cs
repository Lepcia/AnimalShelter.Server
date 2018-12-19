using AnimalShelters.API.UserDtoN;
using AnimalShelters.Model.Entities;
using AutoMapper;

namespace AnimalShelters.API.ViewModels.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
                x.CreateMap<User, UserDto>();
                x.CreateMap<UserDto, User>();
            });
        }
    }
}
