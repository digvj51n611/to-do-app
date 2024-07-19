using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
    public class GenericRepo<TData> : IGenericRepo<TData> where TData : BaseEntity
    {
        private readonly ToDoDbContext _context;
        public GenericRepo(ToDoDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<DataResponse<TData>> GetAsync(int id)
        {
            using (var context = _context)
            {
                try
                {
                    var item = await context.Set<TData>().FindAsync(id);
                    if (item == null)
                    {
                        string message = $"Data with id : {id} Not found";
                        return DataResponse<TData>.FailureResult(ErrorType.NotFoundError, message);
                    }
                    return DataResponse<TData>.SuccessResult(item);
                }
                catch (Exception ex)
                {
                    return DataResponse<TData>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<List<TData>>> GetAllAsync()
        {
            using (var context = _context)
            {
                try
                {
                    var result = await context.Set<TData>().ToListAsync<TData>();
                    if (result.Any())
                    {

                        return DataResponse<List<TData>>.SuccessResult(result);
                    }
                    string message = "Tasks not found";
                    return DataResponse<List<TData>>.FailureResult(ErrorType.NotFoundError, message);
                }
                catch (Exception ex)
                {
                    return DataResponse<List<TData>>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<TData>> UpdateAsync(int id ,Func<TData,TData> updateRecord)
        {
            using (var context = _context)
            {
                try
                {
                    var dataEntry = await context.Set<TData>().FindAsync(id);
                    if (dataEntry == null)
                    {
                        string message = $"Data Not Found with id:{id}";
                        return DataResponse<TData>.FailureResult(ErrorType.NotFoundError, message);
                    }
                    dataEntry = updateRecord.Invoke(dataEntry);
                    context.Update(dataEntry);
                    if (await context.SaveChangesAsync() > 0)
                    {
                        return DataResponse<TData>.SuccessResult(dataEntry);
                    }
                    return DataResponse<TData>.FailureResult(ErrorType.SaveChangesError, "Save Changes Failed");
                }
                catch (Exception ex)
                {
                    return DataResponse<TData>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<TData>> AddAsync(TData data)
        {
            using (var context = _context)
            {
                try
                {
                    context.Set<TData>().Add(data);
                    try
                    {
                        if (await context.SaveChangesAsync() > 0)
                            return DataResponse<TData>.SuccessResult(data);
                        string message = "Save Changes failed";
                        return DataResponse<TData>.FailureResult(ErrorType.SaveChangesError, message);
                    }
                    catch (DbUpdateException ex)
                    {
                        return DataResponse<TData>.FailureResult(ex);
                    }

                }
                catch (Exception ex)
                {
                    return DataResponse<TData>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<TData>> DeleteAsync(int id)
        {
            using (var context = _context)
            {
                try
                {
                    var data = await _context.Set<TData>().FindAsync(id);
                    if (data == null)
                    {
                        string message = $"Data with id:{id} Not Found";
                        return DataResponse<TData>.FailureResult(ErrorType.NotFoundError, message);
                    }
                    context.Remove(data);
                    if (await context.SaveChangesAsync() > 0)
                    {
                        return DataResponse<TData>.SuccessResult(data);
                    }
                    return DataResponse<TData>.FailureResult(ErrorType.SaveChangesError, "Save Changes Failed");
                }
                catch (Exception ex)
                {
                    return DataResponse<TData>.FailureResult(ex);
                }
            }
        }
    }
}
