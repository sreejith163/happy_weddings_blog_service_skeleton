using Happy.Weddings.Blog.Core.DTO.Responses;
using MediatR;
using System;

namespace Happy.Weddings.Blog.Service.Commands.v1.Story
{
    /// <summary>
    /// Command for deleting a story
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}" />
    public class DeleteStoryCommand : IRequest<APIResponse>
    {
        /// <summary>
        /// Gets or sets the story identifier.
        /// </summary>
        public int StoryId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStoryCommand"/> class.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        public DeleteStoryCommand(int storyId)
        {
            StoryId = storyId;
        }
    }
}
