using Common;
using Common.Objects;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents;
using PanelViewModel.BaseDefinitionsViewModels;

namespace PanelBusinessLogicLayer.BusinessServices.BaseDefinitionsServices
{
    public class StudyFieldService : IDisposable
    {
        private readonly StudyFieldComponent _studyFieldComponent;

        public StudyFieldService()
        {
            _studyFieldComponent = new StudyFieldComponent();
        }

        public async Task<List<StudyFieldViewModel>> ReadAsync()
        {
            var result = _studyFieldComponent.Read();
            var viewModel = await result.Select(q => new StudyFieldViewModel
            {
                Id = q.Id,
                RegDate = q.RegDate,
                Title = q.Title
            }).ToListAsync();
            return viewModel;
        }

        public async Task<OperationResult> AddAsync(StudyFieldViewModel viewModel)
        {
            var studyField = new StudyFieldModel()
            {
                Title = viewModel.Title
            };
            await _studyFieldComponent.AddAsync(studyField);
            return OperationResult.Success();
        }

        public async Task<OperationResult> UpdateAsync(StudyFieldViewModel viewModel)
        {
            var studyField = new StudyFieldModel()
            {
                Id = viewModel.Id,
                Title = viewModel.Title
            };

            await _studyFieldComponent.UpdateAsync(studyField);
            return OperationResult.Success();
        }

        public async Task<SysResult<StudyFieldViewModel>> FindAsync(long id)
        {
            try
            {
                var result = await _studyFieldComponent.FindAsync(id);
                if (result == null)
                    return new SysResult<StudyFieldViewModel>() { IsSuccess = true, Message = "نام کاربری یافت نشد", Value = null };
                //**********************************************  عملیات نگاشت
                var viewModel = new StudyFieldViewModel()
                {
                    Id = result.Id,
                    Title = result.Title,
                    RegDate = result.RegDate
                };
                //**********************************************
                return new SysResult<StudyFieldViewModel>() { IsSuccess = true, Message = "اطلاعات با موفقیت واکشی شد", Value = viewModel };
            }
            catch (Exception m)
            {
                return new SysResult<StudyFieldViewModel>() { IsSuccess = false, Message = m.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(long id)
        {
            var model = await _studyFieldComponent.FindAsync(id);
            await _studyFieldComponent.DeleteAsync(model);
            return OperationResult.Success();
        }

        public async Task<SysResult> DeleteAllAsync(List<StudyFieldViewModel> viewModels)
        {
            var ids = viewModels.Select(q => q.Id).ToList();
            await _studyFieldComponent.DeleteAllAsync(ids);
            return new SysResult()
            {
                IsSuccess = true,
                Message = SystemCommonMessage.InformationWasSuccessfullyDeleted
            };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
