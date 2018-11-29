using AnimalShelters.API.Core;
using AnimalShelters.API.ViewModels;
using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.Controllers
{
    [Route("api/[controller]")]
    public class AnimalsController : Controller
    {
        IAnimalRepository _animalRepository;
        IPhotoRepository _photoRepository;
        IAnimalShelterRepository _animalShelterRepository;

        int page = 1;
        int pageSize = 10;

        public AnimalsController(IAnimalRepository animalRepository, IPhotoRepository photoRepository,
            IAnimalShelterRepository animalShelterRepository)
        {
            _animalRepository = animalRepository;
            _photoRepository = photoRepository;
            _animalShelterRepository = animalShelterRepository;
        }

        public IActionResult Get()
        {
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }

            int currentPage = page;
            int currentPageSize = pageSize;
            var totalAnimals = _animalRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalAnimals / pageSize);

            IEnumerable<Animal> _animals = _animalRepository
                .AllIncluding(a => a.AnimalsToAnimalShelter)
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(currentPage)
                .ToList();

            IEnumerable<AnimalViewModel> _animalViewModel = Mapper.Map<IEnumerable<Animal>, IEnumerable<AnimalViewModel>>(_animals);

            Response.AddPagination(page, pageSize, totalAnimals, totalPages);

            return new OkObjectResult(_animalViewModel);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IEnumerable<Animal> _animals = _animalRepository
                .AllIncluding(a => a.AnimalsToAnimalShelter)
                .OrderBy(a => a.Id)
                .ToList();

            IList<AnimalDetailsViewModel> _animalViewModel = new List<AnimalDetailsViewModel>();

            foreach (var animal in _animals)
            {

                AnimalShelter _animalShelterDb = _animalShelterRepository.GetSingle(s => s.Id == animal.AnimalsToAnimalShelter.AnimalShelterId);
                AnimalDetailsViewModel _animalDetailsViewModel = Mapper.Map<Animal, AnimalDetailsViewModel>(animal);
                _animalDetailsViewModel.AnimalShelter = Mapper.Map<AnimalShelter, AnimalShelterViewModel>(_animalShelterDb);
                _animalViewModel.Add(_animalDetailsViewModel);
            }

            return new OkObjectResult(_animalViewModel);
        }

        [HttpGet("{id}", Name = "GetAnimal")]
        public IActionResult Get(int id)
        {
            Animal _animal = _animalRepository.GetSingle(a => a.Id == id, a => a.AnimalsToAnimalShelter);

            if (_animal != null)
            {
                AnimalViewModel _animalViewModel = Mapper.Map<Animal, AnimalViewModel>(_animal);

                return new OkObjectResult(_animalViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/details", Name = "GetAnimalDetails")]
        public IActionResult GetDetails(int id)
        {
            Animal _animal = _animalRepository.GetSingle(a => a.Id == id, a => a.AnimalsToAnimalShelter);

            if (_animal != null)
            {
                AnimalDetailsViewModel _animalViewModel = Mapper.Map<Animal, AnimalDetailsViewModel>(_animal);

                AnimalShelter _animalShelterDb = _animalShelterRepository.GetSingle(s => s.Id == _animal.AnimalsToAnimalShelter.AnimalShelterId);
                _animalViewModel.AnimalShelter = Mapper.Map<AnimalShelter, AnimalShelterViewModel>(_animalShelterDb);

                return new OkObjectResult(_animalViewModel);
            }
            else {
                return NotFound();
            }
        }

        [HttpGet("search", Name = "SearchAnimals")]
        public IActionResult Get([FromBody]AnimalSearchViewModel animalsSearch)
        {
            AnimalSexEnum sex = (AnimalSexEnum)Enum.Parse(typeof(AnimalSexEnum), animalsSearch.Sex);
            AnimalSpeciesEnum species = (AnimalSpeciesEnum)Enum.Parse(typeof(AnimalSpeciesEnum), animalsSearch.Species);
            IEnumerable<Animal> _animals = _animalRepository.FindBy(a =>
            a.Name == (animalsSearch.Name.Length > 0 ? animalsSearch.Name : a.Name) &&
            a.Breed == (animalsSearch.Breed.Length > 0 ? animalsSearch.Breed : a.Breed) &&
            a.Sex == sex &&
            a.Species == species).ToList();

            IEnumerable<AnimalViewModel> _animalViewModel = Mapper.Map<IEnumerable<Animal>, IEnumerable<AnimalViewModel>>(_animals);

            return new OkObjectResult(_animalViewModel);
        }

        [HttpGet("newest", Name = "NewestAnimals")]
        public IActionResult GetNewestAnimals()
        {
            IEnumerable<Animal> _animals = _animalRepository
                   .AllIncluding(a => a.AnimalsToAnimalShelter)
                   .OrderByDescending(a => a.InShelterFrom)
                   .ToList();

            IList<AnimalDetailsViewModel> _animalViewModel = new List<AnimalDetailsViewModel>();

            foreach (var animal in _animals)
            {

                AnimalShelter _animalShelterDb = _animalShelterRepository.GetSingle(s => s.Id == animal.AnimalsToAnimalShelter.AnimalShelterId);
                AnimalDetailsViewModel _animalDetailsViewModel = Mapper.Map<Animal, AnimalDetailsViewModel>(animal);
                _animalDetailsViewModel.AnimalShelter = Mapper.Map<AnimalShelter, AnimalShelterViewModel>(_animalShelterDb);
                _animalViewModel.Add(_animalDetailsViewModel);
            }

            return new OkObjectResult(_animalViewModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody]AnimalViewModel animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Animal _newAnimal = Mapper.Map<AnimalViewModel, Animal>(animal);
            _animalRepository.Add(_newAnimal);
            _animalRepository.Commit();

            animal = Mapper.Map<Animal, AnimalViewModel>(_newAnimal);

            CreatedAtRouteResult result = CreatedAtRoute("GetAnimal", new { controller = "Animals", id = animal.Id }, animal);

            return result;
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody]AnimalViewModel animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Animal _animalDb = _animalRepository.GetSingle(id);

            if (_animalDb == null)
            {
                return NotFound();
            }
            else
            {
                _animalDb.Name = animal.Name;
                _animalDb.Age = animal.Age;
                _animalDb.AgeAccuracy = (AnimalAgeAccuracyEnum)Enum.Parse(typeof(AnimalAgeAccuracyEnum), animal.AgeAccuracy);
                _animalDb.Breed = animal.Breed;
                _animalDb.Description = animal.Description;
                _animalDb.Sex = (AnimalSexEnum)Enum.Parse(typeof(AnimalSexEnum), animal.Sex);
                _animalDb.Size = (AnimalSizeEnum)Enum.Parse(typeof(AnimalSizeEnum), animal.Size);
                _animalDb.Species = (AnimalSpeciesEnum)Enum.Parse(typeof(AnimalSpeciesEnum), animal.Species);
            }

            _animalRepository.Commit();

            animal = Mapper.Map<Animal, AnimalViewModel>(_animalDb);

            return new NoContentResult();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Animal _animal = _animalRepository.GetSingle(id);

            if (_animal == null)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<Photo> _photos = _photoRepository.FindBy(p => p.AnimalId == id);

                foreach(var photo in _photos)
                {
                    _photoRepository.Delete(photo);
                }

                _animalRepository.Delete(_animal);
                _animalRepository.Commit();

                return new NoContentResult();
            }
        }
    }
}
