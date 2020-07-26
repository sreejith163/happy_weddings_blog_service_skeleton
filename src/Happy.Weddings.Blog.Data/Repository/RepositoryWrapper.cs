using AutoMapper;
using Happy.Weddings.Blog.Core.DTO.Responses;
using Happy.Weddings.Blog.Core.DTO.Responses.Story;
using Happy.Weddings.Blog.Core.Entity;
using Happy.Weddings.Blog.Core.Helpers;
using Happy.Weddings.Blog.Core.Repository;
using Happy.Weddings.Blog.Data.DatabaseContext;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Data.Repository
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        private BlogContext repositoryContext { get; set; }

        /// <summary>
        /// Gets the stories.
        /// </summary>
        private IStoriesRepository stories { get; set; }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        private ICommentsRepository comments { get; set; }

        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// The stories sort helper
        /// </summary>
        private ISortHelper<StoryResponse> storiesSortHelper;

        /// <summary>
        /// The comments sort helper
        /// </summary>
        private ISortHelper<Comments> commentsSortHelper;

        /// <summary>
        /// The stories data shaper
        /// </summary>
        private IDataShaper<StoryResponse> storiesDataShaper;

        /// <summary>
        /// The comments data shaper
        /// </summary>
        private IDataShaper<Comments> commentsDataShaper;

        /// <summary>
        /// Gets the stroies.
        /// </summary>
        public IStoriesRepository Stories
        {
            get
            {
                if (stories == null)
                {
                    stories = new StoriesRepository(repositoryContext, mapper, storiesSortHelper, storiesDataShaper);
                }
                return stories;
            }
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        public ICommentsRepository Comments
        {
            get
            {
                if (comments == null)
                {
                    comments = new CommentsRepository(repositoryContext, commentsSortHelper, commentsDataShaper);
                }
                return comments;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryWrapper" /> class.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="storiesSortHelper">The stories sort helper.</param>
        /// <param name="commentsSortHelper">The comments sort helper.</param>
        /// <param name="storiesDataShaper">The stories data shaper.</param>
        /// <param name="commentsDataShaper">The comments data shaper.</param>
        public RepositoryWrapper(
            BlogContext repositoryContext,
            IMapper mapper,
            ISortHelper<StoryResponse> storiesSortHelper, 
            ISortHelper<Comments> commentsSortHelper,
            IDataShaper<StoryResponse> storiesDataShaper,
            IDataShaper<Comments> commentsDataShaper)
        {
            this.repositoryContext = repositoryContext;
            this.mapper = mapper;
            this.storiesSortHelper = storiesSortHelper;
            this.commentsSortHelper = commentsSortHelper;
            this.storiesDataShaper = storiesDataShaper;
            this.commentsDataShaper = commentsDataShaper;
        }

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAsync()
        {
            return await repositoryContext.SaveChangesAsync() >= 0;
        }
    }
}
