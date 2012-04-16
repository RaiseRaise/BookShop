using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using BookShopModel.Model;

namespace BookShopModel.Interfaces
{
    /// <summary>
    /// The interface for the specialised object context. This contains all of
    /// the <code>IDbSet</code> properties that are implemented in both the
    /// functional context class and the mock context class.
    /// </summary>
    public interface IModelContainer
    {
        IDbSet<User> Users { get; }
        IDbSet<Author> Authors { get; }
        IDbSet<Theme> Themes { get; }
        IDbSet<Book> Books { get; }
        IDbSet<Sale> Sales { get; }
        int DbSaveChanges();
    }
}
