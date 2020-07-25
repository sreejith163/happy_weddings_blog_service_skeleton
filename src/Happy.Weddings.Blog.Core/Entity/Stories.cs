using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Happy.Weddings.Blog.Core.Entity
{
    public partial class Stories
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stories"/> class.
        /// </summary>
        public Stories()
        {
            Comments = new HashSet<Comments>();
        }

        /// <summary>
        /// Gets or sets the story identifier.
        /// </summary>
        [Column("Story_Id")]
        public int StoryId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [Column("Created_By")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        [Column("Updated_By")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        [Column("Updated_Date")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
