using Happy.Weddings.Blog.Core.Entity;
using Happy.Weddings.Blog.Core.Helpers;
using Happy.Weddings.Blog.Core.Repository;
using Happy.Weddings.Blog.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Data.Repository
{
    public class CommentsRepository : RepositoryBase<Comments>, ICommentsRepository
    {
        /// <summary>
        /// The sort helper
        /// </summary>
        private ISortHelper<Comments> sortHelper;

        /// <summary>
        /// The data shaper
        /// </summary>
        private IDataShaper<Comments> dataShaper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsRepository" /> class.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        /// <param name="sortHelper">The sort helper.</param>
        /// <param name="dataShaper">The data shaper.</param>
        public CommentsRepository(
            BlogContext repositoryContext, 
            ISortHelper<Comments> sortHelper,
            IDataShaper<Comments> dataShaper)
            : base(repositoryContext)
        {
            this.sortHelper = sortHelper;
            this.dataShaper = dataShaper;
        }

        /// <summary>
        /// Gets all comments.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Comments>> GetAllComments()
        {
            return await FindAll()
               .OrderBy(com => com.Comment)
               .ToListAsync();
        }

        /// <summary>
        /// Gets the comment by identifier.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public async Task<Comments> GetCommentsById(Guid commentId)
        {
            return await FindByCondition(comment => comment.CommentId.Equals(commentId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Creates the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void CreateComment(Comments comment)
        {
            Create(comment);
        }

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void UpdateComment(Comments comment)
        {
            Update(comment);
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void DeleteComment(Comments comment)
        {
            Delete(comment);
        }
    }
}
