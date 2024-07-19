using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.IRepos
{
    public interface IGenericRepo<TData> where TData : class
    {
        public Task<DataResponse<TData>> GetAsync(int id);
        public Task<DataResponse<TData>> UpdateAsync(int id , Func<TData, TData> updateRecord);
        public Task<DataResponse<TData>> AddAsync(TData data);
        public Task<DataResponse<TData>> DeleteAsync(int id);
        public Task<DataResponse<List<TData>>> GetAllAsync();
    }
}
