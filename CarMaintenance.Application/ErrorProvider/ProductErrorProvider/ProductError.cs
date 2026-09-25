using CarMaintenance.Application.Abstractions;

namespace CarMaintenance.Application.ErrorProvider.ProductErrorProvider
{
    public static  class ProductError
    {
        public static Error NotAssignedToPvovider => new("Product.NotAssignedToPvovider", "There is no product to this provider id !");
        public static Error NotAdded => new("Product.NotAdded", "There is an invalid thing in save to DB !");
        public static Error NotDeleted => new("Product.NotDeleted", "There is an operation to delete !");
        public static Error NotFound => new("Product.NotFound", "There is no by this id and provider id  ");
        public static Error NotUpdated => new("Product.NotUpdated", "invalid to update !");
        public static readonly Error Duplicated= new("Product.Duplicated", "You already have a product  with this name and partType");

        public static readonly Error InvalidStockQuantity = new("Product.InvalidStockQuantity", "Quantity cannot be negative.");
        //public static Error NotRemoved => new("Db.NotRemoved", "There is an invalid thing may be in save to DB  or not found ! ");
        //public static Error NotFound => new("Db.NotFound", "There is  not found ! ");
        //public static Error NotUpdated => new("Db.NotUpdated", "There is an invalid thing in update to DB !");
    }
}
