using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Reservation reservation)
        {
            try
            {
                _reservationService.AddReservation(reservation);
                return Ok("Reserva agregada exitosamente.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{date}")]
        public IActionResult Get(string date)
        {
            if (!DateOnly.TryParse(date, out var parsedDate))
                return BadRequest(new { message = "Formato de fecha inválido." });

            var result = _reservationService.GetReservationsByDate(parsedDate);
            return Ok(result);
        }
    }
}