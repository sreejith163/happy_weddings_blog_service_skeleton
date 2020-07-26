using System.Net;
using System.Threading.Tasks;
using Happy.Weddings.Blog.Core.Domain;
using Happy.Weddings.Blog.Core.DTO.Requests.Story;
using Happy.Weddings.Blog.Service.Commands.v1.Story;
using Happy.Weddings.Blog.Service.Queries.v1.Story;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Happy.Weddings.Blog.API.Controllers.v1
{
    /// <summary>
    /// Blog stories operations handled by this controller
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/v1/blogs/stories")]
    [ApiController]
    public class StoriesController : Controller
    {
        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoriesController" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public StoriesController(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Gets the stories.
        /// </summary>
        /// <param name="storyParameters">The story parameters.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetStories([FromQuery] StoryParameters storyParameters)
        {
            var getAllStoriesQuery = new GetAllStoriesQuery(storyParameters);
            var result = await mediator.Send(getAllStoriesQuery);

            if (result.Code == HttpStatusCode.OK)
            {
                Response.Headers.Add("X-Pagination", PagedList<Entity>.ToJson(result.Value as PagedList<Entity>));
            }

            return StatusCode((int)result.Code, result.Value);
        }

        /// <summary>
        /// Gets the story.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        [Route("{storyId}")]
        [HttpGet]
        public async Task<IActionResult> GetStory(int storyId)
        {
            var getStoryQuery = new GetStoryQuery(storyId);
            var result = await mediator.Send(getStoryQuery);
            return StatusCode((int)result.Code, result.Value);
        }

        /// <summary>
        /// Creates the story.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateStory([FromBody] CreateStoryRequest request)
        {
            var createStoryCommand = new CreateStoryCommand(request);
            var result = await mediator.Send(createStoryCommand);
            return StatusCode((int)result.Code, result.Value);
        }

        /// <summary>
        /// Updates the story.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("{storyId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateStory(int storyId, [FromBody] UpdateStoryRequest request)
        {
            var updateStoryCommand = new UpdateStoryCommand(storyId, request);
            var result = await mediator.Send(updateStoryCommand);
            return StatusCode((int)result.Code, result.Value);
        }

        /// <summary>
        /// Deletes the story.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns></returns>
        [Route("{storyId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteStory(int storyId)
        {
            var deleteStoryCommand = new DeleteStoryCommand(storyId);
            var result = await mediator.Send(deleteStoryCommand);
            return StatusCode((int)result.Code, result.Value);
        }
    }
}