namespace Store.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = 500;

        var response = new
        {
            IsSuccess = false,
            Error = new
            {
                Code = "Server.Error",
                Message = exception.Message
            }
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}