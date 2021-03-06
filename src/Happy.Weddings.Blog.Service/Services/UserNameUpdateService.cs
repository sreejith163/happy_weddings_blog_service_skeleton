﻿using Happy.Weddings.Blog.Core.DTO;
using Happy.Weddings.Blog.Core.Services;
using Happy.Weddings.Blog.Service.Commands.v1.Story;
using Happy.Weddings.Blog.Service.Queries.v1.Story;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Happy.Weddings.Blog.Service.Services
{
    /// <summary>
    /// Implemention to update the user details in all the stories
    /// </summary>
    public class UserNameUpdateService : IUserNameUpdateService
    {
        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IServiceScopeFactory serviceScopeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNameUpdateService"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public UserNameUpdateService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// Updates the user name in stories.
        /// </summary>
        /// <param name="updateUserFullNameModel">The update user full name model.</param>
        public async Task UpdateUserNameInStories(UpdateUserFullNameModel updateUserFullNameModel)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var getStoriesByUserIdQuery = new GetStoryByUserIdQuery(updateUserFullNameModel.Id);
            var stories = await mediator.Send(getStoriesByUserIdQuery);

            if (stories != null && stories.Any())
            {
                stories.ForEach(x => x.Author = $"{updateUserFullNameModel.FirstName} {updateUserFullNameModel.LastName}");
            }

            var updateStoriesCommand = new UpdateStoriesByUserIdCommand(updateUserFullNameModel.Id, stories);
            var response = await mediator.Send(updateStoriesCommand);
        }
    }
}
