using Happy.Weddings.Blog.Core.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Happy.Weddings.Blog.Core.DTO.Responses.Story
{
    public class StoryResponseDetails : StoryResponse
    {
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Comments> Comments { get; set; }
    }
}
