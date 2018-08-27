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
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;
        private IAnimalRepository _animalRepository;
        private IFavoriteAnimalRepository _favoriteAnimalRepository;

        int page = 1;
        int pageSize = 10;

        public UsersController(IUserRepository userRepository, IAnimalRepository animalRepository,
            IFavoriteAnimalRepository favoriteAnimalRepository)
        {
            _userRepository = userRepository;
            _animalRepository = animalRepository;
            _favoriteAnimalRepository = favoriteAnimalRepository;
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
            var totalUsers = _userRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            IEnumerable<User> _users = _userRepository
                .AllIncluding(u => u.FavoriteAnimals)
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(currentPageSize)
                .ToList();

            IEnumerable<UserViewModel> _usersViewModels = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(_users);

            Response.AddPagination(page, pageSize, totalUsers, totalPages);

            return new OkObjectResult(_usersViewModels);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            User _user = _userRepository.GetSingle(u => u.Id == id, u => u.FavoriteAnimals);

            if (_user != null)
            {
                UserViewModel _userViewModel = Mapper.Map<User, UserViewModel>(_user);
                return new OkObjectResult(_userViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/details", Name = "GetUserDetails")]
        public IActionResult GetDetails(int id)
        {
            User _user = _userRepository.GetSingle(u => u.Id == id, u => u.FavoriteAnimals);

            if (_user != null)
            {
                UserDetailsViewModel _userViewModel = Mapper.Map<User, UserDetailsViewModel>(_user);

                foreach (var animal in _user.FavoriteAnimals)
                {
                    Animal _animalDb = _animalRepository.GetSingle(animal.Id);
                    _userViewModel.FavoriteAnimals.Add(Mapper.Map<Animal, AnimalViewModel>(_animalDb));
                }

                return new OkObjectResult(_userViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/favoriteAnimals", Name = "GetUserFavoriteAnimals")]
        public IActionResult GetFavoriteAnimals(int id)
        {
            User _user = _userRepository.GetSingle(u => u.Id == id, u => u.FavoriteAnimals);

            if (_user != null)
            {
                List<AnimalViewModel> _animalViewModel = new List<AnimalViewModel>();

                foreach (var animal in _user.FavoriteAnimals)
                {
                    Animal _animalDb = _animalRepository.GetSingle(animal.Id);
                    _animalViewModel.Add(Mapper.Map<Animal, AnimalViewModel>(_animalDb));
                }

                return new OkObjectResult(_animalViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User _newUser = Mapper.Map<UserViewModel, User>(user);
            _userRepository.Add(_newUser);
            _userRepository.Commit();

            user = Mapper.Map<User, UserViewModel>(_newUser);

            CreatedAtRouteResult result = CreatedAtRoute("GetUser", new { controller = "Users", id = user.Id }, user);

            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User _userDb = _userRepository.GetSingle(id);

            if (_userDb == null)
            {
                return NotFound();
            }
            else
            {
                _userDb.FirstName = user.FirstName;
                _userDb.LastName = user.LastName;
                _userDb.Email = user.Email;
                _userDb.Avatar = user.Avatar;
                _userDb.DateOfBirth = user.DateOfBirth;
            }

            _userRepository.Commit();

            user = Mapper.Map<User, UserViewModel>(_userDb);

            return new NoContentResult();
        }

        [HttpPut("{id}/addFavoriteAnimal")]
        public IActionResult AddFavoriteAnimal(int id, [FromBody]AnimalViewModel animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User _user = _userRepository.GetSingle(u => u.Id == id, u => u.FavoriteAnimals);

            if (User != null)
            {
                Animal _newFavoriteAnimal = Mapper.Map<AnimalViewModel, Animal>(animal);
                _user.FavoriteAnimals.Add(_newFavoriteAnimal);
                _userRepository.Commit();
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
            User _user = _userRepository.GetSingle(id);

            if (_user == null)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<FavoriteAnimal> _favoriteAnimals = _favoriteAnimalRepository.FindBy(fa => fa.UserId == id);

                foreach(var animal in _favoriteAnimals)
                {
                    _favoriteAnimalRepository.Delete(animal);
                }

                _userRepository.Delete(_user);
                _userRepository.Commit();

                return new NoContentResult();
            }
        }


    }
}