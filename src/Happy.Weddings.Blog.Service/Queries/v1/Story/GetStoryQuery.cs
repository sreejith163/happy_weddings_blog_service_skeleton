using Happy.Weddings.Blog.Core.DTO.Responses;
using MediatR;

namespace Happy.Weddings.Blog.Service.Queries.v1.Story
{
    /// <summary>
    /// Query for getting a story
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Collections.Generic.List{Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}}" />
    public class GetStoryQuery : IRequest<APIResponse>
    {
        /// <summary>
        /// Gets or sets the story identifier.
        /// </summary>
        public int StoryId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStoryQuery"/> class.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        public GetStoryQuery(int storyId)
        {
            StoryId = storyId;
        }
    }
}
