using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Data.Core.Models;
using Theatre.Data.Core.Services;
using Theatre.Dtos.Entities;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Theatre.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpectaclesController : ControllerBase
    {
        private readonly ISpectacleService _spectacleService;
        private readonly IMapper _mapper;

        public SpectaclesController(ISpectacleService spectacleService, IMapper mapper)
        {
            _spectacleService = spectacleService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var spectacles = await _spectacleService.GetAllAsync();

            var spectaclesDto = _mapper.Map<IEnumerable<Spectacle>, IEnumerable<SpectacleDto>>(spectacles);

            return Ok(spectaclesDto);
        }

        [HttpGet("GetFiltered/{skip}/{take}")]
        public async Task<IActionResult> GetFiltered(int skip, int take, string q = "")
        {
            var spectacles = await _spectacleService.GetFilteredAsync(skip, take, q).ConfigureAwait(false);
            var count = await _spectacleService.GetFilteredCountAsync(q).ConfigureAwait(false);

            var spectaclesDto = _mapper.Map<IEnumerable<Spectacle>, IEnumerable<SpectacleDto>>(spectacles);

            return Ok(new
            {
                body = spectaclesDto,
                totalRecords = count
            });
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var spectacle = await _spectacleService.GetByIdAsync(id);
            if (spectacle == null)
            {
                return NotFound();
            }

            var spectacleDto = _mapper.Map<Spectacle, SpectacleDto>(spectacle);

            return Ok(spectacleDto);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Add(SpectacleDto dto)
        {
            var spectacle = _mapper.Map<SpectacleDto, Spectacle>(dto);

            var createdSpectacle = await _spectacleService.CreateAsync(spectacle);

            var createdDto = _mapper.Map<Spectacle, SpectacleDto>(createdSpectacle);

            return Ok(createdDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spectacle = await _spectacleService.GetByIdAsync(id);
            if (spectacle == null)
            {
                return NotFound();
            }

            await _spectacleService.DeleteAsync(spectacle);

            return Ok();
        }

        [HttpPost("{id}/AddSession")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddSession(Guid id, SpectacleSessionDto dto)
        {
            var spectacle = await _spectacleService.GetByIdAsync(id);
            if (spectacle == null)
            {
                return NotFound();
            }

            var session = _mapper.Map<SpectacleSessionDto, SpectacleSession>(dto);

            spectacle.Sessions.Add(session);
            spectacle = await _spectacleService.UpdateAsync(spectacle);

            var spectacleDto = _mapper.Map<Spectacle, SpectacleDto>(spectacle);

            return Ok(spectacleDto);
        }

        [HttpPost("{sessionId}/Reserve")]
        [Authorize]
        public async Task<IActionResult> Reserve(Guid sessionId)
        {
            var userId = User.Claims.First(c => c.Type == "UserId").Value;

            var session = await _spectacleService.GetSessionAsync(sessionId);
            if (session == null)
            {
                return NotFound();
            }

            var reservation = new SpectacleSessionReservation()
            {
                SpectacleSessionId = session.Id,
                ApplicationUserId = Guid.Parse(userId),
                ReservationDateTime = DateTime.Now
            };

            reservation = await _spectacleService.AddReservationAsync(reservation);

            var reservationDto = _mapper.Map<SpectacleSessionReservation, SpectacleSessionReservationDto>(reservation);

            return Ok(reservationDto);
        }

        ///TODO: other api methods...
    }
}