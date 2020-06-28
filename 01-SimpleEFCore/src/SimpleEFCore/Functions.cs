using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SimpleEFCore
{
    public static class Functions
    {
        public static void ListAll()
        {
            using var context = new AppDbContext();
            foreach (var book in context.Books.AsNoTracking().Include(b => b.Author))
            {
                var webUrl = book.Author.WebUri ?? "no web uri given";
                Console.WriteLine($"{book.Title} by {book.Author.Name}");
                Console.WriteLine("     Published on " +
                                  $"{book.PublishedOn:dd-MMM-yyyy}. {webUrl}");
            }
        }

        public static void ChangeWebUrl()
        {
            Console.WriteLine("Set new WebUrl >");
            var newWebUrl = Console.ReadLine();

            using var context = new AppDbContext();
            var book = context.Books
                .Include(b => b.Author)
                .SingleOrDefault(x => x.Title == "TCAHR");

            if (book == null)
            {
                Console.WriteLine("No such book in the database");
                return;
            }

            book.Author.WebUri = newWebUrl;
            context.SaveChanges();
        }
    }
}
