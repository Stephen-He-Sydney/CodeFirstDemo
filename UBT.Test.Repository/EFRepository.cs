using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Validation;

namespace UBTTest.Repository
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private IDbSet<T> _entities;

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _dbContext.Set<T>();
                return _entities;
            }
        }

        public EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                this._dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var msg = string.Empty;

                foreach (var validationErrors in ex.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, ex);
                throw fail;
            }
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    this.Entities.Add(entity);

                this._dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var msg = string.Empty;

                foreach (var validationErrors in ex.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage)
                            + Environment.NewLine;

                var fail = new Exception(msg, ex);
                throw fail;
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this._dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var msg = string.Empty;

                foreach (var validationErrors in ex.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, ex);
                throw fail;
            }
        }

        public virtual T Delete(dynamic id)
        {
            var entity = GetById(id);
            Delete(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);
                this._dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var msg = string.Empty;

                foreach (var validationErrors in ex.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, ex);
                throw fail;
            }
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    this.Entities.Remove(entity);

                this._dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var msg = string.Empty;

                foreach (var validationErrors in ex.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, ex);
                throw fail;
            }
        }

        public virtual int Count(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? Table.Count() : Table.Count(filter);
        }

        public virtual bool Any(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? Table.AsNoTracking().Any() : Table.AsNoTracking().Any(filter);
        }

        public virtual bool Any(params object[] ids)
        {
            return GetById(ids) != null;
        }

        public virtual void RefreshEntity(T entityToReload)
        {
            this._dbContext.Entry(entityToReload).Reload();
        }

        public virtual void LoadProperties<TProperty>(T entity,
            params Expression<Func<T, TProperty>>[] properties) where TProperty : class
        {
            if (properties != null)
            {
                foreach (var property in properties)
                {
                    var body = property.Body as MemberExpression;
                    var propertyInfo = (System.Reflection.PropertyInfo)body.Member;

                    if (propertyInfo.PropertyType.IsGenericType &&
                        propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        var propertyAsCollection = _dbContext.Entry(entity).Collection(propertyInfo.Name);
                        if (!propertyAsCollection.IsLoaded)
                        {
                            propertyAsCollection.Load();
                        }
                    }
                    else
                    {
                        var propertyAsReference = _dbContext.Entry(entity).Reference(property);
                        if (!propertyAsReference.IsLoaded)
                        {
                            propertyAsReference.Load();
                        }
                    }
                }
            }
        }
    }
}
