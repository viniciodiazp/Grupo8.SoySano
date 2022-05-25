using System;
using System.Collections.Generic;
using System.Text;
using Grupo8.SoySano.Api;
using Grupo8.SoySano.Models;
using Grupo8.SoySano.Utils;
using Newtonsoft.Json;

namespace Grupo8.SoySano.Services
{
    public class UserServiceRestImpl : UserService
    {
        private RestExecutor executor = new RestExecutor();

        public List<User> GetAll()
        {
            string url = string.Format("{0}/users", Constant.Services.SERVICE_BASE_URL);
            string response = this.executor.Execute(url, Constant.Method.GET);
            List<User> entities = JsonConvert.DeserializeObject<List<User>>(response);
            return entities;
        }

        public User GetById(int id)
        {
            string url = string.Format("{0}/users/{1}", Constant.Services.SERVICE_BASE_URL, id);
            string response = this.executor.Execute(url, Constant.Method.GET);
            User entity = JsonConvert.DeserializeObject<User>(response);
            return entity;
        }
        public User Create(User entity)
        {
            string url = string.Format("{0}/users", Constant.Services.SERVICE_BASE_URL);
            string payload = JsonConvert.SerializeObject(entity);
            string response = this.executor.Execute(url, Constant.Method.POST, payload);
            User repsonseEntity = JsonConvert.DeserializeObject<User>(response);
            return repsonseEntity;
        }
        public User Update(User entity)
        {
            string url = string.Format("{0}/users/{1}", Constant.Services.SERVICE_BASE_URL, entity.Id);
            string payload = JsonConvert.SerializeObject(entity);
            string response = this.executor.Execute(url, Constant.Method.PUT, payload);
            User repsonseEntity = JsonConvert.DeserializeObject<User>(response);
            return repsonseEntity;
        }

        public bool Delete(User entity)
        {
            try
            {
                string url = string.Format("{0}/users/{1}", Constant.Services.SERVICE_BASE_URL, entity.Id);
                string response = this.executor.Execute(url, Constant.Method.DELETE);
                return true;
            } catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        

        
    }
}
