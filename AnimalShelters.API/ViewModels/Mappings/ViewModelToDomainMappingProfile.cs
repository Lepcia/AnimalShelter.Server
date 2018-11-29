using AnimalShelters.Model.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, User>()
                .ForMember(a => a.FavoriteAnimals, map => map.UseValue(new List<FavoriteAnimal>()));

            CreateMap<UserViewModel, User>()
                .ForMember(a => a.UserToAnimalShelter, map => map.UseValue(new UserToAnimalShelter()));

            CreateMap<AnimalShelterViewModel, AnimalShelter>()
                .ForMember(a => a.Animals, map => map.UseValue(new List<AnimalsToAnimalShelter>()))
                .ForMember(a => a.UsersToAnimalShelter, map => map.UseValue(new List<UserViewModel>()));

            CreateMap<PhotoViewModel, Photo>();

            CreateMap<AnimalViewModel, Animal>()
                .ForMember(a => a.Photos, map => map.UseValue(new List<Photo>()))
                .ForMember(a => a.AnimalsToAnimalShelter,
                map => map.UseValue(new AnimalsToAnimalShelter()));

        }
    }
}
