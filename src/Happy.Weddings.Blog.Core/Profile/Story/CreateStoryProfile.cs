﻿using Happy.Weddings.Blog.Core.DTO.Requests.Story;
using Happy.Weddings.Blog.Core.Entity;
using System;

namespace Happy.Weddings.Blog.Core.Profile.Story
{
    public class CreateStoryProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStoryProfile"/> class.
        /// </summary>
        public CreateStoryProfile()
        {
            CreateMap<CreateStoryRequest, Stories>()
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(o => DateTime.UtcNow));
        }
    }
}