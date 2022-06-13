using TvMazeScraper.Repositories;

namespace TvMazeScraper
{
    public interface ITvMazeScraperUnitOfWork
    {
        ITvShowRepository TvShows { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
