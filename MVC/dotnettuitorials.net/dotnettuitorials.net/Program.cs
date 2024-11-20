
using dotnettuitorials.net.Repository;

public class Program
{ 
    public static void Main(string[] args)
    {

        
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddMvc();

        //Singleton
        //builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
        //builder.Services.AddSingleton<SomeOtherService>();

        //Scoped
        //builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        //builder.Services.AddScoped<SomeOtherService>();
        //Transeint
        builder.Services.AddTransient<IStudentRepository, StudentRepository>();
        builder.Services.AddTransient<SomeOtherService>();

        var app = builder.Build();

        app.UseRouting();
        //app.MapDefaultControllerRoute();
        //app.MapGet("/", () => "Hello World!");
        app.MapControllerRoute(
                name: "default", // Name of the route
                pattern: "{controller=Home}/{action=Index}/{id?}" // URL pattern for the route
            );

        app.Run();

    }
}
