using AutoMapper;
using Happy.Weddings.Blog.Core.DTO.Responses;
using Happy.Weddings.Blog.Core.Entity;
using Happy.Weddings.Blog.Core.Repository;
using Happy.Weddings.Blog.Service.Commands.v1.Story;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Service.Handlers.v1.Story
{
    /// <summary>
    /// Handler for updating a story
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Happy.Weddings.Blog.Service.v1.Commands.UpdateStoriesByUserIdCommand, Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}" />
    public class UpdateStoriesByUserIdHandler : IRequestHandler<UpdateStoriesByUserIdCommand, APIResponse>
    {
        /// <summary>
        /// The repository
        /// </summary>
        private IRepositoryWrapper repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStoriesByUserIdHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public UpdateStoriesByUserIdHandler(
            IRepositoryWrapper repository,
            IMapper mapper,
            ILogger logger)
        {
            this.repository = repository;
            this.mapper = mapper;
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
        public async Task<APIResponse> Handle(UpdateStoriesByUserIdCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var stories = mapper.Map<List<Stories>>(request.Stories);
                repository.Stories.UpdateStories(stories);

                await repository.SaveAsync();

                return await Task.FromResult(new APIResponse(HttpStatusCode.NoContent));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in method 'UpdateStoryHandler()'");
                var exMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return await Task.FromResult(new APIResponse(exMessage, HttpStatusCode.InternalServerError));
            }
        }
    }
}
