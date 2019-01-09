using AnimalShelters.API.Core;
using AnimalShelters.API.Helpers;
using AnimalShelters.API.Services;
using AnimalShelters.API.UserDtoN;
using AnimalShelters.API.ViewModels;
using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelters.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;
        private IAnimalRepository _animalRepository;
        private IAnimalShelterRepository _animalShelterRepository;
        private IFavoriteAnimalRepository _favoriteAnimalRepository;
        private IRoleRepository _roleRepository;
        private IUserService _userService;
        private readonly AppSettings _appSettings;

        int page = 1;
        int pageSize = 10;

        public UsersController(IUserRepository userRepository, IAnimalRepository animalRepository,
            IFavoriteAnimalRepository favoriteAnimalRepository, IAnimalShelterRepository animalShelterRepository,
            IUserService userService, IOptions<AppSettings> appSettings, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _animalRepository = animalRepository;
            _animalShelterRepository = animalShelterRepository;
            _favoriteAnimalRepository = favoriteAnimalRepository;
            _userService = userService;
            _appSettings = appSettings.Value;
            _roleRepository = roleRepository;

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Email, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString,
                Role = user.Role,
                ShelterId = user.UserToAnimalShelter != null ? user.UserToAnimalShelter.AnimalShelterId : 0
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            var user = Mapper.Map<User>(userDto);
            Role role = _roleRepository.GetSingle(r => r.Name == userDto.RoleName);
            if (role != null)
            {
                user.Role = role;
            }

            try
            {
                // save 
                _userService.Create(user, userDto.Password);
                return new OkObjectResult(user);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = Mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
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
            User user = _userService.GetById(id);
            var userDto = Mapper.Map<UserDto>(user);
            return Ok(userDto);
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = Mapper.Map<User>(userDto);
            user.Id = id;

            Role role = _roleRepository.GetSingle(r => r.Name == userDto.RoleName);
            if (role != null)
            {
                user.Role = role;
            }

            try
            {
                // save 
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
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

            return new OkObjectResult(new { idAnimal = animalId });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [HttpDelete("{idUser}/{idAnimal}")]
        public IActionResult DeleteFavoriteAnimal(int idUser, int idAnimal)
        {
            User _user = _userService.GetById(idUser);
            FavoriteAnimal _favoriteAnimal = _favoriteAnimalRepository.GetSingle(fa => fa.AnimalId == idAnimal && fa.UserId == idUser);

            if (_user == null || _favoriteAnimal == null)
            {
                return NotFound();
            }
            else
            {
                _favoriteAnimalRepository.Delete(_favoriteAnimal);
                _favoriteAnimalRepository.Commit();

                return new OkObjectResult(new { idAnimal = idAnimal });
            }
        }


    }
}
