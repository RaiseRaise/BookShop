using BookShopModel.Model;
using BookShopModel.Interfaces;
using System.Data.Entity;

namespace BookShopModel.ContainerMock
{
    /// <summary>
    /// The concrete mock context object that implements the context's interface.
    /// Provide an instance of this mock context class to client logic when testing, 
    /// instead of providing a functional context object.
    /// </summary>
    public partial class ModelContainerMock : IModelContainer
    {
        public IDbSet<User> Users
        {
            get { return _users  ?? (_users = new MockDbSet<User>()); }
        }
        private IDbSet<User> _users;
        public IDbSet<Author> Authors
        {
            get { return _authors ?? (_authors = new MockDbSet<Author>()); }
        }
        private IDbSet<Author> _authors;
        public IDbSet<Book> Books
        {
            get { return _books ?? (_books = new MockDbSet<Book>()); }
        }
        private IDbSet<Book> _books;
        public IDbSet<Sale> Sales
        {
            get { return _sales ?? (_sales = new MockDbSet<Sale>()); }
        }
        private IDbSet<Sale> _sales;
        public IDbSet<Theme> Themes
        {
            get { return _themes ?? (_themes = new MockDbSet<Theme>()); }
        }
        private IDbSet<Theme> _themes;

        public int DbSaveChanges()
        {
            return 1;
        }
    }
}
