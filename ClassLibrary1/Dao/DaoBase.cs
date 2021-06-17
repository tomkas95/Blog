using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Interface;
using NHibernate;
using NHibernate.Criterion;

namespace ClassLibrary1.Dao
{
    public class DaoBase<T> : IDaoBase<T> where T : class, IEntity// generická třída, bude definovat základní operace
    {
        protected ISession session;
        protected DaoBase()
        {
            session = NHibernateHelper.Session;

        }
        public IList<T> GetAll()
        {
            return session.QueryOver<T>().List<T>();
        }

        public object Create(T entity)
        {
            object o;
            using (ITransaction transaction = session.BeginTransaction())
            {
                o = session.Save(entity);
                transaction.Commit();
            }
            return o;
        }

        public void Delete(T entity)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                transaction.Commit();
            }
        }


        public T GetById(int id)
        {
            return session.CreateCriteria<T>().Add(Restrictions.Eq("Id", id)).UniqueResult<T>();
        }

        public void Update(T entity)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(entity);
                transaction.Commit();
            }
            
        }
    }
}
