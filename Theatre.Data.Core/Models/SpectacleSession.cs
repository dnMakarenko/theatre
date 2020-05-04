using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.Data.Core.Models
{
    public class SpectacleSession : BaseEntity
    {
        public SpectacleSession()
        {
            Reservations = new HashSet<SpectacleSessionReservation>();
        }

        public DateTime StartDateTime { get; set; }

        public int DurationInMinutes { get; set; }

        public int MaxNumberOfTickets { get; set; }

        public Guid SpectacleId { get; set; }
        public virtual Spectacle Spectacle { get; set; }

        public virtual ICollection<SpectacleSessionReservation> Reservations { get; set; }
    }
}
