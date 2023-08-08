using DataAccessLayer.Repositories;
using DataModels.Models;
using PanelViewModel.BaseDefinitionsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents
{
    public class EmploymentTypeComponent : IDisposable
    {
        private readonly Repository<TypeOfEmploymentModel> _repository;

        public EmploymentTypeComponent()
        {
            _repository = new Repository<TypeOfEmploymentModel>();
        }

        public IQueryable<TypeOfEmploymentModel> Read()
        {
            var result = _repository.SelectAllAsQuerable();
            return result;
        }

        public async Task AddAsync(TypeOfEmploymentModel employmentModel)
        {
            var query = await _repository.FirstOrDefaultAsync(q => q.Title == employmentModel.Title);
            if (query != null)
            {
                throw new Exception("عنوان تکراری است");
            }

            var model = new TypeOfEmploymentModel
            {
                Title = employmentModel.Title
            };

            await _repository.AddAsync(model);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(TypeOfEmploymentModel employmentModel)
        {
            var query = await _repository.FirstOrDefaultAsync(q => q.Title == employmentModel.Title);
            var result = await _repository.SingleOrDefaultAsync(q => q.Id == employmentModel.Id);
            if (result.Title != employmentModel.Title)
            {
                if (query != null)
                {
                    throw new Exception("عنوان تکراری است");
                }
            }

            result.Title = employmentModel.Title;
            _repository.Update(result);
            await _repository.SaveChangesAsync();
        }

        public async Task<TypeOfEmploymentModel> FindByIdAsync(long Id)
        {
            var result = await _repository.SingleOrDefaultAsync(q => q.Id == Id);
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
                throw new Exception("اطلاعات به علت خطا حذف نشد");
            }
        }

        public async Task DeleteAsync(TypeOfEmploymentModel personnelsModel)
        {
            _repository.Delete(personnelsModel);
            await _repository.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
