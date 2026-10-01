using AutoMapper;
using Azure.Core;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.CarErrorProvider;
using CarMaintenance.Application.ErrorProvider.ReviewErrorProvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Cars;
using CarMaintenance.Infrastructre.Context;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class CarRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService) : ICarsRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<CarResponse>> AddAsync(CarRequest request)
        {
            var customerId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(customerId))
                return Result<CarResponse>.Failure(ReviewError.NotAuthorized);

            var model = Normalize(request.Model);
            var make = Normalize(request.Make);
            var plate = Normalize(request.PlateNumber);

            var isDuplicated = await _context.Cars.AnyAsync(c =>
                c.CustomerId == customerId &&
                c.Year == request.Year &&
                c.Model.Trim().ToLower().Replace(" ", "") == model &&
                c.Make.Trim().ToLower().Replace(" ", "") == make &&
                c.PlateNumber.Trim().ToLower().Replace(" ", "") == plate);

            if (isDuplicated)
                return Result<CarResponse>.Failure(CarError.Duplicated);

            var car = _mapper.Map<Car>(request);
            car.CustomerId = customerId;
            await _context.Cars.AddAsync(car);

            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows <= 0)
                return Result<CarResponse>.Failure(CarError.NotAdded);

            return Result<CarResponse>.Success(_mapper.Map<CarResponse>(car));
        }

        private static string Normalize(string text) =>
            text.Trim().ToLower().Replace(" ", "");

        public async Task<Result> DeleteAsync(int carId)
        {
            var customerId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(customerId))
                return Result.Failure(ReviewError.NotAuthorized);

          
            var car = await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == carId && c.CustomerId == customerId);

            if (car is null)
                return Result.Failure(CarError.NotFound);

            car.IsDelete = true;

            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows <= 0)
                return Result.Failure(CarError.NotRemoved);

            return Result.Success();
        }

        public async  Task<Result> ToggleStatus(int carId)
        {
            var customerId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(customerId))
                return Result.Failure(ReviewError.NotAuthorized);


            var car = await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == carId && c.CustomerId == customerId);

            if (car is null)
                return Result.Failure(CarError.NotFound);

            car.IsDelete = !(car.IsDelete);
            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows <= 0)
                return Result.Failure(CarError.NotRemoved);

            return Result.Success();
        }

        public async Task<Result<CarResponse>> GetByIdAsync(int carId)
        {
            var customerId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(customerId))
                return Result<CarResponse>.Failure(ReviewError.NotAuthorized);

            var car = await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == carId && c.CustomerId == customerId);

            if (car is null)
                return Result<CarResponse>.Failure(CarError.NotFound);

            var response = _mapper.Map<CarResponse>(car);

            return Result<CarResponse>.Success(response);
        }

        public async Task<Result<IEnumerable<CustomerCarResponse>>> GetAllAsyncPerCustomer()
        {
            var result = await (from c in _context.Cars
                          group c by c.CustomerId into g
                          orderby g.Key
                          select new CustomerCarResponse
                          {
                              CustomerId = g.Key,
                              CarResponses = g.Select(car => new CarResponse
                              {
                                  Id = car.Id,
                                  Make = car.Make,
                                  Model = car.Model,
                                  Year = car.Year,
                                  PlateNumber = car.PlateNumber,
                                  CustomerId = g.Key

                              })
                          }).AsNoTracking().ToListAsync();

            return Result<IEnumerable<CustomerCarResponse>>.Success(result);

        }

        public async Task<Result<IEnumerable<CustomerCarResponse>>> GetAllAsync()
        {
            
            var cars = await _context.Cars
                .AsNoTracking()
                .Select(c => new
                {
                    
                    Car = new CarResponse
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        Year = c.Year,
                        PlateNumber = c.PlateNumber,
                        CustomerId = c.CustomerId


                    }
                })
                .ToListAsync();

            
            var result = (from x in cars
                          group x.Car by x.Car.CustomerId into g
                          orderby g.Key
                          select new CustomerCarResponse
                          {
                              CustomerId = g.Key,
                              CarResponses = g.ToList()
                          }).ToList();

            return Result<IEnumerable<CustomerCarResponse>>.Success(result);
        }

        public async Task<Result> UpdateAsync(int carId, CarRequest request)
        {
            var customerId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(customerId))
                return Result.Failure(ReviewError.NotAuthorized);


            var car = await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == carId && c.CustomerId == customerId);

            _mapper.Map(request, car);


            var rows = await _context.SaveChangesAsync();

            if (rows <= 0)
                return Result.Failure(CarError.NotUpdated);

            return Result.Success();
        }
    }
}
