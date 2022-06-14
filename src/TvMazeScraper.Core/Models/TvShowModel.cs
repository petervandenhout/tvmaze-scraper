using TvMazeScraper.Entities;

namespace TvMazeScraper.Models
{
    public class TvShowModel
    {
        public int Id { get; set; }

        public int TvMazeId { get; set; }

        public string Name { get; set; }

        public IEnumerable<ActorModel> Cast { get; set; }
    }
}
