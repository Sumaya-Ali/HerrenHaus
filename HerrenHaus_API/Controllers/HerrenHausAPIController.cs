using HerrenHaus_API.Data;
using HerrenHaus_API.Models;
using HerrenHaus_API.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace HerrenHaus_API.Controllers
{
    [Route("api/HerrenHausAPI")]
    // [Route("HerrenHausAPI")]
    // [Route("api/[controller]")]
    [ApiController]
    public class HerrenHausAPIController : ControllerBase
    {
        [HttpGet()]
        //[ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HerrenHausDto>> GetAllHerrenHaus()
        {
            return Ok(HerrenHausStore.HerrenHausList);
        }
        //  [HttpGet("id")]
        [HttpGet("{id:int}", Name = "GetHerrenHausByID")]
        //[ProducesResponseType(200,Type=typeof(HerrenHausDto))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HerrenHausDto> GetHerrenHausByID(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var Haus = HerrenHausStore.HerrenHausList.FirstOrDefault(u => u.ID == id);
            if (Haus == null)
            {
                return NotFound();
            }
            return Ok(Haus);
        }
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HerrenHausDto> CreateHerrenHaus([FromBody] HerrenHausDto herrenHausDto)
        {
            if (herrenHausDto == null)
            {
                return BadRequest(herrenHausDto);
            }

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (herrenHausDto.ID > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (HerrenHausStore.HerrenHausList.FirstOrDefault(u => u.Name.ToLower() == herrenHausDto.Name.ToLower()) != null) {
                ModelState.AddModelError("CustomeError_Name", "Name is Duplicate!");
                return BadRequest(ModelState);
            }
            herrenHausDto.ID = HerrenHausStore.HerrenHausList.OrderByDescending(u => u.ID).FirstOrDefault().ID + 1;
            HerrenHausStore.HerrenHausList.Add(herrenHausDto);
            //to provide a link for the created object - return 201
            return CreatedAtRoute("GetHerrenHausByID", new { id = herrenHausDto.ID }, herrenHausDto);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteHerrenHaus(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var Haus = HerrenHausStore.HerrenHausList.FirstOrDefault(u => u.ID == id);
            if (Haus == null)
            {
                return NotFound();
            }
            HerrenHausStore.HerrenHausList.Remove(Haus);
            return NoContent();
        }
    }
}
