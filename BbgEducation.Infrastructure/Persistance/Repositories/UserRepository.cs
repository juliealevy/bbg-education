using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Infrastructure.Persistance.Repositories;
public class UserRepository : IUserRepository
{
    //ToDo: move to database
    private static readonly List<User> _users = new List<User>();
    public void Add(User user) {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email) {
        return _users.SingleOrDefault(u => u.Email == email);
    }
}
