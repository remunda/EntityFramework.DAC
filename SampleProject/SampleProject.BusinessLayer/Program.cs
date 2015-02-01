using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Entities
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new SampleContext())
            {
                var user = new User()
                {
                    UserName = "username",
                    FirstName = "firstname",
                    LastName = "lastname",
                    Email = "email@email.com"
                };

                var article = new Article()
                {
                    Name = "My Article",
                    Description = "Description of my article",
                    Content = "<h1>My Great Article</h1>",
                    CreatedBy = user
                };

                db.Users.Add(user);
                db.Articles.Add(article);

                db.SaveChanges();

                Console.WriteLine("Changes saved");
                Console.ReadLine();

                var users = db.Users.ToList();
                var articles = db.Articles.ToList();

                Console.WriteLine("users count: " + users.Count);
                Console.WriteLine("articles count: " + articles.Count);
                Console.ReadLine();

            }
        }

    }
}
