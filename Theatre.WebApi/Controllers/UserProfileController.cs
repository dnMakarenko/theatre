using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theatre.Data.Core.Models;
using Theatre.Data.Core.Services;
using Theatre.Dtos.Entities;

namespace Theatre.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISpectacleService _spectacleService;
        private readonly IMapper _mapper;

        public UserProfileController(UserManager<ApplicationUser> userManager, ISpectacleService spectacleService, IMapper mapper)
        {
            _userManager = userManager;
            _spectacleService = spectacleService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            var reservations = await _spectacleService.GetUserReservationByUserIdAsync(UserId);

            var reservationsDto = new List<SpectacleSessionReservationDto>();
            _mapper.Map(reservations, reservationsDto);

            return Ok(new
            {
                user.Email,
                user.UserName,
                reservations = reservationsDto
            });
        }
    }
}