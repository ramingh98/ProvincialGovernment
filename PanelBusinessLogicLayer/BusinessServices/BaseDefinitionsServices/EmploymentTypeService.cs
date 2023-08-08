using Common;
using Common.Objects;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents;
using PanelBusinessLogicLayer.BusinessComponents.IdentitiesComponents;
using PanelViewModel.BaseDefinitionsViewModels;
using PanelViewModel.IdentitiesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelBusinessLogicLayer.BusinessServices.BaseDefinitionsServices
{
    public class EmploymentTypeService : IDisposable
    {
        private readonly EmploymentTypeComponent _employmentTypeComponent;

        public EmploymentTypeService()
        {
            _employmentTypeComponent = new EmploymentTypeComponent();
        }

        public async Task<List<EmploymentTypeViewModel>> ReadAsync()
        {
            var result = _employmentTypeComponent.Read();
            var viewModel = await result.Select(q => new EmploymentTypeViewModel
            {
                Id = q.Id,
                RegDate = q.RegDate,
                Title = q.Title
            }).ToListAsync();
            return viewModel;
        }

        public async Task<OperationResult> AddAsync(EmploymentTypeViewModel viewModel)
        {
            try
            {
                var model = new TypeOfEmploymentModel
                {
                    Title = viewModel.Title
                };
                await _employmentTypeComponent.AddAsync(model);
                return OperationResult.Success();
            }
            catch
            {
                return OperationResult.Error("مشکلی در عملیات پیش آمده");
            }
        }

        public async Task<OperationResult> UpdateAsync(EmploymentTypeViewModel viewModel)
        {
            try
            {
                var model = new TypeOfEmploymentModel()
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title
                };
                await _employmentTypeComponent.UpdateAsync(model);
                return OperationResult.Success();
            }
            catch
            {
                return OperationResult.Error("مشکلی در عملیات پیش آمده");
            }
        }

        public async Task<SysResult<EmploymentTypeViewModel>> FindAsync(long id)
        {
            try
            {
                var result = await _employmentTypeComponent.FindByIdAsync(id);
                if (result == null)
                    return new SysResult<EmploymentTypeViewModel>() { IsSuccess = true, Message = "نام کاربری یافت نشد", Value = null };
                //**********************************************  عملیات نگاشت
                var viewModel = new EmploymentTypeViewModel()
                {
                    Id = result.Id,
                    Title = result.Title,
                    RegDate = result.RegDate
                };
                //**********************************************
                return new SysResult<EmploymentTypeViewModel>() { IsSuccess = true, Message = "اطلاعات با موفقیت واکشی شد", Value = viewModel };
            }
            catch (Exception m)
            {
                return new SysResult<EmploymentTypeViewModel>() { IsSuccess = false, Message = m.Message };
            }
        }

        public async Task<SysResult> DeleteAllAsync(List<EmploymentTypeViewModel> employmentTypes)
        {
            //**********************************************  عملیات نگاشت
            var ids = employmentTypes.Select(r => r.Id).ToList();
            await _employmentTypeComponent.DeleteAllAsync(ids);
            //**********************************************
            return new SysResult() { IsSuccess = true, Message = "عملیات حذف با موفقیت انجام شد" };

        }

        public async Task<OperationResult> DeleteAsync(long id)
        {
            var model = await _employmentTypeComponent.FindByIdAsync(id);
            await _employmentTypeComponent.DeleteAsync(model);
            return OperationResult.Success();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
