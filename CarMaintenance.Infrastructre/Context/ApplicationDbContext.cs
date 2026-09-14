using CarMaintenance.Infrastructre.Identity;

namespace CarMaintenance.Infrastructre.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor; 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /*Assembelies*/
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserEntityType).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ProviderEntityType).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ProductEntityType).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ProviderCategoryTypeEntityType).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(SubscriptionEntityType).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(RegistrationRequestEntityType).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(CarEntityType).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ReviewEntityType).Assembly);
            /*Change FK Behaviour*/
            var cascadeFks = builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()).Where(e => e.DeleteBehavior == DeleteBehavior.Cascade && !e.IsOwnership);
            foreach(var fk in cascadeFks)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
           
           
        }
        /*Override  on  save chagnges for auditLogging*/
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var enteries = ChangeTracker.Entries<AuditableEntity>();
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NotFound"; 
            foreach(var entry in enteries)
            {
                if(entry.State == EntityState.Added)
                {
                    var entity = entry.Entity;
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    var entity = entry.Entity;
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = userId;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<ProviderCategoryType> ProviderCategoryTypes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RegistrationRequest> RegistrationRequests { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}