using DapperExtensions;
using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public class BaseService<T, TKey> : BaseIpl<ADOProvider>, IBaseService<T, TKey> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseIpl{T}" /> class.
        /// </summary>
        /// <param name="schema">The schema.</param>
        public BaseService(string strConn = "Data Source=.;Initial Catalog=pe88_database; User Id=sa; Password=Admin123@;Max Pool Size=1000 ;Pooling=true;")
        {
            if (!string.IsNullOrEmpty(strConn))
            {
                _connection = strConn;
            }
        }
        #region base command
        /// <summary>
        /// Get All data
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> GetAll(IFieldPredicate predicate = null)
        {
            //https://github.com/StackExchange/Dapper/tree/master/Dapper.Contrib
            return unitOfWork.GetAll<T>(predicate);
        }

        public IEnumerable<T> GetAll(PredicateGroup predicate)
        {
            //https://github.com/StackExchange/Dapper/tree/master/Dapper.Contrib
            return unitOfWork.GetAll<T>(predicate);
        }

        public IEnumerable<T> GetAll(IBetweenPredicate predicate)
        {
            //https://github.com/StackExchange/Dapper/tree/master/Dapper.Contrib
            return unitOfWork.GetAll<T>(predicate);
        }

        /// <summary>
        /// Get All data by paging
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalRow">The total row.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public virtual IEnumerable<T> GetPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            var data = unitOfWork.GetPage<T>(null, null, pageIndex, pageSize);
            if (data.Count > 0) totalRow = data.Count;
            return data;
        }
        public virtual int Count<T>(IFieldPredicate predicate = null) where T : class
        {
            var data = unitOfWork.Count<T>(predicate);
            return data;
        }
        public T GetById(int id)
        {
            return unitOfWork.FindById<T>(id);
        }
        /// <summary>
        /// Insert new record to table
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>TKey.</returns>
        public T Insert(T entity)
        {
            try
            {
                unitOfWork.Insert(entity);
                return entity;
            }
            catch (Exception ex)
            {
                //log and return
                return default(T);
            }
        }
        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Update(T entity)
        {
            return unitOfWork.Update(entity);
        }
        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Delete(T entity)
        {
            return unitOfWork.Delete<T>(entity);
        }

        public void InsertDataUsingSqlBulkCopy<T>(IEnumerable<T> data, string schema = "")
        {
            unitOfWork.InsertDataUsingSqlBulkCopy<T>(data, schema);
        }

        #endregion

    }
}
