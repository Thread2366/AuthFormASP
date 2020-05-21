using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authorization.Models
{
    public enum CredResult
    {
        OK,
        WrongPassword,
        UnknownLogin,
        LoginAlreadyExists,
        LoginIsEmpty,
        PasswordIsEmpty
    }
}