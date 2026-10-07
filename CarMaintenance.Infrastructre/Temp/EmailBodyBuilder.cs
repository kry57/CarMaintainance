using System.Collections.Generic;
using System.IO;

namespace CarMaintenance.Infrastructre.Temp
{
    public static class EmailBodyBuilder
    {
        public static string GenerateEmailBody(string temp, Dictionary<string, string> placeholder)
        {
            var templatePath = Path.Combine(AppContext.BaseDirectory, "Temp", $"{temp}.html");

            var body = File.ReadAllText(templatePath);

            foreach (var item in placeholder)
                body = body.Replace(item.Key, item.Value);

            return body;
        }
    }
}