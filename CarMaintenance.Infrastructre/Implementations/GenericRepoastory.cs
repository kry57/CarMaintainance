using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.ErrorProvider.DbErrorProvider;
using CarMaintenance.Application.Services.Interfaces;
using CarMaintenance.Infrastructre.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CarMaintenance.Application.Services.Implementations
{
    public class GenericRepoastory<T>(ApplicationDbContext context) : IGenericReposatory<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result> AddAsync(T value)
        {
            await _context.Set<T>().AddAsync(value);
            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows > 0)
                return Result.Success();
            return Result.Failure(DbError.NotAdded);
        }

        public async Task<Result> DeleteAsync(int id)
        {

            if (await _context.Set<T>().FindAsync(id) is T result)
            {
                _context.Set<T>().Remove(result);
                var affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                    return Result.Success();

            }
            return Result.Failure(DbError.NotRemoved);
        }

        public async Task<Result<IEnumerable<T>>> GetAllAsync()
        {
            return Result<IEnumerable<T>>.Success(await _context.Set<T>().AsNoTracking().ToListAsync());
        }

        public async Task<Result<T>> GetById(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result is null)
                return Result<T>.Failure(DbError.NotFound);
            return Result<T>.Success(result);
        }

        public async Task<Result> UpdateAsync(int id, T value)
        {
            var exists = await _context.Set<T>().AnyAsync(e => e.Id == id);
            if (!exists)
                return Result.Failure(DbError.NotFound);
            _context.Set<T>().Update(value);
            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows > 0)
                return Result.Success();
            return Result.Failure(DbError.NotUpdated);

        }

    }
}
