using Microsoft.Extensions.Caching.Memory;
using RestCore.Models.Config;
using RestCore.Models.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace RestCore.Models.Users
{
    public class UserManager
    {
        private MemoryManager memoryManager;

        public UserManager(IMemoryCache memoryCache)
        {
            memoryManager = new MemoryManager(memoryCache);
            if (GetAll() == null)
            {
                InitializeUsers();
            }
        }

        public List<UserModel> GetAll()
        {
            return (List<UserModel>)memoryManager.Get(ConfigOptions.PersistenceKey);
        }

        public UserModel GetFirstId(int id)
        {
            return GetAll().Where((p) => p.Id == id).FirstOrDefault();
        }

        public UserModel GetLastId(int id)
        {
            return GetAll().Where((p) => p.Id == id).LastOrDefault();
        }

        public bool Exist(int id)
        {
            if (GetFirstId(id) == null)
                return false;
            else
                return true;
        }

        public int Add(string Name, string BirthDate)
        {
            UserModel user = new UserModel(Name, BirthDate);
            List<UserModel> userList = GetAll();
            userList.Add(user);
            memoryManager.Set(userList, ConfigOptions.PersistenceKey);
            return user.Id;
        }

        public void Update(UserModel user)
        {
            List<UserModel> userList = GetAll();
            foreach (var userAux in userList.Where(userAux => userAux.Id == user.Id))
            {
                userAux.Name = user.Name;
                userAux.BirthDate = user.BirthDate;
            }
            memoryManager.Set(userList, ConfigOptions.PersistenceKey);
        }

        public void Delete(int id)
        {
            List<UserModel> userList = GetAll();
            userList = userList.Where(u => u.Id != id).ToList();
            memoryManager.Set(userList, ConfigOptions.PersistenceKey);
        }

        private void InitializeUsers()
        {
            List<UserModel> userList = new List<UserModel>
                 {
                    new UserModel(1,"Paco de Lucia", "09/06/2000"),
                    new UserModel(2,"Cristiano Ronaldo", "19/10/2010" ),
                    new UserModel(3, "Luis",  "08/08/1987")
                 };
            memoryManager.Set(userList, ConfigOptions.PersistenceKey);
        }
    }
}
