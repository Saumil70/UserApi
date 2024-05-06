



using User_UserApi.Models;

namespace User_UserApi.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {


        IEnumerable<User> UserIndex();
        void UserAdd(User obj);
        void Update(User obj);
    

    }
}
