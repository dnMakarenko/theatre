using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theatre.Data.Core.Models;
using Theatre.Data.Core.Repositories;

namespace Theatre.Data.Db.Repositories
{
    public class ReservationRepository : BaseRepository<SpectacleSessionReservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<SpectacleSessionReservation>> GetUserReservationsAByUserIdAsync(Guid userId)
        {
            return await base.DbSet.Include(q => q.SpectacleSession).Where(q => q.ApplicationUserId == userId).ToListAsync();
        }
    }
}
