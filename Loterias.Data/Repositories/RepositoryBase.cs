using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Loterias.Data.Context;
using Loterias.Domain.Interfaces.Repositories;
using System.Data.Common;
using System.Linq.Expressions;
using System.Linq;

namespace Loterias.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity>
            where TEntity : class
    {
        protected readonly LoteriasContext _context;

        protected RepositoryBase(LoteriasContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns><see cref="IEnumerable{TEntity}"/>Returns a IEnumerable<typeparamref name="TEntity"/></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAll() => await _context.Set<TEntity>().ToListAsync();

        /// <summary>
        /// Returns the first entity that satisfies the condition or default value if no such is found.
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{TEntity,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="TEntity"/>Returns a <typeparamref name="TEntity"/></returns>
        public virtual async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where));
            try
            {
                return await _context.Set<TEntity>().FirstOrDefaultAsync(where);
            }
            catch (DbException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{TEntity,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="IEnumerable{TEntity}"/>Returns a IEnumerable<typeparamref name="TEntity"/></returns>
        public virtual async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where));

            try
            {
                return await _context.Set<TEntity>().Where(where).ToListAsync();
            }
            catch (DbException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns><see cref="TEntity"/>Returns a <typeparamref name="TEntity"/></returns>
        public virtual async Task<TEntity> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id cannot be zero or lower.", nameof(id));

            return await _context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="model"><see cref="TEntity"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="bool"/>Returns the model</returns>
        public virtual async Task<TEntity> Add(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot add a null reference");

            try
            {
                await _context.Set<TEntity>().AddAsync(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="model"><see cref="TEntity"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="bool"/>Returns the entity</returns>
        public virtual async Task<TEntity> Update(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot update a null reference");

            try
            {
                _context.Set<TEntity>().Update(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove the entity
        /// </summary>
        /// <param name="model"><see cref="TEntity"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="bool"/>Returns true if removed, false if not</returns>
        public virtual async Task<bool> Remove(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot remove a null object");

            try
            {
                _context.Set<TEntity>().Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Releases all resource used by the <see cref="T:Loterias.Data.Repositories.RepositoryBase`1"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="T:Loterias.Data.Repositories.RepositoryBase`1"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="T:Loterias.Data.Repositories.RepositoryBase`1"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:Loterias.Data.Repositories.RepositoryBase`1"/> so the garbage collector can reclaim the memory
        /// that the <see cref="T:Loterias.Data.Repositories.RepositoryBase`1"/> was occupying.</remarks>
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}