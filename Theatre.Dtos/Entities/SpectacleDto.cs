using System.Collections.Generic;

namespace Theatre.Dtos.Entities
{
    public class SpectacleDto : BaseEntityDto
    {
        public SpectacleDto()
        {
            Sessions = new List<SpectacleSessionDto>();
        }
        public string Title { get; set; }
        public IEnumerable<SpectacleSessionDto> Sessions { get; set; }
    }
}
