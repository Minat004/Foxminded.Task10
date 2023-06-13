using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using University.Core.Interfaces;
using University.Core.Models;
using University.Core.Services;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories;
using University.WPF.Services;
using University.WPF.Views;

namespace University.WPF;

public partial class App
{
    private IHost? AppHost { get; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json");
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContextFactory<UniversityDbContext>(options =>
                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("UniversityConnection")));

                services.AddSingleton<MainWindow>();

                services.AddScoped<IDialogService, DialogService>();
                services.AddScoped<ICourseService<Course>, CourseService>();
                services.AddScoped<ICourseRepository<Course>, CourseRepository>();
                services.AddScoped<IGroupRepository<Group>, GroupRepository>();
                services.AddScoped<IGroupService<Group>, GroupService>();
                services.AddScoped<ITeacherRepository<Teacher>, TeacherRepository>();
                services.AddScoped<ITeacherService<Teacher>, TeacherService>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        
        base.OnExit(e);
    }
}