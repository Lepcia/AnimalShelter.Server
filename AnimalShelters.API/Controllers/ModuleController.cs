using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalShelters.API.Core;
using AnimalShelters.API.ViewModels;
using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelters.API.Controllers
{
    [Route("api/[controller]")]
    public class ModuleController : Controller
    {
        IModuleRepository _moduleRepository;

        int page = 1;
        int pageSize = 10;

        public ModuleController(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
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
            var totalModules = _moduleRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalModules / pageSize);

            IEnumerable<Module> _modules = _moduleRepository
                .GetAll()
                .OrderBy(m => m.Order)
                .Skip((currentPage - 1) * pageSize)
                .Take(currentPage)
                .ToList();

            IEnumerable<ModuleViewModel> _moduleViewModel = Mapper.Map<IEnumerable<Module>, IEnumerable<ModuleViewModel>>(_modules);

            Response.AddPagination(page, pageSize, totalModules, totalPages);

            return new OkObjectResult(_moduleRepository);
        }

        [HttpGet("{id}", Name = "GetModule")]
        public IActionResult Get(int id)
        {
            Module _module = _moduleRepository.GetSingle(m => m.Id == id);

            if (_module != null)
            {
                ModuleViewModel _moduleViewModel = Mapper.Map<Module, ModuleViewModel>(_module);

                return new OkObjectResult(_moduleViewModel);
            }
            else
            {
                return NotFound();
            }
        }
    }
}