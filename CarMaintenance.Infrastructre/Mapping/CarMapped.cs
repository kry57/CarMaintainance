using AutoMapper;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Mapping
{
    public class CarMapped : Profile
    {
        public CarMapped()
        {
            CreateMap<CarRequest, Car>();
            CreateMap<Car, CarResponse>();
        }
    }
}
