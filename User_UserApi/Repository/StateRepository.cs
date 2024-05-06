
using User_UserApi.Models;
using User_UserApi.Repository.IRepository;



namespace User_UserApi.Repository
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        private UserEntites _db;
        public StateRepository(UserEntites db) : base(db)
        {
            _db = db;
        }
        public void Update(State obj)
        {
            Update(obj);
        }

    }
}
