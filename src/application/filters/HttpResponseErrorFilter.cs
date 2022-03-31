using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
  public int Order => int.MaxValue - 10;

  public void OnActionExecuting(ActionExecutingContext context) { }

  public void OnActionExecuted(ActionExecutedContext context)
  {
    if (context.Exception is HttpResponseException httpResponseException)
    {
      var errorBody = new
      {
        message = httpResponseException.Message,
      };

      context.Result = new ObjectResult(httpResponseException.Value ?? errorBody)
      {
        StatusCode = httpResponseException.StatusCode
      };

      System.Console.Write($"\n\n\n\n" +
        $"{context.Result}\n " +
        $"{context.Result}\n\n\n\n");

      context.ExceptionHandled = true;
    }
  }
}