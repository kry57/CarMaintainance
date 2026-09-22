namespace CarMaintenance.Infrastructre.OptionsPattern
{
    public  class JWTOptions
    { 
        public readonly static string  SectionName = "JwtSettings" ;
        public string Key { get; set; } = string.Empty; 
        public string Issuer { get; set; } = string.Empty; 
        public string Audience { get; set; } = string.Empty; 
        public int ExpiryMinutes { get; set; } 

    }
}
