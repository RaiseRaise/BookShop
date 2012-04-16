using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;

namespace BookShopModel.ContainerMock
{
    /// <summary>
    /// Concrete object set for use with Mock contexts. Implements all of the
    /// required interfaces, but performs no database functionality; instead
    /// merely stores the data for testing.
    /// </summary>
    public partial class MockDbSet<T> : IDbSet <T> 
        where T : class
    {
        private readonly IList<T> m_container = new List<T>();

        public T Add(T entity)
        {
            m_container.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            m_container.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local
        {
            get { throw new NotImplementedException(); }
        }

        public T Remove(T entity)
        {
            m_container.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_container.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_container.GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return m_container.AsQueryable<T>().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return m_container.AsQueryable<T>().Provider; }
        }
    }
}
