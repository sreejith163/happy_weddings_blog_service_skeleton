using Happy.Weddings.Blog.Core.DTO.Responses;
using Happy.Weddings.Blog.Core.Entity;

namespace Happy.Weddings.Blog.Core.Profile
{
    public class StoryResponseProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoryResponseProfile"/> class.
        /// </summary>
        public StoryResponseProfile()
        {
            CreateMap<Stories, StoryResponse>();
        }
    }
}