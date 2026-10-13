namespace CarMaintainance.API.Filters
{
    public class HasPermissionAttribute(string Permission) : AuthorizeAttribute(Permission) { }
}
