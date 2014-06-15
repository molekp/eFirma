using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Tests
{
    /// <summary>
    /// klasa Fake dla DbSet'ow z DataBaseContext
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        private readonly HashSet<T> m_data;
        private readonly IQueryable m_query;

        public FakeDbSet()
        {
            m_data = new HashSet<T>();
            m_query = m_data.AsQueryable();
        }

        public T Add(T a_entity)
        {
            if(a_entity == null)
            {
                throw new ArgumentNullException();
            }
            m_data.Add(a_entity);
            return a_entity;
        }

        public T Attach(T a_entity)
        {
            m_data.Add(a_entity);
            return a_entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public virtual T Find(params object[] a_keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet and override Find");
        }

        public ObservableCollection<T> Local
        {
            get { return new ObservableCollection<T>(m_data); }
        }

        public T Remove(T a_entity)
        {
            if(a_entity == null)
            {
                throw new ArgumentNullException();
            }
            m_data.Remove(a_entity);
            return a_entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_data.GetEnumerator();
        }

        public Type ElementType
        {
            get { return m_query.ElementType; }
        }

        public Expression Expression
        {
            get { return m_query.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return m_query.Provider; }
        }
    }
}