﻿using Happy.Weddings.Blog.Core.Domain;
using System;

namespace Happy.Weddings.Blog.Core.DTO.Requests.Story
{
    public class StoryParameters : QueryStringParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoryParameters"/> class.
        /// </summary>
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
        /// Gets or sets the search keyword.
        /// </summary>
        public string SearchKeyword { get; set; }
    }
}
