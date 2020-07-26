using Happy.Weddings.Blog.Core.Entity;
using Happy.Weddings.Blog.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Happy.Weddings.Blog.Data.DatabaseContext
{
    public partial class BlogContext: DbContext
    {
        /// <summary>
        /// The configuration manager
        /// </summary>
        private readonly IConfigurationManager configurationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogContext"/> class.
        /// </summary>
        public BlogContext() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadElementDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="configurationManager">The configuration manager.</param>
        public BlogContext(
            DbContextOptions<BlogContext> options,
            IConfigurationManager configurationManager) : base(options)
        {
            this.configurationManager = configurationManager;
        }

        /// <summary>
        /// Gets or sets the stories.
        /// </summary>
        public virtual DbSet<Stories> Stories { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>        /// <value>
        /// The comments.
        /// </value>
        public virtual DbSet<Comments> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;user id=root;password=root;persistsecurityinfo=True;database=blog;Convert Zero Datetime=True");
            }
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PRIMARY");

                entity.ToTable("comments");

                entity.HasIndex(e => e.CommentId)
                    .HasName("commen_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StoryId)
                    .HasName("comments_story_id_idx");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("date");

                entity.Property(e => e.StoryId)
                    .HasColumnName("story_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Story)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.StoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_story_id");
            });

            modelBuilder.Entity<Stories>(entity =>
            {
                entity.HasKey(e => e.StoryId)
                    .HasName("PRIMARY");

                entity.ToTable("stories");

                entity.HasIndex(e => e.StoryId)
                    .HasName("StoryId_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.StoryId)
                    .HasColumnName("story_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("timestamp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
