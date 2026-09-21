using CarMaintenance.Application.Abstractions;
using CarMaintenance.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Services.Interfaces
{
    public interface IGenericReposatory<T> where T : BaseEntity
    {
        Task<Result<IEnumerable<T>>> GetAllAsync();
        Task<Result<T>> GetById(int id);
        Task<Result> AddAsync(T value);
        Task<Result> UpdateAsync(int id ,  T value);
        Task<Result> DeleteAsync(int id);
    }
}
