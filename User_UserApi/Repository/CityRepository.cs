
using User_UserApi.Models;
using User_UserApi.Repository.IRepository;

namespace User_UserApi.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private UserEntites _db;
        public CityRepository(UserEntites db) : base(db)
        {
            _db = db;
        }
        public void Update(City obj)
        {
            Update(obj);
        }



    }
}
