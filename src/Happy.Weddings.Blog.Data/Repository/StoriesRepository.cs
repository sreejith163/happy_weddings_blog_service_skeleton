using AutoMapper;
using AutoMapper.QueryableExtensions;
using Happy.Weddings.Blog.Core.Domain;
using Happy.Weddings.Blog.Core.DTO.Requests.Story;
using Happy.Weddings.Blog.Core.DTO.Responses.Story;
using Happy.Weddings.Blog.Core.Entity;
using Happy.Weddings.Blog.Core.Helpers;
using Happy.Weddings.Blog.Core.Repository;
using Happy.Weddings.Blog.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Data.Repository
{
    public class StoriesRepository : RepositoryBase<Stories>, IStoriesRepository
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// The sort helper
        /// </summary>
        private ISortHelper<StoryResponse> sortHelper;

        /// <summary>
        /// The data shaper
        /// </summary>
        private IDataShaper<StoryResponse> dataShaper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoriesRepository" /> class.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="sortHelper">The sort helper.</param>
        /// <param name="dataShaper">The data shaper.</param>
        public StoriesRepository(
            BlogContext repositoryContext,
            IMapper mapper,
            ISortHelper<StoryResponse> sortHelper,
            IDataShaper<StoryResponse> dataShaper)
            : base(repositoryContext)
        {
            this.mapper = mapper;
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
            var getStoriesParams = new object[] {
                new MySqlParameter("@limit", storyParameters.PageSize),
                new MySqlParameter("@offset", (storyParameters.PageNumber - 1) * storyParameters.PageSize),
                new MySqlParameter("@searchKeyword", storyParameters.SearchKeyword),
                new MySqlParameter("@fromDate", storyParameters.FromDate),
                new MySqlParameter("@toDate", storyParameters.ToDate)
            };

            var stories = await FindAll("CALL GetAllStories(@limit, @offset, @searchKeyword, @fromDate, @toDate)", getStoriesParams).ToListAsync();
            var mappedStories = stories.AsQueryable().ProjectTo<StoryResponse>(mapper.ConfigurationProvider);
            var sortedStories = sortHelper.ApplySort(mappedStories, storyParameters.OrderBy);
            var shapedStories = dataShaper.ShapeData(sortedStories, storyParameters.Fields);

            return await PagedList<Entity>.ToPagedList(shapedStories, storyParameters.PageNumber, storyParameters.PageSize);
        }

        /// <summary>
        /// Gets the story by identifier.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        public async Task<Stories> GetStoryById(int storyId)
        {
            var getStoryParams = new object[] {
                new MySqlParameter("@id", storyId)
            };

            var stories = await FindAll("CALL GetStorybyId(@id)", getStoryParams).ToListAsync();
            return stories.FirstOrDefault();
        }

        /// <summary>
        /// Gets the stories by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<List<Stories>> GetStoriesByUserId(int userId)
        {
            var getStoryParams = new object[] {
                new MySqlParameter("@id", userId)
            };

            return await FindAll("CALL GetStorybyUserId(@id)", getStoryParams).ToListAsync();
        }

        /// <summary>
        /// Gets the story with details.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        public async Task<StoryResponseDetails> GetStoryWithDetails(int storyId)
        {
            return await FindByCondition(story => story.StoryId.Equals(storyId))
                .Include(ac => ac.Comments)
                .ProjectTo<StoryResponseDetails>(mapper.ConfigurationProvider)
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
        /// Updates the stories.
        /// </summary>
        /// <param name="stories">The stories.</param>
        public void UpdateStories(List<Stories> stories)
        {
            UpdateRange(stories);
        }

        /// <summary>
        /// Deletes the story.
        /// </summary>
        /// <param name="story">The story.</param>
        public void DeleteStory(Stories story)
        {
            Delete(story);
        }
    }
}
