using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<TvShow>> GetAsync(IEnumerable<Expression<Func<TvShow, bool>>> predicates, int skip, int take, CancellationToken cancellationToken)
        {
            var query = _dbContext.TvShows.Where(_ => true);

            foreach (var predicate in predicates)
                query = query.Where(predicate);

            query = query.Include(p => p.Cast.OrderByDescending(c => c.Birthday))
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
