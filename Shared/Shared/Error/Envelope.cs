namespace Shared.Error;

public class Envelope
{
    public object? Object { get; set; }

    public ErrorMy? Errory { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;

    private Envelope(object? obj, ErrorMy? error)
    {
        Object = obj;
        Errory = error;
    }

    public static Envelope Ok(object obj)
    {
        return new Envelope(obj, null);
    }
    
    public static Envelope Error(ErrorMy error)
    {
        return new Envelope(null, error);
    }
}