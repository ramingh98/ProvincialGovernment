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
    public class EducationDegreeComponent : IDisposable
    {
        private readonly Repository<EducationDegreeModel> _educationDegreeRepository;

        public EducationDegreeComponent()
        {
            _educationDegreeRepository = new Repository<EducationDegreeModel>();
        }

        public IQueryable<EducationDegreeModel> Read()
        {
            var result = _educationDegreeRepository.SelectAllAsQuerable();
            return result;
        }

        public async Task AddAsync(EducationDegreeModel degreeModel)
        {
            var query = _educationDegreeRepository.FirstOrDefaultAsync(q => q.Title == degreeModel.Title);
            if (query != null)
            {
                throw new Exception("عنوان تکراری است");
            }
            await _educationDegreeRepository.AddAsync(degreeModel);
            await _educationDegreeRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EducationDegreeModel degreeModel)
        {
            var query = await _educationDegreeRepository.FirstOrDefaultAsync(q => q.Title == degreeModel.Title);
            var data = await _educationDegreeRepository.SingleOrDefaultAsync(q => q.Id == degreeModel.Id);
            if (data == null)
            {
                throw new Exception("پست مورد نظر یافت نشد");
            }
            if (data.Title != degreeModel.Title)
            {
                if (query != null)
                {
                    throw new Exception("عنوان تکراری است");
                }
            }
            data.Title = degreeModel.Title;
            _educationDegreeRepository.Update(data);
            await _educationDegreeRepository.SaveChangesAsync();
        }

        public async Task<EducationDegreeModel> FindAsync(long id)
        {
            var result = await _educationDegreeRepository.SingleOrDefaultAsync(q => q.Id == id);
            if (result == null)
            {
                throw new Exception("پست مورد نظر یافت نشد");
            }
            return result;
        }

        public async Task DeleteAllAsync(List<long> ids)
        {
            try
            {
                _educationDegreeRepository.Delete(q => ids.Contains(q.Id));
                await _educationDegreeRepository.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("اطلاعات به علت خطا ثبت نشد");
            }

        }

        public async Task DeleteAsync(EducationDegreeModel degreeModel)
        {
            _educationDegreeRepository.Delete(degreeModel);
            await _educationDegreeRepository.SaveChangesAsync();
        }

        public void Dispose()
        {
            _educationDegreeRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
