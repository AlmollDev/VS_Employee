using EmployeeAdminPortalLST.Data;
using EmployeeAdminPortalLST.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeAdminPortalLST.Services
{
    public class GenericService<T, TKey> : IGenericService<T, TKey> where T : class

    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
;              

        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity; 
        }
        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync();
        }

     

     

        public async Task UpdateAsync(T entity)
        {
              _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();

        }
        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

       

        public async Task<PageResult<T>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            var totalCount = await query.CountAsync();
            var data = await query.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            return new PageResult<T>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = data
            };
        }
        // Advanced: Paging + Sorting + Filtering
        }
        
    /// <summary>
    /// Generic Service Interface
    /// --------------------------------------------
    /// این Interface برای ایجاد یک سرویس عمومی (Generic) برای هر Entity در پروژه استفاده می‌شود.
    /// هدف:
    /// 1. جلوگیری از تکرار کد در CRUD های مشابه برای هر Entity.
    /// 2. قابلیت استفاده مجدد برای هر Entity (مثل Employee, User, Product).
    /// 3. مدیریت عملیات دیتابیس از طریق سرویس و نه مستقیم در Controller.
    ///
    /// نکات:
    /// - T : class → هر Entity که کلاس هست.
    /// - TKey → نوع Id هر Entity (می‌تواند Guid یا int یا هر نوع دیگر باشد).
    /// - متدها شامل CRUD پایه هستند:
    ///     GetAllAsync, GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync
    /// - GetByIdAsync از TKey استفاده می‌کند تا قابل تطبیق با انواع مختلف Id باشد.
    /// </summary>
    public interface IGenericService<T, TKey> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(TKey id);   // می‌تواند Guid یا int یا هر نوع دیگر باشد
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);

        // advanced: Paging/Sorting/Filtering
        Task<PageResult<T>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        IQueryable<T> AsQueryable();
    }
}



