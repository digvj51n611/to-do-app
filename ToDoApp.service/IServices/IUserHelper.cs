using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service.IServices
{
    public interface IUserHelper
    {
        public Task<int> GetIdFromUsername(string username);   
    }
}
