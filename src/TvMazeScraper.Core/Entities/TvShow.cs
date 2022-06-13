namespace TvMazeScraper.Entities
{
    public class TvShow
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Actor> Cast { get; set; }
    }
}