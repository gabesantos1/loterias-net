using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Loterias.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity>
            where TEntity : class
    {
        /// <summary>
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> GetById(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns><see cref="IEnumerable{TEntity}"/></returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Returns the first entity that satisfies the condition or default value if no such is found.
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{TEntity,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> @where);

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{TEntity,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="IEnumerable{TEntity}"/></returns>
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> @where);

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="model"><see cref="TEntity"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="bool"/></returns>
        Task<bool> Add(TEntity model);

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="model"><see cref="TEntity"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="bool"/></returns>
        Task<bool> Update(TEntity model);

        /// <summary>
        /// Remove the entity
        /// </summary>
        /// <param name="model"><see cref="TEntity"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="bool"/></returns>
        Task<bool> Remove(TEntity model);
        void Dispose();
    }
}