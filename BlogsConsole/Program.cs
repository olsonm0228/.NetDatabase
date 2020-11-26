using System;
using NLog.Web;
using System.IO;
 using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogsConsole
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        static void Main(string[] args)
        {

            logger.Info("Program started");
            String menuChoice;
            int idChoice;
            Boolean loop = true;
            var db = new BloggingContext();


            try
            {
                do
                {
                    Console.WriteLine("Enter your selection:");
                    Console.WriteLine("1) Display all blogs");
                    Console.WriteLine("2) Add Blog");
                    Console.WriteLine("3) Create Post");
                    Console.WriteLine("4) Display Posts");
                    Console.WriteLine("5) Delete Blog");
                    Console.WriteLine("6) Edit Blog");
                    Console.WriteLine("Enter q to quit");
                    logger.Info("User input taken");
                    menuChoice = Console.ReadLine();

                    switch(menuChoice){
                        case "1":
                            logger.Info("1) selected");
                            // Display all Blogs from the database
                            var query = db.Blogs.OrderBy(b => b.Name);

                            Console.WriteLine(query.Count() + " Blogs returned");
                            if(query.Count() > 0){
                                foreach (var item in query)
                                {
                                    Console.WriteLine(item.Name);
                                }
                            }
                            break;
                        case "2":
                            logger.Info("2) selected");
                            // Create and save a new Blog
                            //Console.Write("Enter a name for a new Blog: ");
                            //var blog = new Blog { Name = Console.ReadLine() };
                            //var name = Console.ReadLine();

                            //var db = new BloggingContext();
                            Blog blog = InputBlog(db);
                            if (blog != null)
                            {
                                //blog.BlogId = BlogId;
                                db.AddBlog(blog);
                                logger.Info("Blog added - {name}", blog.Name);
                            }
                                //var db = new BloggingContext();
                                // check for unique name

                            // if(name.Equals("")){
                            //     logger.Error("Blog name cannot be null");
                            // } else{
                            //     var blog = new Blog { Name = name };

                            //     db.AddBlog(blog);
                            //     logger.Info("Blog added - {name}", name);
                            // }
                            break;
                        case "3":
                            logger.Info("3) selected");
                            // Display all Blogs from the database
                            Console.WriteLine("Select the blog you would like to post to:");
                            var queryBlogPost = db.Blogs.OrderBy(b => b.BlogId);

                            foreach (var item in queryBlogPost)
                            {
                                Console.WriteLine(item.BlogId +") "+ item.Name);
                            }
                            try{
                                idChoice = Int16.Parse(Console.ReadLine());
                                var existingId = queryBlogPost.Where(i => i.BlogId == idChoice).Count();
                                if(existingId > 0){
                                    //Enter Title - check for title existing
                                    Console.Write("Enter the Post title: ");
                                    var postTitle = Console.ReadLine();
                                    if(postTitle.Equals("")){
                                        logger.Error("Post title cannot be null");
                                    } else{
                                        //Enter Content
                                        Console.Write("Enter the Post content: ");
                                        var postContent = Console.ReadLine();
                                        var post = new Post{Title = postTitle, Content = postContent, BlogId = idChoice};
                                        db.AddPost(post);
                                        logger.Info("Post added - {postTitle}", postTitle);
                                    }
                                } else{
                                    logger.Error("There are no Blogs saved with that Id");
                                }
                            }catch{
                                logger.Error("Invalid Blog ID");
                            }
                            break;
                        case "4":
                            logger.Info("4) selected");
                            // Display all Blogs from the database
                            Console.WriteLine("Select the blog's posts to display");
                            var queryBlogDisplay = db.Blogs.OrderBy(b => b.BlogId);
                            Console.WriteLine("0) Posts from all blogs");
                            foreach (var item in queryBlogDisplay)
                            {
                                Console.WriteLine(item.BlogId +") "+ item.Name);
                            }
                            try{
                                idChoice = Int16.Parse(Console.ReadLine());
                                if(idChoice == 0){
                                    var postQuery = db.Posts.OrderBy(p => p.PostId);

                                    Console.WriteLine(postQuery.Count() + " Blogs returned");
                                    if(postQuery.Count() > 0){
                                        foreach (var item in postQuery)
                                        {
                                            Console.WriteLine("Blog: " + item.Blog.Name);
                                            Console.WriteLine("Title: " + item.Title);
                                            Console.WriteLine("Content: " + item.Content);
                                        }
                                    }
                                }else{
                                    var postQuery = db.Posts.OrderBy(p => p.PostId).Where(p => p.BlogId == idChoice);
                                    Console.WriteLine(postQuery.Count() + " Blogs returned");
                                    if(postQuery.Count() > 0){
                                        foreach (var item in postQuery)
                                        {
                                            Console.WriteLine("Blog: " + item.Blog.Name);
                                            Console.WriteLine("Title: " + item.Title);
                                            Console.WriteLine("Content: " + item.Content);
                                        }
                                    }
                                }
                            }catch{
                                logger.Error("Invalid Blog ID");
                            }
                            break;
                        case "5":
                            logger.Info("5) selected");
                            //var db = new BloggingContext();
                            var blog_here = GetBlog(db);
                            if (blog_here != null)
                            {
                                // delete blog
                                db.DeleteBlog(blog_here);                                logger.Info($"Blog (id: {blog_here.BlogId}) deleted");
                            }

                            Console.WriteLine("Choose the blog to delete:");
                            break;
                        case "6":
                            logger.Info("6) selected");
                            // edit blog
                            Console.WriteLine("Choose the blog to edit:");
                            //var db = new BloggingContext();
                            var blog_there = GetBlog(db);
                            if (blog_there != null)
                            {
                            // input blog
                            Blog UpdatedBlog = InputBlog(db);
                            if (UpdatedBlog != null)
                            {
                                UpdatedBlog.BlogId = blog_there.BlogId;
                                db.EditBlog(UpdatedBlog);
                                logger.Info($"Blog (id: {blog_there.BlogId}) updated");
                            }                            }
                            break;
                        default:
                            logger.Info("User has quit");
                            loop = false;
                            break;
                    }


                } while (loop);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }


        public static Blog GetBlog(BloggingContext db)
        {
            // display all blogs
            var blogs = db.Blogs.OrderBy(b => b.BlogId);
            foreach (Blog b in blogs)
            {
                Console.WriteLine($"{b.BlogId}: {b.Name}");
            }
            if (int.TryParse(Console.ReadLine(), out int BlogId))
            {
                Blog blog = db.Blogs.FirstOrDefault(b => b.BlogId == BlogId);
                if (blog != null)
                {
                    return blog;
                }
            }
            logger.Error("Invalid Blog Id");
            return null;
        }

        public static Blog InputBlog(BloggingContext db)
        {
            Blog blog = new Blog();
            Console.WriteLine("Enter the Blog name");
            blog.Name = Console.ReadLine();

            ValidationContext context = new ValidationContext(blog, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(blog, context, results, true);
            if (isValid)
            {
                //var db = new BloggingContext();
                // check for unique name
                if (db.Blogs.Any(b => b.Name == blog.Name))
                {
                    // generate validation error
                    isValid = false;
                    results.Add(new ValidationResult("Blog name exists", new string[] { "Name" }));
                    logger.Info("Blog name exists", new string[] { "Name" });

                }
                else
                {
                    logger.Info("Validation passed");
                    // save blog to db
                    return blog;
                    //logger.Info("Blog added - {name}", blog.Name);
                }
                //return blog;
            }
            else
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
            }
            return null;
        }
    }
}
