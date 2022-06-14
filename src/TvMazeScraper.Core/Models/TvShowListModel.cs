namespace TvMazeScraper.Models
{
    public class TvShowListModel
    {
        public IEnumerable<TvShowModel> TvShows { get; set; }

        public int Page { get; set; } = 1;

        public int ItemsPerPage { get; set; }

        public int Total { get; set; }
    }
}
