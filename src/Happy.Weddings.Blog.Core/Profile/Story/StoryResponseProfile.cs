using Happy.Weddings.Blog.Core.DTO.Responses.Story;
using Happy.Weddings.Blog.Core.Entity;

namespace Happy.Weddings.Blog.Core.Profile.Story
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