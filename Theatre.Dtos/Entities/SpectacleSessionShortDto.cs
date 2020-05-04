using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.Dtos.Entities
{
    public class SpectacleSessionShortDto : BaseEntityDto
    {
        public SpectacleSessionShortDto()
        {
            Spectacle = new SpectacleShortDto();
        }
        public SpectacleShortDto Spectacle { get; set; }
        public DateTime StartDateTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int MaxNumberOfTickets { get; set; }
    }
}
