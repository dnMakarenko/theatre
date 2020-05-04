using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.Data.Core.Models
{
    public class Spectacle : BaseEntity
    {
        public Spectacle()
        {
            Sessions = new HashSet<SpectacleSession>();
        }
        public string Title { get; set; }

        public virtual ICollection<SpectacleSession> Sessions { get; set; }
    }
}
