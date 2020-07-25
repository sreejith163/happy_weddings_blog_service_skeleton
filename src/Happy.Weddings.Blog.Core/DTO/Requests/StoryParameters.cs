using Happy.Weddings.Blog.Core.Domain;
using System;

namespace Happy.Weddings.Blog.Core.DTO.Requests
{
    public class StoryParameters : QueryStringParameters
    {
        public StoryParameters()
        {
            OrderBy = "Title";
        }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
