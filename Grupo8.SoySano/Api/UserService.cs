using System;
using System.Collections.Generic;
using System.Text;
using Grupo8.SoySano.Models;

namespace Grupo8.SoySano.Api
{
    public interface UserService
    {
        List<User> GetAll();
        User GetById(int id);
        User Create(User entity);
        User Update(User entity);
        bool Delete(User entity);
    }
}
