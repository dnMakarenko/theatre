using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theatre.Data.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Reservations = new HashSet<SpectacleSessionReservation>();
        }

        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public virtual ICollection<SpectacleSessionReservation> Reservations { get; set; }
    }
}
