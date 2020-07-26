using Happy.Weddings.Blog.Core.DTO.Responses;
using Happy.Weddings.Blog.Core.Entity;
using MediatR;
using System.Collections.Generic;

namespace Happy.Weddings.Blog.Service.Commands.v1
{
    /// <summary>
    /// Command for updating a stories based on user id
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}" />
    public class UpdateStoriesByUserIdCommand : IRequest<APIResponse>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the reuqest.
        /// </summary>
        public List<Stories> Stories { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStoriesByUserIdCommand" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="stories">The stories.</param>
        public UpdateStoriesByUserIdCommand(int userId, List<Stories> stories)
        {
            UserId = userId;
            Stories = stories;
        }
    }
}
