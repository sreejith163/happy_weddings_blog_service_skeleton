using Happy.Weddings.Blog.Core.DTO.Responses.Story;
using Happy.Weddings.Blog.Core.Entity;

namespace Happy.Weddings.Blog.Core.Profile.Story
{
    public class StoryResponseDetailsProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoryResponseDetailsProfile"/> class.
        /// </summary>
        public StoryResponseDetailsProfile()
        {
            CreateMap<Stories, StoryResponseDetails>();
        }
    }
}