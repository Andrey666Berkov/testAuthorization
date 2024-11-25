namespace Shared;

public class JwtOPtions
{
        public static string JWT = nameof(JWT) ;

        public string Audience { get; init; } = String.Empty;
        public string Issuer { get; init; }= String.Empty;
        public string Key { get; init; }= String.Empty;
        
        public int Expires { get; init; }
    
}
