﻿using Happy.Weddings.Blog.Core.DTO.Requests;
using Happy.Weddings.Blog.Core.Entity;
using System;

namespace Happy.Weddings.Blog.Core.Profile
{
    public class UpdateStoryProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStoryProfile"/> class.
        /// </summary>
        public UpdateStoryProfile()
        {
            CreateMap<UpdateStoryRequest, Stories>()
                .ForMember(x => x.UpdatedDate, opt => opt.MapFrom(o => DateTime.UtcNow));
        }
    }
}