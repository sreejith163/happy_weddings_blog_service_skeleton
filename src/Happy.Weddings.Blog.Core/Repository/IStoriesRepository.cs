using Happy.Weddings.Blog.Core.Domain;
using Happy.Weddings.Blog.Core.DTO.Requests.Story;
using Happy.Weddings.Blog.Core.DTO.Responses.Story;
using Happy.Weddings.Blog.Core.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Core.Repository
{
    public interface IStoriesRepository : IRepositoryBase<Stories>
    {
        /// <summary>
        /// Gets all story.
        /// </summary>
        /// <param name="storyParameters">The story parameters.</param>
        /// <returns></returns>
        Task<PagedList<Domain.Entity>> GetAllStories(StoryParameters storyParameters);

        /// <summary>
        /// Gets the story by identifier.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        Task<Stories> GetStoryById(int storyId);

        /// <summary>
        /// Gets the stories by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<List<Stories>> GetStoriesByUserId(int userId);

        /// <summary>
        /// Gets the story with details.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        Task<StoryResponseDetails> GetStoryWithDetails(int storyId);

        /// <summary>
        /// Creates the story.
        /// </summary>
        /// <param name="story">The story.</param>
        void CreateStory(Stories story);

        /// <summary>
        /// Updates the story.
        /// </summary>
        /// <param name="story">The story.</param>
        void UpdateStory(Stories story);

        /// <summary>
        /// Updates the stories.
        /// </summary>
        /// <param name="story">The story.</param>
        void UpdateStories(List<Stories> story);

        /// <summary>
        /// Deletes the story.
        /// </summary>
        /// <param name="story">The story.</param>
        void DeleteStory(Stories story);
    }
}
