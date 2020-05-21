using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.WebPages;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Ajax.Utilities;
using NUnit.Framework;

namespace Authorization.Models
{
    public static class Credentials
    {
        private static Dictionary<string, string> credentials = new Dictionary<string, string>();

        public static CredResult Register(string login, string password)
        {
            if (login.IsEmpty()) return CredResult.LoginIsEmpty;
            if (password.IsEmpty()) return CredResult.PasswordIsEmpty;
            if (credentials.ContainsKey(login))
                return CredResult.LoginAlreadyExists;
            credentials.Add(login, Crypto.HashPassword(password));
            return CredResult.OK;
        }

        public static CredResult Auth(string login, string password)
        {
            if (!credentials.ContainsKey(login)) return CredResult.UnknownLogin;
            if (Crypto.VerifyHashedPassword(credentials[login], password)) return CredResult.OK;
            else return CredResult.WrongPassword;
        }
    }

    [TestFixture]
    public static class CredentialsTests
    {
        [Test]
        public static void RegisterTest()
        {
            Assert.AreEqual(CredResult.OK, Credentials.Register("l", "p"));
            Assert.AreEqual(CredResult.LoginAlreadyExists, Credentials.Register("l", "p"));
            Assert.AreEqual(CredResult.OK, Credentials.Auth("l", "p"));
            Assert.AreEqual(CredResult.WrongPassword, Credentials.Auth("l", "!p"));
            Assert.AreEqual(CredResult.UnknownLogin, Credentials.Auth("l!", "p"));
        }
    }
}