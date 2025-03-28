using System.Linq.Expressions;
using FleetManagement.Domain.Models.Common;
using FleetManagement.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infraestructure.Repositories
{
    public class BaseRepository<T> where T : BaseClass
    {
        protected readonly FleetManagementDbContext _databaseContext;
        public BaseRepository(IServiceScopeFactory serviceScopeFactory)
        {
            var scope = serviceScopeFactory.CreateScope();
            _databaseContext = scope.ServiceProvider.GetRequiredService<FleetManagementDbContext>();
        }

        public async Task SaveChanges()
        {
            try
            {
                await this._databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(T model)
        {
            model.LastUpdate = DateTime.Now;
            model.Archived = true;
            this._databaseContext.Entry(model).State = EntityState.Deleted;
            await SaveChanges();
        }

        public async Task<Guid> Create(T model)
        {
            model.CreateDate = DateTime.Now;
            await this._databaseContext.Set<T>().AddAsync(model);
            await SaveChanges();

            return model.Id;
        }

        public async Task Update(T model)
        {
            model.LastUpdate = DateTime.Now;
            this._databaseContext.Entry(model).State = EntityState.Modified;
            await SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _databaseContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllActive()
        {
            return await _databaseContext.Set<T>().Where(w => w.Archived == false).ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var entity = await _databaseContext.Set<T>().Where(w => w.Id == id).FirstOrDefaultAsync();
                _databaseContext.Set<T>().Remove(entity);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<T> GetOne(Guid id)
        {
            return await _databaseContext.Set<T>().Where(w => w.Id == id).FirstOrDefaultAsync();
        }


        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await _databaseContext.Set<T>().AnyAsync(predicate);
        }
    }
}
