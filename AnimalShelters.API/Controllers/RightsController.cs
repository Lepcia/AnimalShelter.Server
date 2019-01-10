using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalShelters.API.Core;
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
    public class RightsController : Controller
    {
        IRightsRepository _rightsRepository;
        IRightsToUserRepository _rightsToUserRepository;

        int page = 1;
        int pageSize = 10;

        public RightsController(IRightsRepository rightsRepository, IRightsToUserRepository rightsToUserRepository)
        {
            _rightsRepository = rightsRepository;
            _rightsToUserRepository = rightsToUserRepository;
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
            var totalRights = _rightsRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalRights / pageSize);

            IEnumerable<Rights> _rights = _rightsRepository
                .GetAll()
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(currentPage)
                .ToList();

            IEnumerable<RightsViewModel> _rightsViewModel = Mapper.Map<IEnumerable<Rights>, IEnumerable<RightsViewModel>>(_rights);

            Response.AddPagination(page, pageSize, totalRights, totalPages);

            return new OkObjectResult(_rightsViewModel);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IEnumerable<Rights> _rights = _rightsRepository
                .GetAll()
                .OrderBy(u => u.Id)
                .ToList();

            IEnumerable<RightsViewModel> _rightsViewModel = Mapper.Map<IEnumerable<Rights>, IEnumerable<RightsViewModel>>(_rights);

            return new OkObjectResult(_rightsViewModel);
        }


        [HttpGet("{id}", Name = "GetRight")]
        public IActionResult Get(int id)
        {
            Rights _right = _rightsRepository.GetSingle(a => a.Id == id);

            if (_right != null)
            {
                RightsViewModel _rightsViewModel = Mapper.Map<Rights, RightsViewModel>(_right);

                return new OkObjectResult(_rightsViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{idUser}", Name = "GetUserRights")]
        public IActionResult GetUserRights(int idUser)
        {
            IEnumerable<RightsToUser> _rightsToUser = _rightsToUserRepository.FindBy(r => r.IdUser == idUser);

            if (_rightsToUser != null)
            {
                IEnumerable<RightsToUserViewModel> _rightsToUserViewModel = Mapper.Map<IEnumerable<RightsToUser>, IEnumerable<RightsToUserViewModel>>(_rightsToUser);

                return new OkObjectResult(_rightsToUserViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{idRight}/{idUser}", Name = "HasRight")]
        public IActionResult CheckIfUserHasRight(int idRight, int idUser)
        {
            RightsToUser _rightToUser = _rightsToUserRepository.GetSingle(r => r.IdRight == idRight && r.IdUser == idUser);

            if (_rightToUser != null)
            {
                RightsToUserViewModel _rightsToUserViewModel = Mapper.Map<RightsToUser, RightsToUserViewModel>(_rightToUser);

                return new OkObjectResult(_rightsToUserViewModel);
            }
            else
            {
                return new OkObjectResult("false");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]RightsViewModel right)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rights _newRight = Mapper.Map<RightsViewModel, Rights>(right);
            _rightsRepository.Add(_newRight);
            _rightsRepository.Commit();

            right = Mapper.Map<Rights, RightsViewModel>(_newRight);

            return new OkObjectResult(right);
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody]RightsViewModel right)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rights _rightDb = _rightsRepository.GetSingle(id);

            if (_rightDb == null)
            {
                return NotFound();
            }
            else
            {
                _rightDb.Name = right.Name;
                _rightDb.Symbol = right.Symbol;
            }

            _rightsRepository.Commit();

            right = Mapper.Map<Rights, RightsViewModel>(_rightDb);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Rights _right = _rightsRepository.GetSingle(id);

            if (_right == null)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<RightsToUser> _rightsToUser = _rightsToUserRepository.FindBy(r => r.IdRight == id);

                foreach(var rightToUser in _rightsToUser)
                {
                    _rightsToUserRepository.Delete(rightToUser);
                }

                _rightsRepository.Delete(_right);
                _rightsRepository.Commit();
                _rightsToUserRepository.Commit();

                return new NoContentResult();
            }
        }        
    }
}