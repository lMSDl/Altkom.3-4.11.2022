//class Program {
//public static void Main(string[] args)
//{
using Microsoft.Extensions.FileProviders;
using Models;
using Services.Interfaces;
using Services.Bogus;
using Services.Bogus.Fakers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(x => x.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Program))); //automatyczne t³umaczenie adnotacji
        builder.Services.AddDirectoryBrowser();

        builder.Services.AddSingleton<IService<User>, Service<User>>();
        builder.Services.AddTransient<EntityFaker<User>, UserFaker>();

builder.Services.AddLocalization(x => x.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(x =>
{
    x.SetDefaultCulture("en");
    x.AddSupportedCultures("en-US", "pl");
    x.AddSupportedUICultures("en-US", "pl");
});


var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        var options = new DefaultFilesOptions();
        options.DefaultFileNames.Clear();
        //options.DefaultFileNames.Add("startpage.html");
        app.UseDefaultFiles(options);

        app.UseStaticFiles();

        /*app.UseStaticFiles(new StaticFileOptions { 
            //Microsoft.Extensions.FileProviders
            FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Public")),
            RequestPath = "/publiczny",
            OnPrepareResponse = x => x.Context.Response.Headers.Append("Cache-Control", "public, max-age=60000")
        });

        app.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Public")),
            RequestPath = "/publiczny",
        });*/
        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Public")),
            RequestPath = "/publiczny",
            EnableDirectoryBrowsing = true,
        });


        app.UseRequestLocalization();
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=home}/{action=index}/{id?}");
        app.MapControllerRoute(
            name: "default",
            pattern: "greetings/{controller=Hello}/{action=Hi}/{userInput?}");

app.Run();

    //}
//}
