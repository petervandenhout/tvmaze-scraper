using System.Linq.Expressions;
using TvMazeScraper.Entities;

namespace TvMazeScraper.Repositories
{
    public interface ITvShowRepository
    {
        /// <summary>
        /// Filters tv shows based on one or more predicates and returns it ordered by id.
        /// </summary>
        /// <param name="predicates">A function to test each element for a condition.</param>
        /// <param name="skip">The number of elements to skip before returning the remaining elements.</param>
        /// <param name="take">The number of elements to return.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<IEnumerable<TvShow>> GetAsync(IEnumerable<Expression<Func<TvShow, bool>>> predicates, int skip, int take, CancellationToken cancellationToken);
    }
}
