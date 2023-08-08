using Common;
using Common.Objects;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents;
using PanelViewModel.BaseDefinitionsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PanelBusinessLogicLayer.BusinessServices.BaseDefinitionsServices
{
    public class PositionTypeService : IDisposable
    {
        private readonly PositionTypeComponent _positionComponent;

        public PositionTypeService()
        {
            _positionComponent = new PositionTypeComponent();
        }

        public async Task<SysResult<List<PositionTypeViewModel>>> ReadAsync()
        {
            var result = _positionComponent.Read();
            var viewModel = await result.Select(q => new PositionTypeViewModel()
            {
                Id = q.Id,
                Title = q.Title,
                RegDate = q.RegDate
            }).ToListAsync();
            return new SysResult<List<PositionTypeViewModel>>() { IsSuccess = true, Message = "اطلاعات با موفقیت واکشی شد", Value = viewModel };
        }

        public async Task<OperationResult> AddAsync(PositionTypeViewModel viewModel)
        {
            var positionModel = new TypeOfPositionModel()
            {
                Title = viewModel.Title
            };
            await _positionComponent.AddAsync(positionModel);
            return OperationResult.Success();
        }

        public async Task<OperationResult> UpdateAsync(PositionTypeViewModel viewModel)
        {
            var model = new TypeOfPositionModel()
            {
                Id = viewModel.Id,
                Title = viewModel.Title
            };

            await _positionComponent.UpdateAsync(model);
            return OperationResult.Success();
        }

        public async Task<OperationResult> DeleteAsync(long id)
        {
            var model = await _positionComponent.FindAsync(id);
            await _positionComponent.DeleteAsync(model);
            return OperationResult.Success();
        }

        public async Task<SysResult> DeleteAllAsync(List<PositionTypeViewModel> viewModels)
        {
            var ids = viewModels.Select(q => q.Id).ToList();
            await _positionComponent.DeleteAllAsync(ids);
            return new SysResult()
            {
                IsSuccess = true,
                Message = SystemCommonMessage.InformationWasSuccessfullyDeleted
            };
        }

        public async Task<SysResult<PositionTypeViewModel>> FindAsync(long id)
        {
            var result = await _positionComponent.FindAsync(id);
            var model = new PositionTypeViewModel()
            {
                Id = result.Id,
                Title = result.Title
            };
            return new SysResult<PositionTypeViewModel>()
            {
                IsSuccess = true,
                Message = SystemCommonMessage.InformationFetchedSuccessfully,
                Value = model
            };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
