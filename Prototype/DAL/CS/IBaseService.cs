using System.Collections.Generic;
using DapperExtensions;

namespace marketplace
{
    public interface IBaseService<T, TKey>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetAll(IFieldPredicate predicate = null);
        IEnumerable<T> GetAll(PredicateGroup predicate);
        IEnumerable<T> GetAll(IBetweenPredicate predicate);
        /// <summary>
        /// Gets the paging.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalRow">The total row.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetPaging(int pageIndex, int pageSize, ref int totalRow);
        /// <summary>
        /// Counts the specified predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        int Count<T>(IFieldPredicate predicate = null) where T : class;
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        T GetById(int id);
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>TKey.</returns>
        T Insert(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Update(T entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        //bool Delete(TKey id);
        bool Delete(T entity);
    }
}
