using ABS_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABS_WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AgeController : ControllerBase
    {
        private readonly IAgeService _ageService;
        public AgeController(IAgeService ageService)
        {
            _ageService = ageService;
        }


        
        // /api/age-structure/{SA4 Code}/{Sex}
        [HttpGet("{SA4Code}/{Sex}")]
        public async Task<ActionResult> GetAllAges(int SA4Code, int Sex)
        {
            return Ok(_ageService.GetAllAges(SA4Code, Sex));
        }


        // /api/age-structure-diff/{SA4 Code}/{Sex}/{ Year1}/{ Year2}
        [HttpGet(("{SA4Code}/{Sex}/{Year1}/{Year2}"))]
        public async Task<ActionResult> GetAgeDiff(int SA4Code, int Sex, string Year1, string Year2)
        {
            var result = _ageService.GetAgeStructDiff(SA4Code, Sex, Year1, Year2);
            if (result == null)
                return NotFound("Sorry, but this does not exist.");
            return Ok(result);
        }

        // TO DO
        // /api/age-structure/1/1 (This will summarise all SA4s in NSW)



    }
}
