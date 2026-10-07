namespace CarMaintenance.Infrastructre.OptionsPattern
{
    public class MailSettings
    {
        public readonly static string SectionName = "MailSettings";
        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Mail { get; set; } = default!;
    }
}
