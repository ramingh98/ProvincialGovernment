using Common;
using DataAccessLayer.Repositories;
using DataModels.Models;

namespace PanelBusinessLogicLayer.BusinessComponents.IdentitiesComponents
{
    public class UserComponent
    {
        private readonly Repository<UsersModel> _userRepository;

        public UserComponent()
        {
            _userRepository = new Repository<UsersModel>();
        }

        public async Task<UsersModel?> FindAsync(long id)
        {
            var result = await _userRepository.FirstOrDefaultAsync(q => q.Id == id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<UsersModel?> FindByUserNameAsync(string userName)
        {
            var result = await _userRepository.SingleOrDefaultAsync(q => q.UserName == userName);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<OperationResult> ChangePasswordAsync(long id, string password)
        {
            var result = await _userRepository.SingleOrDefaultAsync(q => q.Id == id);
            if (result == null)
            {
                return OperationResult.NotFound("کاربر یافت نشد");
            }
            result.Password = password;
            _userRepository.Update(result);
            await _userRepository.SaveChangesAsync();
            return OperationResult.Success();
        }
    }
}
