using CarMaintenance.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.ErrorProvider.SubscriptionErrorProvider
{
    public static  class SubscriptionError
    {
        //Subscription
        //public static Error NotAssignedToPvovider => new("Product.NotAssignedToPvovider", "There is no product to this provider id !");
        public static Error NotAdded => new("Subscription.NotAdded", "There is an invalid thing in save to DB !");
        ////public static Error NotDeleted => new("Product.NotDeleted", "There is an operation to delete !");
        //public static Error CannotReviewOwnProvider => new("Review.CannotReviewOwnProvider", "you can review your providers !");
        public static Error NotAuthorized => new("Subscription.NotAuthorized", "there is no owner by this id !");
        public static  Error InvalidPlan  = new("Subscription.InvalidPlan", "Plan does not exist");
        public static Error NotFound => new("Subscription.NotFound", "There is no Subscription by this id ");
        public static readonly Error SamePlan = new("Subscription.SamePlan", "Provider is already on this plan");
        public static Error NotUpdated => new("Subscription.NotUpdated", "invalid to update !");
        public static  Error NotAnUpgrade = new("Subscription.NotAnUpgrade", "New plan must be higher than the current plan");
        public static  Error NotADowngrade = new("Subscription.NotADowngrade", "New plan must be lower than the current plan");
        public static Error AlreadySubscribed = new("Subscription.AlreadySubscribed", "You already have a Subscription  with same provider !");

        public static Error AlreadyCancelled = new("Subscription.AlreadyCancelled", "You already cancelled iyt ! !");

        //public static  Error InvalidStockQuantity = new("Product.InvalidStockQuantity", "Quantity cannot be negative.");
    }
}
