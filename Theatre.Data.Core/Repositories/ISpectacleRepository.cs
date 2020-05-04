using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Theatre.Data.Core.Models;

namespace Theatre.Data.Core.Repositories
{
    public interface ISpectacleRepository : IRepository<Spectacle>
    {
        Task<Spectacle> GetByTitleAsync(string title);

        Spectacle GetByTitle(string title);

        Task<IEnumerable<Spectacle>> GetAllByTitleAsync(string title);

        IEnumerable<Spectacle> GetAllByTitle(string title);
    }
}
