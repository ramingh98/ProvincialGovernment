using Common;
using PanelBusinessLogicLayer.BusinessComponents.IdentitiesComponents;
using PanelViewModel.IdentitiesViewModels;

namespace PanelBusinessLogicLayer.BusinessServices.IdentitiesServices
{
    public class UserService
    {
        private readonly UserComponent _userComponent;

        public UserService()
        {
            _userComponent = new UserComponent();
        }

        public async Task<UserViewModel?> FindAsync(long id)
        {
            var model = await _userComponent.FindAsync(id);
            if (model == null)
            {
                return null;
            }
            return new UserViewModel
            {
                Id = model.Id,
                Password = model.Password,
                UserName = model.UserName
            };
        }

        public async Task<UserViewModel?> FindByUserNameAsync(string userName)
        {
            var model = await _userComponent.FindByUserNameAsync(userName);
            if (model == null)
            {
                return null;
            }
            return new UserViewModel
            {
                Id = model.Id,
                Password = model.Password,
                UserName = model.UserName
            };
        }

        public async Task<OperationResult> ChangePasswordAsync(long id, string password)
        {
            var result = await _userComponent.ChangePasswordAsync(id, password);
            return result;
        }
    }
}
