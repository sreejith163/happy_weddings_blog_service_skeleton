using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Core.Repository
{
    public interface IRepositoryWrapper
    {
        /// <summary>
        /// Gets the stroies.
        /// </summary>
        IStoriesRepository Stories { get; }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        ICommentsRepository Comments { get; }

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();
    }
}
