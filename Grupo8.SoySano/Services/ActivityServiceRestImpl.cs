using System;
using System.Collections.Generic;
using System.Text;
using Grupo8.SoySano.Api;
using Grupo8.SoySano.Models;
using Grupo8.SoySano.Utils;
using Newtonsoft.Json;

namespace Grupo8.SoySano.Services
{
    public class ActivityServiceRestImpl : ActivityService
    {
        private RestExecutor executor = new RestExecutor();
        public List<Activity> GetAll(User user)
        {
            string url = string.Format("{0}/users/{1}/activities", Constant.Services.SERVICE_BASE_URL, user.Id);
            string response = this.executor.Execute(url, Constant.Method.GET);
            List<Activity> entities = JsonConvert.DeserializeObject<List<Activity>>(response);
            return entities;
        }

        public Activity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Activity Create(Activity entity)
        {
            throw new NotImplementedException();
        }

        public Activity Update(Activity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Activity entity)
        {
            throw new NotImplementedException();
        }
    }
}
