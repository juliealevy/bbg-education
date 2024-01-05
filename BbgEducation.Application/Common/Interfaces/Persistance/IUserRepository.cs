using BbgEducation.Domain.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);

    void ClearAllUsers();  //for development
    //todo... changing email, password, etc.
}
