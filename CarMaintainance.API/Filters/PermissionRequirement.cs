namespace CarMaintainance.API.Filters
{
    public class PermissionRequirement(string Permission) : IAuthorizationRequirement
    {
        public string Permission { get; } = Permission;
    }
}
