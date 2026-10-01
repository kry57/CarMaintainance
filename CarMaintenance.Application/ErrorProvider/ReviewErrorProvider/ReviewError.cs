using CarMaintenance.Application.Abstractions;


namespace CarMaintenance.Application.ErrorProvider.ReviewErrorProvider
{
    public static  class ReviewError
    {
        //public static Error NotAssignedToPvovider => new("Product.NotAssignedToPvovider", "There is no product to this provider id !");
        public static Error NotAdded => new("Review.NotAdded", "There is an invalid thing in save to DB !");
        //public static Error NotDeleted => new("Product.NotDeleted", "There is an operation to delete !");
        public static Error CannotReviewOwnProvider => new("Review.CannotReviewOwnProvider", "you can review your providers !");
        public static Error NotAuthorized => new("Review.NotAuthorized", "there is no customer by this id !");
        public static Error NotFound => new("Review.NotFound", "There is no review by this id ");
        //public static Error NotFoundImage => new("Product.NotFoundImage", "There is no image by this values ! ");
        public static Error NotUpdated => new("Review.NotUpdated", "invalid to update !");
        public static  Error Duplicated = new("Review.Duplicated", "You already have a Review  with same comment and provider !");

        //public static  Error InvalidStockQuantity = new("Product.InvalidStockQuantity", "Quantity cannot be negative.");
    }
}
