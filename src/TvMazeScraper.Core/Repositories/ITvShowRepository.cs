using System.Diagnostics.CodeAnalysis;
using TvMazeScraper.Entities;

namespace TvMazeScraper.Repositories
{
    public interface ITvShowRepository
    {
        /// <summary>
        /// Returns tv shows based on one or more predicates and returns it ordered by id.
        /// </summary>
        /// <param name="skip">The number of elements to skip before returning the remaining elements.</param>
        /// <param name="take">The number of elements to return.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<IEnumerable<TvShow>> GetAsync(int skip, int take, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the total number of tv shows.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<int> CountAsync(CancellationToken cancellationToken);

        TvShow Add([NotNull] TvShow tvShow);
    }
}
