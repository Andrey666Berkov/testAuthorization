using System.Globalization;

namespace Shared.Error;

public class ErrorMy
{
    public string Message { get; set; }
    public string Code { get; set; }
    public Errortype ErrorType { get; set; }

    private ErrorMy(string errorMessage, string code, Errortype errorType)
    {
        Message = errorMessage;
        Code = code;
        ErrorType = errorType;
    }
    public static ErrorMy Validation(string errorMessage, string errorCode)
    {
        return new ErrorMy(errorMessage, errorCode, Errortype.Validation);
    }
    
    public static ErrorMy NotFound(string errorMessage, string errorCode)
    {
        return new ErrorMy(errorMessage, errorCode, Errortype.NotFound);
    }
    
    public static ErrorMy Failure(string errorMessage, string errorCode)
    {
        return new ErrorMy(errorMessage, errorCode, Errortype.Failure);
    }
    
    public static ErrorMy Conflict(string errorMessage, string errorCode)
    {
        return new ErrorMy(errorMessage, errorCode, Errortype.Conflict);
    }
}

public  class ErrorsMy
{
    public class General
    {
        public static ErrorMy ValueValidation(string code)
        {
            string message = $"{code} is invalid";
            return ErrorMy.Validation(message,$"{code}.invalid.validation");
        }
        
        public static ErrorMy NotFound(string obj, string? id=null)
        {
            string message = !String.IsNullOrWhiteSpace(id)? $"Object {obj} id={id} is not found" : $"{obj} is not found";
            return ErrorMy.NotFound(message,$"{obj}.value.notfound");
        }
        public static ErrorMy NotFound(string obj)
        {
            string message = $"object is not found";
            return ErrorMy.NotFound(message,$"object.value.notfound");
        }
        public static ErrorMy Failure(string code)
        {
            string message = $"{code} is failure";
            return  ErrorMy.Failure(message,$"{code}.bad.failure");
        }
        
        public static ErrorMy Conflict(string code)
        {
            string message = $"{code} is Conflict";
            return ErrorMy.Validation(message,$"{code}.bad.Conflict");
        }
    }
}

public enum Errortype
{
    Non,
    Validation,
    NotFound,
    Failure,
    Conflict,
}