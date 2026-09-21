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
        public readonly static Error InvaidCredentails = new("User.InvaidCredentails", "Wrong Email/Password !");
    }
}
