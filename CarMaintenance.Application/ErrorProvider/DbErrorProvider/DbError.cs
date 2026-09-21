using CarMaintenance.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.ErrorProvider.DbErrorProvider
{
    public static  class DbError
    {
        public static Error NotAdded => new("Db.NotAdded", "There is an invalid thing in save to DB !");
        public static Error NotRemoved => new("Db.NotRemoved", "There is an invalid thing may be in save to DB  or not found ! ");
        public static Error NotFound => new("Db.NotFound", "There is  not found ! ");
        public static Error NotUpdated => new("Db.NotUpdated", "There is an invalid thing in update to DB !");
    }
}
