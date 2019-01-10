using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalShelters.API.ViewModels;
using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelters.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RightsToUserController : Controller
    {
        IRightsToUserRepository _rightsToUserRepository;

        public RightsToUserController(IRightsToUserRepository rightsToUserRepository)
        {
            _rightsToUserRepository = rightsToUserRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<RightsToUser> _rightsToUser = _rightsToUserRepository
                .GetAll()
                .OrderBy(u => u.Id)
                .ToList();

            IEnumerable<RightsToUserViewModel> _rightsToUserViewModel = Mapper.Map<IEnumerable<RightsToUser>, IEnumerable<RightsToUserViewModel>>(_rightsToUser);

            return new OkObjectResult(_rightsToUserViewModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody]RightsToUserViewModel rightToUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RightsToUser _newRightToUser = Mapper.Map<RightsToUserViewModel, RightsToUser>(rightToUser);
            _rightsToUserRepository.Add(_newRightToUser);
            _rightsToUserRepository.Commit();

            rightToUser = Mapper.Map<RightsToUser, RightsToUserViewModel>(_newRightToUser);

            return new OkObjectResult(rightToUser);
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody]RightsToUserViewModel rightToUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RightsToUser _rightToUserDb = _rightsToUserRepository.GetSingle(id);

            if (_rightToUserDb == null)
            {
                return NotFound();
            }
            else
            {
                _rightToUserDb.IdRight = rightToUser.IdRight;
                _rightToUserDb.IdUser = rightToUser.IdUser;
            }

            _rightsToUserRepository.Commit();

            rightToUser = Mapper.Map<RightsToUser, RightsToUserViewModel>(_rightToUserDb);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            RightsToUser _rightToUser = _rightsToUserRepository.GetSingle(id);

            if (_rightToUser == null)
            {
                return NotFound();
            }
            else
            {
                _rightsToUserRepository.Delete(_rightToUser);
                _rightsToUserRepository.Commit();

                return new NoContentResult();
            }
        }
    }
}