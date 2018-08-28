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
    public class AnimalsController : Controller
    {
        IAnimalRepository _animalRepository;
        IPhotoRepository _photoRepository;

        int page = 1;
        int pageSize = 10;

        public AnimalsController(IAnimalRepository animalRepository, IPhotoRepository photoRepository)
        {
            _animalRepository = animalRepository;
            _photoRepository = photoRepository;
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
                .GetAll()
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(currentPage)
                .ToList();

            IEnumerable<AnimalViewModel> _animalViewModel = Mapper.Map<IEnumerable<Animal>, IEnumerable<AnimalViewModel>>(_animals);

            Response.AddPagination(page, pageSize, totalAnimals, totalPages);

            return new OkObjectResult(_animalViewModel);
        }

        [HttpGet("{id}", Name = "GetAnimal")]
        public IActionResult Get(int id)
        {
            Animal _animal = _animalRepository.GetSingle(a => a.Id == id);

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
