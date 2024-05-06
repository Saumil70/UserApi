using Microsoft.EntityFrameworkCore;


namespace  User_UserApi.Models
{
    public class UserEntites : DbContext

    {
        public UserEntites(DbContextOptions<UserEntites> options) : base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Countries> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
