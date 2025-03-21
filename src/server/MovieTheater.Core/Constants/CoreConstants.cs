using System;

namespace MovieTheater.Core.Constants;

public class CoreConstants
{
    public static readonly Guid SystemAdministratorId = Guid.Parse("c7fddaee-0c2b-44d8-0c87-08dd624a30f6");

    public struct Schemas
    {
        public const string Common = "Common";
        public const string Security = "Security";
    }

    public struct UserRoles
    {
        public const string SystemAdministrator = "systemadministrator";
    }

    public struct RoleConstants
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Employee = "Employee";
    }
}
