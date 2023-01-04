//https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=visual-studio

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Ef3
{
    internal class Program
    {
        private static void Main()
        {
            using (var db = new BloggingContext())
            {
                // Note: This sample requires the database to be created before running.

                // Create
                Console.WriteLine("Inserting a new blog");
                var blog = new Blog
                {
                    Title = "Carrot Sticks & Celery Cholent",
                    Url = "http://jewishvegan.com",
                    Log = "",                    
                };
                blog.Posts.Add(new Post
                {
                    Title = "Cholent Reinvented",
                    Content = "blah blah"
                });
                blog.Posts.Add(new Post
                {
                    Title = "Cholent Lost",
                    Content = "blah blah"
                });
                Console.WriteLine(blog);
                Console.WriteLine(db.Posts.Count());

                db.Add(blog);
                db.SaveChanges();
                Console.WriteLine(blog);

                //int blogCount = OutputAllBlogs(db);

                Console.WriteLine(db.Posts.Count());

                //Console.WriteLine("Records: " + blogCount);
                
                // // Update
                // Console.WriteLine("Updating the blog and adding a post");
                // blog.Url = "https://devblogs.microsoft.com/dotnet";
                // blog.Posts.Add(
                //     new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
                // db.SaveChanges();

                // Delete
                // Console.WriteLine("Delete the blog");
                // db.Remove(blog);
                // db.SaveChanges();
            }
        }

        private static string BigString(int charCount = 10)
        {
            string s ="";
            for (int i = 0; i < 10_000; i++)
                s += "a";
            return s;
        }

        private static int OutputAllBlogs(BloggingContext db)
        {
            // Read = SELECT * FROM Blogs WHERE CONTAINS(Url,'http') ORDEDR BY BlogID
            Console.WriteLine("Querying for a blog");
            var blogs = db.Blogs
                .Include(b => b.Posts)
                .Where(b => b.Url.Contains("http"))
                .OrderBy(b => b.BlogId); // converts LINQ into SQL

            foreach (var b in blogs)
            {
                Console.WriteLine(b);
            }

            return blogs.Count();
        }
    }
}