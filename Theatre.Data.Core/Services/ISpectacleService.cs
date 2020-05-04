using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Data.Core.Models;

namespace Theatre.Data.Core.Services
{
    public interface ISpectacleService : IService<Spectacle>
    {
        Task<SpectacleSession> AddSessionAsync(SpectacleSession session);
        Spectacle GetByTitle(string title);

        Task<Spectacle> GetByTitleAsync(string title);

        IEnumerable<Spectacle> GetAllByTitle(string title);

        Task<IEnumerable<Spectacle>> GetAllByTitleAsync(string title);

        Task<SpectacleSession> GetSessionAsync(Guid id);

        Task<SpectacleSessionReservation> AddReservationAsync(SpectacleSessionReservation reservation);

        Task<IEnumerable<SpectacleSessionReservation>> GetUserReservationByUserIdAsync(Guid userId);

        Task<IEnumerable<Spectacle>> GetFilteredAsync(int skip, int take, string q);

        Task<int> GetFilteredCountAsync(string q);
    }
}
