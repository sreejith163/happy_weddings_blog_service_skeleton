using Happy.Weddings.Blog.Core.DTO.Requests;
using Happy.Weddings.Blog.Core.DTO.Responses;
using MediatR;

namespace Happy.Weddings.Blog.Service.Queries.v1
{
    /// <summary>
    /// Query for getting stories
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Collections.Generic.List{Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}}" />
    public class GetAllStoriesQuery : IRequest<APIResponse>
    {
        /// <summary>
        /// Gets or sets the story parameters.
        /// </summary>
        public StoryParameters StoryParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllStoriesQuery"/> class.
        /// </summary>
        /// <param name="storyParameters">The story parameters.</param>
        public GetAllStoriesQuery(StoryParameters storyParameters)
        {
            StoryParameters = storyParameters;
        }
    }
}
