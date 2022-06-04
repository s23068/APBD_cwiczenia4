using APBD_cwiczenia4.Models;
using Microsoft.AspNetCore.Mvc;
using static APBD_cwiczenia4.Services.DbServices;

namespace APBD_cwiczenia4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private IDatabaseService _dbService;

        public AnimalsController(IDatabaseService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IActionResult GetAnimals(string ordered)
        {
            if (string.IsNullOrEmpty(ordered) || ordered == "name" || ordered == "description" || ordered == "category" || ordered == "area")
                return Ok(_dbService.GetAnimals(ordered));
            else return BadRequest();
        }
        [HttpPost]
        public IActionResult AddAnimals(Animals animal)
        {
            return Ok(_dbService.AddAnimals(animal));
        }
        [HttpPut]
        public IActionResult UpdateAnimals(Animals animal, int idAnimal)
        {
            if (_dbService.animalExists(idAnimal))
                return Ok(_dbService.UpdateAnimals(animal, idAnimal));
            else return NotFound();
        }
        [HttpDelete]
        public IActionResult DeleteAnimals(int idAnimal)
        {

            if (_dbService.animalExists(idAnimal))
            {
                _dbService.DeleteAnimals(idAnimal);
                return Ok();
            }
            else return NotFound();
        }
    }
}
