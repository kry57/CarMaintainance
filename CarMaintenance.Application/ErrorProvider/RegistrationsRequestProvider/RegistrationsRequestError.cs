using CarMaintenance.Application.Abstractions;


namespace CarMaintenance.Application.ErrorProvider.RegistrationsRequestProvider
{
    public  static class RegistrationsRequestError
    {
        //public static Error NotAssignedToPvovider => new("Product.NotAssignedToPvovider", "There is no product to this provider id !");
        //Registration
        public static Error NotAdded => new("Registration.NotAdded", "There is an invalid thing in save to DB !");
        public static Error TimeSlotAlreadyBooked => new("Registration.TimeSlotAlreadyBooked", "There is an invalid thing in save to DB !");
        public static Error NotAllowed => new("Registration.NotAllowed", "There is an invalid thing in save to DB !");
        public static Error CannotCancel => new("Registration.CannotCancel", "There is an invalid thing in save to DB !");
        public static Error InvalidAppointmentTime => new("Registration.InvalidAppointmentTime", "There is an invalid thing in save to DB !");
        //public static Error NotDeleted => new("Product.NotDeleted", "There is an operation to delete !");
        public static Error CannotReviewOwnProvider => new("Review.CannotReviewOwnProvider", "you can review your providers !");
        public static Error CannotRespondToOwnRequest => new("Registration.CannotRespondToOwnRequest", "you can review your providers !");
        public static Error NoHasCar => new("Review.NoHasCar", "you have not register the car data ! ");
        public static Error NotAuthorized => new("Registration.NotAuthorized", "there is no customer by this id !");
        public static Error NotFound => new("Registration.NotFound", "There is no review by this id ");
        //public static Error NotFoundImage => new("Product.NotFoundImage", "There is no image by this values ! ");
        public static Error NotUpdated => new("Registration.NotUpdated", "invalid to update !");
        public static Error Duplicated = new("Registration.Duplicated", "You already have a Review  with same comment and provider !");

        //public static  Error InvalidStockQuantity = new("Product.InvalidStockQuantity", "Quantity cannot be negative.");
    }
}
