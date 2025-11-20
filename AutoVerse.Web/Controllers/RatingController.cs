using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutoVerse.Web.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService rating)
        {
            _ratingService = rating;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRating(Rating rating)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// Get the logged-in user's ID

            if (userId == null)
            {
                return Unauthorized();
            }
            rating.UserId = userId;

            await _ratingService.AddRatingAsync(rating);
            return RedirectToAction("Details", "Vehicle", new { id = rating.VehicleId});
        }
    }
}
