﻿using Happy.Weddings.Blog.Core.DTO.Responses;
using Happy.Weddings.Blog.Core.Repository;
using Happy.Weddings.Blog.Service.Commands.v1;
using MediatR;
using Serilog;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Service.Handlers.v1
{
    /// <summary>
    /// Handler for deleting a story
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Happy.Weddings.Blog.Service.v1.Commands.DeleteStoryCommand, Happy.Weddings.Blog.Core.DTO.Responses.APIResponse}" />
    public class DeleteStoryHandler : IRequestHandler<DeleteStoryCommand, APIResponse>
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
        /// Initializes a new instance of the <see cref="DeleteStoryHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteStoryHandler(
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
        public async Task<APIResponse> Handle(DeleteStoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var story = await repository.Stories.GetStoryById(request.StoryId);
                if (story == null)
                {
                    return await Task.FromResult(new APIResponse(HttpStatusCode.NotFound));
                }

                repository.Stories.DeleteStory(story);
                await repository.SaveAsync();

                return await Task.FromResult(new APIResponse(HttpStatusCode.NoContent));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in method 'DeleteStoryHandler()'");
                var exMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return await Task.FromResult(new APIResponse(exMessage, HttpStatusCode.InternalServerError));
            }
        }
    }
}