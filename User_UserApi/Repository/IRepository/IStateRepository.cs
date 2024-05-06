



using User_UserApi.Models;

namespace User_UserApi.Repository.IRepository
{
    public interface IStateRepository : IRepository<State>
    {
        void Update(State obj);


    }
}
