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
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;
        private IAnimalRepository _animalRepository;
        private IAnimalShelterRepository _animalShelterRepository;
        private IFavoriteAnimalRepository _favoriteAnimalRepository;

        int page = 1;
        int pageSize = 10;

        public UsersController(IUserRepository userRepository, IAnimalRepository animalRepository,
            IFavoriteAnimalRepository favoriteAnimalRepository, IAnimalShelterRepository animalShelterRepository)
        {
            _userRepository = userRepository;
            _animalRepository = animalRepository;
            _animalShelterRepository = animalShelterRepository;
            _favoriteAnimalRepository = favoriteAnimalRepository;
        }

        [HttpGet]
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
            User _user = _userRepository.GetSingle(u => u.Id == id, u => u.FavoriteAnimals, u => u.UserToAnimalShelter);

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
            User _user = _userRepository.GetSingle(u => u.Id == id, u => u.FavoriteAnimals, u => u.UserToAnimalShelter);

            if (_user != null)
            {
                UserDetailsViewModel _userViewModel = Mapper.Map<User, UserDetailsViewModel>(_user);

                foreach (var animal in _user.FavoriteAnimals)
                {
                    Animal _animalDb = _animalRepository.GetSingle(animal.AnimalId);
                    _userViewModel.FavoriteAnimals.Add(Mapper.Map<Animal, AnimalViewModel>(_animalDb));
                }

                if (_user.UserToAnimalShelter != null)
                {
                    AnimalShelter _animalShelterDb = _animalShelterRepository.GetSingle(_user.UserToAnimalShelter.AnimalShelterId);
                    _userViewModel.UserToAnimalShelter = Mapper.Map<AnimalShelter, AnimalShelterViewModel>(_animalShelterDb);
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
                List<AnimalDetailsViewModel> _animalViewModel = new List<AnimalDetailsViewModel>();

                foreach (var animal in _user.FavoriteAnimals)
                {
                    Animal _animalDb = _animalRepository.GetSingle(a => a.Id == animal.AnimalId, a => a.AnimalsToAnimalShelter);
                    AnimalShelter _animalShelterDb = _animalShelterRepository.GetSingle(s => s.Id == _animalDb.AnimalsToAnimalShelter.AnimalShelterId);
                    AnimalDetailsViewModel _animalDetailsViewModel = Mapper.Map<Animal, AnimalDetailsViewModel>(_animalDb);
                    _animalDetailsViewModel.IsFavorite = true;
                    _animalDetailsViewModel.AnimalShelter = Mapper.Map<AnimalShelter, AnimalShelterViewModel>(_animalShelterDb);
                    _animalViewModel.Add(_animalDetailsViewModel);
                }

                return new OkObjectResult(_animalViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/favoriteIds", Name = "GetFavoriteIds")]
        public IActionResult GetFavoriteAnimalsIds(int id)
        {
            User _user = _userRepository.GetSingle(u => u.Id == id, u => u.FavoriteAnimals);

            List<int> favoriteIds = new List<int>();
            if (_user != null)
            {
                foreach (var animal in _user.FavoriteAnimals)
                {
                    favoriteIds.Add(animal.AnimalId);
                }
                return new OkObjectResult(new { favorites = favoriteIds });
            }
            return new OkObjectResult(new { favorites = favoriteIds });
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
        public IActionResult AddFavoriteAnimal(int id, int animalId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User _user = _userRepository.GetSingle(u => u.Id == id, u => u.FavoriteAnimals);
            Animal _animal = _animalRepository.GetSingle(a => a.Id == animalId);

            if (_user != null && _animal != null)
            {
                if (_user.FavoriteAnimals.Where(fa => fa.AnimalId == animalId).Count() == 0)
                {
                    _user.FavoriteAnimals.Add(new FavoriteAnimal { UserId = _user.Id, AnimalId = _animal.Id });
                    _userRepository.Commit();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Animal already in favorites!");
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
            User _user = _userRepository.GetSingle(id);

            if (_user == null)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<FavoriteAnimal> _favoriteAnimals = _favoriteAnimalRepository.FindBy(fa => fa.UserId == id);

                foreach (var animal in _favoriteAnimals)
                {
                    _favoriteAnimalRepository.Delete(animal);
                }

                _favoriteAnimalRepository.Commit();

                _userRepository.Delete(_user);
                _userRepository.Commit();

                return new NoContentResult();
            }
        }

        [HttpDelete("{idUser}/{idAnimal}")]
        public IActionResult DeleteFavoriteAnimal(int idUser, int idAnimal)
        {
            User _user = _userRepository.GetSingle(idUser);
            FavoriteAnimal _favoriteAnimal = _favoriteAnimalRepository.GetSingle(fa => fa.AnimalId == idAnimal && fa.UserId == idUser);

            if (_user == null || _favoriteAnimal == null)
            {
                return NotFound();
            }
            else
            {
                _favoriteAnimalRepository.Delete(_favoriteAnimal);
                _favoriteAnimalRepository.Commit();

                return new NoContentResult();
            }
        }


    }
}
