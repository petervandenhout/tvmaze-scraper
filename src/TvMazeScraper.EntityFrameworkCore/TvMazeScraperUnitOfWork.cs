using TvMazeScraper.EntityFrameworkCore.Repositories;
using TvMazeScraper.Repositories;

namespace TvMazeScraper.EntityFrameworkCore
{
    public class TvMazeScraperUnitOfWork : ITvMazeScraperUnitOfWork
    {
        private readonly TvMazeScraperDbContext _dbContext;

        public ITvShowRepository TvShows { get; }

        public TvMazeScraperUnitOfWork(TvMazeScraperDbContext dbContext)
        {
            _dbContext = dbContext;

            TvShows = new TvShowRepository(_dbContext);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(true, cancellationToken);
        }
    }
}
