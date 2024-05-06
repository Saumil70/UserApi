





using User_UserApi.Models;

namespace User_UserApi.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Countries>
    {
       void Update(Countries obj);

    }
}
