using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace UBTTest.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Delete an entity in DbSet by set Deleted to true or remove from DbSet
        /// </summary>
        /// <param name="id">id of entity to delete</param>
        T Delete(dynamic id);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        ///     Count total entry in database by condition
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> filter = null);

        /// <summary>
        ///     Check is exists any entry in database by condition
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> filter = null);

        /// <summary>
        ///     Check is exists any entry in database by keys
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Any(params object[] ids);

        /// <summary>
        /// Reload the entity from database
        /// </summary>
        /// <param name="entityToReload"></param>
        void RefreshEntity(T entityToReload);

        /// <summary>
        ///     Load the navigation properties with values from database.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        void LoadProperties<TProperty>(T entity,
            params Expression<Func<T, TProperty>>[] properties) where TProperty : class;
    }
}
