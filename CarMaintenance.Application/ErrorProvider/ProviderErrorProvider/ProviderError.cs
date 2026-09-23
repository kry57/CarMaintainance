using CarMaintenance.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.ErrorProvider.ProviderErrorProvider
{
    public static class ProviderError
    {
        public static readonly Error InvalidData = new("Provider.InvalidData", "Invalid provider data.");
        public static readonly Error DuplicatedProvider = new("Provider.Duplicated", "You already have a provider registered with this name.");
        public static readonly Error NotFound = new("Provider.NotFound", "there is no user by this id !");
        public static readonly Error NotFoundByThisCategory = new("Provider.NotFoundByThisCategory", "this provider does not provide a this type of a category ! ");
        public static readonly Error NoProviderToThisOwner = new("Provider.NoProviderToThisOwner", "this owner has no providers yet !");
    }
}
