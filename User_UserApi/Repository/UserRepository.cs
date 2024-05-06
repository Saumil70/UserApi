﻿using Microsoft.EntityFrameworkCore;
using User_UserApi.Models;
using User_UserApi.Repository.IRepository;



namespace User_UserApi.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private UserEntites _db;
        public UserRepository(UserEntites db) : base(db)
        {
            _db = db;
        }

        public void UserAdd(User obj)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<User> UserIndex()
        {
           var user=_db.Users.Include(u=> u.Country).Include(u=> u.States).Include(u=> u.Cities).ToList();
            return user;    
        }

        public void Update(User obj)
        {
            _db.Update(obj);
        }


    }
}
