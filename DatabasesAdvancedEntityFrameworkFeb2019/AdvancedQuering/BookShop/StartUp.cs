namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
               var result = RemoveBooks(db);
                Console.WriteLine(result);
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(a => a.AgeRestriction == ageRestriction)
                .Select(t => t.Title)
                .OrderBy(x => x)
                .ToList();


            string result = string.Join(Environment.NewLine, books);
            return result;

        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var edditionType = Enum.Parse<EditionType>("Gold");

            var goldenBooks = context.Books
                .Where(b => b.Copies < 5000 && b.EditionType == edditionType)
                .OrderBy(b => b.BookId)
                .Select(t => t.Title)
                .ToList();

            var result = string.Join(Environment.NewLine, goldenBooks);
            return result;
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(p => p.Price > 40)
                .OrderByDescending(p => p.Price);

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            string result = sb.ToString().TrimEnd();
            return result;
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(d => d.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(t => t.Title)
                .ToList();

            string result = string.Join(Environment.NewLine, books);
            return result;

        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var books = context.Books
                .Where(bc => bc.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                .Select(t => t.Title)
                .OrderBy(t => t)
                .ToList();

            string result = string.Join(Environment.NewLine, books);
            return result;

        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime givenDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate.Value < givenDate)
                .OrderByDescending(b => b.ReleaseDate)

                .Select(x => new
                {
                    x.Price,
                    x.EditionType,
                    x.Title
                });

            string result = string.Join(Environment.NewLine, books.Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:f2}"));
            return result;
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(f => f.FirstName.EndsWith(input))
                .Select(x => new
                {
                    Fullname = x.FirstName + " " + x.LastName
                })
                .OrderBy(n => n.Fullname);

            string result = string.Join(Environment.NewLine, authors.Select(x => $"{x.Fullname}"));
            return result;
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(t => t.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(t => t.Title)
                .Select(t => t.Title)
                .ToList();

            string result = string.Join(Environment.NewLine, books);
            return result;

        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                 .Where(x => x.Author.LastName.ToLower().StartsWith(input.ToLower()))
                 .OrderBy(b => b.BookId)
                 .Select(x => new
                 {
                     x.Title,
                     x.Author.FirstName,
                     x.Author.LastName
                 });

            string result = string.Join(Environment.NewLine, books.Select(x => $"{x.Title} ({x.FirstName + " " + x.LastName})"));
            return result;
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                .Where(t => t.Title.Length > lengthCheck)
                .ToList();

            return books.Count();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    BooksCount = x.Books.Sum(sc => sc.Copies)
                })
                .OrderByDescending(x => x.BooksCount)
                .ToList();

            string result = string.Join(Environment.NewLine, authors.Select(x => $"{x.FirstName} {x.LastName} - {x.BooksCount}"));
            return result;
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var books = context.Categories
                .Select(x => new
                {
                    CategoryName = x.Name,
                    TotalProfit = x.CategoryBooks.Sum(e => e.Book.Copies * e.Book.Price)
                })
                .OrderByDescending(x => x.TotalProfit)
                .ThenBy(x => x.CategoryName)
                .ToList();

            string result = string.Join(Environment.NewLine, books.Select(x => $"{x.CategoryName} ${x.TotalProfit:f2}"));
            return result;
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    CategoryName = x.Name,
                    Books = x.CategoryBooks.Select(e => new
                    {
                        e.Book.Title,
                        e.Book.ReleaseDate
                    })
                    .OrderByDescending(rd => rd.ReleaseDate)
                    .Take(3)
                })
                .OrderBy(n => n.CategoryName);

            string result = string.Join(Environment.NewLine, categories.Select(x => $"--{x.CategoryName + Environment.NewLine}{string.Join(Environment.NewLine, x.Books.Select(a => $"{a.Title} ({a.ReleaseDate.Value.Year})"))}"));
            return result;
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(r => r.ReleaseDate.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();

        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Copies < 4000);

            int count = books.Count();

            context.RemoveRange(books);
            context.SaveChanges();

            return count;
        }
    }
}
