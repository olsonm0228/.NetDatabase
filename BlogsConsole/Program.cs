using System;
using NLog.Web;
using System.IO;
 using System.Linq;

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
                    Console.WriteLine("1) Display all blogs");
                    Console.WriteLine("2) Add Blog");
                    Console.WriteLine("3) Create Post");
                    Console.WriteLine("4) Display Posts");
                    Console.WriteLine("Enter q to quit");
                    logger.Info("User input taken");
                    menuChoice = Console.ReadLine();
                    //todo errors!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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
                            Console.Write("Enter a name for a new Blog: ");
                            var name = Console.ReadLine();
                            if(name.Equals("")){
                                logger.Error("Blog name cannot be null");
                            } else{
                                var blog = new Blog { Name = name };

                                db.AddBlog(blog);
                                logger.Info("Blog added - {name}", name);
                            }
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
                                //todo create post here
                            }catch{
                                logger.Error("Invalid Blog ID");
                            }

                            //todo ask what they want to post to and add a post
                            break;
                        case "4":
                            logger.Info("4) selected");
                            // Display all Blogs from the database
                            Console.WriteLine("Select the blog you would like to post to:");
                            var queryBlogDisplay = db.Blogs.OrderBy(b => b.BlogId);

                            foreach (var item in queryBlogDisplay)
                            {
                                Console.WriteLine(item.BlogId +") "+ item.Name);
                            }
                            try{
                                idChoice = Int16.Parse(Console.ReadLine());
                                //todo display posts here
                            }catch{
                                logger.Error("Invalid Blog ID");
                            }
                            //todo display posts on a blog after they select a blog
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
    }
}
