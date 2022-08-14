namespace HR_SYSTEM_V1.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete"

            };
        }

        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();

            var modules = Enum.GetValues(typeof(Modules));

            foreach (var module in modules)
                allPermissions.AddRange(GeneratePermissionsList(module.ToString()));

            return allPermissions;
        }

        public static class Employee
        {
            public const string View = "Permissions.Employee.View";
            public const string Create = "Permissions.Employee.Create";
            public const string Edit = "Permissions.Employee.Edit";
            public const string Delete = "Permissions.Employee.Delete";
        }

        public static class GenralSettings
        {
            public const string View = "Permissions.GenralSettings.View";
            public const string Create = "Permissions.GenralSettings.Create";
            public const string Edit = "Permissions.GenralSettings.Edit";
            public const string Delete = "Permissions.GenralSettings.Delete";
        }

        public static class Premissions
        {
            public const string View = "Permissions.Premissions.View";
            public const string Create = "Permissions.Premissions.Create";
            public const string Edit = "Permissions.Premissions.Edit";
            public const string Delete = "Permissions.Premissions.Delete";
        }
        
        public static class AddNewGroup
        {
            public const string View = "Permissions.AddNewGroup.View";
            public const string Create = "Permissions.AddNewGroup.Create";
            public const string Edit = "Permissions.AddNewGroup.Edit";
            public const string Delete = "Permissions.AddNewGroup.Delete";
        }
        
        public static class Attendance
        {
            public const string View = "Permissions.Attendance.View";
            public const string Create = "Permissions.Attendance.Create";
            public const string Edit = "Permissions.Attendance.Edit";
            public const string Delete = "Permissions.Attendance.Delete";
        }
        
        public static class Salaryreport
        {
            public const string View = "Permissions.Salaryreport.View";
            public const string Create = "Permissions.Salaryreport.Create";
            public const string Edit = "Permissions.Salaryreport.Edit";
            public const string Delete = "Permissions.Salaryreport.Delete";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }


    }
}
