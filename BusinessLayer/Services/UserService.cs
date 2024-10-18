using EntityLayer.Interfaces;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public  class UserService
    {
        IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public string Login(Login login)
        {
            return _userRepo.Login(login);
        }

        public object SignUp(Users users)
        {
            return _userRepo.SignUp(users);
        }
    }
}
