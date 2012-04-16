using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShopModel.Model;
using BookShopModel.Interfaces;
using System.Web.Security;
using BookShopModel.ContainerEF;

namespace BookShopModel
{
    public class Repository
    {
        #region Common
        public IModelContainer modelContainer;
        public Repository(IModelContainer ModelContainer)
        {
            modelContainer = ModelContainer;
        }
        /// <summary>
        /// Шифрование строки
        /// </summary>
        /// <param name="s">Исходная строка</param>
        /// <returns>Зашифрованная строка</returns>
        private string GetMD5HashString(string s)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));
            StringBuilder hash = new StringBuilder();
            foreach (byte b in data)
                hash.Append(b.ToString("x2"));
            return hash.ToString();
        }
        #endregion
        #region UserMethods
        /// <summary>
        /// Check User by login and password 
        /// </summary>
        /// <param name="UserName">Login</param>
        /// <param name="Password">Password</param>
        /// <param name="CorrectUserName">Login from DB</param>
        public bool Authenticate(string UserName, string Password, out string CorrectUserName)
        {
            bool result = false;
            User User = modelContainer.Users.SingleOrDefault(u => u.UserName == UserName);
            if (User != null)
            {
                CorrectUserName = User.UserName;
                result = User != null && GetMD5HashString(Password) == User.Password;
            }
            else
            {
                CorrectUserName = "";
            }
            return result;
        }
        /// <summary>
        /// Get User by UserName
        /// </summary>
        /// <param name="UserName">UserName</param>
        public User GetUser(string UserName)
        {
            return modelContainer.Users.FirstOrDefault(u => u.UserName == UserName);
        }
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="UserName">User Name</param>
        /// <param name="Password">Password</param>
        /// <param name="Email">Email</param>
        /// <returns>Registration result</returns>
        public MembershipCreateStatus CreateUser(string UserName, string Password, string Email)
        {
            if (modelContainer.Users.Any(u => u.UserName == UserName))
                return MembershipCreateStatus.DuplicateUserName;
            if (modelContainer.Users.Any(u => u.Email == Email))
                return MembershipCreateStatus.DuplicateEmail;
            User newUser = new User()
            {
                UserName = UserName,
                Password = GetMD5HashString(Password),
                Email = Email
            };
            modelContainer.Users.Add(newUser);
            if (modelContainer.DbSaveChanges() > 0)
                return MembershipCreateStatus.Success;
            else
                return MembershipCreateStatus.ProviderError;
        }
        #endregion
        #region ThemeMethods
        /// <summary>
        /// Get All Themes
        /// </summary>
        public IQueryable<Theme> GetThemes()
        {
            return modelContainer.Themes;
        }
        /// <summary>
        /// Get Theme by Id
        /// </summary>
        /// <param name="id"></param>
        public Theme GetThemeById(int id)
        {
            return modelContainer.Themes.SingleOrDefault(b => b.Id == id);
        }
        /// <summary>
        /// Create new theme
        /// </summary>
        /// <param name="newTheme">New theme</param>
        /// <returns>0 - if creating fail</returns>
        public int CreateTheme(Theme newTheme)
        {
            modelContainer.Themes.Add(newTheme);
            return modelContainer.DbSaveChanges();
        }
        /// <summary>
        /// Edit theme
        /// </summary>
        /// <param name="newTheme">New data</param>
        /// <returns>0 - if editing fail</returns>
        public int EditTheme(Theme newTheme)
        {
            Theme Theme = modelContainer.Themes.FirstOrDefault(b => b.Id == newTheme.Id);
            if (Theme != null)
            {
                Theme.ThemeName = newTheme.ThemeName;
                return modelContainer.DbSaveChanges();
            }
            return 0;
        }
        /// <summary>
        /// Delete theme
        /// </summary>
        /// <param name="id">id of theme to delete</param>
        /// <returns>0 - if deleting fail</returns>
        public int DeleteTheme(int id)
        {
            Theme Theme = modelContainer.Themes.FirstOrDefault(b => b.Id == id);
            if (Theme != null)
            {
                modelContainer.Themes.Remove(Theme);
                return modelContainer.DbSaveChanges();
            }
            return 0;
        }
        #endregion
        #region AuthorMethods
        public IQueryable<Author> GetAuthors()
        {
            return modelContainer.Authors;
        }
        public Author GetAuthorById(int id)
        {
            return modelContainer.Authors.SingleOrDefault(b => b.Id == id);
        }
        public int CreateAuthor(Author newAuthor)
        {
            modelContainer.Authors.Add(newAuthor);
            return modelContainer.DbSaveChanges();
        }
        public int EditAuthor(Author newAuthor)
        {
            Author Author = modelContainer.Authors.FirstOrDefault(b => b.Id == newAuthor.Id);
            if (Author != null)
            {
                Author.FirstName = newAuthor.FirstName;
                Author.LastName = newAuthor.LastName;
                return modelContainer.DbSaveChanges();
            }
            return 0;
        }
        public int DeleteAuthor(int id)
        {
            Author Author = modelContainer.Authors.FirstOrDefault(b => b.Id == id);
            if (Author != null)
            {
                modelContainer.Authors.Remove(Author);
                return modelContainer.DbSaveChanges();
            }
            return 0;
        }
        #endregion
        #region BookMethods
        public IQueryable<Book> GetBooks()
        {
            return modelContainer.Books;
        }
        public Book GetBookById(int id)
        {
            return modelContainer.Books.SingleOrDefault(b => b.Id == id);
        }
        public int CreateBook(Book newBook)
        {
            modelContainer.Books.Add(newBook);
            return modelContainer.DbSaveChanges();
        }
        public int EditBook(Book newBook)
        {
            Book book = modelContainer.Books.FirstOrDefault(b => b.Id == newBook.Id);
            if (book != null)
            {
                book.AuthorId = newBook.AuthorId;
                book.ThemeId = newBook.ThemeId;
                book.BookName = newBook.BookName;
                book.Price = newBook.Price;
                return modelContainer.DbSaveChanges();
            }
            return 0;
        }
        public int DeleteBook(int id)
        {
            Book book = modelContainer.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                modelContainer.Books.Remove(book);
                return modelContainer.DbSaveChanges();
            }
            return 0;
        }
        #endregion
        #region SaleMethods
        public IQueryable<Sale> GetSales(User owner = null)
        {
            if (owner != null)
                return modelContainer.Sales.Where(s => s.User.Id == owner.Id);
            else
                return modelContainer.Sales;
        }
        public Sale GetSaleById(int id)
        {
            return modelContainer.Sales.SingleOrDefault(b => b.Id == id);
        }
        public int CreateSale(string UserName, Sale newSale)
        {
            User user = GetUser(UserName);
            if (user != null)
            {
                newSale.UserId = user.Id;
                newSale.User = user;
                modelContainer.Sales.Add(newSale);
            }
            return modelContainer.DbSaveChanges();
        }
        public int EditSale(Sale newSale)
        {
            Sale Sale = modelContainer.Sales.FirstOrDefault(b => b.Id == newSale.Id);
            if (Sale != null)
            {
                Sale.BookId = newSale.BookId;
                Sale.UserId = newSale.UserId;
                Sale.Date = newSale.Date;
                Sale.Quantity = newSale.Quantity;
                return modelContainer.DbSaveChanges();
            }
            return 0;
        }
        public int DeleteSale(int id)
        {
            Sale Sale = modelContainer.Sales.FirstOrDefault(b => b.Id == id);
            if (Sale != null)
            {
                modelContainer.Sales.Remove(Sale);
                return modelContainer.DbSaveChanges();
            }
            return 0;
        }
        #endregion
    }
}
