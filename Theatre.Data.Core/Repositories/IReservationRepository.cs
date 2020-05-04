using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Data.Core.Models;

namespace Theatre.Data.Core.Repositories
{
    public interface IReservationRepository : IRepository<SpectacleSessionReservation>
    {
        Task<IEnumerable<SpectacleSessionReservation>> GetUserReservationsAByUserIdAsync(Guid userId);
    }
}
