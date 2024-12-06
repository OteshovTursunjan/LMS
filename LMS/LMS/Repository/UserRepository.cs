using LMS.Context;
using LMS.Repository;
using LMS.Models;
using LMS.Models;
using LMS.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _appDbContext;
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

}
