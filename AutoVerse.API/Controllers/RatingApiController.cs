using AutoVerse.Core.DTOs;
using AutoVerse.Core.Interfaces.Services;
using AutoVerse.API.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutoVerse.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingApiController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingApiController(IRatingService ratingservice)
        {
            _ratingService = ratingservice;
        }

        [HttpPost("AddRating")]
        public async Task<IActionResult> AddRating([FromBody]RatingDto ratingDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }
            ratingDto.UserId = userId;
            var rating = RatingMappings.ToEntity(ratingDto);
            await _ratingService.AddRatingAsync(rating);
            return NoContent();

        }

    }
}
