
using User_UserApi.Models;

namespace User_UserApi.Repository.IRepository
{
    public interface ICityRepository : IRepository<City>
    {
        void Update(City obj);
      


    }
}
