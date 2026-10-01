using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;


namespace CarMaintenance.Application.Services.Cars
{
    public interface ICarsRepository
    {
        Task<Result<CarResponse>> AddAsync(CarRequest request);
        Task<Result> DeleteAsync(int carId);
        Task<Result> ToggleStatus(int carId);
        Task<Result<CarResponse>> GetByIdAsync(int carId);
        Task<Result<IEnumerable<CustomerCarResponse>>> GetAllAsyncPerCustomer();
        Task<Result<IEnumerable<CustomerCarResponse>>> GetAllAsync();

        Task<Result> UpdateAsync(int carId, CarRequest request);

    }
}
