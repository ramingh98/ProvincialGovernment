using Common;
using DataAccessLayer.Repositories;
using DataModels.Models;

namespace PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents
{
    public class StudyFieldComponent : IDisposable
    {
        private readonly Repository<StudyFieldModel> _studyFieldRepository;

        public StudyFieldComponent()
        {
            _studyFieldRepository = new Repository<StudyFieldModel>();
        }

        public IQueryable<StudyFieldModel> Read()
        {
            var result = _studyFieldRepository.SelectAllAsQuerable();
            return result;
        }

        public async Task AddAsync(StudyFieldModel studyField)
        {
            var query = await _studyFieldRepository.FirstOrDefaultAsync(q => q.Title == studyField.Title);
            if (query != null)
            {
                throw new Exception("عنوان تکراری است");
            }
            await _studyFieldRepository.AddAsync(studyField);
            await _studyFieldRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(StudyFieldModel studyField)
        {
            var data = await _studyFieldRepository.SingleOrDefaultAsync(q => q.Id == studyField.Id);
            if (data == null)
            {
                throw new Exception("عنوان مورد نظر یافت نشد");
            }
            data.Title = studyField.Title;
            _studyFieldRepository.Update(data);
            await _studyFieldRepository.SaveChangesAsync();
        }

        public async Task<StudyFieldModel> FindAsync(long id)
        {
            var result = await _studyFieldRepository.SingleOrDefaultAsync(q => q.Id == id);
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
                _studyFieldRepository.Delete(q => ids.Contains(q.Id));
                await _studyFieldRepository.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("اطلاعات به علت خطا ثبت نشد");
            }

        }

        public async Task DeleteAsync(StudyFieldModel studyField)
        {
            _studyFieldRepository.Delete(studyField);
            await _studyFieldRepository.SaveChangesAsync();
        }

        public void Dispose()
        {
            _studyFieldRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
