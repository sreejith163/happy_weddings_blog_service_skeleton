using AutoMapper;
using Happy.Weddings.Blog.Core.DTO.Responses;
using Happy.Weddings.Blog.Core.DTO.Responses.Story;
using Happy.Weddings.Blog.Core.Entity;
using Happy.Weddings.Blog.Core.Helpers;
using Happy.Weddings.Blog.Core.Infrastructure;
using Happy.Weddings.Blog.Core.Repository;
using Happy.Weddings.Blog.Core.Services;
using Happy.Weddings.Blog.Data.DatabaseContext;
using Happy.Weddings.Blog.Data.Repository;
using Happy.Weddings.Blog.Infrastructure;
using Happy.Weddings.Blog.Messaging.Receiver.v1;
using Happy.Weddings.Blog.Service.Commands.v1.Story;
using Happy.Weddings.Blog.Service.Handlers.v1.Story;
using Happy.Weddings.Blog.Service.Helpers;
using Happy.Weddings.Blog.Service.Queries.v1.Story;
using Happy.Weddings.Blog.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Happy.Weddings.Blog.API.Extensions
{
    /// <summary>
    /// Extension for adding object injection lifetime
    /// </summary>
    public static class ServicesInjection
    {
        /// <summary>
        /// Adds the services injection.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="HostingEnvironment">The hosting environment.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddServicesInjection(this IServiceCollection services,
                                                                   IWebHostEnvironment HostingEnvironment,
                                                                   IConfiguration configuration)
        {
            //Configure logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var rabbitMQConfig = configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfig>();

            services.AddSingleton(Log.Logger);
            services.AddSingleton(HostingEnvironment);
            services.AddSingleton(rabbitMQConfig);

            services.AddDbContext<BlogContext>(options =>
                 options.UseMySQL(configuration.GetConnectionString("Database")));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(typeof(Startup));
            services.AddTransient<IRequestHandler<GetAllStoriesQuery, APIResponse>, GetAllStoriesHandler>();
            services.AddTransient<IRequestHandler<GetStoryQuery, APIResponse>, GetStoryHandler>();
            services.AddTransient<IRequestHandler<GetStoryByUserIdQuery, List<Stories>>, GetStoryByUserIdHandler>();
            services.AddTransient<IRequestHandler<CreateStoryCommand, APIResponse>, CreateStoryHandler>();
            services.AddTransient<IRequestHandler<UpdateStoryCommand, APIResponse>, UpdateStoryHandler>();
            services.AddTransient<IRequestHandler<DeleteStoryCommand, APIResponse>, DeleteStoryHandler>();

            services.AddScoped<ISortHelper<StoryResponse>, SortHelper<StoryResponse>>();
            services.AddScoped<ISortHelper<Comments>, SortHelper<Comments>>();

            services.AddScoped<IDataShaper<StoryResponse>, DataShaper<StoryResponse>>();
            services.AddScoped<IDataShaper<Comments>, DataShaper<Comments>>();

            services.AddTransient<IConfigurationManager, ConfigurationManager>();

            services.AddTransient<IUserNameUpdateService, UserNameUpdateService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddHostedService<UsernameUpdateReceiver>();

            return services;
        }
    }
}
