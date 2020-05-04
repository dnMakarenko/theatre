using Theatre.Data.Core;
using Theatre.Data.Core.Repositories;
using Theatre.Data.Db.Repositories;
using System.Threading.Tasks;

namespace Theatre.Data.Db
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Dbcontext;

        private SpectacleRepository _spectacleRepository;

        private SessionRepository _sessionRepository;

        private ReservationRepository _reservationRepository;

        public UnitOfWork(AppDbContext Dbcontext)
        {
            this._Dbcontext = Dbcontext;
        }

        public ISpectacleRepository SpectacleRepository
        {
            get
            {
                if (_spectacleRepository == null)
                {
                    _spectacleRepository = new SpectacleRepository(_Dbcontext);
                }

                return _spectacleRepository;
            }
        }

        public ISessionRepository SessionRepository
        {
            get
            {
                if (_sessionRepository == null)
                {
                    _sessionRepository = new SessionRepository(_Dbcontext);
                }

                return _sessionRepository;
            }
        }

        public IReservationRepository ReservationRepository
        {
            get
            {
                if (_reservationRepository == null)
                {
                    _reservationRepository = new Repositories.ReservationRepository(_Dbcontext);
                }

                return _reservationRepository;
            }
        }
        public async Task<int> CommitAsync()
        {
            return await _Dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _Dbcontext.Dispose();
        }
    }
}
