using AutoMapper;
using AnimalShelters.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimalShelters.Model.Entities;

namespace AnimalShelters.API.ViewModels.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
         public DomainToViewModelMappingProfile()
         {
            CreateMap<User, UserViewModel>()
                .ForMember(vm => vm.FavoriteAnimals,
                map => map.MapFrom(fa => fa.FavoriteAnimals.Select(a => a.AnimalId)))
                .ForMember(vm => vm.Role,
                map => map.MapFrom(u => ((RoleEnum)u.Role).ToString()))
                .ForMember(vm => vm.UserToAnimalShelter,
                map => map.MapFrom(utas => utas.UserToAnimalShelter.AnimalShelterId));

            CreateMap<User, UserDetailsViewModel>()
                .ForMember(vm => vm.FavoriteAnimals,
                map => map.UseValue(new List<AnimalViewModel>()))
                .ForMember(vm => vm.UserToAnimalShelter,
                map => map.UseValue(new AnimalShelterViewModel()));

            CreateMap<AnimalShelter, AnimalShelterViewModel>()
                .ForMember(vm => vm.Animals,
                map => map.MapFrom(asvm => asvm.Animals.Select(a => a.AnimalId)))
                .ForMember(vm => vm.Users, 
                map => map.MapFrom(utas => utas.UsersToAnimalShelter.Select(a => a.UserId)))
                .ForMember(vm => vm.FullAdres,
                map => map.MapFrom(asvm => asvm.FullAdres));

            CreateMap<AnimalShelter, AnimalShelterDetailsViewModel>()
                .ForMember(vm => vm.Animals,
                map => map.UseValue(new List<AnimalViewModel>()))
                .ForMember(vm => vm.Users,
                map => map.UseValue(new List<UserViewModel>()));

            CreateMap<Animal, AnimalViewModel>()
                .ForMember(vm => vm.Photos,
                map => map.MapFrom(avm => avm.Photos.Select(p => p.Id)))
                .ForMember(vm => vm.AnimalsToAnimalShelter,
                map => map.MapFrom(a => a.AnimalsToAnimalShelter.AnimalShelterId));

            CreateMap<Animal, AnimalDetailsViewModel>()
                .ForMember(vm => vm.Photos,
                map => map.UseValue(new List<PhotoViewModel>()))
                .ForMember(vm => vm.AnimalShelter,
                map => map.UseValue(new AnimalShelterViewModel()))
                .ForMember(vm => vm.AgeAccuracy,
                map => map.MapFrom(s => ((AnimalAgeAccuracyEnum)s.AgeAccuracy).ToString()))
                .ForMember(vm => vm.AgesAccuracy,
                map => map.UseValue(Enum.GetNames(typeof(AnimalAgeAccuracyEnum)).ToArray()))
                .ForMember(vm => vm.Sex,
                map => map.MapFrom(s => ((AnimalSexEnum)s.Sex).ToString()))
                .ForMember(vm => vm.Sexes,
                map => map.UseValue(Enum.GetNames(typeof(AnimalSexEnum)).ToArray()))
                .ForMember(vm => vm.Size,
                map => map.MapFrom(s => ((AnimalSizeEnum)s.Size).ToString()))
                .ForMember(vm => vm.Species,
                map => map.MapFrom(s => ((AnimalSpeciesEnum)s.Species).ToString()))
                .ForMember(vm => vm.Sizes,
                map => map.UseValue(Enum.GetNames(typeof(AnimalSizeEnum)).ToArray()));

            CreateMap<Photo, PhotoViewModel>();

            CreateMap<AnimalShelter, AnimalShelterSimpleViewModel>();
         }
    }
}
