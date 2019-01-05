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
    public class PhotosController : Controller
    {
        IPhotoRepository _photoRepository;

        public PhotosController(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }


        [HttpGet("{id}")]
        public IActionResult GetAnimalPhotos(int id)
        {
            IEnumerable<Photo> _photos = _photoRepository.FindBy(p => p.AnimalId == id).ToList();

            IEnumerable<PhotoViewModel> _photosViewModel = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(_photos);

            return new OkObjectResult(_photosViewModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody]PhotoViewModel photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Photo _newPhoto = Mapper.Map<PhotoViewModel, Photo>(photo);
            _photoRepository.Add(_newPhoto);
            _photoRepository.Commit();

            photo = Mapper.Map<Photo, PhotoViewModel>(_newPhoto);

            return new OkObjectResult(photo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Photo _photo = _photoRepository.GetSingle(id);

            if (_photo == null)
            {
                return NotFound();
            }
            else
            {
                _photoRepository.Delete(_photo);
                _photoRepository.Commit();

                return new OkObjectResult(new { state = "OK" });
            }
        }
    }
}
