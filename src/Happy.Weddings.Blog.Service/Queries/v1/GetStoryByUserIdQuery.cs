using Happy.Weddings.Blog.Core.Entity;
using MediatR;
using System.Collections.Generic;

namespace Happy.Weddings.Blog.Service.Queries.v1
{
    /// <summary>
    /// Query for getting a story
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Collections.Generic.List{Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}}" />
    public class GetStoryByUserIdQuery : IRequest<List<Stories>>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStoryByUserIdQuery" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public GetStoryByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
