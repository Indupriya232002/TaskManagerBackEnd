using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Interfaces
{
    public interface IUserRepo
    {
        string Login(Login login);
        object SignUp(Users users);
       // object Logout(Users users); 
    }
}
