using WebApplication5.Exceptions;
using WebApplication5.Models;
using WebApplication5.Services.Repository;
using WebApplication5.Services.Repository.UserGroupsRepository;

namespace WebApplication5.Services.GroupManager
{
    public class GroupManager : IGroupManager
    {
        private readonly IUserGroupsRepository _repository;
        private readonly IGroupRepository _groupRepository;

        public GroupManager(IUserGroupsRepository rep, IGroupRepository groupRep) { _repository = rep; _groupRepository = groupRep; }
        public async Task AddUserToGroup(int groupId, int userId)
        {
            var userGroupFromRepo = await _repository.GetAsync(userId, groupId);
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            if (userGroupFromRepo is not null)
            {
                if (group is not null)
                    throw new GroupManagerException("Вы уже состоите в группе");
                throw new GroupManagerException("Такой группы не существует");
            }
            var userGroup = new UsersGroup() { UserId = userId, GroupId = groupId };
            await _repository.AddAsync(userGroup);
            await _repository.SaveChangesAsync();

        }

        public async Task DeleteUserFromGroup(int groupId, int userId)
        {
            var userGroup = await _repository.GetAsync(userId, groupId) ?? throw new GroupManagerException("Вы не состоите в данной группе");
            await _repository.DeleteAsync(userGroup);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateGroupSettings(GroupSettings newSettings, int groupId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId) ?? throw new GroupManagerException("Нельзя обновить данные несуществующей группы");
            group.Settings = newSettings;
            await _repository.SaveChangesAsync();
        }
    }
}
