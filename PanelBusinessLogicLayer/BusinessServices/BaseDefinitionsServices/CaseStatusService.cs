using DataAccessLayer.Repositories;
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
    public class CaseStatusService : IDisposable
    {
        private readonly CaseStatusComponent _caseStatusComponent;

        public CaseStatusService()
        {
            _caseStatusComponent = new CaseStatusComponent();
        }

        public async Task<List<CaseStatusViewModel>> ReadAsync()
        {
            var result = _caseStatusComponent.Read();
            var viewModel = await result.Select(q => new CaseStatusViewModel
            {
                Id = q.Id,
                RegDate = q.RegDate,
                Title = q.Title
            }).ToListAsync();
            return viewModel;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
