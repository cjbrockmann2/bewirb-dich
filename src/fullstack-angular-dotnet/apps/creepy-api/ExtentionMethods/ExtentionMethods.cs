using Microsoft.AspNetCore.Diagnostics;

namespace CreepyApi.ExtentionMethods;

public static class ExtentionMethods
{
  public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
  {
      app.UseExceptionHandler(a => a.Run(async context =>
      {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;
        if (exception != null)
        {
          await context.Response.WriteAsJsonAsync(new { error = exception?.Message });
        }
      }));
  }
}
