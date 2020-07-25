using Happy.Weddings.Blog.Core.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Core.Repository
{
    public interface ICommentsRepository : IRepositoryBase<Comments>
    {
        /// <summary>
        /// Gets all comments.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Comments>> GetAllComments();

        /// <summary>
        /// Gets the comments by identifier.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<Comments> GetCommentsById(Guid commentId);

        /// <summary>
        /// Creates the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        void CreateComment(Comments comment);

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        void UpdateComment(Comments comment);

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        void DeleteComment(Comments comment);
    }
}
