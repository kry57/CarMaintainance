using CarMaintenance.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.ErrorProvider.CarErrorProvider
{
    public static class CarError
    {
        public static Error NotAdded => new("Car.NotAdded", "There is an invalid thing in save to DB !");
        public static Error Duplicated = new("Car.Duplicated", "You already have a Car  with same data !");
        public static Error NotRemoved => new("Car.NotRemoved", "There is an invalid thing may be in save to DB  or not found ! ");
        public static Error NotFound => new("Car.NotFound", "There is  not found ! ");
        public static Error NotUpdated => new("Car.NotUpdated", "There is an invalid thing ");
    }
}
