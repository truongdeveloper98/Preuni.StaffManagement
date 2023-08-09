using PreUni.StaffManagement.AdminWeb.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppServices(builder.Configuration);

builder.Services.AddControllers();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseCors("_myAllowSpecificOrigins");

}

app.UseCors("_myAllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();



app.MapControllerRoute(
   name: "default",
   pattern: "api/{controller}/{action}/{id?}");

//app.MapFallbackToFile("index.html");

app.MapControllers();

app.Run();
