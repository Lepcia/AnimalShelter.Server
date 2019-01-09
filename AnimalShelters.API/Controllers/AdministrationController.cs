using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalShelters.API.ViewModels;
using AnimalShelters.Data.Abstract;
using AnimalShelters.Data.Repositories;
using AnimalShelters.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelters.API.Controllers
{
    [Route("api/[controller]")]
    public class AdministrationController : Controller
    {
        IRightsRepository _rightsRepository;
        IRightsToUserRepository _rightsToUserRepository;
        IModuleRepository _moduleRepository;
        IUserRepository _userRepository;
        IRoleRepository _roleRepository;
        IRightsToRoleRepository _rightsToRoleRepository;

        public AdministrationController(IRightsRepository rightsRepository, IRightsToUserRepository rightsToUserRepository,
            IModuleRepository moduleRepository, IUserRepository userRepository, IRoleRepository roleRepository,
            IRightsToRoleRepository rightsToRoleRepository)
        {
            _rightsRepository = rightsRepository;
            _rightsToUserRepository = rightsToUserRepository;
            _moduleRepository = moduleRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _rightsToRoleRepository = rightsToRoleRepository;
        }

        public IActionResult Get()
        {
            IEnumerable<RightsDetailsViewModel> _rightsDetailsViewModels = _rightsToUserRepository.GetContext()
                .Select(x => new RightsDetailsViewModel
                {
                    Id = x.Id,
                    IdRight = x.IdRight,
                    IdUser = x.IdUser,
                    UserFirstName = x.User.FirstName,
                    UserLastName = x.User.LastName,
                    Email = x.User.Email,
                    RightName = x.Right.Name,
                    RightSymbol = x.Right.Symbol,
                    IdModule = x.Right.IdModule,
                    ModuleName = x.Right.Module.Name,
                    ModuleSymbol = x.Right.Module.Symbol
                }).ToList();

            return new OkObjectResult(_rightsDetailsViewModels);
        }

        [HttpGet("userModules/{idUser}")]
        public IActionResult GetUserModules(int idUser)
        {
            IEnumerable<ModuleDetailsViewModel> _moduleDetailsViewModel = _rightsToUserRepository.GetContext()
                  .Select(x => new ModuleDetailsViewModel
                  {
                      Id = x.Right.IdModule,
                      Name = x.Right.Module.Name,
                      Symbol = x.Right.Module.Symbol,
                      Icon = x.Right.Module.Icon,
                      Order = x.Right.Module.Order,
                      IdUser = x.IdUser,
                      UserFirstName = x.User.FirstName,
                      UserLastName = x.User.LastName,
                      UserEmail = x.User.Email
                  })
                  .Where(x => x.IdUser == idUser)
                  .GroupBy(x => x.Id)
                  .Select(grp => grp.First())
                  .ToList();

            return new OkObjectResult(_moduleDetailsViewModel);
        }

        [HttpGet("userRoleModules/{idUser}")]
        public IActionResult GetUserModulesByRole(int idUser)
        {
            User _user = _userRepository.GetSingle(u => u.Id == idUser, u => u.Role);

            if (_user != null)
            {
                int idRole = _user.Role.Id;

                IEnumerable<RightsDetailsViewModel> _rightsDetailsViewModels = _rightsToRoleRepository.GetContext()
                .Select(x => new RightsDetailsViewModel
                {
                    Id = x.Right.Id,
                    IdRole = x.IdRole,
                    IdModule = x.Right.IdModule,
                    ModuleName = x.Right.Module.Name,
                    ModuleSymbol = x.Right.Module.Symbol,
                    ModuleIcon = x.Right.Module.Icon,
                    ModuleOrder = x.Right.Module.Order
                    
                })
                .Where(x => x.IdRole == idRole)
                .GroupBy(x => x.IdModule)
                .Select(grp => grp.First())
                .ToList();

                return new OkObjectResult(_rightsDetailsViewModels);
            }
            return NotFound();
        }

        //[HttpGet("userRights/{idUser}")]
        //public IActionResult GetUserRights(int idUser)
        //{


        //}

        [HttpGet("userRightsByRole/{idUser}")]
        public IActionResult GetUserRightsByRole(int idUser)
        {
            User _user = _userRepository.GetSingle(u => u.Id == idUser, u => u.Role);

            if (_user != null)
            {
                int idRole = _user.Role.Id;

                IEnumerable<RightsDetailsViewModel> _rightsDetailsViewModels = _rightsToRoleRepository.GetContext()
                .Select(x => new RightsDetailsViewModel
                {
                    Id = x.Right.Id,
                    IdRight = x.Right.Id,
                    IdModule = x.Right.IdModule,
                    IdRole = x.Role.Id,
                    RoleName = x.Role.Name,
                    RoleSymbol = x.Role.Symbol,
                    RightName = x.Right.Name,
                    RightSymbol = x.Right.Symbol
                })
                .Where(x => x.IdRole == idRole)
                .ToList();

                return new OkObjectResult(_rightsDetailsViewModels);
            }
            return NotFound();
        }

        [HttpGet("roleRights/{idRole}")]
        public IActionResult GetRoleRights(int idRole)
        {
            IEnumerable<RightsDetailsViewModel> _rightsDetailsViewModels = _rightsToRoleRepository.GetContext()
                .Select(x => new RightsDetailsViewModel
                {
                    Id = x.Right.Id,
                    IdRight = x.Right.Id,
                    IdModule = x.Right.IdModule,
                    IdRole = x.Role.Id,
                    RoleName = x.Role.Name,
                    RoleSymbol = x.Role.Symbol,
                    RightName = x.Right.Name,
                    RightSymbol = x.Right.Symbol
                })
                .Where(x => x.IdRole == idRole)
                .ToList();

            return new OkObjectResult(_rightsDetailsViewModels);
        }

        [HttpGet("rightsToModule/{idModule}")]
        public IActionResult GetRightsToModule(int idModule)
        {
            IEnumerable<Rights> _rights = _rightsRepository
                .FindBy(r => r.IdModule == idModule)
                .ToList();

            if (_rights != null)
            {
                IEnumerable<RightsViewModel> _rightsViewModel = Mapper.Map<IEnumerable<Rights>, IEnumerable<RightsViewModel>>(_rights);

                return new OkObjectResult(_rightsViewModel);
            }
            else {
                return NotFound();
            }
        }
    }
}