namespace CarMaintenance.Infrastructre.Seeding
{
    public static class DefaultRole
    {
        public const string Admin = nameof(Admin);
        public const string AdminRoleId = "A5066782-F090-4C8E-9372-77BB4F6A5D2D";
        public const string AdminRoleConcurrencyStamp = "3BB4708A-19D7-4C76-BD01-D5109374B291";

        public const string Provider = nameof(Provider);
        public const string ProviderRoleId = "0FC0730C-FCF7-4514-8ED8-27CB4B232FFC";
        public const string ProviderRoleConcurrencyStamp = "F4FA1400-6AF3-4E5A-8F25-88CA5C3C3006";

        public const string Member = nameof(Member);
        public const string MemberRoleId = "352AB560-5ADB-4A2A-A768-E206124E8F70";
        public const string MemberRoleConcurrencyStamp = "63557FE8-153A-4E20-B2CD-44FDDE734E10";
    }
}
