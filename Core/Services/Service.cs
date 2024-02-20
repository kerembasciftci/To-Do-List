using Core.DataAccess.Repositories;
using Core.Log;
using Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                await _unitOfWork.CommitAsync();
                _logger.LogInfo($"{typeof(T).Name} added successfully");
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding {typeof(T).Name}: {ex.Message}");
                throw;

            }
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _repository.AddRangeAsync(entities);
                await _unitOfWork.CommitAsync();
                _logger.LogInfo($"{typeof(T).Name} added successfully");
                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding {typeof(T).Name}: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var items = await _repository.GetAll().ToListAsync();
                _logger.LogInfo($"{items.Count} items taken");
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAll method didnt work : {ex.Message}");
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                _logger.LogInfo($"Id {id} item successfully came");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Id {id} item didnt came:{ex.Message}");
                throw;
            }

        }

        public async Task RemoveAsync(T entity)
        {
            try
            {
                _repository.Remove(entity);
                await _unitOfWork.CommitAsync();
                _logger.LogInfo($"{entity.GetType().Name} removed");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Entity didnt remove : {ex.Message}");
                throw;
            }
           
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                _repository.RemoveRange(entities);
                await _unitOfWork.CommitAsync();
                _logger.LogInfo("Successfuly entities removed !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Entity didnt remove : {ex.Message}");
                throw;
            }
          
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _repository.Update(entity);
                await _unitOfWork.CommitAsync();
                _logger.LogInfo($"Updated {entity}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Entity didnt remove : {ex.Message}");
                throw;
            }
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            try
            {
                var item = _repository.Where(expression);
                _logger.LogInfo("Element finded");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Element can not found :{ex.Message}");
                throw;
            }
        }
    }
}
