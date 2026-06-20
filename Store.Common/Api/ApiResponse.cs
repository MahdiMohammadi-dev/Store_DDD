using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Store.Common.Api;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public Error? Error { get; set; }

    public static ApiResponse<T> Success(T data)
        => new()
        {
            IsSuccess = true,
            Data = data
        };

    public static ApiResponse<T> Fail(Error error)
        => new()
        {
            IsSuccess = false,
            Error = error
        };
}