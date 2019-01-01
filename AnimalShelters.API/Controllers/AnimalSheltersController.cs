﻿using AnimalShelters.API.Core;
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
    public class AnimalSheltersController : Controller
    {
        IAnimalShelterRepository _animalShelterRepository;
        IAnimalRepository _animalRepository;
        IUserRepository _userRepository;

        int page = 1;
        int pageSize = 10;

        public AnimalSheltersController(IAnimalShelterRepository animalShelterRepository, IAnimalRepository animalRepository,
            IUserRepository userRepository)
        {
            _animalShelterRepository = animalShelterRepository;
            _animalRepository = animalRepository;
            _userRepository = userRepository;
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
            var totalUsers = _animalShelterRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            IEnumerable<AnimalShelter> _shelters = _animalShelterRepository
                .AllIncluding(u => u.Animals, u => u.UsersToAnimalShelter)
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(currentPageSize)
                .ToList();

            IEnumerable<AnimalShelterViewModel> _sheltersViewModels = Mapper.Map<IEnumerable<AnimalShelter>, IEnumerable<AnimalShelterViewModel>>(_shelters);

            Response.AddPagination(page, pageSize, totalUsers, totalPages);

            return new OkObjectResult(_sheltersViewModels);
        }

        [HttpGet("all")]
        public IActionResult GatAll()
        {
            IEnumerable<AnimalShelter> _animalShelters = _animalShelterRepository
                .AllIncluding(u => u.Animals)
                .OrderBy(s => s.Id)
                .ToList();

            IEnumerable<AnimalShelterViewModel> _shelterViewModel = Mapper.Map<IEnumerable<AnimalShelter>, IEnumerable<AnimalShelterViewModel>>(_animalShelters);

            return new OkObjectResult(_shelterViewModel);
        }

        [HttpPost("search")]
        public IActionResult SearchShelters([FromBody]AnimalShelterSearchViewModel searchShelters)
        {
            IEnumerable<AnimalShelter> _animalShelters = _animalShelterRepository.AllIncluding(a => a.Animals, a => a.UsersToAnimalShelter)
                .Where(a => a.Name.ToUpper().Contains(searchShelters.Name.Length > 0 ? searchShelters.Name.ToUpper() : a.Name.ToUpper()) && 
                a.City.ToUpper().Contains(searchShelters.City.Length > 0 ? searchShelters.City.ToUpper() : a.City.ToUpper())
                && a.Street.ToUpper().Contains(searchShelters.Street.Length > 0 ? searchShelters.Street.ToUpper() : a.Street.ToUpper())).ToList();

            IEnumerable<AnimalShelterViewModel> _sheltetViewModel = Mapper.Map<IEnumerable<AnimalShelter>, IEnumerable<AnimalShelterViewModel>> (_animalShelters);

            return new OkObjectResult(_sheltetViewModel);
        }

        [HttpGet("{id}", Name = "GetShelter")]
        public IActionResult Get(int id)
        {
            AnimalShelter _shelter = _animalShelterRepository.GetSingle(s => s.Id == id, s => s.Animals, s => s.UsersToAnimalShelter);

            if (_shelter != null)
            {
                AnimalShelterViewModel _shelterViewModel = Mapper.Map<AnimalShelter, AnimalShelterViewModel>(_shelter);
                return new OkObjectResult(_shelterViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/details", Name = "GetShelterDetails")]
        public IActionResult GetDetails(int id)
        {
            AnimalShelter _shelter = _animalShelterRepository.GetSingle(s => s.Id == id, s => s.Animals, s => s.UsersToAnimalShelter);

            if (_shelter != null)
            {
                AnimalShelterDetailsViewModel _shelterViewModel = Mapper.Map<AnimalShelter, AnimalShelterDetailsViewModel>(_shelter);
                foreach (var animal in _shelter.Animals)
                {
                    Animal _animalDb = _animalRepository.GetSingle(animal.Id);
                    _shelterViewModel.Animals.Add(Mapper.Map<Animal, AnimalViewModel>(_animalDb));
                }

                foreach (var user in _shelter.UsersToAnimalShelter)
                {
                    User _userDb = _userRepository.GetSingle(user.UserId);
                    _shelterViewModel.Users.Add(Mapper.Map<User, UserViewModel>(_userDb));
                }

                return new OkObjectResult(_shelterViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/animals", Name = "GetShelterAnimals")]
        public IActionResult GetShelterAnimals(int id)
        {
            AnimalShelter _shelter = _animalShelterRepository.GetSingle(s => s.Id == id, s => s.Animals);

            if (_shelter != null)
            {
                List<AnimalDetailsViewModel> _animalViewModel = new List<AnimalDetailsViewModel>();

                foreach (var animal in _shelter.Animals)
                {
                    Animal _animalDb = _animalRepository.GetSingle(animal.Id);
                    _animalViewModel.Add(Mapper.Map<Animal, AnimalDetailsViewModel>(_animalDb));
                }

                return new OkObjectResult(_animalViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("simple", Name = "GetSimpleShelters")]
        public IActionResult GetSimpleShelters()
        {
            IEnumerable<AnimalShelter> _animalShelters = _animalShelterRepository.GetAll();

            IEnumerable<AnimalShelterSimpleViewModel> _simpleViewModel = Mapper.Map<IEnumerable<AnimalShelter>, IEnumerable<AnimalShelterSimpleViewModel>>(_animalShelters);

            return new OkObjectResult(_simpleViewModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody]AnimalShelterViewModel shelter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AnimalShelter _newShelter = Mapper.Map<AnimalShelterViewModel, AnimalShelter>(shelter);

            _animalShelterRepository.Add(_newShelter);
            _animalShelterRepository.Commit();

            CreatedAtRouteResult result = CreatedAtRoute("GetShelter", new { controller = "AnimalShelters", id = shelter.Id }, shelter);

            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]AnimalShelterViewModel shelter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AnimalShelter _shelterDb = _animalShelterRepository.GetSingle(id);
            if (_shelterDb == null)
            {
                return NotFound();
            }
            else
            {
                _shelterDb.Name = shelter.Name;
                _shelterDb.Email = shelter.Email;
                _shelterDb.Phone = shelter.Phone;
                _shelterDb.City = shelter.City;
                _shelterDb.Avatar = shelter.Avatar;
                _shelterDb.PostalCode = shelter.PostalCode;
                _shelterDb.Street = shelter.Street;
                _shelterDb.Number = shelter.Number;
            }

            _animalShelterRepository.Commit();

            shelter = Mapper.Map<AnimalShelter, AnimalShelterViewModel>(_shelterDb);
            return new NoContentResult();
        }

        [HttpPut("{id}/addAnimal")]
        public IActionResult AddAnimal(int id, [FromBody]int animalId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AnimalShelter _animalShelter = _animalShelterRepository.GetSingle(s => s.Id == id, s => s.Animals);
            Animal _animal = _animalRepository.GetSingle(a => a.Id == animalId);

            if (_animalShelter != null && _animal != null)
            {
                if (_animalShelter.Animals.Where(a => a.Id == animalId).Count() == 0)
                {
                    _animalShelter.Animals.Add(new AnimalsToAnimalShelter { AnimalId = animalId, AnimalShelterId = _animalShelter.Id });
                    _animalShelterRepository.Commit();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Animal already in this animal shelter!");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AnimalShelter _shelter = _animalShelterRepository.GetSingle(id);

            if (_shelter == null)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<Animal> _animals = _animalRepository.FindBy(a => a.AnimalsToAnimalShelter.AnimalShelterId == id );

                foreach (var animal in _animals)
                {
                    _animalRepository.Delete(animal);
                }

                _animalRepository.Commit();

                _animalShelterRepository.Delete(_shelter);
                _animalShelterRepository.Commit();

                return new NoContentResult();
            }
        }

        [HttpDelete("{idUser}/{idAnimal}")]
        public IActionResult DeleteAnimalFromShelter(int idShelter, int idAnimal)
        {
            AnimalShelter _animalShelter = _animalShelterRepository.GetSingle(idShelter);
            Animal _animal = _animalRepository.GetSingle(a => a.Id == idAnimal && a.AnimalsToAnimalShelter.AnimalShelterId == idShelter);

            if (_animalShelter == null || _animal == null)
            {
                return NotFound();
            }
            else
            {
                ICollection<AnimalsToAnimalShelter> animals = _animalShelter.Animals.Where(a => a.AnimalId != _animal.Id).ToList();
                _animalShelter.Animals = animals;
                _animalShelterRepository.Commit();

                _animalRepository.Delete(_animal);
                _animalRepository.Commit();

                return new NoContentResult();
            }
        }
    }
}
