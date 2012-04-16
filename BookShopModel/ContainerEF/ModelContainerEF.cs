using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using BookShopModel.Model;
using BookShopModel.Interfaces;


namespace BookShopModel.ContainerEF
{
    /// <summary>
    /// The functional concrete object context. This is just like the normal
    /// context that would be generated using the POCO artefact generator, 
    /// apart from the fact that this one implements an interface containing 
    /// the entity set properties and exposes <code>IDbSet</code>
    /// instances for entity set properties.
    /// </summary>
    public class ModelContainerEF : DbContext, IModelContainer
    {
        public IDbSet<User> Users
        {
            get { return base.Set<User>(); }
        }
        public IDbSet<Author> Authors
        {
            get { return base.Set<Author>(); }
        }
        public IDbSet<Theme> Themes
        {
            get { return base.Set<Theme>(); }
        }
        public IDbSet<Book> Books
        {
            get { return base.Set<Book >(); }
        }
        public IDbSet<Sale> Sales
        {
            get { return base.Set<Sale>(); }
        }
        public int DbSaveChanges()
        {
            return this.SaveChanges();
        }
    }
}
