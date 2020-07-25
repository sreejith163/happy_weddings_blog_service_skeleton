using Happy.Weddings.Blog.Core.DTO.Responses;
using Happy.Weddings.Blog.Core.Repository;
using Happy.Weddings.Blog.Service.Queries.v1;
using MediatR;
using Serilog;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Service.Handlers.v1
{
    /// <summary>
    /// Handler for getting all stories
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Happy.Weddings.Blog.Service.v1.Queries.GetAllStoriesQuery, Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}" />
    public class GetAllStoriesHandler : IRequestHandler<GetAllStoriesQuery, APIResponse>
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
        /// Initializes a new instance of the <see cref="GetAllStoriesHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        public GetAllStoriesHandler(
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
        public async Task<APIResponse> Handle(GetAllStoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stories = await repository.Stories.GetAllStories(request.StoryParameters);
                return new APIResponse(stories, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in method 'GetAllStoriesHandler()'");
                var exMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return await Task.FromResult(new APIResponse(exMessage, HttpStatusCode.InternalServerError));
            }
        }
    }
}
