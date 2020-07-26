using Happy.Weddings.Blog.Core.DTO.Requests.Story;
using Happy.Weddings.Blog.Core.DTO.Responses;
using MediatR;

namespace Happy.Weddings.Blog.Service.Commands.v1.Story
{
    /// <summary>
    /// Command for creating a story
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}" />
    public class CreateStoryCommand: IRequest<APIResponse>
    {
        /// <summary>
        /// Gets or sets the reuqest.
        /// </summary>
        public CreateStoryRequest Request { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStoryCommand"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public CreateStoryCommand(CreateStoryRequest request)
        {
            Request = request;
        }
    }
}
