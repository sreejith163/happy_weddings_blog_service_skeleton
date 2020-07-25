using Happy.Weddings.Blog.Core.Domain;
using Happy.Weddings.Blog.Core.DTO.Requests;
using Happy.Weddings.Blog.Core.Entity;
using Happy.Weddings.Blog.Core.Helpers;
using Happy.Weddings.Blog.Core.Repository;
using Happy.Weddings.Blog.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Data.Repository
{
    public class StoriesRepository : RepositoryBase<Stories>, IStoriesRepository
    {
        /// <summary>
        /// The sort helper
        /// </summary>
        private ISortHelper<Stories> sortHelper;

        /// <summary>
        /// The data shaper
        /// </summary>
        private IDataShaper<Stories> dataShaper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoriesRepository" /> class.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        /// <param name="sortHelper">The sort helper.</param>
        /// <param name="sortHelper">The sort helper.</param>
        public StoriesRepository(
            BlogContext repositoryContext, 
            ISortHelper<Stories> sortHelper, 
            IDataShaper<Stories> dataShaper)
            : base(repositoryContext)
        {
            this.sortHelper = sortHelper;
            this.dataShaper = dataShaper;
        }

        /// <summary>
        /// Gets all storiess.
        /// </summary>
        /// <param name="storyParameters">The story parameters.</param>
        /// <returns></returns>
        public async Task<PagedList<Entity>> GetAllStories(StoryParameters storyParameters)
        {
            var stories = FindAll();
            SearchByTitle(ref stories, storyParameters.Name);
            FilterByDate(ref stories, storyParameters.FromDate, storyParameters.ToDate);
            var sortedStories = sortHelper.ApplySort(stories, storyParameters.OrderBy);
            var shapedStories = dataShaper.ShapeData(sortedStories, storyParameters.Fields);

            return await PagedList<Entity>.ToPagedList(shapedStories, storyParameters.PageNumber, storyParameters.PageSize);
        }

        /// <summary>
        /// Gets the story by identifier.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public async Task<Entity> GetStoryById(int storyId, string fields)
        {
            var story = await FindByCondition(story => story.StoryId.Equals(storyId))
                .DefaultIfEmpty(new Stories())
                .FirstOrDefaultAsync();

            return dataShaper.ShapeData(story, fields);
        }

        /// <summary>
        /// Gets the story by identifier.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        public async Task<Stories> GetStoryById(int storyId)
        {
            return await FindByCondition(story => story.StoryId.Equals(storyId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the story with details.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        public async Task<Stories> GetStoryWithDetails(int storyId)
        {
            return await FindByCondition(story => story.StoryId.Equals(storyId))
                .Include(ac => ac.Comments)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Creates the story.
        /// </summary>
        /// <param name="story">The story.</param>
        public void CreateStory(Stories story)
        {
            Create(story);
        }

        /// <summary>
        /// Updates the story.
        /// </summary>
        /// <param name="story">The story.</param>
        public void UpdateStory(Stories story)
        {
            Update(story);
        }

        /// <summary>
        /// Deletes the story.
        /// </summary>
        /// <param name="story">The story.</param>
        public void DeleteStory(Stories story)
        {
            Delete(story);
        }

        /// <summary>
        /// Searches the by title.
        /// </summary>
        /// <param name="stories">The stories.</param>
        /// <param name="title">The title.</param>
        private void SearchByTitle(ref IQueryable<Stories> stories, string title)
        {
            if (!stories.Any() || string.IsNullOrWhiteSpace(title))
                return;

            stories = stories.Where(o => string.Equals(o.Title, title, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Filters the by date.
        /// </summary>
        /// <param name="stories">The stories.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        private void FilterByDate(ref IQueryable<Stories> stories, DateTime? fromDate, DateTime? toDate)
        {
            if (!stories.Any())
                return;

            if (fromDate != null)
            {
                stories = stories.Where(s => s.CreatedDate >= fromDate);
            }
            if (toDate != null)
            {
                stories = stories.Where(s => s.CreatedDate <= toDate);
            }
        }
    }
}
