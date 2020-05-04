using Theatre.Data.Core.Models;
using Theatre.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Theatre.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ISpectacleRepository SpectacleRepository { get; }
        ISessionRepository SessionRepository { get; }
        IReservationRepository ReservationRepository { get; }

        Task<int> CommitAsync();
    }
}
