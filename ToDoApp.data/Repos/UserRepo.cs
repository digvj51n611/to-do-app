using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.data;
using ToDoApp.Data.Entities;
using ToDoApp.Data.IRepos;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Repos
{
    public class UserRepo :GenericRepo<User>, IUserRepo
    {
        public UserRepo(ToDoDbContext context) : base(context)
        {
        }
    }
}
