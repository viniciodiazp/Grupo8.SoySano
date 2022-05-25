using System;
using System.Collections.Generic;
using System.Text;
using Grupo8.SoySano.Models;

namespace Grupo8.SoySano.Api
{
    public interface ActivityService
    {
        List<Activity> GetAll(User user);
        Activity GetById(int id);
        Activity Create(Activity entity);
        Activity Update(Activity entity);
        bool Delete(Activity entity);
    }
}
