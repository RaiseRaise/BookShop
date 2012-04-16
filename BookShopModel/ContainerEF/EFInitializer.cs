using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShopModel.Model;
using System.Data.Entity;

namespace BookShopModel.ContainerEF
{
    /// <summary>
    /// Add some test records
    /// </summary>
    public class EFInitializer : DropCreateDatabaseIfModelChanges<ModelContainerEF>
    {
        protected override void Seed(ModelContainerEF context)
        {
            var users = new List<User>
            {
                new User {UserName = "User", Password = "e10adc3949ba59abbe56e057f20f883e", Email = "User@mail.ru", IsAdmin = false},
                new User {UserName = "Admin", Password = "e10adc3949ba59abbe56e057f20f883e", Email = "Admin@mail.ru", IsAdmin = true},
            };
            users.ForEach(u => context.Users.Add(u));

            var authors = new List<Author>
            {
                new Author {FirstName = "Andrew", LastName="Troelsen"},
                new Author {FirstName = "Sergey", LastName="Lukyanenko"},
                new Author {FirstName = "Gaidar", LastName="Magdanurov"},
                new Author {FirstName = "Eric", LastName="Freeman"}
            };
            authors.ForEach(a => context.Authors.Add(a));
            var themes = new List<Theme>
            {
                new Theme {ThemeName = "Fiction"},
                new Theme {ThemeName = "Scientific"},
                new Theme {ThemeName = "Other"}
            };
            themes.ForEach(t => context.Themes.Add(t));
            context.SaveChanges();
            var books = new List<Book>
            {
                new Book {BookName = "Pro C# 2010", AuthorId = 1, Price= 150, ThemeId = 2},
                new Book {BookName = "Design Patterns", AuthorId = 4, Price= 120, ThemeId = 2},
                new Book {BookName = "Nochnoy Dozor", AuthorId = 2, Price= 80, ThemeId = 1},
                new Book {BookName = "ASP.NET MVC Framework", AuthorId = 3, Price= 140, ThemeId = 2},
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();
        }
    }
}
