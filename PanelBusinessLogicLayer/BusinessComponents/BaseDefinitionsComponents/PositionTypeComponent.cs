using DataAccessLayer.ApplicationDatabase;
using DataAccessLayer.Repositories;
using DataModels.Models;

namespace PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents
{
    public class PositionTypeComponent : IDisposable
    {
        private readonly Repository<TypeOfPositionModel> _positionRepository;

        public PositionTypeComponent()
        {
            _positionRepository = new Repository<TypeOfPositionModel>();
        }

        public IQueryable<TypeOfPositionModel> Read()
        {
            var result = _positionRepository.SelectAllAsQuerable();
            return result;
        }

        public async Task AddAsync(TypeOfPositionModel positionModel)
        {
            var query = await _positionRepository.FirstOrDefaultAsync(q => q.Title == positionModel.Title);
            if (query != null)
            {
                throw new Exception("عنوان تکراری است");
            }
            await _positionRepository.AddAsync(positionModel);
            await _positionRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(TypeOfPositionModel positionModel)
        {
            var data = await _positionRepository.SingleOrDefaultAsync(q => q.Id == positionModel.Id);
            var query = await _positionRepository.FirstOrDefaultAsync(q => q.Title == positionModel.Title);

            if (data == null)
            {
                throw new Exception("پست مورد نظر یافت نشد");
            }
            if (data.Title != positionModel.Title)
            {
                if (query != null)
                {
                    throw new Exception("عنوان تکراری است");
                }
            }
            data.Title = positionModel.Title;
            _positionRepository.Update(data);
            await _positionRepository.SaveChangesAsync();
        }

        public async Task<TypeOfPositionModel> FindAsync(long id)
        {
            var result = await _positionRepository.SingleOrDefaultAsync(q => q.Id == id);
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
                _positionRepository.Delete(q => ids.Contains(q.Id));
                await _positionRepository.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("اطلاعات به علت خطا ثبت نشد");
            }

        }

        public async Task DeleteAsync(TypeOfPositionModel positionModel)
        {
            _positionRepository.Delete(positionModel);
            await _positionRepository.SaveChangesAsync();
        }

        public void Dispose()
        {
            _positionRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
