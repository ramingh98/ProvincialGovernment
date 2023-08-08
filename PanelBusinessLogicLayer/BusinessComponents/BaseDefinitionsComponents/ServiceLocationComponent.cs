using DataAccessLayer.Repositories;
using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents
{
    public class ServiceLocationComponent : IDisposable
    {
        private readonly Repository<ServiceLocationModel> _repository;

        public ServiceLocationComponent()
        {
            _repository = new Repository<ServiceLocationModel>();
        }

        public IQueryable<ServiceLocationModel> Read()
        {
            var result = _repository.SelectAllAsQuerable();
            return result;
        }

        public async Task AddAsync(ServiceLocationModel locationModel)
        {
            var query = await _repository.FirstOrDefaultAsync(q => q.Title == locationModel.Title);
            if (query != null)
            {
                throw new Exception("عنوان تکراری است");
            }
            await _repository.AddAsync(locationModel);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceLocationModel locationModel)
        {
            var query = await _repository.FirstOrDefaultAsync(q => q.Title == locationModel.Title);
            var data = await _repository.SingleOrDefaultAsync(q => q.Id == locationModel.Id);
            if (data == null)
            {
                throw new Exception("پست مورد نظر یافت نشد");
            }
            if (data.Title != locationModel.Title)
            {
                if (query != null)
                {
                    throw new Exception("عنوان تکراری است");
                }
            }
            data.Title = locationModel.Title;
            _repository.Update(data);
            await _repository.SaveChangesAsync();
        }

        public async Task<ServiceLocationModel> FindAsync(long id)
        {
            var result = await _repository.SingleOrDefaultAsync(q => q.Id == id);
            if (result == null)
            {
                throw new Exception("عنوان مورد نظر یافت نشد");
            }
            return result;
        }

        public async Task DeleteAllAsync(List<long> ids)
        {
            try
            {
                _repository.Delete(q => ids.Contains(q.Id));
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("اطلاعات به علت خطا ثبت نشد");
            }

        }

        public async Task DeleteAsync(ServiceLocationModel locationModel)
        {
            _repository.Delete(locationModel);
            await _repository.SaveChangesAsync();
        }

        public void Dispose()
        {
            _repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
