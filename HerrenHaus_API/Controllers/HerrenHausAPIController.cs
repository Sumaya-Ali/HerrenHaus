using HerrenHaus_API.Data;
using HerrenHaus_API.Models;
using HerrenHaus_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace HerrenHaus_API.Controllers
{
    [Route("api/HerrenHausAPI")]
   // [Route("HerrenHausAPI")]
   // [Route("api/[controller]")]
    [ApiController]
    public class HerrenHausAPIController:ControllerBase
    {
        [HttpGet]
        public IEnumerable<HerrenHausDto> GetAllHerrenHaus()
        {
            return HerrenHausStore.HerrenHausList;
        }
      //  [HttpGet("id")]
        [HttpGet("{id:int}")]
        public HerrenHausDto GetHerrenHausByID(int id)
        {
            return HerrenHausStore.HerrenHausList.FirstOrDefault(u=>u.ID==id);
        }
    }
}
