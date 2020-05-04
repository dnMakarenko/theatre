using Theatre.Data.Core.Models;
using Theatre.Data.Core.Repositories;

namespace Theatre.Data.Db.Repositories
{
    public class SessionRepository : BaseRepository<SpectacleSession>, ISessionRepository
    {
        public SessionRepository(AppDbContext dbContext) : base(dbContext)
        { }
    }
}
