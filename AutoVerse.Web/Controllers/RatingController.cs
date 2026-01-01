using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Services;
using AutoVerse.Core.ViewModels;
using AutoVerse.Web.Mappings;
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
        public async Task<IActionResult> AddRating(RatingViewModel ratingVm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// Get the logged-in user's ID

            if (userId == null)
            {
                return Unauthorized();
            }
            ratingVm.UserId = userId;
            var rating = RatingMappings.ToEntity(ratingVm);
            await _ratingService.AddRatingAsync(rating);
            return RedirectToAction("Details", "Vehicle", new { id = rating.VehicleId});
        }
    }
}
