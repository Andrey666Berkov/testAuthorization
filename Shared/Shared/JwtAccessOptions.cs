namespace Shared;

public class JwtAccessOptions
{
        public static string JWT = nameof(JWT) ;

        public string Audience { get; init; } = String.Empty;
        public string Issuer { get; init; }= String.Empty;
        public string Key { get; init; }= String.Empty;
        
        public int Expires { get; init; }
    
}

public class JwtRefreshOptions
{
        public static string JwtRefresh = nameof(JwtRefresh) ;

        public string Audience { get; init; } = String.Empty;
        public string Issuer { get; init; }= String.Empty;
        public string Key { get; init; }= String.Empty;
        
        public int Expires { get; init; }
    
}
