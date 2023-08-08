using Microsoft.EntityFrameworkCore;
using PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents;
using PanelViewModel.BaseDefinitionsViewModels;

namespace PanelBusinessLogicLayer.BusinessServices.BaseDefinitionsServices
{
    public class EducationDegreeService : IDisposable
    {
        private readonly EducationDegreeComponent _educationDegreeComponent;

        public EducationDegreeService()
        {
            _educationDegreeComponent = new EducationDegreeComponent();
        }

        public async Task<List<EducationDegreeViewModel>> ReadAsync()
        {
            var result = _educationDegreeComponent.Read();
            var viewModel = await result.Select(q => new EducationDegreeViewModel
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
