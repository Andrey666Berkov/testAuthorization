namespace Infrastructure;

public class JwtOPtions
{
        public static string JWT =nameof(JWT) ;
        
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
    
}
