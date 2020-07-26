using Happy.Weddings.Blog.Core.DTO.Requests.Story;
using Happy.Weddings.Blog.Core.DTO.Responses;
using MediatR;

namespace Happy.Weddings.Blog.Service.Commands.v1.Story
{
    /// <summary>
    /// Command for updating a story
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}" />
    public class UpdateStoryCommand : IRequest<APIResponse>
    {
        /// <summary>
        /// Gets or sets the story identifier.
        /// </summary>
        /// <value>
        /// The story identifier.
        /// </value>
        public int StoryId { get; set; }

        /// <summary>
        /// Gets or sets the reuqest.
        /// </summary>
        public UpdateStoryRequest Request { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStoryCommand" /> class.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <param name="Request">The request.</param>
        public UpdateStoryCommand(int storyId, UpdateStoryRequest request)
        {
            StoryId = storyId;
            Request = request;
        }
    }
}
