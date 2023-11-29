using interval_recall.BLL.DTOs;
using interval_recall.DAL.EF;
using interval_recall.DAL.Entities;

namespace interval_recall.BLL.Services
{
    public class UserService
    {
        private readonly IntervaRecallContext _dataContext;
        public UserService(IntervaRecallContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateAsync(UserDTO userDTO)
        {
            _dataContext.Users.Add(new User()
            {
                UserName = userDTO.UserName,
                UserGroupId = userDTO.UserGroupId
            });
            await _dataContext.SaveChangesAsync();
        }
        public async Task CreateUserGroup(UserGroupDTO userGroupDTO)
        {
            _dataContext.UserGroups.Add(new UserGroup()
            {
                Title = userGroupDTO.Title
            });
            await _dataContext.SaveChangesAsync();
        }
    }
}
