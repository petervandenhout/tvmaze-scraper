namespace TvMazeScraper.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateOnly Birthday { get; set; }

        public virtual ICollection<TvShow> TvShows { get; set; }
    }
}