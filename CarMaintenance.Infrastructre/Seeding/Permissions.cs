namespace CarMaintenance.Infrastructre.Seeding
{
    public static class Permissions
    {
        public const string Type = "Permissions";
        // still permissions -_- 

        // Account
        public const string ViewAccount = "account:view";
        public const string UpdateAccount = "account:update";
        public const string ChangePassword = "account:change-password";
        // Cars
        public const string ViewCars = "cars:view";
        public const string AddCars = "cars:add";
        public const string UpdateCars = "cars:update";
        public const string DeleteCars = "cars:delete";
        public const string ToggleCarsStatus = "cars:toggle-status";
        // Diagnosis
        public const string UseDiagnosis = "diagnosis:use";

        // Products
        public const string ViewProducts = "products:view";
        public const string AddProducts = "products:add";
        public const string UpdateProducts = "products:update";
        public const string DeleteProducts = "products:delete";
        public const string ToggleProductsStatus = "products:toggle-status";

        // Product Images
        public const string ViewProductImages = "product-images:view";
        public const string AddProductImages = "product-images:add";
        public const string DeleteProductImages = "product-images:delete";
        // Providers
        public const string ViewProviders = "providers:view";
        public const string AddProviders = "providers:add";
        public const string UpdateProviders = "providers:update";
        public const string DeleteProviders = "providers:delete";
        public const string ToggleProvidersStatus = "providers:toggle-status";
        public const string VerifyProviders = "providers:verify";
        // Registration Requests
        public const string ViewProviderRequests = "registration-requests:view-provider";
        public const string RespondToRequests = "registration-requests:respond";
        // Reviews
        public const string ViewReviews = "reviews:view";
        public const string AddReviews = "reviews:add";
        public const string UpdateReviews = "reviews:update";
        public const string DeleteReviews = "reviews:delete";
        public const string ToggleReviewsStatus = "reviews:toggle-status";
        // Subscriptions
        public const string ViewSubscriptions = "subscriptions:view";
        public const string AddSubscriptions = "subscriptions:add";
        public const string CancelSubscriptions = "subscriptions:cancel";
        public const string RenewSubscriptions = "subscriptions:renew";
        public const string UpgradeSubscriptions = "subscriptions:upgrade";
        public const string DowngradeSubscriptions = "subscriptions:downgrade";
        // Users
        public const string ViewUsers = "users:view";
        public const string AddUsers = "users:add";
        public const string UpdateUsers = "users:update";
        // Roles
        public const string ViewRoles = "roles:view";
        public const string AddRoles = "roles:add";
        public const string UpdateRoles = "roles:update";
        public const string DeleteRoles = "roles:delete";
        public const string ToggleRolesStatus = "roles:toggle-status"; 

        public static IList<string?> GetAllPermissions() =>  typeof(Permissions).GetFields().Select(f => f.GetValue(f) as string).ToList();
    }

}
