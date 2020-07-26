using Happy.Weddings.Blog.Core.Entity;
using Happy.Weddings.Blog.Core.Repository;
using Happy.Weddings.Blog.Service.Queries.v1.Story;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Service.Handlers.v1.Story
{
    /// <summary>
    /// Handler for getting a story
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Happy.Weddings.Blog.Service.v1.Queries.GetStoryByUserIdQuery, Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}" />
    public class GetStoryByUserIdHandler : IRequestHandler<GetStoryByUserIdQuery, List<Stories>>
    {
        /// <summary>
        /// The repository
        /// </summary>
        private IRepositoryWrapper repository;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStoryHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        public GetStoryByUserIdHandler(
            IRepositoryWrapper repository,
            ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        public async Task<List<Stories>> Handle(GetStoryByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await repository.Stories.GetStoriesByUserId(request.UserId);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in method 'GetStoriesByUserIdHandler()'");
                return new List<Stories>();
            }
        }
    }
}
