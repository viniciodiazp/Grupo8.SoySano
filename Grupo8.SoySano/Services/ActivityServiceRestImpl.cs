using Grupo8.SoySano.Api;
using Grupo8.SoySano.Models;
using Grupo8.SoySano.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
            
            string url = string.Format("{0}/users/{1}/activities/{2}",
                Constant.Services.SERVICE_BASE_URL, AppHelpers.CurrentUser.Id, id);
            string response = this.executor.Execute(url, Constant.Method.GET);
            Activity entity = JsonConvert.DeserializeObject<Activity>(response);
            return entity;
        }

        public Activity Create(Activity entity)
        {
            string url = string.Format("{0}/users/{1}/activities",
                Constant.Services.SERVICE_BASE_URL, entity.User.Id);
            string payload = JsonConvert.SerializeObject(entity);
            string response = this.executor.Execute(url, Constant.Method.POST, payload);
            Activity repsonseEntity = JsonConvert.DeserializeObject<Activity>(response);
            return repsonseEntity;
        }

        public Activity Update(Activity entity)
        {
            string url = string.Format("{0}/users/{1}/activities/{2}",
                Constant.Services.SERVICE_BASE_URL, entity.User.Id, entity.Id);
            string payload = JsonConvert.SerializeObject(entity);
            string response = this.executor.Execute(url, Constant.Method.POST, payload);
            Activity repsonseEntity = JsonConvert.DeserializeObject<Activity>(response);
            return repsonseEntity;
        }

        public bool Delete(Activity entity)
        {
            try
            {
                string url = string.Format("{0}/users/{1}/activities/{2}", 
                    Constant.Services.SERVICE_BASE_URL, entity.User.Id, entity.Id);
                string response = this.executor.Execute(url, Constant.Method.DELETE);
                return true;
            }
            catch (Exception exception) { 
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}
