using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Ef3
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=blogging.db");
    }

    public static class SystemConst
    {
        public const int MaxStringLengthInDb = 1000;
    }
    
    public class Blog
    {
        // actual database fields
        public int BlogId { get; set; }

        [MaxLength(SystemConst.MaxStringLengthInDb)]
        public string Title { get; set; }

        [MaxLength(SystemConst.MaxStringLengthInDb)]
        public string Log { get; set; }

        [MaxLength(SystemConst.MaxStringLengthInDb)]
        public string Url { get; set; }
        public DateTime StartDate { get; set; }

        // Navigation Property - Not a db field
        public List<Post> Posts { get; } = new List<Post>();

        public override string ToString()
        {
            return $"BlogId={BlogId}, " +
                   $"Title=\"{Title}\"" + Environment.NewLine +
                   $"Log=\"{Log?.Length}\"" + Environment.NewLine +
                   $"Url =\"{Url}\"" + Environment.NewLine +
                   string.Join("\n", Posts);
        }
    }

    public class Post
    {
        // actual database fields
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        // Foreign Key
        public int BlogId { get; set; }

        // Navigation Property - Not a db field
        public Blog Blog { get; set; }

        public override string ToString()
        {
            return $"\tPostId={PostId}\n\tTitle=\"{Title}\"\n\tContent =\"{Content}\"";
        }
    }
}

