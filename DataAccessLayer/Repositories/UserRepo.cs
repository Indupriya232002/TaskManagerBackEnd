using DataAccessLayer.Data;
using EntityLayer.Interfaces;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepo : IUserRepo
    {
        TaskDbContext _dbcontext;

        public UserRepo(TaskDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public string Login(Login login)
        {
          var res=  _dbcontext.Users.Where(obj => obj.Email == login.Email && obj.password == login.password).ToList();
            
            if(res != null && res.Count > 0)
            {
                return "Login added successfully";

            }

            return "Invalid Username and Password";
        }

        //public object Logout(Users users)
        //{
        //    throw new NotImplementedException();
        //}

        public object SignUp(Users users)
        {
           _dbcontext.Users.Add(users);
            _dbcontext.SaveChanges();
            return "Account Created Successfully";
        }
    }
}
