using Common;
using Common.Objects;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents;
using PanelViewModel.BaseDefinitionsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelBusinessLogicLayer.BusinessServices.BaseDefinitionsServices
{
    public class ServiceLocationService : IDisposable
    {
        private readonly ServiceLocationComponent _locationComponent;

        public ServiceLocationService()
        {
            _locationComponent = new ServiceLocationComponent();
        }

        public async Task<List<ServiceLocationViewModel>> ReadAsync()
        {
            var result = _locationComponent.Read();
            var viewModel = await result.Select(q => new ServiceLocationViewModel
            {
                Id = q.Id,
                Title = q.Title,
                RegDate = q.RegDate
            }).ToListAsync();
            return viewModel;
        }

        public async Task<ServiceLocationViewModel> FindAsync(long id)
        {
            var result = await _locationComponent.FindAsync(id);
            var viewModel = new ServiceLocationViewModel
            {
                Id = result.Id,
                RegDate = result.RegDate,
                Title = result.Title
            };
            return viewModel;
        }

        public async Task<OperationResult> AddAsync(ServiceLocationViewModel viewModel)
        {
            var locationModel = new ServiceLocationModel()
            {
                Title = viewModel.Title
            };
            await _locationComponent.AddAsync(locationModel);
            return OperationResult.Success();
        }

        public async Task<OperationResult> UpdateAsync(ServiceLocationViewModel viewModel)
        {
            var model = new ServiceLocationModel()
            {
                Id = viewModel.Id,
                Title = viewModel.Title
            };

            await _locationComponent.UpdateAsync(model);
            return OperationResult.Success();
        }

        public async Task<OperationResult> DeleteAsync(long id)
        {
            var model = await _locationComponent.FindAsync(id);
            await _locationComponent.DeleteAsync(model);
            return OperationResult.Success();
        }

        public async Task<SysResult> DeleteAllAsync(List<ServiceLocationViewModel> viewModels)
        {
            var ids = viewModels.Select(q => q.Id).ToList();
            await _locationComponent.DeleteAllAsync(ids);
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
