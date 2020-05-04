using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theatre.Data.Core;
using Theatre.Data.Core.Models;
using Theatre.Data.Core.Services;

namespace Theatre.Services
{
    public class SpectacleService : ISpectacleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpectacleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SpectacleSessionReservation> AddReservationAsync(SpectacleSessionReservation reservation)
        {
            var session = await _unitOfWork.SessionRepository.DbSet.Include(q => q.Reservations).FirstOrDefaultAsync(q => q.Id == reservation.SpectacleSessionId);

            session.Reservations.Add(reservation);

            await _unitOfWork.SessionRepository.UpdateAsync(session);

            return reservation;
        }

        public async Task<SpectacleSession> GetSessionAsync(Guid id)
        {
            return await _unitOfWork.SessionRepository.DbSet.Include(q => q.Reservations).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<SpectacleSession> AddSessionAsync(SpectacleSession entity)
        {
            return await _unitOfWork.SessionRepository.CreateAsync(entity);
        }

        public Spectacle Create(Spectacle entity)
        {
            return _unitOfWork.SpectacleRepository.Create(entity);
        }

        public async Task<Spectacle> CreateAsync(Spectacle entity)
        {
            return await _unitOfWork.SpectacleRepository.CreateAsync(entity);
        }

        public void Delete(Spectacle entity)
        {
            _unitOfWork.SpectacleRepository.Delete(entity);
        }

        public async Task DeleteAsync(Spectacle entity)
        {
            await _unitOfWork.SpectacleRepository.DeleteAsync(entity);
        }

        public IEnumerable<Spectacle> GetAll()
        {
            return _unitOfWork.SpectacleRepository.GetAll();
        }

        public async Task<IEnumerable<Spectacle>> GetAllAsync()
        {
            return await _unitOfWork.SpectacleRepository.DbSet.Include(q => q.Sessions).ToListAsync();
        }

        public async Task<IEnumerable<Spectacle>> GetFilteredAsync(int skip, int take, string q)
        {
            if (string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q))
            {
                return await _unitOfWork.SpectacleRepository.DbSet.Include(a => a.Sessions).Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                return await _unitOfWork.SpectacleRepository.DbSet.Include(a => a.Sessions).Where(x => x.Title.ToLower().Contains(q.ToLower())).Skip(skip).Take(take).ToListAsync();
            }
        }

        public async Task<int> GetFilteredCountAsync(string q)
        {
            if (string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q))
            {
                return await _unitOfWork.SpectacleRepository.DbSet.Include(a => a.Sessions).CountAsync();
            }
            else
            {
                return await _unitOfWork.SpectacleRepository.DbSet.Include(a => a.Sessions).Where(x => x.Title.ToLower().Contains(q.ToLower())).CountAsync();
            }
        }

        public IEnumerable<Spectacle> GetAllByTitle(string title)
        {
            return _unitOfWork.SpectacleRepository.GetAllByTitle(title);
        }

        public async Task<IEnumerable<Spectacle>> GetAllByTitleAsync(string title)
        {
            return await _unitOfWork.SpectacleRepository.GetAllByTitleAsync(title);
        }

        public Spectacle GetById(Guid Id)
        {
            return _unitOfWork.SpectacleRepository.DbSet.Include(q => q.Sessions).FirstOrDefault(q => q.Id == Id);
        }

        public async Task<Spectacle> GetByIdAsync(Guid Id)
        {
            return await _unitOfWork.SpectacleRepository.DbSet.Include(q => q.Sessions).ThenInclude(x => x.Reservations).FirstOrDefaultAsync(q => q.Id == Id);
        }

        public Spectacle GetByTitle(string title)
        {
            return _unitOfWork.SpectacleRepository.GetByTitle(title);
        }

        public Task<Spectacle> GetByTitleAsync(string title)
        {
            return _unitOfWork.SpectacleRepository.GetByTitleAsync(title);
        }

        public void Save()
        {
            _unitOfWork.SpectacleRepository.Save();
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SpectacleRepository.SaveAsync();
        }

        public Spectacle Update(Spectacle entity)
        {
            return _unitOfWork.SpectacleRepository.Update(entity);
        }

        public async Task<Spectacle> UpdateAsync(Spectacle entity)
        {
            return await _unitOfWork.SpectacleRepository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<SpectacleSessionReservation>> GetUserReservationByUserIdAsync(Guid userId)
        {
            return await _unitOfWork.ReservationRepository.DbSet.Include(q => q.SpectacleSession).ThenInclude(x => x.Spectacle).Where(q => q.ApplicationUserId == userId).ToListAsync();
        }
    }
}
