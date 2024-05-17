using System;
using System.Collections.Generic;

namespace CourtApp.Application.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static class Dashboard
        {
            public const string View = "Permissions.Dashboard.View";
            public const string Create = "Permissions.Dashboard.Create";
            public const string Edit = "Permissions.Dashboard.Edit";
            public const string Delete = "Permissions.Dashboard.Delete";
        }

        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }

        public static class Brands
        {
            public const string View = "Permissions.Brands.View";
            public const string Create = "Permissions.Brands.Create";
            public const string Edit = "Permissions.Brands.Edit";
            public const string Delete = "Permissions.Brands.Delete";
        }       

        public static class Cases
        {
            public const string View = "Permissions.Cases.View";
            public const string Create = "Permissions.Cases.Create";
            public const string Edit = "Permissions.Cases.Edit";
            public const string Delete = "Permissions.Cases.Delete";
        }

        public static class Publishers
        {
            public const string View = "Permissions.Publishers.View";
            public const string Create = "Permissions.Publishers.Create";
            public const string Edit = "Permissions.Publishers.Edit";
            public const string Delete = "Permissions.Publishers.Delete";
        }

        public static class Subjects
        {
            public const string View = "Permissions.Subjects.View";
            public const string Create = "Permissions.Subjects.Create";
            public const string Edit = "Permissions.Subjects.Edit";
            public const string Delete = "Permissions.Subjects.Delete";
        }

        public static class BookTypes
        {
            public const string View = "Permissions.BookTypes.View";
            public const string Create = "Permissions.BookTypes.Create";
            public const string Edit = "Permissions.BookTypes.Edit";
            public const string Delete = "Permissions.BookTypes.Delete";
        }

        public static class Books
        {
            public const string View = "Permissions.Books.View";
            public const string Create = "Permissions.Books.Create";
            public const string Edit = "Permissions.Books.Edit";
            public const string Delete = "Permissions.Books.Delete";
        }
        public static class Clients
        {
            public const string View = "Permissions.Clients.View";
            public const string Create = "Permissions.Clients.Create";
            public const string Edit = "Permissions.Clients.Edit";
            public const string Delete = "Permissions.Clients.Delete";
        }

        public static class ProceedingHeads
        {
            public const string View = "Permissions.PHead.View";
            public const string Create = "Permissions.PHead.Create";
            public const string Edit = "Permissions.PHead.Edit";
            public const string Delete = "Permissions.PHead.Delete";
        }

        public static class ProceedingSubHeads
        {
            public const string View = "Permissions.PSHead.View";
            public const string Create = "Permissions.PSHead.Create";
            public const string Edit = "Permissions.PSHead.Edit";
            public const string Delete = "Permissions.PSHead.Delete";
        }

        public static class CaseWorks
        {
            public const string View = "Permissions.CaseWorks.View";
            public const string Create = "Permissions.CaseWorks.Create";
            public const string Edit = "Permissions.CaseWorks.Edit";
            public const string Delete = "Permissions.CaseWorks.Delete";
        }

        public static class CaseSWorks
        {
            public const string View = "Permissions.CaseSWorks.View";
            public const string Create = "Permissions.CaseSWorks.Create";
            public const string Edit = "Permissions.CaseSWorks.Edit";
            public const string Delete = "Permissions.CaseSWorks.Delete";
        }
        public static class CaseHearing
        {
            public const string View = "Permissions.Hearing.View";
            public const string Create = "Permissions.Hearing.Create";
            public const string Edit = "Permissions.Hearing.Edit";
            public const string Delete = "Permissions.Hearing.Delete";
            public const string Proceeding = "Permissions.Hearing.Proceeding";
            public const string Work = "Permissions.Hearing.Work";
            public const string BringToday = "Permissions.Hearing.BToday";
        }
    }
}