namespace src.application.controllers;

public class ReadinessController
{
  public static string Route => "/readiness";
  public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
  public static Delegate Handle => Action;

  public static async Task<IResult> Action(object request, HttpContext http)
  {
    return Results.Ok(new { status = true });
  }
}
