using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Happy.Weddings.Blog.Core.Entity
{
    public partial class Comments
    {
        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        [Column("Comment_Id")]
        public int CommentId { get; set; }

        /// <summary>
        /// Gets or sets the story identifier.
        /// </summary>
        [Column("Story_Id")]
        public int StoryId { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

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
        /// Gets or sets the story.
        /// </summary>
        public virtual Stories Story { get; set; }
    }
}
