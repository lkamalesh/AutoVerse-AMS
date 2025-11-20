using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> AddRating([FromBody]Rating rating)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }
            rating.UserId = userId;
            await _ratingService.AddRatingAsync(rating);
            return Ok(new { Message = "Rating added successfully" });

        }

    }
}
