using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.Data.Core.Models
{
    public class SpectacleSessionReservation : BaseEntity
    {
        public DateTime ReservationDateTime { get; set; }
        public Guid SpectacleSessionId { get; set; }
        public virtual SpectacleSession SpectacleSession { get; set; }
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
