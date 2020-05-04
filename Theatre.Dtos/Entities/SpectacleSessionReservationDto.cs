using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.Dtos.Entities
{
    public class SpectacleSessionReservationDto : BaseEntityDto
    {
        public SpectacleSessionReservationDto()
        {
            ApplicationUser = new ApplicationUserDto();
        }
        public DateTime ReservationDateTime { get; set; }

        public Guid SpectacleSessionId { get; set; }
        public virtual SpectacleSessionShortDto SpectacleSession { get; set; }

        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUserDto ApplicationUser { get; set; }
    }
}
