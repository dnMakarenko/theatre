using Theatre.Data.Core.Models;
using Theatre.Data.Core.Repositories;
using Theatre.Data.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Theatre.Data.Db.Repositories
{
    public class SpectacleRepository : BaseRepository<Spectacle>, ISpectacleRepository
    {
        public SpectacleRepository(AppDbContext dbContext) : base(dbContext)
        { }

        public Spectacle GetByTitle(string title)
        {
            return base.DbSet.FirstOrDefault(q => q.Title == title || q.Title.Contains(title));
        }

        public Task<Spectacle> GetByTitleAsync(string title)
        {
            return base.DbSet.FirstOrDefaultAsync(q => q.Title == title || q.Title.Contains(title));
        }

        public IEnumerable<Spectacle> GetAllByTitle(string title)
        {
            return DbSet.Where(q => q.Title == title || q.Title.Contains(title)).ToList();
        }

        public async Task<IEnumerable<Spectacle>> GetAllByTitleAsync(string title)
        {
            return await DbSet.Where(q => q.Title == title || q.Title.Contains(title)).ToListAsync();
        }
    }
}
