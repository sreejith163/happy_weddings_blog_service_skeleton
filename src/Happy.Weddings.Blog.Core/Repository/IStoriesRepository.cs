﻿using Happy.Weddings.Blog.Core.Domain;
using Happy.Weddings.Blog.Core.DTO.Requests;
using Happy.Weddings.Blog.Core.Entity;
using System;
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
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        Task<Domain.Entity> GetStoryById(int storyId, string fields);

        /// <summary>
        /// Gets the story by identifier.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        Task<Stories> GetStoryById(int storyId);

        /// <summary>
        /// Gets the story with details.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        Task<Stories> GetStoryWithDetails(int storyId);

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
        /// Deletes the story.
        /// </summary>
        /// <param name="story">The story.</param>
        void DeleteStory(Stories story);
    }
}