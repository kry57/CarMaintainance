using CarMaintenance.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.ErrorProvider.UserErrorPrvider
{
    public static class UserError
    {
        public readonly static Error DuplicatedUser = new("User.DuplicatedUser","This Email Already has been registered before !");
        public readonly static Error NotFoundUser = new("User.NotFoundUser", "There is no User By This Email !");
        public readonly static Error NotUpdated = new("User.NotUpdated", "There is somethinf wrong !");
        public readonly static Error LockedOut = new("User.LockedOut", "This user is locked out for a few of minutes");
        public readonly static Error InvaidCredentails = new("User.InvaidCredentails", "Wrong Email/Password !");
        public readonly static Error DuplicatedConfirmation = new("User.DuplicatedConfirmation", "Confirmed  Before !");
        public readonly static Error ConfirmEmailFrist = new("User.ConfirmEmailFrist", "Confirme! the email then continue .");
        public readonly static Error UnEnabled = new("User.UnEnabled", "this feature not enabled !");
        public readonly static Error Free = new("User.Free", "already free !");
        public readonly static Error NotUnlocked = new("User.NotUnlocked", "un lockd now ");
    }
}
