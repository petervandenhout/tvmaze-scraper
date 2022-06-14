using Microsoft.EntityFrameworkCore;
using TvMazeScraper.Entities;
using TvMazeScraper.Repositories;

namespace TvMazeScraper.EntityFrameworkCore.Repositories
{
    public class TvShowRepository : ITvShowRepository
    {
        private readonly TvMazeScraperDbContext _dbContext;

        public TvShowRepository(TvMazeScraperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.TvShows.CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<TvShow>> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            var query = _dbContext.TvShows.Where(_ => true);

            query = query.Include(p => p.Cast.OrderByDescending(c => c.Birthday))
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
