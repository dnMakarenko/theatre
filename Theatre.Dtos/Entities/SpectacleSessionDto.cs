using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.Dtos.Entities
{
    public class SpectacleSessionDto : SpectacleSessionShortDto
    {
        public SpectacleSessionDto()
        {
            Reservations = new HashSet<SpectacleSessionReservationDto>();
        }
        public IEnumerable<SpectacleSessionReservationDto> Reservations { get; set; }
    }
}
